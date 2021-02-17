using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.common;

namespace data.hotel.easyquery.win32.entitysource
{
    public partial class CPanelEditSourceEntites : UserControl
    {
        #region Gestion des éditeurs
        private static Dictionary<Type, Type> m_dicTypeSourceToEditeur = new Dictionary<Type, Type>();
        private static Dictionary<Type, string> m_dicTypeSourceToLibelleType = new Dictionary<Type, string>();

        //---------------------------------------------------------------------------------------------------------
        public static void RegisterTypeEditeurSource ( Type typeSource, Type typeEditeur, string strLibelleType )
        {
            m_dicTypeSourceToEditeur[typeSource] = typeEditeur;
            m_dicTypeSourceToLibelleType[typeSource] = strLibelleType;
        }

        //---------------------------------------------------------------------------------------------------------
        private static IEnumerable<Type> TypesEditeurs
        {
            get
            {
                List<Type> lst = new List<Type>(m_dicTypeSourceToLibelleType.Keys);
                lst.Sort((x, y) => x.ToString().CompareTo(y.ToString()));
                return lst;
            }
        }

        //---------------------------------------------------------------------------------------------------------
        private static IEditeurSourceEntite GetEditeur ( Type tp )
        {
            Type tpEditeur = null;
            if ( m_dicTypeSourceToEditeur.TryGetValue ( tp, out tpEditeur ))
            {
                IEditeurSourceEntite editeur = Activator.CreateInstance(tpEditeur) as IEditeurSourceEntite;
                return editeur;
            }
            return null;
        }

        //---------------------------------------------------------------------------------------------------------
        private static string GetLibelleTypeSource(Type typeSource)
        {
            string strValue = typeSource.ToString();
            m_dicTypeSourceToLibelleType.TryGetValue(typeSource, out strValue);
            return strValue;
        }
        #endregion

        private ISourceEntitesPourTableDataHotel m_sourceEnCours = null;
        private IEditeurSourceEntite m_editeurEnCours = null;
        private CODEQTableFromDataHotel m_table = null;

        //---------------------------------------------------------------------------------------------------------
        public CPanelEditSourceEntites()
        {
            InitializeComponent();
        }

        private class CCoupleTypeLibelle
        {
            private Type m_typeSource;
            private string m_strLibelle;

            public CCoupleTypeLibelle(Type typeSource, string strLibelle)
            {
                m_strLibelle = strLibelle;
                m_typeSource = typeSource;
            }

            public string Libelle
            {
                get
                {
                    return m_strLibelle;
                }
            }

            public Type TypeSource
            {
                get
                {
                    return m_typeSource;
                }
            }
        }

        //---------------------------------------------------------------------------------------------------------
        public void Init ( ISourceEntitesPourTableDataHotel source,
            CODEQTableFromDataHotel table)
        {
            m_table = table;
            if (source == null)
                m_sourceEnCours = new CSourceEntitesPourTableDataHotelFormule();
            else
                m_sourceEnCours = source;
            List<CCoupleTypeLibelle> lstCouples = new List<CCoupleTypeLibelle>();
            CCoupleTypeLibelle coupleSel = null;
            foreach ( Type tp in TypesEditeurs )
            {
                CCoupleTypeLibelle couple = new CCoupleTypeLibelle(tp, GetLibelleTypeSource(tp));
                if (tp == m_sourceEnCours.GetType())
                    coupleSel = couple;
                lstCouples.Add(couple);
            }
            m_cmbTypeSource.ProprieteAffichee = "Libelle";
            m_cmbTypeSource.ListDonnees = lstCouples;
            m_cmbTypeSource.SelectedValue = coupleSel;
            UpdateAspect();
        }

        //---------------------------------------------------------------------------------------------------------
        private void UpdateAspect()
        {
            if (m_sourceEnCours == null)
                m_sourceEnCours = new CSourceEntitesPourTableDataHotelFormule();
            IEditeurSourceEntite editeur = GetEditeur(m_sourceEnCours.GetType());
            if (editeur == null)
            {
                m_panelEditeSource.ClearAndDisposeControls();
            }
            else
            {
                if (m_editeurEnCours == null || m_editeurEnCours.GetType() != editeur.GetType())
                {
                    m_panelEditeSource.ClearAndDisposeControls();
                    m_editeurEnCours = editeur;
                    ((Control)editeur).Parent = m_panelEditeSource;
                    ((Control)editeur).Dock = DockStyle.Fill;
                    CWin32Traducteur.Translate(editeur);
                }
                editeur.Init(m_sourceEnCours, m_table);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        public CResultAErreurType<ISourceEntitesPourTableDataHotel> MajChamps()
        {
            if (m_editeurEnCours != null)
                return m_editeurEnCours.MajChamps();
            CResultAErreurType<ISourceEntitesPourTableDataHotel> res = new CResultAErreurType<ISourceEntitesPourTableDataHotel>();
            res.EmpileErreur(I.T("Valid source for entity must be set|20003"));
            return res;
        }

        //---------------------------------------------------------------------------------------------------------
        private void m_cmbTypeSource_SelectionChangeCommitted(object sender, EventArgs e)
        {
           CCoupleTypeLibelle couple = m_cmbTypeSource.SelectedValue as CCoupleTypeLibelle;
           Type tp = couple != null ? couple.TypeSource : null;
            if (tp == null)
                return;
            if ( m_sourceEnCours == null || m_sourceEnCours.GetType() != tp )
            {
                m_sourceEnCours = Activator.CreateInstance(tp) as ISourceEntitesPourTableDataHotel;
            }
            UpdateAspect();
        }

        
       
    }
}
