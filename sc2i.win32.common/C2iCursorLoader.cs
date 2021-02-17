using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace sc2i.win32.common
{

    public class C2iCursorLoader : IDisposable
    {
        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string lpFileName);

        private Dictionary<string, Cursor> m_dicCurseurs = new Dictionary<string, Cursor>();

        private C2iCursorLoader()
        {
        }

        private static C2iCursorLoader GetInstance()
        {
            if (m_instance != null)
                return m_instance;
            m_instance = new C2iCursorLoader();
            return m_instance;
        }

        private static C2iCursorLoader m_instance = null;

        public void Dispose()
        {
            foreach (Cursor cur in m_dicCurseurs.Values)
                cur.Dispose();
            m_dicCurseurs.Clear();
        }

        public static Cursor LoadCursor(Type tp, string strNomCurseur, byte[] data)
        {
            string strKey = tp.ToString() + "_" + strNomCurseur;
            Cursor curseur = null;
            if (GetInstance().m_dicCurseurs.TryGetValue(strKey, out curseur))
                return curseur;
            curseur = LoadColorCursor(data);
            GetInstance().m_dicCurseurs[strKey] = curseur;
            return curseur;
        }

        private static Cursor LoadColorCursor(byte[] data)
        {
            string path = Path.GetTempFileName();
            File.WriteAllBytes(path, data);
            Cursor curTmp = new Cursor(LoadCursorFromFile(path));
            File.Delete(path);
            return curTmp;
        }
    }
}