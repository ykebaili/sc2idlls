using sc2i.common;
using sc2i.data.dynamic.StructureImport.SmartImport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.win32.data.dynamic.import.sources
{
    public interface IEditeurSourceSmartImport
    {
        void SetSource ( CSourceSmartImport source, CSetupSmartImportItem currentItem);
        CResultAErreur MajChamps();

        DataTable SourceTable { get; set; }

        event EventHandler ValueChanged;

        //Indique si le contrôle parent est en train de dessiner une image
        void SetIsDrawingImage(bool bIsDrawingImage);
    }
}
