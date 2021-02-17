using System;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;
using sc2i.common;
using sc2i.drawing;
using System.ComponentModel;
using System.Drawing;

namespace sc2i.formulaire
{
    //Une contrôle qui est attaché à un controle windows 
    //utilisé dans les formulaires en sur impression pour
    //accéder aux controles réels du formulaire.

	[Serializable]
	public class C2iWndControleExterne : C2iWnd, I2iWndComposantFenetre
	{
        private List<CDefinitionProprieteDynamique> m_listeProprietesExternes = new List<CDefinitionProprieteDynamique>();
        private List<CDescriptionEvenementParFormule> m_listeEvenements = new List<CDescriptionEvenementParFormule>();

        //---------------------------------------------------------------
        public C2iWndControleExterne()
			: base()
		{
		}

        //---------------------------------------------------------------
		public override bool CanBeUseOnType(Type tp)
		{
			return false;
        }

        ///////////////////////////////////////////////
        ///////////////////////////////////////////////
        ///////////////////////////////////////////////
        ///////////////////////////////////////////////
        ///Surcharge des propriétés à masquer
        //---------------------------------------------------------------
        [Browsable(false)]
        public override bool AnchorBottom
        {
            get
            {
                return base.AnchorBottom;
            }
            set
            {
                base.AnchorBottom = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override bool AnchorLeft
        {
            get
            {
                return base.AnchorLeft;
            }
            set
            {
                base.AnchorLeft = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override bool AnchorRight
        {
            get
            {
                return base.AnchorRight;
            }
            set
            {
                base.AnchorRight = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override bool AnchorTop
        {
            get
            {
                return base.AnchorTop;
            }
            set
            {
                base.AnchorTop = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override bool AutoBackColor
        {
            get
            {
                return base.AutoBackColor;
            }
            set
            {
                base.AutoBackColor = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override EDockStyle DockStyle
        {
            get
            {
                return base.DockStyle;
            }
            set
            {
                base.DockStyle = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override C2iExpression Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override string HelpText
        {
            get
            {
                return base.HelpText;
            }
            set
            {
                base.HelpText = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override ELockMode LockMode
        {
            get
            {
                return base.LockMode;
            }
            set
            {
                base.LockMode = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override Point Position
        {
            get
            {
                return base.Position;
            }
            set
            {
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override System.Drawing.Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override int TabOrder
        {
            get
            {
                return base.TabOrder;
            }
            set
            {
                base.TabOrder = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override C2iExpression Visiblity
        {
            get
            {
                return base.Visiblity;
            }
            set
            {
                base.Visiblity = value;
            }
        }

        //---------------------------------------------------------------
        [Browsable(false)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }


        //---------------------------------------------------------------
        [Browsable(false)]
        public override bool IsLock
        {
            get
            {
                return base.IsLock;
            }
            set
            {
            }
        }

        //---------------------------------------------------------------
        [ReadOnly(true)]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        //---------------------------------------------------------------
        public void AttacheToControl(IControleFormulaireExterne ctrl)
        {
            Name = ctrl.Name;
            m_listeProprietesExternes.Clear();
            CDefinitionProprieteDynamique[] defs = ctrl.GetProprietes();
            if (defs != null)
                m_listeProprietesExternes.AddRange(defs);

            m_listeEvenements.Clear();
            CDescriptionEvenementParFormule[] descs = ctrl.GetDescriptionsEvenements();
            if (descs != null)
                m_listeEvenements.AddRange(descs);

            base.IsLock = false;
            I2iObjetGraphique parent = Parent;
            Parent = null;
            base.Size = ctrl.Size;
            base.Position = ctrl.Location;
            Parent = parent;
            base.IsLock = true;
        }


        //---------------------------------------------------------------
        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            return m_listeProprietesExternes.ToArray();
        }

        //---------------------------------------------------------------
        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            return m_listeEvenements.ToArray();
        }

        //---------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------------------
        protected override sc2i.common.CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            
            return result;
        }
	}
}
