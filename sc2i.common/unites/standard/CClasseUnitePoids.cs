using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites.standard
{
    //--------------------------------------------------------
    [Serializable]
    public class CClasseUnitePoids : IClasseUnite
    {
        public const string c_idClasse = "WEIGHT";
        public const string c_idG = "G";
        public const string c_idDG = "DG";
        public const string c_idCG = "CG";
        public const string c_idMG = "MG";
        public const string c_idDAG = "DAG";
        public const string c_idHG = "HG";
        public const string c_idKG = "KG";
        public const string c_idQUINTAL = "Q";
        public const string c_idTONNE = "T";
        public const string c_idMICROG = "µG";
        public const string c_idNANOG = "NG";
        public const string c_idPICOG = "PG";
        public const string c_idFEMTOG = "FG";
        
        
        //-----------------------------------------------
        public string Libelle
        {
            get { return I.T("Weight|20052"); }
        }

        //-----------------------------------------------
        public string GlobalId
        {
            get { return c_idClasse; }
        }

        //-----------------------------------------------
        public string UniteBase
        {
            get
            {
                return "g";
            }
        }

        //-----------------------------------------------
        public static void RegisterUnites()
        {
            CClasseUnitePoids Poids = new CClasseUnitePoids();
            CGestionnaireUnites.AddClasseUnite ( Poids) ;
            CGestionnaireUnites.AddUnite ( new CUniteStandard ( 
                I.T("Gram|20053"), "g",c_idG, Poids, 1, 0 ) );
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Decigram|20054"), "dg", c_idDG, Poids, 0.1, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Centigram|20055"), "cg", c_idCG, Poids, 0.01, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Milligram|20056"), "mg", c_idMG, Poids, 0.001, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Decagram|20057"), "dag", c_idDAG, Poids, 10, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Hectogram|20058"), "hg", c_idHG, Poids, 100, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Kilogram|20059"), "kg", c_idKG, Poids, 1000, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Quintal|20060"), "q", c_idQUINTAL, Poids, 100000, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Ton|20061"), "t", c_idTONNE, Poids, 1000000, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Microgram|20162"), "µg", c_idMICROG, Poids, 10e-6, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Nanogram|20163"), "ng", c_idNANOG, Poids, 10e-9, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Picogram|20164"), "pg", c_idPICOG, Poids, 10e-12, 0));
            CGestionnaireUnites.AddUnite(new CUniteStandard(
                I.T("Femtogram|20165"), "fg", c_idFEMTOG, Poids, 10e-15, 0));
            
        }
            
    }

    
}
