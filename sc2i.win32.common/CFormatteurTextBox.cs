using System;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CFormatteurTextBox.
	/// </summary>
	public abstract class CFormatteurTextBox
	{
		//indique qu'un formattage est en cours
		private bool m_bIsFormatting = false;

		////////////////////////////////////////
		public CFormatteurTextBox()
		{
		}

		////////////////////////////////////////
		public void AttachTo ( Control control )
		{
			if ( control is TextBox )
				((TextBox)control).TextChanged += new EventHandler ( OnTextChanged );
			if ( control is ComboBox )
				((ComboBox)control).TextChanged += new EventHandler ( OnTextChanged );
			FormatteTexte ( control );
		}

		////////////////////////////////////////
		//retourne faux si le format est incorrect
		public abstract string GetTextToDisplay ( string strTexte, ref int nPosCurseur );
		//Retourne la valeur du champ ( entier si entier, ... )
		public abstract object GetValue ( string strTexte );//

		////////////////////////////////////////
		public void OnTextChanged ( object sender, EventArgs args )
		{
			FormatteTexte ( (Control)sender );
		}

		////////////////////////////////////////
		public void FormatteTexte ( Control sender )
		{
			if ( m_bIsFormatting )//déjà en cours de formattage
				return;
			if ( !(sender is TextBox) && !(sender is ComboBox ) )
				return;

			m_bIsFormatting = true;
			if ( sender is TextBox )
			{
				int nPos = ((TextBox)sender).SelectionStart;
				string strTxt = GetTextToDisplay ( ((TextBox)sender).Text, ref nPos );
				((TextBox)sender).Text = strTxt;
				((TextBox)sender).SelectionStart = Math.Max(nPos,0);
			}
			if ( sender is ComboBox )
			{
				if ( ((ComboBox)sender).DropDownStyle != ComboBoxStyle.DropDownList )
					return;
				int nPos = ((ComboBox)sender).SelectionStart;
				string strTxt = GetTextToDisplay ( ((ComboBox)sender).Text, ref nPos );
				((ComboBox)sender).Text = strTxt;
				((ComboBox)sender).SelectionStart = nPos;
			}
			m_bIsFormatting = false;
		}

	}
}
