using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data.serveur.Access
{
    /// <summary>
    /// contrôle tous les champs de la base qui sont non null et s'assure que 
    /// les valeurs dans la base sont bien non nulles
    /// </summary>
    public class COperationAccessCorrigeLesNonNulls : C2iDataBaseUpdateOperation
    {
        public override sc2i.common.CResultAErreur ExecuterOperation(IDatabaseConnexion connection, sc2i.common.IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;
            CAccess97DatabaseConnexion cnx = connection as CAccess97DatabaseConnexion;
            if (cnx == null)
                return result;
            CAccessDataBaseCreator creator = cnx.GetDataBaseCreator() as CAccessDataBaseCreator;
            if ( creator == null )
                return result;
            foreach (Type tp in CContexteDonnee.GetAllTypes())
            {
                CStructureTable structure = CStructureTable.GetStructure(tp);
                if (structure != null && creator.TableExists ( structure.NomTable ))
                {
                    foreach (CInfoChampTable info in structure.Champs)
                    {
                        
                        if (!info.NullAuthorized && !structure.ChampsId.Contains(info))
                        {
                            result = creator.SetValeursParDefautAuxDonneesNulles(structure.NomTable, info);
                            if (!result)
                                return result;
                        }
                    }
                }
            }
            return result;
        }

        public override string DescriptionOperation
        {
            get { return "Check not null fields"; }
        }
    }
}
