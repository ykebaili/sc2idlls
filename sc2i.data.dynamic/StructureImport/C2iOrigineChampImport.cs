using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.common;

namespace sc2i.data.dynamic.StructureImport
{
    public abstract class C2iOrigineChampImport : I2iSerializable
    {
        public abstract string NomChamp { get; }
        public abstract Type TypeDonnee{get;}
    
        //----------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }
        
        //----------------------------------------------
        public virtual CResultAErreur  Serialize(C2iSerializer serializer)
        {
 	        int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;

            return result;
        }

        //----------------------------------------------
        public abstract object GetValeur(object objet);


    }

    public class C2iOrigineChampImportDataColumn : C2iOrigineChampImport
    {
        private string m_strNomChampOrigine;
        private Type m_typeDonnee;

        //----------------------------------------------
        public C2iOrigineChampImportDataColumn()
            :base()
        {
        }

        //----------------------------------------------
        public C2iOrigineChampImportDataColumn(DataColumn colonne)
        {
            m_strNomChampOrigine = colonne.ColumnName;
            m_typeDonnee = colonne.DataType;
        }

        //----------------------------------------------
        public override string NomChamp
        {
            get{
                return m_strNomChampOrigine;
            }
        }

        //----------------------------------------------
        public override Type TypeDonnee
        {
            get{
                return m_typeDonnee;
            }
        }

        //----------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------
        public override CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( result )
                result = base.Serialize ( serializer );
            if ( !result )
                return result;
            
            serializer.TraiteString ( ref m_strNomChampOrigine );
            serializer.TraiteType ( ref m_typeDonnee );
            return result;
        }

        //----------------------------------------------
        public override object GetValeur(object objet)
        {
            DataRow row = objet as DataRow;
            if (row == null)
                return null;
            if (!row.Table.Columns.Contains(NomChamp))
                return null;
            object val = row[NomChamp];
            if (val == DBNull.Value)
                return null;
            return val;
        }
    }
}
