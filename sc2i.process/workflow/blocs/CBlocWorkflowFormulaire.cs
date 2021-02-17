using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.common;
using System.Drawing;
using sc2i.process.workflow.dessin;
using sc2i.drawing;
using System.ComponentModel;
using sc2i.data;

namespace sc2i.process.workflow.blocs
{
    ///TODO : gérer des blocs formulaire qui restent permanents et qui ne sont pas attachés à une seul fenêtre
    ///ça permettra de gérer des actions de validation qui générallement ont besoin de plusieurs fenêtres (consultation
    ///globale)
    public class CBlocWorkflowFormulaire : CBlocWorkflow
    {
        private C2iExpression m_formuleElementEditePrincipal = null;

        private bool m_bIsFormulaireStandard = true;

        private List<CFormuleNommee> m_listeFormulesDeConditionDeFin = new List<CFormuleNommee>();

        //private int? m_nIdFormulairePrincipal = null;
        // TESTDBKEYOK
        private List<CDbKey> m_listeDbKeysFormulaires = new List<CDbKey>();
        
        //private string m_strInstructions = "";
        private C2iExpression m_formuleInstructions = null;

        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = new CListeRestrictionsUtilisateurSurType();

        private bool m_bMasquerSurChangementDeFormulaire = false;

        // TESTDBKEYOK
        private CDbKey m_dbKeyFormulaireSecondaire = null;
        private C2iExpression m_formuleElementEditeSecondaire = null;
        private bool m_bSecondaireEnEdition = false;

        private bool m_bProposerSortieSiToutesConditionsRemplies = false;

        private bool m_bLockElementEditeEnFinDEtape = true;
        private bool m_bMasquerApresValidation = false;

        private bool m_bNePasExecuterSiToutesConditionsRemplies = false;

        private EModeGestionErreurEtapeWorkflow m_modeGestionErreurs = EModeGestionErreurEtapeWorkflow.DoNothing;

        private CParametreDeclencheurEvenement m_parametreDeclencheurStop = null;

        private string m_strRestrictionExceptionContext = "";


        //-----------------------------------------------------
        public CBlocWorkflowFormulaire()
            : base()
        {
        }

        //-----------------------------------------------------
        public CBlocWorkflowFormulaire(CTypeEtapeWorkflow typeEtape)
            : base(typeEtape)
        {
        }


        //---------------------------------------------------
        public override Size DefaultSize
        {
            get
            {
                return new Size(120, 50);
            }
        }

        //-----------------------------------------------------
        public bool MasquerSurChangementDeFormulaire
        {
            get
            {
                return m_bMasquerSurChangementDeFormulaire;
            }
            set
            {
                m_bMasquerSurChangementDeFormulaire = value;
            }
        }

        //---------------------------------------------------
        public bool PromptForEndWhenAllConditionsAreOk
        {
            get
            {
                return m_bProposerSortieSiToutesConditionsRemplies;
            }
            set
            {
                m_bProposerSortieSiToutesConditionsRemplies = value;
            }
        }

        //---------------------------------------------------
        public bool LockerElementEditeEnFinDEtape
        {
            get
            {
                return m_bLockElementEditeEnFinDEtape;
            }
            set
            {
                m_bLockElementEditeEnFinDEtape = value;
            }
        }

        //---------------------------------------------------
        public bool NePasExecuterSiToutesConditionsRemplies
        {
            get
            {
                return m_bNePasExecuterSiToutesConditionsRemplies;
            }
            set
            {
                m_bNePasExecuterSiToutesConditionsRemplies = value;
            }
        }

        //---------------------------------------------------
        public bool HideAfterValidation
        {
            get
            {
                return m_bMasquerApresValidation;
            }
            set
            {
                m_bMasquerApresValidation = value;
            }
        }

        //-----------------------------------------------------
        public override EModeGestionErreurEtapeWorkflow ModeGestionErreur
        {
            get
            {
                return m_modeGestionErreurs;
            }
        }

        //---------------------------------------------------
        public CParametreDeclencheurEvenement ParametreDeclencheurStop
        {
            get
            {
                return m_parametreDeclencheurStop;
            }
            set
            {
                m_parametreDeclencheurStop = value;
            }
        }

        //-----------------------------------------------------
        public void SetModeGestionErreur(EModeGestionErreurEtapeWorkflow mode)
        {
            m_modeGestionErreurs = mode;
        }
            

        //-----------------------------------------------------
        public C2iExpression FormuleInstructions
        {
            get
            {
                return m_formuleInstructions;
            }
            set
            {
                m_formuleInstructions = value;
            }
        }

