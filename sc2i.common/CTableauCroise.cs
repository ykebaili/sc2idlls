using System;
using System.Collections;
using System.Data;
using System.Linq;

using sc2i.common;
using System.Collections.Generic;

namespace sc2i.common
{

    /// <summary>
    /// Types de cumuls croisés possibles
    /// </summary>
    public enum TypeCumulCroise
    {
        Somme = 0,
        Nombre,
        NombreDistinct,
        Max,
        Min,
        Moyenne,
        Concat
    };

    public class CEnumTypeCumulCroise : CEnumALibelle<TypeCumulCroise>
    {
        public CEnumTypeCumulCroise(TypeCumulCroise cumul) :
            base(cumul)
        { }

        public override string Libelle
        {
            get
            {
                switch (Code)
                {
                    case TypeCumulCroise.Somme:
                        return I.T("Sum|110");
                    case TypeCumulCroise.Nombre:
                        return I.T("Number|111");
                    case TypeCumulCroise.NombreDistinct:
                        return I.T("Distinct Number|112");
                    case TypeCumulCroise.Max:
                        return I.T("Max|113");
                    case TypeCumulCroise.Min:
                        return I.T("Min|114");
                    case TypeCumulCroise.Moyenne:
                        return I.T("Average|115");
                    case TypeCumulCroise.Concat:
                        return I.T("Concat|116");
                }
                return "?";
            }
        }
    }


    #region CCleTableauCroise
    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.CCleTableauCroise")]
    public class CCleTableauCroise : I2iSerializable
    {
        private string m_strNomChamp = "";
        private Type m_typeChamp = null;

        public CCleTableauCroise()
        {
        }

        public CCleTableauCroise(string strNomChamp, Type typeChamp)
        {
            m_strNomChamp = strNomChamp;
            m_typeChamp = typeChamp;
        }

        public string NomChamp
        {
            get
            {
                return m_strNomChamp;
            }
            set
            {
                m_strNomChamp = value;
            }
        }

