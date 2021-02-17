using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.expression
{
    //-------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------
    public class CSourceSupplementaire
    {
        public string NomSource;
        public object Source;

        public CSourceSupplementaire(string strNomSource,
            object source)
        {
            NomSource = strNomSource;
            Source = source;
        }
    }

    //-------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------
    /// <summary>
    /// Permet d'ajouter une source en tant que source de formule pour des formules qui historiquement
    /// ne portaient que sur une seule source.
    /// PAr exemple, on avait des formules qui portaient sur l'élément édité dans les formulaires
    /// avec Multi source, il a été possible d'ajouter le CurrentWindow en restant compatible avec les anciennes
    /// formules.
    /// </summary>
    public class CDefinitionMultiSourceForExpression : IElementAVariableInstance
    {
        private Dictionary<string, object> m_dicSourcesSupplementaires = new Dictionary<string, object>();
        private object m_objetPrincipal = null;

        //--------------------------------------------------------------------------
        public CDefinitionMultiSourceForExpression(
            object objetPrincipal,
            params CSourceSupplementaire[] SourcesSupplementaires)
        {
            ObjetPrincipal = objetPrincipal;
            if (SourcesSupplementaires != null)
            {
                foreach (CSourceSupplementaire src in SourcesSupplementaires)
                    m_dicSourcesSupplementaires[src.NomSource] = src.Source;
            }
        }

        //--------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lstDefs = new List<CDefinitionProprieteDynamique>();
            foreach (KeyValuePair<string, object> kv in m_dicSourcesSupplementaires)
            {
                lstDefs.Add(new CDefinitionProprieteDynamiqueSourceSupplementaire(
                    kv.Key, kv.Value));
            }
            return lstDefs.ToArray();
        }

        //--------------------------------------------------------------------------
        public object ObjetPrincipal
        {
            get
            {
                return m_objetPrincipal;
            }
            set
            {
                CDefinitionMultiSourceForExpression def = value as CDefinitionMultiSourceForExpression;
                if ( def != null )
                {
                    m_objetPrincipal = def.ObjetPrincipal;
                    foreach ( string strNomSource in def.GetNomSources() )
                        AddSource ( strNomSource, def.GetSource ( strNomSource ));
                }
                else
                    m_objetPrincipal = value;
            }
        }

        //--------------------------------------------------------------------------
        public CObjetPourSousProprietes DefinitionObjetPrincipal
        {
            get
            {
                CObjetPourSousProprietes obj = ObjetPrincipal as CObjetPourSousProprietes;
                if (obj == null)
                    obj = new CObjetPourSousProprietes(ObjetPrincipal);
                return obj;
            }
        }

        //--------------------------------------------------------------------------
        public void AddSource(string strNomSource, object objet)
        {
            m_dicSourcesSupplementaires[strNomSource] = objet;
        }

        //--------------------------------------------------------------------------
        public void RemoveSource(string strNomSource)
        {
            if (m_dicSourcesSupplementaires.ContainsKey(strNomSource))
                m_dicSourcesSupplementaires.Remove(strNomSource);
        }

        //--------------------------------------------------------------------------
        public object GetSource(string strNomSource)
        {
            foreach (KeyValuePair<string, object> kv in m_dicSourcesSupplementaires)
            {
                if (kv.Key.ToUpper() == strNomSource.ToUpper())
                    return kv.Value;
            }
            return null;
        }

        //--------------------------------------------------------------------------
        public IEnumerable<string> GetNomSources()
        {
            return m_dicSourcesSupplementaires.Keys.ToArray();
        }

        


    }

    //--------------------------------------------------------------------------
    public class CDefinitionProprieteDynamiqueSourceSupplementaire : CDefinitionProprieteDynamiqueInstance
    {
        public static string c_strCleType = "SRCSUPP";

        //--------------------------------------------------------------------------
        public CDefinitionProprieteDynamiqueSourceSupplementaire()
            :base()
        {
        }

        //--------------------------------------------------------------------------
        public CDefinitionProprieteDynamiqueSourceSupplementaire(string strNom,
            object instance)
            : base(
                strNom, strNom, instance, "")
        {
        }

        //--------------------------------------------------------------------------
        public override string CleType
        {
            get
            {
                return c_strCleType;
            }
        }
    }

    //--------------------------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CInterpreteurProprieteDynamiqueSourceSupplementaire : IInterpreteurProprieteDynamique
    {
        //----------------------------------------------------------------
        public static void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(CDefinitionProprieteDynamiqueSourceSupplementaire.c_strCleType,
                typeof(CInterpreteurProprieteDynamiqueSourceSupplementaire));
        }

        //----------------------------------------------------------------
        public CResultAErreur GetValue(object objet, string strPropriete)
        {
            CResultAErreur result = CResultAErreur.True;
            CDefinitionMultiSourceForExpression defMultiSource = objet as CDefinitionMultiSourceForExpression;
            if ( defMultiSource != null )
            {
                result.Data = defMultiSource.GetSource ( strPropriete );
                return result;
            }
            return result;
        }

        //----------------------------------------------------------------
        public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
        {
            return CResultAErreur.True;
        }

        //----------------------------------------------------------------
        public bool ShouldIgnoreForSetValue(string strPropriete)
        {
            return true;
        }

        //----------------------------------------------------------------
        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return null;
        }
    }


}
