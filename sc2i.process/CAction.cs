using System;
using System.Collections;
using System.Drawing;

using sc2i.common;
using sc2i.drawing;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.common.recherche;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using sc2i.process.recherche;
using System.Drawing.Drawing2D;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CAction.
	/// </summary>
	public abstract class CAction : 
        C2iObjetGraphiqueSansChilds, 
        IObjetDeProcess,
        I2iCloneableAvecTraitementApresClonage
	{
		private CProcess m_process;
		private string m_strLibelle = "";

		private int m_nId = 0;
		
		/// ////////////////////////////////////////////////////////
		public CAction( CProcess process)
		{
			m_process = process;
			m_nId = m_process.GetIdNouvelObjetDeProcess();
			Position = new Point ( 0, 0);
			Size = DefaultSize;
			Parent = process;
		}

		/// ////////////////////////////////////////////////////////
		///Si vrai, indique qu'une telle action peut être executée
		///directement sur le poste client. Sinon, a besoin d'un contexte
		///séparé pour l'execution du process qui utilise cette action
		public abstract bool PeutEtreExecuteSurLePosteClient { get;}

		/// ////////////////////////////////////////////////////////
		public CProcess Process
		{
			get
			{
				return m_process;
			}
		}

		/// ////////////////////////////////////////////////////////
		public virtual Size DefaultSize
		{
			get
			{
				return new Size ( 150, 40 );
			}
		}

		/// ////////////////////////////////////////////////////////
		public virtual Point[] GetPolygoneDessin()
		{
			Rectangle rect = RectangleAbsolu;
			return new Point[]
				{
					new Point ( rect.Left, rect.Top ),
					new Point ( rect.Right, rect.Top),
					new Point ( rect.Right, rect.Bottom),
					new Point ( rect.Left, rect.Bottom )
				};
		}

		/// ////////////////////////////////////////////////////////
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return m_strLibelle;
			}
			set
			{
				m_strLibelle = value.Trim();
			}
		}

		/// <summary>
		/// Retourne une liste de CLienAction
		/// </summary>
		/// <returns></returns>
		public CLienAction[] GetLiensArrivant()
		{
			return Process.GetLiensForAction ( this, true );
		}

		/// <summary>
		/// Retourne les liens de cette action, hors liens d'erreur
		/// </summary>
		/// <returns></returns>
		public CLienAction[] GetLiensSortantHorsErreur()
		{
			return Process.GetLiensForAction ( this, false, true );
		}

		/// <summary>
		/// Retourne une liste de CLienAction
		/// </summary>
		/// <returns></returns>
		public CLienAction[] GetLiensSortant()
		{
			return Process.GetLiensForAction ( this, false );
		}

		/// ////////////////////////////////////////////////////////
		///La gestion des erreurs est centralisée dans le CAction, les
		///autres classes n'ont pas besoin de connaitre l'existence des liens d'erreur !!
		public CLienAction[] GetLiensSortantsPossibles()
		{
			ArrayList lstLiens = new ArrayList(GetMyLiensSortantsPossibles());
			bool bHasLienErreur = false;
			foreach ( CLienAction lien in lstLiens )
			{
				if ( lien is CLienErreur )
				{
					bHasLienErreur = true;
					break;
				}
			}
			if ( !bHasLienErreur )
			{
				foreach ( CLienAction lien in GetLiensSortant() )
				{
					if ( lien is CLienErreur )
					{
						bHasLienErreur = true;
						break;
					}
				}
				if ( !bHasLienErreur )
					lstLiens.Add ( new CLienErreur(Process) );
			}
			return (CLienAction[])lstLiens.ToArray(typeof(CLienAction));
		}

		
		/// ////////////////////////////////////////////////////////
		/// Retourne les liens sortants possibles. Par défaut, retourne un lien bête
		protected virtual CLienAction[] GetMyLiensSortantsPossibles()
		{
			if ( GetLiensSortantHorsErreur().Length == 0 )
				return new CLienAction[]{new CLienAction(Process)};	
			else
				return new CLienAction[0];
		}

		/// ////////////////////////////////////////////////////////
		public int IdObjetProcess
		{
			get
			{
				return m_nId;
			}
		}


		/// ////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strLibelle );
			serializer.TraiteInt ( ref m_nId );

			return result;
		}

