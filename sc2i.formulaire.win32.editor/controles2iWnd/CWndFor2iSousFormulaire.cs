using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using System.Drawing;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndFor2iSousFormulaire : CWndFor2iFenetre
	{
        private IControleWndFor2iWnd m_conteneurSousFormulaire = null;
		public new static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndSousFormulaire), typeof(CWndFor2iSousFormulaire));
		}

		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			base.MyCreateControle(createur, wnd, parent, fournisseurProprietes);
            ScrollableControl ctrl = this.Control as ScrollableControl;
            if (ctrl != null && wnd is C2iWndSousFormulaire)
                ctrl.AutoScroll = ((C2iWndSousFormulaire)wnd).AutoScroll;

		}

		protected override void AfterCreateChilds()
		{
			C2iWndSousFormulaire sousFormulaire = WndAssociee as C2iWndSousFormulaire;
			if (sousFormulaire != null && sousFormulaire.AdjustToContent)
			{
				foreach (IControleWndFor2iWnd ctrl in Childs)
                    if (ctrl.Control != null)
                    {
                        ctrl.Control.SizeChanged += new EventHandler(Control_SizeChanged);
                        ctrl.Control.VisibleChanged += new EventHandler(Control_SizeChanged);
                    }
			}			
		}

		void Control_SizeChanged(object sender, EventArgs e)
		{
			RecalcSize();
		}

		private void RecalcSize()
		{
			if (Control == null)
				return;
			int nXMax = 0;
			int nYMax = 0;
            if (Control != null)
            {
                Control.SuspendLayout();
                Control.ResumeLayout();
            }
			foreach (IControleWndFor2iWnd ctrl in Childs)
			{
				if (ctrl.Control != null)
				{
					if (ctrl.Control.Right > nXMax)
						nXMax = ctrl.Control.Right;
					if (ctrl.Control.Bottom > nYMax)
						nYMax = ctrl.Control.Bottom;
				}
			}
			if (nXMax > 0 && nYMax > 0)
			{
				Control.Size = new Size(nXMax, nYMax);
			}
		}

        public override IWndAContainer WndContainer
        {
            get
            {
                return m_conteneurSousFormulaire;
            }
            set
            {
                m_conteneurSousFormulaire = value as IControleWndFor2iWnd;
            }
        }

        public override bool IsRacineForEvenements
        {
            get
            {
                return true;
            }
        }
	}
}