        //-----------------------------------------------------
        public C2iExpression FormuleElementEditePrincipal
        {
            get
            {
                return m_formuleElementEditePrincipal;
            }
            set
            {
                m_formuleElementEditePrincipal = value;
            }
        }

        //---------------------------------------------------
        public bool IsStandardForm
        {
            get
            {
                return m_bIsFormulaireStandard;
            }
            set
            {
                m_bIsFormulaireStandard = value;
            }
        }

        //---------------------------------------------------
        [ExternalReferencedEntityDbKey(typeof(CFormulaire))]
        public CDbKey DbKeyFormulaireSecondaire
        {
            get
            {
                return m_dbKeyFormulaireSecondaire;
            }
            set
            {
                m_dbKeyFormulaireSecondaire = value;
            }
        }

        //---------------------------------------------------
        [ExternalReferencedEntityDbKey(typeof(CFormulaire))]
        public CDbKey[] ListeDbKeysFormulaires
        {
            get
            {
                return m_listeDbKeysFormulaires.ToArray();
            }
            set
            {
                m_listeDbKeysFormulaires = new List<CDbKey>(value);
            }
        }

        //---------------------------------------------------
        public C2iExpression FormuleElementEditeSecondaire
        {
            get
            {
                return m_formuleElementEditeSecondaire;
            }
            set
            {
                m_formuleElementEditeSecondaire = value;
            }
        }

        //---------------------------------------------------
        public bool SecondaireEnEdition
        {
            get{
                return m_bSecondaireEnEdition;
            }
            set
            {
                m_bSecondaireEnEdition = value;
            }
        }

        //---------------------------------------------------
        public IEnumerable<CFormuleNommee> FormulesConditionFin
        {
            get
            {
                return m_listeFormulesDeConditionDeFin.AsReadOnly();
            }
            set
            {
                m_listeFormulesDeConditionDeFin.Clear();
                m_listeFormulesDeConditionDeFin.AddRange(value);
            }
        }

        //---------------------------------------------------
        public CListeRestrictionsUtilisateurSurType Restrictions
        {
            get
            {
                return m_listeRestrictions;
            }
            set
            {
                m_listeRestrictions = value;
            }
        }

        //---------------------------------------------------
        public void AddFormuleConditionFin(CFormuleNommee formule)
        {
            m_listeFormulesDeConditionDeFin.Add(formule);
        }

        //---------------------------------------------------
        public void RemoveFormuleConditionFin(CFormuleNommee formule)
        {
            m_listeFormulesDeConditionDeFin.Remove(formule);
        }

        //---------------------------------------------------
        public void ClearFormulesConditionFin()
        {
            m_listeFormulesDeConditionDeFin.Clear();
        }

        //-----------------------------------------------------
        public string RestrictionExceptionContext
        {
            get
            {
                return m_strRestrictionExceptionContext;
            }
            set
            {
                m_strRestrictionExceptionContext = value;
            }
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            //Ajoute de proposition de fermeture auto
            //8 : Ajout de LockElementEditeEnFinDetape
            //9 : Masquer après validation
            //10 : ajout de mode gestion erreur
            //11 : ajout de "ne pas executer si toutes conditions remplies
            //12 : ajout de parametre de déclencheur stop
            //13 : ajout de resdtrictionExceptionContext
            //14 : Passage des Ids de Formulaires en DbKey Formulaires
            return 14;
        }

        //---------------------------------------------------
        public override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            
            serializer.TraiteBool(ref m_bIsFormulaireStandard);

            result = serializer.TraiteObject<C2iExpression>(ref m_formuleElementEditePrincipal);
            if (!result)
                return result;

            if (nVersion >= 1)
            {
                result = serializer.TraiteListe<CFormuleNommee>(m_listeFormulesDeConditionDeFin);
                // m_nIdFormulairePrincipal est obsolète, l'Id du Formulaire proncipal ne doit plus être utilisé
                //int nIdFormulaire = m_nIdFormulairePrincipal != null ? m_nIdFormulairePrincipal.Value : -1;
                int nIdFormulaire = -1;
                serializer.TraiteInt(ref nIdFormulaire);
                //if (serializer.Mode == ModeSerialisation.Lecture)
                //    m_nIdFormulairePrincipal = nIdFormulaire >= 0 ? (int?)nIdFormulaire : null;

            }

            if (nVersion >= 2)
            {
                if (nVersion < 6)
                {
                    string strInstructionsTemp = "";
                    serializer.TraiteString(ref strInstructionsTemp);
                    if (strInstructionsTemp != "")
                        m_formuleInstructions = new C2iExpressionConstante(strInstructionsTemp);
                }
                result = serializer.TraiteObject<CListeRestrictionsUtilisateurSurType>(ref m_listeRestrictions);
                if (!result)
                    return result;
            }

