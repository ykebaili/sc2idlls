using System;
using System.Collections;

using sc2i.common;
using sc2i.data;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// PErmet de stocker l'arbre des relations nécéssaires au filtre
	/// </summary>
	

	/// <summary>
	/// Description résumée de CFiltreDataAvance.
	/// </summary>
	[Serializable]
	public class CFiltreDataAvance : CFiltreData
	{
		private string m_strTablePrincipale;

		private CComposantFiltre m_composantPrincipal = null;

        private List<CComposantFiltreChamp> m_listeChampsAAjouterAArbreTable = new List<CComposantFiltreChamp>();

		/// ////////////////////////////////////////////////////////////////
		public CFiltreDataAvance()
		{
		}

		private static string RemplaceIsNullForAvance(Match mtch)
		{
			Group p1 = mtch.Groups["P1"];
			Group op = mtch.Groups["OP"];
			string strStart = mtch.Value.Substring(0, p1.Index - mtch.Index);
			string strOp = mtch.Value.Substring(op.Index - mtch.Index, op.Length);
			string strEnd = mtch.Value.Substring(op.Index - mtch.Index + op.Length);
			strOp = strOp.ToUpper() == "IS NULL" ? "HasNo" : "Has";
			string strFinal = strStart + strOp + "(" + p1.Value + ")" + strEnd;
			return strFinal;

		}

        /// ////////////////////////////////////////////////////////////////
        ///Permet d'inclure dans le get relations, des champs à ajouter
        ///à l'arbre de tables, par exemple lorsque le select d'une requête
        ///a besoin de champs de sous tables
        public CComposantFiltreChamp[] ChampsAAjouterAArbreTable
        {
            get
            {
                return m_listeChampsAAjouterAArbreTable.ToArray();
            }
            set
            {
                m_listeChampsAAjouterAArbreTable.Clear();
                if (value != null)
                {
                    foreach (CComposantFiltreChamp champ in m_listeChampsAAjouterAArbreTable)
                        m_listeChampsAAjouterAArbreTable.Add(champ);
                }
            }
        }

        /// ////////////////////////////////////////////////////////////////
        public void AddChampAAjouterAArbreTable(CComposantFiltreChamp champ)
        {
            m_listeChampsAAjouterAArbreTable.Add(champ);
        }

		/// ////////////////////////////////////////////////////////////////
		public static CFiltreDataAvance ConvertFiltreToFiltreAvance(string strTable, CFiltreData filtre)
		{
			if (filtre is CFiltreDataAvance)
				return (CFiltreDataAvance)filtre.GetClone();
			string strFiltre = filtre.Filtre;
			if (!filtre.HasFiltre)
			{
				return new CFiltreDataAvance(strTable, "");
			}
			//Remplace is null et is not null par has et hasno
            string strUpper = strFiltre.ToUpper();
            if (strUpper.Contains("IS NULL") || strUpper.Contains("IS NOT NULL"))
            {
                Regex expReg = new Regex("[( ]*(?<P1>[^ ]+)[) ]*(?<OP>is null|is not null)", RegexOptions.IgnoreCase);
                strFiltre = expReg.Replace(strFiltre, new MatchEvaluator(RemplaceIsNullForAvance));
            }
			CResultAErreur result = CAnalyseurSyntaxiqueFiltre.AnalyseFormule(strFiltre, strTable);
			if (!result)
			{
				result.EmpileErreur(I.T("Error while analyzing filter @1|134", strFiltre));
				throw new CExceptionErreur(result.Erreur);
			}
			CFiltreDataAvance retour = new CFiltreDataAvance(strTable, (CComposantFiltre)result.Data);
			foreach (object parametre in filtre.Parametres)
				retour.Parametres.Add(parametre);
			return retour;
		}

		/// ////////////////////////////////////////////////////////////////
		public CFiltreDataAvance( string strTablePrincipale , string strFiltre, params object[] parametres)
			:base ( strFiltre, parametres )
		{
			m_strTablePrincipale = strTablePrincipale;	
		}

        public CFiltreDataAvance(string strTablePrincipale, CComposantFiltre composantPrincipal, params object[] parametres)
            : base(composantPrincipal != null ? composantPrincipal.GetString() : "", parametres)
        {
            m_composantPrincipal = composantPrincipal;
            m_strTablePrincipale = strTablePrincipale;
        }
            

		//////////////////////////////////////////////////
		public override string Filtre
		{
			get
			{
				return base.Filtre;
			}
			set
			{
				if ( value !=  base.Filtre )
				{
					base.Filtre = value;
					m_composantPrincipal = null;
				}
			}
		}
		//////////////////////////////////////////////////
		protected void CopyTo ( CFiltreDataAvance filtre )
		{
			base.CopyTo ( filtre );
			filtre.m_strTablePrincipale = m_strTablePrincipale;
            if ( m_composantPrincipal != null )
                m_composantPrincipal = (CComposantFiltre)CCloner2iSerializable.Clone ( m_composantPrincipal );
		}

		//////////////////////////////////////////////////
		public override CFiltreData GetClone()
		{
			CFiltreDataAvance filtre = new CFiltreDataAvance(m_strTablePrincipale,"");
			CopyTo ( filtre );
			return filtre;
		}

		/// ////////////////////////////////////////////////////////////////
		public string TablePrincipale
		{
			get
			{
				return m_strTablePrincipale;
			}
			set
			{
				m_strTablePrincipale = value;
			
			}
		}

		//////////////////////////////////////////////////
		public CComposantFiltre ComposantPrincipal
		{
			get
			{
				if ( m_composantPrincipal == null )
					CalculeComposantPrincipal();
				return m_composantPrincipal;
			}
			set
			{
				m_composantPrincipal = value;
				base.Filtre = m_composantPrincipal.GetString();
			}
		}

		/// ////////////////////////////////////////////////////////////////
		protected CResultAErreur CalculeComposantPrincipal()
		{
			m_composantPrincipal = null;
			CResultAErreur result = CAnalyseurSyntaxiqueFiltre.AnalyseFormule(Filtre, m_strTablePrincipale);
			if ( !result )
				return result;
			m_composantPrincipal = (CComposantFiltre)result.Data;
			return result;
		}


		/// ////////////////////////////////////////////////////////////////
		/// Le data du result contient un CInfoRelationComposantFiltre[]
		public CResultAErreur GetArbreTables()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_composantPrincipal == null )
				result = CalculeComposantPrincipal();
			if ( !result )
				return result;
			CArbreTableParente arbre = new CArbreTableParente ( m_strTablePrincipale );
			result = GetRelations ( m_composantPrincipal, arbre, false );
            foreach (CComposantFiltreChamp champ in ChampsAAjouterAArbreTable)
                GetRelations(champ, arbre, true);
			result.Data = arbre;
			return result;
		}		

		//// ////////////////////////////////////////////////////////////////
		private CResultAErreur GetRelations ( CComposantFiltre composant, CArbreTable arbre, bool bFillesLeftOuter )
		{
			CResultAErreur result = CResultAErreur.True;
			bool bIsComposantLocalLeftOuter = false;
			//Le ou entraine des left outers
			if ( composant is CComposantFiltreOperateur )
			{
				CComposantFiltreOperateur op = (CComposantFiltreOperateur)composant;
				if ( op.Operateur.Id == CComposantFiltreOperateur.c_IdOperateurOu )
					bIsComposantLocalLeftOuter = true;
			}
			if ( composant is CComposantFiltreHasNo )
				bIsComposantLocalLeftOuter = true;
			if ( composant is CComposantFiltreChamp )
			{
				CComposantFiltreChamp champ = (CComposantFiltreChamp)composant;
				result = IntegreRelationsChamps ( arbre, champ, bIsComposantLocalLeftOuter || bFillesLeftOuter );
				if ( !result )
					return result;
			}			
			foreach ( CComposantFiltre composantFils in composant.Parametres )
			{
				result = GetRelations ( composantFils, arbre, bIsComposantLocalLeftOuter || bFillesLeftOuter );
				if ( !result )
					return result;
			}
			return result;
		}

		/// ////////////////////////////////////////////////////////////////
        private CResultAErreur IntegreRelationsChamps(CArbreTable arbre, CComposantFiltreChamp champ, bool bIsLeftOuter)
        {
            CResultAErreur result = CResultAErreur.True;
            CArbreTable arbreEnCours = arbre;
            foreach (CInfoRelationComposantFiltre relation in champ.Relations)
            {
                arbreEnCours = arbreEnCours.IntegreRelation(relation, bIsLeftOuter, champ.IdChampCustom );
                if (arbreEnCours == null)
                {
                    result.EmpileErreur(I.T("Error while integrating relation @1|107", relation.RelationKey));
                    return result;
                }
            }
            return result;
        }

		/// ////////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strTablePrincipale );

			I2iSerializable obj = m_composantPrincipal;
			result = serializer.TraiteObject ( ref obj );
			m_composantPrincipal = (CComposantFiltre)obj;
			return result;
		}

		/// ////////////////////////////////////////////////////////////////
		public void ChangeTableDeBase ( string strTable, string strRelationPourAllerSurLaNouvelleTable )
		{
			CComposantFiltre composantPrincipal = ComposantPrincipal;
			m_strTablePrincipale = strTable;
			foreach (CComposantFiltreChamp champ in composantPrincipal.ExtractExpressionsType(typeof(CComposantFiltreChamp)))
			{
				champ.EmpileRelation ( strTable, strRelationPourAllerSurLaNouvelleTable );
			}
			
			base.Filtre = ComposantPrincipal.GetString();
			CalculeComposantPrincipal();
		}


        //-------------------------------------------------------------------------
        public override void RenumerotteParameters(int nNumDebut)
        {
            ComposantPrincipal.RenumerotteParameters(nNumDebut);
            m_strFiltre = ComposantPrincipal.GetString();
        }

	}
}
