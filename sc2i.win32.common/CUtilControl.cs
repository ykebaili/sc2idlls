using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;

namespace sc2i.win32.common
{
    public static class CUtilControl
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        private static Dictionary<IntPtr, int?> m_setControlesSuspended = new Dictionary<IntPtr, int?>();

        private static EventHandler m_disposeHandler = null;

        public static void SuspendDrawing(this Control ctrl)
        {
            if ( m_disposeHandler == null )
                m_disposeHandler = new EventHandler(ctrl_Disposed);

            if (ctrl.IsHandleCreated)
            {
                IntPtr ptr = ctrl.Handle;
                int? nNb = null;
                if (!m_setControlesSuspended.TryGetValue(ptr, out nNb))
                {
                    nNb = 0;
                    ctrl.Disposed += m_disposeHandler;
                    ctrl.SuspendLayout();
                    SendMessage(ctrl.Handle, WM_SETREDRAW, false, 0);
                }
                nNb++;
                m_setControlesSuspended[ptr] = nNb;
            }
            
        }

        public static void ctrl_Disposed(object sender, EventArgs e)
        {
            try
            {
                Control ctrl = sender as Control;
                if (ctrl != null && ctrl.IsHandleCreated && m_setControlesSuspended.ContainsKey(ctrl.Handle))
                    m_setControlesSuspended.Remove(ctrl.Handle);
            }
            catch { }
        }
        
        public static void ResumeDrawing(this Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
                return;
            int? nNb = 0;
            IntPtr ptr = ctrl.Handle;
            if (ptr == null)
                return;
            if (ptr != null && m_setControlesSuspended.TryGetValue(ptr, out nNb))
            {
                nNb--;
                m_setControlesSuspended[ptr] = nNb;
            }
            if (nNb == null || nNb == 0)
            {

                if (m_setControlesSuspended.ContainsKey(ptr))
                    m_setControlesSuspended.Remove(ptr);
                ctrl.Disposed -= m_disposeHandler;
                SendMessage(ctrl.Handle, WM_SETREDRAW, true, 0);
                ctrl.ResumeLayout();
                ctrl.Invalidate(true);
                ctrl.Update();
                //SendMessage(ctrl.Handle, (int)WindowsMessages.WM_PAINT, 0, 0);
                //ctrl.Refresh();
                //ctrl.Invalidate(true);
            }
        }

        public static bool IsDrawingSuspended(this Control ctrl)
        {
            return ctrl.IsHandleCreated && m_setControlesSuspended.ContainsKey(ctrl.Handle);
        }

        public static void ResumeDrawingForced(this Control ctrl)
        {
            if (ctrl.IsHandleCreated && m_setControlesSuspended.ContainsKey(ctrl.Handle))
                m_setControlesSuspended.Remove(ctrl.Handle);
            ResumeDrawing(ctrl);
        }

        public static void ClearAndDisposeControls(this Control ctrl)
        {
            ctrl.SuspendDrawing();
            foreach (Control fils in new ArrayList(ctrl.Controls))
            {
                fils.Visible = false;
                fils.Dispose();
                ctrl.Controls.Remove(fils);
            }
            ctrl.ResumeDrawing();
        }


        //---------------------------------------------------------
        /// <summary>
        /// recupère une pile de contrôles actifs (en descendant dans les fils
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static List<Control> GetActiveControls(Control ctrl)
        {
            List<Control> lst = new List<Control>();
            ContainerControl cont = ctrl as ContainerControl;
            if (cont != null)
            {
                ctrl = cont.ActiveControl;
                while (ctrl != null)
                {
                    lst.Add(ctrl);
                    ctrl = ctrl as ContainerControl;
                    if (ctrl != null)
                        ctrl = ((ContainerControl)ctrl).ActiveControl;
                }
            }
            return lst;
        }

