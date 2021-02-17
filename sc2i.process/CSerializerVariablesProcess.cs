using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Runtime.Remoting;
using System.Data;
using System.IO;

using sc2i.expression;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.data;


namespace sc2i.process
{
	public class CSerializerValeursVariablesProcess : MarshalByRefObject
	{
		/// ////////////////////////////////////////////////////////
		private static int GetNumVersion()
		{
			return 11;
			//Passage du CProcess à la serialization dans cette classe
		}

		/// ////////////////////////////////////////////////////////
		public static CResultAErreur SerializeValeurVariable(
			ref object valeur,
			CVariableDynamique variable,
			C2iSerializer serializer,
			CContexteDonnee contexteDonnee)
		{
			return SerializeValeurVariable(
				ref valeur,
				variable,
				serializer,
				contexteDonnee,
				false);
		}

		/// ////////////////////////////////////////////////////////
		internal static CResultAErreur SerializeValeurVariable( 
			ref object valeur, 
			CVariableDynamique variable,  
			C2iSerializer serializer, 
			CContexteDonnee contexteDonnee,
			bool bAvecCompatibiliteSurVersionQuiTravaillaitDirectementDansCProcess)
		{
			int nVersion = GetNumVersion();
			CResultAErreur  result = CResultAErreur.True;
			if (!bAvecCompatibiliteSurVersionQuiTravaillaitDirectementDansCProcess)
				result = serializer.TraiteVersion(ref nVersion);
			if (!result) 
				return result;
			if (variable is CVariableDynamiqueSaisie ||
				variable is CVariableDynamiqueSelectionObjetDonnee)
			{
				result = serializer.TraiteObjetSimple(ref valeur);
			}
			if (variable is CVariableProcessTypeComplexe)
			{
				Type tp = variable.TypeDonnee.TypeDotNetNatif;
				if (tp == typeof(int) || tp == typeof(double) || tp == typeof(string) ||
					tp == typeof(DateTime) || tp == typeof(bool))
				{
					if (nVersion >= 5)
					{
						serializer.TraiteObjetSimple(ref valeur);
					}
				}
				else if (typeof(IObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(tp))
				#region ObjetAIdNumerique AUTO

				{
					if (!variable.TypeDonnee.IsArrayOfTypeNatif)
					{
						IObjetDonneeAIdNumeriqueAuto objet = (IObjetDonneeAIdNumeriqueAuto)valeur;
						SerializeObjetAIdAuto(nVersion, objet == null ? tp : objet.GetType(), ref objet, serializer, contexteDonnee);
						valeur = objet;
					}
					else
					{
						int nNb = 0;
						if (valeur != null)
							nNb = ((IList)valeur).Count;
						serializer.TraiteInt(ref nNb);
						switch (serializer.Mode)
						{
							case ModeSerialisation.Ecriture:
								if (valeur != null)
								{
									foreach (IObjetDonneeAIdNumeriqueAuto objet in (IList)valeur)
									{
										IObjetDonneeAIdNumeriqueAuto objetForRef = objet;
										SerializeObjetAIdAuto(nVersion, objetForRef == null ? tp : objetForRef.GetType(), ref objetForRef, serializer, contexteDonnee);
									}
								}
								break;
							case ModeSerialisation.Lecture:
								ArrayList lst = new ArrayList();
								for (int nElt = 0; nElt < nNb; nElt++)
								{
									IObjetDonneeAIdNumeriqueAuto element = null;
									SerializeObjetAIdAuto(nVersion, tp, ref element, serializer, contexteDonnee);
									if (element != null)
										lst.Add(element);
								}
								valeur = (IObjetDonneeAIdNumeriqueAuto[])lst.ToArray(typeof(IObjetDonneeAIdNumeriqueAuto));
								break;
						}
					}
				}
				#endregion
			}
			return result;
		}

		/// ////////////////////////////////////////////////////////
		private static void SerializeObjetAIdAuto(
			int nVersion, 
			Type typeObjet, 
			ref IObjetDonneeAIdNumeriqueAuto valeur, 
			C2iSerializer serializer,
			CContexteDonnee contexte )
		{
			int nId = -1;
			if (valeur is CObjetDonnee && ((CObjetDonnee)valeur).Row.RowState != DataRowState.Deleted
				&& ((CObjetDonnee)valeur).Row.RowState != DataRowState.Detached)
				nId = valeur.Id;
			serializer.TraiteInt(ref nId);
			if (nVersion >= 8 && nId >= 0)
				serializer.TraiteType(ref typeObjet);
			if (nId != -1 && serializer.Mode == ModeSerialisation.Lecture)
			{
				valeur = (IObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance(typeObjet, new object[] { contexte });
				if (!valeur.ReadIfExists(nId))
					valeur = null;
			}
		}
	}

	public class CFournisseurValeursSerializeesProcess : IFournisseurProprietesDynamiques
	{
		private CProcess m_process = null;
		private CContexteDonnee m_contexte = null;

		//------------------------------------------------------------
		public CFournisseurValeursSerializeesProcess(CProcess process, CContexteDonnee contexte)
		{
			m_process = process;
			m_contexte = contexte;
		}

		//------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux)
		{
			return m_process.GetDefinitionsChamps ( tp, nNbNiveaux );
		}

		//------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
		{
			return m_process.GetDefinitionsChamps ( tp, nNbNiveaux, defParente );
		}

		//------------------------------------------------------------
		public object GetValeurPropriete(object objetInterroge, CDefinitionProprieteDynamique propriete)
		{
			if (objetInterroge is CProcess && propriete is CDefinitionProprieteDynamiqueVariableDynamique)
			{
				byte[] bt = m_process.GetSerialisationValeurVariable(((CDefinitionProprieteDynamiqueVariableDynamique)propriete).IdChamp);
				if (bt == null)
					return null;
				MemoryStream stream = new MemoryStream(bt);
				BinaryReader reader = new BinaryReader(stream);
				CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
				object valeur = null;
				CVariableDynamique variable = m_process.GetVariable(((CDefinitionProprieteDynamiqueVariableDynamique)propriete).IdChamp);
				CResultAErreur result = CSerializerValeursVariablesProcess.SerializeValeurVariable(ref valeur, variable, serializer, m_contexte);
				if (result)
					return valeur;
				return null;
			}
			else
				return CInterpreteurProprieteDynamique.GetValue(objetInterroge, propriete.NomPropriete).Data;
		}

	}

}
