using System;
using System.Data;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.dynamic
{
	public interface IRelationDefinisseurChamp_Formulaire
	{
		IDefinisseurChampCustom Definisseur{get;set;}
		CFormulaire Formulaire{get;set;}
	}

	//---------------------------------------------------
	//---------------------------------------------------
	//---------------------------------------------------
	public class CRelationDefinisseurChamp_FormulaireStatic : 
		IRelationDefinisseurChamp_Formulaire,
		IObjetALectureTableComplete
	{
		IDefinisseurChampCustom m_definisseur;
		CFormulaire m_formulaire;

		public CRelationDefinisseurChamp_FormulaireStatic ( IDefinisseurChampCustom definisseur, CFormulaire formulaire )
		{
			m_definisseur = definisseur;
			m_formulaire = formulaire;
		}


		public CFormulaire Formulaire
		{
			get
			{
				return m_formulaire;
			}
			set
			{
				m_formulaire = value;
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
	}
	/// <summary>
	/// Description résumée de CRelationDefinisseurChamp_Formulaire.
	/// </summary>
	public abstract class CRelationDefinisseurChamp_Formulaire : CObjetDonneeAIdNumeriqueAuto,
		IRelationDefinisseurChamp_Formulaire
	{
		/// ///////////////////////////////////////////////////////////////////////
#if PDA
		public CRelationDefinisseurChamp_Formulaire()
			:base()
		{
		}
#endif
		/// ///////////////////////////////////////////////////////////////////////
		public CRelationDefinisseurChamp_Formulaire( CContexteDonnee contexte )
			:base(contexte)
		{
		}

		/// ///////////////////////////////////////////////////////////////////////
		public CRelationDefinisseurChamp_Formulaire ( DataRow row )
			:base(row)
		{
		}

		//-------------------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
		}
		//-------------------------------------------------------------------
		public override string DescriptionElement
		{
			get
			{
				return I.T("Relation between @1 and @2|100", Definisseur.DescriptionElement, Formulaire.DescriptionElement);
			}
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{GetChampId()};
		}

		/// ///////////////////////////////////////////////////////
		[Relation(CFormulaire.c_nomTable,CFormulaire.c_champId,CFormulaire.c_champId,true,false,true)]
		[DynamicField("Form")]
		public CFormulaire Formulaire
		{
			get
			{
				CFormulaire leFormulaire = new CFormulaire(ContexteDonnee);
				leFormulaire.Id = (int) Row[CFormulaire.c_champId];
				return leFormulaire;
			}
			set
			{
				AssureExiste(value);
				Row[CFormulaire.c_champId] = value.Id;
			}
		}

		/// ///////////////////////////////////////////////////////
		public abstract IDefinisseurChampCustom Definisseur{get;set;}


	}
}