        //--------------------------------------------------
        /// <summary>
        /// Définit le contrôle actif en descendant dans les fils
        /// </summary>
        /// <param name="ctrlParent"></param>
        /// <param name="lstControles"></param>
        public static void SetActiveControls(Control ctrlParent, List<Control> lstControles)
        {
            ContainerControl cont = ctrlParent as ContainerControl;
            if (lstControles == null || cont == null)
                return;
            foreach (Control ctrl in lstControles)
            {
                try
                {
                    cont.ActiveControl = ctrl;
                    cont = ctrl as ContainerControl;
                    if (cont == null)
                        break;
                }
                catch { }
            }
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Retourne la liste des contrôles dans l'ordre de tabulation. Seuls
        /// les contrôles visibles et avec TabOrder à true sont retournés
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<Control> GetTabOrderedControlsList(this Control parent)
        {
            List<Control> lst = new List<Control>();
            foreach (Control ctrl in parent.Controls)
                lst.Add(ctrl);
            lst.Sort((x, y) => x.TabIndex.CompareTo(y.TabIndex));
            List<Control> newList = new List<Control>();

            //Remplace chaque contrôle par son contenu si nécéssaire
            foreach (Control ctrlTmp in lst)
            {
                if (ctrlTmp.Controls.Count > 0)
                {
                    List<Control> lstTmp = ctrlTmp.GetTabOrderedControlsList();
                    newList.AddRange(lstTmp);
                }
                else
                    newList.Add(ctrlTmp);
            }

            //Supprime les contrôles invisibles et sans TabStop
            lst = new List<Control>();
            foreach (Control ctrlTmp in newList)
            {
                if (ctrlTmp.TabStop && ctrlTmp.Visible)
                    lst.Add(ctrlTmp);
            }
            return lst;
        }

        //-----------------------------------------------------------------------
        public static Control GetActiveControl(this Control parent)
        {
            List<Control> lst = GetActiveControls(parent);
            if (lst.Count > 0)
                return lst[lst.Count - 1];
            return null;
        }

        //-----------------------------------------------------------------------
        public static Control GetNextTabOrderedControl(this Control parent, Control ctrl, bool bForward)
        {
            if ( parent == null )
                return null;

            if (ctrl == null)
            {
                //Retourne le premier ou le dernier suivant bForward
                List<Control> lst = parent.GetTabOrderedControlsList();
                if (lst.Count > 0)
                {
                    if (bForward)
                        return lst[0];
                    else
                        return lst[lst.Count - 1];
                }
                return null;
            }

            if (ctrl.Controls.Count > 0 )
            {
                //le contrôle a des fils, va chercher le premier. dans le cas
                //de backward, il faut aller chercher le précédent
                List<Control> lst = ctrl.GetTabOrderedControlsList();
                if (lst.Count > 0)
                {
                    if (bForward)
                        return lst[0];
                    else
                        return lst[lst.Count - 1];
                }
            }

            Control partialParent = ctrl.Parent;
            while (partialParent != null)
            {
                ctrl = GetNext(partialParent, ctrl, bForward);
                if (ctrl != null)
                    return ctrl;
                if (partialParent == parent)
                    return null;
                ctrl = partialParent;
                partialParent = partialParent.Parent;
            }
            return null;
        }

        private static Control GetNext(Control parent, Control ctrl, bool bForward)
        {
            List<Control> lst = new List<Control>();
            foreach (Control t in parent.Controls)
                lst.Add(t);
            lst.Sort((x, y) => x.TabIndex.CompareTo(y.TabIndex));
            if (!bForward)
                lst.Reverse();
            int nIndex = lst.IndexOf(ctrl);
            if (nIndex >= 0)
            {
                nIndex++;
                while (nIndex < lst.Count)
                {
                    ctrl = lst[nIndex];
                    if (ctrl.Controls.Count > 0)
                        ctrl = parent.GetNextTabOrderedControl(ctrl, bForward);
                    if (ctrl != null && ctrl.Visible && ctrl.TabStop)
                        return ctrl;
                    nIndex++;
                }
            }
            return null;
        }


    }
}
