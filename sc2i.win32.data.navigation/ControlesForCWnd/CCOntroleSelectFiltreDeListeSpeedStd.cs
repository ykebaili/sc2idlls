using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;


using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.data.dynamic;
using sc2i.win32.data.dynamic;
using sc2i.data;


namespace sc2i.win32.data.navigation.ControlesForCWnd
{
	[AutoExec("Autoexec")]
    public partial class CControleSelectFiltreDeListeSpeedStd : UserControl, IEditeurProprieteFiltreDynamique
    {

        private Type m_typeElement = null;

		private IWindowsFormsEditorService m_service= null;
        
        
        public CControleSelectFiltreDeListeSpeedStd()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CProprieteFiltreDynamiqueEditor.SetTypeEditeur(typeof(CControleSelectFiltreDeListeSpeedStd));
        }

        private CFiltreDynamiqueInDb m_filtre = null;
        private CFiltreDynamiqueInDb Filtre
        {
            get
            {
                return m_filtre;
            }
            set
            {
                m_filtre = value;
                //m_listBox.SelectedItem = m_filtre;
            }
        }

        
        #region IEditeurProprieteFiltreDynamique Membres

        public C2iWndListeSpeedStandard.CPointeurFiltreDynamiqueInDb EditeFiltre(Type typeElement, IServiceProvider provider, object value)
        {
            m_typeElement = typeElement;

            // Uses the IWindowsFormsEditorService to display a 
            // drop-down UI in the Properties window.
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                m_typeElement = typeElement;
				m_service = edSvc;
                // Initialise le dernier filtre sélectionné
                CFiltreDynamiqueInDb filtre = new CFiltreDynamiqueInDb(CSc2iWin32DataClient.ContexteCourant);
                C2iWndListeSpeedStandard.CPointeurFiltreDynamiqueInDb ptrFilter = value as C2iWndListeSpeedStandard.CPointeurFiltreDynamiqueInDb;
                if (ptrFilter != null)
                {
                    if (filtre.ReadIfExists(ptrFilter.DbKey))
                        Filtre = filtre;
                }
                
                // Affiche le controle dans une fenêtre déroulante
                edSvc.DropDownControl(this);

                filtre = Filtre;
                if (filtre != null)
                {
                    if (ptrFilter == null)
                        ptrFilter = new C2iWndListeSpeedStandard.CPointeurFiltreDynamiqueInDb();
                    ptrFilter.DbKey = filtre.DbKey;
                    ptrFilter.Libelle = filtre.Libelle;
                    return ptrFilter;
                }
            }

            return new C2iWndListeSpeedStandard.CPointeurFiltreDynamiqueInDb();
      
        }

        #endregion

        //------------------------------------------------------------------------------
        private void CControleSelectFiltreDeListeSpeedStd_Load(object sender, EventArgs e)
        {
            if (m_typeElement == null)
                return;

            //Initialisation de la liste des filtres disponibles
            CListeObjetsDonnees listeFiltresPossibles = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, typeof(CFiltreDynamiqueInDb));
            listeFiltresPossibles.Filtre = new CFiltreData(CFiltreDynamiqueInDb.c_champTypeElements + "=@1", m_typeElement.ToString());
            
            m_listBox.Items.Clear();
            m_listBox.Items.Add("(None)");

            foreach (CFiltreDynamiqueInDb filtre in listeFiltresPossibles)
            {
                m_listBox.Items.Add(filtre);
            }
            

        }

        //------------------------------------------------------------------------------
        private void m_listBox_SelectedValueChanged(object sender, EventArgs e)
        {
            CFiltreDynamiqueInDb filtre = m_listBox.SelectedItem as CFiltreDynamiqueInDb;
            if (filtre != null)
                m_filtre = filtre;
            else
                m_filtre = null;
            if (m_service != null)
				m_service.CloseDropDown();
             
            
        }

    }
}
