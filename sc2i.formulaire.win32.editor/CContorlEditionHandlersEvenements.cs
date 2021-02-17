using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.formulaire.win32
{
	public partial class CControlEditionHandlersEvenements : UserControl
	{
		//---------------------------------------------
		public CControlEditionHandlersEvenements()
		{
			InitializeComponent();
		}

		//---------------------------------------------
		public void Init(C2iWnd wnd, IFournisseurProprietesDynamiques fournisseur)
		{
			List<CControleEditionHandlerEvenement> lstReserve = new List<CControleEditionHandlerEvenement>();
			this.SuspendDrawing();
			foreach(  Control ctrl in Controls )
				if (ctrl is CControleEditionHandlerEvenement)
				{
					ctrl.Visible = false;
					lstReserve.Add((CControleEditionHandlerEvenement)ctrl);
				}
			if (wnd == null)
			{
				this.ResumeDrawing();
				return;
			}
			foreach (CDescriptionEvenementParFormule desc in wnd.GetDescriptionsEvenements())
			{
				CControleEditionHandlerEvenement ctrl = null;
				if ( lstReserve.Count > 0 )
				{
					ctrl = lstReserve[0];
					lstReserve.RemoveAt ( 0 );
				}
				else
				{
					ctrl = new CControleEditionHandlerEvenement();
					Controls.Add ( ctrl );
				}
				ctrl.Visible = true;
				ctrl.Parent = this;
				ctrl.Dock = DockStyle.Top;
				ctrl.Init(wnd, desc, fournisseur);
				ctrl.BringToFront();
			}
			this.ResumeDrawing();
		}
	}
}
