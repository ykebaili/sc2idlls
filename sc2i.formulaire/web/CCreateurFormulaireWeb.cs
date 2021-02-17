using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.formulaire.web
{
    public class CCreateurFormulaireWeb
    {
        private static Dictionary<Type, Type> m_dicType2iWndTo2iWebControl = new Dictionary<Type, Type>();

        //-------------------------------------------------------------------------
        public static void RegisterWebControl(Type type2iWnd, Type typeWebControl)
        {
            m_dicType2iWndTo2iWebControl[type2iWnd] = typeWebControl;
        }

        //-------------------------------------------------------------------------
        public I2iWebControl GetWebControle(C2iWnd wnd, CContexteGenerationControleWeb contexteWeb)
        {
            if (wnd == null)
                return null;
            Type tp = null;
            if (m_dicType2iWndTo2iWebControl.TryGetValue(wnd.GetType(), out tp))
            {
                I2iWebControl webControl = Activator.CreateInstance(tp, new object[0]) as I2iWebControl;
                return webControl;
            }
            return null;
        }
    }
}
