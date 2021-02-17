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
using sc2i.data;

namespace sc2i.process.workflow.blocs
{
    public class CBlocWorkflowProcess : CBlocWorkflow
    {
        // TESTDBKEYOK
        private CDbKey m_dbKeyProcess = null;
        private CProcess m_process = null;
        private bool m_bDemarrageManuel = false;
        private string m_strInstructions = "";
        private bool m_bUtiliserSortieDeProcessCommeCodeRetour = false;

        //TODO : pouvoir récupérer la variable de retour du process comme ActivationCodes 

        //-----------------------------------------------------
        public CBlocWorkflowProcess()
            : base()
        {
        }

        //-----------------------------------------------------
        public CBlocWorkflowProcess(CTypeEtapeWorkflow typeEtape)
            : base(typeEtape)
        {
        }

        //-----------------------------------------------------
        public string Instructions
        {
            get
            {
                return m_strInstructions;
            }
            set
            {
                m_strInstructions = value;
            }
        }

        //---------------------------------------------------
        [ExternalReferencedEntityDbKey(typeof(CProcessInDb))]
        public CDbKey DbKeyProcess
        {
            get
            {
                return m_dbKeyProcess;
            }
            set
            {
                m_dbKeyProcess = value;
            }
        }

        //---------------------------------------------------
        public CProcess Process
        {
            get
            {
                return m_process;
            }
            set
            {
                m_process = value;
            }
        }

        //---------------------------------------------------
        /// <summary>
        /// Si true, le process s'exectue lorsque l'utilisateur affecté le demande
        /// </summary>
        public bool DemarrageManuel
        {
            get
            {
                return m_bDemarrageManuel;
            }
            set
            {
                m_bDemarrageManuel = value;
            }
        }

        //---------------------------------------------------
        public override Size DefaultSize
        {
            get
            {
                return new Size(120, 50);
            }
        }

        //---------------------------------------------------
        public bool UtiliserLaValeurDeSortieDeProcessCommeCodeRetour
        {
            get
            {
                return m_bUtiliserSortieDeProcessCommeCodeRetour;
            }
            set
            {
                m_bUtiliserSortieDeProcessCommeCodeRetour = value;
            }
        }

        
        //---------------------------------------------------
        private int GetNumVersion()
        {
            //return 2; //Ajout de sortie en tant que codes retour
            return 3; //Passage de Id Process en DbKey

        }

        //---------------------------------------------------
        public override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            if (nVersion < 3)
                // TESTDBKEYOK
                serializer.ReadDbKeyFromOldId(ref m_dbKeyProcess, typeof(CProcessInDb));
            else
                serializer.TraiteDbKey(ref m_dbKeyProcess);

            serializer.TraiteBool(ref m_bDemarrageManuel);
            serializer.TraiteString(ref m_strInstructions);
            if (nVersion >= 1)
            {
                result = serializer.TraiteObject<CProcess>(ref m_process);
                if (!result)
                    return result;
            }
            if (nVersion >= 2)
            {
                serializer.TraiteBool(ref m_bUtiliserSortieDeProcessCommeCodeRetour);
            }

            
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

            if (DemarrageManuel)
            {
                Image img = TypeEtape == null || TypeEtape.ParametresInitialisation.Affectations.Formules.Count() == 0 ?
                    Resource1._1346459174_group_alerte :
                    Resource1._1346459174_group;
                if (rct.Size.Width > img.Width &&
                    rct.Size.Height > img.Height)
                {
                    contexte.Graphic.DrawImage(img, new Rectangle(rct.Right - img.Width, rct.Top, img.Width, img.Height));
                }
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
            get { return I.T("Process|20070"); }
        }

        //---------------------------------------------------
        public override string BlocTypeCode
        {
            get { return "PROCESS"; }
        }

        //---------------------------------------------------
        public override Image BlocImage
        {
            get { return Resource1._1345538971_kservices; }
        }

        //---------------------------------------------------
        public override bool IsBlocAInterfaceUtilisateur
        {
            get
            {
                return DemarrageManuel;
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
        public override CResultAErreur RunAndSaveIfOk(CEtapeWorkflow etape)
        {
            if (DemarrageManuel)
            {
                return etape.ContexteDonnee.SaveAll(true);
            }
            else
            {
                return StartProcess(etape);
            }
            return CResultAErreur.True;
        }

        //-------------------------------------------------------------
        public CResultAErreur StartProcess(CEtapeWorkflow etape)
        {
            CResultAErreur result = CResultAErreur.True;
            CProcess process = m_process;
            if (process == null)
            {
                if (m_dbKeyProcess != null)
                {
                    CProcessInDb processInDb = new CProcessInDb(etape.ContexteDonnee);
                    if (processInDb.ReadIfExists(m_dbKeyProcess))
                    {
                        process = processInDb.Process;
                    }
                }
            }
            if (process != null)
            {
                if ( process.Libelle.Trim().Length == 0 && etape != null )
                    process.Libelle = I.T("Step @1|20109", etape.Libelle);
                result = CProcessEnExecutionInDb.StartProcess(process, new CInfoDeclencheurProcess(TypeEvenement.Specifique),
                    new CReferenceObjetDonnee(etape),
                    etape.ContexteDonnee.IdSession,
                    null,
                    null);
                if (result)
                {
                    etape.Refresh();//Relit l'étape car elle a pu être modifiée par le process
                    if (m_bUtiliserSortieDeProcessCommeCodeRetour && (result.Data is string || result.Data is string[]))
                    {
                        if (result.Data is string)
                            etape.CodesRetour = new string[] { (string)result.Data };
                        if (result.Data is string[])
                            etape.CodesRetour = (string[])result.Data;
                    }
                    result = EndAndSaveIfOk(etape);
                }
            }
            return result;
        }
    }
}
