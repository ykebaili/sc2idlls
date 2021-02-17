using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;

namespace sc2i.win32.data.dynamic
{
    public class CParametreEditModulesParametrage : I2iSerializable
    {
        private Orientation m_orientation = Orientation.Vertical;
        private int m_nSplitterDistance = 212;
        private string[] m_strSelectionPath = new string[0];

        public CParametreEditModulesParametrage()
        {
        }

        public Orientation Orientation
        {
            get
            {
                return m_orientation;
            }
            set
            {
                m_orientation = value;
            }
        }

        public int SplitterDistance
        {
            get
            {
                return m_nSplitterDistance;
            }
            set
            {
                m_nSplitterDistance = value;
            }
        }

        public string[] SelectionPath
        {
            get
            {
                return m_strSelectionPath;
            }
            set
            {
                m_strSelectionPath = value;
            }
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            int nOrientation = (int)m_orientation;
            serializer.TraiteInt(ref nOrientation);
            m_orientation = (Orientation)nOrientation;
            serializer.TraiteInt(ref m_nSplitterDistance);

            int nNb = m_strSelectionPath.Length;
            serializer.TraiteInt ( ref nNb );
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (string strPath in m_strSelectionPath)
                    {
                        string strTmp = strPath;
                        serializer.TraiteString(ref strTmp);
                    }
                    break;
                case ModeSerialisation.Lecture:
                    List<string> lst = new List<string>();
                    for (int n = 0; n < nNb; n++)
                    {
                        string strTmp = "";
                        serializer.TraiteString(ref strTmp);
                        lst.Add(strTmp);
                    }
                    m_strSelectionPath = lst.ToArray();
                    break;
            }
            return result;
        }
    }
}
