using System;
using System.Drawing;

using sc2i.common;

namespace sc2i.process
{
	/// <summary>
	/// Lien s'executant pour une boucle
	/// </summary>
	public class CLienAsynchrone : CLienAction
	{
		private string m_strLibelle = I.T("Asynchronous action|274");
		/// ///////////////////////////////////////////////////
		public CLienAsynchrone( CProcess process)
			:base(process)
		{
		}

		/// //////////////////////////////////////////////////////////////
		public override string Libelle
		{
			get
			{
				return m_strLibelle;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public override Pen GetNewPenCouleurCadre()
		{
			Pen pen = new Pen(Color.Black, 2);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			return pen;
		}

		
		/// ///////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion( ref nVersion );
			if ( !result) 
				return result;
			result = base.MySerialize ( serializer );
			if ( !result )
				return result;

			return result;
		}

		/// ///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			return CResultAErreur.True;
		}




	}
}
