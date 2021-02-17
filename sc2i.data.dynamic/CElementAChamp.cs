using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using sc2i.common;
using System.Text;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CElementAChamp.
	/// </summary>
	public abstract class CElementAChamp : CObjetDonneeAIdNumeriqueAuto, IObjetDonneeAChamps
	{

		private static Hashtable m_tableTypeElementToNomTableRelationToChamp = new Hashtable();

		//-------------------------------------------------------------------
#if PDA
		public CElementAChamp()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CElementAChamp( CContexteDonnee ctx )
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CElementAChamp( System.Data.DataRow row )
			:base(row)
		{	
		}

        //-------------------------------------------------------------------
        public abstract CRoleChampCustom RoleChampCustomAssocie{get;}

		/// <summary>
		/// Crée une nouvelle relation de type CRelationElementAChamp_ChampCustom
		/// </summary>
		/// <returns></returns>
		public abstract CRelationElementAChamp_ChampCustom GetNewRelationToChamp( );

		/// <summary>
		/// Retourne tous les definisseurs de champs configurant cet élément
		/// </summary>
		public abstract IDefinisseurChampCustom[] DefinisseursDeChamps{get;}

		/// <summary>
		/// Retourne toutes les relations à des valeurs de champs
		/// </summary>
		public abstract CListeObjetsDonnees RelationsChampsCustom{get;}

		public string GetNomTableRelationToChamps()
		{
			object val = m_tableTypeElementToNomTableRelationToChamp[GetType()];
			if ( val is string )
				return (string)val;
			CRelationElementAChamp_ChampCustom rel = GetNewRelationToChamp();
			//rel.CreateNewInCurrentContexte();
			val = rel.GetNomTable();
			//rel.CancelCreate();
			m_tableTypeElementToNomTableRelationToChamp[GetType()] = val;
			return (string)val;
		}																							
		
		
		public CListeObjetsDonnees GetRelationsToChamps ( int nIdChamp )
		{
			CListeObjetsDonnees liste = RelationsChampsCustom;
			liste.InterditLectureInDB = true;
			liste.Filtre = new CFiltreData(CChampCustom.c_champId + " = @1", nIdChamp );
			return liste;
		}


		//-------------------------------------------------------------------
		public virtual object GetValeurChamp(IVariableDynamique champ)
		{
            return GetValeurChamp(champ.IdVariable);
		}

		//-------------------------------------------------------------------
        public virtual object GetValeurChamp(string strIdChamp)
        {
            return CUtilElementAChamps.GetValeurChamp(this, strIdChamp);
        }

        //-------------------------------------------------------------------
        public virtual object GetValeurChamp(int idChamp)
        {
            return GetValeurChamp(idChamp, DataRowVersion.Default);
        }

        //-------------------------------------------------------------------
        public virtual object GetValeurChamp(int idChamp, DataRowVersion version)
		{
            return CUtilElementAChamps.GetValeurChamp(this, idChamp, version);
		}

		//-------------------------------------------------------------------
		public virtual CResultAErreur SetValeurChamp(IVariableDynamique champ, object valeur)
		{
            return SetValeurChamp(champ.IdVariable, valeur);
		}

		//-------------------------------------------------------------------
		public virtual CResultAErreur SetValeurChamp (string strIdChamp, object valeur )
		{
            return CUtilElementAChamps.SetValeurChamp(this, strIdChamp, valeur);
		}

        //-------------------------------------------------------------------
        public CResultAErreur SetValeurChamp(int idChamp, object valeur)
        {
            return CUtilElementAChamps.SetValeurChamp(this, idChamp, valeur);
        }

		//-------------------------------------------------------------------
		//Ne pas surcharger, surcharge plutot GetRelationsChampsHorsFormulaire !
		public virtual CChampCustom[] GetChampsHorsFormulaire()
		{
			ArrayList listeChamps = new ArrayList();
			foreach ( IRelationDefinisseurChamp_ChampCustom rel in GetRelationsChampsHorsFormulaire() )
			{
				listeChamps.Add ( rel.ChampCustom );
			}
			
			return (CChampCustom[])listeChamps.ToArray(typeof(CChampCustom));
		}

		//-------------------------------------------------------------------
		public virtual IRelationDefinisseurChamp_ChampCustom[] GetRelationsChampsHorsFormulaire()
		{
			ArrayList listeRelations = new ArrayList();
			Hashtable tableChamps = new Hashtable();
			
			foreach(IDefinisseurChampCustom definisseur in DefinisseursDeChamps)
			{
				foreach(IRelationDefinisseurChamp_ChampCustom relChamp in definisseur.RelationsChampsCustomDefinis)
				{
					CChampCustom champ = relChamp.ChampCustom;
					if (tableChamps[champ] == null)
					{
						tableChamps[champ] = champ;
						listeRelations.Add( relChamp );
					}
				}
			}
			listeRelations.Sort ( );

			return (IRelationDefinisseurChamp_ChampCustom[])listeRelations.ToArray(typeof(IRelationDefinisseurChamp_ChampCustom));
		}

		//-------------------------------------------------------------------
		private class CFormulaireComparer : IComparer
		{
			#region Membres de IComparer

			public int Compare(object x, object y)
			{
				if ( x is CFormulaire && y is CFormulaire )
				{
					int n1, n2;
					n1 = ((CFormulaire)x).NumeroOrdre;
					n2 = ((CFormulaire)y).NumeroOrdre;
					return n1.CompareTo(n2);
				}
				return 0;
			}

			#endregion

		}


		//-------------------------------------------------------------------
		public virtual CFormulaire[] GetFormulaires()
		{
            //Stef 11/10/2010 : replacé par CUTilElementAChamp
            return CUtilElementAChamps.GetFormulaires(this);
			/*ArrayList listeFormulaires = new ArrayList();
			Hashtable tableFormulaires = new Hashtable();
			
			foreach(IDefinisseurChampCustom definisseur in DefinisseursDeChamps)
			{
				foreach(IRelationDefinisseurChamp_Formulaire rel in definisseur.RelationsFormulaires)
				{
					CFormulaire formulaire = rel.Formulaire;
					if (tableFormulaires[formulaire] == null)
					{
						tableFormulaires[formulaire] = formulaire;
						listeFormulaires.Add( formulaire );
					}
				}
			}
			listeFormulaires.Sort ( new CFormulaireComparer() );

			return (CFormulaire[])listeFormulaires.ToArray(typeof(CFormulaire));*/
		}

		public CChampCustom[] TousLesChamps
		{
			get
			{
				Hashtable table = new Hashtable();
				foreach ( CChampCustom champ in GetChampsHorsFormulaire() )
					table[champ] = true;
				foreach ( CFormulaire formulaire in GetFormulaires() )
					foreach ( CRelationFormulaireChampCustom rel in formulaire.RelationsChamps )
						table[rel.Champ] = true;
				CChampCustom[] champs = new CChampCustom[table.Count];
				int nChamp = 0;
				foreach ( CChampCustom champ in table.Keys )
				{
					champs[nChamp] = champ;
					nChamp++;
				}
				return champs;
			}
		}

		

		
	}
}