#if !PDA
		/// ////////////////////////////////////////////////////////
        private static Dictionary<Type, Image> m_dicImages = new Dictionary<Type, Image>();
        public virtual Image GetImage()
		{
            Image img = null;
            if ( m_dicImages.TryGetValue ( GetType(), out img ))
                return img;
            try
			{
				//Nom d'image : le nom du type suivi de gif
				string[] strNoms = GetType().ToString().Split('.');
				string strNomImage = strNoms[strNoms.Length-1]+".bmp";
				img = new Bitmap ( GetType(), strNomImage );
                m_dicImages[GetType()] = img;
				return img;
			}
			catch{}
			return null;
		}
#endif

        /// ////////////////////////////////////////////////////////
        public virtual Rectangle RectangleCadre
        {
            get
            {
                Rectangle rct = RectangleAbsolu;
                rct = new Rectangle(rct.Left, rct.Top, rct.Width - 4, rct.Height - 4);
                return rct;
            }
        }

        /// ////////////////////////////////////////////////////////
        protected Brush GetNewBrushForFond()
        {
            Rectangle rctCadre = RectangleCadre;
            return new LinearGradientBrush(new Point(rctCadre.Left, rctCadre.Top),
                new Point(rctCadre.Right, rctCadre.Bottom),
                Color.FromArgb(229, 236, 246), Color.FromArgb(183, 201, 227));
        }

		/// ////////////////////////////////////////////////////////
		protected override void MyDraw ( CContextDessinObjetGraphique ctx )
		{
			Graphics g = ctx.Graphic;
            Rectangle rct = RectangleAbsolu;

            Rectangle rctCadre = RectangleCadre;

            Rectangle rctOmbre = new Rectangle(rct.Left + 4, rct.Bottom - 4, rct.Width - 4, 4);
            Brush brOmbre = new SolidBrush(Color.FromArgb(50, 0, 0, 0));
            g.FillRectangle(brOmbre, rctOmbre);
            rctOmbre = new Rectangle(rct.Right - 4, rct.Top + 4, 4, rct.Height - 8);
            g.FillRectangle(brOmbre, rctOmbre);
            brOmbre.Dispose();

            Brush bWhite = GetNewBrushForFond();
       		g.FillRectangle ( bWhite, rctCadre );

			Pen pBlack = new Pen ( Color.Black );
			g.DrawRectangle ( pBlack, rctCadre );
			bWhite.Dispose();
			pBlack.Dispose();
#if !PDA
			Image img = GetImage();
			if ( img != null )
			{
				g.DrawImage ( img, rctCadre.Left+1,rctCadre.Top+1, img.Width, img.Height );
			}
#endif
			DrawLibelle ( g );
           
		}

		/// ////////////////////////////////////////////////////////
		public virtual void DrawLibelle ( Graphics g )
		{
			if ( Libelle.Length == 0 )
				return;
			Font ft = new Font ( "Arial", 7, FontStyle.Regular );
			SizeF size = g.MeasureString ( Libelle, ft );
            Rectangle rect = RectangleCadre;
			Point pt = new Point (rect.Left+rect.Width/2 - (int)size.Width/2,
				rect.Top+rect.Height/2-(int)size.Height/2);

			Brush bBlack = new SolidBrush(Color.Black );
			g.DrawString ( Libelle, ft, bBlack, pt.X, pt.Y);
			bBlack.Dispose();
		}

		/// ////////////////////////////////////////////////////////
		protected virtual void DrawVariableEntree ( Graphics g, CVariableDynamique variable )
		{
			Font ft = new Font ( "Arial", 7, FontStyle.Regular );
			string strNomVariable = "< "+(variable==null?"":variable.Nom);
			SizeF size = g.MeasureString ( strNomVariable, ft );
            Rectangle rect = RectangleCadre;
			int nHeight = (int)size.Height+2;
			int nWidth = (int)size.Width+2;
			rect.Offset ( rect.Width-nWidth, 0 );
			rect.Height = nHeight;
			rect.Width = nWidth;
			Pen pBlack = new Pen ( Color.Black );
            Brush br = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                new Point(rect.Right, rect.Bottom),
                Color.Green, Color.DarkGreen);
			g.FillRectangle ( br, rect );
			g.DrawRectangle ( pBlack, rect );
            br.Dispose();
			pBlack.Dispose();
			Brush bBlack = new SolidBrush(Color.White);
			g.DrawString ( strNomVariable, ft, bBlack, rect.Left+1, rect.Top+1);
			bBlack.Dispose();
			ft.Dispose();
		}

		/// ////////////////////////////////////////////////////////
		protected virtual void DrawVariableSortie ( Graphics g, CVariableDynamique variable )
		{
            Rectangle rect = RectangleCadre;
            Brush br = new LinearGradientBrush(new Point(rect.Left, rect.Top),
                new Point(rect.Right, rect.Bottom),
                Color.Yellow, Color.DarkOrange);
			DrawSortie ( g, variable == null?"":variable.Nom, br );
            br.Dispose();
		}


		protected virtual void DrawSortie ( Graphics g, string strValeur, Brush brosse )
		{
			Font ft = new Font ( "Arial", 7, FontStyle.Regular );
			SizeF size = g.MeasureString ( strValeur, ft );
            Rectangle rect = RectangleCadre;
			int nHeight = (int)size.Height+2;
			int nWidth = (int)size.Width+2;
			rect.Offset ( rect.Width-nWidth, rect.Height-nHeight );
			rect.Height = nHeight;
			rect.Width = nWidth;
			Pen pBlack = new Pen ( Color.Black );
			g.FillRectangle ( brosse, rect );
			g.DrawRectangle ( pBlack, rect );
			pBlack.Dispose();
			Brush bBlack = new SolidBrush(Color.Black );
			g.DrawString ( strValeur, ft, bBlack, rect.Left+1, rect.Top+1);
			bBlack.Dispose();
			ft.Dispose();

			
		}

		/// ////////////////////////////////////////////////////////
		public virtual CResultAErreur VerifieDonnees()
		{
			return CResultAErreur.True;
		}

		/// ////////////////////////////////////////////////////////
		/// Le data du result doit contenir la liste des liens de suite à lancer 
		/// (CLien[])
		public CResultAErreur Execute ( CContexteExecutionAction contexte )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				 result = ExecuteAction ( contexte );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
			}
			contexte.LastErreur = "";
			if ( !result )
			{
				contexte.LastErreur = result.Erreur.ToString();
				foreach ( CLienAction lien in GetLiensSortant() )
				{
					if ( lien is CLienErreur )
					{
						result = CResultAErreur.True;
						result.Data = lien;
						return result;
					}
				}
				if ( !(this is CActionErreur) )
					result.EmpileErreur(I.T("Error during the execution of action @1|101",Libelle));
			}
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected abstract CResultAErreur ExecuteAction ( CContexteExecutionAction contexte );


		/// ////////////////////////////////////////////////////////
		public CResultAErreur ExecuteLien ( CContexteExecutionAction contexte, CLienAction lien )
		{
			return lien.ActionArrivee.Execute ( contexte );
		}

		/// ////////////////////////////////////////////////////////
		/// aJoute à la hashtable les ids des variables nécéssaires à cette action
		public abstract void AddIdVariablesNecessairesInHashtable ( Hashtable table );

		/// ////////////////////////////////////////////////////////
		public int[] GetIdsVariablesNecessaires()
		{
			Hashtable table = new Hashtable();
			AddIdVariablesNecessairesInHashtable ( table );
			int [] lst = new int[table.Count];
			int nIndex = 0;
			foreach ( int nId in table.Keys )
				lst[nIndex++] = nId;
			return lst;
		}

		/// ////////////////////////////////////////////////////////

		/// <summary>
		/// Ajoute les ids des variables utilisées par une expression à une hashtable
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="table"></param>
		protected void AddIdVariablesExpressionToHashtable ( C2iExpression expression, Hashtable table )
		{
			if ( expression != null )
			{
				foreach ( C2iExpressionChamp exp in expression.ExtractExpressionsType ( typeof(C2iExpressionChamp) ) )
				{
					CDefinitionProprieteDynamique prop = exp.DefinitionPropriete;
					if ( prop is CDefinitionProprieteDynamiqueVariableDynamique )
						table[((CDefinitionProprieteDynamiqueVariableDynamique)prop).IdChamp] = true;
				}
			}
		}

        public override I2iObjetGraphique GetCloneAMettreDansParent(I2iObjetGraphique parent, Dictionary<Type, object> dicObjetsPourCloner)
        {
            return (I2iObjetGraphique)CCloner2iSerializable.Clone(this, dicObjetsPourCloner, new object[]{Process});
            
        }

        public void TraiteApresClonage(I2iSerializable objetSource)
        {
            m_nId = Process.GetIdNouvelObjetDeProcess();
        }





        public virtual bool UtiliseObjet(object objetCherche)
        {
            return CTesteurUtilisationObjet.DoesUse(this, objetCherche);            
        }
    }
}
