using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace sc2i.data.serveur
{
    public class CUtilDataAdapter
    {
        public static void DisposeAdapter(IDataAdapter adapter)
        {
            IDbDataAdapter dbAd = adapter as IDbDataAdapter;
            if (dbAd != null)
            {
                try
                {
                    if (dbAd.SelectCommand != null)
                        dbAd.SelectCommand.Dispose();
                }
                catch { }
                try
                {
                    if (dbAd.UpdateCommand != null)
                        dbAd.UpdateCommand.Dispose();
                }
                catch { }
                try
                {
                    if (dbAd.InsertCommand != null)
                        dbAd.InsertCommand.Dispose();
                }
                catch { }
                try
                {
                    if (dbAd.DeleteCommand != null)
                        dbAd.DeleteCommand.Dispose();
                }
                catch { }
            }
            IDisposable dis = adapter as IDisposable;
            if (dis != null)
            {
                try
                {
                    dis.Dispose();
                }
                catch { }
            }
        }
            
    }
}
