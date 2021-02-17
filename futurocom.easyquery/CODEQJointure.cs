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

namespace futurocom.easyquery
{
    [AutoExec("Autoexec")]
    [Serializable]
    public class CODEQJointure : CODEQFromObjetsSource, IObjetDeEasyQuery
    {
        public enum EModeJointure
        {
            Stricte = 0,
            Left,
            Right,
            Outer
        }

        private class CMapRows
        {
            public readonly List<DataRow> Rows1;
            public readonly List<DataRow> Rows2;

            public CMapRows(List<DataRow> r1, List<DataRow> r2)
            {
                Rows1 = r1;
                Rows2 = r2;
            }
        }

        private EModeJointure m_modeJointure = EModeJointure.Stricte;

        private List<CParametreJointure> m_listeParametresJointure = new List<CParametreJointure>();
        
        private List<IColumnDeEasyQuery> m_listeColonnes = new List<IColumnDeEasyQuery>();


        //---------------------------------------------------
        public CODEQJointure() : base()
        {
        }

 
        //---------------------------------------------------
        public static void Autoexec()
        {
            CODEQBase.RegisterTypeDerivePossible(typeof(CODEQBase), typeof(CODEQJointure));
        }

        //---------------------------------------------------
        public override int NbSourceRequired
        {
            get { return 2; }
        }

        //---------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Join|20005"); }
        }

        //---------------------------------------------------
        public override IObjetDeEasyQuery[] ElementsSource
        {
            get
            {
                return base.ElementsSource;
            }
            set
            {
                base.ElementsSource = value;
                if (m_listeColonnes.Count == 0 && value != null)
                {
                    foreach (IObjetDeEasyQuery table in value)
                    {
                        if (table != null)
                        {
                            foreach (IColumnDeEasyQuery col in table.Columns)
                            {
                                IColumnDeEasyQuery newCol = new CColumnEQFromSource(col);
                                m_listeColonnes.Add(newCol);
                            }
                        }
                    }
                }
            }
        }

        //---------------------------------------------------
        public IObjetDeEasyQuery Table1
        {
            get
            {
                IObjetDeEasyQuery[] sources = ElementsSource;
                if ( sources.Length > 0 )
                    return sources[0];
                return null;
            }
            
        }

        //---------------------------------------------------
        public IObjetDeEasyQuery Table2
        {
            get
            {
                IObjetDeEasyQuery[] sources = ElementsSource;
                if (sources.Length >= 2)
                    return sources[1];
                return null;
            }
           
        }

        //---------------------------------------------------
        public EModeJointure ModeJointure
        {
            get
            {
                return m_modeJointure;
            }
            set
            {
                m_modeJointure = value;
            }
        }

        //---------------------------------------------------
        public bool AddParametre(IColumnDeEasyQuery colFromTable1, IColumnDeEasyQuery colFromTable2)
        {
            if ( Table1 == null || Table2 == null )
                return false;
            CDefinitionProprieteDynamique def1 = null;
            CDefinitionProprieteDynamique def2 = null;
            foreach ( CDefinitionProprieteDynamique def in Table1.GetDefinitionsChamps ( Table1.GetType() ) )
            {
                if (def.NomProprieteSansCleTypeChamp == colFromTable1.ColumnName)
                {
                    def1 = def;
                    break;
                }
            }
            foreach (CDefinitionProprieteDynamique def in Table2.GetDefinitionsChamps(Table2.GetType()))
            {
                if (def.NomProprieteSansCleTypeChamp == colFromTable2.ColumnName)
                {
                    def2 = def;
                    break;
                }
            }
            if (def1 != null && def2 != null)
            {
                CParametreJointure parametre = new CParametreJointure(
                    new C2iExpressionChamp(def1),
                    new C2iExpressionChamp(def2));
                m_listeParametresJointure.Add(parametre);
                return true;
            }
            return false;
        }

        //---------------------------------------------------
        public IEnumerable<CParametreJointure> ParametresJointure
        {
            get
            {
                return m_listeParametresJointure.AsReadOnly();
            }
            set
            {
                m_listeParametresJointure = new List<CParametreJointure>(value);
            }
        }

