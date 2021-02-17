using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.process.workflow.blocs;
using sc2i.data.dynamic;
using sc2i.win32.data.navigation;
using sc2i.data;
using sc2i.expression;
using sc2i.common;
using sc2i.process.workflow;
using sc2i.win32.common;
using sc2i.win32.expression;
using sc2i.win32.data.dynamic;
using sc2i.win32.data;
using System.IO;
using sc2i.process;

namespace sc2i.win32.process.workflow.bloc
{
    public partial class CFormEditionBlocWorkflowFormulaire : Form
    {
        private CBlocWorkflowFormulaire m_blocFormulaire = null;
        //private CFormulaire m_formulairePrincipalSelectionne = null;
        private CFormulaire m_formulaireSecondaireSelectionne = null;
        private HashSet<CDbKey> m_listeDbKeysFormulaires = new HashSet<CDbKey>();
        
        
        //-------------------------------------------------------
        public CFormEditionBlocWorkflowFormulaire()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //-------------------------------------------------------
        public static bool EditeBloc(CBlocWorkflowFormulaire bloc)
        {
            if (bloc == null)
                return false;
            CFormEditionBlocWorkflowFormulaire form = new CFormEditionBlocWorkflowFormulaire();
            form.m_blocFormulaire = bloc;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        //-------------------------------------------------------
        private bool m_bIsInitializing = false;
        private void InitChamps()
        {
            m_bIsInitializing = true;
            m_txtFormuleElementEdite.Init(new CFournisseurPropDynStd(), typeof(CEtapeWorkflow));
            m_txtFormuleElementEdite.Formule = m_blocFormulaire.FormuleElementEditePrincipal;
            m_txtFormuleElementSecondaire.Init(new CFournisseurPropDynStd(), typeof(CEtapeWorkflow));
            m_txtFormuleElementSecondaire.Formule = m_blocFormulaire.FormuleElementEditeSecondaire;
            m_txtFormuleInstructions.Init(new CFournisseurPropDynStd(), typeof(CEtapeWorkflow));
            m_txtFormuleInstructions.Formule = m_blocFormulaire.FormuleInstructions;
            m_rbtnStandard.Checked = m_blocFormulaire.IsStandardForm;
            m_rbtnFormulaireSpecifique.Checked = !m_blocFormulaire.IsStandardForm;
            InitSelectFormulaire(m_txtFormuleElementEdite);
            InitSelectFormulaire(m_txtFormuleElementSecondaire);

            m_listeDbKeysFormulaires.Clear();
            foreach (CDbKey nKeyForm in m_blocFormulaire.ListeDbKeysFormulaires)
            {
                m_listeDbKeysFormulaires.Add(nKeyForm);
            }

            if (m_blocFormulaire.DbKeyFormulaireSecondaire != null)
            {
                CFormulaire form = new CFormulaire(CContexteDonneeSysteme.GetInstance());
                if (form.ReadIfExists(m_blocFormulaire.DbKeyFormulaireSecondaire))
                    m_txtSelectFormulaireSecondaire.ElementSelectionne = form;
                m_formulaireSecondaireSelectionne = form;
            }
            m_wndListeFormules.Init(m_blocFormulaire.FormulesConditionFin.ToArray(),
                typeof(CEtapeWorkflow),
                new CFournisseurPropDynStd());
            m_lnkSelectFormulaires.Visible = m_rbtnFormulaireSpecifique.Checked;
            m_panelRestrictions.Init(m_blocFormulaire.Restrictions);
            
            CParametresInitialisationEtape parametres = m_blocFormulaire.TypeEtape != null ? m_blocFormulaire.TypeEtape.ParametresInitialisation : new CParametresInitialisationEtape();
            m_panelAffectations.Init(parametres);
            m_chkHideOnChangeForm.Checked = m_blocFormulaire.MasquerSurChangementDeFormulaire;
            m_chkLockItemWhenComplete.Checked = m_blocFormulaire.LockerElementEditeEnFinDEtape;
            m_chkMasquerApresValidation.Checked = m_blocFormulaire.HideAfterValidation;
            m_chkSecondaireEnEdition.Checked = m_blocFormulaire.SecondaireEnEdition;

            m_chkPromptToEnd.Checked = m_blocFormulaire.PromptForEndWhenAllConditionsAreOk;
            m_chkPasserSiPasErreur.Checked = m_blocFormulaire.NePasExecuterSiToutesConditionsRemplies;

            m_chkUseStopHandler.Checked = m_blocFormulaire.ParametreDeclencheurStop != null;
            if ( m_blocFormulaire.ParametreDeclencheurStop != null )
            {
                m_panelParametreDeclenchement.Visible = true;
                m_panelParametreDeclenchement.Init ( m_blocFormulaire.ParametreDeclencheurStop );
            }
            else
            {
                m_panelParametreDeclenchement.Visible = false;
            }

            UpdateVisuLinkFormulaires();

            InitPanelGestionErreur();
            m_panelChamps.ElementEdite = m_blocFormulaire.TypeEtape;

            m_txtExceptionRestriction.Text = m_blocFormulaire.RestrictionExceptionContext;

            m_bIsInitializing = false;

        }

        //-------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            
            C2iExpression formule = GetFormuleValideElementEdite(m_txtFormuleElementEdite);
            if (formule == null)
                result.EmpileErreur(I.T("Edited elements formula is not valid|20052"));
            else
                m_blocFormulaire.FormuleElementEditePrincipal = formule;

            formule = GetFormuleValideElementEdite(m_txtFormuleElementSecondaire);
            m_blocFormulaire.FormuleElementEditeSecondaire = formule;

            m_blocFormulaire.FormuleInstructions = m_txtFormuleInstructions.Formule;
            
            if (m_rbtnFormulaireSpecifique.Checked)
            {
                if (m_listeDbKeysFormulaires.Count == 0)
                    result.EmpileErreur(I.T("Select the form to use|20053"));
                else
                {
                    m_blocFormulaire.ListeDbKeysFormulaires = m_listeDbKeysFormulaires.ToArray();
                    m_blocFormulaire.IsStandardForm = false;
                }
            }
            else
            {
                m_blocFormulaire.IsStandardForm = true;
            }

            CFormulaire formulaire = m_txtSelectFormulaireSecondaire.ElementSelectionne as CFormulaire;
            if (m_blocFormulaire.FormuleElementEditeSecondaire != null &&
                formulaire == null)
                result.EmpileErreur(I.T("Select the secondary form to use|20086"));
            m_blocFormulaire.DbKeyFormulaireSecondaire = formulaire != null ? formulaire.DbKey : null;
            m_blocFormulaire.MasquerSurChangementDeFormulaire = m_chkHideOnChangeForm.Checked;
            m_blocFormulaire.LockerElementEditeEnFinDEtape = m_chkLockItemWhenComplete.Checked;
            m_blocFormulaire.HideAfterValidation = m_chkMasquerApresValidation.Checked;
            if (result)
            {
                m_blocFormulaire.FormulesConditionFin = m_wndListeFormules.GetFormules();
            }
            if (result)
            {
                m_blocFormulaire.Restrictions = m_panelRestrictions.GetListeRestrictions();
            }
            CResultAErreurType<CParametresInitialisationEtape> resParam = m_panelAffectations.MajChamps();
            if (resParam )
            {
                if (m_blocFormulaire.TypeEtape != null)
                    m_blocFormulaire.TypeEtape.ParametresInitialisation = resParam.DataType;
            }
            else
            {
                result.EmpileErreur ( resParam.Erreur );
                return result;
            }
            m_blocFormulaire.SecondaireEnEdition = m_chkSecondaireEnEdition.Checked;

            m_blocFormulaire.PromptForEndWhenAllConditionsAreOk = m_chkPromptToEnd.Checked;
            m_blocFormulaire.NePasExecuterSiToutesConditionsRemplies = m_chkPasserSiPasErreur.Checked;

            EModeGestionErreurEtapeWorkflow modeGestionErreur = EModeGestionErreurEtapeWorkflow.DoNothing;
            foreach (Control ctrl in m_panelGestionErreur.Controls)
            {
                CheckBox chk = ctrl as CheckBox;
                if (chk != null && chk.Checked)
                {
                    EModeGestionErreurEtapeWorkflow? modeChk = chk.Tag as EModeGestionErreurEtapeWorkflow?;
                    if (modeChk != null)
                        modeGestionErreur |= modeChk.Value;
                }
            }
            m_blocFormulaire.SetModeGestionErreur (modeGestionErreur);

            if (m_chkUseStopHandler.Checked)
            {
                result = m_panelParametreDeclenchement.MAJ_Champs();
                CParametreDeclencheurEvenement parametre = null;
                if (result)
                {
                    parametre = m_panelParametreDeclenchement.ParametreDeclencheur;
                    result = parametre.VerifieDonnees();
                }
                if (!result)
                    return result;
                m_blocFormulaire.ParametreDeclencheurStop = parametre;
            }
            else
                m_blocFormulaire.ParametreDeclencheurStop = null;

            m_blocFormulaire.RestrictionExceptionContext = m_txtExceptionRestriction.Text.Trim();

            result = m_panelChamps.MAJ_Champs();

            return result;
        }


