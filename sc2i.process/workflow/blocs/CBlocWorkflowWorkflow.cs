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
using System.Drawing.Drawing2D;
using sc2i.data;
using System.Collections;

namespace sc2i.process.workflow.blocs
{
    public class CBlocWorkflowWorkflow : CBlocWorkflow
    {
        // TESTDBKEYOK
        private CDbKey m_dbKeyTypeWorkflow = null;
        private CDbKey m_dbKeyTypeEtapeDemarrage = null;

        private C2iExpression m_formuleInitialisationWorkflow = null;

        //-----------------------------------------------------
        public CBlocWorkflowWorkflow()
            : base()
        {
        }

        //-----------------------------------------------------
        public CBlocWorkflowWorkflow(CTypeEtapeWorkflow typeEtape)
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
        public override EModeGestionErreurEtapeWorkflow ModeGestionErreur
        {
            get
            {
                return EModeGestionErreurEtapeWorkflow.SetError;
            }
        }

        //---------------------------------------------------
        [ExternalReferencedEntityDbKey(typeof(CTypeWorkflow))]
        public CDbKey DbKeyTypeWorkflow
        {
            get
            {
                return m_dbKeyTypeWorkflow;
            }
            set
            {
                m_dbKeyTypeWorkflow = value;
            }
        }

        //---------------------------------------------------
        [ExternalReferencedEntityDbKey(typeof(CTypeEtapeWorkflow))]
        public CDbKey DbKeyTypeEtapeDémarrage
        {
            get
            {
                return m_dbKeyTypeEtapeDemarrage;
            }
            set
            {
                m_dbKeyTypeEtapeDemarrage = value;
            }
        }

        //---------------------------------------------------
        public C2iExpression FormuleInitialisationWorkflowLance
        {
            get
            {
                return m_formuleInitialisationWorkflow;
            }
            set
            {
                m_formuleInitialisationWorkflow = value;
            }
        }


        
        //---------------------------------------------------
        private int GetNumVersion()
        {
            //return 1;
            return 2; // Passage des Id de TypeWorkflow et Type Etape à DbKey
        }

        //---------------------------------------------------
        public override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            if (nVersion < 2)
            {
                // TESTDBKEYOK
                serializer.ReadDbKeyFromOldId(ref m_dbKeyTypeWorkflow, typeof(CTypeWorkflow));
                // TESTDBKEYOK
                serializer.ReadDbKeyFromOldId(ref m_dbKeyTypeEtapeDemarrage, typeof(CTypeEtapeWorkflow));
            }
            else
            {
                serializer.TraiteDbKey(ref m_dbKeyTypeWorkflow);
                serializer.TraiteDbKey(ref m_dbKeyTypeEtapeDemarrage);
            }

