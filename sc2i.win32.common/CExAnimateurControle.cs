using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Windows;
using System.Drawing;
using sc2i.win32.common;
using System.Runtime.InteropServices;

namespace sc2i.win32.common
{
	
	[ProvideProperty("AnimateOnEnter", typeof(Control))]
	[ProvideProperty("AnimateOnMouseDown", typeof(Control))]
	public class CExAnimateurControle : Component, IExtenderProvider
	{
		public enum EAnimationControl
		{
			None,
			Zoom,
			DoubleZoom
		}
		private Dictionary<Control, EAnimationControl> m_tableValeursOnEnter = new Dictionary<Control, EAnimationControl>();
		private Dictionary<Control, EAnimationControl> m_tableValeursOnMouseDown = new Dictionary<Control, EAnimationControl>();
		private EventHandler OnEnterEventHandler = null;
		private C2iComboSelectDynamicClass c2iComboSelectDynamicClass1;
		private IContainer components;
		private MouseEventHandler OnMouseDownEventHandler = null;


		public CExAnimateurControle()
		{
			OnEnterEventHandler += new EventHandler(OnEnterControl);
			OnMouseDownEventHandler += new MouseEventHandler(OnMouseDownControl);
		}

		public bool CanExtend(object extendee)
		{
			return extendee is Control;
		}

		/////////////////////////////////////////
		public void SetAnimateOnEnter(object extendee, EAnimationControl eAnimate)
		{
			if (!(extendee is Control))
				return;

			if (eAnimate != EAnimationControl.None)
			{
				m_tableValeursOnEnter[(Control)extendee] = eAnimate;
				((Control)extendee).MouseEnter += OnEnterEventHandler;
			}
			else
			{
				if (m_tableValeursOnEnter.ContainsKey((Control)extendee))
					m_tableValeursOnEnter.Remove((Control)extendee);
			}
		}

		/////////////////////////////////////////
		[DefaultValue(CExAnimateurControle.EAnimationControl.None)]
		public EAnimationControl GetAnimateOnEnter(object extendee)
		{
			EAnimationControl eVal = EAnimationControl.None;
			if (extendee is Control)
			{
				if (m_tableValeursOnEnter.TryGetValue((Control)extendee, out eVal))
					return eVal;
			}
			return EAnimationControl.None;
		}

		/////////////////////////////////////////
		public bool ShouldSerializeAnimateOnEnter(object control)
		{
			return GetAnimateOnEnter(control) != EAnimationControl.None;
		}

		/////////////////////////////////////////
		public void SetAnimateOnMouseDown(object extendee, EAnimationControl eAnimate)
		{
			if (!(extendee is Control))
				return;

			if (eAnimate != EAnimationControl.None)
			{
				m_tableValeursOnMouseDown[(Control)extendee] = eAnimate;
				((Control)extendee).MouseDown += OnMouseDownEventHandler;
			}
			else
			{
				if (m_tableValeursOnMouseDown.ContainsKey((Control)extendee))
					m_tableValeursOnMouseDown.Remove((Control)extendee);
			}
		}

		

		/////////////////////////////////////////
		[DefaultValue(CExAnimateurControle.EAnimationControl.None)]
		public EAnimationControl GetAnimateOnMouseDown(object extendee)
		{
			EAnimationControl eVal = EAnimationControl.None;
			if (extendee is Control)
			{
				if (m_tableValeursOnMouseDown.TryGetValue((Control)extendee, out eVal))
					return eVal;
			}
			return EAnimationControl.None;
		}

		/////////////////////////////////////////
		public bool ShouldSerializeAnimateOnMouseDown(object control)
		{
			return GetAnimateOnMouseDown(control) != EAnimationControl.None;
		}

		private class CLockerAnimation
		{
		}


		[DllImport("user32")]
		internal static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport("User32.dll")]
		internal static extern void ReleaseDC(IntPtr dc);


		/////////////////////////////////////////
		public void OnEnterControl ( object control, EventArgs args )
		{
			if ( !(control is Control ) || ((Control)control).Parent == null)
				return;
			EAnimationControl animation = GetAnimateOnEnter(control);
			if ( animation != EAnimationControl.None )
				((Control)control).BeginInvoke(new StartAnimateDelegate(StartAnimate), (Control)control, animation);
		}

