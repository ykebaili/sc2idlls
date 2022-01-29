using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using System.Data;
using sc2i.common.Referencement;

namespace sc2i.data.dynamic.NommageEntite
{

    /// <summary>
    /// Nommage d'une entitié particulière
    /// </summary>
    /// <remarks>
    /// Le nommage d'entité est une fonction purement administrateur.<br></br>
    /// Elle permet de donner un nom à une entité de l'application (toutes les entités peuvent être nommées) et 
    /// à faire référence à cette entité dans des formules en utilisant ce nom.<bR></bR>
    /// <p>Le nommage des entités, bien que non obligatoire est fortement conseillé pour permettre
    /// simplement la maintenance efficace du paramétrage de l'application</p>
    /// <p>Une même entité peut avoir plusieurs noms</p>
    /// /// </remarks>
    [DynamicClass("Entity Naming")]
    [Table(CNommageEntite.c_nomTable, CNommageEntite.c_champId, true)]
    [FullTableSync]
    [ObjetServeurURI("CNommageEntiteServeur")]
    [Unique(false, "The Strong Name field must be unique", CNommageEntite.c_champNomFort)]
    public class CNommageEntite : CObjetDonneeAIdNumeriqueAuto,
        IObjetALectureTableComplete,
        IElementAReferenceExterneExplicite,
        IObjetSansVersion
    {
        public const string c_nomTable = "ENTITY_NAMING";
        public const string c_champId = "ENTITYNAM_ID";
        // Référenes à l'entité nommée
        public const string c_champTypeEntite = "ENTNAM_TYPE_ENTITY";
        //public const string c_champIdEntite = "ENTNAM_ID_ENTITY";
        public const string c_champCleEntite = "ENTNAM_KEY_ENTITY";
        public const string c_champNomFort = "ENTNAM_STRONG_NAME";
        //

        public CNommageEntite(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        public CNommageEntite(DataRow row)
            : base(row)
        {
        }


        public override string DescriptionElement
        {
            get
            {
                return I.T("Strong Naming @1 for Entity @2|10010", NomFort, DescriptionObjetNomme);
            }
        }

        protected override void MyInitValeurDefaut()
        {

        }

        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { CNommageEntite.c_champNomFort };
        }

        //--------------------------------------------------------------------------
        /// <summary>
        /// Type de l'entité nommée. Le type est stocké sous forme d'un codage interne
        /// </summary>
        [TableFieldProperty(CNommageEntite.c_champTypeEntite, 255)]
        [DynamicField("Entity type")]
        public string TypeEntityString
        {
            get
            {
                return (string)Row[c_champTypeEntite];
            }
            set
            {
                Row[c_champTypeEntite] = value;
            }
        }

        public Type TypeEntite
        {
            get
            {
                return CActivatorSurChaine.GetType(TypeEntityString);
            }
            set
            {
                if (value == null)
                    Row[c_champTypeEntite] = "";
                else
                    Row[c_champTypeEntite] = value.ToString();
            }
        }



        //--------------------------------------------------------------------------
        /// <summary>
        /// Id de l'entité nommée
        /// </summary>
        [TableFieldProperty(CNommageEntite.c_champCleEntite, 64)]
        [ReplaceField("IdEntite", "Entity Id")]
        [DynamicField("Entity Key string")]
        public string CleEntiteString
        {
            get
            {
                return (string)Row[c_champCleEntite];
            }
            set
            {
                Row[c_champCleEntite] = value;
            }
        }

        //--------------------------------------------------------------------------
        [DynamicField("Entity key")]
        public CDbKey CleEntite
        {
            get
            {
                return CDbKey.CreateFromStringValue(CleEntiteString);
            }
            set
            {
                if (value != null)
                    CleEntiteString = value.StringValue;
                else
                    CleEntiteString = "";
            }
        }


        //--------------------------------------------------------------------------
        /// <summary>
        /// Nom de l'entité. Si le nom proposé contient des caractères invalides, ils sont automatiquement supprimés par le système.
        /// </summary>
        [TableFieldProperty(CNommageEntite.c_champNomFort, 200)]
        [DynamicField("Strong Name")]
        public string NomFort
        {
            get
            {
                return (string)Row[c_champNomFort];
            }
            set
            {
                string strCaracteresAutorises = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789éèçàâêîôûäëïöüÿ_";
                StringBuilder bl = new StringBuilder();
                foreach (char c in value)
                {
                    if (c == '-' || c == ' ')
                        bl.Append('_');
                    else if (strCaracteresAutorises.IndexOf(c) >= 0)
                        bl.Append(c);
                }
                Row[c_champNomFort] = bl.ToString();
            }
        }

        //-----------------------------------------------------------------------------
        /// <summary>
        /// Retourn l'objet nommé par cette entité de nommage
        /// </summary>
        /// <returns></returns>
        [DependanceObjetDonnee]
        public CObjetDonneeAIdNumerique GetObjetNomme()
        {
            Type tpObjet = TypeEntite;

            object obj = Activator.CreateInstance(tpObjet, new object[] { ContexteDonnee });
            CObjetDonneeAIdNumerique objAIdNum = obj as CObjetDonneeAIdNumerique;
            if (objAIdNum != null && objAIdNum.ReadIfExists(CleEntite))
                return objAIdNum;
            return null;
        }

        [DynamicField("Named Object Description")]
        public string DescriptionObjetNomme
        {
            get
            {
                CObjetDonneeAIdNumerique objNommé = GetObjetNomme();
                if (objNommé != null)
                    return objNommé.DescriptionElement;
                return "NULL";
            }
        }


        public override string ToString()
        {
            return NomFort;
        }

        //////////////////////////////////////////////////////////////////////////////////
        public object[] GetReferencesExternesExplicites(CContexteGetReferenceExterne contexteGetRef)
        {
            CObjetDonnee objet = GetObjetNomme();
            if (objet != null)
                return new object[] { objet };
            return new object[0];
        }
    }

        

