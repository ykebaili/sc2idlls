using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites.standard
{
    [Serializable]
    public class CUniteStandard : IUnite
    {
        private string m_strLibelleLong;
        private string m_strLibelleCourt;
        private string m_strId;
        private string m_strClasseUniteId;

        private double m_fFacteurVersBase = 1;
        private double m_fOffsetVersBase = 0;

        //------------------------------------
        public CUniteStandard(
            string strLibelleLong,
            string strLibelleCourt,
            string strId,
            IClasseUnite classe,
            double fFacteurVersBase,
            double fOffsetVersBase)
        {
            m_strLibelleLong = strLibelleLong;
            m_strLibelleCourt = strLibelleCourt;
            m_strId = strId;
            m_strClasseUniteId = classe.GlobalId;
            m_fFacteurVersBase = fFacteurVersBase;
            m_fOffsetVersBase = fOffsetVersBase;
        }


        //------------------------------------
        public string LibelleLong
        {
            get
            {
                return m_strLibelleLong;
            }
        }

        //------------------------------------
        public string LibelleCourt
        {
            get{
                return m_strLibelleCourt;
            }
        }
        

        //------------------------------------
        public string GlobalId
        {
            get
            {
                return m_strId;
            }
        }

        //------------------------------------
        public IClasseUnite Classe
        {
            get {
                return CGestionnaireUnites.GetClasse(m_strClasseUniteId);
            }
        }

        //------------------------------------
        public double FacteurVersBase
        {
            get
            {
                return m_fFacteurVersBase;
            }
        }

        //------------------------------------
        public double OffsetVersBase
        {
            get
            {
                return m_fOffsetVersBase;
            }
        }

        //------------------------------------
    }
}
