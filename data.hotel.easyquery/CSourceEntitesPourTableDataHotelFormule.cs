using futurocom.easyquery;
using sc2i.common;
using sc2i.expression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery
{
    public class CSourceEntitesPourTableDataHotelFormule : ISourceEntitesPourTableDataHotel
    {
        private C2iExpression m_formuleEntites = null;

        //------------------------------------------------------
        public CSourceEntitesPourTableDataHotelFormule()
        {
        }

        //------------------------------------------------------
        public C2iExpression FormuleEntites
        {
            get
            {
                return m_formuleEntites;
            }
            set
            {
                m_formuleEntites = value;
            }
        }


        //------------------------------------------------------
        public IEnumerable<string> GetListeIdsEntites(CEasyQuery query)
        {
            List<string> lstResult = new List<string>();
            if ( FormuleEntites != null )
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(query);
                CResultAErreur result = FormuleEntites.Eval(ctx);
                if (!result)
                    return lstResult;
                IEnumerable en = result.Data as IEnumerable;
                if (result.Data is string)
                    en = null;
                if ( en != null )
                    foreach ( object elt in en)
                    {
                        if (elt != null)
                            lstResult.Add(elt.ToString());
                    }
                else
                {
                    if (result.Data != null)
                        lstResult.Add(result.Data.ToString());
                }
            }
            return lstResult;
        }

        //------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int  nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result )
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleEntites);
            return result;
        }

        
    }
}
