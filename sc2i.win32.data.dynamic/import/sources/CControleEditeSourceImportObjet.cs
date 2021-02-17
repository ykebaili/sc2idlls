using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data.dynamic.StructureImport.SmartImport;

namespace sc2i.win32.data.dynamic.import.sources
{
    [AutoExec("Autoexec")]
    public partial class CControleEditeSourceImportObjet : UserControl, IEditeurSourceSmartImport
    {
        //---------------------------------------------------------------
        public CControleEditeSourceImportObjet()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------
        public static void Autoexec()
        {
            CControleEditeSourceImport.RegisterEditeur(typeof(CSourceSmartImportObjet), typeof(CControleEditeSourceImportObjet));
        }

        //---------------------------------------------------------------
        public void SetSource(CSourceSmartImport source, CSetupSmartImportItem currentItem)
        {
            
        }

        //---------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            return CResultAErreur.True;
        }

        //---------------------------------------------------------------
        public DataTable SourceTable
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        //---------------------------------------------------------------
        public event EventHandler ValueChanged;

        //---------------------------------------------------------------
        public void SetIsDrawingImage(bool bIsDrawingImage)
        {
            
        }
    }
}
