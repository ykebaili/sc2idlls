using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using System.ComponentModel;
using System.Drawing.Design;
using sc2i.formulaire.subform;

namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iContainerSousFormulaire.
	/// </summary>
    /// 
    [WndName("Sub form")]
	[Serializable]
	public class C2iWndConteneurSousFormulaire : C2iWndComposantFenetre
	{
        private C2iWndPanel.PanelBorderStyle m_borderStyle = C2iWndPanel.PanelBorderStyle._3D;
        private bool m_bAutosize = false;
        private C2iWndReference m_subFormRef = null;
        private C2iExpression m_formuleElementEdite = null;

        private List<CFormuleNommee> m_listeParametresSubForm = new List<CFormuleNommee>();

		public C2iWndConteneurSousFormulaire()
            :base()
		{
            Size = new Size(100, 50);
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		

		/// ///////////////////////
		[System.ComponentModel.Browsable(false)]
		public override bool AcceptChilds
		{
			get
			{
				return false;
			}
		}

        /// ///////////////////////
        /// ///////////////////////////////////////
        [TypeConverter(typeof(CTableauFormuleNommeeConvertor))]
        [System.ComponentModel.Editor(typeof(CListeFonctionsEditor), typeof(UITypeEditor))]
        public CFormuleNommee[] SubFormParameters
        {
            get
            {
                if (m_listeParametresSubForm == null)
                    m_listeParametresSubForm = new List<CFormuleNommee>();
                return m_listeParametresSubForm.ToArray();
            }
            set
            {
                m_listeParametresSubForm.Clear();
                if (value != null)
                    m_listeParametresSubForm.AddRange(value);
            }
        }


        /// ///////////////////////
        public C2iWndPanel.PanelBorderStyle BorderStyle
        {
            get{
                return m_borderStyle;
            }
            set{m_borderStyle = value;
            }
        }

        public bool Autosize
        {
            get
            {
                return m_bAutosize;
            }
            set
            {
                m_bAutosize = value;
            }
        }

        /// ///////////////////////
        [TypeConverter(typeof(C2iWndReferenceConvertor))]
        [Editor(typeof(C2iWndReferenceEditor), typeof(UITypeEditor))]
        [DefaultValue(null)]
        public C2iWndReference SubFormReference
        {
            get
            {
                return m_subFormRef;
            }
            set
            {
                m_subFormRef = value;
                if (Autosize && m_subFormRef != null)
                {
                    C2iWnd frm = C2iWndProvider.GetForm(m_subFormRef);
                    if (frm != null)
                    {
                        int nborder = 0;
                        switch (BorderStyle)
                        {
                            case C2iWndPanel.PanelBorderStyle._3D:
                                nborder = 2;
                                break;
                            case C2iWndPanel.PanelBorderStyle.Plein:
                                nborder = 1;
                                break;
                        }
                        Size = new Size(frm.Size.Width + 2*nborder, frm.Size.Height + 2*nborder);
                    }
                }
            }
        }

        /// ///////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
        [DefaultValue(null)]
        public C2iExpression EditedElement
        {
            get
            {
                return m_formuleElementEdite;
            }
            set
            {
                m_formuleElementEdite = value;
            }
        }


		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle ( Position , Size );
			//rect = contexte.ConvertToAbsolute(rect);
			g.FillRectangle(b, rect);
			b.Dispose();
            Image img = Resources.SubForm;
            g.DrawImage(img, rect);
            switch (BorderStyle)
            {
                case C2iWndPanel.PanelBorderStyle._3D:
                    DrawCadre3D(rect, true, g);
                    break;
                case C2iWndPanel.PanelBorderStyle.Plein:
                    Pen pen = new Pen(ForeColor);
                    g.DrawRectangle(pen, rect);
                    pen.Dispose();
                    break;
            }
			base.MyDraw ( ctx );
		}

		

		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
            serializer.TraiteBool(ref m_bAutosize);
            int nTmp = (int)m_borderStyle;
            serializer.TraiteInt(ref nTmp);
            m_borderStyle = (C2iWndPanel.PanelBorderStyle)nTmp;
            if (nVersion >= 1)
            {
                result = serializer.TraiteObject<C2iWndReference>(ref m_subFormRef);
                if (result)
                    result = serializer.TraiteObject<C2iExpression>(ref m_formuleElementEdite);
                if ( !result )
                    return result;
            }
            if (nVersion >= 2)
                result = serializer.TraiteListe<CFormuleNommee>(m_listeParametresSubForm);
			return result;
		}

        /// ///////////////////////////////////////
        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());

            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "SubForm", "SubForm",
                new CTypeResultatExpression(typeof(C2iWnd), false),
                false, false, ""));


            return lst.ToArray();

        }
	}
}
