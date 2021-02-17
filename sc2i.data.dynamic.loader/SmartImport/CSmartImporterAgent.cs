using sc2i.common;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.multitiers.server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.loader.SmartImport
{
    public class CSmartImporterAgent : C2iObjetServeur, ISmartImporterAgent
    {
        //---------------------------------------------------------------------------------
        public CSmartImporterAgent ( int nIdSession )
            :base ( nIdSession)
        { }

        //---------------------------------------------------------------------------------
        public CResultAErreurType<CSessionImport> DoSmartImport(
            DataTable tableSource,
            CConfigMappagesSmartImport config,
            COptionExecutionSmartImport options,
            IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;
            CSessionImport sessionImport = new CSessionImport();
            sessionImport.TableSource = tableSource;
            sessionImport.ConfigMappage = config;
            CResultAErreurType<CSessionImport> resSession = new CResultAErreurType<CSessionImport>();
            resSession.DataType = sessionImport;

            int nTaillePaquet = options.TaillePaquets == null ? tableSource.Rows.Count : options.TaillePaquets.Value;
            int nStartLigne = options.StartLine == null ? 0 : options.StartLine.Value;
            int nEndLigne = options.EndLine == null ? tableSource.Rows.Count - 1 : Math.Min(options.EndLine.Value, tableSource.Rows.Count - 1);
            int nNbToDo = nEndLigne - nStartLigne + 1;
            int nNbDone = 0;

            int? nIdVersionDonnees = null;
            if ( options.UtiliserVersionDonnee)
            {
                using (CContexteDonnee ctx = new CContexteDonnee(IdSession, true, false))
                {
                    CVersionDonnees version = new CVersionDonnees(ctx);
                    version.CreateNew();
                    version.TypeVersion = new CTypeVersion(CTypeVersion.TypeVersion.Previsionnelle);
                    version.Libelle = I.T("Import data @1|20013", DateTime.Now.ToString("G"));
                    result = version.CommitEdit();
                    if ( !result )
                    {
                        resSession.EmpileErreur(result.Erreur);
                        return resSession;
                    }
                    nIdVersionDonnees = version.Id;
                }
            }

            if (nStartLigne > 0)
            { 
                sessionImport.AddLog(new CLigneLogImport(ETypeLigneLogImport.Info,
                    null,
                    "",
                    0,
                    nStartLigne - 1,
                    "Lines 0 to " + (nStartLigne - 1) + " are ignored"));
                sessionImport.SetNonImportees(0, nStartLigne-1);
            }
            if (nEndLigne < tableSource.Rows.Count-1)
            {
                sessionImport.AddLog(new CLigneLogImport(ETypeLigneLogImport.Info,
                    null,
                    "",
                    nEndLigne + 1,
                    tableSource.Rows.Count - 1,
                    "Lines " + (nEndLigne + 1) + " to " + (tableSource.Rows.Count - 1) + " are ignored"));
                sessionImport.SetNonImportees(nEndLigne + 1, tableSource.Rows.Count - 1);
            }

            //Ajoute dans la liste des lignes non importées, les lignes ignorées
            
            using (C2iSponsor sponsor = new C2iSponsor())
            {
                if (indicateur != null)
                {
                    sponsor.Register(indicateur);
                    indicateur.SetBornesSegment(0, nNbToDo);
                }
                try
                {


                    while (nStartLigne <= nEndLigne)
                    {
                        using (CContexteDonnee ctxDonnees = new CContexteDonnee(IdSession, true, false))
                        {
                            if ( m_nIdSession != null )
                                ctxDonnees.SetVersionDeTravail ( nIdVersionDonnees, false);
                            CContexteImportDonnee ctxImport = new CContexteImportDonnee(ctxDonnees);
                            ctxImport.BestEffort = options.BestEffort;

                            ctxImport.StartLine = nStartLigne;
                            ctxImport.EndLine = Math.Min(nStartLigne + nTaillePaquet - 1, tableSource.Rows.Count - 1);
                            if (ctxImport.EndLine > nEndLigne)
                                ctxImport.EndLine = nEndLigne;
                            if (indicateur != null)
                                indicateur.PushSegment(nNbDone, nNbDone + ctxImport.EndLine.Value - ctxImport.StartLine.Value);
                            result = config.ImportTable(
                                tableSource,
                                ctxImport,
                                indicateur);
                            sessionImport.AddLogs(ctxImport.Logs);
                            if (indicateur != null)
                                indicateur.PopSegment();
                            if (result)
                            {
                                if (indicateur != null)
                                    indicateur.SetInfo(I.T("Saving|20012"));
                                result = ctxDonnees.SaveAll(true);
                                if (!result)
                                {
                                    sessionImport.SetNonImportees(ctxImport.StartLine.Value,
                                        ctxImport.EndLine.Value);
                                    sessionImport.AddLog(new CLigneLogImport(
                                        ETypeLigneLogImport.Error,
                                        null,
                                        "",
                                        ctxImport.StartLine.Value,
                                        ctxImport.EndLine.Value,
                                        "Error saving lines " + ctxImport.StartLine.Value + " to " + ctxImport.EndLine.Value + " " +
                                        result.Erreur.ToString()));
                                }
                                if (!result && !ctxImport.BestEffort)
                                    return resSession;
                                if (result)
                                    sessionImport.SetImportees(ctxImport.StartLine.Value,
                                        ctxImport.EndLine.Value);
                            }
                            else
                                sessionImport.SetNonImportees(ctxImport.StartLine.Value,
                                        ctxImport.EndLine.Value);
                            if (!result && !options.BestEffort)
                                return resSession;
                            nStartLigne += nTaillePaquet;
                            nNbDone += nTaillePaquet;
                            if (indicateur != null)
                                indicateur.SetValue(nNbDone);
                        }
                    }
                }
                catch (Exception e)
                {
                    resSession.EmpileErreur(new CErreurException(e));
                }
                finally
                {
                    
                    if (!result && !options.BestEffort)
                        resSession.EmpileErreur(result.Erreur);
                }
                return resSession;
            }
        }

        
    }

}