		/////////////////////////////////////////
		public void OnMouseDownControl(object control, MouseEventArgs args)
		{
			if (!(control is Control) || ((Control)control).Parent == null)
				return;
			EAnimationControl animation = GetAnimateOnMouseDown(control);
			if (animation != EAnimationControl.None)
				((Control)control).BeginInvoke(new StartAnimateDelegate(StartAnimate), (Control)control, animation);
		}

		delegate void StartAnimateDelegate(Control ctrl, EAnimationControl animation);

		/////////////////////////////////////////
		private void StartAnimate(Control control, EAnimationControl animation)
		{
			//Sauve le fond
			Bitmap bmpFond = CScreenCopieur.GetDesktopImage();
			//Copie l'image du contrôle
			Bitmap bmpControle = CScreenCopieur.GetWindowImage((Control)control);

			IntPtr deskDC = GetDC(IntPtr.Zero);
			Graphics g = Graphics.FromHdc(deskDC);
			
			Point pt = ((Control)control).Parent.PointToScreen(((Control)control).Location);
			Size sz = ((Control)control).Size;
			Rectangle maxRect = new Rectangle(0, 0, 0, 0);
			
			int nNbZoom = animation == EAnimationControl.Zoom ? 1 : 2;
			for (int nZoom = 0; nZoom < nNbZoom; nZoom++)
			{
				double fPas = 6;
				double fSize = 0;
				double fRatio = (double)sz.Height / (double)sz.Width;
				while (true)
				{
					fSize += fPas;
					if (fPas > 0)
					{
						fPas /= 1.1;
						if (fPas < 0.1) fPas = 0.1;
					}
					else
					{
						fPas *= 1.1;
					}
					double fNewWidth = (double)sz.Width + fSize;
					double fNewHeight = (double)sz.Height + fSize * fRatio;
					double fEcartWidth = (fNewWidth - sz.Width) / 2.0;
					double fEcartHeight = (fNewHeight - sz.Height) / 2.0;
					Rectangle rct = new Rectangle((int)(pt.X - fEcartWidth),
						(int)(pt.Y - fEcartHeight),
						(int)(sz.Width + fEcartWidth * 2),
						(int)(sz.Height + fEcartHeight * 2));
					g.DrawImage(bmpControle, rct);
					if (rct.Width > maxRect.Width ||
						rct.Height > maxRect.Height)
						maxRect = rct;
					if (fSize >= 25)
					{
						fPas = -fPas;
					}
					if (fSize < 1)
						break;
					System.Threading.Thread.Sleep(2);
					//maxRect.Inflate(4, 4);
					g.DrawImage(bmpFond, maxRect, maxRect, GraphicsUnit.Pixel);
				}
				System.Threading.Thread.Sleep(2);
			}
			//Redessine le fond
			g.DrawImage(bmpFond, maxRect, maxRect, GraphicsUnit.Pixel);
			g.Dispose();
			bmpControle.Dispose();
			bmpFond.Dispose();
			
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.c2iComboSelectDynamicClass1 = new sc2i.win32.common.C2iComboSelectDynamicClass(this.components);
			// 
			// c2iComboSelectDynamicClass1
			// 
			this.c2iComboSelectDynamicClass1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.c2iComboSelectDynamicClass1.FormattingEnabled = true;
			this.c2iComboSelectDynamicClass1.IsLink = false;
			this.c2iComboSelectDynamicClass1.Location = new System.Drawing.Point(0, 0);
			this.c2iComboSelectDynamicClass1.LockEdition = false;
			this.c2iComboSelectDynamicClass1.Name = "c2iComboSelectDynamicClass1";
			this.c2iComboSelectDynamicClass1.Size = new System.Drawing.Size(121, 21);
			this.c2iComboSelectDynamicClass1.TabIndex = 0;
			this.c2iComboSelectDynamicClass1.TypeSelectionne = null;

		}	
	}
}
