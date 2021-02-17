using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.data.serveur;

using sc2i.common;


#if !PDA_DATA

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CDroitUtilisateurServeur.
	/// </summary>
	public class CDroitUtilisateurServeur : CObjetServeur
	{
		public const string c_droitsDataSource = "USER_RIGHTS_DEF";

		//-------------------------------------------------------------------
		public CDroitUtilisateurServeur ( int nIdSession )
			:base ( nIdSession )
		{
			if ( !CSc2iDataServer.ExitsConnexion ( c_droitsDataSource ) )
				InitGestionDroits();
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CDroitUtilisateur.c_nomTable;
		}

        //-------------------------------------------------------------------
        public override bool  ActivateQueryCache
        {
            get
            {
                return false;
            }
        }
		//-------------------------------------------------------------------
		public override CResultAErreur SaveAll(CContexteSauvegardeObjetsDonnees contexteSauvegarde, DataRowState etatsAPrendreEnCompte)
		{
			return CResultAErreur.True;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CDroitUtilisateur);
		}
		//-------------------------------------------------------------------
		/// <summary>
		/// Initialise la gestion de droits.
		/// </summary>
		public static void InitGestionDroits()
		{
			if ( !CSc2iDataServer.ExitsConnexion ( c_droitsDataSource ) )
			{
				CSc2iDataServer.AddDefinitionConnexion(
					new CDefinitionConnexionDataSource(
					c_droitsDataSource,
					typeof(CGestionnaireDroitsUtilisateurs),
					"" ) );
			}
			CSc2iDataServer.SetIdConnexionForClasse(typeof(CDroitUtilisateurServeur), c_droitsDataSource);
		}
	}
}

#endif