        //---------------------------------------------------
        public override IEnumerable<IColumnDeEasyQuery> GetColonnesFinales()
        {
            List<IColumnDeEasyQuery> lstCols = new List<IColumnDeEasyQuery>();
            HashSet<string> colsFaites = new HashSet<string>();
            foreach ( IColumnDeEasyQuery col in ColonnesSource )
            {
                if (!colsFaites.Contains(col.ColumnName))
                {
                    colsFaites.Add(col.ColumnName);
                    lstCols.Add(col);
                }
            }
            return lstCols.AsReadOnly();
        }
        

        //---------------------------------------------------
        public IEnumerable<IColumnDeEasyQuery> ColonnesSource
        {
            get
            {
                return m_listeColonnes.AsReadOnly();
            }
            set
            {
                m_listeColonnes = new List<IColumnDeEasyQuery>(value);
            }
        }
        
        //-------------------------------------------------------
        /// <summary>
        /// Retourne la colonne finale associé à une colonne de la base
        /// </summary>
        /// <param name="colonne"></param>
        /// <returns></returns>
        public IColumnDeEasyQuery GetColonneFinaleFor(IColumnDeEasyQuery colonne)
        {
            return m_listeColonnes.FirstOrDefault(c => c is CColumnEQFromSource &&
                ((CColumnEQFromSource)c).IdColumnSource == colonne.Id);
        }

        //-------------------------------------------------------
        public IColumnDeEasyQuery GetColumnParenteFor(IColumnDeEasyQuery colonne)
        {
            CColumnEQFromSource colFromSource = colonne as CColumnEQFromSource;
            if (colFromSource == null)
                return null;
            IColumnDeEasyQuery retour = null;
            if (Table1 != null)
                retour = Table1.Columns.FirstOrDefault(c => c is CColumnEQFromSource &&
                ((CColumnEQFromSource)c).Id == colFromSource.IdColumnSource);
            if ( retour == null )
                retour = Table2.Columns.FirstOrDefault(c => c is CColumnEQFromSource &&
                ((CColumnEQFromSource)c).Id == colFromSource.IdColumnSource);
            return retour;
        }
            
        //---------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            DateTime dtChrono = DateTime.Now;
            CResultAErreur result = CResultAErreur.True;
            if (Table1 == null)
            {
                result.EmpileErreur(I.T("No source @1|20006", "1"));
                return result;
            }
            if (Table2 == null)
            {
                result.EmpileErreur(I.T("No source @2|20006", "2"));
                return result;
            }
            result = Table1.GetDatas(sources);
            if (!result || !(result.Data is DataTable))
            {
                result.EmpileErreur(I.T("Error in table @1 datas|20007", Table1.NomFinal));
                return result;
            }
            DataTable t1 = result.Data as DataTable;
            result = Table2.GetDatas(sources);
            if (!result || !(result.Data is DataTable))
            {
                result.EmpileErreur(I.T("Error in table @1 datas|20007", Table2.NomFinal));
                return result;
            }
            DataTable t2 = result.Data as DataTable;
            
