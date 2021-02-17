using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.drawing;
using sc2i.common;
using System.Drawing;
using System.Collections;
using System.Data;
using sc2i.expression;
using futurocom.easyquery.CAML;
using futurocom.easyquery.postFillter;


namespace futurocom.easyquery
{
    /// <summary>
    /// CObjetDeEasyQueryTableFromBase : un table utilisée dans une requête
    /// </summary>
    [Serializable]
    public abstract class CODEQBase : 
        C2iObjetGraphique, 
        IObjetDeEasyQuery, 
        IFournisseurProprietesDynamiques,
        I2iCloneableAvecTraitementApresClonage,
        IElementARunnableEasyQueryDynamique
    {
        public static string c_extPropColonneId = "FUT_COL_ID";

        public static string c_nomVariableQuery = "Query";

        public const int c_nTailleOmbre = 4;
        
        private static Dictionary<Type, List<Type>> m_dicObjetsDerivesPossiblesForType = new Dictionary<Type, List<Type>>();


        private string m_strNomFinal = "";
        private string m_strId = "";
        private string m_strCommentaires = "";

        private bool m_bIsExpanded = true;

        private bool m_bUseCache = false;

        private Dictionary<IColumnDeEasyQuery, Rectangle> m_dicColonneToRect = new Dictionary<IColumnDeEasyQuery, Rectangle>();

        private List<CColonneEQCalculee> m_listeColonnesCalculees = new List<CColonneEQCalculee>();

        private IPostFilter m_postFilter = null;

        /// <summary>
        /// contient le cache de la table si l'option UseCache est activée
        /// </summary>
        [NonSerialized]
        private DataTable m_tableCache = null;


        //-------------------------------------------------------
        public CODEQBase()
        {
            Size = new Size(80, 150);
            m_strId = Guid.NewGuid().ToString();
        }

        //-------------------------------------------------------
        protected virtual int HeaderHeight
        {
            get
            {
                return 20;
            }
        }

        //-------------------------------------------------------
        public bool IsExpanded
        {
            get
            {
                return m_bIsExpanded;
            }
            set
            {
                m_bIsExpanded = value;
            }
        }

        //-------------------------------------------------------
        public bool UseCache
        {
            get
            {
                return m_bUseCache;
            }
            set
            {
                m_bUseCache = value;
            }
        }

        //-------------------------------------------------------
        [DynamicMethod("Clear data cache")]
        public void ClearCache()
        {
            m_tableCache = null;
        }

        //-------------------------------------------------------
        public override Size Size
        {
            get
            {
                if (m_bIsExpanded)
                    return base.Size;
                else
                    return new Size(base.Size.Width, HeaderHeight+c_nTailleOmbre);
            }
            set
            {
                if ( m_bIsExpanded )
                    base.Size = value;
            }
        }

        //-------------------------------------------------------
        public IPostFilter PostFilter
        {
            get
            {
                return m_postFilter;
            }
            set
            {
                m_postFilter = value;
            }
        }

        //-------------------------------------------------------
        public virtual IEnumerable<CCAMLItemField> CAMLFields
        {
            get
            {
                List<CCAMLItemField> fields = new List<CCAMLItemField>();
                foreach (IColumnDeEasyQuery def in Columns)
                {
                    CCAMLItemField field = new CCAMLItemField(def.ColumnName,
                        "", "");
                    fields.Add(field);
                }
                return fields.AsReadOnly();
            }
        }
                

        //-------------------------------------------------------
        /// <summary>
        /// Enregistre un type d'objet pouvant être créé à partir d'un autre type d'objet
        /// </summary>
        /// <param name="typeObjetParent"></param>
        /// <param name="typeObjetFils"></param>
        public static void RegisterTypeDerivePossible(Type typeObjetParent, Type typeObjetFils)
        {
            List<Type> lst = null;
            if (!m_dicObjetsDerivesPossiblesForType.TryGetValue(typeObjetParent, out lst))
            {
                lst = new List<Type>();
                m_dicObjetsDerivesPossiblesForType[typeObjetParent] = lst;
            }
            if (!lst.Contains(typeObjetFils))
                lst.Add(typeObjetFils);
        }

        //-------------------------------------------------------
        public Type[] TypesDerivesPossibles
        {
            get
            {
                HashSet<Type> set = new HashSet<Type>();
                FillSetDerivesPossibles(GetType(), set, true);
                return set.ToArray();
            }
        }

        //-------------------------------------------------------
        private void FillSetDerivesPossibles(Type tp, HashSet<Type> set, bool bAvecInterfaces)
        {
            List<Type> lst = null;
            if (m_dicObjetsDerivesPossiblesForType.TryGetValue(tp, out lst))
                foreach (Type tpDerive in lst)
                    set.Add(tpDerive);
            if ( bAvecInterfaces )
                foreach ( Type inte in tp.GetInterfaces() )
                    FillSetDerivesPossibles ( inte, set, false );
            tp = tp.BaseType;
            if (tp != null)
                FillSetDerivesPossibles(tp, set, false);
        }

        

        //-------------------------------------------------------
        public abstract string TypeName { get; }



        //-------------------------------------------------------
        public override bool AcceptChilds
        {
            get
            {
                return false;
            }
        }

        //-------------------------------------------------------
        public string Id
        {
            get
            {
                return m_strId;
            }
        }

        //-------------------------------------------------------
        public string NomFinal
        {
            get
            {
                return m_strNomFinal;
            }
            set
            {
                m_strNomFinal = value;
            }
        }

        //-------------------------------------------------------
        public string Commentaires
        {
            get
            {
                return m_strCommentaires;
            }
            set
            {
                m_strCommentaires = value;
            }
        }

        //-------------------------------------------------------
        public override string TooltipText
        {
            get
            {
                return m_strCommentaires;
            }
        }

        //-------------------------------------------------------
        public IEnumerable<CColonneEQCalculee> ColonnesCalculees
        {
            get
            {
                return m_listeColonnesCalculees.AsReadOnly();
            }
            set
            {
                if (value != null)
                    m_listeColonnesCalculees = new List<CColonneEQCalculee>(value);
            }
        }

        //-------------------------------------------------------
        public void AddColonneCalculee(CColonneEQCalculee colCalc)
        {
            if (!m_listeColonnesCalculees.Contains(colCalc))
                m_listeColonnesCalculees.Add(colCalc);
        }

        //-------------------------------------------------------
        public void RemoveColonneCalculee(CColonneEQCalculee colCalc)
        {
            m_listeColonnesCalculees.Remove(colCalc);
        }

        public void ResetColonnesCalculees()
        {
            m_listeColonnesCalculees.Clear();
        }

        //-------------------------------------------------------
        public void AddDonneesCalculees(DataTable table)
        {
            foreach (CColonneEQCalculee col in ColonnesCalculees)
            {
                if ( col.Formule == null )
                    continue;
                if (!table.Columns.Contains(col.ColumnName))
                {
                    DataColumn dataCol = new DataColumn(col.ColumnName, col.DataType);
                    dataCol.AllowDBNull = true;
                    table.Columns.Add(dataCol);
                }
            }

            DataRow previousRow = null;
            //Stef 22/07/2015 : le contexte d'évaluation est le même pour
            //toutes les rows, ce qui permet de ne pas réinterpreter les champs calculés
            //appartenant à la query (utilisation du cache du contexteEvaluation), mais uniquement les champs calculées
            //de chaque row
            CContexteEvaluationExpression contexte = new CContexteEvaluationExpression("");
            foreach ( DataRow row in table.Rows )
            {
                contexte.ChangeSource(new CDataRowForChampCalculeODEQ(row, previousRow, Query));
                previousRow = row;
                foreach ( CColonneEQCalculee col in ColonnesCalculees )
                {
                    C2iExpression formule = col.Formule;
                    if ( formule != null )
                    {
                        CResultAErreur result = formule.Eval (contexte );
                        if ( result && result.Data != null )
                        {
                            if ( col.DataType.IsAssignableFrom ( result.Data.GetType() )) 
                            {
                                try{
                                    row[col.ColumnName] = result.Data;
                                }
                                catch{}
                            }
                        }
                    }
                }
            }
        }
            

        //-------------------------------------------------------
        public CEasyQuery Query
        {
            get
            {
                I2iObjetGraphique parent = Parent;
                while (parent != null && !(parent is CEasyQuery))
                    parent = parent.Parent;
                return parent as CEasyQuery;
            }
        }


        //-------------------------------------------------------
        public CResultAErreur GetDatas(CListeQuerySource sources)
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_bUseCache && m_tableCache != null && Query != null && Query.UseRuntimeCache)
            {
                result.Data = m_tableCache;
                return result;
            }

