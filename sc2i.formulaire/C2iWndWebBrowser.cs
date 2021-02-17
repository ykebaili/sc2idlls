using System;
using System.Drawing;
using System.IO;
using System.Drawing.Design;
using System.Collections.Generic;


using sc2i.expression;
using sc2i.common;
using sc2i.drawing;
using System.ComponentModel;


namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iLabel.
	/// </summary>
	[WndName("Web browser")]
	[Serializable]
	public class C2iWndWebBrowser : C2iWndComposantFenetre
	{
		private C2iExpression m_formuleURL = null;
        private bool m_bEnableContextMenu = true;
        private bool m_bHideErrors = true;
        private bool m_bEnableShortcuts = true;
        private bool m_bAllowNavigation = true;
        private bool m_bPreventDownload = false;

		/// ///////////////////////
		public C2iWndWebBrowser()
		{
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		

		/// ///////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		public C2iExpression UrlFormula
		{
			get
			{
				return m_formuleURL;
			}
			set
			{
				m_formuleURL = value;
			}
		}

        /// ///////////////////////
        public bool AllowNavigation
        {
            get
            {
                return m_bAllowNavigation;
            }
            set
            {
                m_bAllowNavigation = value;
            }
        }

        /// ///////////////////////
        public bool PreventDownload
        {
            get
            {
                return m_bPreventDownload;
            }
            set
            {
                m_bPreventDownload = value;
            }
        }


        /// ///////////////////////
        public bool BrowserContextMenu
        {
            get
            {
                return m_bEnableContextMenu;
            }
            set
            {
                m_bEnableContextMenu = value;
            }
        }

        /// ///////////////////////
        public bool HideErrors
        {
            get
            {
                return m_bHideErrors;
            }
            set
            {
                m_bHideErrors = value;
            }
        }

        /// ///////////////////////
        public bool EnableShortCuts
        {
            get
            {
                return m_bEnableShortcuts;
            }
            set
            {
                m_bEnableShortcuts = value;
            }
        }
		
		

#if PDA
#else
		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle ( Position , Size );
            g.DrawImage(Resources.webfuturocom, rect);
			b.Dispose();
			base.MyDraw ( ctx );
		}
		
#endif
		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
			//Ajout de l'image variable
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
            serializer.TraiteBool ( ref m_bEnableContextMenu );
            serializer.TraiteBool(ref m_bHideErrors);
            serializer.TraiteBool(ref m_bEnableShortcuts);
            serializer.TraiteBool(ref m_bAllowNavigation);
            serializer.TraiteBool(ref m_bPreventDownload);

            result = serializer.TraiteObject<C2iExpression>(ref m_formuleURL);

			return result;
		}

        //-------------------------------------------------------------
        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();

            lst.Add(new CDefinitionMethodeDynamique(
                "Navigate", "Navigate",
                new CTypeResultatExpression(typeof(void), false),
                false,
                I.T("Navitage to specified URL|20022"),
                new string[]{
                    I.T("URL|20023")}));

            lst.Add(new CDefinitionMethodeDynamique(
                "GoBack", "GoBack",
                new CTypeResultatExpression(typeof(void), false),
                false,
                I.T("Navitage to previous page|20024"),
                new string[0]));

            lst.Add(new CDefinitionMethodeDynamique(
                "GoForward", "GoForward",
                new CTypeResultatExpression(typeof(void), false),
                false,
                I.T("Navitage to next page|20025"),
                new string[0]));

            lst.Add(new CDefinitionMethodeDynamique(
                "GoHome", "GoHome",
                new CTypeResultatExpression(typeof(void), false),
                false,
                I.T("Navitage to home page|20026"),
                new string[0]));


            return lst.ToArray();
        }

       

      
	}
}
