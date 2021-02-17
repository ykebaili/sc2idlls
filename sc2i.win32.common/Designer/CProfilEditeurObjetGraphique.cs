using sc2i.common;

namespace sc2i.win32.common
{


    public class CProfilEditeurObjetGraphique : I2iSerializable
    {
        public CProfilEditeurObjetGraphique()
        {

        }

        private EFormePoignee m_formePoignees;
        public EFormePoignee FormeDesPoignees
        {
            get
            {
                return m_formePoignees;
            }
            set
            {
                m_formePoignees = value;
            }
        }


        private bool m_bHistorisationActive;
        public bool HistorisationActive
        {
            get
            {
                return m_bHistorisationActive;
            }
            set
            {
                m_bHistorisationActive = value;
            }
        }

        private int m_nbHistorisation;
        public int NombreHistorisation
        {
            get
            {
                return m_nbHistorisation;
            }
            set
            {
                m_nbHistorisation = value;
            }
        }

        private int m_nMarge;
        public int Marge
        {
            get
            {
                return m_nMarge;
            }
            set
            {
                m_nMarge = value;
            }
        }

        private bool m_bTjrsAlignerSurGrille;
        public bool ToujoursAlignerSurLaGrille
        {
            get
            {
                return m_bTjrsAlignerSurGrille;
            }
            set
            {
                m_bTjrsAlignerSurGrille = value;
            }
        }
        private EModeAffichageGrille m_modeAffichageGrille;
        public EModeAffichageGrille ModeAffichageGrille
        {
            get
            {
                return m_modeAffichageGrille;
            }
            set
            {
                m_modeAffichageGrille = value;
            }
        }
        private CGrilleEditeurObjetGraphique m_grille;
        public CGrilleEditeurObjetGraphique Grille
        {
            get
            {
                return m_grille;
            }
            set
            {
                m_grille = value;
            }
        }


		#region I2iSerializable Membres
		private int GetNumVersion()
		{
			return 0;
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			
			serializer.TraiteInt(ref m_nbHistorisation);
			
			int nModeAffichage = (int)m_modeAffichageGrille;
			serializer.TraiteInt(ref nModeAffichage);
			m_modeAffichageGrille = (EModeAffichageGrille)nModeAffichage;


			serializer.TraiteBool(ref m_bTjrsAlignerSurGrille);
			serializer.TraiteInt(ref m_nMarge);

			int nFormePoignees = (int)m_formePoignees;
			serializer.TraiteInt(ref nFormePoignees);
			m_formePoignees = (EFormePoignee)nFormePoignees;

			I2iSerializable grille = m_grille;
			serializer.SerializeObjet(ref grille);
			m_grille = (CGrilleEditeurObjetGraphique)grille;

			return result;
		}

		#endregion
	}

}
