using System;
using System.Data;
using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.multitiers.server;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de C2iExporteurSurServeur.
	/// </summary>
	public class C2iExporteurSurServeur : C2iObjetServeur,I2iExporteurSurServeur
	{
		/// //////////////////////////////////////////////////////////
		public C2iExporteurSurServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// //////////////////////////////////////////////////////////
		public CResultAErreur ExportData ( string strChaineSerializationListeObjetDonnee, C2iStructureExport structure, IElementAVariablesDynamiquesAvecContexteDonnee elementAVariablesDynamiquesPourFiltre, IIndicateurProgression indicateur )
		{
			System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
			//Déserialise la liste d'objets
			CStringSerializer serializer = new CStringSerializer ( strChaineSerializationListeObjetDonnee, ModeSerialisation.Lecture );
			C2iSponsor sponsor = new C2iSponsor();
			sponsor.Register ( indicateur );
			CResultAErreur  result = CResultAErreur.True;
			try
			{
				using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
				{
					I2iSerializable objet = null;
					contexte.GestionParTablesCompletes = true;
					result = serializer.TraiteObject(ref objet, contexte);
					if (!result)
						return result;
					if (objet == null || !(objet is CListeObjetsDonnees))
					{
						result.EmpileErreur(I.T("Error during the deserialization of source list|101"));
						return result;
					}
					CListeObjetsDonnees listeSource = (CListeObjetsDonnees)objet;
					listeSource.RemplissageProgressif = false;
					structure.TraiterSurServeur = false;
					DataSet ds = new DataSet();
					result = structure.Export(IdSession, listeSource, ref ds, elementAVariablesDynamiquesPourFiltre, indicateur);
					if (result)
						result.Data = ds;
					return result;
				}
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			finally
			{
				sponsor.Unregister(indicateur);
			}
			return result;

		}
	}
}
