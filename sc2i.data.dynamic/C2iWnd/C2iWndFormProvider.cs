using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.formulaire.subform;
using sc2i.formulaire;
using sc2i.common;

namespace sc2i.data.dynamic
{
    [AutoExec("Autoexec")]
    public class C2iWndFormProvider : I2iWndProvider
    {
        //---------------------------------------------------------
        public static void Autoexec()
        {
            C2iWndProvider.RegisterProvider(new C2iWndFormProvider());
        }

        //---------------------------------------------------------
        public string ProviderId
        {
            get { return "return SC2I_FORM"; }
        }

        //---------------------------------------------------------
        public IEnumerable<C2iWndReference> GetAvailable2iWnds()
        {
            CListeObjetDonneeGenerique<CFormulaire> lst = new CListeObjetDonneeGenerique<CFormulaire>(CContexteDonneeSysteme.GetInstance());
            List<C2iWndReference> lstReferences = new List<C2iWndReference>();
            foreach (CFormulaire form in lst)
            {
                C2iWndReference reference = new C2iWndReference(
                    ProviderId,
                    form.Id.ToString(),
                    form.Libelle);
                lstReferences.Add(reference);
            }
            return lstReferences.AsReadOnly();
        }

        //---------------------------------------------------------
        private CFormulaire GetFormulaire(C2iWndReference info)
        {
            try
            {
                int nId = Int32.Parse(info.WndKey);
                CFormulaire form = new CFormulaire(CContexteDonneeSysteme.GetInstance());
                if (form.ReadIfExists(nId))
                    return form;
            }
            catch { }
            return null;
        }


        //---------------------------------------------------------
        public C2iWndReference Get2iWndInfo(C2iWndReference info)
        {
            CFormulaire form = GetFormulaire(info);
            if (form != null)
                return new C2iWndReference(
                    ProviderId,
                    form.Id.ToString(),
                    form.Libelle);
            return null;
        }

        //---------------------------------------------------------
        public C2iWnd Get2iWnd(C2iWndReference info)
        {
            CFormulaire form = GetFormulaire(info);
            if (form != null)
                return form.Formulaire;
            return null;
        }

    }
}
