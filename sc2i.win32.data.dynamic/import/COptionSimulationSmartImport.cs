using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.win32.data.dynamic.import
{
    public class COptionSimulationSmartImport
    {
        public int? StartLine = null;
        public int? EndLine = null;
        public bool TestDbWriting = true;

        public int? NbLineToImport
        {
            get
            {
                if ( EndLine != null )
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
