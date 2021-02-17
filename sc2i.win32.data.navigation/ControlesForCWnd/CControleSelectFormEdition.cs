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
    public partial class CControleSelectFormEdition : UserControl, ISelectionneurReferenceFormEdition
    {

        private Type m_typeElement = null;

		IWindowsFormsEditorService m_service = null;
        
        public CControleSelectFormEdition()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CSelecteurDeFormEdition.SetTypeEditeur(typeof(CControleSelectFormEdition));
        }


		private IReferenceFormEdition m_reference = null;
		private IReferenceFormEdition ReferenceFormEdition
		{
			get
			{
				return m_reference;
			}
			set
			{
				m_reference = value;
			}
		}
        
         //------------------------------------------------------------------------------
        private void CControleSelectFormEdition_Load(object sender, EventArgs e)
        {
            if (m_typeElement == null)
                return;

            m_listBox.Items.Clear();
            m_listBox.Items.Add("(None)");

			foreach ( CReferenceTypeForm refTypeForm in CFormFinder.GetReferencesTypeToEdit ( m_typeElement ) )
				m_listBox.Items.Add ( refTypeForm );
			m_listBox.SelectedItem = ReferenceFormEdition;
        }

        //------------------------------------------------------------------------------
        private void m_listBox_SelectedValueChanged(object sender, EventArgs e)
        {
			m_reference = m_listBox.SelectedItem as IReferenceFormEdition;
            if (m_service!= null)
				m_service.CloseDropDown();
        }


		
		public IReferenceFormEdition GetReferenceFormEdition(Type typeElements, IServiceProvider provider, object value)
		{
			m_typeElement = typeElements;

            // Uses the IWindowsFormsEditorService to display a 
            // drop-down UI in the Properties window.
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                m_service = edSvc;
                // Initialise le dernier filtre sélectionné
                ReferenceFormEdition = value as IReferenceFormEdition;
                
                // Affiche le controle dans une fenêtre déroulante
                edSvc.DropDownControl(this);

				return ReferenceFormEdition;
            }

			return null;
      
        }
	}
}