        //-------------------------------------------------------
        private void m_rbtnFormulaireSpecifique_CheckedChanged(object sender, EventArgs e)
        {
            m_lnkSelectFormulaires.Visible = m_rbtnFormulaireSpecifique.Checked;
        }

        //-------------------------------------------------------
        private C2iExpression GetFormuleValideElementEdite(CTextBoxZoomFormule editeur)
        {
            C2iExpression formule = editeur.Formule;
            bool bHasErreur = true;
            if (formule != null)
            {
                CTypeResultatExpression typeResultat = formule.TypeDonnee;
                if (typeResultat != null &&
                    !typeResultat.IsArrayOfTypeNatif &&
                    typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(typeResultat.TypeDotNetNatif))
                    bHasErreur = false;
            }
            if (!bHasErreur)
                return formule;
            return null;
        }

        //-------------------------------------------------------
        private void InitSelectFormulaire(CTextBoxZoomFormule editeur)
        {
            if ( editeur == null )
                return;
            C2iExpression formule = GetFormuleValideElementEdite(editeur);
            CFormulaire formulaireSel = null;
            C2iTextBoxFiltreRapide selecteur = null;
            if (editeur == m_txtFormuleElementSecondaire)
            {
                formulaireSel = m_txtSelectFormulaireSecondaire.ElementSelectionne as CFormulaire;
                selecteur = m_txtSelectFormulaireSecondaire;
            }
            if (formule != null && selecteur != null)
            {
                Type tpEdite = formule.TypeDonnee.TypeDotNetNatif;
                CFiltreData filtre = new CFiltreData(CFormulaire.c_champTypeElementEdite + "=@1",
                        tpEdite.ToString());
                CRoleChampCustom role = CRoleChampCustom.GetRoleForType(tpEdite);
                if ( role != null )
                    filtre = CFiltreData.GetOrFiltre ( filtre,
                        CFormulaire.GetFiltreFormulairesForRole ( role.CodeRole ));

                selecteur.InitAvecFiltreDeBase(typeof(CFormulaire),
                    "Libelle",
                    filtre, true);
                if (formulaireSel != null && formulaireSel.TypeElementEdite == tpEdite)
                    selecteur.ElementSelectionne = formulaireSel;
                else
                    selecteur.ElementSelectionne = null;
            }
        }

