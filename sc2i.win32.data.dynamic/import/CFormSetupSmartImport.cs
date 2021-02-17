using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.expression;
using sc2i.multitiers.client;
using sc2i.win32.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sc2i.win32.data.dynamic.import
{
    public partial class CFormSetupSmartImport : Form
    {
        private CValeursProprietes m_rootValues = null;
        private string m_strNomFichierExemple = "";
        private DataTable m_sourceTable = null;
        private IParametreLectureFichier m_parametreLectureFichier = null;
        private COptionSimulationSmartImport m_optionsSimulation = new COptionSimulationSmartImport();
        private COptionExecutionSmartImport m_optionsExecution = new COptionExecutionSmartImport();

        private CSessionImport m_sessionImport = new CSessionImport();


        public CFormSetupSmartImport()
        {
            InitializeComponent();
            
        }

        public static void ShowValeurs ( CObjetDonnee objet )
        {
            using (CFormSetupSmartImport frm = new CFormSetupSmartImport() )
            {
                frm.Init ( new CValeursProprietes(objet));
                frm.ShowDialog();
            }
        }

        private void Init ( CValeursProprietes valeurRoot)
        {
            m_rootValues = valeurRoot;
            CConfigMappagesSmartImport conf = new CConfigMappagesSmartImport();
            conf.KeyEntite = valeurRoot.DbKeyObjet;
            conf.TypeEntite = valeurRoot.TypeObjet;
            m_ctrlSetup.Fill(valeurRoot, conf, m_sourceTable);
        }

        private void m_ctrlSetup_SelectionChanged(object sender, EventArgs e)
        {
            CSetupSmartImportItem item = m_ctrlSetup.CurrentSetupItem;
            if (item == null)
                m_lblChemin.Text = "";
            else
            {
                IEnumerable<CDefinitionProprieteDynamique> defs = item.Chemin;
                StringBuilder bl = new StringBuilder();
                foreach (CDefinitionProprieteDynamique def in defs)
                {
                    bl.Append(def.Nom);
                    bl.Append(".");
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 1, 1);
                m_lblChemin.Text = bl.ToString();
            }
        }

        //-----------------------------------------------------------------
        private void m_btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        //-----------------------------------------------------------------
        private bool SaveConfig()
        {
            CResultAErreurType<CConfigMappagesSmartImport> res = m_ctrlSetup.GetConfigFinale();
            if (!res || res.DataType == null)
            {
                CFormAlerte.Afficher(res.Erreur);
                return false;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = I.T("Import structure(*.futimp)|*.futimp|All files|*.*|20122");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CResultAErreur result = CSerializerObjetInFile.SaveToFile(res.DataType, "IMPORT_STRUCT", dlg.FileName);
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                    return false;
                }
            }
            return true;
        }

        //-----------------------------------------------------------
        private void m_btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = I.T("Import structure(*.futimp)|*.futimp|All files|*.*|20122");
            if ( dlg.ShowDialog() == DialogResult.OK)
            {
                CConfigMappagesSmartImport config = new CConfigMappagesSmartImport();
                CResultAErreur result = CSerializerObjetInFile.ReadFromFile(config, "IMPORT_STRUCT", dlg.FileName);
                if (!result)
                    CFormAlerte.Afficher(result.Erreur);
                else
                {
                    if ( MessageBox.Show(I.T("Current import structure will be replaced. Continue ?|20123"),
                        "",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        m_ctrlSetup.Fill(m_rootValues, config, m_sourceTable);
                    }
                }
            }
        }

        //-----------------------------------------------------------
        private void CFormSetupSmartImport_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        //-----------------------------------------------------------
        private void m_btnSetupSource_Click(object sender, EventArgs e)
        {
            IParametreLectureFichier parametre = CAssistantLectureFichier.CreateParametreLectureFichier(ref m_strNomFichierExemple);
            if (parametre != null)
            {
                m_sessionImport.TableSource = null;
                m_parametreLectureFichier = parametre;
                CreateSourceTable();
                CResultAErreurType<CConfigMappagesSmartImport> resConf = m_ctrlSetup.GetConfigFinale();
                CConfigMappagesSmartImport conf = resConf.DataType;
                m_ctrlSetup.Fill(m_rootValues, conf, m_sourceTable);

            }
        }

        //-----------------------------------------------------------
        private void CreateSourceTable()
        {
            if (m_parametreLectureFichier != null)
            {
                m_sourceTable = new DataTable();
                foreach (DataColumn col in m_parametreLectureFichier.GetColonnes())
                {
                    DataColumn copie = new DataColumn(col.ColumnName, col.DataType);
                    m_sourceTable.Columns.Add(copie);
                }
            }
            FillListeColonnes();
        }

        //-----------------------------------------------------------
        private void FillListeColonnes()
        {
            m_wndListeChamps.BeginUpdate();
            m_wndListeChamps.Items.Clear();
            if ( m_sourceTable != null )
            {
                foreach ( DataColumn col in m_sourceTable.Columns)
                {
                    ListViewItem item = new ListViewItem(col.ColumnName);
                    m_wndListeChamps.Items.Add(item);
                }
            }
            m_wndListeChamps.EndUpdate();
        }

        //--------------------------------------------------------------
        private void m_btnImporter_Click(object sender, EventArgs e)
        {
            CConfigMappagesSmartImport config = null;
            DataTable table = null;
            CResultAErreur result = PrepareImport(ref config, ref table);
            if (!result || table == null)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            if (!CFormOptionsImport.EditeOptions(m_optionsExecution))
                return;

            IIndicateurProgression indicateur = null;
            indicateur = CFormProgression.GetNewIndicateurAndPopup(I.T("Importing data|20245"));
            CResultAErreurType<CSessionImport> resSession;
            resSession = config.DoImportSurServeur(CSc2iWin32DataClient.ContexteCourant.IdSession,
                table,
                m_optionsExecution,
                indicateur);
            CFormProgression.EndIndicateur(indicateur);
            if (m_optionsExecution.NomFichierSauvegarde != null &&
                m_optionsExecution.NomFichierSauvegarde.Length > 0)
            {
                result = CSerializerObjetInFile.SaveToFile(resSession.DataType, "IMPORT_SESSION", m_optionsExecution.NomFichierSauvegarde);
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                }
            }
            SetSessionEnCours ( resSession.DataType);
            CFormEditionSessionImport.EditeSession(m_sessionImport);
        }

        //-----------------------------------------------------------------
        private void m_btnCopy_Click(object sender, EventArgs e)
        {
            CResultAErreurType<CConfigMappagesSmartImport> resConfig = m_ctrlSetup.GetConfigFinale();
            if (!resConfig)
            {
                CFormAlerte.Afficher(resConfig.Erreur);
                return;
            }
            CSerializerObjetInClipBoard.Copy(resConfig.DataType, typeof(CConfigMappagesSmartImport).ToString());
        }


        //-----------------------------------------------------------------
        private void m_btnPaste_Click(object sender, EventArgs e)
        {
            I2iSerializable ser = null;
            CResultAErreur res = CSerializerObjetInClipBoard.Paste(ref ser, typeof(CConfigMappagesSmartImport).ToString());
            if ( !res )
            {
                CFormAlerte.Afficher(res.Erreur);
                return;
            }
            CConfigMappagesSmartImport conf = ser as CConfigMappagesSmartImport;
            if ( conf != null && MessageBox.Show(I.T("Current import structure will be replaced. Continue ?|20123"),
                        "",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_ctrlSetup.Fill(m_rootValues, conf, m_sourceTable);
            }
        }

        private CResultAErreur PrepareImport ( 
            ref CConfigMappagesSmartImport config,
            ref DataTable tableSource )
        {
            CResultAErreur result = CResultAErreur.True;
            CResultAErreurType<CConfigMappagesSmartImport> resConfig = m_ctrlSetup.GetConfigFinale();
            if (!resConfig)
            {
                result.EmpileErreur ( resConfig.Erreur );
                return result;
            }
            config = resConfig.DataType;
            if (m_sessionImport.TableSource != null)
                tableSource = m_sessionImport.TableSource;
            else if (m_parametreLectureFichier != null)
            {
                result = m_parametreLectureFichier.LectureFichier(m_strNomFichierExemple);
                if (!result)
                    return result;
                tableSource = result.Data as DataTable;
            }
            return result;
        }

        private void m_btnTesterImport_Click(object sender, EventArgs e)
        {
            CConfigMappagesSmartImport config = null;
            DataTable table = null;
            CResultAErreur result = PrepareImport(ref config, ref table);
            if (!result || table == null)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }

            DialogResult dr = MessageBox.Show(I.T("Would you like to save configuration first ?|20225"), "",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
                return;
            if (dr == DialogResult.Yes)
                if (!SaveConfig())
                    return;

            if (!CFormOptionsSimulation.EditeOptions(m_optionsSimulation))
                return;

            using (CContexteDonnee ctx = new CContexteDonnee(CSc2iWin32DataClient.ContexteCourant.IdSession, true, false))
            {
                CContexteImportDonnee contexteImport = new CContexteImportDonnee(ctx);
                contexteImport.StartLine = m_optionsSimulation.StartLine;
                contexteImport.EndLine = m_optionsSimulation.EndLine;
                contexteImport.BestEffort = true;

                IIndicateurProgression indicateur = null;
                indicateur = CFormProgression.GetNewIndicateurAndPopup(I.T("Testing import|20246"));
                result = config.ImportTable(table, contexteImport, indicateur);
                CFormProgression.EndIndicateur(indicateur);
                indicateur = null;
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                    return;
                }
                if (m_optionsSimulation.TestDbWriting)
                {
                    CSessionClient session = CSessionClient.GetSessionForIdSession(ctx.IdSession);
                    session.BeginTrans();
                    result = ctx.SaveAll(false);
                    session.RollbackTrans();
                    if (!result)
                    {
                        if (CFormAlerte.Afficher(result.Erreur, EFormAlerteBoutons.ContinuerAnnuler, EFormAlerteType.Info) == System.Windows.Forms.DialogResult.Cancel)
                            return;
                    }
                }
                if (CFormResultatSmartImport.ShowImportResult(ctx, contexteImport, config, m_rootValues, table))
                {
                    result = ctx.SaveAll(true);
                    if (!result)
                    {
                        CFormAlerte.Afficher(result.Erreur);
                        return;
                    }
                }


            }
        }

        private void m_btnSaveSession_Click(object sender, EventArgs e)
        {
            CConfigMappagesSmartImport config = null;
            DataTable tableSource = null;
            CResultAErreur res = PrepareImport(ref config, ref tableSource);
            if (!res)
            {
                CFormAlerte.Afficher(res.Erreur);
                return;
            }
            m_sessionImport.ConfigMappage = config;
            m_sessionImport.TableSource = tableSource;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = I.T("Import session(*.futimpses)|*.futimpses|All files|*.*|20148");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_lblImportSession.Text = dlg.FileName;
                CResultAErreur result = CSerializerObjetInFile.SaveToFile(m_sessionImport, "IMPORT_SESSION", dlg.FileName);
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                    return;
                }
            }
            return;
        }

        private void m_btnOpenSession_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = I.T("Import session(*.futimpses)|*.futimpses|All files|*.*|20148");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CSessionImport session = new CSessionImport();
                CResultAErreur result = CSerializerObjetInFile.ReadFromFile(session, "IMPORT_SESSION", dlg.FileName);
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                    return;
                }
                else
                {
                    m_lblImportSession.Text = dlg.FileName;
                    SetSessionEnCours(session);
                }
            }
            return;
        }

        private void SetSessionEnCours ( CSessionImport session )
        {
            m_sessionImport = session;
            m_sourceTable = m_sessionImport.TableSource;
            FillListeColonnes();
            m_ctrlSetup.Fill(m_rootValues,
                m_sessionImport.ConfigMappage,
                m_sourceTable);
        }

        private void m_lnkEditerSession_LinkClicked(object sender, EventArgs e)
        {
            CConfigMappagesSmartImport config = null;
            DataTable tableSource = null;
            CResultAErreur res = PrepareImport(ref config, ref tableSource);
            if (!res)
            {
                CFormAlerte.Afficher(res.Erreur);
                return;
            }
            m_sessionImport.ConfigMappage = config;
            m_sessionImport.TableSource = tableSource;
            CFormEditionSessionImport.EditeSession(m_sessionImport);
        }
        
    }
}
