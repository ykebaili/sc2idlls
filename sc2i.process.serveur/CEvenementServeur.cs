using System;
using System.Collections;

using sc2i.data;
using sc2i.data.serveur;

using sc2i.common;
using sc2i.process;
using System.Data;

namespace sc2i.process.serveur
{
	/// <summary>
	/// Description résumée de CEvenementServeur.
	/// </summary>
	public class CEvenementServeur : CObjetServeurAvecBlob
	{
#if PDA
		//-------------------------------------------------------------------
		public CEvenementServeur(  )
			:base (  )
		{
		}
#endif
		//-------------------------------------------------------------------
		public CEvenementServeur( int nIdSession )
			:base ( nIdSession )
		{
		}

		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CEvenement.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			
			try
			{
				CEvenement evt = (CEvenement)objet;

                if (evt.Libelle.Trim() == string.Empty)
                    result.EmpileErreur(I.T("The Event label could not be empty|114"));

                if (!CObjetDonneeAIdNumerique.IsUnique(evt, CEvenement.c_champLibelle, evt.Libelle))
                    result.EmpileErreur("An Event with the same label already exist|116");

				if ( evt.TypeCible == null )
					result.EmpileErreur(I.T("Target Type incorrect|107"));
				if ( evt.TypeEvenement == TypeEvenement.Date )
				{
					if ( evt.ProprieteSurveillee == null )
                        result.EmpileErreur(I.T("The supervised property is not valid|108"));
					else
					{
						if ( evt.ProprieteSurveillee.TypeDonnee.TypeDotNetNatif != typeof(DateTime) &&
                            evt.ProprieteSurveillee.TypeDonnee.TypeDotNetNatif != typeof(CDateTimeEx) && 
                            evt.ProprieteSurveillee.TypeDonnee.TypeDotNetNatif != typeof(DateTime?))
							result.EmpileErreur(I.T("The supervised property must be of the Date/Time type|109"));
					}
				}
				
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
			return typeof(CEvenement);
		}
		//-------------------------------------------------------------------
        public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
        {
            CResultAErreur result = base.TraitementAvantSauvegarde(contexte);
            if (!result)
                return result;
            DataTable table = contexte.Tables[GetNomTable()];
            if (table == null)
                return result;
            ArrayList lst = new ArrayList(table.Rows);
            foreach ( DataRow row in new ArrayList(table.Rows) )
            {
                if ( row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified )
                {
                    CEvenement evt = new CEvenement ( row );
                    if ( evt.TypeEvenement == TypeEvenement.Suppression )
                    {
                        if ( !evt.DeclencherSurContexteClient )
                        {
                            evt.DeclencherSurContexteClient = true;
                        }
                        if ( !evt.DeclencherSurContexteClient )
                        {
                            result.EmpileErreur(I.T("Event @1 contains not compatible with 'Delete' event elements|20002",evt.Libelle ));
                            return result;
                        }
                    }
                }
            }
            return result;
        }
	}
}
