using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.common;
using sc2i.data.dynamic.easyquery;
using sc2i.expression;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using sc2i.drawing;

namespace sc2i.data.dynamic.EasyQuery
{
    [AutoExec("Autoexec")]
    public class CODEQTableFilleFromFramework : CODEQFromObjetsSource, IObjetDeEasyQuery, IODEQTableFromFramework
    {
        public const string c_nomChampParentId = "SYS_PARENT_ID";

        private List<IColumnDeEasyQuery> m_listeColonnes = new List<IColumnDeEasyQuery>();

        private List<CColumnEQFromSource> m_listeColonnesFromParent = new List<CColumnEQFromSource>();

        private CFiltreDynamique m_filtre = null;

        private CDefinitionProprieteDynamique m_definitionSource = null;

        private Dictionary<object, DataRow> m_dicRowsParentes = null;

        //--------------------------------------------------
        public static void Autoexec()
        {
            CODEQBase.RegisterTypeDerivePossible(typeof(IODEQTableFromFramework), typeof(CODEQTableFilleFromFramework));
        }

        //--------------------------------------------------
        public CDefinitionProprieteDynamique ChampSource
        {
            get
            {
                return m_definitionSource;
            }
            set
            {
                m_definitionSource = value;
            }
        }

        //--------------------------------------------------
        public override int NbSourceRequired
        {
            get { return 1; }
        }

        //--------------------------------------------------
        public override string TypeName
        {
            get { return I.T("Child table|20072"); }
        }

        //--------------------------------------------------
        public CFiltreDynamique FiltreDynamique
        {
            get
            {
                if (m_filtre == null)
                {
                    m_filtre = new CFiltreDynamique();
                }
                CContexteDonnee ctxDonnee = Query.IContexteDonnee as CContexteDonnee;
                if (ctxDonnee == null)
                    ctxDonnee = CContexteDonneeSysteme.GetInstance();
                    m_filtre.ElementAVariablesExterne = new CElementAVariablesDynamiquesAvecContexteFromIElementAVariablesDynamiques(Query, ctxDonnee);
                m_filtre.TypeElements = m_definitionSource != null?m_definitionSource.TypeDonnee.TypeDotNetNatif:null;
                return m_filtre;
            }
            set
            {
                m_filtre = value;
            }
        }

        //--------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            CResultAErreur result = CResultAErreur.True;
            CResultAErreurType<CColumnDeEasyQueryChampDeRequete> resCol = GetColonneIdSource();
            m_dicRowsParentes = null;
            if (!resCol)
            {
                result.EmpileErreur(resCol.Erreur);
                return result;
            }
            IODEQTableFromFramework tableSource = this.ElementsSource[0] as IODEQTableFromFramework;
            if (tableSource != null)
            {
                result = tableSource.GetDatas(sources);
                if (!result)
                    return result;

                DataTable tableParente = result.Data as DataTable;

                C2iRequeteAvancee requete = new C2iRequeteAvancee();
                requete.TableInterrogee = CContexteDonnee.GetNomTableForType(TypeElements);
                foreach (IColumnDeEasyQuery col in m_listeColonnes)
                {
                    CColumnDeEasyQueryChampDeRequete colR = col as CColumnDeEasyQueryChampDeRequete;
                    requete.ListeChamps.Add(colR);
                }
                if (requete.ListeChamps.Count == 0)
                {
                    result.Data = new DataTable();
                    return result;
                }
                
                bool bRelTrouve = false;
                CComposantFiltreOperateur cpOperateur = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurIn);
                CComposantFiltre cpFinal = cpOperateur;

                result = FiltreDynamique.GetFiltreData();
                if (!result)
                    return result;
                CFiltreDataAvance filtre = result.Data as CFiltreDataAvance;
                if (filtre == null)
                    filtre = new CFiltreDataAvance(requete.TableInterrogee, "");

                CDefinitionProprieteDynamiqueRelation rel = m_definitionSource as CDefinitionProprieteDynamiqueRelation;

                string strNomChampParent = null;

                //// Relation standard
                if (rel != null)
                {
                    //m_definitionSource.GetDefinitionInverse(TypeElements);
                    if (rel.Relation.TableParente == requete.TableInterrogee)
                    {
                        cpOperateur.Parametres.Add(new CComposantFiltreChamp(rel.Relation.ChampsParent[0], requete.TableInterrogee));
                        strNomChampParent = rel.Relation.ChampsParent[0];
                    }
                    else
                    {
                        cpOperateur.Parametres.Add(new CComposantFiltreChamp(rel.Relation.ChampsFille[0], requete.TableInterrogee));
                        strNomChampParent = rel.Relation.ChampsFille[0];
                    }

                    bRelTrouve = true;
                }
                else
                {

                    ///Relation Type id
                    CDefinitionProprieteDynamiqueRelationTypeId relTypeId = m_definitionSource as CDefinitionProprieteDynamiqueRelationTypeId;
                    if (relTypeId != null)
                    {
                        cpOperateur.Parametres.Add(new CComposantFiltreChamp(relTypeId.Relation.ChampId, requete.TableInterrogee));
                        strNomChampParent = relTypeId.Relation.ChampId;
                        cpFinal = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEt);
                        cpFinal.Parametres.Add(cpOperateur);
                        CComposantFiltre cpType = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEgal);
                        cpType.Parametres.Add(new CComposantFiltreChamp(relTypeId.Relation.ChampType, requete.TableInterrogee));
                        cpType.Parametres.Add(new CComposantFiltreVariable("@" + (filtre.Parametres.Count + 1).ToString("0")));

