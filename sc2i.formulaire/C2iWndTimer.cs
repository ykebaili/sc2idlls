using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;

namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iWndTimer.
	/// </summary>
	[WndName("Timer")]
	[Serializable]
	public class C2iWndTimer : C2iWndComposantFenetre
	{
		public const string c_strIdEventOnTick = "TIMER_TICK";

		private int m_nPeriod = 0;

		public C2iWndTimer()
		{
			base.Size = new Size ( 17,15);
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		/// ///////////////////////////////////////
		public override Size  Size
		{
		  get 
			{ 
				 return base.Size;
			}
			  set 
			{ 
			}
		}

		
		

		/// ///////////////////////////////////////
		public int Period
		{
			get
			{
				return m_nPeriod;
			}
			set
			{
				m_nPeriod = value;
			}
		}

		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Image img = GetImage(GetType());
			Rectangle rct = new Rectangle ( Position, Size );
			if (img != null)
			{
				ctx.Graphic.DrawImage(img, Position);
				
                //Stef 20/08/2013 : ne pas disposer car 
                //L'image peut être exploitée ailleurs
                //img.Dispose();
			}
			else
			{
				Brush br = new SolidBrush(BackColor);
				ctx.Graphic.FillRectangle(br, rct);
				br.Dispose();
			}
			base.MyDraw(ctx);

		}

		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteInt ( ref m_nPeriod );
			return result;
		}

		/// ///////////////////////////////////////
		public override CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());

			lst.Add ( new CDefinitionProprieteDynamiqueDeportee (
				"Interval", "Interval",
				new CTypeResultatExpression ( typeof(int), false ),
				false,
				false,
				""));

            lst.Add(new CDefinitionMethodeDynamique(
                "Stop", "Stop", new CTypeResultatExpression(typeof(void), false), false));
            lst.Add(new CDefinitionMethodeDynamique(
                "Start", "Start", new CTypeResultatExpression(typeof(void), false), false));
            
			return lst.ToArray();
		}

		/// ///////////////////////////////////////
		public override CDescriptionEvenementParFormule[]  GetDescriptionsEvenements()
		{
			List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
			lst.Add ( new CDescriptionEvenementParFormule ( 
				c_strIdEventOnTick,
				"OnTick", 
				I.T("Occurs when the time period of timer is ellapser|20005")));
			return lst.ToArray();
		}


      

       

	}
}
