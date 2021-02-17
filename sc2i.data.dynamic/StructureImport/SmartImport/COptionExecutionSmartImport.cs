using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class COptionExecutionSmartImport
    {
        private int? m_nLigneStart = null;
        private int? m_nLigneEnd = null;

        private int? m_nTaillePaquet = null;
        private bool m_bUtiliserVersionDonnee = false;

        private bool m_bBestEffort = true;
        private string m_strNomFichierSauvegarde = "";

        //----------------------------------------------
        public COptionExecutionSmartImport()
        {}

        //----------------------------------------------
        public int? StartLine
        {
            get
            {
                return m_nLigneStart;
            }
            set
            {
                m_nLigneStart = value;
            }
        }

        //----------------------------------------------
        public bool UtiliserVersionDonnee
        {
            get{
                return m_bUtiliserVersionDonnee;
            }
            set { m_bUtiliserVersionDonnee = value; }
        }

        //----------------------------------------------
        public int? EndLine
        {
            get
            {
                return m_nLigneEnd;
            }
            set
            {
                m_nLigneEnd = value;
            }
        }

        //----------------------------------------------
        public int? TaillePaquets
        {
            get
            {
                return m_nTaillePaquet;
            }
            set
            {
                m_nTaillePaquet = value;
            }
        }

        //----------------------------------------------
        public bool BestEffort
        {
            get
            {
                return m_bBestEffort;
            }
            set
            {
                m_bBestEffort = value;
            }
        }

        //----------------------------------------------
        public string NomFichierSauvegarde
        {
            get
            {
                return m_strNomFichierSauvegarde;
            }
            set
            {
                m_strNomFichierSauvegarde = value;
            }
        }

        //--------------------------------------------------------
        public int? NbLineToImport
        {
            get
            {
                if (EndLine != null)
                {
                    if (StartLine != null)
                        return EndLine.Value - StartLine.Value + 1;
                    return EndLine.Value + 1;
                }
                return null;
            }
            set
            {
                if (value == null)
                    EndLine = null;
                else
                {
                    if (StartLine == null)
                        EndLine = value - 1;
                    else
                        EndLine = value - 1 + StartLine;
                }
            }
        }
        
    }
}