            result = GetDatasHorsCalculees(sources);
            DataTable table = result.Data as DataTable;
            if (result && table != null)
            {
                AddDonneesCalculees(table);
            }
            if ( m_postFilter != null && table != null)
            {
                CResultAErreurType<DataTable> resTable = m_postFilter.FiltreData(table, Query, sources);
                if ( !resTable )
                {
                    result.EmpileErreur(resTable.Erreur);
                    return result;
                }
                table = resTable.DataType;
                result.Data = table;
            }

            if (m_bUseCache && table != null)
                m_tableCache = table;
            return result;
        }
                

        //-------------------------------------------------------
        protected abstract CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources);

        //-------------------------------------------------------
        public IEnumerable<IColumnDeEasyQuery> Columns
        {
            get
            {
                List<IColumnDeEasyQuery> lst = new List<IColumnDeEasyQuery>();
                lst.AddRange ( GetColonnesFinales());
                foreach (CColonneEQCalculee col in ColonnesCalculees)
                    lst.Add(col);
                return lst.AsReadOnly();
            }
        }

        //-------------------------------------------------------
        public abstract IEnumerable<IColumnDeEasyQuery> GetColonnesFinales();


        //-------------------------------------------------------
        public override I2iObjetGraphique[] Childs
        {
            get { return new I2iObjetGraphique[0]; }
        }

        //-------------------------------------------------------
        public override bool AddChild(I2iObjetGraphique child)
        {
            return false;
        }

        //-------------------------------------------------------
        public override bool ContainsChild(I2iObjetGraphique child)
        {
            return false;
        }

        //-------------------------------------------------------
        public override void RemoveChild(I2iObjetGraphique child)
        {
            
        }

        //-------------------------------------------------------
        public override void BringToFront(I2iObjetGraphique child)
        {
            
        }

        //-------------------------------------------------------
        public override void FrontToBack(I2iObjetGraphique child)
        {
            
        }


        //-------------------------------------------------------
        private int GetNumVersion()
        {
            return 3;
            //3 : Ajout du post filter
            //2 : Ajout du cache
            //1 : Ajout des colonnes calculées
            
        }

        //-------------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            serializer.TraiteString(ref m_strId);
            serializer.TraiteString(ref m_strNomFinal);
            serializer.TraiteBool(ref m_bIsExpanded);
            serializer.TraiteString(ref m_strCommentaires);
            if (nVersion >= 1)
                result = serializer.TraiteListe<CColonneEQCalculee>(m_listeColonnesCalculees);
            if (!result)
                return result;
            if (nVersion >= 2)
                serializer.TraiteBool(ref m_bUseCache);

            if (nVersion >= 3)
            {
                result = serializer.TraiteObject<IPostFilter>(ref m_postFilter);
            }
            

            if ( serializer.IsForClone)
                m_strId = Guid.NewGuid().ToString();

            
            return result;
        }

        //-------------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            Rectangle rct = RectangleAbsolu;
            rct.Inflate(-c_nTailleOmbre/2, -c_nTailleOmbre/2);
            rct.Offset(c_nTailleOmbre/2, c_nTailleOmbre/2);
            ctx.Graphic.FillRectangle(Brushes.LightGray, rct);
            rct.Offset(-c_nTailleOmbre, -c_nTailleOmbre);
            ctx.Graphic.FillRectangle(Brushes.White, rct);
            ctx.Graphic.DrawRectangle(Pens.Black, rct);

            CEasyQuery query = Query;

            Rectangle rctHeader = new Rectangle(rct.Location, new Size(rct.Width, HeaderHeight));
            ctx.Graphic.DrawRectangle(Pens.Black, rctHeader);
            rctHeader.Inflate(-1, -1);

            DrawHeader(ctx, rctHeader);
            //Dessins des colonnes
            if (m_bIsExpanded)
            {
                Rectangle rctCols = new Rectangle(rct.Left + 1, rctHeader.Bottom + 2, rct.Width - 2, rct.Height - rctHeader.Height - 2);
                DrawColumns(ctx, rctCols);
            }
            if (m_postFilter != null)
                m_postFilter.Draw(this, ctx);
        }

        //-------------------------------------------------------
        protected virtual void DrawHeader(CContextDessinObjetGraphique ctx, Rectangle rctHeader)
        {
            Font ft = new Font(FontFamily.GenericSansSerif, 7);
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            ctx.Graphic.DrawString(NomFinal, ft, Brushes.Black, rctHeader, format);
            
            ft.Dispose();
        }

        //-------------------------------------------------------
        protected virtual void DrawColumns(CContextDessinObjetGraphique ctx, Rectangle rctCols)
        {
            Region oldClip = ctx.Graphic.Clip;
            Region clip = new Region(rctCols);
            ctx.Graphic.Clip = clip;

            m_dicColonneToRect.Clear();
            int nY = 0;

            Font ft = new Font(FontFamily.GenericSansSerif, 7);
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Near;
            format.Alignment = StringAlignment.Near;
            format.FormatFlags = StringFormatFlags.NoWrap;

            int nHeightCol = (int)ctx.Graphic.MeasureString("ABCDEFGHIJKLMNOPQRSTUVXYZ", ft).Height;

            foreach (IColumnDeEasyQuery col in Columns)
            {
                Rectangle rctCol = new Rectangle(rctCols.Left, rctCols.Top + nY, rctCols.Width, nHeightCol);
                
                ctx.Graphic.DrawString(col.ColumnName, ft, Brushes.Black, rctCol, format);
                m_dicColonneToRect[col] = rctCol;
                nY += nHeightCol;
            }
            ctx.Graphic.Clip = oldClip;
            clip.Dispose();

            ft.Dispose();
        }

        //-------------------------------------------------------
        public virtual CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux)
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            if (typeInterroge == typeof(DataRow) || typeInterroge == typeof(CDataRowForChampCalculeODEQ) ||
                typeInterroge == GetType() )
            {
                foreach (IColumnDeEasyQuery col in Columns)
                {
                    Type tp = col.DataType;
                    if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
                        tp = tp.GetGenericArguments()[0];
                    DataColumn dataCol = new DataColumn(col.ColumnName, tp);
                    lst.Add(new CDefinitionProprieteDynamiqueDataColumn(dataCol));
                }
                if (Query != null)
                    lst.Add(new CDefinitionProprieteDynamiqueRunnableEasyQuery(c_nomVariableQuery, Query));
            }
            else if ( typeInterroge == typeof(CEasyQuery) && Query != null)
            {
                lst.AddRange(Query.GetProprietesInstance());
            }
            lst.AddRange(new CFournisseurGeneriqueProprietesDynamiques().GetDefinitionsChamps(typeInterroge));

            
            return lst.ToArray();
        }

        //-------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
        {
            return GetDefinitionsChamps(typeInterroge, 0);
        }

        //-------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
        {
            return GetDefinitionsChamps(objet != null ? objet.TypeAnalyse : null, 0);
        }

        //-------------------------------------------------------
        public virtual CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            return GetDefinitionsChamps(objet != null ? objet.TypeAnalyse : null, 0);
        }


        //-------------------------------------------------------
        public IColumnDeEasyQuery GetColonneAt(Point ptAbsolu)
        {
            foreach (KeyValuePair<IColumnDeEasyQuery, Rectangle> kv in m_dicColonneToRect)
            {
                if (kv.Value.Contains(ptAbsolu))
                    return kv.Key;
            }
            return null;
        }

        //-------------------------------------------------------
        public Rectangle GetRectAbsoluColonne(IColumnDeEasyQuery col)
        {
            Rectangle rct = new Rectangle(0, 0, 0, 0);
            m_dicColonneToRect.TryGetValue(col, out rct);
            return rct;
        }

        //-------------------------------------------------------
        public void TraiteApresClonage(I2iSerializable source)
        {
            m_strId = Guid.NewGuid().ToString();
            foreach (IColumnDeEasyQuery col in Columns)
            {
                IColumnDeEasyQueryAGUID colAGUID = col as IColumnDeEasyQueryAGUID;
                if (colAGUID != null)
                    colAGUID.ForceId(Guid.NewGuid().ToString());
            }
        }


        //-------------------------------------------------------
        public IRunnableEasyQuery GetQuery(string strLibelle)
        {
            if (strLibelle == c_nomVariableQuery)
                return Query;
            return null;
        }
    }
        
}
