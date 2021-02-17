using System;
using System.Drawing;
using System.Linq;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Design;
using System.Collections.Generic;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using System.ComponentModel;
using sc2i.expression.FonctionsDynamiques;

namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iFenetre.
	/// </summary>
	[Serializable]
	[AWndIcone("ico_panel")]
    [WndName("Form")]
	public class C2iWndFenetre : C2iWnd, IElementAFonctionsDynamiques, I2iWndAParametres
	{
		private string m_strTexte = "";


		//Uniquement utilisée pour le dessin, non serialisée
		private Image m_imageFond = null;

		//0 si pas de rafraichissement
		//Sinon, la fenêtre doit être rafraichie toutes les x minutes
		private double m_fMinutesRefresh = 0;

        private bool m_bAutoSize = false;

        private List<CFonctionDynamique> m_listeFonctions = new List<CFonctionDynamique>();
        private List<CFormuleNommee> m_listeParametres = new List<CFormuleNommee>();

        private Dictionary<string, object> m_dicValeursParametresForcés = new Dictionary<string, object>();

		/// ///////////////////////////////////////
		public C2iWndFenetre()
		{
			Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            LockMode = ELockMode.Independant;
            Size = new Size(200, 100);
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		/// ///////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public override bool IsLock
		{
			get
			{
				return true;
			}
			set
			{
				base.IsLock = true;
			}
		}

		/// ///////////////////////////////////////
		public string Text
		{
			get
			{
				return m_strTexte;
			}
			set
			{
				m_strTexte = value;
			}
		}

        /// ///////////////////////////////////////
        [System.ComponentModel.Editor(typeof(CFonctionDynamiqueEditor), typeof(UITypeEditor))]
        public CFonctionDynamique[] Functions
        {
            get
            {
                if (m_listeFonctions == null)
                    m_listeFonctions = new List<CFonctionDynamique>();
                return m_listeFonctions.ToArray();
            }
            set
            {
                m_listeFonctions.Clear();
                if (value != null)
                    m_listeFonctions.AddRange(value);
            }
        }

        /// ///////////////////////////////////////
        public IEnumerable<string> GetNomsParametres()
        {
            List<string> lst = new List<string>();
            foreach (CFormuleNommee f in Parameters)
                lst.Add(f.Libelle);
            return lst.ToArray();
        }

        /// ///////////////////////////////////////
        [DynamicMethod("Set a parameter value for this form","Parameter name","parameter value")]
        public void SetParameterValue(string strParametre, object valeur)
        {
            m_dicValeursParametresForcés[strParametre.ToUpper()] = valeur;
        }

        /// ///////////////////////////////////////
        [DynamicMethod("Clear all parameters value and set them to default")]
        public void ResetParametersValues()
        {
            m_dicValeursParametresForcés.Clear();
        }

        /// ///////////////////////////////////////
        public object GetValeurForceeParametre(string strParametre)
        {
            object val = null;
            m_dicValeursParametresForcés.TryGetValue(strParametre.ToUpper(), out val);
            return val;
        }

        /// ///////////////////////////////////////
        [TypeConverter(typeof(CTableauFormuleNommeeConvertor))]
        [System.ComponentModel.Editor(typeof(CListeFonctionsEditor), typeof(UITypeEditor))]
        public CFormuleNommee[] Parameters
        {
            get
            {
                if (m_listeParametres == null)
                    m_listeParametres = new List<CFormuleNommee>();
                return m_listeParametres.ToArray();
            }
            set
            {
                m_listeParametres.Clear();
                if (value != null)
                    m_listeParametres.AddRange(value);
            }
        }

        /// ///////////////////////////////////////
        public bool AutoSize
        {
            get
            {
                return m_bAutoSize;
            }
            set
            {
                m_bAutoSize = value;
            }
        }

		/// ///////////////////////////////////////
		/// Début pour implémentation du délai de rafraichissement.
		/// à implémenter : le rafraichissement proprement dit dans le Createur2iWnd
        [Browsable(false)]
		public double RefreshMinutes
		{
			get
			{
				return m_fMinutesRefresh;
			}
			set
			{
				m_fMinutesRefresh = value;
			}
		}




		/// ///////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		protected override Size ClientSize
		{
			get
			{
				Size sz = Size;
				sz.Width -= 2;
				sz.Height -= 2;
				return sz;
			}
		}

		/// ///////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		protected override Point OrigineCliente
		{
			get
			{
				return new Point(1, 1);
			}
		}


		/// ///////////////////////////////////////
		public override Point Magnetise(Point pt)
		{
			return pt;
			//Point newPt = pt;
			//if ( m_gridSize.Width > 1 )
			//    newPt.X = (int)(Math.Round((double)(newPt.X/m_gridSize.Width))*m_gridSize.Width);
			//if ( m_gridSize.Height > 1 )
			//    newPt.Y = (int)(Math.Round((double)(newPt.Y/m_gridSize.Height))*m_gridSize.Height);
			//return newPt;
		}

		/// ///////////////////////////////////////
		[System.ComponentModel.Browsable(false)]
		public override bool AcceptChilds
		{
			get
			{
				return true;
			}
		}

		/// ///////////////////////////////////////

		/// <summary>
		/// Image dessinée au fond du formulaire, 
		/// non serialisée
		/// </summary>
        [Browsable(false)]
		public Image ImageFond
		{
			get
			{
				return m_imageFond;
			}
			set
			{
				m_imageFond = value;
			}
		}

		//private Point GetPointDansContexte(Point pt)
		//{ 

		//}

		/// ///////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			//Dessin de la grille
			Graphics g = ctx.Graphic;
			DrawRectangle(new Rectangle(Position, Size), g);

			base.MyDraw(ctx);
		}

		private Bitmap m_bmp;
		private bool m_bCache = false;
        [Browsable(false)]
		public bool Cache
		{
			get
			{
				return m_bCache;
			}
			set
			{
				m_bCache = value;
			}
		}

		/// ///////////////////////////////////////
		public override void DrawInterieur(CContextDessinObjetGraphique ctx)
		{
			Graphics g = ctx.Graphic;
			Point pt = Point.Empty;

			if (!Cache || m_bmp == null)
			{
				//if (!ctx.WorkWithLimits)
				{
					m_bmp = new Bitmap(ClientRect.Width, ClientRect.Height);
					Graphics gbmp = Graphics.FromImage(m_bmp);
					if (m_imageFond != null)
						gbmp.DrawImage(m_imageFond, 0, 0);
					gbmp.Dispose();
				}
				/*else
				{

					Rectangle rctSource = RectangleAbsolu;
					rctSource.Intersect(ctx.LimitesDessin);
					rctSource = GlobalToClient(rctSource);
					m_bmp = new Bitmap(rctSource.Width, rctSource.Height);
					if (m_imageFond != null)
					{
						Graphics gbmp = Graphics.FromImage(m_bmp);
						gbmp.DrawImage(m_imageFond, new Rectangle(Point.Empty, m_bmp.Size), rctSource, GraphicsUnit.Pixel);
						gbmp.Dispose();
					}
					pt = rctSource.Location;
				}*/
			}

			g.DrawImageUnscaled(m_bmp, pt);
        }
        
        /// //////////////////////////////////////////////
        [DynamicField("ChildControls")]
        [Browsable(false)]
        public C2iWnd[] ChildControls
        {
            get
            {
                List<C2iWnd> lst = new List<C2iWnd>();
                foreach (C2iWnd wnd in Childs)
                    lst.Add(wnd);
                return lst.ToArray();
            }
        }

        [DynamicField("Height")]
        public int Height
        {
            get
            {
                return Size.Height;
            }
        }

        [DynamicField("Width")]
        public int Width
        {
            get
            {
                return Size.Width;
            }
        }

		/// ///////////////////////////////////////
        private int GetNumVersion()
        {
            return 7;
            //Ajout du délai de rafraichissement
            //4 : ajout des fonctions
            //5 : ajout de autosize
            //6 : Ajout des paramètres
            //7 : transformation des fonctions en fonctions dynamiques
        }

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			serializer.TraiteString(ref m_strTexte);

			if (nVersion < 2)
			{
				int nTmp = 0;
				serializer.TraiteInt(ref nTmp);

				nTmp = 0;
				serializer.TraiteInt(ref nTmp);
			}


			//MODIF FAB
			if (nVersion > 0)
			{
				bool bHasImage = m_imageFond != null;
				serializer.TraiteBool(ref bHasImage);
				if (bHasImage)
				{
					switch (serializer.Mode)
					{
						case ModeSerialisation.Lecture:
							Byte[] bt = null;
							serializer.TraiteByteArray(ref bt);
							if (m_imageFond != null)
								m_imageFond.Dispose();
							m_imageFond = null;
							MemoryStream stream = new MemoryStream(bt);
							try
							{
								Bitmap bmp = (Bitmap)Bitmap.FromStream(stream);
								m_imageFond = bmp;
							}
							catch
							{
								m_imageFond = null;
							}
							stream.Close();
							break;

						case ModeSerialisation.Ecriture:
							MemoryStream streamSave = new MemoryStream();
							try
							{
								m_imageFond.Save(streamSave, System.Drawing.Imaging.ImageFormat.Png);
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
			}
			if (nVersion >= 3)
				serializer.TraiteDouble(ref m_fMinutesRefresh);
			else
				m_fMinutesRefresh = 0;
            if (nVersion >= 4 && nVersion < 7 && serializer.Mode == ModeSerialisation.Lecture)
            {
                List<CFormuleNommee> lst = new List<CFormuleNommee>();
                result = serializer.TraiteListe<CFormuleNommee>(lst);
                m_listeFonctions.Clear();
                foreach ( CFormuleNommee formule in lst )
                {
                    CFonctionDynamique fonction = new CFonctionDynamique(formule.Libelle);
                    fonction.Formule = formule.Formule;
                    fonction.Nom = formule.Libelle;
                    m_listeFonctions.Add(fonction);
                }
                
            }
            if (!result)
                return result;
            if (nVersion >= 5)
                serializer.TraiteBool(ref m_bAutoSize);
            if (nVersion >= 6)
                result = serializer.TraiteListe<CFormuleNommee>(m_listeParametres);
            if (result && nVersion >= 7)
            {
                result = serializer.TraiteListe<CFonctionDynamique>(m_listeFonctions);
            }
            if (!result)
                return result;

			return result;
		}

		/*/// ///////////////////////////////////////
		protected override CResultAErreur MyRead ( CContexteLecture2i ctx )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = VerifieVersion ( ctx, ref nVersion);
			if ( !result )
				return result;
			m_strTexte = ctx.ReadString();
			m_gridSize = new Size(ctx.ReadInt(),ctx.ReadInt());
			return result;
		}*/

        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {

            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
            

            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "Text", "Text",
                new CTypeResultatExpression(typeof(string), false),
                false,
                false,
                ""));

            /*foreach (CFonctionDynamique fonction in Functions)
            {
                if (fonction.Formule != null)
                {
                    CDefinitionMethodeDynamique defMeth = new CDefinitionMethodeDynamique(
                        fonction.IdFonction,
                        fonction.Nom,
                        fonction.Formule.TypeDonnee,
                        CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(fonction.Formule.TypeDonnee.TypeDotNetNatif)
                    );
                    lst.Add(defMeth);
                }
            }*/


            lst.Add ( new CDefinitionMethodeDynamique(
                "YesNoBox",
                "YesNoBox",
                new CTypeResultatExpression(typeof(bool), false),
                false ));
            lst.Add(new CDefinitionMethodeDynamique(
                "OkCancelBox",
                "OkCancelBox",
                new CTypeResultatExpression(typeof(bool), false),
                false));
                        

			
            return lst.ToArray();
        }

        //////////////////////////////////////////////
        /// <summary>
        /// Juste pour que dynamic method fonctionne bien
        /// </summary>
        /// <returns></returns>
        [DynamicMethod("Display a simple message box",
            "Message to display")]
        public bool MessageBox(string strMessage)
        {
            return true;
        }

        //////////////////////////////////////////////
        /// <summary>
        /// Juste pour que dynamic method fonctionne bien
        /// </summary>
        /// <returns></returns>
        [DynamicMethod("Display a simple message box, with Yes/No buttons. returns true on 'yes' click",
            "Message to display")]
        public bool YesNoBox(string strMessage)
        {
            return true;
        }

        //////////////////////////////////////////////
        /// <summary>
        /// Juste pour que dynamic method fonctionne bien
        /// </summary>
        /// <returns></returns>
        [DynamicMethod("Display a simple message box, with Ok/Cancel buttons. returns true on 'ok' click",
            "Message to display")]
        public bool OkCancelBox(string strMessage)
        {
            return true;
        }



        public override void OnDesignSelect(Type typeEdite, object objetEdite, IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            CListeFonctionsEditor.ObjetPourSousProprietes = new CObjetPourSousProprietes(this);
            CFonctionDynamiqueEditor.ObjetPourSousProprietes = new CObjetPourSousProprietes(this);
        }




        //--------------------------------------------------------------------
        public IEnumerable<CFonctionDynamique> FonctionsDynamiques
        {
            get { return m_listeFonctions.AsReadOnly(); }
        }

        //--------------------------------------------------------------------
        public CFonctionDynamique GetFonctionDynamique(string strIdFonction)
        {
            return m_listeFonctions.FirstOrDefault(f => f.IdFonction == strIdFonction);
        }

    }


    


	[global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class AWndIcone : Attribute
	{
		public AWndIcone(string iconeName)
		{
			m_strNomIco = iconeName;
		}
		private string m_strNomIco;
		/*public Bitmap Icone
		{
			get
			{
				try
				{
					return (Bitmap)Properties.Resources.ResourceManager.GetObject(m_strNomIco);
				}
				catch
				{
				}
				return null;
			}
		}*/

        


        
	}

    public interface IEditeurListeFonctions
	{
		CFormuleNommee[] EditeFonctions ( CFormuleNommee[] formulesNommees );
		CObjetPourSousProprietes ObjetPourSousProprietes { get;set;}
	}

	/// <summary>
	/// Description résumée de CProprieteChampCustomEditor.
	/// </summary>
    public class CListeFonctionsEditor : UITypeEditor
    {
        private static Type m_typeEditeur = null;
        private static CObjetPourSousProprietes m_objetPourSousProprietes = null;

        /// ///////////////////////////////////////////
        public CListeFonctionsEditor()
        {
        }


        /// ///////////////////////////////////////////
        public static void SetTypeEditeur(Type typeEditeur)
        {
            m_typeEditeur = typeEditeur;
        }

        /// ///////////////////////////////////////////
        public static CObjetPourSousProprietes ObjetPourSousProprietes
        {
            get
            {
                return m_objetPourSousProprietes;
            }
            set
            {
                m_objetPourSousProprietes = value;
            }
        }



        /// ///////////////////////////////////////////
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
            System.IServiceProvider provider,
            object value)
        {
            if (m_typeEditeur == null)
                return null;
            IEditeurListeFonctions editeur = null;
            editeur = (IEditeurListeFonctions)Activator.CreateInstance(m_typeEditeur);
            editeur.ObjetPourSousProprietes = m_objetPourSousProprietes;
            object retour = editeur.EditeFonctions((CFormuleNommee[])value);
            if (editeur is IDisposable)
                ((IDisposable)editeur).Dispose();
            return retour;
        }

        /// ///////////////////////////////////////////
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (m_typeEditeur == null)
                return UITypeEditorEditStyle.None;
            return UITypeEditorEditStyle.Modal;
        }

        

       
    }
}