            //Trouve toutes les lignes des la table 2 correspondant à la table 1
            List<CMapRows> maps1To2 = new List<CMapRows>();
            if ( ParametresJointure.Count() == 0 )
            {
            }
            else
            {
                CParametreJointure param0 = m_listeParametresJointure[0];
                Dictionary<object, List<DataRow>> dicValeursToRow1 = null;
                //Première passe : on évalue toutes les expression sur tous les enregistrement de t1 et t2
                List<DataRow> lst1 = new List<DataRow>();
                foreach ( DataRow row in t1.Rows )
                    lst1.Add ( row );
                result = CParametreJointure.GetDicValeurs(lst1, param0.FormuleTable1, ref dicValeursToRow1);
                if (!result)
                    return result;
                Dictionary<object, List<DataRow>> dicValeursToRow2 = null;
                List<DataRow> lst2 = new List<DataRow>();
                foreach ( DataRow row in t2.Rows )
                    lst2.Add ( row );
                result = CParametreJointure.GetDicValeurs(lst2, param0.FormuleTable2, ref dicValeursToRow2);
                maps1To2 = GetMatches(dicValeursToRow1, dicValeursToRow2, param0.Operateur);
                
                //Passe suivantes : on évalue pour chaque paire ce ceux qui matchent
                for (int n = 1; n < m_listeParametresJointure.Count; n++)
                {
                    CParametreJointure parametre = m_listeParametresJointure[n];
                    List<CMapRows> lstMaps = new List<CMapRows>();
                    foreach (CMapRows maps in maps1To2)
                    {
                        dicValeursToRow1 = null;
                        result = CParametreJointure.GetDicValeurs(maps.Rows1, parametre.FormuleTable1, ref dicValeursToRow1);
                        if (!result)
                            return result;
                        dicValeursToRow2 = null;
                        result = CParametreJointure.GetDicValeurs(maps.Rows2, parametre.FormuleTable2, ref dicValeursToRow2);
                        lstMaps.AddRange(GetMatches(dicValeursToRow1, dicValeursToRow2, parametre.Operateur));
                    }
                    maps1To2 = lstMaps;
                }
                DataTable tableResult = new DataTable(NomFinal);
                Dictionary<IColumnDeEasyQuery, IColumnDeEasyQuery> colsFromTable1 = new Dictionary<IColumnDeEasyQuery,IColumnDeEasyQuery>();
                Dictionary<IColumnDeEasyQuery, IColumnDeEasyQuery> colsFromTable2 = new Dictionary<IColumnDeEasyQuery,IColumnDeEasyQuery>();
                foreach ( IColumnDeEasyQuery col in m_listeColonnes )
                {
                    CColumnEQFromSource colFromSource = col as CColumnEQFromSource;
                    if (colFromSource != null)
                    {
                        IColumnDeEasyQuery colSource = Table1.Columns.FirstOrDefault(c => c.Id == colFromSource.IdColumnSource);
                        if (colSource != null)
                        {
                            colsFromTable1[col] = colSource;
                            IColumnDeEasyQuery colSource2 = Table2.Columns.FirstOrDefault(c => c.Id == colFromSource.IdColumnSource);
                            if (colSource2 != null)
                                colsFromTable2[col] = colSource2;
                        }
                        else
                        {
                            colSource = Table2.Columns.FirstOrDefault(c => c.Id == colFromSource.IdColumnSource);
                            if (colSource != null)
                                colsFromTable2[col] = colSource;
                        }
                        if (colSource != null && !tableResult.Columns.Contains(col.ColumnName))
                        {
                            Type tp = col.DataType;
                            if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
                                tp = tp.GetGenericArguments()[0];
                            DataColumn dtCol = new DataColumn(col.ColumnName, tp);
                            dtCol.ExtendedProperties[CODEQBase.c_extPropColonneId] = col.Id;
                            tableResult.Columns.Add(dtCol);
                        }
                    }
                }
                foreach ( CMapRows map in maps1To2 )
                {
                    lst1 = map.Rows1;
                    if (lst1.Count == 0)
                        lst1.Add(null);
                    lst2 = map.Rows2;
                    if (lst2.Count == 0)
                        lst2.Add(null);
                    foreach ( DataRow row1 in lst1 )
                    {
                        foreach ( DataRow row2 in lst2 )
                        {
                            if (row1 != null || row2 != null)
                            {
                                DataRow row = tableResult.NewRow();
                                if (row1 != null)
                                    foreach (KeyValuePair<IColumnDeEasyQuery, IColumnDeEasyQuery> kv in colsFromTable1)
                                    {
                                        try
                                        {
                                            row[kv.Key.ColumnName] = row1[kv.Value.ColumnName];
                                        }
                                        catch { }
                                    }
                                if (row2 != null)
                                    foreach (KeyValuePair<IColumnDeEasyQuery, IColumnDeEasyQuery> kv in colsFromTable2)
                                    {
                                        try
                                        {
                                            row[kv.Key.ColumnName] = row2[kv.Value.ColumnName];
                                        }
                                        catch
                                        {
                                        }
                                    }
                                tableResult.Rows.Add(row);
                            }
                        }
                    }
                }
                tableResult.AcceptChanges();
                result.Data = tableResult;
            }
            TimeSpan sp = DateTime.Now - dtChrono;
            Console.WriteLine("Join " + NomFinal + " : " + sp.TotalMilliseconds.ToString());

