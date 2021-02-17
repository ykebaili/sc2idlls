using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.multitiers.client
{
    [Serializable]
    [DynamicClass("Active session information")]
    public class CInfoSessionAsDynamicClass
    {
        public class CDureeSession : IComparable<CDureeSession>
        {
            public CDureeSession(TimeSpan span)
            {
                Duree = span;
            }

            public TimeSpan Duree { get; set; }

            private string GetDuree(TimeSpan span)
            {
                string strChaine = "";
                if (span.TotalDays > 1)
                    strChaine = ((int)span.TotalDays).ToString() + I.T("d|20507");
                if (span.TotalHours > 0)
                    strChaine += ((int)(span.TotalHours % 24)).ToString() + I.T("h|20508");
                strChaine += ((int)(span.TotalMinutes % 60)).ToString() + I.T("m|20509");
                return strChaine;
            }

            public override string ToString()
            {
                return GetDuree(Duree);
            }

            #region IComparable<CDuree> Membres

            public int CompareTo(CDureeSession other)
            {
                return Duree.CompareTo(other.Duree);
            }

            #endregion
        }

        public CInfoSessionAsDynamicClass()
        {
            Invalide = false;
        }

        [DynamicField("Is system session")]
        public bool IsSystem { get; set; }

        [DynamicField("Is invalid")]
        public bool Invalide { get; set; }

        [DynamicField("Session id")]
        public int IdSession { get; set; }

        [DynamicField("User id")]
        public CDbKey KeyUtilisateur { get; set; }

        [DynamicField("User name")]
        public string NomUtilisateur { get; set; }

        [DynamicField("Connection date")]
        public DateTime? DateDebutConnexion { get; set; }

        [DynamicField("Last activity date")]
        public DateTime? DateDerniereActivité { get; set; }

        [DynamicField("Session label")]
        public string LibelleSession { get; set; }

        [DynamicField("Connection duration")]
        public CDureeSession DureeConnexion
        {
            get
            {
                if (DateDebutConnexion != null)
                    return new CDureeSession(DateTime.Now - DateDebutConnexion.Value);
                return null;
            }
        }

        [DynamicField("Inactivity duration")]
        public CDureeSession DureeInactivité
        {
            get
            {
                if (DateDerniereActivité != null)
                    return new CDureeSession(DateTime.Now - DateDerniereActivité.Value);
                return null;
            }
        }

        [DynamicMethod("Close current Session")]
        public bool CloseSession()
        {
            CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
            if (session != null)
            {
                try
                {
                    session.CloseSession();
                    return true;
                }
                catch
                {
                    try
                    {
                        IGestionnaireSessions gestionnaire = (IGestionnaireSessions)C2iFactory.GetNewObject(typeof(IGestionnaireSessions));
                        ISessionClientSurServeur sessionSurServeur = gestionnaire.GetSessionClientSurServeur(IdSession);
                        sessionSurServeur.CloseSession();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

    }
}
