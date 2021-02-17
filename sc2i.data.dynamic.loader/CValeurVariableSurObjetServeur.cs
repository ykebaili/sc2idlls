using System;
using System.Collections;
using System.Data;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CValeurVariableSurObjetServeur : CObjetServeur, IObjetServeur
	{
#if PDA
		///////////////////////////////////////////////////
		public CValeurVariableSurObjetServeur (  )
			:base (  )
		{
		}
#endif
		
		///////////////////////////////////////////////////
		public CValeurVariableSurObjetServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CValeurVariableSurObjet.c_nomTable;
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CValeurVariableSurObjet valeur = (CValeurVariableSurObjet)objet;
			
				if (valeur.Nom == "")
					result.EmpileErreur(I.T("The variable name cannot be empty|155"));
				if ( valeur.ElementLie == null )
					result.EmpileErreur(I.T("The linked element cannot be null|156"));
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CValeurVariableSurObjet);
		}

		/////////////////////////////////////////////////////
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			DataTable table = contexte.Tables[GetNomTable()];
			if ( table != null )
			{
				ArrayList lst = new ArrayList ( table.Rows );
				foreach ( DataRow row in lst )
				{
					if ( row.RowState == DataRowState.Modified || 
						row.RowState == DataRowState.Added )
					{
						if ( row[CValeurVariableSurObjet.c_champValeur].ToString().Trim()=="") 
							row.Delete();
					}
				}
			}
			return result;
		}
	}
}
