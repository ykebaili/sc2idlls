using System;
using System.Data;

using sc2i.common;
using System.Collections;

namespace sc2i.data
{
    /// <summary>
    /// Description résumée de CDonneeBinaire.
    /// </summary>
    [Serializable]
    public class CDonneeBinaireInRow
    {
        [NonSerialized]
        private DataRow m_row;
        private int m_nIdSession;
        private string m_strChamp;

		//Données d'origine (à la lecture) du blob
		private byte[] m_donneesOriginales = null;
        private byte[] m_donnees;
        private bool m_bLoaded;
        private bool m_bHasChangeSinceRead;
        private DateTime? m_dateLastModification = null;


        ////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// loaderType : Type du loader
        /// strChamp : Champ contenant le blob
        /// strFiltreLigne : clause where indiquant la ligne dans la table
        /// </summary>
        /// <param name="loaderType"></param>
        /// <param name="strChamp"></param>
        /// <param name="strFiltreLigne"></param>
        public CDonneeBinaireInRow(int nIdSession, DataRow row, string strChamp)
        {
            m_row = row;
            m_nIdSession = nIdSession;
            m_strChamp = strChamp;
            m_donnees = null;
			m_donneesOriginales = null;
            m_bLoaded = false;
            m_bHasChangeSinceRead = false;
        }

        public CDonneeBinaireInRow(int nIdSession, DataRow row, string strChamp, byte[] data)
        {
            m_row = row;
            m_nIdSession = nIdSession;
            m_strChamp = strChamp;
            m_donnees = data;
            if (m_donnees != null)
                m_donneesOriginales = (byte[])data.Clone();
            else
                m_donneesOriginales = null;
            m_bLoaded = true;
            m_bHasChangeSinceRead = false;
        }

		////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Si la CDonneeBinaireInRow n'est pas liée à la row, renvoie un
		/// nouveau CDonneeBinaireInRow qui correspond à la bonne row
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public CDonneeBinaireInRow GetSafeForRow(DataRow row)
		{
			if (m_row != row)
			{
				CDonneeBinaireInRow newData = GetCloneForRow(row);
				CContexteDonnee.ChangeRowSansDetectionModification(row, m_strChamp, newData);
				return newData;
			}
			return this;
		}

        ////////////////////////////////////////////////////////////////////////////////////
        public DataRow Row
        {
            get
            {
                return m_row;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        public byte[] Donnees
        {
            get
            {
                AssureDonnees();
                return m_donnees;
            }
            set
            {
                m_bLoaded = true;
                m_bHasChangeSinceRead = true;
                m_donnees = value;
                //S'assure que la modification est détectée
                m_row[m_strChamp] = DBNull.Value;
                m_row[m_strChamp] = this;
                m_dateLastModification = DateTime.Now;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        public bool HasChange()
        {
            return m_bHasChangeSinceRead;
        }

        ////////////////////////////////////////////////////////////////////////////////////
        public void ForceHasChangeToFalse()
        {
            m_bHasChangeSinceRead = false;
            m_dateLastModification = null;
        }

        ////////////////////////////////////////////////////////////////////////////////////
        public DateTime? DateLastModification
        {
            get
            {
                return m_dateLastModification;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        private void AssureDonnees()
        {
            if (m_bLoaded)
                return;
            IObjetServeur loader = ((CContexteDonnee)m_row.Table.DataSet).GetTableLoader(m_row.Table.TableName);
            CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow(m_row.Table.PrimaryKey, m_row);
			ArrayList lstKeys = new ArrayList();
			foreach (DataColumn col in m_row.Table.PrimaryKey)
				lstKeys.Add(m_row[col]);
            CResultAErreur result = loader.ReadBlob(m_strChamp, lstKeys.ToArray());
            if (!result)
                throw new CExceptionErreur(result.Erreur);
            m_donnees = (byte[])result.Data;
			if (m_donnees != null)
				m_donneesOriginales = (byte[])m_donnees.Clone();
			else
				m_donneesOriginales = null;
            m_bLoaded = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		/// <param name="nIdVersionArchive">Id du CVersionDonnees auquel est associée la modification</param>
		/// <returns></returns>
		public CResultAErreur SaveData( int? nIdVersionArchive)
        {
            if (!m_bHasChangeSinceRead)
                return CResultAErreur.True;
            if (!m_bLoaded)
                return CResultAErreur.True;
            IObjetServeur loader = ((CContexteDonnee)m_row.Table.DataSet).GetTableLoader(m_row.Table.TableName);
			ArrayList lst = new ArrayList();
			foreach (DataColumn col in m_row.Table.PrimaryKey)
				lst.Add(m_row[col]);
			CResultAErreur result = loader.SaveBlob(m_strChamp, lst.ToArray(), m_donnees, nIdVersionArchive, m_donneesOriginales);
            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////
        public CDonneeBinaireInRow GetCloneForRow(DataRow row)
        {
            AssureDonnees();
            CDonneeBinaireInRow newDonnee = new CDonneeBinaireInRow(m_nIdSession, row, m_strChamp);
            newDonnee.m_bLoaded = m_bLoaded;
            newDonnee.m_bHasChangeSinceRead = m_bHasChangeSinceRead;
            if (m_donnees != null)
            {
				byte[] data = (byte[])m_donnees.Clone();
                newDonnee.m_donnees = data;
            }
            else
                newDonnee.m_donnees = null;
			if (m_donneesOriginales != null)
			{
				byte[] data = (byte[])m_donneesOriginales.Clone();
				newDonnee.m_donneesOriginales = data;
			}
			else
				newDonnee.m_donneesOriginales = null;
            return newDonnee;
        }


		public override string ToString()
		{
			return I.T("Blob|186");
		}

    }
}
