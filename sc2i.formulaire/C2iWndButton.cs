using System;
using System.Drawing;
using System.Collections;
using System.Drawing.Design;


using sc2i.common;
using sc2i.formulaire;
using System.Collections.Generic;
using sc2i.expression;
using System.ComponentModel;

namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iWndButton.
	/// </summary>
	[WndName("Button")]
	public class C2iWndButton : C2iWndComposantFenetre
	{
		public  static string c_strIdEvenementClick = "OnClick";

		private string m_strText = "";
        private int m_nDelayInSeconds = 0;

        private CActionSur2iLink m_action = null;

		public C2iWndButton()
		{
			BackColor = SystemColors.Control;
            LockMode = ELockMode.Independant;
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		/// ////////////////////////////////////
		private int GetNumVersion()
		{
			return 3;
            //2 : ajout de delay in seconds
            //3 : ajout de action sur link
		}

		/// ///////////////////////////////////////
		public string Text
		{
			get
			{
				return m_strText;
			}
			set
			{
				m_strText = value;
			}
		}

        /////////////////////////////////////////////////////////////////
        [TypeConverter(typeof(CActionSur2iLinkConvertor))]
        [System.ComponentModel.Editor(typeof(CActionSur2iLinkEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(null)]
        public CActionSur2iLink Action
        {
            get
            {
                return m_action;
            }
            set
            {
                m_action = value;
            }
        }

        /// ////////////////////////////////////
        public int AutoClickInSeconds
        {
            get
            {
                return m_nDelayInSeconds;
            }
            set
            {
                m_nDelayInSeconds = value;
            }
        }

		/// ////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			if (nVersion >= 1)
				serializer.TraiteString(ref m_strText);
            if (nVersion >= 2)
                serializer.TraiteInt(ref m_nDelayInSeconds);
            if (nVersion >= 3)
                serializer.TraiteObject<CActionSur2iLink>(ref m_action);

			return result;
		}

		protected override void MyDraw(sc2i.drawing.CContextDessinObjetGraphique ctx)
		{
            Graphics g = ctx.Graphic;
            Brush b = new SolidBrush(BackColor);
            int nHeightBtn = Size.Height;
            if (Size.Height > 4 && AutoClickInSeconds > 0 )
                nHeightBtn = Size.Height - 4;
            Rectangle rect = new Rectangle(Position, new Size(Size.Width, nHeightBtn));
            g.FillRectangle(b, rect);
            b.Dispose();
            Pen pen = new Pen(ForeColor);
            g.DrawRectangle(pen, rect);
            pen.Dispose();
            pen = new Pen(Color.FromArgb(205, 210, 224), 2);
            rect.Offset(2, 2);
            rect.Width -= 3;
            rect.Height -= 3;
            g.DrawRectangle(pen, rect);
            pen.Dispose();
            if (Size.Height > 4 && AutoClickInSeconds > 0)
            {
                rect = new Rectangle(rect.Left, rect.Top + Size.Height - 4, Size.Width, 4);
                b = new SolidBrush(Color.Red);
                g.FillRectangle(b, rect);
                b.Dispose();
                rect = new Rectangle(rect.Left, rect.Top, Math.Max(Size.Width / 3, 1), 4);
                rect.Width = Size.Width / 3;
                b = new SolidBrush(Color.Green);
                g.FillRectangle(b, rect);
                b.Dispose();
            }
            base.MyDraw(ctx);

		}

		public override void DrawInterieur(sc2i.drawing.CContextDessinObjetGraphique ctx)
		{
			Brush br = new SolidBrush(ForeColor);
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			ctx.Graphic.DrawString(Text, Font, br, ClientRect, sf);
			br.Dispose();
		}

		public override sc2i.expression.CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
			lst.Add(new CDefinitionProprieteDynamiqueDeportee(
				"Text", "Text",
				new CTypeResultatExpression(typeof(string), false),
				false,
				false,
				""));
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "AutoClickDelay", "DelayInSeconds",
                new CTypeResultatExpression(typeof(int), false),
                false,
                false,
                ""));
			return lst.ToArray();
		}

		public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
		{
			List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
			lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementClick, "OnClick",
				I.T("Occurs when the user click on the button|20054")));
			return lst.ToArray();
		}

        /////////////////////////////////////////////////////////////////
        public override void OnDesignSelect(
            Type typeEdite,
            object objetEdite,
            sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            CActionSur2iLinkEditor.SetObjet(GetObjetPourAnalyseFormule(typeEdite));
        }
	}
 

}
