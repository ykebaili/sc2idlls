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
	[WndName("Image")]
	[AWndIcone("ico_image")]
	[Serializable]
	public class C2iWndImage : C2iWndComposantFenetre
	{
        public static string c_strIdEvenementClick = "OnClick";

		private Bitmap m_imageDefault = null;

		private C2iExpression m_formuleImageVariable = null;

        private CActionSur2iLink m_action = null;

		/// ///////////////////////
		public C2iWndImage()
		{
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
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

		/// ///////////////////////
		public Bitmap DefaultImage
		{
			get
			{
				return m_imageDefault;
			}
			set
			{
				if ( value == null )
					m_imageDefault = null;
				else
				{
					if ( m_imageDefault != null )
						m_imageDefault.Dispose();
					m_imageDefault = new Bitmap(value.Width, value.Height);
					Graphics g = Graphics.FromImage(m_imageDefault);
					Brush br = new SolidBrush ( BackColor );
					g.FillRectangle ( br, 0, 0, value.Width, value.Height );
					br.Dispose();
					g.DrawImage ( value, new Rectangle ( 0, 0, value.Width, value.Height ) );
					g.Dispose();
				}
			}
		}

		/// ///////////////////////
		public Image GetImageToDisplay(CContexteEvaluationExpression contexte)
		{
			Image imgToCopy = null;
			if (ImageFormula != null && contexte != null)
			{
				try
				{
					CResultAErreur result = ImageFormula.Eval(contexte);
					if (result && result.Data is Bitmap)
					{
						imgToCopy = (Bitmap)result.Data;
					}
				}
				catch
				{
					imgToCopy = null;
				}
			}
			if (imgToCopy == null)
				imgToCopy = DefaultImage;
			if (imgToCopy != null)
			{
				return (Image)imgToCopy.Clone();
			}
			return null;
		}

		/// ///////////////////////
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		public C2iExpression ImageFormula
		{
			get
			{
				return m_formuleImageVariable;
			}
			set
			{
				m_formuleImageVariable = value;
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
			g.FillRectangle(b, rect);
			b.Dispose();
			base.MyDraw ( ctx );
		}

		/// ///////////////////////
		public override void DrawInterieur ( CContextDessinObjetGraphique ctx )
		{
			Graphics g = ctx.Graphic;
			Size sz = ClientSize;
			Rectangle rect = new Rectangle ( 0, 0, (int)sz.Width, (int)sz.Height);
			if ( m_imageDefault != null )
			{
				int nWidth = m_imageDefault.Width;
				int nHeight = m_imageDefault.Height;
				if ( nWidth == 0 || nHeight == 0 )
					return;

				double fRatioImage = (double)nWidth/(double)nHeight;
				double fRatioSize = (double)rect.Width/(double)rect.Height;
				if ( fRatioImage < fRatioSize )
				{
					//Prend toute la hauteur
					nHeight = rect.Height;
					nWidth = (int)(rect.Height * fRatioImage);
				}
				else
				{
					nWidth = rect.Width;
					nHeight = (int)(rect.Width / fRatioImage);
				}
				Rectangle destRect = new Rectangle ( 
					(rect.Width - nWidth)/2,
					(rect.Height-nHeight)/2,
					nWidth,
					nHeight );
				g.DrawImage ( m_imageDefault, destRect);
			}
		}

		
#endif
		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
			//Ajout de l'image variable
            //Ajout d'action
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;

			bool bHasImage = m_imageDefault != null;
			serializer.TraiteBool(ref bHasImage);
			if (bHasImage)
			{
				switch (serializer.Mode)
				{
					case ModeSerialisation.Lecture:
						Byte[] bt = null;
						serializer.TraiteByteArray(ref bt);
                        if (m_imageDefault != null)
                            m_imageDefault.Dispose();
                        m_imageDefault = null;
						MemoryStream stream = new MemoryStream(bt);
						try
						{
							Bitmap bmp = (Bitmap)Bitmap.FromStream(stream);
							m_imageDefault = bmp;
						}
						catch
						{
							m_imageDefault = null;
						}
						stream.Close();
						break;

					case ModeSerialisation.Ecriture:
						MemoryStream streamSave = new MemoryStream();
						try
						{
                            Bitmap copie = new Bitmap(m_imageDefault);
							copie.Save(streamSave, System.Drawing.Imaging.ImageFormat.Png);
                            copie.Dispose();
						}
						catch (Exception e)
						{
							string strVal = e.ToString();
						}
						Byte[] buf = streamSave.GetBuffer();
						serializer.TraiteByteArray(ref buf);
						streamSave.Close();
						break;

				}
			}
			if (nVersion >= 1)
			{
				I2iSerializable iFormule = m_formuleImageVariable;
				result = serializer.TraiteObject(ref iFormule);
				m_formuleImageVariable = (C2iExpression)iFormule;
			}
            if ( !result )
                return result;
            if (nVersion >= 2)
                result = serializer.TraiteObject<CActionSur2iLink>(ref m_action);

			return result;
		}

        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementClick, "OnClick",
                I.T("Occurs when the user click on the button|20005")));
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