                        filtre.Parametres.Add(tableSource.TypeElements.ToString());
                        cpFinal.Parametres.Add(cpType);
                        bRelTrouve = true;
                    }
                    
                }

                if ( strNomChampParent != null )
                    requete.ListeChamps.Add ( new C2iChampDeRequete(c_nomChampParentId, new CSourceDeChampDeRequete(strNomChampParent), typeof(int), OperationsAgregation.None, false));



                if (!bRelTrouve)
                {
                    result.EmpileErreur(I.T("Can not find link for table @1|20076", NomFinal));
                    return result;
                }
               
                
                
                int nParametre = filtre.Parametres.Count;
                cpOperateur.Parametres.Add(new CComposantFiltreVariable("@"+(filtre.Parametres.Count+1).ToString("0")));
                filtre.Parametres.Add(new int[0]);
                
                if (filtre.ComposantPrincipal == null)
                    filtre.ComposantPrincipal = cpFinal;
                else
                {
                    CComposantFiltreOperateur opEt = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEt);
                    opEt.Parametres.Add(cpFinal);
                    opEt.Parametres.Add(filtre.ComposantPrincipal);
                    filtre.ComposantPrincipal = opEt;
                }

                m_dicRowsParentes = new Dictionary<object, DataRow>();
                DataTable maTable = null;
                int nLectureParLot = 500;
                for (int nRow = 0; nRow < tableParente.Rows.Count; nRow += nLectureParLot)
                {
                    int nMax = Math.Min(nRow + nLectureParLot, tableParente.Rows.Count);
                    List<int> lstIds = new List<int>();
                    for (int n = nRow; n < nMax; n++)
                    {
                        DataRow row = tableParente.Rows[n];
                        int nVal = (int)row[resCol.DataType.ColumnName];
                        lstIds.Add(nVal);
                        m_dicRowsParentes[nVal] = row;
                    }
                    filtre.Parametres[nParametre] = lstIds.ToArray();
                    DataTable tableTmp = null;
                    requete.FiltreAAppliquer = filtre;
                    result = requete.ExecuteRequete(CContexteDonneeSysteme.GetInstance().IdSession);
                    if (!result || !(result.Data is DataTable))
                    {
                        result.EmpileErreur(I.T("Error on table @1|20070", NomFinal));
                        return result;
                    }
                    tableTmp = result.Data as DataTable;
                    if (maTable == null)
                        maTable = tableTmp;
                    else
                        maTable.Merge(tableTmp);
                }
                if ( maTable == null )
                {
                    maTable = new DataTable(NomFinal);
                    foreach ( IColumnDeEasyQuery colEQ in ColonnesOrCalculees )
                    {
                        DataColumn col = new DataColumn(colEQ.ColumnName, colEQ.DataType);
                        try
                        {
                            maTable.Columns.Add(col);
                        }
                        catch { }
                    }
                    DataColumn colParent = new DataColumn(c_nomChampParentId, typeof(int));
                    try
                    {
                        maTable.Columns.Add(colParent);
                    }
                    catch { }
                }
                else
                {
                    //Ajoute les colonnes from parent
                    Dictionary<CColumnEQFromSource, string> dicColFromSourceToNom = new Dictionary<CColumnEQFromSource,string>();
                    foreach (CColumnEQFromSource colFromSource in m_listeColonnesFromParent)
                    {
                        if (!maTable.Columns.Contains(colFromSource.ColumnName))
                            maTable.Columns.Add(colFromSource.ColumnName, colFromSource.DataType);
                        IColumnDeEasyQuery colSource = tableSource.Columns.FirstOrDefault(c=>c.Id == colFromSource.IdColumnSource);
                        if ( colSource != null && tableParente.Columns.Contains ( colSource.ColumnName ))
                            dicColFromSourceToNom[colFromSource] = colSource.ColumnName;
                    }
                    if (maTable.Columns.Contains(c_nomChampParentId))
                    { 
                        
                        foreach ( DataRow row in maTable.Rows )
                        {
                            if ( row[c_nomChampParentId] is int )
                            {
                                DataRow rowParente = null;
                                if ( m_dicRowsParentes.TryGetValue((int)row[c_nomChampParentId], out rowParente ))
                                {
                                    if ( rowParente != null )
                                    {
                                        foreach ( KeyValuePair<CColumnEQFromSource, string> kv in dicColFromSourceToNom )
                                        {
                                            row[kv.Key.ColumnName] = rowParente[kv.Value];
                                        }
                                    }
                                }
                            }
                        }
                        maTable.Columns.Remove(c_nomChampParentId);
                    }
                }
                result.Data = maTable;
                
            }
            return result;
        }

        //---------------------------------------------------
        public void SetColonnesOrCalculees(IEnumerable<IColumnDeEasyQuery> cols)
        {
            m_listeColonnes.Clear();
            m_listeColonnesFromParent.Clear();
            foreach ( IColumnDeEasyQuery col in cols )
            {
                if (col is CColumnEQFromSource)
                    m_listeColonnesFromParent.Add((CColumnEQFromSource)col);
                else
                    m_listeColonnes.Add(col);
            }
        }

        //--------------------------------------------------
        public void AddColonneDeRequete(CColumnDeEasyQueryChampDeRequete col)
        {
            m_listeColonnes.Add(col);
        }

        //---------------------------------------------------
        public IEnumerable<IColumnDeEasyQuery> ColonnesOrCalculees
        {
            get
            {
                List<IColumnDeEasyQuery> lstColonnes = new List<IColumnDeEasyQuery>();
                lstColonnes.AddRange(m_listeColonnes);
                lstColonnes.AddRange(m_listeColonnesFromParent);
                return lstColonnes.AsReadOnly();
            }
        }

        //--------------------------------------------------
        public override IEnumerable<IColumnDeEasyQuery> GetColonnesFinales()
        {
            return ColonnesOrCalculees;
        }

        //--------------------------------------------------
        public CResultAErreurType<CColumnDeEasyQueryChampDeRequete> GetColonneIdSource()
        {
            CResultAErreurType<CColumnDeEasyQueryChampDeRequete> result = new CResultAErreurType<CColumnDeEasyQueryChampDeRequete>();
            IODEQTableFromFramework tableSource = ElementsSource[0] as IODEQTableFromFramework;
            if ( tableSource == null )
            {
                result.EmpileErreur(I.T("Incompatible source table|20074"));
                return result;
            }
            Type tp = tableSource.TypeElements;
            if ( !typeof(IObjetDonneeAIdNumerique).IsAssignableFrom(tp ) )
            {
                result.EmpileErreur(I.T("Incompatible source table|20074"));
                return result;
            }
            CStructureTable structure = CStructureTable.GetStructure ( tp );
            HashSet<string> setIds = new HashSet<string>();
            setIds.Add ( "#PP|"+structure.ChampsId[0].Propriete );
            if (structure.ChampsId.Length == 1)
                setIds.Add(structure.ChampsId[0].NomChamp);

            foreach ( CColumnDeEasyQueryChampDeRequete col in tableSource.ChampsDeRequete )
            {
                if ( col.Sources.Length == 1 )
                {
                    if ( setIds.Contains(col.Sources[0].Source) && 
                        col.OperationAgregation == OperationsAgregation.None)
                    {
                        result.Data = col;
                        return result;
                    }
                }
            }
            result.EmpileErreur(I.T("Source table must reference 'Id' field with 'None' operation|20075"));
            return result;
        }


        //--------------------------------------------------
        public CResultAErreur GetErreurIncompatibilitéTableParente()
        {
            CResultAErreur result = CResultAErreur.True;
            if ( ElementsSource.Length != 1 )
            {
                result.EmpileErreur(I.T("Unkown source table|20073"));
                return result;
            }
            CResultAErreurType<CColumnDeEasyQueryChampDeRequete> resCol = GetColonneIdSource();
            if ( !resCol )
            {
                result.EmpileErreur ( resCol.Erreur);
            }
            return result;
            
        }



        //---------------------------------------------------------------------------
        public Type TypeElements
        {
            get
            {
                return m_definitionSource != null ? m_definitionSource.TypeDonnee.TypeDotNetNatif : typeof(DBNull);
            }
        }

        //---------------------------------------------------------------------------
        public IEnumerable<CColumnDeEasyQueryChampDeRequete> ChampsDeRequete
        {
            get
            {
                return from c in m_listeColonnes where c is CColumnDeEasyQueryChampDeRequete select c as CColumnDeEasyQueryChampDeRequete;
            }
        }
        //---------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
            //Version 1 : ajout des colonnes from parent
        }

        //---------------------------------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.MySerialize(serializer);
            if ( result ) 
                result = serializer.TraiteListe<IColumnDeEasyQuery>(m_listeColonnes);
            if (result)
                result = serializer.TraiteObject<CFiltreDynamique>(ref m_filtre);
            if (result)
                result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_definitionSource);
            if (nVersion >= 1 && result)
                result = serializer.TraiteListe<CColumnEQFromSource>(m_listeColonnesFromParent);
            return result;
        }

        //---------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            base.MyDraw(ctx);
            foreach (IObjetDeEasyQuery objet in ElementsSource)
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

        
    }
}
