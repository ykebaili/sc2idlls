using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using sc2i.process.workflow.dessin;
using sc2i.drawing;

namespace sc2i.process.workflow.blocs
{
    public abstract class CBlocWorkflow : I2iSerializable
    {
        private CTypeEtapeWorkflow m_typeEtape = null;
        private string m_strLibelleForDebug = "";

        //---------------------------------------------------
        public CBlocWorkflow()
        {
        }

        //---------------------------------------------------
        public abstract Size DefaultSize { get; }

        //-----------------------------------------------------
        public abstract EModeGestionErreurEtapeWorkflow ModeGestionErreur{get;}

        //---------------------------------------------------
        [DynamicField("Label for Debug")]
        public string LibelleForDebug
        {
            get
            {
                return m_strLibelleForDebug;
            }
            set
            {
                m_strLibelleForDebug = value;
            }
        }

        //---------------------------------------------------
        public CBlocWorkflow(CTypeEtapeWorkflow typeEtape)
            :this()
        {
            m_typeEtape = typeEtape;
        }

        //---------------------------------------------------
        public CTypeEtapeWorkflow TypeEtape
        {
            get
            {
                return m_typeEtape;
            }
            set
            {
                m_typeEtape = value;
            }
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }


        //---------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;

            result = MySerialize ( serializer );

            return result;
        }

        //---------------------------------------------------
        public abstract CResultAErreur MySerialize ( C2iSerializer serializer );

        //---------------------------------------------------
        public abstract Point[] GetPolygoneDessin(CWorkflowEtapeDessin donneesDessin);

        //---------------------------------------------------
        public void Draw(CContextDessinObjetGraphique contexte, CWorkflowEtapeDessin etape)
        {
            MyDraw(contexte, etape);
            if (etape != null && etape.TypeEtape != null && etape.TypeEtape.IsDefautStart)
            {
                int? nMinxX = null;
                int? nMinY = null;
                int? nMaxX = null;
                foreach (Point pt in GetPolygoneDessin(etape))
                {
                    if (nMinxX == null || nMinxX.Value > pt.X)
                        nMinxX = pt.X;
                    if (nMinY == null || nMinY.Value > pt.Y)
                        nMinY = pt.Y;
                    if (nMaxX == null || nMaxX.Value < pt.X)
                        nMaxX = pt.X;
                }
                Point ptImage = new Point((nMaxX.Value - nMinxX.Value) / 2 + nMinxX.Value - Resource1.Start.Width / 2,
                    nMinY.Value);
                contexte.Graphic.DrawImageUnscaled(Resource1.Start, ptImage);
            }
        }


        //---------------------------------------------------
        protected void DrawAffectationIcon(CContextDessinObjetGraphique contexte, CWorkflowEtapeDessin etape, Point pt)
        {
            Image img = IsBlocAInterfaceUtilisateur?Resource1._1346459174_group_alerte:Resource1._1346459174_group;
            contexte.Graphic.DrawImageUnscaled(img, pt);
        }



        //---------------------------------------------------
        protected abstract void MyDraw ( CContextDessinObjetGraphique contexte, CWorkflowEtapeDessin donneesDessin );


        //---------------------------------------------------
        public abstract string BlocName { get; }

        //---------------------------------------------------
        public abstract string BlocTypeCode { get; }

        //---------------------------------------------------
        public abstract Image BlocImage { get; }

        //---------------------------------------------------
        public abstract bool IsBlocAInterfaceUtilisateur { get; }

        //---------------------------------------------------
        public abstract CResultAErreur RunAndSaveIfOk(CEtapeWorkflow EtapeWorkflow);

        //---------------------------------------------------
        //Appellée par Workflow.StartWorkflowMultiSteps, pour lancer ou relancer un
        //workflow à des étapes données, les étapes pouvant être dans des sous workflows.
        public virtual CResultAErreur StartWithPath(CEtapeWorkflow etape, string strPath)
        {
            return CResultAErreur.True;
        }

        //---------------------------------------------------
        public virtual CResultAErreur EndEtapeNoSave(CEtapeWorkflow etape)
        {
            return etape.InternalSetInfosTerminéeInCurrentContexte(EEtatEtapeWorkflow.Terminée);
        }

        //---------------------------------------------------
        public CResultAErreur EndAndSaveIfOk(CEtapeWorkflow etape)
        {
            //Termine simplement l'étape
            CResultAErreur result = EndEtapeNoSave(etape);
            if ( result )
                result = etape.ContexteDonnee.SaveAll(true);
            return result;
        }



        //---------------------------------------------------
        public virtual CResultAErreur GetErreursManualEndEtape(CEtapeWorkflow etape)
        {
            return CResultAErreur.True;
        }

        //---------------------------------------------------
        [DynamicField("Possible return codes")]
        public virtual string[] CodesRetourPossibles
        {
            get
            {
                return new string[0];
            }
        }




        //---------------------------------------------------
        public virtual void OnCancelStep( CEtapeWorkflow etape)
        {
            //Rien à faire en général, sauf pour certains types de blocs
        }

        
    }
}
