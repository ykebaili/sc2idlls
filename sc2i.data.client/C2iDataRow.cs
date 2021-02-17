using System;
using System.Data;

namespace sc2i.data
{
	/// <summary>
	/// Encapsule une DataRow. Lors d'un modification d'une valeur de colonne,
	/// la valeur n'est modifiée que si elle est différente de la valeur précédente
	/// </summary>
	public class C2iDataRow
	{
		private DataRow m_row = null;

		private DataRowVersion m_rowVersionToReturn = DataRowVersion.Default;


		/// //////////////////////////////////////////////////////
		public C2iDataRow( DataRow row)
		{
			m_row = row;
		}

		/// //////////////////////////////////////////////////////
		public C2iDataRow( DataRow row, DataRowVersion rowVersionToReturn )
		{
			m_row = row;
			m_rowVersionToReturn = rowVersionToReturn;
		}

		/// //////////////////////////////////////////////////////
		public virtual object this[DataColumn col, DataRowVersion version]
		{
			get
			{
				return m_row[col, version];
			}
		}

		private object ConvertDBNull(object valeur)
		{
			if (valeur == DBNull.Value)
				return null;
			return valeur;
		}

        /// //////////////////////////////////////////////////////
        public T Get<T>(string strColonne)
        {
            return (T)this[strColonne, true];
        }

		/// //////////////////////////////////////////////////////
		public virtual object this[string strCol, bool bConvertNullEnDbNull]
		{
			get
			{
				if (bConvertNullEnDbNull)
					return ConvertDBNull(Row[strCol, m_rowVersionToReturn]);
				return Row[strCol, m_rowVersionToReturn];
			}
			set
			{
                if (!IsEquivalent(m_row[strCol], value))
                {
                    if (bConvertNullEnDbNull && value == null)
                    {
                        if (Row[strCol] != DBNull.Value)
                            this[strCol] = DBNull.Value;
                    }
                    else
                    {
                        this[strCol] = value;
                    }
                }
			}
		}

		/// //////////////////////////////////////////////////////
		public virtual object this[string strCol, DataRowVersion version]
		{
			get
			{
				return m_row[strCol, version];
			}
		}

		/// //////////////////////////////////////////////////////
		public virtual object this[int nCol, DataRowVersion version]
		{
			get
			{
				return m_row[nCol, version];
			}
		}

        /// //////////////////////////////////////////////////////
        private void ChangeContexteModification()
        {
            try
            {
                if (m_row.Table != null && m_row.Table.DataSet != null)
                {
                    CContexteDonnee ctx = m_row.Table.DataSet as CContexteDonnee;
                    if (ctx != null)
                    {
                        if (m_row.RowState == DataRowState.Unchanged)//Premier changement : réinit
                            m_row[CObjetDonnee.c_champContexteModification] = "";

                        //Règle appliquée : on garde le premier contexte non vide
                        //utilisé pour modifier l'élément
                        if ((string)m_row[CObjetDonnee.c_champContexteModification] == "" &&
                            ctx.IdModificationContextuelle != "")
                        {
                            m_row[CObjetDonnee.c_champContexteModification] = ctx.IdModificationContextuelle;

                        }
                    }
                }
            }
            catch
            {
            }
        }

		/// //////////////////////////////////////////////////////
		public virtual object this[DataColumn col]
		{
			get
			{
				return m_row[col, m_rowVersionToReturn];
			}
			set
			{
                if ( !IsEquivalent ( m_row[col], value ) )
				/*if ( !m_row[col].Equals(value) || 
					(value is CDonneeBinaireInRow && ((CDonneeBinaireInRow)value).HasChange()))*/
				{
                    ChangeContexteModification();
					try
					{
						m_row[col] = value;
					}
					catch
					{
						if ( col.DataType == typeof(string) && value.ToString().Length > col.MaxLength )
							m_row[col] = value.ToString().Substring(0, col.MaxLength);
					}
				}

			}
		}

