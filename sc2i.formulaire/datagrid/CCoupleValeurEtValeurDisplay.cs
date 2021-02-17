using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.formulaire.datagrid
{
    public class CCoupleValeurEtValeurDisplay
    {
        public string StringValue { get; set; }
        public object ObjectValue { get; set; }

        public CCoupleValeurEtValeurDisplay()
        {
        }

        public CCoupleValeurEtValeurDisplay(string strValue, object objValue)
        {
            StringValue = strValue;
            ObjectValue = objValue;
        }
    }
}
