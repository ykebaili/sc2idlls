using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using sc2i.common;

namespace sc2i.expression
{
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueDataColumn : CDefinitionProprieteDynamique
	{
		private const string c_strCleType = "DT";

		public CDefinitionProprieteDynamiqueDataColumn()
			: base()
		{
		}

		
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueDataColumn));
		}

		public CDefinitionProprieteDynamiqueDataColumn(DataColumn col)
			: base(col.ColumnName, col.ColumnName,
			new CTypeResultatExpression(col.DataType, false),
			false,
			false)
		{
		}

		//------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		
	}

	public class CInterpreteurProprieteDynamiqueDataColumn : IInterpreteurProprieteDynamique
	{
		//------------------------------------------------------------
		public bool ShouldIgnoreForSetValue(string strPropriete)
		{
			return false;
		}

		//------------------------------------------------------------
		public sc2i.common.CResultAErreur GetValue(object objet, string strPropriete)
		{
			CResultAErreur result = CResultAErreur.True;
			DataRow row = objet as DataRow;
            if (row == null)
            {
                try
                {
                    row = C2iConvert.ChangeType<DataRow>(objet);
                }
                catch { }
            }
			if (row == null)
				return result;
			if (row.Table.Columns.Contains(strPropriete))
				result.Data = row[strPropriete];
			return result;
		}

        //------------------------------------------------------------
		public CResultAErreur SetValue ( object objet, string strPropriete, object valeur )
		{
			CResultAErreur result = CResultAErreur.True;
			DataRow row = objet as DataRow;
			if ( row == null )
				return result;
			try
			{
				if ( valeur == null )
					valeur = DBNull.Value;
				row[strPropriete] = valeur;
			}
			catch ( Exception e)
			{
				result.EmpileErreur ( new CErreurException(e));
				result.EmpileErreur(I.T("Error while affecting value|20003"));
			}
			return result;
		}

        //------------------------------------------------------------
        public class COptimiseurProprieteDataColumn : IOptimiseurGetValueDynamic
        {
            private string m_strNomColonne = "";

            //------------------------------------------------------------
            public COptimiseurProprieteDataColumn ( string strNomColonne )
            {
                m_strNomColonne = strNomColonne;
            }
            //------------------------------------------------------------
            public object  GetValue(object objet)
            {
                DataRow row = objet as DataRow;
                try
                {
                if ( row != null )
                {
                    return row[m_strNomColonne];
                }
                }
                catch 
                {
                }
                return null;
            }

            //------------------------------------------------------------
            public Type GetTypeRetourne()
            {
                return typeof(object);
            }
        }
        //------------------------------------------------------------
        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            return new COptimiseurProprieteDataColumn(strPropriete);
        }
	}
}
