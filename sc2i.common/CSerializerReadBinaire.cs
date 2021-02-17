using System;
using System.IO;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CSerializerReadBinaire.
	/// </summary>
	public class CSerializerReadBinaire : C2iSerializer
	{
		private BinaryReader m_reader;

		/// //////////////////////////////////////
		public CSerializerReadBinaire(BinaryReader reader )
		{
			m_reader = reader;
		}

		public override ModeSerialisation Mode
		{
			get
			{
				return ModeSerialisation.Lecture;
			}
		}

		/// //////////////////////////////////////////////
		public override void TraiteString ( ref string strChaine )
		{
			strChaine = m_reader.ReadString();
		}

		/// //////////////////////////////////////////////
		public override void TraiteInt ( ref int nVal )
		{
			nVal = m_reader.ReadInt32();
		}


		/// //////////////////////////////////////////////
		public override void TraiteDouble( ref double fVal)
		{
			fVal = m_reader.ReadDouble();
		}

		

		/// //////////////////////////////////////////////
		public override void TraiteBool( ref bool bVal )
		{
			bVal = m_reader.ReadBoolean();
		}

		/// //////////////////////////////////////////////
		public override void TraiteByte( ref Byte btVal)
		{
			btVal = m_reader.ReadByte();
		}

		/// //////////////////////////////////////////////
		public override void TraiteByteArray( ref Byte[] bts)
		{
			int nWidth = m_reader.ReadInt32();
			bts = new Byte[nWidth];
			m_reader.Read ( bts, 0, nWidth );
		}
		
		/// //////////////////////////////////////////////
		public override void TraiteLong ( ref long nVal )
		{
			nVal = m_reader.ReadInt64 ( );
		}

		/// //////////////////////////////////////////////
		public override void TraiteFloat ( ref float fVal )
		{
			fVal = m_reader.ReadSingle ( );
		}

		/// //////////////////////////////////////////////
		public override void TraiteDecimal(ref decimal dVal)
		{
			dVal = m_reader.ReadDecimal();
		}

	}
}
