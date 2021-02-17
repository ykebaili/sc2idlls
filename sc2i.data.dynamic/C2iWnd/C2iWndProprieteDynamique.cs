using System;
using System.IO;
using System.Drawing;
#if PDA
#else
using System.Drawing.Design;
#endif


using sc2i.common;
using sc2i.drawing;
using sc2i.formulaire;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Type obsolète, pour compatibilité
	/// </summary>
	[Serializable]
	public class C2iWndProprieteDynamique : C2iWndLabel
	{
		//private IVariableDynamique m_variable = null;
		private string m_strFormatAffichage = "";
		private CDefinitionProprieteDynamique m_propriete = null;

		public C2iWndProprieteDynamique()
		{
			//Size = new Size ( Size.Width, 22 );
		}

		/// //////////////////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return false;//Plus utilisé
		}

		/// //////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
		}

		/// //////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			if ( result && nVersion >= 1 )
				result = base.MySerialize(serializer);

			serializer.TraiteString ( ref m_strFormatAffichage);
			
			I2iSerializable objet = m_propriete;
			result = serializer.TraiteObject ( ref objet );
			if (!result )
				return result;
			m_propriete = (CDefinitionProprieteDynamique)objet;
			return result;
		}

		/// //////////////////////////////////////////////////
		public CDefinitionProprieteDynamique Propriete
		{
			get
			{
				return m_propriete;
			}
			set
			{
				m_propriete = value;
			}
		}

		/// //////////////////////////////////////////////////
#if PDA
#else
		[System.ComponentModel.Description(@"Format")]
#endif
		public string FormatAffichage
		{
			get
			{
				return m_strFormatAffichage;
			}
			set
			{
				m_strFormatAffichage = value;
			}
		}

#if PDA
#else
		/// //////////////////////////////////////////////////
		public override void DrawInterieur(CContextDessinObjetGraphique ctx)
		{
			if ( m_propriete != null )
				Text = m_propriete.Nom;
			else
				Text = I.T("No property|132");
			base.DrawInterieur ( ctx );
		}
#endif
	}
}
