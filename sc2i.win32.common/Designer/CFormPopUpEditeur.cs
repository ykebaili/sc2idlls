using System;
using System.Drawing;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	public partial class CFormPopUpEditeur : Form
	{
		public CFormPopUpEditeur()
		{
			InitializeComponent();
			Location = new Point(Cursor.Position.X - Width, Cursor.Position.Y - Height);
		}

		private void m_btnOk_Click(object sender, EventArgs e)
		{
			Close();
		}

		//public static void Afficher(CFormPopUpEditeur form)
		//{
		//    form.ShowDialog();
		//}
	}

}