            if (nVersion >= 3)
            {
                serializer.TraiteBool(ref m_bMasquerSurChangementDeFormulaire);
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleElementEditeSecondaire);
                if (nVersion <= 13)
                    // TESTDBKEYOK
                    serializer.ReadDbKeyFromOldId(ref m_dbKeyFormulaireSecondaire, typeof(CFormulaire));
                else
                    serializer.TraiteDbKey(ref m_dbKeyFormulaireSecondaire);
            }
            else
                m_bMasquerSurChangementDeFormulaire = true ;

            if (nVersion >= 4)
                serializer.TraiteBool(ref m_bSecondaireEnEdition);

            if (nVersion >= 5)
            {
                int nNombreFormulaires = m_listeDbKeysFormulaires.Count;
                serializer.TraiteInt(ref nNombreFormulaires);
                switch (serializer.Mode)
                {
                    case ModeSerialisation.Lecture:
                        m_listeDbKeysFormulaires.Clear();
                        for (int i = 0; i < nNombreFormulaires; i++)
                        {
                            CDbKey nKeyTemp = null;
                            if (nVersion < 14)
                                // TESTDBKEYOK
                                serializer.ReadDbKeyFromOldId(ref nKeyTemp, typeof(CFormulaire));
                            else
                                serializer.TraiteDbKey(ref nKeyTemp);
                            m_listeDbKeysFormulaires.Add(nKeyTemp);
                        }
                        break;
                    case ModeSerialisation.Ecriture:
                        foreach (CDbKey nKey in m_listeDbKeysFormulaires)
                        {
                            // TESTDBKEYOK
                            CDbKey nKeyTemp = nKey;
                            serializer.TraiteDbKey(ref nKeyTemp);
                        }
                        break;
                 
                }

            }

