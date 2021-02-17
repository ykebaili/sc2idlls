using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using sc2i.process.workflow.blocs;
using sc2i.common;
using System.Drawing;
using sc2i.process.workflow;

namespace sc2i.win32.process.workflow
{
    public class CDummyObjetWorkflowPourCreation : C2iObjetGraphique
    {
        private Type m_typeBloc = null;
        private CTypeEtapeWorkflow m_typeEtape = null;

        //------------------------------------------------------------------
        public CDummyObjetWorkflowPourCreation(CTypeEtapeWorkflow typeEtape)
        {
            m_typeEtape = typeEtape;
            if (m_typeEtape.Bloc != null)
                Size = m_typeEtape.Bloc.DefaultSize;
            else
                Size = new Size(120, 50);
        }

        //------------------------------------------------------------------
        public CDummyObjetWorkflowPourCreation(CBlocWorkflow bloc)
            :base()
        {
            if (bloc != null)
            {
                m_typeBloc = bloc.GetType();
                Size = bloc.DefaultSize;
            }
        }

        //------------------------------------------------------------------
        public CTypeEtapeWorkflow TypeEtape
        {
            get
            {
                return m_typeEtape;
            }
        }

        //------------------------------------------------------------------
        public Type TypeBloc
        {
            get
            {
                return m_typeBloc;
            }
        }


        //------------------------------------------------------------------
        public override I2iObjetGraphique[] Childs
        {
            get { return new I2iObjetGraphique[0]; }
        }

        //------------------------------------------------------------------
        public override bool AddChild(I2iObjetGraphique child)
        {
            return false;
        }

        //------------------------------------------------------------------
        public override bool ContainsChild(I2iObjetGraphique child)
        {
            return false;
        }

        //------------------------------------------------------------------
        public override void RemoveChild(I2iObjetGraphique child)
        {
            
        }

        //------------------------------------------------------------------
        public override void BringToFront(I2iObjetGraphique child)
        {
            
        }

        //------------------------------------------------------------------
        public override void FrontToBack(I2iObjetGraphique child)
        {
            
        }

        //------------------------------------------------------------------
        protected override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            return CResultAErreur.True;
        }

        //------------------------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            
        }
    }
}