        //-------------------------------------------------------
        private void m_txtFormuleElementEdite_Validated(object sender, EventArgs e)
        {
            InitSelectFormulaire(sender as CTextBoxZoomFormule);
            UpdatePanelDeclencheur();
        }

        //-------------------------------------------------------
        private void UpdatePanelDeclencheur()
        {
            C2iExpression formule = m_txtFormuleElementEdite.Formule;
            if (formule != null && formule.TypeDonnee != null)
            {
                Type tp = formule.TypeDonnee.TypeDotNetNatif;
                if (tp != null)
                {
                    if (m_panelParametreDeclenchement.ParametreDeclencheur != null && m_panelParametreDeclenchement.ParametreDeclencheur.TypeCible != tp)
                    {
                        CParametreDeclencheurEvenement p = new CParametreDeclencheurEvenement();
                        p.TypeCible = tp;
                        m_panelParametreDeclenchement.Init(p);
                        m_panelParametreDeclenchement.Visible = m_chkUseStopHandler.Checked;
                    }
                }
                else 
                    m_panelParametreDeclenchement.Visible = false;
            }
                
        }

        //-------------------------------------------------------
        private void CFormEditionBlocWorkflowFormulaire_Load(object sender, EventArgs e)
        {
            if ( !DesignMode)
                InitChamps();
        }