            return result;
        }

        private List<CMapRows> GetMatches(Dictionary<object, List<DataRow>> dic1,
            Dictionary<object, List<DataRow>> dic2, EOperateurJointure operateur)
        {
            List<CMapRows> lstRetour = new List<CMapRows>();
            if (operateur == EOperateurJointure.Egal)
            {
                foreach (KeyValuePair<object, List<DataRow>> kv in dic1)
                {
                    List<DataRow> lst2 = null;
                    if (dic2.TryGetValue(kv.Key, out lst2))
                    {
                        //Egalité trouvée
                        lstRetour.Add ( new CMapRows ( kv.Value, lst2 ) );
                    }
                    else
                    {
                        if (ModeJointure == EModeJointure.Left || ModeJointure == EModeJointure.Outer)
                        {
                            lstRetour.Add ( new CMapRows ( kv.Value, new List<DataRow>() ));
                        }
                    }
                }
                if (ModeJointure == EModeJointure.Right || ModeJointure == EModeJointure.Outer)
                //Ajoute les élément 2 non trouvés dans les éléments 1
                {
                    foreach (KeyValuePair<object, List<DataRow>> kv in dic2)
                    {
                        if (!dic1.ContainsKey(kv.Key))
                        {
                            lstRetour.Add(new CMapRows(new List<DataRow>(), kv.Value));
                        }
                    }
                }
            }
            else//Autre opérateur, on est obligé de tout balayer
            {
                foreach (KeyValuePair<object, List<DataRow>> kv1 in dic1)
                {
                    foreach (KeyValuePair<object, List<DataRow>> kv2 in dic2)
                    {
                        if ( CParametreJointure.Compare ( kv1.Key, kv2.Key, operateur ) )
                            lstRetour.Add ( new CMapRows(kv1.Value, kv2.Value ));
                    }
                }
            }
            return lstRetour;
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
            result = serializer.TraiteListe<CParametreJointure>(m_listeParametresJointure);
            if (result)
                result = serializer.TraiteListe<IColumnDeEasyQuery>( m_listeColonnes);
            if (!result)
                return result;

            int nMode = (int)ModeJointure;
            serializer.TraiteInt(ref nMode);
            ModeJointure = (EModeJointure)nMode;

            return result;
        }

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            base.MyDraw(ctx);
            foreach ( IObjetDeEasyQuery objet in ElementsSource )
            {
                if (objet != null)
                {
                    Pen pen = new Pen(Brushes.Black, 2);
                    AdjustableArrowCap cap = new AdjustableArrowCap(4, 4, true);
                    pen.CustomEndCap = cap;
                    CLienTracable lien = CTraceurLienDroit.GetLienPourLier(objet.RectangleAbsolu, RectangleAbsolu, EModeSortieLien.Automatic);
                    lien.RendVisibleAvecLesAutres(ctx.Liens);
                    ctx.AddLien(lien);
                    lien.Draw(ctx.Graphic, pen);
                    pen.Dispose();
                    cap.Dispose();
                }
            }

        }

        //---------------------------------------------------
        protected override void DrawHeader(CContextDessinObjetGraphique ctx, Rectangle rctHeader)
        {
            Image img = Resource1.Jointure;
            ctx.Graphic.DrawImage(img, rctHeader.Left, rctHeader.Top + (rctHeader.Height - img.Height) / 2);
            Rectangle reste = new Rectangle(rctHeader.Left + img.Width, rctHeader.Top,
                rctHeader.Width - img.Width, rctHeader.Height);
            base.DrawHeader(ctx, reste);
        }

        

        
    }
}
