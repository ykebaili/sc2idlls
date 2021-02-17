using sc2i.common;
using sc2i.expression;
using sc2i.multitiers.client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class CConfigMappagesSmartImport : I2iSerializable
    {
        private string m_strIdMappage = null;
        private Type m_typeEntite = null;
        private CDbKey m_cleEntite = null;
        private EOptionCreationElementImport m_optionCreation = EOptionCreationElementImport.None;

        private List<CMappageChampSimple> m_listeMappagesChamps = new List<CMappageChampSimple>();
        private List<CMappageEntiteParente> m_listeMappagesParents = new List<CMappageEntiteParente>();
        private List<CMappageEntitesFilles> m_listeMappagesFilles = new List<CMappageEntitesFilles>();


        //--------------------------------------------------------
        public CConfigMappagesSmartImport()
        {
            m_strIdMappage =CUniqueIdentifier.GetNew();
        }

        
       
        //--------------------------------------------------------
        public Type TypeEntite
        {
            get
            {
                return m_typeEntite;
            }
            set
            {
                m_typeEntite = value;
            }
        }

        //------------------------------------------------
        public string IdMappage
        {
            get
            {
                return m_strIdMappage;
            }
            set
            {
                m_strIdMappage = value;
            }
        }

        //------------------------------------------------
        public CDbKey KeyEntite
        {
            get
            {
                return m_cleEntite;
            }
            set
            {
                m_cleEntite = value;
            }
        }

        //------------------------------------------------
        public EOptionCreationElementImport OptionCreation
        {
            get
            {
                return m_optionCreation;
            }
            set
            {
                m_optionCreation = value;
            }
        }

        //------------------------------------------------
        public IEnumerable<CMappageChampSimple> MappagesChampsSimples
        {
            get
            {
                return m_listeMappagesChamps.AsReadOnly();
            }
            set
            {
                List<CMappageChampSimple> lst = new List<CMappageChampSimple>();
                if (value != null)
                    lst.AddRange(value);
                m_listeMappagesChamps = lst;
            }
        }

        //------------------------------------------------
        public CMappageChampSimple GetMappageSimpleFor ( CDefinitionProprieteDynamique def)
        {
            return MappagesChampsSimples.FirstOrDefault(m => m.Propriete.GetType() == def.GetType() && m.Propriete.NomPropriete == def.NomPropriete);
        }

        //------------------------------------------------
        public IEnumerable<CMappageEntiteParente> MappagesEntitesParentes
        {
            get
            {
                return m_listeMappagesParents.AsReadOnly();
            }
            set
            {
                List<CMappageEntiteParente> lst = new List<CMappageEntiteParente>();
                if (value != null)
                    lst.AddRange(value);
                m_listeMappagesParents = lst;
            }
        }

        //------------------------------------------------
        public CMappageEntiteParente GetMappageParentFor(CDefinitionProprieteDynamique def)
        {
            return MappagesEntitesParentes.FirstOrDefault(m => m.Propriete.GetType() == def.GetType() && m.Propriete.NomPropriete == def.NomPropriete);
        }

        //------------------------------------------------
        public IEnumerable<CMappageEntitesFilles> MappagesEntitesFilles
        {
            get
            {
                return m_listeMappagesFilles.AsReadOnly();
            }
            set
            {
                List<CMappageEntitesFilles> lst = new List<CMappageEntitesFilles>();
                if (value != null)
                    lst.AddRange(value);
                m_listeMappagesFilles = lst;
            }
        }

        //------------------------------------------------
        public CMappageEntitesFilles GetMappageFilleFor ( CDefinitionProprieteDynamique def )
        {
            return MappagesEntitesFilles.FirstOrDefault(m => m.Propriete.GetType() == def.GetType() && m.Propriete.NomPropriete == def.NomPropriete);
        }

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
        }

        //------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            if (nVersion >= 1)
                serializer.TraiteString(ref m_strIdMappage);
            serializer.TraiteType(ref m_typeEntite);
            serializer.TraiteDbKey(ref m_cleEntite);
            serializer.TraiteEnum<EOptionCreationElementImport>(ref m_optionCreation);
            result = serializer.TraiteListe<CMappageChampSimple>(m_listeMappagesChamps);
            if ( result )
                result = serializer.TraiteListe<CMappageEntiteParente>(m_listeMappagesParents);
            if ( result )
                result = serializer.TraiteListe<CMappageEntitesFilles>(m_listeMappagesFilles);
            if (!result) 
                return result;

            return result;

        }

        //------------------------------------------------
        public CResultAErreur ImportTable(
            DataTable table,
            CContexteImportDonnee contexteImport,
            IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;

            Dictionary<DataRow, DataRow> m_dicSourceToDest = new Dictionary<DataRow, DataRow>();
            contexteImport.TableSource = table;
            int nNbDoTo = table.Rows.Count;
            if (contexteImport.StartLine != null)
                nNbDoTo -= contexteImport.StartLine.Value;
            if (contexteImport.EndLine != null)
                nNbDoTo -= (table.Rows.Count - contexteImport.EndLine.Value) - 1;
            int nNbDone = 0;
            if (indicateur != null)
                indicateur.SetBornesSegment(0, nNbDoTo);
            int nStart = contexteImport.StartLine == null ? 0 : contexteImport.StartLine.Value;
            int nEnd = contexteImport.EndLine == null ? table.Rows.Count - 1 : Math.Min(contexteImport.EndLine.Value, table.Rows.Count-1);
            for (int nRowIndex = nStart; nRowIndex <= nEnd; nRowIndex++)
            {
                DataRow row = table.Rows[nRowIndex];
                if (indicateur != null)
                    indicateur.SetInfo(I.T("Import line @1|20106", nRowIndex.ToString()));
                contexteImport.StartImportRow(row, nRowIndex);
                result = ImportRow(row, contexteImport, null, false);
                nNbDone++;
                if (indicateur != null)
                    indicateur.SetValue(nNbDone);
                if (!result)
                {
                    contexteImport.AddLog(new CLigneLogImport(
                        ETypeLigneLogImport.Error,
                        row,
                        null,
                        contexteImport,
                        I.T("Error on line import|20102")));
                    if (!contexteImport.BestEffort)
                        return result;
                    result = CResultAErreur.True;
                }
            }
            return result;
        }
    

        //------------------------------------------------
        public CResultAErreur ImportRow(
            DataRow row, 
            CContexteImportDonnee contexteImport,
            CValeursImportFixe valeursFixes,
            bool bIsRoot)
        {
            CResultAErreur result = CResultAErreur.True;
            contexteImport.PushIdConfigMappage(IdMappage);
            try
            {

                string strFiltre = "";
                CResultAErreurType<CObjetDonnee> resObjet = FindObjet(row, contexteImport, valeursFixes, ref strFiltre);
                if (!resObjet)
                {
                    contexteImport.AddLog(new CLigneLogImport(
                        ETypeLigneLogImport.Error,
                        row,
                        "",
                        contexteImport,
                        resObjet.Erreur.ToString()));
                    result.EmpileErreur(resObjet.Erreur);
                    return result;
                }
                if (resObjet.DataType == null && this.OptionCreation == EOptionCreationElementImport.None)
                {
                    contexteImport.AddLog(new CLigneLogImport(
                        ETypeLigneLogImport.Alert,
                        row,
                        "",
                        contexteImport,
                        I.T("Line not imported because creation is forbidden for this element|20095")));
                    return result;
                }

                CObjetDonnee objet = resObjet.DataType;
                bool bIsNew = false;
                if (objet == null)
                {
                    bIsNew = true;
                    //Création
                    try
                    {
                        objet = Activator.CreateInstance(TypeEntite, new object[] { contexteImport.ContexteDonnee }) as CObjetDonnee;
                    }
                    catch { }
                    if (objet == null)
                    {
                        result.EmpileErreur(I.T("Error while creating object @1|20096", DynamicClassAttribute.GetNomConvivial(TypeEntite)));
                        contexteImport.AddLog(new CLigneLogImport(
                            ETypeLigneLogImport.Error,
                            row,
                            null,
                            contexteImport,
                            result.Erreur.ToString()));
                        return result;
                    }
                    if (objet is CObjetDonneeAIdNumeriqueAuto)
                        ((CObjetDonneeAIdNumeriqueAuto)objet).CreateNewInCurrentContexte();
                    else
                        objet.CreateNewInCurrentContexte(null);
                    contexteImport.AddElementQuiAEteAjoute(objet);
                    contexteImport.SetEntiteForFiltre(strFiltre, objet);
                }

                if (bIsRoot)
                    contexteImport.SetElementPourLigne(contexteImport.CurrentRowIndex, objet);

                contexteImport.SetObjetImporteForIdMappage(IdMappage, objet);

                result = UpdateObjet(row, objet, bIsNew, contexteImport, valeursFixes);
                if (!result)
                {
                    contexteImport.AddLog(new CLigneLogImport(
                        ETypeLigneLogImport.Error,
                        row,
                        "",
                        contexteImport,
                        result.Erreur.ToString()));
                    return result;
                }
            }
            catch ( Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            finally
            {
                contexteImport.PopIdConfigMappage();
            }
            return result;

        }

        //------------------------------------------------
        private CResultAErreur UpdateObjet ( 
            DataRow row, 
            CObjetDonnee objet, 
            bool bIsNewObject, 
            CContexteImportDonnee contexteImport,
            CValeursImportFixe valeursFixes )
        {
            CResultAErreur result = CResultAErreur.True;

            foreach ( CMappageChampSimple mappage in MappagesChampsSimples )
            {
                if ( mappage.Source != null && mappage.Source.ShouldImport ( row, contexteImport))
                { 
                    try
                    {
                        contexteImport.PushChamp(mappage.Propriete);
                        result = mappage.GetValue(row, contexteImport);
                        if (!result)
                        {
                            contexteImport.AddLog(new CLigneLogImport(
                                ETypeLigneLogImport.Error,
                                row,
                                mappage.Source.LibelleSource,
                                contexteImport,
                                result.Erreur.ToString()));
                            return result;
                        }

                        try
                        {
                            CInterpreteurProprieteDynamique.SetValue(objet, mappage.Propriete, result.Data);

                            //Stockage valeur témoin
                            CSourceSmartImportField sourceField = mappage.Source as CSourceSmartImportField;
                            if ( sourceField != null )
                            {
                                result = CInterpreteurProprieteDynamique.GetValue ( objet, mappage.Propriete );
                                if (result)
                                    contexteImport.SetValeurTemoin(sourceField.NomChampSource, result.Data);
                                result = CResultAErreur.True;
                            }
                        }
                        catch (Exception e)
                        {
                            result.EmpileErreur(new CErreurException(e));
                            result.EmpileErreur(I.T("Error while affecting value @1 to field @2|20097",
                                result.Data == null ? "null" : result.Data.ToString()));
                            contexteImport.AddLog(new CLigneLogImport(
                                    ETypeLigneLogImport.Error,
                                    row,
                                    mappage.Source.LibelleSource,
                                    contexteImport,
                                    result.Erreur.ToString()));
                            return result;
                        }
                    }
                    finally
                    {
                        contexteImport.PopChamp();
                    }
                }
            }

            foreach (CMappageEntiteParente mapParent in MappagesEntitesParentes)
            {
                if ( mapParent.Source != null && mapParent.Source.ShouldImport ( row, contexteImport ))
                {
                    try
                    {
                        contexteImport.PushChamp(mapParent.Propriete);

                        CResultAErreurType<CObjetDonnee> resObjet = mapParent.GetObjetAssocie(row, contexteImport, true);
                        if (!resObjet)
                        {
                            contexteImport.AddLog(new CLigneLogImport(
                                ETypeLigneLogImport.Error,
                                row,
                                mapParent.Source.LibelleSource,
                                contexteImport,
                                resObjet.MessageErreur));
                        }
                        else
                        {
                            try
                            {
                                if (mapParent.AlternativeSetMethodInfo != null)
                                    mapParent.AlternativeSetMethodInfo.Invoke(objet, new object[] { resObjet.DataType });
                                else
                                {
                                    result = CInterpreteurProprieteDynamique.SetValue(objet, mapParent.Propriete, resObjet.DataType);
                                    if (!result && mapParent.Propriete is CDefinitionProprieteDynamiqueDotNet)
                                    {
                                        //Tente affectation via fonction spécifique
                                        PropertyInfo info = objet.GetType().GetProperty(mapParent.Propriete.NomProprieteSansCleTypeChamp);
                                        if (info != null)
                                        {
                                            SpecificImportSetAttribute spec = info.GetCustomAttribute<SpecificImportSetAttribute>(true);
                                            if (spec != null)
                                            {
                                                MethodInfo method = objet.GetType().GetMethod(spec.NomMethodeSetSpecifique);
                                                if (method != null)
                                                    method.Invoke(objet, new object[] { resObjet.DataType });
                                                mapParent.AlternativeSetMethodInfo = method;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                result.EmpileErreur(new CErreurException(e));
                                result.EmpileErreur(I.T("Error while affecting value @1 to field @2|20097",
                                    result.Data == null ? "null" : result.Data.ToString()));
                                contexteImport.AddLog(new CLigneLogImport(
                                    ETypeLigneLogImport.Error,
                                        row,
                                        mapParent.Source.LibelleSource,
                                        contexteImport,
                                        result.Erreur.ToString()));
                                return result;
                            }
                        }
                    }
                    finally 
                    { 
                        contexteImport.PopChamp(); 
                    }
                
                }
            }

            if ( valeursFixes != null )
            {
                foreach (KeyValuePair<string, object> kv in valeursFixes.ValeursFixes)
                    objet.Row[kv.Key] = kv.Value == null ? DBNull.Value : kv.Value;
            }

            foreach ( CMappageEntitesFilles mapFille in m_listeMappagesFilles )
            {
                try
                {
                    contexteImport.PushChamp(mapFille.Propriete);
                    result = mapFille.ImportRow(row, contexteImport, objet);
                    if (!result)
                    {
                        contexteImport.AddLog(new CLigneLogImport(
                                    ETypeLigneLogImport.Error,
                                    row,
                                    "",
                                    contexteImport,
                                    result.Erreur.ToString()));
                        result = CResultAErreur.True;
                    }
                }
                finally
                {
                    contexteImport.PopChamp();
                }
            }

            return result;
        }

        //------------------------------------------------
        public CResultAErreurType<CObjetDonnee> FindObjet ( 
            DataRow rowSource, 
            CContexteImportDonnee contexteImport, 
            CValeursImportFixe valeursFixes,
            ref string strFiltre )
        {
            CResultAErreurType<CObjetDonnee> resObjet = new CResultAErreurType<CObjetDonnee>();
            CResultAErreur res = CResultAErreur.True;

            StringBuilder blFiltre = new StringBuilder();

            if ( valeursFixes != null )
            {
                foreach ( KeyValuePair<string, object> kv in valeursFixes.ValeursFixes)
                {
                    blFiltre.Append(kv.Key);
                    blFiltre.Append("=");
                    blFiltre.Append(kv.Value == null ? "null" : kv.Value.ToString());
                    blFiltre.Append(";");
                }
            }
           
            
            //Création du filtre
            CComposantFiltreDynamiqueEt compoPrincipal = new CComposantFiltreDynamiqueEt();

            //Recherche à partir des champs simples
            foreach ( CMappageChampSimple mapSimple in MappagesChampsSimples)
            {
                if ( mapSimple.UseAsKey )
                {
                    res = mapSimple.GetValue(rowSource, contexteImport);
                    if (!res)
                    {
                        res.EmpileErreur(I.T("Erreur while searching object|20093"));
                        resObjet.EmpileErreur(res.Erreur);
                        return resObjet;
                    }

                    //Stockage valeur témoin
                    CSourceSmartImportField sourceField = mapSimple.Source as CSourceSmartImportField;
                    if ( sourceField != null )
                        contexteImport.SetValeurTemoin(sourceField.NomChampSource, res.Data);

                    blFiltre.Append(mapSimple.Propriete.NomPropriete);
                    if ( res.Data == null )
                    {
                        CComposantFiltreDynamiqueTestNull testNull = new CComposantFiltreDynamiqueTestNull();
                        testNull.Champ = mapSimple.Propriete;
                        testNull.TestNull = true;
                        compoPrincipal.AddComposantFils ( testNull );
                        blFiltre.Append("=null;");
                    }
                    else
                    {
                        CComposantFiltreDynamiqueValeurChamp testValeur = new CComposantFiltreDynamiqueValeurChamp();
                        testValeur.Champ = mapSimple.Propriete;
                        testValeur.IdOperateur = CComposantFiltreOperateur.c_IdOperateurEgal;
                        testValeur.ExpressionValeur = new C2iExpressionConstante(res.Data);
                        compoPrincipal.AddComposantFils(testValeur);
                        blFiltre.Append("=");
                        blFiltre.Append(res.Data.ToString());
                        blFiltre.Append(";");
                    }
                }
            }


            //Recherche à partir des objets parents
            foreach ( CMappageEntiteParente mapParent in m_listeMappagesParents)
            {
                if ( mapParent.UseAsKey)
                {
                    CResultAErreurType<CObjetDonnee> resParent = mapParent.GetObjetAssocie(rowSource, contexteImport, false);
                    if (!resParent)
                    {
                        resObjet.EmpileErreur(resParent.Erreur);
                        return resObjet;
                    }
                    CObjetDonneeAIdNumerique objetId = resParent.DataType as CObjetDonneeAIdNumerique;
                    CDefinitionProprieteDynamique defId = null;
                    object valeurTest = null;

                    if ( typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(mapParent.Propriete.TypeDonnee.TypeDotNetNatif) )
                    {
                        defId = new CDefinitionProprieteDynamiqueDotNet(
                                "Id", 
                                "Id", 
                                new CTypeResultatExpression(typeof(int), false), 
                                false, 
                                true,"");
                        defId.InsereParent(mapParent.Propriete);
                        valeurTest = objetId != null?objetId.Id:-1;
                    }
                    else if ( CObjetDonnee.TypeManageIdUniversel(mapParent.Propriete.TypeDonnee.TypeDotNetNatif))
                    {
                        defId = new CDefinitionProprieteDynamiqueDotNet("Universal id",
                                "IdUniversel",
                                new CTypeResultatExpression ( typeof(string), false),
                                false, true,"" );
                        defId.InsereParent ( mapParent.Propriete);
                        valeurTest = resParent.DataType != null ? resParent.DataType.IdUniversel : null;
                    }
                    else
                    {
                        resObjet.EmpileErreur(I.T("Can not search objet of type @1|20094",
                            DynamicClassAttribute.GetNomConvivial(mapParent.Propriete.TypeDonnee.TypeDotNetNatif)));
                        return resObjet;
                    }
                    blFiltre.Append(defId.NomPropriete);

                    if ( resParent.DataType == null )
                    {
                        CComposantFiltreDynamiqueTestNull testNull = new CComposantFiltreDynamiqueTestNull();
                        testNull.Champ = defId;
                        testNull.TestNull = true;
                        compoPrincipal.AddComposantFils ( testNull );
                        blFiltre.Append("=null;");
                    }
                    else
                    {
                        CComposantFiltreDynamiqueValeurChamp testValeur = new CComposantFiltreDynamiqueValeurChamp();
                        testValeur.Champ = defId;
                        testValeur.IdOperateur = CComposantFiltreOperateur.c_IdOperateurEgal;
                        testValeur.ExpressionValeur = new C2iExpressionConstante(valeurTest);
                        compoPrincipal.AddComposantFils(testValeur);
                        blFiltre.Append("=");
                        blFiltre.Append(valeurTest.ToString());
                        blFiltre.Append(";");
                    }
                }
            }
            if (blFiltre.Length == 0)//Pas de filtre
                return resObjet;
            strFiltre = blFiltre.ToString();
            CObjetDonnee objet = contexteImport.GetEntiteForFiltre ( TypeEntite, strFiltre );
            if ( objet != null )
            {
                resObjet.DataType = objet;
                return resObjet;
            }

            CFiltreDynamique filtre = new CFiltreDynamique(contexteImport.ContexteDonnee);
            filtre.TypeElements = TypeEntite;
            filtre.ComposantPrincipal = compoPrincipal;
            res = filtre.GetFiltreData();
            if (res && res.Data is CFiltreDataAvance)
            {
                if ( valeursFixes != null )
                {
                    CFiltreDataAvance filtreData = res.Data as CFiltreDataAvance;
                    StringBuilder blAdd = new StringBuilder();
                    int nParam = filtreData.Parametres.Count +1;
                    foreach( KeyValuePair<string, object> kv in valeursFixes.ValeursFixes)
                    {
                        if ( kv.Value == null )
                        {
                            blAdd.Append("HasNo(");
                            blAdd.Append(kv.Key);
                            blAdd.Append(") and ");
                        }
                        else
                        {
                            blAdd.Append(kv.Key);
                            blAdd.Append("=@");
                            blAdd.Append((nParam++));
                            blAdd.Append(" and ");
                            filtreData.Parametres.Add ( kv.Value );
                        }
                    }
                    blAdd.Remove(blAdd.Length - 5, 5);
                    if (filtreData.Filtre.Length > 0)
                        blAdd.Insert(0, " and ");
                    filtreData.Filtre += blAdd;
                }
                objet = Activator.CreateInstance(TypeEntite, new object[] { contexteImport.ContexteDonnee }) as CObjetDonnee;
                if (objet.ReadIfExists(((CFiltreData)res.Data)))
                {
                    contexteImport.SetEntiteForFiltre(strFiltre, objet);
                    resObjet.DataType = objet;
                    return resObjet;
                }
            }
            else
            {
                res.EmpileErreur(I.T("Erreur while searching object|20093"));
                resObjet.EmpileErreur(res.Erreur);
                return resObjet;
            }
            return resObjet;

        }

        //-------------------------------------------------------------------------------------------
        public CResultAErreurType<CSessionImport> DoImportSurServeur ( 
            int nIdSession, 
            DataTable tableSource,
            COptionExecutionSmartImport options,
            IIndicateurProgression indicateur)
        {
            ISmartImporterAgent st = C2iFactory.GetNewObjetForSession("CSmartImporterAgent", typeof(ISmartImporterAgent), nIdSession) as ISmartImporterAgent;
            CResultAErreurType<CSessionImport> result = st.DoSmartImport(tableSource, this, options, indicateur);
            return result;
        }

    }

    public interface ISmartImporterAgent
    {
        CResultAErreurType<CSessionImport> DoSmartImport(
            DataTable tableSource,
            CConfigMappagesSmartImport config, 
            COptionExecutionSmartImport options,
            IIndicateurProgression indicateur);
    }
}
