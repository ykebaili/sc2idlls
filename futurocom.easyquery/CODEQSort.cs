using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;
using sc2i.drawing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Collections;

namespace futurocom.easyquery
{
    [AutoExec("Autoexec")]
    [Serializable]
    public class CODEQSort : CODEQFromObjetsSource, IObjetDeEasyQuery
    {
        [Serializable]
        public class CSortColonne : I2iSerializable
        {
            private string m_strIdColonne = "";
            private bool m_bCroissant = true;

            //-------------------------------------------------------------
            public CSortColonne()
            {
            }

            //-------------------------------------------------------------
            public CSortColonne(string strIdColonne, bool bCroissant)
            {
                m_strIdColonne = strIdColonne;
                m_bCroissant = bCroissant;
            }

            //-------------------------------------------------------------
            public string IdColonne
            {
                get
                {
                    return m_strIdColonne;
                }
                set
                {
                    m_strIdColonne = value;
                }
            }


            //-------------------------------------------------------------
            public bool Croissant
            {
                get
                {
                    return m_bCroissant;
                }
                set
                {
                    m_bCroissant = value;
                }
            }

            //-------------------------------------------------------------
            private int GetNumVersion()
            {
                return 0;
            }

            //-------------------------------------------------------------
            public CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
                if ( !result) 
                    return result;
                serializer.TraiteString ( ref m_strIdColonne );
                serializer.TraiteBool ( ref m_bCroissant );
                return result;
            }
        }


        //-------------------------------------------------------------
        private List<string> m_listeIdsOrdreDeColonnes = new List<string>();

        //-------------------------------------------------------------
        private List<CSortColonne> m_listeSort = new List<CSortColonne>();

       
        //---------------------------------------------------
        public CODEQSort() : base()
        {
        }

        //---------------------------------------------------
        public static void Autoexec()
        {
            CODEQBase.RegisterTypeDerivePossible(typeof(CODEQBase), typeof(CODEQSort));
        }

        //---------------------------------------------------
        public IEnumerable<string> OrdreDeColonnes
        {
            get
            {
                return m_listeIdsOrdreDeColonnes.AsReadOnly();
            }
            set
            {
                m_listeIdsOrdreDeColonnes = new List<string>();
                if (value != null)
                    m_listeIdsOrdreDeColonnes.AddRange(value);
            }
        }

        //---------------------------------------------------
        public IEnumerable<IColumnDeEasyQuery> ColonnesOrdonnees
        {
            get
            {
                List<IColumnDeEasyQuery> lstCols = new List<IColumnDeEasyQuery>();
                if (ElementsSource.Length > 0)
                {
                    IObjetDeEasyQuery source = ElementsSource[0];
                    HashSet<string> setFaits = new HashSet<string>();
                    foreach (string strIdCol in OrdreDeColonnes)
                    {
                        IColumnDeEasyQuery col = source.Columns.FirstOrDefault(c => c.Id == strIdCol);
                        if (col != null)
                        {
                            lstCols.Add(col);
                            setFaits.Add(strIdCol);
                        }
                    }
                    foreach (IColumnDeEasyQuery col in source.Columns)
                        if (!setFaits.Contains(col.Id))
                            lstCols.Add(col);
                }
                return lstCols.AsReadOnly();
            }
            set
            {
                List<string> lstIds = new List<string>();
                if (value != null)
                {
                    foreach (IColumnDeEasyQuery col in value)
                        if (col != null)
                            lstIds.Add(col.Id);
                }
                OrdreDeColonnes = lstIds;
            }
        }

        //---------------------------------------------------
        public IEnumerable<CSortColonne> ColonnesDeTri
        {
            get
            {
                return m_listeSort.AsReadOnly();
            }
            set
            {
                m_listeSort = new List<CSortColonne>();
                if (value != null)
                    m_listeSort.AddRange(value);
            }
        }

