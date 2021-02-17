using System;
using System.IO;
using System.Drawing;

#if PDA
#else
using System.Drawing.Design;
#endif


using sc2i.drawing;
using sc2i.common;
using sc2i.formulaire;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de C2iChampCustomTextBox.
	/// </summary>
	[WndName("Variable")]
	public abstract class C2iWndVariable : C2iWndComposantFenetre
	{
		public enum TypeAlignement
		{
			Gauche = 0,
			Droite = 1,
			Centre = 2
		}

		//private IVariableDynamique m_variable = null;
		private string m_strMasqueEdition = "";
		private TypeAlignement m_alignement = TypeAlignement.Gauche;
		private bool m_bMultiLine = false;

		public C2iWndVariable()
		{
			//Size = new Size ( Size.Width, 22 );
			Size = new Size ( Size.Width, 22 );
		}

		/// //////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
			//1 : Ajout de multiligne
		}

		/// //////////////////////////////////////////////////
		public override Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				if ( Variable != null && Variable.IsChoixParmis() )
					base.Size = new Size ( value.Width, 22 );
				else
					base.Size = value;
			}
		}

		/// //////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			/*int nIdVariable = -1;
			if ( m_variable != null && serializer.Mode == ModeSerialisation.Ecriture )
				nIdVariable = m_variable.Id;

			serializer.TraiteInt ( ref nIdVariable );
			if ( serializer.Mode == ModeSerialisation.Lecture )
			{
				if ( nIdVariable == -1 )
					m_variable = null;
				else
					m_variable = ((CFiltreDynamique)serializer.GetObjetAttache(typeof(CFiltreDynamique))).GetVariable ( nIdVariable );
			}*/

			serializer.TraiteString ( ref m_strMasqueEdition );
			
			int nAlignement = (int)m_alignement;
			serializer.TraiteInt ( ref nAlignement );
			m_alignement = (TypeAlignement)nAlignement;

			if ( nVersion >= 1 )
				serializer.TraiteBool ( ref m_bMultiLine );
			else
				m_bMultiLine = Size.Height > 22;
			return result;
		}

		/// //////////////////////////////////////////////////
		public abstract IVariableDynamique Variable{get;}

		/// //////////////////////////////////////////////////
#if PDA
#else
		[System.ComponentModel.Description(@"Masque d'édition :\r# pour un nombre
$ pour un caractère quelconque
> pour une lettre majuscule
< pour une lettre minuscule")]
#endif
		public string MasqueEdition
		{
			get
			{
				return m_strMasqueEdition;
			}
			set
			{
				m_strMasqueEdition = value;
			}
		}

		/// //////////////////////////////////////////////////
		public TypeAlignement Alignement
		{
			get
			{
				return m_alignement;
			}
			set
			{
				m_alignement = value;
			}
		}


#if PDA
#else
		/// //////////////////////////////////////////////////
		public override void DrawInterieur(CContextDessinObjetGraphique ctx)
		{
			Graphics g = ctx.Graphic;
			Rectangle rect = new Rectangle ( new Point (0, 0), Size );
			if ( Variable == null )
			{
				DrawTextBox ( rect, g );
				return;
			}
			if ( Variable.IsChoixParmis() )
				DrawCombo(rect, g);
			else
			{
				if ( Variable.TypeDonnee.TypeDotNetNatif == typeof(double) ||
					Variable.TypeDonnee.TypeDotNetNatif == typeof(int) ||
					Variable.TypeDonnee.TypeDotNetNatif == typeof(string) )
				{
					DrawTextBox ( rect, g );
				}
				else if ( Variable.TypeDonnee.TypeDotNetNatif == typeof(DateTime)  )
					DrawCombo ( rect, g );
				else if (Variable.TypeDonnee.TypeDotNetNatif == typeof(bool) )
					DrawCheckBox ( rect, g );
			}
		}

		/// //////////////////////////////////////////////////
		private void DrawTextBox ( Rectangle rect, Graphics g )
		{
			g.FillRectangle(new SolidBrush(BackColor), rect );
			DrawCadre3D ( rect, true, g );
			rect.Width -= 4;
			rect.Height -= 4;
			rect.Offset(2,2);
			DrawNomChamp ( rect, g );
		}

		/// //////////////////////////////////////////////////
		private void DrawCombo ( Rectangle rect, Graphics g )
		{
			g.FillRectangle ( new SolidBrush(BackColor), rect );
			DrawCadre3D ( rect, true, g );

			//Bouton combo
			Rectangle rectBouton = new Rectangle(rect.Right-19, rect.Top+2,	17, 18);
			g.FillRectangle ( SystemBrushes.Control, rectBouton );
			DrawCadre3D ( rectBouton, false, g );

			//Dessin de la flêche
			for ( int n = 0; n < 4; n++ )
				g.DrawLine(Pens.Black, new Point ( rectBouton.Left+5+n, rectBouton.Top+7+n),
					new Point ( rectBouton.Left+11-n, rectBouton.Top+7+n ) );
			g.DrawLine ( Pens.Black, rectBouton.Left+8, rectBouton.Top+9, rectBouton.Left+8, rectBouton.Top+10 );
			rect.Width -= 21;
			rect.Height-=4;
			rect.Offset(2,2);
			DrawNomChamp ( rect, g );
		}

		/// //////////////////////////////////////////////////
		private void DrawCheckBox ( Rectangle rect, Graphics g )
		{
			g.FillRectangle(new SolidBrush(BackColor), rect);
			Rectangle checkRect = new Rectangle ( rect.Left, rect.Top+rect.Height/2-6, 13, 13 );
			g.FillRectangle(SystemBrushes.Window, checkRect);
			DrawCadre3D ( checkRect, true, g );
			rect.Width -= 21;
			rect.Height -= 4;
			rect.Offset(19,2);
			DrawNomChamp ( rect, g );
		}

		/// //////////////////////////////////////////////////
		private void DrawNomChamp ( Rectangle rect, Graphics g )
		{
			if ( Font == null )
				return;
			Region oldClip = g.Clip;
			g.Clip = new Region(rect);
			string strNom = Variable == null?"[UNDEFINED|30002]":"["+Variable.Nom+"]";
			SizeF sz = g.MeasureString(strNom, Font);
			Point pt = new Point ( rect.Left, rect.Top );
			switch ( Alignement )
			{
				case TypeAlignement.Gauche :
					pt.X = rect.Left;
					break;
				case TypeAlignement.Droite :
					pt.X = rect.Right - (int)sz.Width;
					break;
				case TypeAlignement.Centre :
					pt.X = rect.Left + rect.Width/2 - (int)sz.Width/2;
					break;
			}
			g.DrawString ( strNom, Font, new SolidBrush(ForeColor), pt );
		}
#endif

		/// //////////////////////////////////////////////////
		public override bool CouleurFondAutomatique
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// //////////////////////////////////////////////////
		public bool MultiLine
		{
			get
			{
				return m_bMultiLine;
			}
			set
			{
				m_bMultiLine = value;
			}
		}




        

        public override bool CanBeUseOnType(Type tp)
        {
            return true;
        }

       







        public override void OnDesignSelect(Type typeEdite, object objetEdite, sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
        }
			

	}

}