            if (nVersion >= 1)
            {
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleInitialisationWorkflow);
                if (!result)
                    return result;
            }
            else if ( serializer.Mode == ModeSerialisation.Lecture )
                m_formuleInitialisationWorkflow = null;
            
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
                rct.Size.Height > BlocImage.Size.Height)
            {
                contexte.Graphic.DrawImage(BlocImage, new Point(rct.Left, rct.Top));
                Brush brTrans = new SolidBrush(Color.FromArgb(128, donneesDessin.BackColor));
                contexte.Graphic.FillRectangle(brTrans, rct.Left, rct.Top,
                    BlocImage.Width,
                    BlocImage.Height);
                brTrans.Dispose();
            }

            int nSizeMarge = Math.Min(10, Math.Max(1, rct.Width / 10));
            Rectangle rctMarge = new Rectangle(rct.Left, rct.Top,
                nSizeMarge,
                rct.Height);
            Brush brMarge = new SolidBrush(Color.FromArgb(64, 0, 0, 0));
            contexte.Graphic.FillRectangle(brMarge, rctMarge);
            rctMarge.Offset(rct.Width - nSizeMarge, 0);
            contexte.Graphic.FillRectangle(brMarge, rctMarge);
            brMarge.Dispose();

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
            get { return I.T("Workflow|20066"); }
        }

        //---------------------------------------------------
        public override string BlocTypeCode
        {
            get { return "WORKFLOW"; }
        }

        //---------------------------------------------------
        public override Image BlocImage
        {
            get { return Resource1.icones_workflow; }
        }

        //---------------------------------------------------
        public override bool IsBlocAInterfaceUtilisateur
        {
            get { return false; }
        }

        //---------------------------------------------------
        public CWorkflow GetOrCreateWorkflowInCurrentContexte ( CEtapeWorkflow etape )
        {
            CResultAErreur result = CResultAErreur.True;
            if ( DbKeyTypeWorkflow == null )
            {
                return null;
            }
            //Lance le démarrage du workflow 
            if (etape.WorkflowLancé != null)
            {
                if ( etape.WorkflowLancé.IdEtapeAppelante != etape.Id )
                    etape.WorkflowLancé.EtapeAppelante = etape;//Pour pallier aux soucis d'id négatifs stockés parfois
                return etape.WorkflowLancé;
            }
            else
            {
                CTypeWorkflow typeWorkflow = new CTypeWorkflow(etape.ContexteDonnee);
                if (typeWorkflow.ReadIfExists(DbKeyTypeWorkflow))
                {
                    CWorkflow workflow = new CWorkflow(etape.ContexteDonnee);
                    workflow.CreateNewInCurrentContexte();
                    workflow.TypeWorkflow = typeWorkflow;
                    workflow.Libelle = etape.Libelle;
                    workflow.EtapeAppelante = etape;
                    etape.WorkflowLancé = workflow;
                    return workflow;
                }
            }
            return null;

        }

        //---------------------------------------------------
        public override CResultAErreur RunAndSaveIfOk(CEtapeWorkflow etape)
        {
            CResultAErreur result = CResultAErreur.True;
            if ( DbKeyTypeWorkflow == null )
            {
                return EndAndSaveIfOk(etape);
            }
            CTypeEtapeWorkflow typeEtapeDémarrage = null;
            if (m_dbKeyTypeEtapeDemarrage != null)
            {
                typeEtapeDémarrage = new CTypeEtapeWorkflow(etape.ContexteDonnee);
                if (!typeEtapeDémarrage.ReadIfExists(m_dbKeyTypeEtapeDemarrage))
                    typeEtapeDémarrage = null;
            }
            CWorkflow workflow = GetOrCreateWorkflowInCurrentContexte(etape);
            if (workflow != null && !workflow.IsRunning)
            {
                result = PrepareToStartWorkflow(etape);
                if (workflow.TypeWorkflow.Etapes.Count == 0)
                    return EndAndSaveIfOk(etape);
                result = workflow.DémarreWorkflow(typeEtapeDémarrage, false);
            }
            return etape.ContexteDonnee.SaveAll(true);
        }

        //---------------------------------------------------
        protected virtual CResultAErreur PrepareToStartWorkflow(CEtapeWorkflow etape)
        {
            CResultAErreur result = CResultAErreur.True;
            CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(etape);
            if (m_formuleInitialisationWorkflow != null)
            {
                result = m_formuleInitialisationWorkflow.Eval(ctxEval);
                if (!result)
                {
                    return result;
                }
            }
            return result;
        }

        //---------------------------------------------------
        public override CResultAErreur StartWithPath ( CEtapeWorkflow etape, string strPath )
        {
            CResultAErreur result = CResultAErreur.True;
            CWorkflow workflow = GetOrCreateWorkflowInCurrentContexte(etape);
            if (workflow != null)
            {
                if (!workflow.IsRunning)
                {
                    result = PrepareToStartWorkflow(etape);
                    if (!result)
                        return result;
                }
                ArrayList lst = new ArrayList();
                lst.Add(strPath);
                if ( ! workflow.StartWorkflowMultiSteps(lst) )
                {
                    result.EmpileErreur(I.T("Error while launching sub workflow from step @1|20108", etape.Libelle));
                }
            }
            return result;
        }

        //---------------------------------------------------
        public override void OnCancelStep(CEtapeWorkflow etape)
        {
            if (etape.WorkflowLancé != null)
            {
                etape.WorkflowLancé.StopWorkflow();
            }
        }

        //---------------------------------------------------
        /// <summary>
        /// executée lorsque le workflow redémarre fictivement suite
        /// au redémarrage d'un étape du sous workflow
        /// </summary>
        public virtual CResultAErreur OnBlocRedemarréParUneEtapeDuSousWorkflow(CEtapeWorkflow etape)
        {
            return CResultAErreur.True;
        }
    }
}
