using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using sc2i.multitiers.client;
using sc2i.multitiers.server;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.common;


namespace CTesteurBaseAccess
{
    public class CSessionClientSurServeurSagexProSolo: CSessionClientSurServeur , IInfoUtilisateur
    {
		///////////////////////////////////////////////////////
		public CSessionClientSurServeurSagexProSolo(CSessionClient sessionSurClient)
			: base(sessionSurClient)
		{

		}

        public override void ChangeUtilisateur(int nIdUtilisateur)
        {
            
        }

        public override IInfoUtilisateur GetInfoUtilisateur()
        {
            return this;
        }

        public override string GetVersionServeur()
        {
            return "";
        }

        public override void MyCloseSession()
        {
            
        }

        #region IInfoUtilisateur Membres

        public IDonneeDroitUtilisateur GetDonneeDroit(string strDroit)
        {
            return null;
        }

        public CListeRestrictionsUtilisateurSurType GetListeRestrictions(int? nIdVersion)
        {
            return new CListeRestrictionsUtilisateurSurType(true);
        }

        public CRestrictionUtilisateurSurType GetRestrictionsSur(Type tp, int? nIdVersion)
        {
            return new CRestrictionUtilisateurSurType(tp);
        }

        public CRestrictionUtilisateurSurType GetRestrictionsSurObjet(object objet, int? nIdVersion)
        {
            if (objet.GetType() != null)
                return new CRestrictionUtilisateurSurType(objet.GetType());
            return null;
        }

        public ReadOnlyCollection<Type> GetTypesARestrictionsSurObjets(int? nIdVersion)
        {
            return new List<Type>().AsReadOnly();
        }

        public int IdUtilisateur
        {
            get { return 0; }
        }

        public int[] ListeIdsGroupes
        {
            get { return new int[0]; }
        }

        public string NomUtilisateur
        {
            get { return "Anonymous"; }
        }

        #endregion
    }
}
