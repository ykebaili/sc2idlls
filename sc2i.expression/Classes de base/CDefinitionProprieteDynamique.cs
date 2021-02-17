using System;
using System.Collections;

using sc2i.common;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

namespace sc2i.expression
{
	

	/// <summary>
	/// Description résumée de CDefinitionProprieteDynamique.
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamique : IDefinitionChampExpression, I2iSerializable, IComparable
	{
		public const char c_strCaractereStartCleType = '#';
		public const char c_strCaractereEndCleType = '|';
		private const string c_strCleType = "BS";

		protected string m_strNomConvivial = "";
		protected string m_strNomPropriete = "" ;
		protected CTypeResultatExpression m_typeDonnee;
		protected bool m_bHasSubProprietes = false;
		protected bool m_bIsReadOnly = false;
		
		//Rubrique pour le classement du champ. Non sérialisée !!
		protected string m_strRubrique = "";

		public CDefinitionProprieteDynamique()
		{			
		}

		public CDefinitionProprieteDynamique
			(
			string strNomConvivial, 
			string strNomPropriete, 
			CTypeResultatExpression type, 
			bool bHasSubProprietes,
			bool bIsReadOnly
			)
		{
			m_strNomConvivial = strNomConvivial.Trim()==""?strNomPropriete:strNomConvivial;
            string strCle = "";
            string strPropSansCle = "";
            if (DecomposeNomProprieteUnique(strNomPropriete, ref strCle, ref strPropSansCle))
            {
                //Si la propriété contient déjà un type, et que ce n'est pas le même
                //que le type de moi même, et qu'on n'appelle pas directement
                //le constructeur de CDefintionProrieteDynamique mais celui d'une classe
                //dérivée (dans ce cas CLEType != BS). En effet, on peut appeller un 
                //new CDefinitionProprieteDynamique directement en lui passant le CLETYPE,
                //Mais qu'il ne s'agisse pas de BS. Dans ce cas, si pas de vérif, on créera
                //une propriété avec #BS|#PP|Libelle par exemple si on a passé #PP comme nom
                //de propriété
                if (strCle != CleType && CleType != c_strCleType)
                    strNomPropriete = c_strCaractereStartCleType + CleType + c_strCaractereEndCleType + strNomPropriete;
            }
            else
                strNomPropriete = c_strCaractereStartCleType + CleType + c_strCaractereEndCleType + strNomPropriete;

/*			if (strNomPropriete.Length > 0 && strNomPropriete[0] != c_strCaractereStartCleType)
				strNomPropriete = c_strCaractereStartCleType + CleType + c_strCaractereEndCleType+strNomPropriete;*/
			m_strNomPropriete = strNomPropriete;
			m_typeDonnee = type;
			m_bHasSubProprietes = bHasSubProprietes;
			m_bIsReadOnly = bIsReadOnly;
			m_strRubrique = "";
			
		}

		public CDefinitionProprieteDynamique
			(
			string strNomConvivial, 
			string strNomPropriete, 
			CTypeResultatExpression type, 
			bool bHasSubProprietes,
			bool bIsReadOnly,
			string strRubrique
			)
		{
			m_strNomConvivial = strNomConvivial.Trim()==""?strNomPropriete:strNomConvivial;
            string strCle = "";
            string strPropSansCle = "";
            if (DecomposeNomProprieteUnique(strNomPropriete, ref strCle, ref strPropSansCle))
            {
                //Si la propriété contient déjà un type, et que ce n'est pas le même
                //que le type de moi même, et qu'on n'appelle pas directement
                //le constructeur de CDefintionProrieteDynamique mais celui d'une classe
                //dérivée (dans ce cas CLEType != BS). En effet, on peut appeller un 
                //new CDefinitionProprieteDynamique directement en lui passant le CLETYPE,
                //Mais qu'il ne s'agisse pas de BS. Dans ce cas, si pas de vérif, on créera
                //une propriété avec #BS|#PP|Libelle par exemple si on a passé #PP comme nom
                //de propriété
                if (strCle != CleType && CleType != c_strCleType)
                    strNomPropriete = c_strCaractereStartCleType + CleType + c_strCaractereEndCleType + strNomPropriete;
            }
            else
                strNomPropriete = c_strCaractereStartCleType + CleType + c_strCaractereEndCleType + strNomPropriete;
            m_strNomPropriete = strNomPropriete;
			m_typeDonnee = type;
			m_bHasSubProprietes = bHasSubProprietes;
			m_bIsReadOnly = bIsReadOnly;
			m_strRubrique = strRubrique==null?"":strRubrique;
			
		}

		//-----------------------------------------------
		public static void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueDotNet));
		}

		//---------------------------------------------------------
		/// <summary>
		/// Décompose une propriété (#CLE|NomPropriete) en
		/// clé et nom de propriété.
		/// </summary>
		/// <param name="strProprieteAvecCle"></param>
		/// <param name="strCle"></param>
		/// <param name="strPropriete"></param>
		/// <returns></returns>
		public static bool DecomposeNomProprieteUnique ( string strProprieteAvecCle, ref string strCle, ref string strPropriete )
		{
			if ( strProprieteAvecCle.Length < 1 )
				return false;
			if (strProprieteAvecCle[0] != c_strCaractereStartCleType)
				return false;
			int nPos = strProprieteAvecCle.IndexOf(c_strCaractereEndCleType);
			if (nPos <= 0)
				return false;
			strCle = strProprieteAvecCle.Substring(1, nPos - 1);
			strPropriete = strProprieteAvecCle.Substring(strCle.Length + 2);
			return true;
		}

		//---------------------------------------------------------
		public virtual string CleType
		{
			get
			{
				return c_strCleType;
			}
		}

		public CDefinitionProprieteDynamique Clone()
		{
			CDefinitionProprieteDynamique newDef =
				(CDefinitionProprieteDynamique) Activator.CreateInstance( this.GetType() );
			this.CopyTo(newDef);
			return newDef;
		}

		public virtual void CopyTo(CDefinitionProprieteDynamique def)
		{
			def.m_strNomConvivial = m_strNomConvivial;
			def.m_strNomPropriete = m_strNomPropriete;
			def.m_typeDonnee = m_typeDonnee;
			def.m_bHasSubProprietes = m_bHasSubProprietes;
			def.m_bIsReadOnly = m_bIsReadOnly;
			def.m_strRubrique = m_strRubrique;
		}

		/// ////////////////////////////////////////////
		public string Nom
		{
			get
			{
				return m_strNomConvivial;
			}
		}

        //-----------------------------------------------
        public CDefinitionProprieteDynamique GetDefinitionInverse ( Type typePortantLaPropriete )
        {
            return CFournisseurProprieteDynamiqueInverse.GetProprieteInverse ( typePortantLaPropriete, this );
        }

		/// ////////////////////////////////////////////
		public void ChangeNom ( string strNewNom )
		{
			m_strNomConvivial = strNewNom;
		}

		/// ////////////////////////////////////////////
		public string NomPropriete
		{
			get
			{
				return m_strNomPropriete;
			}
		}

        /// ////////////////////////////////////////////
        protected void SetNomPropriete ( string strNomPropriete )
        {
            m_strNomPropriete = strNomPropriete;
        }

		/// ////////////////////////////////////////////
		public string NomProprieteSansCleTypeChamp
		{
			get
			{
				string strCle = "";
				string strProp = "";
				if (DecomposeNomProprieteUnique(NomPropriete, ref strCle, ref strProp))
					return strProp;
				return NomPropriete;
			}
		}

        protected void SetNomProprieteSansCleTypeChamp ( string strNomPropriete )
        {
            m_strNomPropriete = c_strCaractereStartCleType + CleType + c_strCaractereEndCleType + strNomPropriete;
        }

		/// ////////////////////////////////////////////
		public bool HasSubProperties
		{
			get
			{
				return m_bHasSubProprietes;
			}
			set
			{
				m_bHasSubProprietes = value;
			}
		}

		/// ////////////////////////////////////////////
		public bool IsReadOnly
		{
			get
			{
				return m_bIsReadOnly;
			}
		}

		/// ////////////////////////////////////////////
		///Insere le nom du parent devant le champ
		///LE type de l'élement peut devenir multiple si le type du parent est un table
		public virtual void InsereParent ( CDefinitionProprieteDynamique parent )
		{
			if (parent==null)
				return;
			if ( parent.NomPropriete != "" )
			{
				m_strNomPropriete = parent.NomPropriete+"."+m_strNomPropriete;
				m_strNomConvivial = m_strNomConvivial + " " + parent.Nom;
			}
			if ( parent.TypeDonnee.IsArrayOfTypeNatif && !m_typeDonnee.IsArrayOfTypeNatif )
				m_typeDonnee = m_typeDonnee.GetTypeArray();
		}

		/// ////////////////////////////////////////////
		public CTypeResultatExpression TypeDonnee
		{
			get
			{
				return m_typeDonnee;
			}
		}

		/// ////////////////////////////////////////////
		public string Rubrique
		{
			get
			{
				return m_strRubrique;
			}
			set
			{
				m_strRubrique = value;
			}
		}

		/// ////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 3;
		}

		/// ////////////////////////////////////////////
		public virtual CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strNomConvivial );
			serializer.TraiteString ( ref m_strNomPropriete );
            if (m_typeDonnee == null)//correction : il arrive que le type soit null à la savuegarde
                m_typeDonnee = new CTypeResultatExpression();
			m_typeDonnee.Serialize ( serializer );
			if ( nVersion > 2 )
				serializer.TraiteBool ( ref m_bHasSubProprietes );

            //Si le nom de propriete commence par #TS|., supprime le #TS.
            if (serializer.Mode == ModeSerialisation.Lecture &&
                m_strNomPropriete.StartsWith("#TS|."))
                m_strNomPropriete = m_strNomPropriete.Substring(5);
			return result;
		}


		/// ////////////////////////////////////////////
		public int CompareTo ( object obj )
		{
			if ( !(obj is CDefinitionProprieteDynamique))
				return -1;
			string strNom = ((CDefinitionProprieteDynamique)obj).Nom;
			return Nom.CompareTo(strNom);
		}

		/// ////////////////////////////////////////////
		public override bool Equals(object obj)
		{
			if ( ! (obj is CDefinitionProprieteDynamique ) )
				return false;
			CDefinitionProprieteDynamique lautre = (CDefinitionProprieteDynamique)obj;
			return lautre.NomPropriete == NomPropriete && lautre.Nom == Nom && lautre.TypeDonnee.Equals(TypeDonnee);
		}

		/// ////////////////////////////////////////////
		public override int GetHashCode()
		{
			return NomPropriete.GetHashCode();
		}

		/// ////////////////////////////////////////////
		public override string ToString()
		{
			if ( Nom.Trim() == "" )
				return NomPropriete;
			return Nom;
		}

		/// ////////////////////////////////////////////
		/// <summary>
		/// retourne un nom de champ (ou chemin) compatible avec
		/// la syntaxe d'un CComposantFiltreChamp
		/// </summary>
		public virtual string NomChampCompatibleCComposantFiltreChamp
		{
			get
			{
				return NomPropriete;
			}
		}

		/// ////////////////////////////////////////////
		public virtual ERestriction GetRestrictionAAppliquer(CRestrictionUtilisateurSurType rest)
		{
			return rest.GetRestriction(NomProprieteSansCleTypeChamp);
		}

		///////////////////////////////////////////////
		/// <summary>
		/// Retourne l'objet permettant d'explorer les sous propriétés de cet
		/// objet
		/// </summary>
		public virtual CObjetPourSousProprietes GetObjetPourAnalyseSousProprietes()
		{
			if ( m_typeDonnee != null )
				return new CObjetPourSousProprietes(m_typeDonnee);
			return null;
		}

       
	}

}
