using System;
using System.IO;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.documents;

#if !PDA_DATA

namespace sc2i.documents.serveur
{
	/// <summary>
	/// Description résumée de C2iRapportCrystalServeur.
	/// </summary>
	public class C2iRapportCrystalServeur : CObjetServeurAvecBlob
	{
		private const string c_strNewCodeEtat = "NEW_CODE_ETAT_CRYSTAL";

		public C2iRapportCrystalServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return C2iRapportCrystal.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				C2iRapportCrystal rapport = (C2iRapportCrystal)objet;

				if (rapport.Libelle == "")
					result.EmpileErreur(I.T("The report label cannot be empty|103"));

				if (!CObjetDonneeAIdNumerique.IsUnique(rapport, C2iRapportCrystal.c_champLibelle, rapport.Libelle))
					result.EmpileErreur(I.T("Another report exists with this label|104"));
					
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(C2iRapportCrystal);
		}
		//-------------------------------------------------------------------
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			DataTable table = contexte.Tables[GetNomTable()];
			foreach ( DataRow row in table.Rows )
			{
				C2iRapportCrystal rapport = new C2iRapportCrystal ( row );
				if ( row.RowState == DataRowState.Modified )
					rapport.NumVersion++;	
				else if ( row.RowState == DataRowState.Added )
				{
					lock(this)
					{
						CDatabaseRegistre registre = new CDatabaseRegistre(m_nIdSession);
						rapport.CodeEtat = (int) registre.GetValeurLong(c_strNewCodeEtat, 1000);
						registre.SetValeur(c_strNewCodeEtat,(rapport.CodeEtat+1).ToString()); 
					}
				}
			}
			return result;
		}
		//-------------------------------------------------------------------
	}
}
#endif