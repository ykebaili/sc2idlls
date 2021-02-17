using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing.Design;

namespace sc2i.formulaire.subform
{
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Référence à une fenêtre
    /// </summary>
    public class C2iWndReference : I2iSerializable
    {
        private string m_strProviderKey = "";
        private string m_str2iWndKey = "";
        private string m_str2iWndLabel = "";

        public C2iWndReference()
        {
        }

        public C2iWndReference(string strProviderKey, string str2iWndKey, string str2iWndLabel)
        {
            m_strProviderKey = strProviderKey;
            m_str2iWndKey = str2iWndKey;
            m_str2iWndLabel = str2iWndLabel;
        }

        public string ProviderKey
        {
            get
            {
                return m_strProviderKey;
            }
            set
            {
                m_strProviderKey = value;
            }
        }
        public string WndKey
        {
            get
            {
                return m_str2iWndKey;
            }
            set
            {
                m_str2iWndKey = value;
            }
        }

        public string WndLabel
        {
            get
            {
                return m_str2iWndLabel;
            }
            set
            {
                m_str2iWndLabel = value;
            }
        }

        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strProviderKey);
            serializer.TraiteString(ref m_str2iWndKey);
            serializer.TraiteString(ref m_str2iWndLabel);
            return result;
        }
    }


    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Fournisseur générique de fenêtres
    /// </summary>
    public interface I2iWndProvider
    {
        /// <summary>
        /// Retourne la clé du provider (identifiant unique du provider)
        /// </summary>
        string ProviderId { get; }

        /// <summary>
        /// Retourne la liste des 2iWnds disponibles
        /// </summary>
        /// <returns></returns>
        IEnumerable<C2iWndReference> GetAvailable2iWnds();

        /// <summary>
        /// Return un C2iWndReference avec libellé à jour à partir d'un C2iWndReference
        /// dont le libellé peut être douteux
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        C2iWndReference Get2iWndInfo(C2iWndReference info);

        /// <summary>
        /// Retourne le 2iWnd demandé
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        C2iWnd Get2iWnd(C2iWndReference info);
    }


    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Fournisseur de fenêtres
    /// </summary>
    public static class C2iWndProvider
    {
        private static Dictionary<string, I2iWndProvider> m_dicProviders = new Dictionary<string, I2iWndProvider>();

        //----------------------------------------------------------------
        public static void RegisterProvider(I2iWndProvider provider)
        {
            if (provider != null)
            {
                m_dicProviders[provider.ProviderId] = provider;
            }
        }

        //----------------------------------------------------------------
        public static IEnumerable<C2iWndReference> GetAvailable2iWnd()
        {
            List<C2iWndReference> lst = new List<C2iWndReference>();
            foreach (I2iWndProvider provider in m_dicProviders.Values)
            {
                lst.AddRange(provider.GetAvailable2iWnds());
            }
            lst.Sort((x, y) => x.WndLabel.CompareTo(y.WndLabel));
            return lst.AsReadOnly();
        }

        //----------------------------------------------------------------
        public static C2iWnd GetForm ( C2iWndReference wndReference )
        {
            I2iWndProvider provider = null;
            if ( m_dicProviders.TryGetValue ( wndReference.ProviderKey, out provider ))
                return provider.Get2iWnd ( wndReference );
            return null;
        }
    }

    public interface I2iWndReferenceSelector
    {
        C2iWndReference Select2iWndReference(C2iWndReference reference);
    }

    //----------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    public class C2iWndReferenceEditor : UITypeEditor
    {
        private static I2iWndReferenceSelector m_editeur = null;
        /// ///////////////////////////////////////////
        public C2iWndReferenceEditor()
        {
        }

        /// ///////////////////////////////////////////
        public static void SetEditeur(I2iWndReferenceSelector editeur)
        {
            m_editeur = editeur;
        }

        

        /// ///////////////////////////////////////////
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
            System.IServiceProvider provider,
            object value)
        {
            object retour = null;
            if (m_editeur != null)
            {
                retour = m_editeur.Select2iWndReference(value as C2iWndReference);
            }
            return retour;
        }

        /// ///////////////////////////////////////////
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (m_editeur == null )
                return UITypeEditorEditStyle.None;
            return UITypeEditorEditStyle.Modal;
        }


    }

    /// ///////////////////////////////////////////
    public class C2iWndReferenceConvertor : CGenericObjectConverter<C2iWndReference>
    {
        public override string GetString(C2iWndReference value)
        {
            if (value != null)
                return value.WndLabel;
            return "";
        }
    }
}
