using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace futurocom.chart
{
    public interface IFournisseurValeursSerie : I2iSerializable
    {
        string SourceId { get; set; }

        string LabelType { get; }

        string GetLabel(CDonneesDeChart donnees);

        object[] GetValues(CChartSetup chartSetup);

        bool IsApplicableToSource(CParametreSourceChart parametre);
    }
}
