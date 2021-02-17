using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites
{
    [Serializable]
    public class CUniteCustom : IUnite
    {
        private string m_strLibelleLong;
        private string m_strLibelleCourt;
        private string m_strId;
        private string m_strClasseUniteId;

        private double m_fFacteurVersBase = 1;
        private double m_fOffsetVersBase = 0;

        //------------------------------------
        public CUniteCustom()
        {
        }


        //------------------------------------
        public string LibelleLong
        {
            get
            {
                return m_strLibelleLong;
            }
            set
            {
                m_strLibelleLong = value;
            }
        }

        //------------------------------------
        public string LibelleCourt
        {
            get{
                return m_strLibelleCourt;
            }
            set
            {
                m_strLibelleCourt = value;
            }
        }
        

        //------------------------------------
        public string GlobalId
        {
            get
            {
                return m_strId;
            }
            set
            {
                m_strId = value.ToUpper();
            }
        }

        //------------------------------------
        public IClasseUnite Classe
        {
            get {
                return CGestionnaireUnites.GetClasse(m_strClasseUniteId);
            }
            set{
                if (value != null)
                    m_strClasseUniteId = value.GlobalId;
                else
                    m_strClasseUniteId = "";
            }
        }

        //------------------------------------
        public double FacteurVersBase
        {
            get
            {
                return m_fFacteurVersBase;
            }
            set
            {
                m_fFacteurVersBase = value;
            }
        }

        //------------------------------------
        public double OffsetVersBase
        {
            get
            {
                return m_fOffsetVersBase;
            }
            set
            {
                m_fOffsetVersBase = value;
            }
        }

        //------------------------------------
    }
}