		/// //////////////////////////////////////////////////////
		public virtual object this[string strCol]
		{
			get
			{
				return m_row[strCol, m_rowVersionToReturn];
			}
			set
			{
                if ( !IsEquivalent(m_row[strCol], value) )
/*				if ( !m_row[strCol].Equals(value) || 
					(value is CDonneeBinaireInRow && ((CDonneeBinaireInRow)value).HasChange()))*/
				{
                    ChangeContexteModification();
					try
					{
						m_row[strCol] = value;
					}
					catch
					{
						DataColumn col = m_row.Table.Columns[strCol];
						this[col] = value;
					}
				}
			}
		}

		/// //////////////////////////////////////////////////////
		public virtual object this[int nCol]
		{
			get
			{
				return m_row[nCol, m_rowVersionToReturn];
			}
			set
			{
                if ( !IsEquivalent(m_row[nCol], value ) )
				/*if ( !m_row[nCol].Equals(value) || 
					(value is CDonneeBinaireInRow && ((CDonneeBinaireInRow)value).HasChange()))*/
				{
                    ChangeContexteModification();
					try
					{
						m_row[nCol] = value;
					}
					catch
					{
						DataColumn col = m_row.Table.Columns[nCol];
						
                        this[col] = value;
					}
				}
			}
		}

		/// //////////////////////////////////////////////////////
		public DataRow Row
		{
			get
			{
				return m_row;
			}
		}

		/// //////////////////////////////////////////////////////
		public static implicit operator DataRow ( C2iDataRow row2i )
		{
			return row2i.Row;
		}

		/// //////////////////////////////////////////////////////
		public bool HasErrors 
		{
			get
			{
				return m_row.HasErrors;
			}
		}

		/// //////////////////////////////////////////////////////
		public object[] ItemArray
		{
			get
			{
				return m_row.ItemArray;
			}
			set
			{
				m_row.ItemArray = value;
			}
		}

		/// //////////////////////////////////////////////////////
		public string RowError
		{
			get
			{
				return m_row.RowError;
			}
			set
			{
				m_row.RowError = value;
			}
		}

		/// //////////////////////////////////////////////////////
		public DataRowState RowState
		{
			get
			{
				return m_row.RowState;
			}
		}

		/// //////////////////////////////////////////////////////
		public DataTable Table
		{
			get
			{
				return m_row.Table;
			}
		}

		/// <summary>
		/// /////////////////////////////////////////////////////////////////////
		/// </summary>
		public void AcceptChanges ()
		{
			m_row.AcceptChanges ();
            try
            {
                m_row[CObjetDonnee.c_champContexteModification] = "";
            }
            catch { }
		}

		/// /////////////////////////////////////////////////////////////////////
		public override bool Equals ( object obj )
		{
			if ( obj is DataRow  )
				return m_row.Equals( obj );
			if ( obj is C2iDataRow )
				return m_row.Equals(((C2iDataRow)obj).Row);
			return false;
		}


		/// <summary>
		/// /////////////////////////////////////////////////////////////////////
		/// </summary>
		public override int GetHashCode()
		{
			return m_row.GetHashCode();
		}

		/// <summary>
		/// /////////////////////////////////////////////////////////////////////
		/// </summary>
		public bool HasVersion ( DataRowVersion version)
		{
			return m_row.HasVersion ( version );
		}

		
		/// <summary>
		/// /////////////////////////////////////////////////////////////////////
		/// </summary>
		public void RejectChanges ()
		{
			m_row.RejectChanges ();
		}

		/// <summary>
		/// /////////////////////////////////////////////////////////////////////
		/// </summary>
		public override string ToString()
		{
			return m_row.ToString();
		}

        public bool IsEquivalent(object v1, object v2)
        {
            if ( v1 == DBNull.Value )
                v1 = null;
            if ( v2 == DBNull.Value )
                v2 = null;
            if (v1 == null && v2 == null)
                return true;
            if ((v1 == null) != (v2 == null))
                return false;
            if (v1.GetType() != v2.GetType())
                return false;
            if (v1 is DateTime)
            {
                TimeSpan sp = (DateTime)v1 - (DateTime)v2;
                return Math.Abs(sp.TotalMilliseconds) < 100.0f;
            }
            if (v2 is CDonneeBinaireInRow )
            {
                return ((CDonneeBinaireInRow)v2).HasChange();
            }
            if (v1 is double && v2 is double)
            {
                return Math.Abs(((double)v1 - (double)v2)) < 0.0000001;
            }
            return v1.Equals(v2);
        }

	}
}
