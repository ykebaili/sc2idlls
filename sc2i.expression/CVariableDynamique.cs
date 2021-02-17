using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;
using sc2i.common.unites;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de CVariableDynamique.
	/// </summary>
    [ReplaceClass("sc2i.data.dynamic.CVariableDynamique")]
	public abstract class CVariableDynamique : 
        MarshalByRefObject, 
        I2iSerializable, 
        IVariableDynamique, 
        IComparable
	{
        private string m_strId = "";
		private string m_strNomVariable="";
		private string m_strDescription = "";
        private string m_strRubrique = "";
        private string m_strClasseUniteId = "";
        private string m_strFormatUnite = "";

		private IElementAVariablesDynamiquesBase m_elementAVariables = null;

		/// ///////////////////////////////////////////
		public CVariableDynamique( )
		{
            m_strId = "";
            m_strRubrique = I.T("Others|56");

		}

		/// ///////////////////////////////////////////
		public abstract string LibelleType{get;}

		/// ///////////////////////////////////////////
		public CVariableDynamique( IElementAVariablesDynamiquesBase elementAVariables )
		{
			m_strId = elementAVariables.GetNewIdForVariable();
			m_elementAVariables = elementAVariables;
		}

		/// ///////////////////////////////////////////
		public string IdVariable
		{
			get
			{
				return m_strId;
			}
			set
			{
				m_strId = value;
			}
		}

		/// ///////////////////////////////////////////
		public IElementAVariablesDynamiquesBase ElementAVariables
		{   
			get
			{
				return m_elementAVariables;
			}
		}

		/// ///////////////////////////////////////////
		public string Nom
		{
			get
			{
				return m_strNomVariable;
			}
			set
			{
				m_strNomVariable = value;
			}
		}

		/// ///////////////////////////////////////////
		public string Description
		{
			get
			{
				return m_strDescription;
			}
			set
			{
				m_strDescription = value;
			}
		}

        /// ///////////////////////////////////////////
        public IClasseUnite ClasseUnite
        {
            get
            {
                if (m_strClasseUniteId.Length > 0)
                    return CGestionnaireUnites.GetClasse(m_strClasseUniteId);
                return null;
            }
            set
            {
                if (value == null)
                    m_strClasseUniteId = "";
                else
                    m_strClasseUniteId = value.GlobalId;
            }
        }

        /// ///////////////////////////////////////////
        public string FormatAffichageUnite
        {
            get
            {
                return m_strFormatUnite;
            }
            set
            {
                m_strFormatUnite = value;
            }
        }


		
		/// ///////////////////////////////////////////
		public abstract CTypeResultatExpression TypeDonnee{get;}

		/// ///////////////////////////////////////////
		///Indique si l'utilisateur choisis parmis des valeurs ou non (combo !)
		public abstract bool IsChoixParmis();

		/// ///////////////////////////////////////////
		/// Indique si la variable correspond à un choix d'utilisateur
		public abstract bool IsChoixUtilisateur();

		public virtual CResultAErreur VerifieValeur(object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		/// ///////////////////////////////////////////
		public abstract IList Valeurs{get;}

		/// ///////////////////////////////////////////
		private int GetNumVersion()
		{
            //2 : ajout de la rubrique
            //3 : ajout de l'unité
            return 4; // Passage de int Id à string IsVariable
		}
		
		/// ///////////////////////////////////////////
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            //TESTDBKEYTODO
            if (nVersion < 4)
            {
                if (serializer.Mode == ModeSerialisation.Lecture)
                {
                    int nIdOld = -1;
                    serializer.TraiteInt(ref nIdOld);
                    m_strId = nIdOld.ToString();
                }
            }
            else
                serializer.TraiteString(ref m_strId);

            serializer.TraiteString(ref m_strNomVariable);
            serializer.TraiteString(ref m_strDescription);
            if (m_elementAVariables == null)
                m_elementAVariables = (IElementAVariablesDynamiquesBase)serializer.GetObjetAttache(typeof(IElementAVariablesDynamiquesBase));
            if (nVersion >= 2)
                serializer.TraiteString(ref m_strRubrique);
            if (nVersion >= 3)
            {
                serializer.TraiteString(ref m_strClasseUniteId);
                serializer.TraiteString(ref m_strFormatUnite);
            }
            return result;
        }

		/// ///////////////////////////////////////////
		public override string ToString()
		{
			return Nom;
		}

		/// ///////////////////////////////////////////
		public virtual int CompareTo ( object obj )
		{
			if ( !(obj is CVariableDynamique ) )
				return -1;
			return Nom.CompareTo ( ((CVariableDynamique)obj).Nom );
		}

        /// ///////////////////////////////////////////
        public virtual string Rubrique
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
	}
}
