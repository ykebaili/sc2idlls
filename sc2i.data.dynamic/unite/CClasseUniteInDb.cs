using System;
using System.Collections;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.expression;
using sc2i.common.recherche;
using sc2i.common.unites;

namespace sc2i.data.dynamic.unite
{
	/// <summary>
	/// Une classe d'unité représente un type de mesure
    /// représenté par une unité.
    /// Par exemple, "Distance" est une classe d'unité.
	/// </summary>
    /// <remarks>
    /// Le système définit par défaut 4 classes d'unités<BR></BR>
    /// <LI>Distance</LI>
    /// <LI>Temps</LI>
    /// <LI>Poids</LI>
    /// <LI>Volume</LI>
    /// L'application combine les unités système et les classes d'unités système avec les unités
    /// créées par les utilisateurs de l'application.<BR></BR>
    /// Il convient de ne jamais modifier les identifiants globaux des classes d'unités
    /// une fois que celles-ci sont utilisées.
    /// </remarks>
	[ObjetServeurURI("CClasseUniteInDbServeur")]
	[Table(CClasseUniteInDb.c_nomTable,CClasseUniteInDb.c_champId,true)]
	[FullTableSync]
	[DynamicClass("Unity class")]
	public class CClasseUniteInDb : CObjetDonneeAIdNumeriqueAuto, IClasseUnite, IObjetSansVersion
	{
		#region Déclaration des constantes
		public const string c_nomTable = "UNIT_CLASS";
		public const string c_champId = "UNCL_ID";
		public const string c_champLibelle = "UNCL_LABEL";
		public const string c_champGlobalId = "UNCL_GLOBAL_ID";
        public const string c_champUniteBase = "UNCL_BASE_UNIT";
		#endregion

		/// ///////////////////////////////////////////////////////
		public CClasseUniteInDb( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// ///////////////////////////////////////////////////////
		public CClasseUniteInDb ( System.Data.DataRow row )
			:base(row)
		{
		}
		/// ///////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			
		}

		/// ///////////////////////////////////////////////////////
		public override string GetChampId()
		{
			return c_champId;
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[] {c_champLibelle};
		}


		/// ///////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Unity class @1|20053", Libelle);
			}
		}

		////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return c_nomTable;
		}


        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Libellé de la classe d'unité.
        /// </summary>
        [TableFieldProperty(c_champLibelle, 255)]
        [DynamicField("Label")]
        public string Libelle
        {
            get
            {
                return Row.Get<string>(c_champLibelle);
            }
            set
            {
                Row[c_champLibelle] = value;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Indique quelle est l'unité de base pour cette classe d'unité
        /// </summary>
        [TableFieldProperty(c_champUniteBase, 255)]
        [DynamicField("Base unit")]
        public string UniteBase
        {
            get{
                return Row.Get<string>(c_champUniteBase);
            }
            set{
                Row[c_champUniteBase] = value;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Id servant d'identifiant global pour la gestion des unités
        /// </summary>
        [TableFieldProperty(c_champGlobalId, 255)]
        [DynamicField("Global id")]
        public string GlobalId
        {
            get
            {
                return Row.Get<string>(c_champGlobalId);
            }
            set
            {
                Row[c_champGlobalId] = value.ToUpper();
            }
        }

        ////////////////////////////////////////////////////////////
        protected override CResultAErreur MyCanDelete()
        {
            CResultAErreur result = base.MyCanDelete();
            if (!result)
                return result;
            foreach (IUnite unite in CGestionnaireUnites.Unites)
            {
                if (unite.Classe != null && unite.Classe.GlobalId == GlobalId)
                {
                    result.EmpileErreur(I.T("Can not delete unit class @1|20055", Libelle));
                }
            }
            return result;
        }

    }


	
}