        //-------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //-------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = MajChamps();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        //-------------------------------------------------------------------
        private void m_txtSelectFormulaireSecondaire_ElementSelectionneChanged(object sender, EventArgs e)
        {
            m_formulaireSecondaireSelectionne = m_txtSelectFormulaireSecondaire.ElementSelectionne as CFormulaire;
        }

        //---------------------------------------------------------------------
        private void m_txtSelectFormulaireSecondaire_Enter(object sender, EventArgs e)
        {
            InitSelectFormulaire(sender as CTextBoxZoomFormule);
        }

        //------------------------------------------------------------------------
        private void m_lnkSelectFormulaires_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            C2iExpression formule = GetFormuleValideElementEdite(m_txtFormuleElementEdite);
            
            if (formule != null)
            {
                Type tpEdite = formule.TypeDonnee.TypeDotNetNatif;
                CFiltreData filtre = new CFiltreData(CFormulaire.c_champTypeElementEdite + "=@1",
                        tpEdite.ToString());
                CRoleChampCustom role = CRoleChampCustom.GetRoleForType(tpEdite);
                if (role != null)
                    filtre = CFiltreData.GetOrFiltre(filtre,
                        CFormulaire.GetFiltreFormulairesForRole(role.CodeRole));

                CListeObjetDonneeGenerique<CFormulaire> listeFormulaires = new CListeObjetDonneeGenerique<CFormulaire>(
                    CSc2iWin32DataClient.ContexteCourant, filtre);
                

                m_menuFormulaires.Items.Clear();
                
                foreach (CFormulaire formulaire in listeFormulaires)
                {
                    CDbKey nKeyForm = formulaire.DbKey;
                    ToolStripMenuItem itemForm = new ToolStripMenuItem(formulaire.Libelle);
                    itemForm.Tag = nKeyForm;
                    itemForm.Checked = m_listeDbKeysFormulaires.Contains(nKeyForm);
                    itemForm.Click += new EventHandler(itemForm_Click);
                    itemForm.Enabled = true;
                    m_menuFormulaires.Items.Add(itemForm);                            
                }
                m_menuFormulaires.Show(m_lnkSelectFormulaires, new Point(0, m_lnkSelectFormulaires.Height));
            }
        }

