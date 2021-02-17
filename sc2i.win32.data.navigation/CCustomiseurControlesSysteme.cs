using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common.dynamicControls;
using sc2i.multitiers.client;
using sc2i.data;
using System.IO;
using sc2i.common;
using System.Runtime.InteropServices;

namespace sc2i.win32.data.navigation
{
    public class CCustomiseurControlesSysteme
    {
        [DllImport("user32")]
        public static extern short GetKeyState(int vKey);

        private const string c_racineCleRegistre = "FRMVIS_";
        private static KeyEventHandler m_handlerKey = new KeyEventHandler(racine_KeyDown);
        //------------------------------------------------------
        public static void AppliqueToForm(Form racine)
        {
            if (racine == null)
                return;
            racine.KeyPreview = true;
            racine.KeyDown -= m_handlerKey;

            CSessionClient session = CSessionClient.GetSessionUnique();
            IInfoUtilisateur info = session != null ? session.GetInfoUtilisateur() : null;
            bool bIsAdmin = false;
            if (info != null)
            {
                if (info.GetDonneeDroit(CDroitDeBaseSC2I.c_droitInterface) != null)
                {
                    bIsAdmin = true;
                    racine.KeyDown += m_handlerKey;
                }
            }
            CSetupVisibiliteControles setup = GetSetupForWindow(session, racine);
            if (setup != null && (
                !bIsAdmin || 
                (GetKeyState(0x10) & 0xF000)!=0xF000 || //SHIFT
                (GetKeyState(0x12) & 0xF000)!=0xF000)) //ALT
                setup.Apply(racine, false);
        }

        //------------------------------------------------------
        private static CSetupVisibiliteControles GetSetupForWindow ( CSessionClient session, Form frm )
        {
            CSetupVisibiliteControles setup = new CSetupVisibiliteControles();
            string strKey = c_racineCleRegistre + frm.GetType().ToString();
            //cherche le setup dans le registre
            CDataBaseRegistrePourClient reg = new CDataBaseRegistrePourClient(session.IdSession );
            byte[] bts = reg.GetValeurBlob ( strKey );
            if ( bts != null )
            {
                MemoryStream stream = new MemoryStream(bts);
                BinaryReader reader = new BinaryReader(stream);
                CSerializerReadBinaire ser = new CSerializerReadBinaire( reader );
                CResultAErreur result = setup.Serialize ( ser );
                if ( !result )
                    setup = new CSetupVisibiliteControles();
                reader.Close();
                stream.Close();
                stream.Dispose();
            }
            return setup;
        }

        //------------------------------------------------------
        private static void SetSetupForWindow ( CSessionClient session, Form form, CSetupVisibiliteControles setup )
        {
            string strKey = c_racineCleRegistre + form.GetType().ToString();
            CDataBaseRegistrePourClient reg = new CDataBaseRegistrePourClient(session.IdSession);
            if ( setup == null )
            {
                reg.SetValeurBlob ( strKey, new byte[0] );
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream);
                CSerializerSaveBinaire ser = new CSerializerSaveBinaire(writer );
                CResultAErreur result = setup.Serialize ( ser );
                stream.Flush();
                reg.SetValeurBlob ( strKey, stream.GetBuffer() );
                writer.Close();
                stream.Close();
                stream.Dispose();
            }
                
        }

        //------------------------------------------------------
        static void racine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Shift && e.KeyCode == Keys.F7)
            {
                CSessionClient session = CSessionClient.GetSessionUnique();
                IInfoUtilisateur info = session != null ? session.GetInfoUtilisateur() : null;
                if (info != null)
                {
                    if (info.GetDonneeDroit(CDroitDeBaseSC2I.c_droitInterface) != null)
                    {
                        CSetupVisibiliteControles setup = new CSetupVisibiliteControles();
                        Control ctrl = sender as Control;
                        if (ctrl != null)
                        {
                            Form frm = ctrl.FindForm();
                            if (frm != null)
                            {
                                setup = GetSetupForWindow(session, frm);
                                CFormSetupFenetreDynamique.ShowArbre(frm, setup);
                                SetSetupForWindow(session, frm, setup);
                                setup.Apply(frm, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
