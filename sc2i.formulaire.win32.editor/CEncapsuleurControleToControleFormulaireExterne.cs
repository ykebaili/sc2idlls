using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using sc2i.expression;
using System.Drawing;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.common;

namespace sc2i.formulaire.win32
{
    public delegate bool ControleExterneEventHandler ( string strIdEvent, object sender );

    [AttributeUsage(AttributeTargets.Event)]
    public class ControleExterneEventAttribute : Attribute
    {
        public readonly string NomEvenement;
        public readonly string Description;

        public ControleExterneEventAttribute(string strNomEvenement, string strDescription)
        {
            NomEvenement = strNomEvenement;
            Description = strDescription;
        }
    }


    /// <summary>
    /// Convertit un controle en controle de formulaire externe
    /// </summary>
    public class CEncapsuleurControleToControleFormulaireExterne : IControleFormulaireExterne, IElementAProprietesDynamiquesDeportees
    {
        private Control m_controleAssocie = null;
        private Point m_position;
        private IRuntimeFor2iWnd m_runtime = null;

        public CEncapsuleurControleToControleFormulaireExterne(Control ctrl, Point positionDansParentReference)
        {
            m_controleAssocie = ctrl;
            m_position = positionDansParentReference;
        }


        private static Dictionary<Type, List<CDescriptionEvenementParFormule>> m_cacheDescriptionsEvenements = new Dictionary<Type, List<CDescriptionEvenementParFormule>>();
        private static Dictionary<Type, List<CDefinitionProprieteDynamique>> m_cacheProprietes = new Dictionary<Type, List<CDefinitionProprieteDynamique>>();

        //----------------------------------------------------------------------------------------
        private static List<CDescriptionEvenementParFormule> GetDescriptionsEvenements(Type tp)
        {
            List<CDescriptionEvenementParFormule> lst = null; new List<CDescriptionEvenementParFormule>();
            if (m_cacheDescriptionsEvenements.TryGetValue(tp, out lst))
                return lst;
            lst = new List<CDescriptionEvenementParFormule>();
            //Cherche s'il y a des evenements compatibles
            foreach (EventInfo info in tp.GetEvents())
            {
                if (info.EventHandlerType == typeof(ControleExterneEventHandler))
                {
                    string strNom = info.Name;
                    string strDescription = "";
                    object[] attrs = info.GetCustomAttributes(typeof(ControleExterneEventAttribute), true);
                    if (attrs.Length > 0)
                    {
                        ControleExterneEventAttribute att = attrs[0] as ControleExterneEventAttribute;
                        strNom = att.NomEvenement;
                        strDescription = att.Description;
                    }
                    CDescriptionEvenementParFormule desc = new CDescriptionEvenementParFormule(
                        info.Name,
                        strNom,
                        strDescription);
                    lst.Add(desc);
                }
            }
            m_cacheDescriptionsEvenements[tp] = lst;
            return lst;

        }

        //----------------------------------------------------------------------------------------
        public object Control
        {
            get
            {
                return m_controleAssocie;
            }
        }

        //----------------------------------------------------------------------------------------
        //convertit un contrôle en controle formulaire externe, si possible
        public static IControleFormulaireExterne GetControleFormulaireExterne(Control ctrl, Control ctrlParentReference)
        {
            if (ctrl is IControleFormulaireExterne)
                return (IControleFormulaireExterne)ctrl;

            List<CDescriptionEvenementParFormule> lstEvents = GetDescriptionsEvenements(ctrl.GetType());
            if (lstEvents.Count > 0)
            {
                return new CEncapsuleurControleToControleFormulaireExterne(ctrl, ctrlParentReference.PointToClient(ctrl.PointToScreen(new Point ( 0, 0 ))));
            }
            return null;
        }

        //----------------------------------------------------------------------------------------
        public static List<IControleFormulaireExterne> GetControlesFormulaireExterne ( Control parent )
        {
            List<Control> lst = new List<Control>();
            FillListeControles(parent, lst);
            List<IControleFormulaireExterne> lstRetour = new List<IControleFormulaireExterne>();
            foreach (Control ctrl in lst)
            {
                IControleFormulaireExterne ctrlEx = GetControleFormulaireExterne(ctrl, parent);
                if (ctrlEx != null)
                    lstRetour.Add(ctrlEx);
            } 
            return lstRetour;
        }

