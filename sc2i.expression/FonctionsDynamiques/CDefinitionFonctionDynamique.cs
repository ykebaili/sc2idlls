using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using sc2i.common;
using System.Collections.Generic;
using sc2i.expression.FonctionsDynamiques;
using System.Text;

namespace sc2i.expression.FonctionsDynamiques
{
	/// <summary>
	/// Description résumée de CDefinitionFonctionDynamique.
	/// </summary>
	[Serializable]
	public class CDefinitionFonctionDynamique : CDefinitionProprieteDynamique
	{
		private const string c_strCleType = "FUNCDYN";

        private string m_strIdFonction = "";

        //Les paramètres sont stockés dans la définition pour que même si on n'arrive pas
        //à avoir la fonction, le champ stocke les paramètres de celle-ci
        private List<CParametreFonctionDynamique> m_listeParametresDeLaFonction = new List<CParametreFonctionDynamique>();

		public CDefinitionFonctionDynamique()
			:base()
		{
		}
		

		public override string CleType
		{
			get { return c_strCleType; }
		}

        public CDefinitionFonctionDynamique(
            CFonctionDynamique fonction)
            : base(
            fonction.Nom,
            fonction.Nom,
            fonction.TypeRetourne,
            true,
            true)
        {
            StringBuilder bl = new StringBuilder();
            bl.Append ( fonction.Nom );
            bl.Append("(");
            foreach ( CParametreFonctionDynamique parametre in fonction.Parametres )
            {
                bl.Append ( parametre.Nom );
                bl.Append("; ");
            }
            if ( fonction.Parametres.Count() > 0 )
                bl.Remove ( bl.Length-2, 2 );
            bl.Append(")");
            m_strNomConvivial = bl.ToString();
            m_strIdFonction = fonction.IdFonction;
            m_listeParametresDeLaFonction = new List<CParametreFonctionDynamique>(fonction.Parametres);
        }

        //---------------------------------------------------------------
        public string IdFonction
        {
            get
            {
                return m_strIdFonction;
            }
        }

        //---------------------------------------------------------------
        public IEnumerable<CParametreFonctionDynamique> ParametresDeLaFonction
        {
            get
            {
                return m_listeParametresDeLaFonction;
            }
        }

        //---------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            result = base.Serialize ( serializer );
            if ( !result )
                return result;
            serializer.TraiteString ( ref m_strIdFonction );
            result = serializer.TraiteListe<CParametreFonctionDynamique>(m_listeParametresDeLaFonction);
            if (!result)
                return result;
            return result;
        }
	}

    //-------------------------------------------------------------------------------------
    public interface IFournisseurFonctionsDynamiquesSupplementaire
    {
        IEnumerable<CDefinitionProprieteDynamique> GetDefinitionsFonctionsSupplementaires(CObjetPourSousProprietes objet);
        CFonctionDynamique GetFonctionSupplementaire(string strIdFonction);
    }

    [AutoExec("Autoexec")]
    public class CFournisseurFonctionsDynamiques : IFournisseurProprieteDynamiquesSimplifie
    {
        private static List<IFournisseurFonctionsDynamiquesSupplementaire> m_listeFournisseursSupplementaires = new List<IFournisseurFonctionsDynamiquesSupplementaire>();
        private static Dictionary<string, CFonctionDynamique> m_dicFonctionsDynamiques = new Dictionary<string, CFonctionDynamique>();
        private static Dictionary<Type, List<CFonctionDynamique>> m_dicFonctionsDynamiquesParType = new Dictionary<Type, List<CFonctionDynamique>>();

        //-------------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurFonctionsDynamiques());
        }

        //-------------------------------------------------------------------------------------
        public static void RegisterFournisseurSupplementaire ( IFournisseurFonctionsDynamiquesSupplementaire fournisseur )
        {
            if ( fournisseur != null )
            {
                if ( m_listeFournisseursSupplementaires.FirstOrDefault(f=>f.GetType() == fournisseur.GetType()) == null )
                    m_listeFournisseursSupplementaires.Add ( fournisseur );
            }
        }

        //-------------------------------------------------------------------------------------
        public static void RegisterFonctionDynamique ( Type tp, CFonctionDynamique fonction )
        {
            if ( fonction == null )
                return;
            List<CFonctionDynamique> lst = null;
            if ( !m_dicFonctionsDynamiquesParType.TryGetValue ( tp, out lst ) )
            {
                lst = new List<CFonctionDynamique>();
                m_dicFonctionsDynamiquesParType[tp] = lst;
            }
            CFonctionDynamique fExistante = lst.FirstOrDefault(f=>f.IdFonction == fonction.IdFonction );
            if ( fExistante != null )
                lst.Remove ( fExistante );
            lst.Add(fonction);
            lst.Sort((x, y) => x.Nom.CompareTo(y.Nom));
            m_dicFonctionsDynamiques[fonction.IdFonction] = fonction;
        }

        //-------------------------------------------------------------------------------------
        public static CFonctionDynamique GetFonctionGlobale(string strIdFonction)
        {
            CFonctionDynamique fonction = null;
            m_dicFonctionsDynamiques.TryGetValue(strIdFonction, out fonction);
            if (fonction == null)
            {
                foreach (IFournisseurFonctionsDynamiquesSupplementaire f in m_listeFournisseursSupplementaires)
                {
                    fonction = f.GetFonctionSupplementaire(strIdFonction);
                    if (fonction != null)
                        break;
                }
            }
            return fonction;
        }

      

        //-------------------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            List<CDefinitionProprieteDynamique> lstRetour = new List<CDefinitionProprieteDynamique>();
            if (objet == null || objet.TypeAnalyse == null)
                return lstRetour.ToArray();
            List<CFonctionDynamique> lst = null;
            if ( m_dicFonctionsDynamiquesParType.TryGetValue ( objet.TypeAnalyse, out lst ) )
            {
                foreach ( CFonctionDynamique fonction in lst )
                {
                    CDefinitionFonctionDynamique def = new CDefinitionFonctionDynamique(fonction);
                    def.Rubrique = I.T("Methods|58");
                    lstRetour.Add ( def);
                }
            }
            IElementAFonctionsDynamiques eltAFonctions = objet.ObjetAnalyse as IElementAFonctionsDynamiques;
            if (eltAFonctions != null)
            {
                foreach (CFonctionDynamique fonction in eltAFonctions.FonctionsDynamiques)
                {
                    CDefinitionFonctionDynamique def = new CDefinitionFonctionDynamique(fonction);
                    def.Rubrique = I.T("Methods|58");
                    lstRetour.Add(def);
                }
            }
            foreach (IFournisseurFonctionsDynamiquesSupplementaire f in m_listeFournisseursSupplementaires)
            {
                try
                {
                    lstRetour.AddRange ( f.GetDefinitionsFonctionsSupplementaires(objet ) );
                }
                catch { }
            }
            lstRetour.Sort((x, y) => x.Nom.CompareTo(y.Nom));

            return lstRetour.ToArray();
        }

    }

}