        //-------------------------------------------------------
        void itemForm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                CDbKey nKey = item.Tag as CDbKey;
                if (!m_listeDbKeysFormulaires.Contains(nKey))
                    m_listeDbKeysFormulaires.Add(nKey);
                else
                    m_listeDbKeysFormulaires.Remove(nKey);
                UpdateVisuLinkFormulaires();
            }
        }

        //-------------------------------------------------------
        private void UpdateVisuLinkFormulaires()
        {
            m_lnkSelectFormulaires.Text = I.T("Select specific Forms|10009");
            if (m_listeDbKeysFormulaires.Count > 0)
                m_lnkSelectFormulaires.Text += " (" + m_listeDbKeysFormulaires.Count.ToString() + ")";
        }

        private void m_btnCopy_Click(object sender, EventArgs e)
        {
            if (m_blocFormulaire != null)
            {
                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream);
                CSerializerSaveBinaire ser = new CSerializerSaveBinaire(writer);
                CResultAErreur result = ser.TraiteObject<CBlocWorkflowFormulaire>(ref m_blocFormulaire);
                if (result)
                    Clipboard.SetData(typeof(CBlocWorkflowFormulaire).ToString(), stream.GetBuffer());
                stream.Close();
                writer.Close();
                stream.Dispose();
            }
        }

        private void m_btnPaste_Click(object sender, EventArgs e)
        {
            byte[] data = Clipboard.GetData(typeof(CBlocWorkflowFormulaire).ToString()) as byte[];
            if (data != null)
            {
                try
                {
                    MemoryStream stream = new MemoryStream(data);
                    BinaryReader reader = new BinaryReader(stream);
                    CSerializerReadBinaire ser = new CSerializerReadBinaire(reader);
                    ser.IsForClone = true;
                    CBlocWorkflowFormulaire bloc = new CBlocWorkflowFormulaire();
                    if (ser.TraiteObject<CBlocWorkflowFormulaire>(ref bloc))
                    {
                        CBlocWorkflowFormulaire old = m_blocFormulaire;
                        m_blocFormulaire = bloc;
                        InitChamps();
                        m_blocFormulaire = old;
                    }
                    reader.Close();
                    stream.Close();
                    stream.Dispose();
                }
                catch { }
            }
        }

        //-------------------------------------------------------
        private void InitPanelGestionErreur()
        {
            m_panelGestionErreur.SuspendDrawing();
            if (m_panelGestionErreur.Controls.Count == 0)
            {
                foreach (EModeGestionErreurEtapeWorkflow mode in Enum.GetValues(typeof(EModeGestionErreurEtapeWorkflow)))
                {
                    CheckBox chk = new CheckBox();
                    chk.Text = new CModeGestionErreurEtapeWorkflow(mode).Libelle;
                    chk.Tag = mode;
                    m_panelGestionErreur.Controls.Add(chk);
                    chk.Dock = DockStyle.Left;
                    chk.BringToFront();
                }
            }
            foreach (Control ctrl in m_panelGestionErreur.Controls)
            {
                CheckBox chk = ctrl as CheckBox;
                if (chk != null)
                {
                    EModeGestionErreurEtapeWorkflow? mode = chk.Tag as EModeGestionErreurEtapeWorkflow?;
                    if (mode != null)
                        chk.Checked = (m_blocFormulaire.ModeGestionErreur & mode.Value) == mode.Value;
                }
            }
        }

        private void m_chkUseStopHandler_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_bIsInitializing && !DesignMode)
            {
                m_panelParametreDeclenchement.Visible = m_chkUseStopHandler.Checked;
                if ( m_chkUseStopHandler.Checked )
                    UpdatePanelDeclencheur();

            }
        }
    }

    //-------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurBlocWorkflowFormulaire : IEditeurBlocWorkflow
    {
        public static void Autoexec()
        {
            CAllocateurEditeurBlocWorkflow.RegisterEditeur(typeof(CBlocWorkflowFormulaire),
                typeof(CEditeurBlocWorkflowFormulaire));
        }

        //-------------------------------------------------------
        public bool EditeBloc(CBlocWorkflow bloc)
        {
            return CFormEditionBlocWorkflowFormulaire.EditeBloc ( bloc as CBlocWorkflowFormulaire);
        }

    }

}
