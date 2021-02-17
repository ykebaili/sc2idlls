using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
    public class CCtrlShowErreurValidation : CCtrlShowErreur
    {
		public CCtrlShowErreurValidation()
			:base()
        {
			DoubleBuffered = true;
        }
		public override void Initialiser(IErreur err)
		{
			base.Initialiser(err);
			if (err is CErreurValidation && ((CErreurValidation)err).IsAvertissement)
				m_panIco.BackgroundImage = Properties.Resources.ImgExclam;
			else
				m_panIco.BackgroundImage = Properties.Resources.ImgErreur_old;
		}
    }
}
