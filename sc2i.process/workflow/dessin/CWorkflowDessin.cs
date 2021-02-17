using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using sc2i.common;
using System.Drawing;

namespace sc2i.process.workflow.dessin
{
    public class CWorkflowDessin : C2iObjetGraphique
    {
        private CTypeWorkflow m_typeWorkflow = null;

        private List<C2iObjetGraphique> m_listeFils = new List<C2iObjetGraphique>();

        //----------------------------------------------------------
        public CWorkflowDessin()
            :base()
        {
            Size = new Size(900, 1272);

        }

        //----------------------------------------------------------
        public CWorkflowDessin(CTypeWorkflow typeWorkflow)
            : this()
        {
            m_typeWorkflow = typeWorkflow;
        }

        //----------------------------------------------------------
        public override bool NoMove
        {
            get
            {
                return true;
            }
        }

        //----------------------------------------------------------
        public override bool NoDelete
        {
            get
            {
                return true;
            }
        }

        //----------------------------------------------------------
        public override bool NoPoignees
        {
            get
            {
                return true;
            }
        }

        //----------------------------------------------------------
        public IWorflowDessin GetChild(string strIdUniversel)
        {
            return (IWorflowDessin)m_listeFils.FirstOrDefault(e => e is IWorflowDessin && ((IWorflowDessin)e).IdUniversel == strIdUniversel);
        }

        

        //----------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public CTypeWorkflow TypeWorkflow
        {
            get
            {
                return m_typeWorkflow;
            }
            set
            {
                m_typeWorkflow = value;
            }
        }   

        //----------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public override I2iObjetGraphique[] Childs
        {
            get { return m_listeFils.ToArray(); }
        }

        //----------------------------------------------------------
        public override bool AddChild(I2iObjetGraphique child)
        {
            if (child is C2iObjetGraphique)
            {
                m_listeFils.Add(child as C2iObjetGraphique);
                return true;
            }
            return false;
        }

        //----------------------------------------------------------
        public override bool ContainsChild(I2iObjetGraphique child)
        {
            return m_listeFils.Contains(child as C2iObjetGraphique);

        }

        //----------------------------------------------------------
        public override void RemoveChild(I2iObjetGraphique child)
        {
            C2iObjetGraphique objet = child as C2iObjetGraphique;
            if (objet != null && m_listeFils.Contains(objet))
            {
                m_listeFils.Remove(objet);
            }

        }

        //----------------------------------------------------------
        public override void DeleteChild(I2iObjetGraphique child)
        {
            C2iObjetGraphique objet = child as C2iObjetGraphique;
            if (objet != null && m_listeFils.Contains(objet))
            {
                IWorflowDessin wkf = child as IWorflowDessin;
                if (wkf != null)
                {
                    CResultAErreur result = wkf.Delete();
                    if (!result)
                        return;
                }
            }
            base.DeleteChild(child);
        }

        //----------------------------------------------------------
        public override void BringToFront(I2iObjetGraphique child)
        {
            C2iObjetGraphique objet = child as C2iObjetGraphique;
            if (objet != null && ContainsChild(objet))
            {
                m_listeFils.Remove(objet);
                m_listeFils.Insert(0, objet);
            }
        }

        //----------------------------------------------------------
        public override void FrontToBack(I2iObjetGraphique child)
        {
            C2iObjetGraphique objet = child as C2iObjetGraphique;
            if (objet != null && ContainsChild(objet))
            {
                m_listeFils.Remove(objet);
                m_listeFils.Add(objet);
            }
        }


        //----------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------------------
        protected override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteListe<C2iObjetGraphique>(m_listeFils);
            if (result && serializer.Mode == ModeSerialisation.Lecture)
            {
                foreach (C2iObjetGraphique objet in m_listeFils)
                    objet.Parent = this;
            }

            return result;
        }

        //----------------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            Rectangle rct = RectangleAbsolu;
            rct.Width -= 5;
            rct.Height -= 5;
            rct.Offset(5, 5);
            Brush br = new SolidBrush(Color.White);
            Brush brOmbre = new SolidBrush(Color.FromArgb(64, 0, 0, 0));
            ctx.Graphic.FillRectangle(brOmbre,rct);
            brOmbre.Dispose();
            rct.Offset(-5, -5);
            ctx.Graphic.FillRectangle(br, rct);
            Pen pen = new Pen(Color.Black);
            ctx.Graphic.DrawRectangle(pen, rct);
            br.Dispose();
            pen.Dispose();
        }

        //----------------------------------------------------------
        public IEnumerable<CWorkflowLienDessin> GetLiensSortants ( CWorkflowEtapeDessin objet )
        {
            return from l in Childs
                   where l is CWorkflowLienDessin &&
                       ((CWorkflowLienDessin)l).Lien.EtapeSource == objet.TypeEtape
                   select l as CWorkflowLienDessin;
        }

        //----------------------------------------------------------
        public IEnumerable<CWorkflowLienDessin> GetLiensEntrants(CWorkflowEtapeDessin objet)
        {
            return from l in Childs
                   where l is CWorkflowLienDessin &&
                       ((CWorkflowLienDessin)l).Lien.EtapeDestination== objet.TypeEtape
                   select l as CWorkflowLienDessin;
        }

    }
}
