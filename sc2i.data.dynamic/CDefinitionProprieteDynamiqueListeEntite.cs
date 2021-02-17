using System;

using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Propriété définie par une liste d'entités
	/// La propriété contient l'id de la liste
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueListeEntites : CDefinitionProprieteDynamique
	{
		private const string c_strCleType = "LE";

        //TESTDBKEYTODO : remplacer m_nIdListeEntite par un DbKeyChamp
        protected CDbKey m_dbKeyListeEntite = null;

		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueListeEntites()
			:base()
		{
		}
		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueListeEntites( CListeEntites liste )
			: base(
			liste.Libelle.Replace(" ", "_"),
			liste.Id.ToString(),
			new CTypeResultatExpression(liste.TypeElements, true ),
			true,
			true )
		{
            m_dbKeyListeEntite = liste.DbKey;
		}

		/// //////////////////////////////////////////////////////
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition ( c_strCleType, typeof(CInterperteurProprieteDynamiqueListeEntite) );
		}

		//-----------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		/// //////////////////////////////////////////////////////
		[ExternalReferencedEntityDbKey(typeof(CListeEntites))]
        public CDbKey DbKeyListeEntite
		{
			get
			{
				return m_dbKeyListeEntite;
			}
		}

		public override void CopyTo(CDefinitionProprieteDynamique def)
		{
			base.CopyTo(def);
			((CDefinitionProprieteDynamiqueListeEntites)def).m_dbKeyListeEntite = m_dbKeyListeEntite;
		}

		/// ////////////////////////////////////////
		private int GetNumVersion()
		{
			//return 0;
            return 1;
		}

		/// ////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if ( !result )
				return result;
            if (nVersion < 1)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyListeEntite, typeof(CListeEntites));
            else
                serializer.TraiteDbKey(ref m_dbKeyListeEntite);

			return result;
		}
	}

	/// ////////////////////////////////////////
	public class CInterperteurProprieteDynamiqueListeEntite : IInterpreteurProprieteDynamique
	{
		//------------------------------------------------------------
		public bool ShouldIgnoreForSetValue(string strPropriete)
		{
			return false;
		}

		//------------------------------------------------------------
		public CResultAErreur GetValue(object objet, string strPropriete)
		{
			CResultAErreur result = CResultAErreur.True;
			CObjetDonnee objetDonnee = objet as CObjetDonnee;
			if (objetDonnee == null)
				return result;

			int nIdListe = -1;
			try
			{
				nIdListe = Int32.Parse(strPropriete);
			}
			catch
			{
				result.EmpileErreur(I.T("Bad entity list property format (@1)|20028", strPropriete ));
				return result;
			}
			CListeEntites liste = new CListeEntites(objetDonnee.ContexteDonnee);
			try
			{
				if (liste.ReadIfExists(nIdListe) )
				{
					CListeObjetsDonnees resultListe = liste.GetElementsLiesFor(objetDonnee);
					if (resultListe != null)
						result.Data = resultListe.ToArray(liste.TypeElements);
				}
				else
				{
					result.EmpileErreur ( I.T("Entity list @1 doesn't exists|20029", strPropriete ));
					return result;
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}

		public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Forbidden affectation|20034"));
			return result;
		}

        public class COptimiseurProprieteDynamiqueListeEntite : IOptimiseurGetValueDynamic
        {
            private int m_nIdList;
            private Type m_typeElements = null;

            public COptimiseurProprieteDynamiqueListeEntite ( int nIdList, Type typeElements )
            {
                m_nIdList = nIdList;
                m_typeElements = typeElements;
            }

            public object GetValue(object objet)
            {
                CObjetDonnee objetDonnee = objet as CObjetDonnee;
                if (objetDonnee == null || m_nIdList < 0)
                    return null;

                CListeEntites liste = new CListeEntites(objetDonnee.ContexteDonnee);
                try
                {
                    if (liste.ReadIfExists(m_nIdList))
                    {
                        CListeObjetsDonnees resultListe = liste.GetElementsLiesFor(objetDonnee);
                        return resultListe;
                    }
                }
                catch 
                {

                }
                return null;
            }

            public Type GetTypeRetourne()
            {
                return m_typeElements;
            }

        }

        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            int nId = -1;
            Type tp2 = null;
            try
            {
                nId = Int32.Parse(strPropriete);
                CListeEntites liste = new CListeEntites(CContexteDonneeSysteme.GetInstance());
                if (liste.ReadIfExists(nId))
                    tp2 = liste.TypeElements;
            }
            catch
            {
            }
            return new COptimiseurProprieteDynamiqueListeEntite(nId, tp2);
        }
    }

	[AutoExec("Autoexec")]
	public class CFournisseurProprietesDynamiquesListeEntite : IFournisseurProprieteDynamiquesSimplifie
	{

		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiquesListeEntite());
		}

		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
			if ( objet == null )
				return lstProps.ToArray();
			Type tp = objet.TypeAnalyse;
			if ( tp == null )
				return lstProps.ToArray();
            if (!C2iFactory.IsInit())
                return lstProps.ToArray();

			CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
			CListeObjetsDonnees listeEntites = new CListeObjetsDonnees(contexte, typeof(CListeEntites));
			listeEntites.Filtre = new CFiltreData(
				CListeEntites.c_champTypeElementSourceDeRecherche + "=@1",
				tp.ToString());
			foreach (CListeEntites liste in listeEntites)
			{
				CDefinitionProprieteDynamiqueListeEntites def = new CDefinitionProprieteDynamiqueListeEntites(liste);
				def.Rubrique = I.T("Lists|60");
				lstProps.Add ( def );
			}
			return lstProps.ToArray();
		}
	}
}
