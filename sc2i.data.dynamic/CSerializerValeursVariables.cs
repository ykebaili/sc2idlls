using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Runtime.Remoting;
using System.Data;
using System.IO;

using sc2i.expression;
using sc2i.common;
using sc2i.data;


namespace sc2i.data.dynamic
{
	public class CSerializerValeursVariables : MarshalByRefObject
	{
		/// ////////////////////////////////////////////////////////
		private static int GetNumVersion()
		{
			return 14;
			//12 : correction bug : Avant, seules les variablesTypeComplexes etaient sérialisées
            //13 : serialisation des éléments [Serializable]
            //14 : Passage de l'Id de l'ObjetDonnee à CDbKey
		}

		/// ////////////////////////////////////////////////////////
		public static CResultAErreur SerializeValeurVariable(
			ref object valeur,
			IVariableDynamique variable,
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
		public static CResultAErreur SerializeValeurVariable( 
			ref object valeur, 
			IVariableDynamique variable,  
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
			else
			{
				if (nVersion <= 11 && variable.GetType().ToString().IndexOf("CVariableProcessTypeComplexe") < 0)
				{
					//Avant, on ne serialisait que les variables complexes
					return result;
				}
				Type tp = variable.TypeDonnee.TypeDotNetNatif;
				if (tp == typeof(int) || tp == typeof(double) || tp == typeof(string) ||
					tp == typeof(DateTime) || tp == typeof(bool))
				{
					if (nVersion >= 5)
					{
                        if (variable.TypeDonnee.IsArrayOfTypeNatif)
                        {
                            IList lst = valeur as IList;
                            serializer.TraiteListeObjetsSimples(ref lst);
                            if ( serializer.Mode == ModeSerialisation.Lecture )
                                valeur = lst;
                        }
                        else
						    serializer.TraiteObjetSimple(ref valeur);
					}
				}
				else if (typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(tp))
				#region ObjetAIdNumerique AUTO
				{
					if (!variable.TypeDonnee.IsArrayOfTypeNatif)
					{
						IObjetDonneeAIdNumerique objet = (IObjetDonneeAIdNumerique)valeur;
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
									foreach (IObjetDonneeAIdNumerique objet in (IList)valeur)
									{
										IObjetDonneeAIdNumerique objetForRef = objet;
										SerializeObjetAIdAuto(nVersion, objetForRef == null ? tp : objetForRef.GetType(), ref objetForRef, serializer, contexteDonnee);
									}
								}
								break;
							case ModeSerialisation.Lecture:
								ArrayList lst = new ArrayList();
								for (int nElt = 0; nElt < nNb; nElt++)
								{
									IObjetDonneeAIdNumerique element = null;
									SerializeObjetAIdAuto(nVersion, tp, ref element, serializer, contexteDonnee);
									if (element != null)
										lst.Add(element);
								}
								valeur = (IObjetDonneeAIdNumerique[])lst.ToArray(typeof(IObjetDonneeAIdNumeriqueAuto));
								break;
						}
					}
				}
                else if (variable.TypeDonnee.TypeDotNetNatif.GetCustomAttributes(typeof(SerializableAttribute), true).Length > 0)
                {
                    if (nVersion >= 13)
                    {
                        bool bHasObjet = valeur != null;
                        serializer.TraiteBool(ref bHasObjet);
                        if (bHasObjet)
                            result = serializer.TraiteSerializable(ref valeur);
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
			ref IObjetDonneeAIdNumerique valeur, 
			C2iSerializer serializer,
			CContexteDonnee contexte )
        {

            //TESTDBKEYTODO : REmplacer l'id par un DbKey
            CDbKey dbKey = null;

            if (valeur is CObjetDonnee && ((CObjetDonnee)valeur).Row.RowState != DataRowState.Deleted
                && ((CObjetDonnee)valeur).Row.RowState != DataRowState.Detached)
                dbKey = valeur.DbKey;
            if (nVersion < 14)
                serializer.ReadDbKeyFromOldId(ref dbKey, typeObjet);
            else
                serializer.TraiteDbKey(ref dbKey);

			if (nVersion >= 8 && dbKey != null)
				serializer.TraiteType(ref typeObjet);
			if (dbKey != null && serializer.Mode == ModeSerialisation.Lecture)
			{
				valeur = (IObjetDonneeAIdNumerique)Activator.CreateInstance(typeObjet, new object[] { contexte });
				if (!valeur.ReadIfExists(dbKey))
					valeur = null;
			}
		}
	}
}
