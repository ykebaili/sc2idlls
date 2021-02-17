using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using sc2i.data;
using sc2i.data.Excel;
using sc2i.common;
using sc2i.win32.data;
using sc2i.win32.common;
using System.Collections;
using sc2i.common.memorydb;

namespace sc2i.test.metier
{
	public partial class CFormMain : Form
	{

        enum ETypeFichier
        {
            Excel,
            CSV
        }


		public CFormMain()
		{
			InitializeComponent();
		}

   
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            c2iTextBoxNumerique1.LockEdition = checkBox1.Checked;
        }

        private void m_txtSeparateur_TextChanged(object sender, EventArgs e)
        {
            c2iTextBoxNumerique1.SeparateurMilliers = m_txtSeparateur.Text;
            c2iTextBoxNumerique1.Refresh();
        }

        private void CFormMain_Load(object sender, EventArgs e)
        {
            using (CMemoryDb contexteMemoire = new CMemoryDb())
            {

                CResultAErreur result = CSerializerObjetInFile.ReadFromFile(
                    contexteMemoire, "TIMOS_INVENTORY_DATA", "c:\\partage\\timos\\TimosData.dat");

                if (!result)
                    return;

                /*m_txtSelectTypeEquipement.InitAvecFiltreDeBase(
                    contexteMemoire, 
                    typeof(CTypeEquipement),
                    "Libelle", 
                    null,
                    new CFiltreMemoryDb(CTypeEquipement.c_champLibelle + " LIKE @1"),
                    true);

                CTypeEquipement typeEquip = new CTypeEquipement(contexteMemoire);
                if (typeEquip.ReadIfExist("10225"))
                {
                    string strLabel = typeEquip.Libelle;
                    //m_txtSelectTypeEquipement.SelectedObject = typeEquip;
                }*/


            }

        }
	}
}