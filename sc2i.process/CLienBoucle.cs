using System;
using System.Drawing;

using sc2i.common;

namespace sc2i.process
{
	/// <summary>
	/// Lien s'executant pour une boucle
	/// </summary>
	public class CLienBoucle : CLienAction
	{
		private string m_strLibelle = I.T("For each|275");
		/// ///////////////////////////////////////////////////
		public CLienBoucle( CProcess process)
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
			return new Pen(Color.Blue, 2);
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
