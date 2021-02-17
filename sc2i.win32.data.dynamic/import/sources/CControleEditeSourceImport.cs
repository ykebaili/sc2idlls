using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.win32.common;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.expression;

namespace sc2i.win32.data.dynamic.import.sources
{
    public partial class CControleEditeSourceImport : UserControl
    {

        //----------------------------------------------------------------------------
        private static Dictionary<Type, Type> m_dicTypesEditeurs = new Dictionary<Type, Type>();

        private IEditeurSourceSmartImport m_currentEditeur = null;
        private object m_defaultValue = null;
        private DataTable m_sourceTable = null;
        private bool m_bIsDrawingImage = false;

        private CSetupSmartImportItem m_currentItem = null;

        private List<Type> m_typesSourcesValides = null;

        private Dictionary<Type, IEditeurSourceSmartImport> m_dicEditeurs = new Dictionary<Type, IEditeurSourceSmartImport>();

        //----------------------------------------------------------------------------
        public static void RegisterEditeur ( Type typeSource, Type typeEditeur )
        {
            m_dicTypesEditeurs[typeSource] = typeEditeur;
        }

        //----------------------------------------------------------------------------
        private Type GetTypeEditeur ( Type typeSource )
        {
            Type tp = null;
            m_dicTypesEditeurs.TryGetValue(typeSource, out tp);
            return tp;
        }

        private CSourceSmartImport m_source = new CSourceSmartImportNoImport();
        //----------------------------------------------------------------------------
        public CControleEditeSourceImport()
        {
            InitializeComponent();
        }

        //----------------------------------------------------------------------------
        public IEnumerable<Type> TypesSourcesValides
        {
            get
            { return m_typesSourcesValides; 
            }
            set
            {
                if (value == null)
                    m_typesSourcesValides = null;
                else
                    m_typesSourcesValides = new List<Type>(value);
            }
        }

        //----------------------------------------------------------------------------
        public DataTable SourceTable
        {
            get
            {
                return m_sourceTable;
            }
            set
            {
                m_sourceTable = value;
                foreach (IEditeurSourceSmartImport editeur in m_dicEditeurs.Values)
                    editeur.SourceTable = value;
            }
        }

        //----------------------------------------------------------------------------
        private void CControleEditeSourceImport_SizeChanged(object sender, EventArgs e)
        {
            //Ajuste l'image pour qu'elle ait une bonne tête
            m_imageType.Width = (int)(((double)ClientSize.Height / 32.0) * 64);
            m_panelOnlyOnCreate.Width = ClientSize.Height;
            m_imageFormula.Size = new Size(ClientSize.Height / 2, ClientSize.Height / 2);
            m_imageFormula.Left = m_panelOnlyOnCreate.ClientSize.Height - m_imageFormula.Width;
            m_imageFormula.Top = m_panelOnlyOnCreate.ClientSize.Width - m_imageFormula.Height;
        }

        //----------------------------------------------------------------------------
        private IEditeurSourceSmartImport GetEditeur ( CSourceSmartImport source )
        {
            if (source == null)
                return null;
            IEditeurSourceSmartImport editeur = null;
            if (!m_dicEditeurs.TryGetValue(source.GetType(), out editeur ))
            {
                Type tp = GetTypeEditeur(source.GetType());
                if ( tp != null )
                {
                    editeur = Activator.CreateInstance(tp, new object[0]) as IEditeurSourceSmartImport;
                    if (editeur != null)
                    {
                        m_dicEditeurs[source.GetType()] = editeur;
                        editeur.SourceTable = m_sourceTable;
                        editeur.ValueChanged += editeur_ValueChanged;
                    }
                }
            }
            return editeur;
        }

        public event EventHandler ValueChanged;

