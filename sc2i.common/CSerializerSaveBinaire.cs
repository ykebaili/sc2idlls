using System;
using System.IO;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CContexteSauvegarde2iBinaire.
	/// </summary>
	public class CSerializerSaveBinaire : C2iSerializer
	{
		private BinaryWriter m_writer;

		/// //////////////////////////////////////
		public CSerializerSaveBinaire(BinaryWriter writer )
		{
			m_writer = writer;
		}

		/// //////////////////////////////////////
		public override ModeSerialisation Mode
		{
			get
			{
				return ModeSerialisation.Ecriture;
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteString ( ref string strChaine )
		{
            if (strChaine == null)
                m_writer.Write("");
            else
			    m_writer.Write( strChaine );
		}

		/// //////////////////////////////////////////////
		public override void TraiteInt ( ref int nVal )
		{
			m_writer.Write ( (Int32)nVal );
		}


		/// //////////////////////////////////////////////
		public override void TraiteDouble ( ref double fVal )
		{
			m_writer.Write ( fVal );
		}

		/// //////////////////////////////////////////////
		public override void TraiteBool ( ref bool bVal )
		{
			m_writer.Write ( bVal );
		}

		/// //////////////////////////////////////////////
		public override void TraiteByte ( ref Byte btVal )
		{
			m_writer.Write ( btVal );
		}

		/// //////////////////////////////////////////////
		public override void TraiteByteArray ( ref Byte[] bts )
		{
			if ( bts == null )
				m_writer.Write((Int32)0);
			else
			{
				m_writer.Write((Int32)bts.Length);
				m_writer.Write ( bts, 0, bts.Length );
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteLong(ref long nVal)
		{
			 m_writer.Write ( nVal );
		}

		/// //////////////////////////////////////////////
		public override void TraiteFloat(ref float fVal)
		{
			m_writer.Write ( fVal );
		}

		/// //////////////////////////////////////////////
		public override void TraiteDecimal(ref decimal dVal)
		{
			m_writer.Write(dVal);
		}


		

	}
}
