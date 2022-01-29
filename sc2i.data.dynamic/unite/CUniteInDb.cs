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
	/// Repr�sente une unit� ger�e par le syst�me
	/// </summary>
    /// <remarks>
    /// Le syst�me d�finit par d�faut 4 classes d'unit�s<BR></BR>
    /// <LI>Distance (fm,pm,nm,�m,mm,cm,dm,m,dam,hm,km,in,ft,yd)</LI>
    /// <LI>Temps (s,min,h,day,week,year,century)</LI>
    /// <LI>Poids (fg,pg,ng,�g,mg,cg,dg,g,dag,hg,q,t)</LI>
    /// <LI>Volume(ml,cl,dl,l,dal,hl)</LI>
    /// Chaque classe d'unit� est associ�e � une unit� de base. Chaque unit�
    /// sait se convertir vers cette unit� de base, via une formule de type
    /// Ax+B.<BR></BR>
    /// Par exemple, la classe d'unit� de poids utilise comme unit� de base
    /// l'unit� G. L'unit� MG se convertit vers l'unit� de base, via la formule
    /// G = 0.001*x+0 o� x est une valeur en MG. Le "facteur vers base" de l'unit�
    /// MG est donc 0.001, son offset est 0.
    /// </remarks>
	[ObjetServeurURI("CUniteInDbServeur")]
	[Table(CUniteInDb.c_nomTable,CUniteInDb.c_champId,true)]
	[FullTableSync]
	[DynamicClass("Unity class")]
	public class CUniteInDb : CObjetDonneeAIdNumeriqueAuto, IUnite, IObjetSansVersion
	{
		#region D�claration des constantes
		public const string c_nomTable = "UNIT_ITEM";
		public const string c_champId = "UNIT_ID";
		public const string c_champLibelleLong = "UNIT_LONG_LABEL";
        public const string c_champLibelle = "UNIT_LABEL";
		public const string c_champGlobalId = "UNIT_GLOBAL_ID";
        public const string c_champIdClasse = "UNIT_CLASS";
        public const string c_champFacteurVersBase = "UNIT_FACTOR_TO_BASE";
        public const string c_champOffsetVersBase = "UNIT_OFFSET_TO_BASE";
		#endregion

		/// ///////////////////////////////////////////////////////
		public CUniteInDb( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// ///////////////////////////////////////////////////////
		public CUniteInDb ( System.Data.DataRow row )
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
				return I.T("Unit @1|20054", Libelle);
			}
		}

		////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return c_nomTable;
		}


        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Libell� de l'unit� (abr�viation internationale de pr�f�rence)
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
        /// Libell� long de l'unit�
        /// </summary>
        [TableFieldProperty(c_champLibelleLong, 255)]
        [DynamicField("long Label")]
        public string LibelleLong
        {
            get
            {
                return Row.Get<string>(c_champLibelleLong);
            }
            set
            {
                Row[c_champLibelleLong] = value;
            }
        }

        public string LibelleCourt
        {
            get { return Libelle; }
        }

        /// <summary>
        /// Identifiant unit� global de l'unit�<BR></BR>
        /// Attention, il est fort d�conseill� de modifier cet identifiant, il peut
        /// �tre utilis� dans certaines entit�s pour stocker une unit�. Sa modification
        /// provoquerait des erreurs ou des pertes de donn�es.
        /// </summary>
        [TableFieldProperty(c_champGlobalId, 255)]
        [DynamicField("Global Id")]
        public string GlobalId
        {
            get
            {
                return Row.Get<string>(c_champGlobalId);
            }
            set
            {
                Row[c_champGlobalId] = value;
            }
        }

        /// <summary>
        /// Permet de retrouver (via son idGlobal) la classe
        /// d'unit� � laquelle appartient cette unit�.
        /// </summary>
        [TableFieldProperty(c_champIdClasse, 255)]
        [DynamicField("Unity class Id")]
        public string ClassId
        {
            get{
                return Row.Get<string>(c_champIdClasse);
            }
            set{
                Row[c_champIdClasse] = value;
            }
        }

        //-------------------------------------------------------
        /// <summary>
        /// Classe d'unit� � laquelle appartient l'unit�
        /// </summary>
        [DynamicField("Unit class")]
        public IClasseUnite Classe
        {
            get 
            {
                string strId = ClassId;
                return CGestionnaireUnites.GetClasse(strId);
            }
            set
            {
                if (value != null)
                    ClassId = value.GlobalId;
                else
                    ClassId = "";
            }
        }


        //-------------------------------------------------------
        /// <summary>
        /// Une unit� se convertit vers l'unit� de base de sa
        /// classe 
        /// via la formule Ax+B (o� x est exprim� dans l'unit�).<BR></BR>
        /// FactorToBase repr�sente le facteur A.
        /// </summary>
        [TableFieldProperty(c_champFacteurVersBase)]
        [DynamicField("FactorToBase")]
        public double FacteurVersBase
        {
            get
            {
                return Row.Get<double>(c_champFacteurVersBase);
            }
            set
            {
                Row[c_champFacteurVersBase] = value;
            }
        }

        //-------------------------------------------------------
        /// <summary>
        /// Une unit� se convertit vers l'unit� de base de sa
        /// classe 
        /// via la formule Ax+B (o� x est exprim� dans l'unit�).<BR></BR>
        /// OffsetToBase repr�sente la valeur B.
        /// </summary>
        [TableFieldProperty(c_champOffsetVersBase)]
        [DynamicField("OffsetToBase")]
        public double OffsetVersBase
        {
            get
            {
                return Row.Get<double>(c_champOffsetVersBase);
            }
            set
            {
                Row[c_champOffsetVersBase] = value;
            }
        }
    }


	
}