        void editeur_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, null);
        }


        //----------------------------------------------------------------------------
        private void m_imageType_Click(object sender, EventArgs e)
        {
            ToolStripDropDown menu = new ToolStripDropDown();
            foreach ( Type tp in CSourceSmartImport.GetTypesDeSources())
            {
                if (m_typesSourcesValides == null || m_typesSourcesValides.Contains(tp))
                {
                    ToolStripMenuItem itemTypeSource = new ToolStripMenuItem(CSourceSmartImport.GetTypeLabel(tp));
                    itemTypeSource.Tag = tp;
                    itemTypeSource.Checked = m_source != null && m_source.GetType() == tp;
                    itemTypeSource.Image = CSourceSmartImport.GetImage(tp);
                    itemTypeSource.Click += itemTypeSource_Click;
                    menu.Items.Add(itemTypeSource);
                }
            }
            menu.Show(this, new Point(0, Height));
        }


        //----------------------------------------------------------------------------
        void itemTypeSource_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if ( item != null )
            {
                SetTypeSource(item.Tag as Type);
                if ( SourceChanged != null )
                    SourceChanged ( this, null);
            }
        }

        //----------------------------------------------------------------------------
        private void SetTypeSource(Type tp)
        {
            if (m_source != null && m_source.GetType() == tp)
                return;
            if (tp == typeof(CSourceSmartImportNoImport))
                m_source = null;
            else
                m_source = Activator.CreateInstance(tp, new object[0]) as CSourceSmartImport;
            if (m_source != null && m_source.GetType() == typeof(CSourceSmartImportFixedValue))
                ((CSourceSmartImportFixedValue)m_source).Valeur = m_defaultValue;
                
            UpdateVisuel();
        }

        //----------------------------------------------------------------------------
        public void Init ( CSourceSmartImport source, CSetupSmartImportItem currentItem )
        {
            m_source = source;
            m_currentItem = currentItem;
            UpdateVisuel();
        }

        //----------------------------------------------------------------------------
        public CSourceSmartImport Source
        {
            get
            {
                if ( m_currentEditeur != null )
                    m_currentEditeur.MajChamps();
                return m_source;
            }
            
        }

        

        //----------------------------------------------------------------------------
        public object DefaultValue
        {
            get
            {
                return m_defaultValue;
            }
            set
            { m_defaultValue = value; }
        }

        //----------------------------------------------------------------------------
        public event EventHandler SourceChanged;

        //----------------------------------------------------------------------------
        public void SetIsDrawingImage(bool bIsDrawingImage)
        {
            m_bIsDrawingImage = bIsDrawingImage;
            if (m_currentEditeur != null)
                m_currentEditeur.SetIsDrawingImage(bIsDrawingImage);
        }
        
        //----------------------------------------------------------------------------
        private void UpdateVisuel()
        {
            if (m_source != null)
            {
                m_imageType.Image = m_source.GetImage();
                Type tp = GetTypeEditeur(m_source.GetType());
                if (m_currentEditeur != null && m_currentEditeur.GetType() != tp)
                {
                    m_panelEditeur.Controls.Remove((Control)m_currentEditeur);
                    m_currentEditeur = null;
                }

                IEditeurSourceSmartImport editeur = GetEditeur(m_source);
                if (editeur != null)
                {
                    m_currentEditeur = editeur;
                    ((Control)m_currentEditeur).Dock = DockStyle.Fill;
                    m_panelEditeur.Controls.Add((Control)m_currentEditeur);
                    editeur.SetIsDrawingImage(m_bIsDrawingImage);
                    editeur.SetSource(m_source, m_currentItem);
                }
            }
            else
            {
                m_imageType.Image = CSourceSmartImport.GetImage(typeof(CSourceSmartImportNoImport));
                if (m_currentEditeur != null)
                    m_panelEditeur.Controls.Remove((Control)m_currentEditeur);
                m_currentEditeur = null;
            }
            UpdateVisuelOnlyOnCreate();
        }

        private void m_panelEditeur_Paint(object sender, PaintEventArgs e)
        {

        }

        private void m_imageOnlyOnCreate_Click(object sender, EventArgs e)
        {
            ToolStripDropDown menu = new ToolStripDropDown();
            ToolStripMenuItem itemApply = new ToolStripMenuItem(new COptionImport(EOptionImport.OnUpdateAndCreate).Libelle);
            itemApply.Tag = EOptionImport.OnUpdateAndCreate;
            itemApply.Image = Properties.Resources.Apply_on_update_yes;
            itemApply.Click += itemApplyOnUpdate_Click;
            menu.Items.Add(itemApply);
            
            itemApply = new ToolStripMenuItem(new COptionImport(EOptionImport.OnCreate).Libelle);
            itemApply.Tag = EOptionImport.OnCreate;
            itemApply.Image = Properties.Resources.apply_on_update_no;
            itemApply.Click += itemApplyOnUpdate_Click;
            menu.Items.Add(itemApply);

            itemApply = new ToolStripMenuItem(new COptionImport(EOptionImport.Never).Libelle);
            itemApply.Tag = EOptionImport.Never;
            itemApply.Image = Properties.Resources.cancelx16;
            itemApply.Click += itemApplyOnUpdate_Click;
            menu.Items.Add(itemApply);

            menu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem itemFormula = new ToolStripMenuItem(I.T("Condition formula|20218"));
            itemFormula.Image = m_source != null && m_source.Condition != null && m_source.Condition.GetType() != typeof(C2iExpressionVrai) ?
                sc2i.win32.data.dynamic.Properties.Resources.Check :
                null;
            itemFormula.Click += itemFormula_Click;
            menu.Items.Add(itemFormula);

            if (m_source is CSourceSmartImportField )
            {
                ToolStripMenuItem itemOptionsNull = new ToolStripMenuItem(I.T("Null values options|20162"));
                itemOptionsNull.Image = m_source.OptionsValeursNulles != null?
                    sc2i.win32.data.dynamic.Properties.Resources.Check :
                null;
                itemOptionsNull.Click += itemOptionsNull_Click;
                menu.Items.Add(itemOptionsNull);
            }


            menu.Show(m_imageOnlyOnCreate, new Point(0, m_imageOnlyOnCreate.Height));

        }

        //------------------------------------------------------------
        void itemOptionsNull_Click(object sender, EventArgs e)
        {
            if ( m_source != null )
            {
                COptionsValeursNulles options = m_source.OptionsValeursNulles;
                if (CFormOptionsValeursNulle.EditeOptions(ref options))
                    m_source.OptionsValeursNulles = options;
            }
        }

        //------------------------------------------------------------
        void itemFormula_Click(object sender, EventArgs e)
        {
            C2iExpression formule = m_source != null ? m_source.Condition : null;
            if (CFormStdEditeFormule.EditeFormule(ref formule,
                new CFournisseurPropDynForDataTable(m_sourceTable),
                typeof(DataTable), true))
                m_source.Condition = formule;
            UpdateVisuelOnlyOnCreate();
            if (SourceChanged != null)
                SourceChanged(this, null);
        }

        void itemApplyOnUpdate_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null && item.Tag is EOptionImport && m_source != null)
                m_source.OptionImport= (EOptionImport)item.Tag;
            UpdateVisuelOnlyOnCreate();
            if (SourceChanged != null)
                SourceChanged(this, null);
        }

        private void UpdateVisuelOnlyOnCreate( )
        {
            if (m_source != null)
            {
                m_panelOnlyOnCreate.Visible = true;

                switch (m_source.OptionImport)
                {
                    case EOptionImport.OnUpdateAndCreate:
                        m_imageOnlyOnCreate.Image = Properties.Resources.Apply_on_update_yes;
                        break;
                    case EOptionImport.OnCreate:
                        m_imageOnlyOnCreate.Image = Properties.Resources.apply_on_update_no;
                        break;
                    case EOptionImport.Never:
                        m_imageOnlyOnCreate.Image = Properties.Resources.cancelx16;
                        break;
                }
                m_imageFormula.Visible = m_source.Condition != null;
            }
            else
            {
                m_panelOnlyOnCreate.Visible = false;
                m_imageFormula.Visible = false;
            }

        }
    }
}
