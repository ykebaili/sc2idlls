using System;
using System.Drawing;
using System.Collections;

using sc2i.drawing;
using sc2i.expression;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.process
{
	/// <summary>
	/// //Toute action qui retourne une valeur
	/// </summary>
	public abstract class CActionFonction : CActionLienSortantSimple
	{
        /* TESTDBKEYOK (XL)*/

        private string m_strIdVariableForResult = "";

		private bool m_bVariableCanBeNull = false;

		/// ///////////////////////////////////////////////
		public CActionFonction( CProcess process )
			:base ( process )
		{
		}

		/// ///////////////////////////////////////////////
		public abstract CTypeResultatExpression TypeResultat{get;}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			table[m_strIdVariableForResult] = true;
		}

		/// ///////////////////////////////////////////////
		public string IdVariableResultat
		{
			get
			{
				return m_strIdVariableForResult;
			}
			set
			{
				m_strIdVariableForResult = value;
			}
		}

		/// ///////////////////////////////////////////////
		public bool VariableRetourCanBeNull
		{
			get
			{
				return m_bVariableCanBeNull;
			}
			set
			{
				m_bVariableCanBeNull = value;
			}
		}

		/// ///////////////////////////////////////////////
		public CVariableDynamique VariableResultat
		{
			get
			{
                return Process.GetVariable(m_strIdVariableForResult);
			}
			set
			{
                if (value == null)
                    m_strIdVariableForResult = "";
                else
                    m_strIdVariableForResult = value.IdVariable;
			}
		}

		/// ///////////////////////////////////////////////
		private int GetNumVersion()
		{
			//return 0;
            return 1; // Passage de int IdVariableRetour en String
		}

		/// ///////////////////////////////////////////////
		
		/*Appelle la fonction de sérialisation de la classe de base.
		 * si une classe héritait de CActionLienSortantSimple et qu'elle hérite 
		 * maintenant de CActionFonction, ça permet de résoudre les problèmes
		 * de serialisation des anciennes version. 
		 * Cette fonction est notamment utilisée par CActionLancerProcess
		 * */
		protected CResultAErreur BaseSerialize(C2iSerializer serializer)
		{
			return base.MySerialize(serializer);
		}

		/// ///////////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize ( serializer );
			if ( !result )
				return result;

            if (nVersion < 1 && serializer.Mode == ModeSerialisation.Lecture)
            {
                int nIdTemp = -1;
                serializer.TraiteInt(ref nIdTemp);
                m_strIdVariableForResult = nIdTemp.ToString();
            }
            else
                serializer.TraiteString(ref m_strIdVariableForResult);
			
			return result;
		}

		/// ///////////////////////////////////////////////
		/// pour compatiblité des actions
		/// héritant de CActionFonction mais qui n'en héritaient pas avant
		protected CResultAErreur MySerializeClasseParente ( C2iSerializer serializer )
		{
			return base.MySerialize ( serializer );
		}

		/// ///////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			CVariableDynamique variable = Process.GetVariable ( m_strIdVariableForResult );
			if ( ! m_bVariableCanBeNull && m_strIdVariableForResult != "0" )
			{
				if ( variable == null )
				{
                    result.EmpileErreur(I.T("The n°@1 variable of @2 action doesn't exist|173", m_strIdVariableForResult, Libelle));
					return result;
				}
				if ( !TypeResultat.Equals(variable.TypeDonnee) )
				{
					result.EmpileErreur(I.T("The @1 action return its result in the @2 variable which is a bad type|174",variable.Nom,Libelle));
					return result;
				}
			}
			return result;
		}

		/// ///////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw(ctx);
			Graphics g = ctx.Graphic;
			if ( VariableResultat != null )
			{
				DrawVariableSortie ( g, VariableResultat );
			}
			/*
			

			Font ft = new Font ( "Arial", 7, FontStyle.Regular );
			string strNomVariable = "> "+(VariableResultat==null?"":VariableResultat.Nom);
			SizeF size = g.MeasureString ( strNomVariable, ft );
			Rectangle rect = RectangleAbsolu;
			int nHeight = (int)size.Height+2;
			int nWidth = (int)size.Width+2;
			rect.Offset ( rect.Width-nWidth, rect.Height-nHeight );
			rect.Height = nHeight;
			rect.Width = nWidth;
			Pen pBlack = new Pen ( Color.Black );
			g.FillRectangle ( Brushes.Yellow, rect );
			g.DrawRectangle ( pBlack, rect );
			pBlack.Dispose();
			Brush bBlack = new SolidBrush(Color.Black );
			g.DrawString ( strNomVariable, ft, bBlack, rect.Left+1, rect.Top+1);
			bBlack.Dispose();
			ft.Dispose();*/
		}

	}
}
