using System;
using System.Data;
using System.IO;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.DonneeCumulee;
using System.Text;
using System.Collections.Generic;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Contient le stockage du résultat de requêtes
    /// </summary>
    /// <remarks>
    /// <P>
    /// Les données cumulés sont generées à partir des <see cref="CTypeDonneeCumulee">Types de données cumulées</see>.<BR></BR>
    /// Elles peuvent être interrogées grâce à la fonction GetDonneeCumulée accessible sur toutes les entités de l'application.
    /// </P>
    /// <P>
    /// Toutes les clés sont stockées sous forme de texte, les valeurs sont stockées sous forme de nombre décimaux.
    /// </P>
    /// <p>
    /// Une données cumulée peut stocker jusqu'à 10 clés et 80 valeurs numériques décimales
    /// </p>
    /// </remarks>
    [Table(CDonneeCumulee.c_nomTable, CDonneeCumulee.c_champId, true)]
    [FullTableSync]
    [ObjetServeurURI("CDonneeCumuleeServeur")]
    [DynamicClass("Precalculated data")]
    [NoRelationTypeId]
    public class CDonneeCumulee : CObjetDonneeAIdNumeriqueAuto, IObjetSansVersion
    {
        public const string c_nomTable = "PRECALC_DATA";
        public const string c_champId = "PREDAT_ID";

        public const string c_baseChampCle = "PREDAT_KEY";

        public const string c_champCle0 = c_baseChampCle + "0";
        public const string c_champCle1 = c_baseChampCle + "1";
        public const string c_champCle2 = c_baseChampCle + "2";
        public const string c_champCle3 = c_baseChampCle + "3";
        public const string c_champCle4 = c_baseChampCle + "4";
        public const string c_champCle5 = c_baseChampCle + "5";
        public const string c_champCle6 = c_baseChampCle + "6";
        public const string c_champCle7 = c_baseChampCle + "7";
        public const string c_champCle8 = c_baseChampCle + "8";
        public const string c_champCle9 = c_baseChampCle + "9";

        public const string c_baseChampValeur = "PREDAT_VAL";

        public const string c_champValeur0 = c_baseChampValeur + "0";
        public const string c_champValeur1 = c_baseChampValeur + "1";
        public const string c_champValeur2 = c_baseChampValeur + "2";
        public const string c_champValeur3 = c_baseChampValeur + "3";
        public const string c_champValeur4 = c_baseChampValeur + "4";
        public const string c_champValeur5 = c_baseChampValeur + "5";
        public const string c_champValeur6 = c_baseChampValeur + "6";
        public const string c_champValeur7 = c_baseChampValeur + "7";
        public const string c_champValeur8 = c_baseChampValeur + "8";
        public const string c_champValeur9 = c_baseChampValeur + "9";
        public const string c_champValeur10 = c_baseChampValeur + "10";
        public const string c_champValeur11 = c_baseChampValeur + "11";
        public const string c_champValeur12 = c_baseChampValeur + "12";
        public const string c_champValeur13 = c_baseChampValeur + "13";
        public const string c_champValeur14 = c_baseChampValeur + "14";
        public const string c_champValeur15 = c_baseChampValeur + "15";
        public const string c_champValeur16 = c_baseChampValeur + "16";
        public const string c_champValeur17 = c_baseChampValeur + "17";
        public const string c_champValeur18 = c_baseChampValeur + "18";
        public const string c_champValeur19 = c_baseChampValeur + "19";
        public const string c_champValeur20 = c_baseChampValeur + "20";
        public const string c_champValeur21 = c_baseChampValeur + "21";
        public const string c_champValeur22 = c_baseChampValeur + "22";
        public const string c_champValeur23 = c_baseChampValeur + "23";
        public const string c_champValeur24 = c_baseChampValeur + "24";
        public const string c_champValeur25 = c_baseChampValeur + "25";
        public const string c_champValeur26 = c_baseChampValeur + "26";
        public const string c_champValeur27 = c_baseChampValeur + "27";
        public const string c_champValeur28 = c_baseChampValeur + "28";
        public const string c_champValeur29 = c_baseChampValeur + "29";
        public const string c_champValeur30 = c_baseChampValeur + "30";
        public const string c_champValeur31 = c_baseChampValeur + "31";
        public const string c_champValeur32 = c_baseChampValeur + "32";
        public const string c_champValeur33 = c_baseChampValeur + "33";
        public const string c_champValeur34 = c_baseChampValeur + "34";
        public const string c_champValeur35 = c_baseChampValeur + "35";
        public const string c_champValeur36 = c_baseChampValeur + "36";
        public const string c_champValeur37 = c_baseChampValeur + "37";
        public const string c_champValeur38 = c_baseChampValeur + "38";
        public const string c_champValeur39 = c_baseChampValeur + "39";
        public const string c_champValeur40 = c_baseChampValeur + "40";
        public const string c_champValeur41 = c_baseChampValeur + "41";
        public const string c_champValeur42 = c_baseChampValeur + "42";
        public const string c_champValeur43 = c_baseChampValeur + "43";
        public const string c_champValeur44 = c_baseChampValeur + "44";
        public const string c_champValeur45 = c_baseChampValeur + "45";
        public const string c_champValeur46 = c_baseChampValeur + "46";
        public const string c_champValeur47 = c_baseChampValeur + "47";
        public const string c_champValeur48 = c_baseChampValeur + "48";
        public const string c_champValeur49 = c_baseChampValeur + "49";
        public const string c_champValeur50 = c_baseChampValeur + "50";
        public const string c_champValeur51 = c_baseChampValeur + "51";
        public const string c_champValeur52 = c_baseChampValeur + "52";
        public const string c_champValeur53 = c_baseChampValeur + "53";
        public const string c_champValeur54 = c_baseChampValeur + "54";
        public const string c_champValeur55 = c_baseChampValeur + "55";
        public const string c_champValeur56 = c_baseChampValeur + "56";
        public const string c_champValeur57 = c_baseChampValeur + "57";
        public const string c_champValeur58 = c_baseChampValeur + "58";
        public const string c_champValeur59 = c_baseChampValeur + "59";

        // ATTENTION : Champs à supprimer, ne pas utiliser
        // Les constante ont été gardée pour la suppression des champs dans la mise 
        // à jour de la structure de la base
        public const string c_champValeur60 = c_baseChampValeur + "60";
        public const string c_champValeur61 = c_baseChampValeur + "61";
        public const string c_champValeur62 = c_baseChampValeur + "62";
        public const string c_champValeur63 = c_baseChampValeur + "63";
        public const string c_champValeur64 = c_baseChampValeur + "64";
        public const string c_champValeur65 = c_baseChampValeur + "65";
        public const string c_champValeur66 = c_baseChampValeur + "66";
        public const string c_champValeur67 = c_baseChampValeur + "67";
        public const string c_champValeur68 = c_baseChampValeur + "68";
        public const string c_champValeur69 = c_baseChampValeur + "69";
        public const string c_champValeur70 = c_baseChampValeur + "70";
        public const string c_champValeur71 = c_baseChampValeur + "71";
        public const string c_champValeur72 = c_baseChampValeur + "72";
        public const string c_champValeur73 = c_baseChampValeur + "73";
        public const string c_champValeur74 = c_baseChampValeur + "74";
        public const string c_champValeur75 = c_baseChampValeur + "75";
        public const string c_champValeur76 = c_baseChampValeur + "76";
        public const string c_champValeur77 = c_baseChampValeur + "77";
        public const string c_champValeur78 = c_baseChampValeur + "78";
        public const string c_champValeur79 = c_baseChampValeur + "79";
        // Fin des champs valeur à supprimer

        public const string c_baseChampDate = "PREDAT_DATE";

        public const string c_champDate0 = c_baseChampDate + "0";
        public const string c_champDate1 = c_baseChampDate + "1";
        public const string c_champDate2 = c_baseChampDate + "2";
        public const string c_champDate3 = c_baseChampDate + "3";
        public const string c_champDate4 = c_baseChampDate + "4";
        public const string c_champDate5 = c_baseChampDate + "5";
        public const string c_champDate6 = c_baseChampDate + "6";
        public const string c_champDate7 = c_baseChampDate + "7";
        public const string c_champDate8 = c_baseChampDate + "8";
        public const string c_champDate9 = c_baseChampDate + "9";
        public const string c_champDate10 = c_baseChampDate + "10";
        public const string c_champDate11 = c_baseChampDate + "11";
        public const string c_champDate12 = c_baseChampDate + "12";
        public const string c_champDate13 = c_baseChampDate + "13";
        public const string c_champDate14 = c_baseChampDate + "14";
        public const string c_champDate15 = c_baseChampDate + "15";
        public const string c_champDate16 = c_baseChampDate + "16";
        public const string c_champDate17 = c_baseChampDate + "17";
        public const string c_champDate18 = c_baseChampDate + "18";
        public const string c_champDate19 = c_baseChampDate + "19";
        public const string c_champDate20 = c_baseChampDate + "20";
        public const string c_champDate21 = c_baseChampDate + "21";
        public const string c_champDate22 = c_baseChampDate + "22";
        public const string c_champDate23 = c_baseChampDate + "23";
        public const string c_champDate24 = c_baseChampDate + "24";
        public const string c_champDate25 = c_baseChampDate + "25";
        public const string c_champDate26 = c_baseChampDate + "26";
        public const string c_champDate27 = c_baseChampDate + "27";
        public const string c_champDate28 = c_baseChampDate + "28";
        public const string c_champDate29 = c_baseChampDate + "29";
        public const string c_champDate30 = c_baseChampDate + "30";
        public const string c_champDate31 = c_baseChampDate + "31";
        public const string c_champDate32 = c_baseChampDate + "32";
        public const string c_champDate33 = c_baseChampDate + "33";
        public const string c_champDate34 = c_baseChampDate + "34";
        public const string c_champDate35 = c_baseChampDate + "35";
        public const string c_champDate36 = c_baseChampDate + "36";
        public const string c_champDate37 = c_baseChampDate + "37";
        public const string c_champDate38 = c_baseChampDate + "38";
        public const string c_champDate39 = c_baseChampDate + "39";

        public const string c_baseChampTexte = "PREDAT_STRING";

        public const string c_champTexte0 = c_baseChampTexte + "0";
        public const string c_champTexte1 = c_baseChampTexte + "1";
        public const string c_champTexte2 = c_baseChampTexte + "2";
        public const string c_champTexte3 = c_baseChampTexte + "3";
        public const string c_champTexte4 = c_baseChampTexte + "4";
        public const string c_champTexte5 = c_baseChampTexte + "5";
        public const string c_champTexte6 = c_baseChampTexte + "6";
        public const string c_champTexte7 = c_baseChampTexte + "7";
        public const string c_champTexte8 = c_baseChampTexte + "8";
        public const string c_champTexte9 = c_baseChampTexte + "9";
        public const string c_champTexte10 = c_baseChampTexte + "10";
        public const string c_champTexte11 = c_baseChampTexte + "11";
        public const string c_champTexte12 = c_baseChampTexte + "12";
        public const string c_champTexte13 = c_baseChampTexte + "13";
        public const string c_champTexte14 = c_baseChampTexte + "14";
        public const string c_champTexte15 = c_baseChampTexte + "15";
        public const string c_champTexte16 = c_baseChampTexte + "16";
        public const string c_champTexte17 = c_baseChampTexte + "17";
        public const string c_champTexte18 = c_baseChampTexte + "18";
        public const string c_champTexte19 = c_baseChampTexte + "19";
        public const string c_champTexte20 = c_baseChampTexte + "20";
        public const string c_champTexte21 = c_baseChampTexte + "21";
        public const string c_champTexte22 = c_baseChampTexte + "22";
        public const string c_champTexte23 = c_baseChampTexte + "23";
        public const string c_champTexte24 = c_baseChampTexte + "24";
        public const string c_champTexte25 = c_baseChampTexte + "25";
        public const string c_champTexte26 = c_baseChampTexte + "26";
        public const string c_champTexte27 = c_baseChampTexte + "27";
        public const string c_champTexte28 = c_baseChampTexte + "28";
        public const string c_champTexte29 = c_baseChampTexte + "29";
        public const string c_champTexte30 = c_baseChampTexte + "30";
        public const string c_champTexte31 = c_baseChampTexte + "31";
        public const string c_champTexte32 = c_baseChampTexte + "32";
        public const string c_champTexte33 = c_baseChampTexte + "33";
        public const string c_champTexte34 = c_baseChampTexte + "34";
        public const string c_champTexte35 = c_baseChampTexte + "35";
        public const string c_champTexte36 = c_baseChampTexte + "36";
        public const string c_champTexte37 = c_baseChampTexte + "37";
        public const string c_champTexte38 = c_baseChampTexte + "38";
        public const string c_champTexte39 = c_baseChampTexte + "39";



        /// <summary>
        /// //////////////////////////////////////////////////
        /// </summary>
        /// <param name="ctx"></param>
        public CDonneeCumulee(CContexteDonnee ctx)
            : base(ctx)
        {
        }

        /// //////////////////////////////////////////////////
        public CDonneeCumulee(DataRow row)
            : base(row)
        {
        }

        /// //////////////////////////////////////////////////
        public void SetValeurChamp(CChampDonneeCumulee champ, object value)
        {
            if (champ.NumeroChamp >= 0)
            {
                switch (champ.TypeChamp)
                {
                    case ETypeChampDonneeCumulee.Cle:
                        if (champ.NumeroChamp <= 10)
                            Row[GetNomChampCle(champ.NumeroChamp)] =
                                value != null ? (object)value.ToString() : (object)DBNull.Value;
                        break;
                    case ETypeChampDonneeCumulee.Decimal:
                        if (champ.NumeroChamp <= 59)
                        {
                            object val = DBNull.Value;
                            if (value != null)
                            {
                                if (value is Double)
                                    val = value;
                                else
                                {
                                    try
                                    {
                                        val = Convert.ToDouble(value);
                                    }
                                    catch { }
                                }
                            }
                            Row[GetNomChampValeur(champ.NumeroChamp)] = val;
                        }
                        break;
                    case ETypeChampDonneeCumulee.Date:
                        if (champ.NumeroChamp < 39)
                        {
                            Row[GetNomChampDate(champ.NumeroChamp)] =
                                value is DateTime ? value : DBNull.Value;
                        }
                        break;
                    case ETypeChampDonneeCumulee.Texte:
                        if (champ.NumeroChamp <= 39)
                            Row[GetNomChampTexte(champ.NumeroChamp)] =
                                value != null ? (object)value.ToString() : (object)DBNull.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        public static string GetNomChampCle(int nCle)
        {
            return c_baseChampCle + nCle.ToString();
        }

        public static string GetNomChampValeur(int nCle)
        {
            return c_baseChampValeur + nCle.ToString();
        }

        public static string GetNomChampDate(int nCle)
        {
            return c_baseChampDate + nCle.ToString();
        }

        public static string GetNomChampTexte(int nCle)
        {
            return c_baseChampTexte + nCle.ToString();
        }

        /// //////////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
        }

        /// //////////////////////////////////////////////////
        public override string DescriptionElement
        {
            get
            {
                return I.T("The cumulated data @1|165", Id.ToString());
            }
        }

        /// //////////////////////////////////////////////////
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champId };
        }

        #region Champs Clé
        /// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 0
        /// </summary>
        [TableFieldProperty(c_champCle0, 255, NullAutorise = true)]
        [DynamicField("Key0")]
        [IndexField]
        public string Cle0
        {
            get
            {
                return (string)Row[c_champCle0];
            }
            set
            {
                Row[c_champCle0] = value;
            }
        }
        /// //////////////////////////////////////////////////
        /// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 1
        /// </summary>
        [TableFieldProperty(c_champCle1, 255, NullAutorise = true)]
        [DynamicField("Key1")]
        [IndexField]
        public string Cle1
        {
            get
            {
                return (string)Row[c_champCle1];
            }
            set
            {
                Row[c_champCle1] = value;
            }
        }

        /// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 2
        /// </summary>
        [TableFieldProperty(c_champCle2, 255, NullAutorise = true)]
        [DynamicField("Key2")]
        [IndexField]
        public string Cle2
        {
            get
            {
                return (string)Row[c_champCle2];
            }
            set
            {
                Row[c_champCle2] = value;
            }
        }

        /// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 3
        /// </summary>
        [TableFieldProperty(c_champCle3, 255, NullAutorise = true)]
        [DynamicField("Key3")]
        [IndexField]
        public string Cle3
        {
            get
            {
                return (string)Row[c_champCle3];
            }
            set
            {
                Row[c_champCle3] = value;
            }
        }/// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 4
        /// </summary>
        [TableFieldProperty(c_champCle4, 255, NullAutorise = true)]
        [DynamicField("Key4")]
        [IndexField]
        public string Cle4
        {
            get
            {
                return (string)Row[c_champCle4];
            }
            set
            {
                Row[c_champCle4] = value;
            }
        }/// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 5
        /// </summary>
        [TableFieldProperty(c_champCle5, 255, NullAutorise = true)]
        [DynamicField("Key5")]
        [IndexField]
        public string Cle5
        {
            get
            {
                return (string)Row[c_champCle5];
            }
            set
            {
                Row[c_champCle5] = value;
            }
        }/// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 6
        /// </summary>
        [TableFieldProperty(c_champCle6, 255, NullAutorise = true)]
        [DynamicField("Key6")]
        [IndexField]
        public string Cle6
        {
            get
            {
                return (string)Row[c_champCle6];
            }
            set
            {
                Row[c_champCle6] = value;
            }
        }/// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 7
        /// </summary>
        [TableFieldProperty(c_champCle7, 255, NullAutorise = true)]
        [DynamicField("Key7")]
        [IndexField]
        public string Cle7
        {
            get
            {
                return (string)Row[c_champCle7];
            }
            set
            {
                Row[c_champCle7] = value;
            }
        }/// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 8
        /// </summary>
        [TableFieldProperty(c_champCle8, 255, NullAutorise = true)]
        [IndexField]
        [DynamicField("Key8")]
        public string Cle8
        {
            get
            {
                return (string)Row[c_champCle8];
            }
            set
            {
                Row[c_champCle8] = value;
            }
        }/// //////////////////////////////////////////////////
        /// <summary>
        /// Valeur de clé 9
        /// </summary>
        [TableFieldProperty(c_champCle9, 255, NullAutorise = true)]
        [DynamicField("Key9")]
        [IndexField]
        public string Cle9
        {
            get
            {
                return (string)Row[c_champCle9];
            }
            set
            {
                Row[c_champCle9] = value;
            }
        }

        #endregion

        #region Champs Valeur
        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur0
        [TableFieldProperty(c_champValeur0, true)]
        [DynamicField("Value0")]
        public double Valeur0
        {
            get
            {
                if (Row[c_champValeur0] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur0];
            }
            set
            {
                Row[c_champValeur0] = value;
            }
        }



        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur1
        [TableFieldProperty(c_champValeur1, true)]
        [DynamicField("Value1")]
        public double Valeur1
        {
            get
            {
                if (Row[c_champValeur1] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur1];
            }
            set
            {
                Row[c_champValeur1] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur2
        [TableFieldProperty(c_champValeur2, true)]
        [DynamicField("Value2")]
        public double Valeur2
        {
            get
            {
                if (Row[c_champValeur2] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur2];
            }
            set
            {
                Row[c_champValeur2] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur3
        [TableFieldProperty(c_champValeur3, true)]
        [DynamicField("Value3")]
        public double Valeur3
        {
            get
            {
                if (Row[c_champValeur3] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur3];
            }
            set
            {
                Row[c_champValeur3] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur4
        [TableFieldProperty(c_champValeur4, true)]
        [DynamicField("Value4")]
        public double Valeur4
        {
            get
            {
                if (Row[c_champValeur4] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur4];
            }
            set
            {
                Row[c_champValeur4] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur5
        [TableFieldProperty(c_champValeur5, true)]
        [DynamicField("Value5")]
        public double Valeur5
        {
            get
            {
                if (Row[c_champValeur5] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur5];
            }
            set
            {
                Row[c_champValeur5] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur6
        [TableFieldProperty(c_champValeur6, true)]
        [DynamicField("Value6")]
        public double Valeur6
        {
            get
            {
                if (Row[c_champValeur6] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur6];
            }
            set
            {
                Row[c_champValeur6] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur7
        [TableFieldProperty(c_champValeur7, true)]
        [DynamicField("Value7")]
        public double Valeur7
        {
            get
            {
                if (Row[c_champValeur7] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur7];
            }
            set
            {
                Row[c_champValeur7] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur8
        [TableFieldProperty(c_champValeur8, true)]
        [DynamicField("Value8")]
        public double Valeur8
        {
            get
            {
                if (Row[c_champValeur8] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur8];
            }
            set
            {
                Row[c_champValeur8] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur9
        [TableFieldProperty(c_champValeur9, true)]
        [DynamicField("Value9")]
        public double Valeur9
        {
            get
            {
                if (Row[c_champValeur9] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur9];
            }
            set
            {
                Row[c_champValeur9] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur10
        [TableFieldProperty(c_champValeur10, true)]
        [DynamicField("Value10")]
        public double Valeur10
        {
            get
            {
                if (Row[c_champValeur10] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur10];
            }
            set
            {
                Row[c_champValeur10] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur11
        [TableFieldProperty(c_champValeur11, true)]
        [DynamicField("Value11")]
        public double Valeur11
        {
            get
            {
                if (Row[c_champValeur11] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur11];
            }
            set
            {
                Row[c_champValeur11] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur12
        [TableFieldProperty(c_champValeur12, true)]
        [DynamicField("Value12")]
        public double Valeur12
        {
            get
            {
                if (Row[c_champValeur12] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur12];
            }
            set
            {
                Row[c_champValeur12] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur13
        [TableFieldProperty(c_champValeur13, true)]
        [DynamicField("Value13")]
        public double Valeur13
        {
            get
            {
                if (Row[c_champValeur13] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur13];
            }
            set
            {
                Row[c_champValeur13] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur14
        [TableFieldProperty(c_champValeur14, true)]
        [DynamicField("Value14")]
        public double Valeur14
        {
            get
            {
                if (Row[c_champValeur14] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur14];
            }
            set
            {
                Row[c_champValeur14] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur15
        [TableFieldProperty(c_champValeur15, true)]
        [DynamicField("Value15")]
        public double Valeur15
        {
            get
            {
                if (Row[c_champValeur15] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur15];
            }
            set
            {
                Row[c_champValeur15] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur16
        [TableFieldProperty(c_champValeur16, true)]
        [DynamicField("Value16")]
        public double Valeur16
        {
            get
            {
                if (Row[c_champValeur16] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur16];
            }
            set
            {
                Row[c_champValeur16] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur17
        [TableFieldProperty(c_champValeur17, true)]
        [DynamicField("Value17")]
        public double Valeur17
        {
            get
            {
                if (Row[c_champValeur17] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur17];
            }
            set
            {
                Row[c_champValeur17] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur18
        [TableFieldProperty(c_champValeur18, true)]
        [DynamicField("Value18")]
        public double Valeur18
        {
            get
            {
                if (Row[c_champValeur18] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur18];
            }
            set
            {
                Row[c_champValeur18] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur19
        [TableFieldProperty(c_champValeur19, true)]
        [DynamicField("Value19")]
        public double Valeur19
        {
            get
            {
                if (Row[c_champValeur19] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur19];
            }
            set
            {
                Row[c_champValeur19] = value;
            }
        }

        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur20
        [TableFieldProperty(c_champValeur20, true)]
        [DynamicField("Value20")]
        public double Valeur20
        {
            get
            {
                if (Row[c_champValeur20] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur20];
            }
            set
            {
                Row[c_champValeur20] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur21
        [TableFieldProperty(c_champValeur21, true)]
        [DynamicField("Value21")]
        public double Valeur21
        {
            get
            {
                if (Row[c_champValeur21] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur21];
            }
            set
            {
                Row[c_champValeur21] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur22
        [TableFieldProperty(c_champValeur22, true)]
        [DynamicField("Value22")]
        public double Valeur22
        {
            get
            {
                if (Row[c_champValeur22] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur22];
            }
            set
            {
                Row[c_champValeur22] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur23
        [TableFieldProperty(c_champValeur23, true)]
        [DynamicField("Value23")]
        public double Valeur23
        {
            get
            {
                if (Row[c_champValeur23] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur23];
            }
            set
            {
                Row[c_champValeur23] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur24
        [TableFieldProperty(c_champValeur24, true)]
        [DynamicField("Value24")]
        public double Valeur24
        {
            get
            {
                if (Row[c_champValeur24] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur24];
            }
            set
            {
                Row[c_champValeur24] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur25
        [TableFieldProperty(c_champValeur25, true)]
        [DynamicField("Value25")]
        public double Valeur25
        {
            get
            {
                if (Row[c_champValeur25] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur25];
            }
            set
            {
                Row[c_champValeur25] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur26
        [TableFieldProperty(c_champValeur26, true)]
        [DynamicField("Value26")]
        public double Valeur26
        {
            get
            {
                if (Row[c_champValeur26] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur26];
            }
            set
            {
                Row[c_champValeur26] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur27
        [TableFieldProperty(c_champValeur27, true)]
        [DynamicField("Value27")]
        public double Valeur27
        {
            get
            {
                if (Row[c_champValeur27] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur27];
            }
            set
            {
                Row[c_champValeur27] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur28
        [TableFieldProperty(c_champValeur28, true)]
        [DynamicField("Value28")]
        public double Valeur28
        {
            get
            {
                if (Row[c_champValeur28] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur28];
            }
            set
            {
                Row[c_champValeur28] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur29
        [TableFieldProperty(c_champValeur29, true)]
        [DynamicField("Value29")]
        public double Valeur29
        {
            get
            {
                if (Row[c_champValeur29] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur29];
            }
            set
            {
                Row[c_champValeur29] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur30
        [TableFieldProperty(c_champValeur30, true)]
        [DynamicField("Value30")]
        public double Valeur30
        {
            get
            {
                if (Row[c_champValeur30] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur30];
            }
            set
            {
                Row[c_champValeur30] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur31
        [TableFieldProperty(c_champValeur31, true)]
        [DynamicField("Value31")]
        public double Valeur31
        {
            get
            {
                if (Row[c_champValeur31] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur31];
            }
            set
            {
                Row[c_champValeur31] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur32
        [TableFieldProperty(c_champValeur32, true)]
        [DynamicField("Value32")]
        public double Valeur32
        {
            get
            {
                if (Row[c_champValeur32] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur32];
            }
            set
            {
                Row[c_champValeur32] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur33
        [TableFieldProperty(c_champValeur33, true)]
        [DynamicField("Value33")]
        public double Valeur33
        {
            get
            {
                if (Row[c_champValeur33] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur33];
            }
            set
            {
                Row[c_champValeur33] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur34
        [TableFieldProperty(c_champValeur34, true)]
        [DynamicField("Value34")]
        public double Valeur34
        {
            get
            {
                if (Row[c_champValeur34] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur34];
            }
            set
            {
                Row[c_champValeur34] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur35
        [TableFieldProperty(c_champValeur35, true)]
        [DynamicField("Value35")]
        public double Valeur35
        {
            get
            {
                if (Row[c_champValeur35] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur35];
            }
            set
            {
                Row[c_champValeur35] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur36
        [TableFieldProperty(c_champValeur36, true)]
        [DynamicField("Value36")]
        public double Valeur36
        {
            get
            {
                if (Row[c_champValeur36] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur36];
            }
            set
            {
                Row[c_champValeur36] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur37
        [TableFieldProperty(c_champValeur37, true)]
        [DynamicField("Value37")]
        public double Valeur37
        {
            get
            {
                if (Row[c_champValeur37] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur37];
            }
            set
            {
                Row[c_champValeur37] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur38
        [TableFieldProperty(c_champValeur38, true)]
        [DynamicField("Value38")]
        public double Valeur38
        {
            get
            {
                if (Row[c_champValeur38] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur38];
            }
            set
            {
                Row[c_champValeur38] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur39
        [TableFieldProperty(c_champValeur39, true)]
        [DynamicField("Value39")]
        public double Valeur39
        {
            get
            {
                if (Row[c_champValeur39] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur39];
            }
            set
            {
                Row[c_champValeur39] = value;
            }
        }

        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur40
        [TableFieldProperty(c_champValeur40, true)]
        [DynamicField("Value40")]
        public double Valeur40
        {
            get
            {
                if (Row[c_champValeur40] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur40];
            }
            set
            {
                Row[c_champValeur40] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur41
        [TableFieldProperty(c_champValeur41, true)]
        [DynamicField("Value41")]
        public double Valeur41
        {
            get
            {
                if (Row[c_champValeur41] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur41];
            }
            set
            {
                Row[c_champValeur41] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur42
        [TableFieldProperty(c_champValeur42, true)]
        [DynamicField("Value42")]
        public double Valeur42
        {
            get
            {
                if (Row[c_champValeur42] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur42];
            }
            set
            {
                Row[c_champValeur42] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur43
        [TableFieldProperty(c_champValeur43, true)]
        [DynamicField("Value43")]
        public double Valeur43
        {
            get
            {
                if (Row[c_champValeur43] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur43];
            }
            set
            {
                Row[c_champValeur43] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur44
        [TableFieldProperty(c_champValeur44, true)]
        [DynamicField("Value44")]
        public double Valeur44
        {
            get
            {
                if (Row[c_champValeur44] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur44];
            }
            set
            {
                Row[c_champValeur44] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur45
        [TableFieldProperty(c_champValeur45, true)]
        [DynamicField("Value45")]
        public double Valeur45
        {
            get
            {
                if (Row[c_champValeur45] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur45];
            }
            set
            {
                Row[c_champValeur45] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur46
        [TableFieldProperty(c_champValeur46, true)]
        [DynamicField("Value46")]
        public double Valeur46
        {
            get
            {
                if (Row[c_champValeur46] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur46];
            }
            set
            {
                Row[c_champValeur46] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur47
        [TableFieldProperty(c_champValeur47, true)]
        [DynamicField("Value47")]
        public double Valeur47
        {
            get
            {
                if (Row[c_champValeur47] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur47];
            }
            set
            {
                Row[c_champValeur47] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur48
        [TableFieldProperty(c_champValeur48, true)]
        [DynamicField("Value48")]
        public double Valeur48
        {
            get
            {
                if (Row[c_champValeur48] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur48];
            }
            set
            {
                Row[c_champValeur48] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur49
        [TableFieldProperty(c_champValeur49, true)]
        [DynamicField("Value49")]
        public double Valeur49
        {
            get
            {
                if (Row[c_champValeur49] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur49];
            }
            set
            {
                Row[c_champValeur49] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur50
        [TableFieldProperty(c_champValeur50, true)]
        [DynamicField("Value50")]
        public double Valeur50
        {
            get
            {
                if (Row[c_champValeur50] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur50];
            }
            set
            {
                Row[c_champValeur50] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur51
        [TableFieldProperty(c_champValeur51, true)]
        [DynamicField("Value51")]
        public double Valeur51
        {
            get
            {
                if (Row[c_champValeur51] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur51];
            }
            set
            {
                Row[c_champValeur51] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur52
        [TableFieldProperty(c_champValeur52, true)]
        [DynamicField("Value52")]
        public double Valeur52
        {
            get
            {
                if (Row[c_champValeur52] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur52];
            }
            set
            {
                Row[c_champValeur52] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur53
        [TableFieldProperty(c_champValeur53, true)]
        [DynamicField("Value53")]
        public double Valeur53
        {
            get
            {
                if (Row[c_champValeur53] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur53];
            }
            set
            {
                Row[c_champValeur53] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur54
        [TableFieldProperty(c_champValeur54, true)]
        [DynamicField("Value54")]
        public double Valeur54
        {
            get
            {
                if (Row[c_champValeur54] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur54];
            }
            set
            {
                Row[c_champValeur54] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur55
        [TableFieldProperty(c_champValeur55, true)]
        [DynamicField("Value55")]
        public double Valeur55
        {
            get
            {
                if (Row[c_champValeur55] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur55];
            }
            set
            {
                Row[c_champValeur55] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur56
        [TableFieldProperty(c_champValeur56, true)]
        [DynamicField("Value56")]
        public double Valeur56
        {
            get
            {
                if (Row[c_champValeur56] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur56];
            }
            set
            {
                Row[c_champValeur56] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur57
        [TableFieldProperty(c_champValeur57, true)]
        [DynamicField("Value57")]
        public double Valeur57
        {
            get
            {
                if (Row[c_champValeur57] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur57];
            }
            set
            {
                Row[c_champValeur57] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur58
        [TableFieldProperty(c_champValeur58, true)]
        [DynamicField("Value58")]
        public double Valeur58
        {
            get
            {
                if (Row[c_champValeur58] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur58];
            }
            set
            {
                Row[c_champValeur58] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Valeur59
        [TableFieldProperty(c_champValeur59, true)]
        [DynamicField("Value59")]
        public double Valeur59
        {
            get
            {
                if (Row[c_champValeur59] == DBNull.Value)
                    return 0;
                return (double)Row[c_champValeur59];
            }
            set
            {
                Row[c_champValeur59] = value;
            }
        }
        #endregion

        #region Champs Date
        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date0
        [TableFieldProperty(c_champDate0, true)]
        [DynamicField("Date0")]
        [IndexField]
        public DateTime? Date0
        {
            get
            {
                if (Row[c_champDate0] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate0];
            }
            set
            {
                Row[c_champDate0, true] = value;
            }
        }



        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date1
        [TableFieldProperty(c_champDate1, true)]
        [DynamicField("Date1")]
        public DateTime? Date1
        {
            get
            {
                if (Row[c_champDate1] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate1];
            }
            set
            {
                Row[c_champDate1, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date2
        [TableFieldProperty(c_champDate2, true)]
        [DynamicField("Date2")]
        public DateTime? Date2
        {
            get
            {
                if (Row[c_champDate2] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate2];
            }
            set
            {
                Row[c_champDate2, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date3
        [TableFieldProperty(c_champDate3, true)]
        [DynamicField("Date3")]
        public DateTime? Date3
        {
            get
            {
                if (Row[c_champDate3] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate3];
            }
            set
            {
                Row[c_champDate3, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date4
        [TableFieldProperty(c_champDate4, true)]
        [DynamicField("Date4")]
        public DateTime? Date4
        {
            get
            {
                if (Row[c_champDate4] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate4];
            }
            set
            {
                Row[c_champDate4, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date5
        [TableFieldProperty(c_champDate5, true)]
        [DynamicField("Date5")]
        public DateTime? Date5
        {
            get
            {
                if (Row[c_champDate5] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate5];
            }
            set
            {
                Row[c_champDate5, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date6
        [TableFieldProperty(c_champDate6, true)]
        [DynamicField("Date6")]
        public DateTime? Date6
        {
            get
            {
                if (Row[c_champDate6] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate6];
            }
            set
            {
                Row[c_champDate6, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date7
        [TableFieldProperty(c_champDate7, true)]
        [DynamicField("Date7")]
        public DateTime? Date7
        {
            get
            {
                if (Row[c_champDate7] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate7];
            }
            set
            {
                Row[c_champDate7, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date8
        [TableFieldProperty(c_champDate8, true)]
        [DynamicField("Date8")]
        public DateTime? Date8
        {
            get
            {
                if (Row[c_champDate8] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate8];
            }
            set
            {
                Row[c_champDate8, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date9
        [TableFieldProperty(c_champDate9, true)]
        [DynamicField("Date9")]
        public DateTime? Date9
        {
            get
            {
                if (Row[c_champDate9] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate9];
            }
            set
            {
                Row[c_champDate9, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date10
        [TableFieldProperty(c_champDate10, true)]
        [DynamicField("Date10")]
        public DateTime? Date10
        {
            get
            {
                if (Row[c_champDate10] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate10];
            }
            set
            {
                Row[c_champDate10, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date11
        [TableFieldProperty(c_champDate11, true)]
        [DynamicField("Date11")]
        public DateTime? Date11
        {
            get
            {
                if (Row[c_champDate11] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate11];
            }
            set
            {
                Row[c_champDate11, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date12
        [TableFieldProperty(c_champDate12, true)]
        [DynamicField("Date12")]
        public DateTime? Date12
        {
            get
            {
                if (Row[c_champDate12] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate12];
            }
            set
            {
                Row[c_champDate12, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date13
        [TableFieldProperty(c_champDate13, true)]
        [DynamicField("Date13")]
        public DateTime? Date13
        {
            get
            {
                if (Row[c_champDate13] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate13];
            }
            set
            {
                Row[c_champDate13, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date14
        [TableFieldProperty(c_champDate14, true)]
        [DynamicField("Date14")]
        public DateTime? Date14
        {
            get
            {
                if (Row[c_champDate14] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate14];
            }
            set
            {
                Row[c_champDate14, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date15
        [TableFieldProperty(c_champDate15, true)]
        [DynamicField("Date15")]
        public DateTime? Date15
        {
            get
            {
                if (Row[c_champDate15] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate15];
            }
            set
            {
                Row[c_champDate15, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date16
        [TableFieldProperty(c_champDate16, true)]
        [DynamicField("Date16")]
        public DateTime? Date16
        {
            get
            {
                if (Row[c_champDate16] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate16];
            }
            set
            {
                Row[c_champDate16, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date17
        [TableFieldProperty(c_champDate17, true)]
        [DynamicField("Date17")]
        public DateTime? Date17
        {
            get
            {
                if (Row[c_champDate17] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate17];
            }
            set
            {
                Row[c_champDate17, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date18
        [TableFieldProperty(c_champDate18, true)]
        [DynamicField("Date18")]
        public DateTime? Date18
        {
            get
            {
                if (Row[c_champDate18] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate18];
            }
            set
            {
                Row[c_champDate18, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date19
        [TableFieldProperty(c_champDate19, true)]
        [DynamicField("Date19")]
        public DateTime? Date19
        {
            get
            {
                if (Row[c_champDate19] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate19];
            }
            set
            {
                Row[c_champDate19, true] = value;
            }
        }

        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date20
        [TableFieldProperty(c_champDate20, true)]
        [DynamicField("Date20")]
        public DateTime? Date20
        {
            get
            {
                if (Row[c_champDate20] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate20];
            }
            set
            {
                Row[c_champDate20, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date21
        [TableFieldProperty(c_champDate21, true)]
        [DynamicField("Date21")]
        public DateTime? Date21
        {
            get
            {
                if (Row[c_champDate21] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate21];
            }
            set
            {
                Row[c_champDate21, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date22
        [TableFieldProperty(c_champDate22, true)]
        [DynamicField("Date22")]
        public DateTime? Date22
        {
            get
            {
                if (Row[c_champDate22] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate22];
            }
            set
            {
                Row[c_champDate22, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date23
        [TableFieldProperty(c_champDate23, true)]
        [DynamicField("Date23")]
        public DateTime? Date23
        {
            get
            {
                if (Row[c_champDate23] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate23];
            }
            set
            {
                Row[c_champDate23, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date24
        [TableFieldProperty(c_champDate24, true)]
        [DynamicField("Date24")]
        public DateTime? Date24
        {
            get
            {
                if (Row[c_champDate24] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate24];
            }
            set
            {
                Row[c_champDate24, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date25
        [TableFieldProperty(c_champDate25, true)]
        [DynamicField("Date25")]
        public DateTime? Date25
        {
            get
            {
                if (Row[c_champDate25] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate25];
            }
            set
            {
                Row[c_champDate25, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date26
        [TableFieldProperty(c_champDate26, true)]
        [DynamicField("Date26")]
        public DateTime? Date26
        {
            get
            {
                if (Row[c_champDate26] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate26];
            }
            set
            {
                Row[c_champDate26, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date27
        [TableFieldProperty(c_champDate27, true)]
        [DynamicField("Date27")]
        public DateTime? Date27
        {
            get
            {
                if (Row[c_champDate27] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate27];
            }
            set
            {
                Row[c_champDate27, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date28
        [TableFieldProperty(c_champDate28, true)]
        [DynamicField("Date28")]
        public DateTime? Date28
        {
            get
            {
                if (Row[c_champDate28] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate28];
            }
            set
            {
                Row[c_champDate28, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date29
        [TableFieldProperty(c_champDate29, true)]
        [DynamicField("Date29")]
        public DateTime? Date29
        {
            get
            {
                if (Row[c_champDate29] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate29];
            }
            set
            {
                Row[c_champDate29, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date30
        [TableFieldProperty(c_champDate30, true)]
        [DynamicField("Date30")]
        public DateTime? Date30
        {
            get
            {
                if (Row[c_champDate30] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate30];
            }
            set
            {
                Row[c_champDate30, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date31
        [TableFieldProperty(c_champDate31, true)]
        [DynamicField("Date31")]
        public DateTime? Date31
        {
            get
            {
                if (Row[c_champDate31] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate31];
            }
            set
            {
                Row[c_champDate31, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date32
        [TableFieldProperty(c_champDate32, true)]
        [DynamicField("Date32")]
        public DateTime? Date32
        {
            get
            {
                if (Row[c_champDate32] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate32];
            }
            set
            {
                Row[c_champDate32, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date33
        [TableFieldProperty(c_champDate33, true)]
        [DynamicField("Date33")]
        public DateTime? Date33
        {
            get
            {
                if (Row[c_champDate33] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate33];
            }
            set
            {
                Row[c_champDate33, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date34
        [TableFieldProperty(c_champDate34, true)]
        [DynamicField("Date34")]
        public DateTime? Date34
        {
            get
            {
                if (Row[c_champDate34] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate34];
            }
            set
            {
                Row[c_champDate34, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date35
        [TableFieldProperty(c_champDate35, true)]
        [DynamicField("Date35")]
        public DateTime? Date35
        {
            get
            {
                if (Row[c_champDate35] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate35];
            }
            set
            {
                Row[c_champDate35, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date36
        [TableFieldProperty(c_champDate36, true)]
        [DynamicField("Date36")]
        public DateTime? Date36
        {
            get
            {
                if (Row[c_champDate36] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate36];
            }
            set
            {
                Row[c_champDate36, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date37
        [TableFieldProperty(c_champDate37, true)]
        [DynamicField("Date37")]
        public DateTime? Date37
        {
            get
            {
                if (Row[c_champDate37] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate37];
            }
            set
            {
                Row[c_champDate37, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date38
        [TableFieldProperty(c_champDate38, true)]
        [DynamicField("Date38")]
        public DateTime? Date38
        {
            get
            {
                if (Row[c_champDate38] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate38];
            }
            set
            {
                Row[c_champDate38, true] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Date39
        [TableFieldProperty(c_champDate39, true)]
        [DynamicField("Date39")]
        public DateTime? Date39
        {
            get
            {
                if (Row[c_champDate39] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDate39];
            }
            set
            {
                Row[c_champDate39, true] = value;
            }
        }
        #endregion

        #region Champs texte

        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte0
        [TableFieldProperty(c_champTexte0, 255, NullAutorise = true)]
        [DynamicField("Text0")]
        public string Texte0
        {
            get
            {
                return (string)Row[c_champTexte0];
            }
            set
            {
                Row[c_champTexte0] = value;
            }
        }



        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte1
        [TableFieldProperty(c_champTexte1, 255, NullAutorise = true)]
        [DynamicField("Text1")]
        public string Texte1
        {
            get
            {
                return (string)Row[c_champTexte1];
            }
            set
            {
                Row[c_champTexte1] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte2
        [TableFieldProperty(c_champTexte2, 255, NullAutorise = true)]
        [DynamicField("Text2")]
        public string Texte2
        {
            get
            {
                return (string)Row[c_champTexte2];
            }
            set
            {
                Row[c_champTexte2] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte3
        [TableFieldProperty(c_champTexte3, 255, NullAutorise = true)]
        [DynamicField("Text3")]
        public string Texte3
        {
            get
            {
                return (string)Row[c_champTexte3];
            }
            set
            {
                Row[c_champTexte3] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte4
        [TableFieldProperty(c_champTexte4, 255, NullAutorise = true)]
        [DynamicField("Text4")]
        public string Texte4
        {
            get
            {
                return (string)Row[c_champTexte4];
            }
            set
            {
                Row[c_champTexte4] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte5
        [TableFieldProperty(c_champTexte5, 255, NullAutorise = true)]
        [DynamicField("Text5")]
        public string Texte5
        {
            get
            {
                return (string)Row[c_champTexte5];
            }
            set
            {
                Row[c_champTexte5] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte6
        [TableFieldProperty(c_champTexte6, 255, NullAutorise = true)]
        [DynamicField("Text6")]
        public string Texte6
        {
            get
            {
                return (string)Row[c_champTexte6];
            }
            set
            {
                Row[c_champTexte6] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte7
        [TableFieldProperty(c_champTexte7, 255, NullAutorise = true)]
        [DynamicField("Text7")]
        public string Texte7
        {
            get
            {
                return (string)Row[c_champTexte7];
            }
            set
            {
                Row[c_champTexte7] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte8
        [TableFieldProperty(c_champTexte8, 255, NullAutorise = true)]
        [DynamicField("Text8")]
        public string Texte8
        {
            get
            {
                return (string)Row[c_champTexte8];
            }
            set
            {
                Row[c_champTexte8] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte9
        [TableFieldProperty(c_champTexte9, 255, NullAutorise = true)]
        [DynamicField("Text9")]
        public string Texte9
        {
            get
            {
                return (string)Row[c_champTexte9];
            }
            set
            {
                Row[c_champTexte9] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte10
        [TableFieldProperty(c_champTexte10, 255, NullAutorise = true)]
        [DynamicField("Text10")]
        public string Texte10
        {
            get
            {
                return (string)Row[c_champTexte10];
            }
            set
            {
                Row[c_champTexte10] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte11
        [TableFieldProperty(c_champTexte11, 255, NullAutorise = true)]
        [DynamicField("Text11")]
        public string Texte11
        {
            get
            {
                return (string)Row[c_champTexte11];
            }
            set
            {
                Row[c_champTexte11] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte12
        [TableFieldProperty(c_champTexte12, 255, NullAutorise = true)]
        [DynamicField("Text12")]
        public string Texte12
        {
            get
            {
                return (string)Row[c_champTexte12];
            }
            set
            {
                Row[c_champTexte12] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte13
        [TableFieldProperty(c_champTexte13, 255, NullAutorise = true)]
        [DynamicField("Text13")]
        public string Texte13
        {
            get
            {
                return (string)Row[c_champTexte13];
            }
            set
            {
                Row[c_champTexte13] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte14
        [TableFieldProperty(c_champTexte14, 255, NullAutorise = true)]
        [DynamicField("Text14")]
        public string Texte14
        {
            get
            {
                return (string)Row[c_champTexte14];
            }
            set
            {
                Row[c_champTexte14] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte15
        [TableFieldProperty(c_champTexte15, 255, NullAutorise = true)]
        [DynamicField("Text15")]
        public string Texte15
        {
            get
            {
                return (string)Row[c_champTexte15];
            }
            set
            {
                Row[c_champTexte15] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte16
        [TableFieldProperty(c_champTexte16, 255, NullAutorise = true)]
        [DynamicField("Text16")]
        public string Texte16
        {
            get
            {
                return (string)Row[c_champTexte16];
            }
            set
            {
                Row[c_champTexte16] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte17
        [TableFieldProperty(c_champTexte17, 255, NullAutorise = true)]
        [DynamicField("Text17")]
        public string Texte17
        {
            get
            {
                return (string)Row[c_champTexte17];
            }
            set
            {
                Row[c_champTexte17] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte18
        [TableFieldProperty(c_champTexte18, 255, NullAutorise = true)]
        [DynamicField("Text18")]
        public string Texte18
        {
            get
            {
                return (string)Row[c_champTexte18];
            }
            set
            {
                Row[c_champTexte18] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte19
        [TableFieldProperty(c_champTexte19, 255, NullAutorise = true)]
        [DynamicField("Text19")]
        public string Texte19
        {
            get
            {
                return (string)Row[c_champTexte19];
            }
            set
            {
                Row[c_champTexte19] = value;
            }
        }

        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte20
        [TableFieldProperty(c_champTexte20, 255, NullAutorise = true)]
        [DynamicField("Text20")]
        public string Texte20
        {
            get
            {
                return (string)Row[c_champTexte20];
            }
            set
            {
                Row[c_champTexte20] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte21
        [TableFieldProperty(c_champTexte21, 255, NullAutorise = true)]
        [DynamicField("Text21")]
        public string Texte21
        {
            get
            {
                return (string)Row[c_champTexte21];
            }
            set
            {
                Row[c_champTexte21] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte22
        [TableFieldProperty(c_champTexte22, 255, NullAutorise = true)]
        [DynamicField("Text22")]
        public string Texte22
        {
            get
            {
                return (string)Row[c_champTexte22];
            }
            set
            {
                Row[c_champTexte22] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte23
        [TableFieldProperty(c_champTexte23, 255, NullAutorise = true)]
        [DynamicField("Text23")]
        public string Texte23
        {
            get
            {
                return (string)Row[c_champTexte23];
            }
            set
            {
                Row[c_champTexte23] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte24
        [TableFieldProperty(c_champTexte24, 255, NullAutorise = true)]
        [DynamicField("Text24")]
        public string Texte24
        {
            get
            {
                return (string)Row[c_champTexte24];
            }
            set
            {
                Row[c_champTexte24] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte25
        [TableFieldProperty(c_champTexte25, 255, NullAutorise = true)]
        [DynamicField("Text25")]
        public string Texte25
        {
            get
            {
                return (string)Row[c_champTexte25];
            }
            set
            {
                Row[c_champTexte25] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte26
        [TableFieldProperty(c_champTexte26, 255, NullAutorise = true)]
        [DynamicField("Text26")]
        public string Texte26
        {
            get
            {
                return (string)Row[c_champTexte26];
            }
            set
            {
                Row[c_champTexte26] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte27
        [TableFieldProperty(c_champTexte27, 255, NullAutorise = true)]
        [DynamicField("Text27")]
        public string Texte27
        {
            get
            {
                return (string)Row[c_champTexte27];
            }
            set
            {
                Row[c_champTexte27] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte28
        [TableFieldProperty(c_champTexte28, 255, NullAutorise = true)]
        [DynamicField("Text28")]
        public string Texte28
        {
            get
            {
                return (string)Row[c_champTexte28];
            }
            set
            {
                Row[c_champTexte28] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte29
        [TableFieldProperty(c_champTexte29, 255, NullAutorise = true)]
        [DynamicField("Text29")]
        public string Texte29
        {
            get
            {
                return (string)Row[c_champTexte29];
            }
            set
            {
                Row[c_champTexte29] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte30
        [TableFieldProperty(c_champTexte30, 255, NullAutorise = true)]
        [DynamicField("Text30")]
        public string Texte30
        {
            get
            {
                return (string)Row[c_champTexte30];
            }
            set
            {
                Row[c_champTexte30] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte31
        [TableFieldProperty(c_champTexte31, 255, NullAutorise = true)]
        [DynamicField("Text31")]
        public string Texte31
        {
            get
            {
                return (string)Row[c_champTexte31];
            }
            set
            {
                Row[c_champTexte31] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte32
        [TableFieldProperty(c_champTexte32, 255, NullAutorise = true)]
        [DynamicField("Text32")]
        public string Texte32
        {
            get
            {
                return (string)Row[c_champTexte32];
            }
            set
            {
                Row[c_champTexte32] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte33
        [TableFieldProperty(c_champTexte33, 255, NullAutorise = true)]
        [DynamicField("Text33")]
        public string Texte33
        {
            get
            {
                return (string)Row[c_champTexte33];
            }
            set
            {
                Row[c_champTexte33] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte34
        [TableFieldProperty(c_champTexte34, 255, NullAutorise = true)]
        [DynamicField("Text34")]
        public string Texte34
        {
            get
            {
                return (string)Row[c_champTexte34];
            }
            set
            {
                Row[c_champTexte34] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte35
        [TableFieldProperty(c_champTexte35, 255, NullAutorise = true)]
        [DynamicField("Text35")]
        public string Texte35
        {
            get
            {
                return (string)Row[c_champTexte35];
            }
            set
            {
                Row[c_champTexte35] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte36
        [TableFieldProperty(c_champTexte36, 255, NullAutorise = true)]
        [DynamicField("Text36")]
        public string Texte36
        {
            get
            {
                return (string)Row[c_champTexte36];
            }
            set
            {
                Row[c_champTexte36] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte37
        [TableFieldProperty(c_champTexte37, 255, NullAutorise = true)]
        [DynamicField("Text37")]
        public string Texte37
        {
            get
            {
                return (string)Row[c_champTexte37];
            }
            set
            {
                Row[c_champTexte37] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte38
        [TableFieldProperty(c_champTexte38, 255, NullAutorise = true)]
        [DynamicField("Text38")]
        public string Texte38
        {
            get
            {
                return (string)Row[c_champTexte38];
            }
            set
            {
                Row[c_champTexte38] = value;
            }
        }


        //////////////////////////////////////////////////////////
        /// <summary>
        /// Texte39
        [TableFieldProperty(c_champTexte39, 255, NullAutorise = true)]
        [DynamicField("Text39")]
        public string Texte39
        {
            get
            {
                return (string)Row[c_champTexte39];
            }
            set
            {
                Row[c_champTexte39] = value;
            }
        }

        #endregion

        //////////////////////////////////////////////////////////
        /// <summary>
        /// Type de donnée correspondant à la donnée
        /// </summary>
        [Relation(CTypeDonneeCumulee.c_nomTable,
             CTypeDonneeCumulee.c_champId,
             CTypeDonneeCumulee.c_champId,
             true,
             true,
             true)
        ]
        [DynamicField("Data type")]
        public CTypeDonneeCumulee TypeDonneeCumulee
        {
            get
            {
                return (CTypeDonneeCumulee)GetParent(CTypeDonneeCumulee.c_champId, typeof(CTypeDonneeCumulee));
            }
            set
            {
                SetParent(CTypeDonneeCumulee.c_champId, value);
            }
        }

        //-------------------------------------------------------------------------
        public bool FillFromDonneeTransportable(CDonneeCumuleeTransportable donnee)
        {
            CTypeDonneeCumulee typeDonnee = new CTypeDonneeCumulee(ContexteDonnee);
            if (!typeDonnee.ReadIfExists(donnee.IdTypeDonneeCumulee))
                return false;
            TypeDonneeCumulee = typeDonnee;
            foreach (CChampDonneeCumulee champ in donnee.GetChampsDefinis())
            {
                SetValeurChamp(champ, donnee.GetValeurChamp(champ));
            }
            return true;
        }

        //-------------------------------------------------------------------------
        [DynamicMethod("Returns the double value for a specific field number","Field number")]
        public double? GetValue ( int nNumValue )
        {
            string strNomChamp = GetNomChampValeur(nNumValue);
            try
            {
                object val = Row[strNomChamp];
                    return val as double?;
            }
            catch { }
            return null;
        }

        //-------------------------------------------------------------------------
        [DynamicMethod("Returns the date value for a specific field number", "Field number")]
        public DateTime? GetDateValue(int nNumValue)
        {
            string strNomChamp = GetNomChampDate(nNumValue);
            try
            {
                object val = Row[strNomChamp];
                return val as DateTime?;
            }
            catch { }
            return null;
        }

        //-------------------------------------------------------------------------
        [DynamicMethod("Returns the text value for a specific field number", "Field number")]
        public string GetTextValue(int nNumValue)
        {
            string strNomChamp = GetNomChampTexte(nNumValue);
            try
            {
                object val = Row[strNomChamp];
                return val as string;
            }
            catch { }
            return null;
        }

        //-------------------------------------------------------------------------
        [DynamicMethod("Returns the Key value for a specific field number", "Field number")]
        public string GetKeyValue(int nNumValue)
        {
            string strNomChamp = GetNomChampCle(nNumValue);
            try
            {
                object val = Row[strNomChamp];
                return val as string;
            }
            catch { }
            return null;
        }
        //-------------------------------------------------------------------------
        /*public static void CreateOrFillFromDonneeCumulee ( CContexteDonnee ctx, CDonneeCumuleeTransportable donnee )
        {
            StringBuilder bl = new StringBuilder();
            //récupère les valeurs de clé
            List<object> lstParametres = new List<object>();
            for ( int n = 0; n < 10; n++ )
            {
                string strVal = donnee.GetValeurCle ( nCle );
                if ( strVal != null )
                {
                    bl.Append(GetNomChampCle(nCle));
                    bl.Append("=@");
                    bl.Append(lstParametres.Count);
                    bl.Append(" and ");

        */



    }


    /// /////////////////////////////////////////////////////////////
    [AutoExec("Autoexec")]
    public class CMethodeSupplementaireGetDonneesCumulees : CMethodeSupplementaire
    {
        protected CMethodeSupplementaireGetDonneesCumulees()
            : base(typeof(CObjetDonneeAIdNumerique))
        {
        }

        public static void Autoexec()
        {
            CGestionnaireMethodesSupplementaires.RegisterMethode(new CMethodeSupplementaireGetDonneesCumulees());
        }

        public override string Name
        {
            get
            {
                return "GetDonneesCumulees";
            }
        }

        public override string Description
        {
            get
            {
                return I.T("Return cumulated data related to an object|166");
            }
        }

        public override Type ReturnType
        {
            get
            {
                return typeof(CDonneeCumulee);
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
                return new CInfoParametreMethodeDynamique[]
					{
						new CInfoParametreMethodeDynamique ( "TypeDonnee",
						I.T("Data type to be returned|167"),
						typeof(string) )
					};
            }
        }

        public override object Invoke(object objetAppelle, params object[] parametres)
        {
            if (parametres.Length != 1 || parametres[0] == null ||
                !(objetAppelle is CObjetDonneeAIdNumerique))
                return null;
            CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)objetAppelle;
            CListeObjetsDonnees listeVide = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CDonneeCumulee));
            listeVide.Filtre = new CFiltreDataImpossible();
            CTypeDonneeCumulee typeDonnee = new CTypeDonneeCumulee(objet.ContexteDonnee);
            if (!typeDonnee.ReadIfExists(new CFiltreData(CTypeDonneeCumulee.c_champCode + "=@1",
                parametres[0].ToString())))
                return listeVide;
            return typeDonnee.GetDonneesCumuleesForObjet((CObjetDonneeAIdNumerique)objetAppelle);
        }




    }
}
