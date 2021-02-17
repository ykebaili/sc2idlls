using System;
using System.Collections;
using System.Drawing;

using sc2i.drawing;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionCondition.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionCondition : CAction
	{
		private C2iExpression m_expressionCondition = null;

		public CActionCondition( CProcess process)
			:base(process)
		{
			Libelle= I.T("Condition|129");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Boolean condition|130"),
				I.T("Allow creation of a condition connection|131"),
				typeof(CActionCondition),
				CGestionnaireActionsDisponibles.c_categorieDeroulement );
		}

		public override Size DefaultSize
		{
			get
			{
				return new Size ( 80, 40 );
			}
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable ( ExpressionCondition, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}


		/// //////////////////////////////////////////////////////
		protected override CLienAction[] GetMyLiensSortantsPossibles()
		{
			CLienAction[] liens = GetLiensSortantHorsErreur();
			bool bHasOui= false, bHasNon = false;
			foreach ( CLienFromCondition lien in liens )
			{
				bHasOui |= lien.IsSiOui;
				bHasNon |= !lien.IsSiOui;
			}
			ArrayList lst = new ArrayList();
			if ( !bHasOui )
			{
				CLienFromCondition lien = new CLienFromCondition ( Process );
				lien.IsSiOui = true;
				lst.Add ( lien );
			}
			if ( !bHasNon )
			{
				CLienFromCondition lien = new CLienFromCondition ( Process );
				lien.IsSiOui = false;
				lst.Add ( lien );
			}
			return (CLienAction[])lst.ToArray(typeof(CLienAction));
		}

		public override Point[] GetPolygoneDessin()
		{
			Rectangle rect = RectangleAbsolu;
			Point[] pts = new Point[4];
			pts[0] = new Point (rect.Left+rect.Width/2, rect.Top);
			pts[1] = new Point ( rect.Right, rect.Top+rect.Height/2);
			pts[2] = new Point ( rect.Left+rect.Width/2, rect.Bottom );
			pts[3] = new Point ( rect.Left, rect.Top+rect.Height/2);
			return pts;
		}


		protected override void MyDraw (CContextDessinObjetGraphique ctx )
		{
			Graphics g = ctx.Graphic;
			Point[] pts = GetPolygoneDessin();
            Brush bWhite = GetNewBrushForFond();
			Pen pBlack = new Pen ( Color.Black );
			g.FillPolygon ( bWhite, pts );
			g.DrawPolygon ( pBlack, pts );
			bWhite.Dispose();
			pBlack.Dispose();
			DrawLibelle ( g );

		}

		/// /////////////////////////////////////////
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
			result = base.MySerialize( serializer );
			if ( !result )
				return result;
			I2iSerializable objet = m_expressionCondition;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
			{
				result.EmpileErreur(I.T("Serialisation error in the condition formula|132"));
				return result;
			}
			m_expressionCondition = (C2iExpression)objet;
			return result;
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression ExpressionCondition
		{
			get
			{
				return m_expressionCondition;
			}
			set
			{
				m_expressionCondition = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( ExpressionCondition == null )
			{
				result.EmpileErreur(I.T("The condition formula isn't correct|133"));
				return result;
			}
			if ( ExpressionCondition.TypeDonnee.TypeDotNetNatif != typeof(bool) )
			{
				result.EmpileErreur(I.T("The condition formula must be return a boolean (YES/NO)|134"));
				return result;
			}
			return result;
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			result = VerifieDonnees();
			if ( !result )
				return result;

            CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process);
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = ExpressionCondition.Eval ( contexteEval );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error during the condition formula evaluation|135"));
				return result;
			}
			if ( !(result.Data is bool ) )
			{
				result.EmpileErreur(I.T("The confition formula doesn't return a boolean|136"));
				return result;
			}
			bool bResult = (bool)result.Data;
			foreach (  CLienFromCondition lien in GetLiensSortantHorsErreur() )
			{
				if ( lien.IsSiOui && bResult )
					result.Data = lien;
				if ( !lien.IsSiOui && !bResult )
					result.Data = lien;
			}
			return result;
		}


	}
}