        public Type TypeDonnee
        {
            get
            {
                return m_typeChamp;
            }
            set
            {
                m_typeChamp = value;
            }
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNomChamp);
            serializer.TraiteType(ref m_typeChamp);
            return result;
        }
    }
    #endregion

    #region Classe CCumulCroise
    /// <summary>
    /// représente un cumul dans un tableau croisé
    /// </summary>
    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.CCumulCroise")]
    public class CCumulCroise : I2iSerializable
    {
        //Nom du champ à cumuler
        private string m_strNomChamp = "";
        private TypeCumulCroise m_typeCumul = TypeCumulCroise.Somme;
        private string m_strPrefixFinal = "";
        private Type m_typeDonneeSource = typeof(int);

        //Indique que ce cumul ne créera qu'une seul colonne et non pas une par valeur de colonne pivot
        private bool m_bHorsPivot = false;

        /// //////////////////////////////////////////////////////
        public CCumulCroise()
        {
        }

        /// //////////////////////////////////////////////////////
        public CCumulCroise(
            string strNomChamp,
            TypeCumulCroise typeCumul,
            Type typeDonneeSource)
        {
            m_strNomChamp = strNomChamp;
            m_typeCumul = typeCumul;
        }

        /// //////////////////////////////////////////////////////
        public string NomChamp
        {
            get
            {
                return m_strNomChamp;
            }
            set
            {
                m_strNomChamp = value;
            }
        }

        /// //////////////////////////////////////////////////////
        public TypeCumulCroise TypeCumul
        {
            get
            {
                return m_typeCumul;
            }
            set
            {
                m_typeCumul = value;
            }
        }

        /// //////////////////////////////////////////////////////
        public Type TypeDonneeSource
        {
            get
            {
                if (m_typeDonneeSource == null)
                    m_typeDonneeSource = typeof(double);
                return m_typeDonneeSource;
            }
            set
            {
                m_typeDonneeSource = value;
            }
        }

        /// //////////////////////////////////////////////////////
        public Type TypeDonneeFinal
        {
            get
            {
                try
                {
                    return COperateurCumul.GetNewOperateur(this).GetTypeFinal(TypeDonneeSource);
                }
                catch
                {
                    return typeof(double);
                }
            }
        }


        /// //////////////////////////////////////////////////////
        public string PrefixFinal
        {
            get
            {
                return m_strPrefixFinal;
            }
            set
            {
                m_strPrefixFinal = value;
            }
        }

        /// //////////////////////////////////////////////////////
        public bool HorsPivot
        {
            get
            {
                return m_bHorsPivot;
            }
            set
            {
                m_bHorsPivot = value;
            }
        }

        /// //////////////////////////////////////////////////////
        private int GetNumVersion()
        {
            return 2;
        }

        /// //////////////////////////////////////////////////////
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNomChamp);
            serializer.TraiteString(ref m_strPrefixFinal);

            int nType = (int)m_typeCumul;
            serializer.TraiteInt(ref nType);
            m_typeCumul = (TypeCumulCroise)nType;

            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bHorsPivot);
            if (nVersion >= 2)
            {
                if (m_typeDonneeSource == null)
                    m_typeDonneeSource = typeof(double);
                serializer.TraiteType(ref m_typeDonneeSource);
            }
            if (m_typeDonneeSource == null)
                m_typeDonneeSource = typeof(double);
            return result;
        }

        public static bool IsTypeCompatibleWithCumul(Type typeDonnee, TypeCumulCroise typeCumul)
        {
            switch (typeCumul)
            {
                case TypeCumulCroise.Nombre:
                case TypeCumulCroise.NombreDistinct:
                case TypeCumulCroise.Concat:
                    return true;
                case TypeCumulCroise.Max:
                case TypeCumulCroise.Min:
                    return typeof(IComparable).IsAssignableFrom(typeDonnee);
                case TypeCumulCroise.Somme:
                case TypeCumulCroise.Moyenne:
                    return typeof(double).IsAssignableFrom(typeDonnee) || typeof(int).IsAssignableFrom(typeDonnee) ||
                        typeof(Decimal).IsAssignableFrom(typeDonnee);
            }
            return false;
        }

    }
    #endregion

    #region Operateurs de cumul

    #region classe COperateurCumul
    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.COperateurCumul")]
    public abstract class COperateurCumul
    {
        protected CCumulCroise m_cumul = null;
        protected DataColumn m_colonneFinale = null;

        public COperateurCumul(CCumulCroise cumul)
        {
            m_cumul = cumul;
        }

        public static COperateurCumul GetNewOperateur(CCumulCroise cumul)
        {
            switch (cumul.TypeCumul)
            {
                case TypeCumulCroise.Max:
                    return new COperateurCumulMinMax(cumul, true);
                case TypeCumulCroise.Min:
                    return new COperateurCumulMinMax(cumul, false);
                case TypeCumulCroise.Moyenne:
                    return new COperateurCumulMoyenne(cumul);
                case TypeCumulCroise.Nombre:
                    return new COperateurCumulNombre(cumul);
                case TypeCumulCroise.NombreDistinct:
                    return new COperateurCumulNombreDistinct(cumul);
                case TypeCumulCroise.Somme:
                    return new COperateurCumulSomme(cumul);
                case TypeCumulCroise.Concat:
                    return new COperateurCumulConcat(cumul);
            }
            return null;
        }

        public abstract Type GetTypeFinal(Type typeSource);

        //Crée dans la table les colonnes nécéssaires au calcul
        /// <summary>
        /// Extended data sera stocké dans le extended data de la colonne (CROSS_KEY)
        /// </summary>
        /// <param name="tableDest"></param>
        /// <param name="strSuffixeColonne"></param>
        /// <param name="typeDonneesSource"></param>
        /// <param name="extendedData"></param>
        public abstract void PrepareTableForData(
            DataTable tableDest,
            string strSuffixeColonne,
            Type typeDonneesSource,
            object extendedData);

        public abstract void IntegreDonnee(DataRow rowDest, DataRow rowSource);

        public virtual CResultAErreur FinaliseCalcule(DataTable tableDest)
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

        public string GetNomColonne(string strSuffixe)
        {
            string strNom = m_cumul.PrefixFinal;
            if (strNom != "" && strSuffixe != "")
                strNom += "_";
            strNom += strSuffixe;
            return strNom;
        }

    }
    #endregion

    #region Concat
    class COperateurCumulConcat : COperateurCumul
    {
        public COperateurCumulConcat(CCumulCroise cumul)
            : base(cumul)
        {
        }

        public override Type GetTypeFinal(Type typeSource)
        {
            return typeof(string);
        }

        public override void PrepareTableForData(DataTable tableDest, string strSuffixeColonne, Type typeDonneesSource, object extendedData)
        {
            if (m_colonneFinale != null)
                return;
            m_colonneFinale = new DataColumn(GetNomColonne(strSuffixeColonne), typeof(string));
            m_colonneFinale.DefaultValue = "";
            m_colonneFinale.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] = extendedData;
            tableDest.Columns.Add(m_colonneFinale);
            return;
        }

        public override void IntegreDonnee(DataRow rowDest, DataRow rowSource)
        {
            object donnee = rowSource[m_cumul.NomChamp];
            string oldVal = (string)rowDest[m_colonneFinale];
            rowDest[m_colonneFinale] = oldVal + ((donnee != null && donnee != DBNull.Value) ? donnee.ToString() : "");
        }
    }
    #endregion

    #region MinMax
    [ReplaceClass("sc2i.data.dynamic.COperateurCumulMinMax")]
    public class COperateurCumulMinMax : COperateurCumul
    {
        private bool m_bIsMax = false;

        /// ///////////////////////////////////////////////////////
        public COperateurCumulMinMax(CCumulCroise cumul, bool bIsMax)
            : base(cumul)
        {
            m_bIsMax = bIsMax;
        }

        /// ///////////////////////////////////////////////////////
        public override void PrepareTableForData(
            DataTable tableDest,
            string strSuffixeColonne,
            Type typeDonnee,
            object extendedData)
        {
            if (m_colonneFinale != null)
                return;
            m_colonneFinale = new DataColumn(GetNomColonne(strSuffixeColonne), typeDonnee);
            m_colonneFinale.AllowDBNull = true;
            m_colonneFinale.DefaultValue = DBNull.Value;
            m_colonneFinale.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] = extendedData;
            tableDest.Columns.Add(m_colonneFinale);
            return;
        }

        /// ///////////////////////////////////////////////////////
        public override Type GetTypeFinal(Type typeSource)
        {
            return typeSource;
        }

        /// ///////////////////////////////////////////////////////
        public override void IntegreDonnee(DataRow rowDest, DataRow rowSource)
        {
            object donnee = rowSource[m_cumul.NomChamp];
            object oldVal = rowDest[m_colonneFinale];
            if (oldVal == DBNull.Value)
            {
                rowDest[m_colonneFinale] = donnee;
                return;
            }
            if (donnee is IComparable)
            {
                int nResultCompare = ((IComparable)donnee).CompareTo(oldVal);
                if (m_bIsMax && nResultCompare > 0)
                    rowDest[m_colonneFinale] = donnee;
                if (!m_bIsMax && nResultCompare < 0)
                    rowDest[m_colonneFinale] = donnee;
            }
        }

    }
    #endregion

    #region moyenne
    [ReplaceClass("sc2i.data.dynamic.COperateurCumulMoyenne")]
    public class COperateurCumulMoyenne : COperateurCumul
    {
        //La colonne finale stocke le cumul des valeurs

        //La colonne nombre stocke le nombre de valeurs
        //A la fin, la première et divisée par le seconde,
        //le resultat est stocké dans le colonne finale et la colonne
        //Nombre est supprimée
        private DataColumn m_colonneNombre = null;

        /// ///////////////////////////////////////////////////////
        public COperateurCumulMoyenne(CCumulCroise cumul)
            : base(cumul)
        {
        }

        /// ///////////////////////////////////////////////////////
        public override void PrepareTableForData(
            DataTable tableDest,
            string strSuffixeColonne,
            Type typeDonnee,
            object extendedData)
        {
            if (m_colonneFinale != null)
                return;
            m_colonneFinale = new DataColumn(GetNomColonne(strSuffixeColonne), typeof(double));
            m_colonneFinale.DefaultValue = 0;
            m_colonneFinale.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] = extendedData;
            tableDest.Columns.Add(m_colonneFinale);
            m_colonneNombre = new DataColumn(GetNomColonne(strSuffixeColonne) + "_NB", typeof(double));
            m_colonneNombre.DefaultValue = 0;
            tableDest.Columns.Add(m_colonneNombre);
            return;
        }

        /// ///////////////////////////////////////////////////////
        public override Type GetTypeFinal(Type typeSource)
        {
            return typeof(double);
        }

        /// ///////////////////////////////////////////////////////
        public override void IntegreDonnee(DataRow rowDest, DataRow rowSource)
        {
            object donnee = rowSource[m_cumul.NomChamp];
            double fOldSm, fOldNb;
            fOldSm = (double)rowDest[m_colonneFinale];
            fOldNb = (double)rowDest[m_colonneNombre];
            if (donnee == DBNull.Value || donnee == null)
                return;

            rowDest[m_colonneFinale] = fOldSm + Convert.ToDouble(donnee); ;
            rowDest[m_colonneNombre] = fOldNb + 1;
        }

        /// ///////////////////////////////////////////////////////
        public override CResultAErreur FinaliseCalcule(DataTable tableDest)
        {
            CResultAErreur result = CResultAErreur.True;
            //Fait les divisions
            foreach (DataRow row in tableDest.Rows)
            {
                if (((double)row[m_colonneNombre]) != 0)
                    row[m_colonneFinale] = ((double)row[m_colonneFinale]) / ((double)row[m_colonneNombre]);
                else
                    row[m_colonneFinale] = 0;
            }
            tableDest.Columns.Remove(m_colonneNombre);
            return result;
        }

    }
    #endregion

    #region Nombre
    [ReplaceClass("sc2i.data.dynamic.COperateurCumulNombre")]
    public class COperateurCumulNombre : COperateurCumul
    {
        /// ///////////////////////////////////////////////////////
        public COperateurCumulNombre(CCumulCroise cumul)
            : base(cumul)
        {

        }

        /// ///////////////////////////////////////////////////////
        public override void PrepareTableForData(
            DataTable tableDest,
            string strSuffixeColonne,
            Type typeDonnee,
            object extendedData)
        {
            if (m_colonneFinale != null)
                return;
            m_colonneFinale = new DataColumn(GetNomColonne(strSuffixeColonne), typeof(int));
            m_colonneFinale.DefaultValue = 0;
            m_colonneFinale.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] = extendedData;
            tableDest.Columns.Add(m_colonneFinale);
            return;
        }

        /// ///////////////////////////////////////////////////////
        public override Type GetTypeFinal(Type typeSource)
        {
            return typeof(int);
        }

        /// ///////////////////////////////////////////////////////
        public override void IntegreDonnee(DataRow rowDest, DataRow rowSource)
        {
            object donnee = rowSource[m_cumul.NomChamp];
            int oldVal = (int)rowDest[m_colonneFinale];
            rowDest[m_colonneFinale] = oldVal + 1;
        }
    }
    #endregion

    #region NombreDistinct
    [ReplaceClass("sc2i.data.dynamic.COperateurCumulNombreDistinct")]
    public class COperateurCumulNombreDistinct : COperateurCumul
    {
        //Pour chaque ligne, la liste des valeurs est stockée dans une hashtable
        //en fin de calcul, le nombre d'éléments de la hashtable est copiée
        //Dans la colonne finale.
        DataColumn m_colonneHashtable = null;
        /// ///////////////////////////////////////////////////////
        public COperateurCumulNombreDistinct(CCumulCroise cumul)
            : base(cumul)
        {

        }

        /// ///////////////////////////////////////////////////////
        public override void PrepareTableForData(
            DataTable tableDest,
            string strSuffixeColonne,
            Type typeDonnee,
            object extendedData)
        {
            if (m_colonneFinale != null)
                return;
            m_colonneFinale = new DataColumn(GetNomColonne(strSuffixeColonne), typeof(int));
            m_colonneFinale.DefaultValue = 0;
            tableDest.Columns.Add(m_colonneFinale);
            m_colonneFinale.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] = extendedData;
            m_colonneHashtable = new DataColumn(GetNomColonne(strSuffixeColonne) + "_VALUES", typeof(Hashtable));
            m_colonneHashtable.AllowDBNull = true;
            m_colonneHashtable.DefaultValue = DBNull.Value;
            tableDest.Columns.Add(m_colonneHashtable);
            return;
        }

        /// ///////////////////////////////////////////////////////
        public override Type GetTypeFinal(Type typeSource)
        {
            return typeof(int);
        }

        /// ///////////////////////////////////////////////////////
        public override void IntegreDonnee(DataRow rowDest, DataRow rowSource)
        {
            object donnee = rowSource[m_cumul.NomChamp];
            Hashtable table = rowDest[m_colonneHashtable] as Hashtable;
            if (table == null)
            {
                table = new Hashtable();
                rowDest[m_colonneHashtable] = table;
            }


            table[donnee] = true;
        }

        /// ///////////////////////////////////////////////////////
        public override CResultAErreur FinaliseCalcule(DataTable tableDest)
        {
            CResultAErreur result = CResultAErreur.True;
            foreach (DataRow row in tableDest.Rows)
            {
                if (row[m_colonneHashtable] != DBNull.Value)
                {
                    Hashtable table = (Hashtable)row[m_colonneHashtable];
                    row[m_colonneFinale] = table.Count;
                }
            }
            tableDest.Columns.Remove(m_colonneHashtable);
            return result;
        }

    }
    #endregion

    #region Somme
    [ReplaceClass("sc2i.data.dynamic.COperateurCumulSomme")]
    public class COperateurCumulSomme : COperateurCumul
    {
        /// ///////////////////////////////////////////////////////
        public COperateurCumulSomme(CCumulCroise cumul)
            : base(cumul)
        {

        }

        /// ///////////////////////////////////////////////////////
        public override void PrepareTableForData(
            DataTable tableDest,
            string strSuffixeColonne,
            Type typeDonnee,
            object extendedData)
        {
            if (m_colonneFinale != null)
                return;
            m_colonneFinale = new DataColumn(GetNomColonne(strSuffixeColonne), typeof(double));
            m_colonneFinale.DefaultValue = 0;
            m_colonneFinale.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] = extendedData;
            tableDest.Columns.Add(m_colonneFinale);
            return;
        }

        /// ///////////////////////////////////////////////////////
        public override Type GetTypeFinal(Type typeSource)
        {
            return typeof(double);
        }

        /// ///////////////////////////////////////////////////////
        public override void IntegreDonnee(DataRow rowDest, DataRow rowSource)
        {
            object donnee = rowSource[m_cumul.NomChamp];
            try
            {
                double fOldVal = (double)rowDest[m_colonneFinale];
                rowDest[m_colonneFinale] = fOldVal + Convert.ToDouble(donnee);
            }
            catch
            {
            }
        }
    }
    #endregion


    #endregion

    #region Classe CColonneePivot
    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.CColonneePivot")]
    public class CColonneePivot : I2iSerializable
    {
        private string m_strNomChamp = "";
        private string m_strPrefix = "";
        private string[] m_strValeursSystematiques = new string[0];

        /// //////////////////////////////////////////
        public CColonneePivot()
        {
        }

        /// //////////////////////////////////////////
        public CColonneePivot(string strNomChamp, string strPrefix)
        {
            m_strNomChamp = strNomChamp;
            m_strPrefix = strPrefix;
        }

        /// //////////////////////////////////////////
        public string NomChamp
        {
            get
            {
                return m_strNomChamp;
            }
            set
            {
                m_strNomChamp = value;
            }
        }

        /// //////////////////////////////////////////
        public string Prefixe
        {
            get
            {
                return m_strPrefix;
            }
            set
            {
                m_strPrefix = value;
            }
        }

        /// //////////////////////////////////////////
        public string[] ValeursSystematiques
        {
            get
            {
                return m_strValeursSystematiques;
            }
            set
            {
                m_strValeursSystematiques = value;
            }
        }

        /// //////////////////////////////////////////
        private int GetNumVersion()
        {
            return 1;
            //1 : Ajout des valeurs systématiques
        }

        /// //////////////////////////////////////////
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strNomChamp);
            serializer.TraiteString(ref m_strPrefix);

            if (nVersion >= 1)
            {
                IList lst = new ArrayList(m_strValeursSystematiques);
                serializer.TraiteListeObjetsSimples(ref lst);
                if (lst != null && serializer.Mode == ModeSerialisation.Lecture)
                {
                    m_strValeursSystematiques = new string[lst.Count];
                    int nIndex = 0;
                    foreach (string strVal in lst)
                        m_strValeursSystematiques[nIndex++] = strVal;
                }
            }
            else
                m_strValeursSystematiques = new string[0];
            return result;
        }

    }
    #endregion
    [ReplaceClass("sc2i.data.dynamic.CChampFinalDeTableauCroise")]
    public abstract class CChampFinalDeTableauCroise : I2iSerializable
    {
        private Type m_typeDonnees = typeof(string);

        public CChampFinalDeTableauCroise()
        {
        }

        //------------------------------
        public Type TypeDonnee
        {
            get
            {
                return m_typeDonnees;
            }
            set
            {
                m_typeDonnees = value;
            }
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteType(ref m_typeDonnees);
            return result;
        }

        public abstract string NomChamp { get; set; }


    }

    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.CChampFinalDeTableauCroiseCle")]
    public class CChampFinalDeTableauCroiseCle : CChampFinalDeTableauCroise
    {
        private string m_strNomChamp;

        public CChampFinalDeTableauCroiseCle(string strNomChamp, Type type)
        {
            m_strNomChamp = strNomChamp;
            TypeDonnee = type;
        }

        public CChampFinalDeTableauCroiseCle()
        {
        }

        public override string NomChamp
        {
            get
            {
                return m_strNomChamp;
            }
            set
            {
                m_strNomChamp = value;
            }
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;

            serializer.TraiteString(ref m_strNomChamp);
            return result;
        }
    }

    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.CChampFinalDeTableauCroiseDonnee")]
    public class CChampFinalDeTableauCroiseDonnee : CChampFinalDeTableauCroise
    {
        private CColonneePivot m_colonnePivot = null;
        private CCumulCroise m_cumulCroise = null;

        public CChampFinalDeTableauCroiseDonnee()
        {
        }

        public CChampFinalDeTableauCroiseDonnee(
            CColonneePivot pivot,
            CCumulCroise cumul,
            Type typeDonnee)
        {
            m_colonnePivot = pivot;
            m_cumulCroise = cumul;
            TypeDonnee = typeDonnee;
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;

            result = serializer.TraiteObject<CColonneePivot>(ref m_colonnePivot);
            if (!result)
                return result;
            result = serializer.TraiteObject<CCumulCroise>(ref m_cumulCroise);
            if (!result)
                return result;

            return result;
        }

        public override string NomChamp
        {
            get
            {
                string strNom = "";
                if (m_cumulCroise.HorsPivot && m_cumulCroise.PrefixFinal.Length > 0)
                    return m_cumulCroise.PrefixFinal;
                if (m_colonnePivot != null)
                    strNom = new CEnumTypeCumulCroise(m_cumulCroise.TypeCumul).Libelle+" "+
                        m_colonnePivot.NomChamp + "/";
                strNom += m_cumulCroise.NomChamp;
                return strNom;
            }
            set
            {
            }
        }

        public CColonneePivot Pivot
        {
            get
            {
                return m_colonnePivot;
            }
        }

        public CCumulCroise CumulCroise
        {
            get
            {
                return m_cumulCroise;
            }
        }
    }

    /// <summary>
    /// Utilisé dans les extendedProperties des datacolumn
    /// pour savoir à quel champ la colonne correspond
    /// </summary>
    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.CChampFinalDetableauCroiseDonneeAvecValeur")]
    public class CChampFinalDetableauCroiseDonneeAvecValeur : CChampFinalDeTableauCroiseDonnee
    {
        public CChampFinalDetableauCroiseDonneeAvecValeur(
            CColonneePivot colonne,
            CCumulCroise cumul,
            object valeur)
            : base(colonne, cumul, cumul.TypeDonneeFinal)//valeur == null ? typeof(string) : valeur.GetType())
        {
            ValeurPivot = valeur;
        }

        public object ValeurPivot { get; set; }

        //------------------------------------------------------------------------
        public override string NomChamp
        {
            get
            {
                string strNom = "";
                if ( CumulCroise != null && CumulCroise.PrefixFinal.Length > 0 )
                    strNom += CumulCroise.PrefixFinal+"_";
                if ( Pivot != null && Pivot.Prefixe.Length > 0 )
                    strNom += Pivot.Prefixe+"_";
                if ( ValeurPivot != null )
                    strNom += ValeurPivot.ToString();
                return strNom;
            }
            set
            {
                
            }
        }
    }






    #region classe CTableauCroise
    /// <summary>
    /// Permet de réaliser un tableau croisé sur un datatable
    /// </summary>
    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.CTableauCroise")]
    public class CTableauCroise : I2iSerializable
    {
        public static string c_ExtendedPropertyToColumnKey = "CROSS_KEY";
        /// <summary>
        /// Liste de string représentant les noms des colonnes conservées comme clé
        /// </summary>
        private List<CCleTableauCroise> m_listeChampsCles = new List<CCleTableauCroise>();

        /// <summary>
        /// Liste des CColonneePivot représentant les champs de base pour les colonnes.
        /// </summary>
        private List<CColonneePivot> m_listeChampsColonne = new List<CColonneePivot>();

        /// <summary>
        /// Liste de CCumulCroise représentant les cumuls à réaliser
        /// </summary>
        private List<CCumulCroise> m_listeTotaux = new List<CCumulCroise>();

        /// //////////////////////////////////////////
        public CTableauCroise()
        {
        }

        /// //////////////////////////////////////////
        /// <summary>
        /// Liste des noms des champs clés
        /// </summary>
        public CCleTableauCroise[] ChampsCle
        {
            get
            {
                return m_listeChampsCles.ToArray();
            }
            set
            {
                m_listeChampsCles = new List<CCleTableauCroise>(value);
            }
        }

        /// //////////////////////////////////////////
        public void AddChampCle(CCleTableauCroise cle)
        {
            m_listeChampsCles.Add(cle);
        }

        /// //////////////////////////////////////////
        public void InsertChampCle(int nPos, CCleTableauCroise cle)
        {
            m_listeChampsCles.Insert(nPos, cle);
        }

        /// //////////////////////////////////////////
        public void RemoveChampCle(CCleTableauCroise cle)
        {
            m_listeChampsCles.Remove(cle);
        }



        /// //////////////////////////////////////////
        /// <summary>
        /// Liste des champs qui passent en colonne ( 1 champ par valeur )
        /// </summary>
        public CColonneePivot[] ChampsColonne
        {
            get
            {
                return m_listeChampsColonne.ToArray();
            }
            set
            {
                m_listeChampsColonne = new List<CColonneePivot>(value);
            }
        }

        /// //////////////////////////////////////////
        public void AddChampColonne(CColonneePivot pivot)
        {
            m_listeChampsColonne.Add(pivot);
        }

        /// //////////////////////////////////////////
        public void RemoveChampColonne(CColonneePivot pivot)
        {
            m_listeChampsColonne.Remove(pivot);
        }

        /// //////////////////////////////////////////
        /// <summary>
        /// Liste des champs à totaliser
        /// </summary>
        public CCumulCroise[] CumulsCroises
        {
            get
            {
                return m_listeTotaux.ToArray();
            }
            set
            {
                m_listeTotaux = new List<CCumulCroise>(value);
            }
        }

        /// //////////////////////////////////////////
        public void AddCumul(CCumulCroise cumul)
        {
            m_listeTotaux.Add(cumul);
        }

        /// //////////////////////////////////////////
        public void RemoveCumul(CCumulCroise cumul)
        {
            m_listeTotaux.Remove(cumul);
        }

        /// //////////////////////////////////////////
        private int GetNumVersion()
        {
            return 1;
            ///V1 : Remplacement des arraylist par des listes, remplacement de la clé par un CCle
            ///
        }

        /// //////////////////////////////////////////
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            if (nVersion < 1)
            {
                #region compatiblité V0
                IList lst = new ArrayList();
                foreach (CCleTableauCroise cle in lst)
                    lst.Add(cle.NomChamp);
                serializer.TraiteListeObjetsSimples(ref lst);
                List<CCleTableauCroise> lstCles = new List<CCleTableauCroise>();
                foreach (string strCle in lst)
                {
                    lstCles.Add(new CCleTableauCroise(strCle, typeof(string)));
                }
                m_listeChampsCles = lstCles;

                ArrayList tmpListe = new ArrayList(m_listeChampsColonne);
                result = serializer.TraiteArrayListOf2iSerializable(tmpListe);
                if (!result)
                    return result;
                m_listeChampsColonne = new List<CColonneePivot>();
                foreach (CColonneePivot colonne in tmpListe)
                    m_listeChampsColonne.Add(colonne);

                tmpListe = new ArrayList(m_listeTotaux);
                result = serializer.TraiteArrayListOf2iSerializable(tmpListe);
                if (!result)
                    return result;
                m_listeTotaux = new List<CCumulCroise>();
                foreach (CCumulCroise cumul in tmpListe)
                    m_listeTotaux.Add(cumul);
                #endregion
            }
            else
            {
                result = serializer.TraiteListe<CCleTableauCroise>(m_listeChampsCles);
                if (result)
                    result = serializer.TraiteListe<CColonneePivot>(m_listeChampsColonne);
                if (result)
                    result = serializer.TraiteListe<CCumulCroise>(m_listeTotaux);
                if (!result)
                    return result;
            }
            return result;
        }

        /// //////////////////////////////////////////
        public void CopieFrom(CTableauCroise tableauSource)
        {
            m_listeChampsCles.Clear();
            m_listeChampsColonne.Clear();
            m_listeTotaux.Clear();
            m_listeChampsCles.AddRange(tableauSource.ChampsCle);
            m_listeChampsColonne.AddRange(tableauSource.ChampsColonne);
            m_listeTotaux.AddRange(tableauSource.CumulsCroises);
        }

        /// //////////////////////////////////////////
        /// <summary>
        /// Valide que les paramètres sont corrects pour une datatable donnée
        /// </summary>
        /// <returns></returns>
        public CResultAErreur VerifieDonnees(DataTable tableSource)
        {
            CResultAErreur result = CResultAErreur.True;
            //Vérifie l'existance des champs clés
            foreach (CCleTableauCroise cle in ChampsCle)
            {
                DataColumn col = tableSource.Columns[cle.NomChamp];
                if (col == null)
                    result.EmpileErreur(I.T("The column '@1' doesn't exist|197", cle.NomChamp));
            }

            Hashtable tablePivots = new Hashtable();
            //Vérifie l'existance des colonnes pivot
            foreach (CColonneePivot colonne in m_listeChampsColonne)
            {
                DataColumn col = tableSource.Columns[colonne.NomChamp];
                if (col == null)
                    result.EmpileErreur(I.T("The column '@1' doesn't exist|197", colonne.NomChamp));
                else
                {
                    if (m_listeChampsCles.Count(c => c.NomChamp == colonne.NomChamp) > 0)
                        result.EmpileErreur(I.T("The column '@1' cannot be simultaneously pivot and key|198", colonne.NomChamp));

                    tablePivots[colonne.NomChamp] = true;
                }
            }

            //Vérifie l'existence et le type de donnée des colonnes totaux
            foreach (CCumulCroise cumul in m_listeTotaux)
            {
                DataColumn col = tableSource.Columns[cumul.NomChamp];
                if (col == null)
                {
                    result.EmpileErreur(I.T("The column '@1' doesn't exist|197", cumul.NomChamp));
                }
                else
                {
                    if (m_listeChampsCles.Count(c => c.NomChamp == cumul.NomChamp) > 0)
                        result.EmpileErreur(I.T("The column '@1' cannot be simultaneously key and total|200", cumul.NomChamp));
                    if (tablePivots[cumul.NomChamp] != null)
                        result.EmpileErreur(I.T("The column '@1' cannot be simultaneously pivot and total|201", cumul.NomChamp));
                    if (!CCumulCroise.IsTypeCompatibleWithCumul(col.DataType, cumul.TypeCumul))
                        result.EmpileErreur(I.T("The column '@1' isn't compatible with the operation @2|202", cumul.NomChamp, cumul.TypeCumul.ToString()));
                }
            }
            return result;

        }

        /// //////////////////////////////////////////
        public CChampFinalDeTableauCroise[] ChampsFinaux
        {
            get
            {
                List<CChampFinalDeTableauCroise> lst = new List<CChampFinalDeTableauCroise>();
                foreach (CCleTableauCroise cle in ChampsCle)
                {
                    lst.Add(new CChampFinalDeTableauCroiseCle(cle.NomChamp, cle.TypeDonnee));
                }
                foreach (CColonneePivot colonnePivot in ChampsColonne)
                {
                    foreach (CCumulCroise cumul in CumulsCroises)
                    {
                        if (!cumul.HorsPivot)
                        {
                            lst.Add(new CChampFinalDeTableauCroiseDonnee(colonnePivot, cumul, cumul.TypeDonneeFinal));
                            if ( colonnePivot.ValeursSystematiques != null )
                                foreach ( string strValS in colonnePivot.ValeursSystematiques )
                                    lst.Add ( new CChampFinalDetableauCroiseDonneeAvecValeur (
                                        colonnePivot,
                                        cumul,
                                        strValS));
                        }
                    }
                }
                foreach (CCumulCroise cumul in CumulsCroises)
                {
                    if (cumul.HorsPivot)
                        lst.Add(new CChampFinalDeTableauCroiseDonnee(null, cumul, cumul.TypeDonneeFinal));
                }
                return lst.ToArray();
            }
        }

        /// //////////////////////////////////////////
        public static string GetNomColonneePivot(CColonneePivot colonne, string strVal)
        {
            string strNomColonne = (colonne.Prefixe != "" ? colonne.Prefixe + "_" : "") + strVal;
            return strNomColonne;
        }


        /// //////////////////////////////////////////
        /// <summary>
        /// Crée la table pivot à partir d'une table source
        /// </summary>
        /// <param name="tableSource"></param>
        /// <returns>Le data du result contient un datatable représentant la table croisée</returns>
        public CResultAErreur CreateTableCroisee(DataTable tableSource)
        {
            DataTable tableFinale = new DataTable();

            ArrayList listeOperateursCumul = new ArrayList();

            //Liste des operateurs hors pivot
            ArrayList listeOperateursHorsPivot = new ArrayList();

            CResultAErreur result = VerifieDonnees(tableSource);
            if (!result)
                return result;

            //Crée les colonnes de clé
            ArrayList lstCles = new ArrayList();
            foreach (CCleTableauCroise cle in m_listeChampsCles)
            {
                DataColumn colSource = tableSource.Columns[cle.NomChamp];
                DataColumn colCree = new DataColumn(colSource.ColumnName, colSource.DataType);
                colCree.ExtendedProperties[c_ExtendedPropertyToColumnKey] = new CChampFinalDeTableauCroiseCle(cle.NomChamp, cle.TypeDonnee);
                tableFinale.Columns.Add(colCree);
                lstCles.Add(colCree);
            }
            tableFinale.PrimaryKey = (DataColumn[])lstCles.ToArray(typeof(DataColumn));

            //Crée les colonnes de Cumul hors pivot
            foreach (CCumulCroise cumul in m_listeTotaux)
            {
                if (cumul.HorsPivot)
                {
                    COperateurCumul operateur = COperateurCumul.GetNewOperateur(cumul);
                    operateur.PrepareTableForData(
                        tableFinale,
                        "",
                        tableSource.Columns[cumul.NomChamp].DataType,
                        new CChampFinalDetableauCroiseDonneeAvecValeur(null, cumul, null));
                    listeOperateursHorsPivot.Add(operateur);
                }
            }

            //Colonne de pivot->Hashtable de valeurs de pivot->Liste des opérateurs
            Hashtable tableTablesValeursPivot = new Hashtable();
            //Crée les valeurs systématiques de pivots
            //Traite la ligne
            foreach (CColonneePivot colonne in m_listeChampsColonne)
            {
                Hashtable tablePivot = (Hashtable)tableTablesValeursPivot[colonne.NomChamp];
                if (tablePivot == null)
                {
                    tablePivot = new Hashtable();
                    tableTablesValeursPivot[colonne.NomChamp] = tablePivot;
                }
                foreach (string strValSystématique in colonne.ValeursSystematiques)
                {
                    string strValPivot = strValSystématique;
                    string strNomColonne = GetNomColonneePivot(colonne, strValPivot);
                    //Si c'est une nouvelle valeur de pivot, il faut créer les colonnes de cumul
                    ArrayList opsPivot = (ArrayList)tablePivot[strNomColonne];
                    if (opsPivot == null)
                    {
                        opsPivot = new ArrayList();
                        tablePivot[strNomColonne] = opsPivot;
                        foreach (CCumulCroise cumul in m_listeTotaux)
                        {
                            if (!cumul.HorsPivot)
                            {
                                COperateurCumul operateur = COperateurCumul.GetNewOperateur(cumul);
                                opsPivot.Add(operateur);
                                operateur.PrepareTableForData(
                                    tableFinale,
                                    strNomColonne,
                                    tableSource.Columns[cumul.NomChamp].DataType,
                                    new CChampFinalDetableauCroiseDonneeAvecValeur(colonne, cumul, strValPivot));
                                listeOperateursCumul.Add(operateur);
                            }
                        }
                    }
                }
            }




            //Création des lignes
            foreach (DataRow rowSource in tableSource.Rows)
            {
                ArrayList lstKeys = new ArrayList();
                foreach (CCleTableauCroise cle in m_listeChampsCles)
                    lstKeys.Add(rowSource[cle.NomChamp]);
                DataRow rowDest = tableFinale.Rows.Find((object[])lstKeys.ToArray(typeof(object)));
                if (rowDest == null)
                {
                    //Crée la ligne finale
                    rowDest = tableFinale.NewRow();
                    int nIndex = 0;
                    foreach (CCleTableauCroise cle in m_listeChampsCles)
                        rowDest[cle.NomChamp] = lstKeys[nIndex++];
                    tableFinale.Rows.Add(rowDest);
                }

                //Traite la ligne
                foreach (CColonneePivot colonne in m_listeChampsColonne)
                {
                    Hashtable tablePivot = (Hashtable)tableTablesValeursPivot[colonne.NomChamp];
                    if (tablePivot == null)
                    {
                        tablePivot = new Hashtable();
                        tableTablesValeursPivot[colonne.NomChamp] = tablePivot;
                    }
                    object valPivot = rowSource[colonne.NomChamp];
                    string strValPivot = "NULL";
                    if (valPivot != null)
                        strValPivot = valPivot.ToString();

                    string strNomColonne = GetNomColonneePivot(colonne, strValPivot);
                    //Si c'est une nouvelle valeur de pivot, il faut créer les colonnes de cumul
                    ArrayList opsPivot = (ArrayList)tablePivot[strNomColonne];
                    if (opsPivot == null)
                    {
                        opsPivot = new ArrayList();
                        tablePivot[strNomColonne] = opsPivot;
                        foreach (CCumulCroise cumul in m_listeTotaux)
                        {
                            if (!cumul.HorsPivot)
                            {
                                COperateurCumul operateur = COperateurCumul.GetNewOperateur(cumul);
                                opsPivot.Add(operateur);
                                operateur.PrepareTableForData(
                                    tableFinale,
                                    strNomColonne,
                                    tableSource.Columns[cumul.NomChamp].DataType,
                                    new CChampFinalDetableauCroiseDonneeAvecValeur(colonne, cumul, valPivot));
                                listeOperateursCumul.Add(operateur);
                            }
                        }
                    }
                    foreach (COperateurCumul operateur in opsPivot)
                    {
                        operateur.IntegreDonnee(rowDest, rowSource);
                    }
                }
                foreach (COperateurCumul operateur in listeOperateursHorsPivot)
                    operateur.IntegreDonnee(rowDest, rowSource);

            }
            foreach (COperateurCumul operateur in listeOperateursCumul)
            {
                result = operateur.FinaliseCalcule(tableFinale);
                if (!result)
                    return result;
            }
            foreach (COperateurCumul operateur in listeOperateursHorsPivot)
            {
                result = operateur.FinaliseCalcule(tableFinale);
                if (!result)
                    return result;
            }

            //Stef le 8/1/2011 : les valeurs systématiques sont créées avant
            /*
			//Vérifie que les valeurs systematiques des colonnes pivot ont bien été créées
			foreach ( CColonneePivot colonne in ChampsColonne )
			{
				foreach ( string strValeurSystematique in colonne.ValeursSystematiques )
				{
					string strNomColonne = GetNomColonneePivot ( colonne, strValeurSystematique );
					if ( tableFinale.Columns[strNomColonne] == null )
					{
						foreach ( CCumulCroise cumul in m_listeTotaux )
						{
							if ( !cumul.HorsPivot )
							{
								COperateurCumul operateur = COperateurCumul.GetNewOperateur ( cumul );
								try
								{
                                    operateur.PrepareTableForData(
                                        tableFinale, 
                                        strNomColonne, 
                                        tableSource.Columns[cumul.NomChamp].DataType, 
                                        new CChampFinalDetableauCroiseDonneeAvecValeur ( 
                                            colonne, cumul, strValeurSystematique) );
									operateur.FinaliseCalcule(tableFinale);
								}
								catch{}//Echoue car colonne existe déjà !!, donc pas de pb
								
							}
						}
					}
				}
			}*/
            //Fin modif 8/11/2011


            if (result)
                result.Data = tableFinale;
            return result;
        }
    }
    #endregion
}

