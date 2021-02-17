using System;
using System.Data;
using System.Collections.Generic;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic
{
    #region RelationElement_EO
    [AttributeUsage(AttributeTargets.Class)]
    [Serializable]
    public class RelationElement_ModuleParametrageAttribute : RelationTypeIdAttribute
    {
        public override string TableFille
        {
            get
            {
                return CRelationElement_ModuleParametrage.c_nomTable;
            }
        }

        //////////////////////////////////////
        public override int Priorite
        {
            get
            {
                return 600;
            }
        }

        protected override string MyIdRelation
        {
            get
            {
                return "ELEMENT_SETTING_MODULE";
            }
        }


        public override string ChampId
        {
            get
            {
                return CRelationElement_ModuleParametrage.c_champIdElement;
            }
        }

        public override string ChampType
        {
            get
            {
                return CRelationElement_ModuleParametrage.c_champTypeElement;
            }
        }

        public override bool Composition
        {
            get
            {
                return true;
            }
        }
        public override bool CanDeleteToujours
        {
            get
            {
                return true;
            }
        }


        public override string NomConvivialPourParent
        {
            get
            {
                return I.T("Setting Module|10002");
            }
        }

        protected override bool MyIsAppliqueToType(Type tp)
        {
            return tp.IsSubclassOf(typeof(CObjetDonneeAIdNumerique));
        }
    }

    #endregion
    /// <summary>
    /// Relation entre une Entite et un
    /// <see cref="CModuleParametrage">Module de paramétrage</see>.<br/>  
    /// </summary>
    [Table(CRelationElement_ModuleParametrage.c_nomTable, CRelationElement_ModuleParametrage.c_champId, true)]
    [ObjetServeurURI("CRelationElement_ModuleParametrageServeur")]
    [DynamicClass("Setting Module / Element")]
    [RelationElement_ModuleParametrage]
    public class CRelationElement_ModuleParametrage : CObjetDonneeAIdNumeriqueAuto
    {
        public const string c_nomTable = "ELEMENT_SET_MODULE";
        public const string c_champId = "ELMT_SET_MODULE_ID";

        public const string c_champTypeElement = "EL_SM_ELEMENT_TYPE";
        public const string c_champIdElement = "EL_SM_ELEMENT_ID";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public CRelationElement_ModuleParametrage(CContexteDonnee ctx)
            : base(ctx)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public CRelationElement_ModuleParametrage(DataRow row)
            : base(row)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        public override string DescriptionElement
        {
            get
            {
                string strInfo = I.T("Element / Setting Module|10003");
                strInfo += " ";
                if (ElementLie != null)
                    strInfo += ElementLie.DescriptionElement;
                if (ModuleParametrage != null)
                    strInfo += "/" + ModuleParametrage.Libelle;
                return strInfo;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champId };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void MyInitValeurDefaut()
        {
        }


        /// <summary>
        /// Module de paramétrage concerné
        /// </summary>
        [Relation(
            CModuleParametrage.c_nomTable,
            CModuleParametrage.c_champId,
            CModuleParametrage.c_champId,
            true,
            true,
            true)]
        [DynamicField("Setting Module")]
        public CModuleParametrage ModuleParametrage
        {
            get
            {
                return (CModuleParametrage)GetParent(CModuleParametrage.c_champId, typeof(CModuleParametrage));
            }
            set
            {
                SetParent(CModuleParametrage.c_champId, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type TypeElement
        {
            get
            {
                return CActivatorSurChaine.GetType(StringTypeElement);
            }
            set
            {
                StringTypeElement = value.ToString();
            }
        }

        /// <summary>
        /// Type de l'élément concerné. Le type est stocké sous forme de codage interne
        /// </summary>
        [TableFieldProperty(c_champTypeElement, 1024)]
        [IndexField]
        [DynamicField("Element type string")]
        public string StringTypeElement
        {
            get
            {
                return (string)Row[c_champTypeElement];
            }
            set
            {
                Row[c_champTypeElement] = value;
            }
        }

        /// <summary>
        /// Id de l'élément concerné
        /// </summary>
        [TableFieldPropertyAttribute(c_champIdElement)]
        [IndexField]
        [DynamicField("Element id")]
        public int IdElement
        {
            get
            {
                return (int)Row[c_champIdElement];
            }
            set
            {
                Row[c_champIdElement] = value;
            }
        }

        /// <summary>
        /// Element lié au Module de Paramétrage.<br/>
        /// </summary>
        [DynamicFieldAttribute("Linked element")]
        public CObjetDonneeAIdNumerique ElementLie
        {
            get
            {
                Type tp = TypeElement;
                if (tp == null)
                    return null;
                
                CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[] { ContexteDonnee });
                if (obj.ReadIfExists(IdElement))
                    return obj;
                return null;
            }
            set
            {
                if (value == null)
                {
                    TypeElement = null;
                    IdElement = -1;
                }
                else
                {
                    TypeElement = value.GetType();
                    IdElement = value.Id;
                }
            }
        }

        //------------------------------------------------------------------
        public static CListeObjetsDonnees GetListeRelationsForElement(CObjetDonneeAIdNumerique objet)
        {
            CFiltreData filtre = new CFiltreData(
                c_champTypeElement + "=@1 and " +
                c_champIdElement + "= @2",
                objet.GetType().ToString(),
                objet.Id);
            CListeObjetsDonnees liste = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CRelationElement_ModuleParametrage), filtre);
            return liste;
        }

        //------------------------------------------------------------------
        public static CListeObjetsDonnees GetEntitesOrganisationnellesDirectementLiees(CObjetDonneeAIdNumerique objet)
        {
            CFiltreData filtre = new CFiltreData(
                CRelationElement_ModuleParametrage.c_champTypeElement + "=@1 and " +
                CRelationElement_ModuleParametrage.c_champIdElement + "=@2",
                objet.GetType().ToString(),
                objet.Id);
            CListeObjetsDonnees liste = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CRelationElement_ModuleParametrage));
            liste.Filtre = filtre;
            return liste.GetDependances("EntiteOrganisationnelle");
        }


    }
}
