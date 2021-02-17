using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using System.Reflection;
using sc2i.win32.common;

namespace sc2i.win32.common
{
    public class CApplicateurRestrictions
    {
        public static void AppliqueRestrictions(
            Control ctrl, 
            CListeRestrictionsUtilisateurSurType lstRestrictions,
            CGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            if (ctrl == null) 
                return;

            Type tp = ctrl.GetType();
            while (tp != null)
            {
                foreach (FieldInfo info in tp.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField))
                {
                    //cherche les linkFields
                    if (typeof(CExtLinkField).IsAssignableFrom(info.FieldType))
                    {
                        try
                        {
                            CExtLinkField linkField = info.GetValue(ctrl) as CExtLinkField;
                            if (linkField != null)
                                linkField.AppliqueRestrictions(lstRestrictions, gestionnaireReadOnly);
                        }
                        catch { }
                    }
                }
                tp = tp.BaseType;
            }
            //Cherche les controles qui sont des IControleAGestionRestriction
            AppliqueRestrictionsSurIControlesAGestionRestrictions(ctrl,
                lstRestrictions,
                gestionnaireReadOnly);
        }

        //-------------------------------------------------------------------------------------
        private static void AppliqueRestrictionsSurIControlesAGestionRestrictions(Control ctrl, CListeRestrictionsUtilisateurSurType lstRestrictions, CGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            IControleAGestionRestrictions ctrlRes = ctrl as IControleAGestionRestrictions;
            if (ctrlRes != null)
                ctrlRes.AppliqueRestrictions(lstRestrictions, gestionnaireReadOnly);
            else
            {
                foreach (Control child in ctrl.Controls)
                    AppliqueRestrictionsSurIControlesAGestionRestrictions(child, lstRestrictions, gestionnaireReadOnly);
            }
        }

    }
}