        //----------------------------------------------------------------------------------------
        private static void FillListeControles(Control ctrl, List<Control> lst)
        {
            lst.Add(ctrl);
            foreach (Control child in ctrl.Controls)
                FillListeControles(child, lst);
        }

        //----------------------------------------------------------------------------------------
        private static List<CDefinitionProprieteDynamique> GetDefinitionsChamps(Type tp)
        {
            List<CDefinitionProprieteDynamique> lst = null; 
            if (m_cacheProprietes.TryGetValue(tp, out lst))
                return lst;
            CDefinitionProprieteDynamique[] defs = new CFournisseurGeneriqueProprietesDynamiques().GetDefinitionsChamps(tp);
            lst = new List<CDefinitionProprieteDynamique>();
            foreach (CDefinitionProprieteDynamique def in defs)
            {
                if (def is CDefinitionProprieteDynamiqueDotNet)
                {
                    CDefinitionProprieteDynamiqueDeportee defDep = new CDefinitionProprieteDynamiqueDeportee(
                        def.Nom,
                        def.NomPropriete,
                        def.TypeDonnee,
                        def.HasSubProperties,
                        def.IsReadOnly,
                        def.Rubrique);
                    lst.Add(defDep);
                }
            }
            m_cacheProprietes[tp] = lst;
            return lst;
        }


        //----------------------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetProprietes()
        {
            return GetDefinitionsChamps(m_controleAssocie.GetType()).ToArray();
        }

        //----------------------------------------------------------------------------------------
        public CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            return GetDescriptionsEvenements(m_controleAssocie.GetType()).ToArray();
        }

        //----------------------------------------------------------------------------------------
        public string Name
        {
            get { return m_controleAssocie.Name; }
        }

        //----------------------------------------------------------------------------------------
        public System.Drawing.Point Location
        {
            get { return m_position; }
        }

        //----------------------------------------------------------------------------------------
        public System.Drawing.Size Size
        {
            get { return m_controleAssocie.Size; }
        }

        //----------------------------------------------------------------------------------------
        public void AttacheToWndFor2iWnd(IRuntimeFor2iWnd wnd)
        {
            m_runtime = wnd;
            foreach (CHandlerEvenementParFormule handler in wnd.WndAssociee.GetHanlders())
            {
                if (handler.FormuleEvenement != null)
                {
                    //Trouve l'evenement dans le type
                    EventInfo info = m_controleAssocie.GetType().GetEvent(handler.IdEvenement);
                    if (info != null)
                    {
                        info.AddEventHandler(m_controleAssocie, new ControleExterneEventHandler(OnEventSurControle));
                    }
                }
            }
        }

        //----------------------------------------------------------------------------------------
        public bool OnEventSurControle(string strIdEvent, object sender)
        {
            IControleWndFor2iWnd ctrl = m_runtime as IControleWndFor2iWnd;
            if (ctrl != null)
            {
                CResultAErreur result = CUtilControlesWnd.DeclencheEvenement(strIdEvent, ctrl);
                if (result.Data is bool)
                    return (bool)result.Data;
            }
            return true;
        }

        //----------------------------------------------------------------------------------------
        public object GetValeurDynamiqueDeportee(string strPropriete)
        {
            if ( m_controleAssocie == null )
                return null;
            CResultAErreur result = CInterpreteurProprieteDynamique.GetValue ( m_controleAssocie, strPropriete );
            if (result)
                return result.Data;
            return null;
        }

        //----------------------------------------------------------------------------------------
        public void SetValeurDynamiqueDeportee(string strPropriete, object valeur)
        {
            if ( m_controleAssocie == null )
                return;
            try{
                CInterpreteurProprieteDynamique.SetValue ( m_controleAssocie, strPropriete, valeur );
            }
            catch{}
        }

    }
}