            if (nVersion >= 6)
            {
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleInstructions);
                if (!result)
                    return result;
            }
            if (nVersion >= 7)
            {
                serializer.TraiteBool(ref m_bProposerSortieSiToutesConditionsRemplies);
            }
            if (nVersion >= 8)
                serializer.TraiteBool(ref m_bLockElementEditeEnFinDEtape);

            if (nVersion >= 9)
                serializer.TraiteBool(ref m_bMasquerApresValidation);

            if (nVersion >= 10)
            {
                int nVal = (int)m_modeGestionErreurs;
                serializer.TraiteInt(ref nVal);
                m_modeGestionErreurs = (EModeGestionErreurEtapeWorkflow)nVal;
            }
            else
                m_modeGestionErreurs = EModeGestionErreurEtapeWorkflow.DoNothing;

            if (nVersion >= 11)
                serializer.TraiteBool(ref m_bNePasExecuterSiToutesConditionsRemplies);
            else
                m_bNePasExecuterSiToutesConditionsRemplies = false;
            if (nVersion >= 12)
                serializer.TraiteObject<CParametreDeclencheurEvenement>(ref m_parametreDeclencheurStop);
            else
                m_parametreDeclencheurStop = null;
            if (nVersion >= 13)
                serializer.TraiteString(ref m_strRestrictionExceptionContext);

            return result;
        }


        //---------------------------------------------------
        public override Point[] GetPolygoneDessin(CWorkflowEtapeDessin donneesDessin)
        {
            Rectangle rct = donneesDessin.RectangleAbsolu;
            return new Point[]{
                new Point(rct.Left, rct.Top),
                new Point(rct.Right, rct.Top ),
                new Point ( rct.Right, rct.Bottom ),
                new Point ( rct.Left, rct.Bottom )};
        }

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique contexte, CWorkflowEtapeDessin donneesDessin)
        {
            Rectangle rct = donneesDessin.RectangleAbsolu;
            Brush br = new SolidBrush(Color.FromArgb(64, 0, 0, 0));
            rct.Size = new Size(rct.Width - 4, rct.Height - 4);
            contexte.Graphic.FillRectangle(br,
                rct.Left + 4,
                rct.Top + 4,
                rct.Width,
                rct.Height);
            
            br.Dispose();
            br = new SolidBrush(donneesDessin.BackColor);
            Pen pen = new Pen(donneesDessin.ForeColor);
            Font ft = donneesDessin.Font;
            if (ft == null)
                ft = new Font("Arial", 8);

            contexte.Graphic.FillRectangle(br, rct);
            contexte.Graphic.DrawRectangle(pen, rct);
            if (rct.Size.Width > BlocImage.Size.Width && 
                rct.Size.Height > BlocImage.Size.Height )
            {
                contexte.Graphic.DrawImage(BlocImage, new Point(rct.Left, rct.Top));
                Brush brTrans = new SolidBrush(Color.FromArgb(128, donneesDessin.BackColor));
                contexte.Graphic.FillRectangle(brTrans, rct.Left, rct.Top,
                    BlocImage.Width,
                    BlocImage.Height);
                brTrans.Dispose();
            }
            Image img = TypeEtape == null || TypeEtape.ParametresInitialisation.Affectations.Formules.Count() == 0 ?
                Resource1._1346459174_group_alerte:
                Resource1._1346459174_group;

            if (rct.Size.Width > img.Width &&
                rct.Size.Height > img.Height)
            {
                contexte.Graphic.DrawImage(img, new Rectangle(rct.Right - img.Width, rct.Top, img.Width, img.Height));
            }

            if ( m_parametreDeclencheurStop != null )
            {
            img = Resource1.stop_sign;
            if ( rct.Width > 32 && rct.Height > 32 )
                contexte.Graphic.DrawImage ( img, new Rectangle ( rct.Right - img.Width, rct.Bottom - img.Width, img.Width, img.Height ));
            }
                



            if (TypeEtape != null && ft != null)
            {
                br.Dispose();
                br = new SolidBrush(donneesDessin.ForeColor);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                contexte.Graphic.DrawString(TypeEtape.Libelle, ft, br, rct, sf);
                sf.Dispose();
            }
            if (donneesDessin.Font == null)
                ft.Dispose();
            pen.Dispose();
            br.Dispose();
        }

        //---------------------------------------------------
        public override string BlocName
        {
            get { return I.T("Form|20065"); }
        }

        //---------------------------------------------------
        public override string BlocTypeCode
        {
            get { return "FORM"; }
        }

        //---------------------------------------------------
        public override Image BlocImage
        {
            get { return Resource1._1345538464_application_form_edit; }
        }

        //---------------------------------------------------
        public override bool IsBlocAInterfaceUtilisateur
        {
            get
            {
                return true;
            }
        }

        //---------------------------------------------------
        public override CResultAErreur RunAndSaveIfOk(CEtapeWorkflow etape)
        {
            CResultAErreur result = CResultAErreur.True;
            if (NePasExecuterSiToutesConditionsRemplies)
            {
                if (GetErreursManualEndEtape(etape))
                    return EndAndSaveIfOk(etape);
            }

            //S'assure que l'évenement stoppeur est bien créé
            if (ParametreDeclencheurStop != null)
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(etape);
                result = FormuleElementEditePrincipal.Eval(ctx);
                if (!result)
                    return result;
                CObjetDonneeAIdNumerique objCible = result.Data as CObjetDonneeAIdNumerique;
                if (objCible != null)
                {
                    CResultAErreurType<CHandlerEvenement> resH = CHandlerEvenement.CreateHandlerOnObject(
                        etape.ContexteDonnee,
                        objCible,
                        ParametreDeclencheurStop,
                        "Workflow step " + TypeEtape.Id + "/" + etape.UniversalId,
                        etape.UniversalId);
                    if (!resH)
                    {
                        result.EmpileErreur(resH.Erreur);
                        return result;
                    }
                    resH.DataType.EtapeWorkflowATerminer = etape;
                }
            }

            //Asynchrone, le démarrage ne fait rien, l'étape attendra la validation du formulaire
            return etape.ContexteDonnee.SaveAll(true);
        }

        //---------------------------------------------------
        public override CResultAErreur GetErreursManualEndEtape(CEtapeWorkflow etape)
        {
            CResultAErreur result = base.GetErreursManualEndEtape(etape);
            if (result)
            {
                result = CResultAErreur.True;
                //Vérifie toutes les conditions de fin
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(etape);
                foreach (CFormuleNommee fn in FormulesConditionFin)
                {
                    if (fn != null && fn.Formule != null)
                    {
                        CResultAErreur resTmp = fn.Formule.Eval(ctx);
                        if (!resTmp)
                        {
                            result.EmpileErreur(I.T("Error in formula '@1' for step @2|20082",
                                fn.Libelle, etape.LastError));
                            return result;
                        }
                        bool bOk = resTmp.Data != null;
                        if (bOk)
                        {
                            bool? bConv = CUtilBool.BoolFromString(resTmp.Data.ToString());
                            bOk = bConv == true;
                        }
                        if (!bOk)
                            result.EmpileErreur(fn.Libelle);
                    }
                }
            }
            return result;
        }

        //---------------------------------------------------
        public override CResultAErreur EndEtapeNoSave(CEtapeWorkflow etape)
        {
            CResultAErreur result = GetErreursManualEndEtape(etape);
            if (result)
                result = base.EndEtapeNoSave(etape);
            return result;
        }

    }
}
