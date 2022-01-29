using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Modules de paramétrage. Les modules de paramétrage sont utilisés pour organiser les différentes entités de l'application
    /// dans une arborescence.
    /// </summary>
    /// <remarks>
    /// <p>L'utilisation des modules de paramétrage est optionnelle. Cette entité ne sert qu'à organiser le paramétrage et n'a aucune influence sur 
    /// le comportement de l'application.</p>
    /// <p>Le paramétrage de l'application nécéssite générallement de nombreuses entités dont l'existence est lié à des modules de l'appplication.<br></br>
    /// L'utilisation des modules de paramétrage permet à l'administrateur d'organiser ces entités dans une arborescence
    /// afin d'y avoir accès rapidement et de regrouper entre elles toutes les entités participant à un module de l'application</p>
    /// <p>Les modules de paramétrages sont hiérarchiques, c'est à dire que chaque module peut contenir des sous modules, ce qui permet
    /// une organisation précise des entités.</p>
    /// <p>Chaque module de paramétrage peut contenir autant d'entités que souhaitées et de tous types</p>
    /// <p>Une même entité peut être utilisée dans plusieurs modules de paramétrage</p>
    /// </remarks>
    [DynamicClass("Setting Module")]
    [Table(CModuleParametrage.c_nomTable, CModuleParametrage.c_champId, true)]
    [FullTableSync]
    [ObjetServeurURI("CModuleParametrageServeur")]
    public class CModuleParametrage :   CObjetHierarchique, IObjetALectureTableComplete, IObjetSansVersion
    {
        public const string c_nomTable = "SETTING_MODULE";

        public const string c_champId = "SET_MODULE_ID";
        public const string c_champLibelle = "SET_MODULE_LABEL";
        public const string c_champDescription = "SET_MODULE_DESC";
        
        // Constantes pour l'objet hiérarchique
        public const string c_champCodeSystemePartiel = "SM_PARTIAL_SYSTEME_CODE";
        public const string c_champCodeSystemeComplet = "SM_FULL_SYSTEME_CODE";
        public const string c_champNiveau = "SM_LEVEL";
        public const string c_champIdParent = "SM_PARENT_ID";


        /// /////////////////////////////////////////////
        public CModuleParametrage(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        /// /////////////////////////////////////////////
        public CModuleParametrage(DataRow row)
            : base(row)
        {
        }


        /// /////////////////////////////////////////////
        public override string DescriptionElement
        {
            get
            {
                return I.T("Setting Module @1|10001", Libelle);
            }
        }

        /// /////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
            base.MyInitValeurDefaut();
        }

        /// /////////////////////////////////////////////
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champLibelle };
        }


        /// /////////////////////////////////////////////
        ///<summary>Libellé complet du module (comprend le nom du module et de ses modules parents</summary>
        [DynamicField("Full label")]
        public override string LibelleComplet
        {
            get
            {
                string strText = "";
                if (ObjetParent != null)
                {
                    strText = ((CObjetHierarchique)ObjetParent).LibelleComplet + "/";

                    if (ObjetParent.ObjetParent == null)
                    {
                        int nPos = strText.IndexOf('/');
                        if (nPos >= 0)
                            strText = strText.Substring(nPos + 1);
                    }
                }
                strText += Libelle;
                return strText;
            }
        }

        ///////////////////////////////////////////////////////////////
        /// <summary>
        /// Libellé du module
        /// </summary>
        [sc2i.common.DescriptionField]
        [TableFieldProperty(c_champLibelle, 255)]
        [sc2i.common.DynamicField("Label")]
        public override string Libelle
        {
            get
            {
                return (string)Row[c_champLibelle];
            }
            set
            {
                Row[c_champLibelle] = value;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Description du module
        /// </summary>
        /// <remarks>
        /// La description du module permet de stocker des informations concernant ce module, et générallement décrivant
        /// le rôle des éléments qu'il contient.
        /// </remarks>
        [TableFieldProperty(c_champDescription, 2000)]
        [sc2i.common.DynamicField("Description")]
        public string Description
        {
            get
            {
                return (string)Row[c_champDescription];
            }
            set
            {
                Row[c_champDescription] = value;
            }
        }


        //----------------------------------------------------
        public override int NbCarsParNiveau
        {
            get
            {
                return 2;
            }
        }

        //----------------------------------------------------
        public override string ChampCodeSystemePartiel
        {
            get
            {
                return c_champCodeSystemePartiel;
            }
        }

        //----------------------------------------------------
        public override string ChampCodeSystemeComplet
        {
            get
            {
                return c_champCodeSystemeComplet;
            }
        }

        //----------------------------------------------------
        public override string ChampNiveau
        {
            get
            {
                return c_champNiveau;
            }
        }

        //----------------------------------------------------
        public override string ChampLibelle
        {
            get
            {
                return c_champLibelle;
            }
        }

        //----------------------------------------------------
        public override string ChampIdParent
        {
            get
            {
                return c_champIdParent;
            }
        }


        //----------------------------------------------------
        /// <summary>
        /// Module de Paramétrage parent dans la hiérarchie
        /// </summary>
        [Relation(
            c_nomTable,
            c_champId,
            c_champIdParent,
            false,
            true)]
        [DynamicField("Parent Module")]
        public CModuleParametrage ModuleParent
        {
            get
            {
                return (CModuleParametrage)ObjetParent;
            }
            set
            {
                ObjetParent = value;
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Modules de Paramétrage fils dans la hiérarchie
        /// </summary>
        [RelationFille(typeof(CModuleParametrage), "ModuleParent")]
        [DynamicChilds("Child Modules", typeof(CModuleParametrage))]
        public CListeObjetsDonnees ModulesFils
        {
            get
            {
                return ObjetsFils;
            }
        }


        //----------------------------------------------------
        /// <summary>
        /// Indique le code (unique pour son parent) du module de paramétrage
        /// </summary>
        /// <remarks>
        /// Chaque module de paramétrage se voit affecter un code unique dans son parent. La concaténation des codes parent et du code du module
        /// donnent un code unique au module.
        /// </remarks>
        [TableFieldProperty(c_champCodeSystemePartiel, 2)]
        [DynamicField("Partial system code")]
        public override string CodeSystemePartiel
        {
            get
            {
                string strVal = "";
                if (Row[c_champCodeSystemePartiel] != DBNull.Value)
                    strVal = (string)Row[c_champCodeSystemePartiel];
                strVal = strVal.Trim().PadLeft(2, '0');
                return (string)Row[c_champCodeSystemePartiel];
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Indique le code complet de l'entité organisationnelle.
        /// </summary>
        /// <remarks>
        /// Le code complet est composé du code de l'entité parente, et du code partiel.<BR>
        /// </BR>
        /// </remarks>
        [TableFieldProperty(c_champCodeSystemeComplet, 100)]
        [DynamicField("Full system code")]
        public override string CodeSystemeComplet
        {
            get
            {
                return (string)Row[c_champCodeSystemeComplet];
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Indique le niveau hiérarchique( nombre de parents ) du module
        /// </summary>
        /// <remarks>
        /// Le niveau d'un module sans parent est 0
        /// </remarks>
        [TableFieldProperty(c_champNiveau)]
        [DynamicField("Level")]
        public override int Niveau
        {
            get
            {
                return (int)Row[c_champNiveau];
            }
        }


        [RelationFille(typeof(CRelationElement_ModuleParametrage), "ModuleParametrage")]
        [DynamicChilds("Elements Relations", typeof(CRelationElement_ModuleParametrage))]
        public CListeObjetsDonnees RelationsElements
        {
            get
            {
                return GetDependancesListe(CRelationElement_ModuleParametrage.c_nomTable, c_champId);
            }
        }


   }
}
