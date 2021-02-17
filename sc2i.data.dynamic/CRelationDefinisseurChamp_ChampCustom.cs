using System;
using System.Data;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.dynamic
{

//---------------------------------------------------
	//---------------------------------------------------
	//---------------------------------------------------
	//---------------------------------------------------
	public interface IRelationDefinisseurChamp_ChampCustom : IComparable
	{
		IDefinisseurChampCustom Definisseur{get;set;}
		CChampCustom ChampCustom{get;set;}
		int Ordre{get;set;}
	}

	//---------------------------------------------------
	//---------------------------------------------------
	//---------------------------------------------------
	//---------------------------------------------------
	//---------------------------------------------------
	public class CRelationDefinisseurChampCustomStatic : IRelationDefinisseurChamp_ChampCustom
	{
		IDefinisseurChampCustom m_definisseur;
		CChampCustom m_champ;
		int m_nOrdre = 0;

		public CRelationDefinisseurChampCustomStatic ( IDefinisseurChampCustom definisseur, CChampCustom champ, int nOrdre )
		{
			m_definisseur = definisseur;
			m_champ = champ;
			m_nOrdre = nOrdre;
		}


		public CChampCustom ChampCustom
		{
			get
			{
				return m_champ;
			}
			set
			{
				m_champ = value;
			}
		}

		public IDefinisseurChampCustom Definisseur
		{
			get
			{
				return m_definisseur;
			}
			set
			{
				m_definisseur = value;
			}
		}

		public int Ordre
		{
			get
			{
				return m_nOrdre;
			}
			set
			{
				m_nOrdre = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public int CompareTo ( object obj )
		{
			if ( !(obj is IRelationDefinisseurChamp_ChampCustom ) )
				return -1;
			return Ordre.CompareTo (((IRelationDefinisseurChamp_ChampCustom)obj).Ordre );
		}
	}
	/// <summary>
	/// Description résumée de CRelationDefinisseurChamp_ChampCustom.
	/// </summary>
	public abstract class CRelationDefinisseurChamp_ChampCustom : 
		CObjetDonneeAIdNumeriqueAuto, 
		IRelationDefinisseurChamp_ChampCustom,
		IObjetALectureTableComplete
	{
		public const string c_champOrdre = "REL_ORDER_FIELD";
		/// ///////////////////////////////////////////////////////////////////////
#if PDA
		public CRelationDefinisseurChamp_ChampCustom()
			:base()
		{
		}
#endif
		/// ///////////////////////////////////////////////////////////////////////
		public CRelationDefinisseurChamp_ChampCustom( CContexteDonnee contexte )
			:base(contexte)
		{
		}

		/// ///////////////////////////////////////////////////////////////////////
		public CRelationDefinisseurChamp_ChampCustom ( DataRow row )
			:base(row)
		{
		}

		//-------------------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
			Ordre = 0;
		}
		//-------------------------------------------------------------------
		public override string DescriptionElement
		{
			get
			{
				return I.T("Relation between @1 and @2|100",Definisseur.DescriptionElement, ChampCustom.DescriptionElement);
			}
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champOrdre};
		}

		/// ///////////////////////////////////////////////////////
		[Relation(
            CChampCustom.c_nomTable,
            CChampCustom.c_champId,
            CChampCustom.
            c_champId,
            true,
            false,
            true)]
		[
		DynamicField("Champ Custom")
		]
		public CChampCustom ChampCustom
		{
			get
			{
				return (CChampCustom)GetParent ( CChampCustom.c_champId, typeof(CChampCustom));
			}
			set
			{
				SetParent ( CChampCustom.c_champId, value );
			}
		}

		/// ///////////////////////////////////////////////////////
		public abstract IDefinisseurChampCustom Definisseur{get;set;}

		/// ///////////////////////////////////////////////////////
		[TableFieldProperty(c_champOrdre)]
		public int Ordre
		{
			get
			{
				return (int)Row[c_champOrdre];
			}
			set
			{
				Row[c_champOrdre] = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public int CompareTo ( object obj )
		{
			if ( !(obj is IRelationDefinisseurChamp_ChampCustom ) )
				return -1;
			return Ordre.CompareTo (((IRelationDefinisseurChamp_ChampCustom)obj).Ordre );
		}


	}
}