    //////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Implémente une méthode d'extension pour la classe CObjetDonneeAIdNumerique
    /// qui retourn la liste des Nommages de l'objet
    /// </summary>
    public static class CNommageEntiteExtensions
    {
        public static string[] GetSrongNames(this CObjetDonneeAIdNumerique objet)
        {
            List<string> strNames = new List<string>();
            CListeObjetsDonnees lstNommages = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CNommageEntite));
            //TESTDBKEYOK
            lstNommages.Filtre = new CFiltreData(
                CNommageEntite.c_champTypeEntite + " = @1 and " +
                CNommageEntite.c_champCleEntite+ " = @2",
                objet.TypeString,
                objet.DbKey.StringValue);

            foreach (CNommageEntite nom in lstNommages)
            {
                strNames.Add(nom.NomFort);
            }

            return strNames.ToArray();
        }
    }


    /// <summary>
    /// Ajoute une méthode à CObjetDonneeAIdNumerique, pour
    /// qu'il puisse retourner la liste des Noms de l'entité
    /// </summary>
    [AutoExec("Autoexec")]
    public class CMethodGetStrongNamesSurObjet : CMethodeSupplementaire
    {
        protected CMethodGetStrongNamesSurObjet()
            : base(typeof(CObjetDonneeAIdNumerique))
        {
        }

        public static void Autoexec()
        {
            CGestionnaireMethodesSupplementaires.RegisterMethode(new CMethodGetStrongNamesSurObjet());
        }

        public override string Name
        {
            get
            {
                return "GetStrongNames";
            }
        }

        public override string Description
        {
            get
            {
                return I.T("Return list of Entity Strong Names|10011");
            }
        }

        public override Type ReturnType
        {
            get
            {
                return typeof(string);
            }
        }

        public override bool ReturnArrayOfReturnType
        {
            get
            {
                return true;
            }
        }


        public override CInfoParametreMethodeDynamique[] InfosParametres
        {
            get
            {
                return new CInfoParametreMethodeDynamique[] { };
            }
        }

        public override object Invoke(object objetAppelle, params object[] parametres)
        {
            if (objetAppelle is CObjetDonneeAIdNumerique)
                return ((CObjetDonneeAIdNumerique)objetAppelle).GetSrongNames();
            else
                return new string[] { };
        }
    }
}