        //---------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Sort and order columns|20013"); }
        }

        
        //---------------------------------------------------
        public override int NbSourceRequired
        {
            get { return 1;}
        }

        //---------------------------------------------------
        public IObjetDeEasyQuery TableSource
        {
            get
            {
                IObjetDeEasyQuery[] sources = ElementsSource;
                if ( sources.Length == 1 )
                    return sources[0];
                return null;
            }
            set
            {
                if (value == null)
                    ElementsSource = null;
                else
                    ElementsSource = new IObjetDeEasyQuery[] { value };
            }
        }

        //---------------------------------------------------
        public override IEnumerable<IColumnDeEasyQuery> GetColonnesFinales()
        {
            return ColonnesOrdonnees;
        }

        //---------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            CResultAErreur result = CResultAErreur.True;
            if (TableSource != null)
            {
                result = TableSource.GetDatas(sources);
                if (!result)
                    return result;
                DataTable table = result.Data as DataTable;
                if (table != null)
                {
                    DataTable tableFinale = new DataTable ( NomFinal );
                    Dictionary<string, string> dicIdColToNameCol = new Dictionary<string,string>();
                    foreach (IColumnDeEasyQuery col in ColonnesOrdonnees)
                    {
                        DataColumn dCol = table.Columns[col.ColumnName];
                        if (dCol != null)
                        {
                            DataColumn newCol = new DataColumn(dCol.ColumnName, dCol.DataType);
                            newCol.AllowDBNull = true;
                            tableFinale.Columns.Add(newCol);
                            dicIdColToNameCol[col.Id] = col.ColumnName;
                        }
                    }
                    foreach (DataColumn col in table.Columns)
                    {
                        if (tableFinale.Columns[col.ColumnName] == null)
                        {
                            DataColumn newCol = new DataColumn(col.ColumnName, col.DataType);
                            newCol.AllowDBNull = true;
                            tableFinale.Columns.Add(newCol);
                            //dicIdColToNameCol[col.Id] = col.ColumnName;
                        }
                    }
                    //Applique le tri
                    StringBuilder bl = new StringBuilder();
                    foreach (CSortColonne sort in ColonnesDeTri)
                    {
                        string strCol = "";
                        if (dicIdColToNameCol.TryGetValue(sort.IdColonne, out strCol))
                        {
                            bl.Append(strCol);
                            if (!sort.Croissant)
                                bl.Append(" desc");
                            bl.Append(",");
                        }
                    }
                    if (bl.Length > 0)
                    {
                        bl.Remove(bl.Length - 1, 1);
                        table.DefaultView.Sort = bl.ToString();
                    }
                    //Importe les lignes
                    foreach (DataRowView rowView in table.DefaultView )
                    {
                        DataRow row = rowView.Row;
                        tableFinale.ImportRow(row);
                    }

                    
                    result.Data = tableFinale;
                }
            }
            if (!(result.Data is DataTable))
            {
                result.EmpileErreur(I.T("Error in table @1|20002", NomFinal));
            }
            return result;
        }

        


        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.MySerialize(serializer);
            if (!result)
                return result;
            serializer.TraiteListString(m_listeIdsOrdreDeColonnes);
            result = serializer.TraiteListe<CSortColonne>(m_listeSort);
            if ( !result ) 
                return result;
            return result;
        }

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            base.MyDraw(ctx);
            IObjetDeEasyQuery source = TableSource;
            if (source != null)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                CLienTracable lien = CTraceurLienDroit.GetLienPourLier(source.RectangleAbsolu, RectangleAbsolu, EModeSortieLien.Automatic);
                lien.RendVisibleAvecLesAutres(ctx.Liens);
                ctx.AddLien(lien);
                AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
                pen.CustomEndCap = cap;
                lien.Draw(ctx.Graphic, pen);
                pen.Dispose();
                cap.Dispose();
                
            }

        }

        //---------------------------------------------------
        protected override void DrawHeader(CContextDessinObjetGraphique ctx, Rectangle rctHeader)
        {
            Image img = Resource1.filtre;
            ctx.Graphic.DrawImage(img, rctHeader.Left, rctHeader.Top + (rctHeader.Height - img.Height) / 2);
            Rectangle reste = new Rectangle(rctHeader.Left + img.Width, rctHeader.Top,
                rctHeader.Width - img.Width, rctHeader.Height);
            base.DrawHeader(ctx, reste);
        }

        

        
    }
}
