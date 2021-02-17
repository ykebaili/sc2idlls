using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public abstract class CSourceSmartImport : I2iSerializable
    {
        private EOptionImport m_optionImport = EOptionImport.OnUpdateAndCreate;
        private C2iExpression m_formuleCondition = null;

        //--------------------------------------------------------
        public CSourceSmartImport()
        {
        }
    
        //--------------------------------------------------------
        private static Dictionary<Type, string> m_dicTypesPossibles = new Dictionary<Type, string>();

        //--------------------------------------------------------
        public static void RegisterTypeSource ( Type tp, string strName )
        {
            m_dicTypesPossibles[tp] = strName;
        }

        //--------------------------------------------------------
        public virtual bool ShouldImport ( DataRow row, CContexteImportDonnee contexte )
        {
            switch (m_optionImport)
            {
                case EOptionImport.OnCreate:
                    if (row.RowState != DataRowState.Added)
                        return false;
                    break;
                case EOptionImport.Never:
                    return false;
            }
            if ( Condition != null && !(Condition is C2iExpressionVrai))
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(row);
                CResultAErreur result = Condition.Eval(ctx);
                if ( !result )
                {
                    result.EmpileErreur(I.T("Error on condition formula '@1'|20103",
                        Condition.GetString()));
                    contexte.AddLog(new CLigneLogImport(ETypeLigneLogImport.Error,
                        row,
                        "",
                        contexte,
                        result.Erreur.ToString()));
                    return false;
                }
                return CUtilBool.BoolFromObject(result.Data);
            }
            return true;
        }


        //--------------------------------------------------------
        public static IEnumerable<Type> GetTypesDeSources()
        {
            List<Type> lstTypes = new List<Type>();
            lstTypes.AddRange(m_dicTypesPossibles.Keys);
            lstTypes.Sort((x, y) => m_dicTypesPossibles[x].CompareTo(m_dicTypesPossibles[y]));
            return lstTypes.AsReadOnly();
        }

        //--------------------------------------------------------
        public static string GetTypeLabel(Type tp)
        {
            string strVal = "";
            m_dicTypesPossibles.TryGetValue(tp, out strVal);
            return strVal;
        }

        //--------------------------------------------------------
        public string GetTypeLabel()
        {
            return GetTypeLabel(GetType());
        }

        //--------------------------------------------------------
        public static Bitmap GetImage ( Type tp )
        {
            CSourceSmartImport s = Activator.CreateInstance( tp, new object[0]) as CSourceSmartImport;
            if ( s != null )
                return s.GetImage();
            return null;
        }

        //--------------------------------------------------------
        public abstract string LibelleSource { get; }



        //--------------------------------------------------------
        public abstract Bitmap GetImage();

        //--------------------------------------------------------
        public EOptionImport OptionImport
        {
            get
            {
                return m_optionImport;
            }
            set
            {
                m_optionImport = value;
            }
        }

        //--------------------------------------------------------
        public C2iExpression Condition
        {
            get
            {
                return m_formuleCondition;
            }
            set { m_formuleCondition = value; }
        }

        //--------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        

        //--------------------------------------------------------
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteEnum<EOptionImport>(ref m_optionImport);
            result = serializer.TraiteObject<C2iExpression>(ref m_formuleCondition);
            if (!result)
                return result;

            return result;
        }


        //--------------------------------------------------------
        //Le data du result contient la valeur
        public abstract CResultAErreur GetValue(DataRow rowSource, CContexteImportDonnee contexteImport);

        //--------------------------------------------------------
        public virtual COptionsValeursNulles OptionsValeursNulles
        {
            get
            {
                return null;
            }
            set { }
        }


    }

    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    [AutoExec("Autoexec")]
    [Serializable]
    public class CSourceSmartImportNoImport : CSourceSmartImport
    {
        //--------------------------------------------------------
        public CSourceSmartImportNoImport()
            :base()
        {
        }

        //--------------------------------------------------------
        public static void Autoexec()
        {
            CSourceSmartImport.RegisterTypeSource(typeof(CSourceSmartImportNoImport), I.T("No source|20087"));
        }

        //--------------------------------------------------------
        public override Bitmap GetImage()
        {
            return Resource.Import_No;
        }

        //--------------------------------------------------------
        public override string LibelleSource
        {
            get { return "No source"; }
        }
        //--------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;

            return result;
        }

        //--------------------------------------------------------
        public override CResultAErreur GetValue(DataRow rowSource, CContexteImportDonnee contexteImport)
        {
            return CResultAErreur.True;
        }
    }

    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    [AutoExec("Autoexec")]
    [Serializable]
    public class CSourceSmartImportFixedValue : CSourceSmartImport
    {
        object m_valeur = null;
        //--------------------------------------------------------
        public CSourceSmartImportFixedValue()
            : base()
        {
        }

        //--------------------------------------------------------
        public static void Autoexec()
        {
            CSourceSmartImport.RegisterTypeSource(typeof(CSourceSmartImportFixedValue), I.T("Fixed value|20088"));
        }

        //--------------------------------------------------------
        public override string LibelleSource
        {
            get { return Valeur != null ? Valeur.ToString() : "null"; }
        }

        //--------------------------------------------------------
        public override Bitmap GetImage()
        {
            return Resource.Import_Default;
        }

        //--------------------------------------------------------
        public object Valeur
        {
            get { return m_valeur; }
            set{m_valeur = value;}
        }
        
        //--------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( result )
                result = base.Serialize(serializer);
            if (!result)
                return result;
            result = serializer.TraiteObjetSimple(ref m_valeur);
            return result;
        }

        //--------------------------------------------------------
        public override CResultAErreur GetValue(DataRow rowSource, CContexteImportDonnee contexteImport)
        {
            CResultAErreur result = CResultAErreur.True;
            if (Valeur is CValeurParDefautObjetDonnee)
                result.Data = ((CValeurParDefautObjetDonnee)Valeur).GetObjet(contexteImport.ContexteDonnee);
            else
                result.Data = Valeur;
            return result;
        }

    }

    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    [AutoExec("Autoexec")]
    [Serializable]
    public class CSourceSmartImportField : CSourceSmartImport
    {
        private string m_strFieldName = "";
        private COptionsValeursNulles m_optionsValeursNulles = null;
        //--------------------------------------------------------
        public CSourceSmartImportField()
            : base()
        {
        }

        //--------------------------------------------------------
        public static void Autoexec()
        {
            CSourceSmartImport.RegisterTypeSource(typeof(CSourceSmartImportField), I.T("Field|20089"));
        }

        //--------------------------------------------------------
        public override Bitmap GetImage()
        {
            return Resource.Import_Field;
        }

        //--------------------------------------------------------
        public override string LibelleSource
        {
            get { return m_strFieldName; }
        }

        //--------------------------------------------------------
        public string NomChampSource
        {
            get
            {
                return m_strFieldName;
            }
            set { m_strFieldName = value; }
        }

        //--------------------------------------------------------
        public override COptionsValeursNulles OptionsValeursNulles
        {
            get
            {
                return m_optionsValeursNulles;
            }
            set
            {
                if (value == null || value.IsDefaultBehavior)
                    m_optionsValeursNulles = null;
                else
                    m_optionsValeursNulles = value;
            }
        }

        //--------------------------------------------------------
        private int GetNumVersion()
        {
            //return 0;
            return 1;//ajout des options valeurs nulles
        }

        //--------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( result )
                result = base.Serialize(serializer);
            if ( !result )
                return result;
            serializer.TraiteString(ref m_strFieldName);
            if (nVersion >= 1)
                serializer.TraiteObject<COptionsValeursNulles>(ref m_optionsValeursNulles);
            return result;
        }

        //--------------------------------------------------------
        public override CResultAErreur GetValue(DataRow rowSource, CContexteImportDonnee contexteImport)
        {
            CResultAErreur result = CResultAErreur.True;
            if (NomChampSource != null && rowSource.Table.Columns.Contains(NomChampSource))
            {
                result.Data = rowSource[NomChampSource];
                if (result.Data == DBNull.Value)
                    result.Data = null;
            }
            return result;
        }

    }

    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    [AutoExec("Autoexec")]
    [Serializable]
    public class CSourceSmartImportFormula : CSourceSmartImport
    {
        private C2iExpression m_formule = null;
        //--------------------------------------------------------
        public CSourceSmartImportFormula()
            : base()
        {
        }

        //--------------------------------------------------------
        public static void Autoexec()
        {
            CSourceSmartImport.RegisterTypeSource(typeof(CSourceSmartImportFormula), I.T("Formula|20090"));
        }

        //--------------------------------------------------------
        public override Bitmap GetImage()
        {
            return Resource.Import_Formula;
        }

        //--------------------------------------------------------
        public override string LibelleSource
        {
            get { return m_formule != null ? m_formule.GetString() : ""; }
        }

        //--------------------------------------------------------
        public C2iExpression Formule
        {
            get { return m_formule; }
            set { m_formule = value; }
        }

        //--------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( result )
                result =base.Serialize(serializer);
            if (!result) return result;

            result = serializer.TraiteObject<C2iExpression>(ref m_formule);
            if (!result)
                return result;
            return result;
        }

        //--------------------------------------------------------
        public override CResultAErreur GetValue(DataRow rowSource, CContexteImportDonnee contexteImport)
        {
            CResultAErreur result = CResultAErreur.True;
            if ( Formule != null )
            {
                CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(rowSource);
                ctx.AttacheObjet(typeof(CContexteDonnee), contexteImport.ContexteDonnee);
                result = Formule.Eval(ctx);
            }
            return result;
        }
    }

    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    [AutoExec("Autoexec")]
    [Serializable]
    public class CSourceSmartImportObjet : CSourceSmartImport
    {
        //--------------------------------------------------------
        public CSourceSmartImportObjet()
            : base()
        {
        }

        //--------------------------------------------------------
        public static void Autoexec()
        {
            CSourceSmartImport.RegisterTypeSource(typeof(CSourceSmartImportObjet), I.T("Object|20091"));
        }

        //--------------------------------------------------------
        public override Bitmap GetImage()
        {
            return Resource.Import_Object;
        }

       
        //--------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------
        public override string LibelleSource
        {
            get { return "Object"; }
        }

        //--------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result) return result;
            return result;
        }

        //--------------------------------------------------------
        public override CResultAErreur GetValue(DataRow rowSource, CContexteImportDonnee contexteImport)
        {
            throw new Exception("Can not call GetValue on CSourceSmartImportObjet");
        }
    }

    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    //--------------------------------------------------------
    /// <summary>
    /// La source est une valeur importée précédement
    /// </summary>
    [AutoExec("Autoexec")]
    [Serializable]
    public class CSourceSmartImportReference : CSourceSmartImport
    {
        private string m_strIdMappageReference = "";
        //--------------------------------------------------------
        public CSourceSmartImportReference()
            : base()
        {
        }

        //--------------------------------------------------------
        public static void Autoexec()
        {
            CSourceSmartImport.RegisterTypeSource(typeof(CSourceSmartImportReference), I.T("Imported object|20091"));
        }

        //--------------------------------------------------------
        public override Bitmap GetImage()
        {
            return Resource.Import_Object_ref;
        }

        //--------------------------------------------------------
        public override string LibelleSource
        {
            get { return "Reference to " + m_strIdMappageReference; }
        }

        //--------------------------------------------------------
        //Indique le mappage parent qui a importé l'objet
        public string IdMappageReference
        {
            get
            {
                return m_strIdMappageReference;
            }
            set { m_strIdMappageReference = value; }
        }


        //--------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (result)
                serializer.TraiteString(ref m_strIdMappageReference);
            return result;
        }

        //--------------------------------------------------------
        public override CResultAErreur GetValue(DataRow rowSource, CContexteImportDonnee contexteImport)
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_strIdMappageReference != null)
                result.Data = contexteImport.GetObjetImporteForIdMappage(m_strIdMappageReference);
            return result;
        }
    }
}
