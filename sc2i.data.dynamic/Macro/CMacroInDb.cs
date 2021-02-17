using System;
using System.Collections;
using System.Data;

using sc2i.common;

using sc2i.multitiers.client;
using sc2i.expression;
using System.IO;


namespace sc2i.data.dynamic.macro
{
    /// <summary>
    /// Permet de définir le civilité d'une personne (ex: Monsieur, Madamme...)
    /// </summary>
    [DynamicClass("Macro")]
    [Table(CMacroInDb.c_nomTable, CMacroInDb.c_champId, true)]
    [FullTableSync]
    [ObjetServeurURI("CMacroInDbServeur")]
    public class CMacroInDb : CObjetDonneeAIdNumeriqueAuto,
        IObjetALectureTableComplete,
        IObjetSansVersion
    {
        public const string c_nomTable = "MACRO_VERSION";
        public const string c_champId = "MACRO_ID";
        public const string c_champLibelle = "MACRO_LABEL";
        public const string c_champTypeCible = "MACRO_TARGET_TYPE";
        public const string c_champCondition = "MACRO_CONDITION";


        /// /////////////////////////////////////////////
        public CMacroInDb(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        /// /////////////////////////////////////////////
        public CMacroInDb(DataRow row)
            : base(row)
        {
        }

        /// /////////////////////////////////////////////
        public override string DescriptionElement
        {
            get
            {
                return I.T("Macro @1|20051", Libelle);
            }
        }

        /// /////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
        }

        /// /////////////////////////////////////////////
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champLibelle };
        }




        /// <summary>
        /// Le libellé de la macro
        /// </summary>
        [TableFieldProperty(c_champLibelle, 255)]
        [DynamicField("Label")]
        public string Libelle
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


        //-----------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        [TableFieldProperty(c_champTypeCible)]
        [DynamicField("Target type string")]
        public string TypeCibleString
        {
            get
            {
                return (string)Row[c_champTypeCible];
            }
            set
            {
                Row[c_champTypeCible] = value;
            }
        }

        //-----------------------------------------------------------
        public Type TypeCible
        {
            get
            {
                return CActivatorSurChaine.GetType(TypeCibleString);
            }
            set
            {
                if (value == null)
                    TypeCibleString = "";
                else
                    TypeCibleString = value.ToString(); ;
            }
        }

        /// /////////////////////////////////////////////////////////////
        [TableFieldProperty(c_champCondition, NullAutorise = true)]
        public CDonneeBinaireInRow DataCondition
        {
            get
            {
                if (Row[c_champCondition] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champCondition);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champCondition, donnee);
                }
                return ((CDonneeBinaireInRow)Row[c_champCondition]).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champCondition] = value;
            }
        }


        /// /////////////////////////////////////////////////////////////
        public C2iExpression FormuleCondition
        {
            get
            {
                C2iExpression formule = new C2iExpressionVrai();
                if (DataCondition.Donnees != null)
                {
                    MemoryStream stream = new MemoryStream(DataCondition.Donnees);
                    BinaryReader reader = new BinaryReader(stream);
                    CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                    CResultAErreur result = serializer.TraiteObject<C2iExpression>(ref formule);
                    if (!result)
                    {
                        formule = new C2iExpressionVrai();
                    }
                    reader.Close();
                    stream.Close();
                }
                return formule;
            }
            set
            {
                if (value == null)
                {
                    CDonneeBinaireInRow data = DataCondition;
                    data.Donnees = null;
                    DataCondition = data;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    C2iExpression formule = value;
                    CResultAErreur result=  serializer.TraiteObject<C2iExpression>(ref formule);
                    if (result)
                    {
                        CDonneeBinaireInRow data = DataCondition;
                        data.Donnees = stream.GetBuffer();
                        DataCondition = data;
                    }
                    writer.Close();
                    stream.Close();
                }
            }
        }

    }
}
