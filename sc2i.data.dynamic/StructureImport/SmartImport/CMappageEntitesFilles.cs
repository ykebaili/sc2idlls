using sc2i.common;
using sc2i.expression;
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
    public class CMappageEntitesFilles : I2iSerializable
    {
        private CDefinitionProprieteDynamique m_propriete = null;

        private List<CMappageEntiteFille> m_listeMappagesEntitesFilles = new List<CMappageEntiteFille>();

        //------------------------------------------------
        public CMappageEntitesFilles()
        {
        }

        //------------------------------------------------
        public CDefinitionProprieteDynamique Propriete
        {
            get
            {
                return m_propriete;
            }
            set
            {
                m_propriete = value;
            }
        }

        //------------------------------------------------
        public IEnumerable<CMappageEntiteFille> MappagesEntitesFilles
        {
            get
            {
                return m_listeMappagesEntitesFilles.AsReadOnly();
            }
            set
            {
                List<CMappageEntiteFille> lst = new List<CMappageEntiteFille>();
                if (value != null)
                    lst.AddRange(value);
                m_listeMappagesEntitesFilles = lst;
            }
        }

        //------------------------------------------------
        public CMappageEntiteFille GetMappageForEntite ( CDbKey key )
        {
            return MappagesEntitesFilles.FirstOrDefault(c => c.ConfigMappage != null && c.ConfigMappage.KeyEntite == key);
        }

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_propriete);
            if (result)
                result = serializer.TraiteListe<CMappageEntiteFille>(m_listeMappagesEntitesFilles);
            return result;
        }

        //------------------------------------------------
        public CResultAErreur ImportRow(DataRow row, CContexteImportDonnee contexteImport, CObjetDonnee parent)
        {
            CResultAErreur result = CResultAErreur.True;
            if ( MappagesEntitesFilles.Count() == 0 )
                return result;
            CResultAErreurType<CValeursImportFixe> resFixe = GetValeursFixesPourFilles(parent);
            if ( !resFixe )
            {
                result.EmpileErreur(resFixe.Erreur);
                return resFixe;
            }
            foreach ( CMappageEntiteFille mappage in MappagesEntitesFilles )
            {
                if (mappage.Source != null && mappage.ConfigMappage != null)
                {
                    if (!mappage.Source.ShouldImport(row, contexteImport))
                    {
                        contexteImport.AddLog(new CLigneLogImport(ETypeLigneLogImport.Error,
                            row,
                            mappage.Source.LibelleSource,
                            contexteImport,
                            I.T("Line was not imported due to import options or condition|20104")));
                    }
                    else
                    {
                        result = mappage.ConfigMappage.ImportRow(row, contexteImport, resFixe.DataType, false);
                        if (!result)
                            return result;
                    }
                }
            }
            return result;
        }

        //------------------------------------------------
        private CResultAErreurType<CValeursImportFixe> GetValeursFixesPourFilles( CObjetDonnee parent )
        {
            CResultAErreurType<CValeursImportFixe> resVals = new CResultAErreurType<CValeursImportFixe>();
            if ( Propriete is CDefinitionProprieteDynamiqueDotNet )
            {
                Type tp = MappagesEntitesFilles.ElementAt(0).ConfigMappage.TypeEntite;
                CDefinitionProprieteDynamique pDeFille = Propriete.GetDefinitionInverse(parent.GetType());
                PropertyInfo info = tp.GetProperty(pDeFille.NomProprieteSansCleTypeChamp);
                if (info != null)
                {
                    RelationAttribute relPar = info.GetCustomAttribute<RelationAttribute>(true);
                    if (relPar != null)
                    {
                        CValeursImportFixe vals = new CValeursImportFixe();
                        for (int nChamp = 0; nChamp < relPar.ChampsFils.Length; nChamp++)
                        {
                            vals.SetValeur(relPar.ChampsFils[nChamp], parent.Row[relPar.ChampsParent[nChamp]]);
                        }
                        resVals.DataType = vals;
                        return resVals;
                    }
                }
            }
            if ( Propriete is CDefinitionProprieteDynamiqueRelationTypeId && parent is CObjetDonneeAIdNumerique)
            {
                RelationTypeIdAttribute att = ((CDefinitionProprieteDynamiqueRelationTypeId)Propriete).Relation;
                if ( att != null )
                {
                    CValeursImportFixe vals = new CValeursImportFixe();
                    vals.SetValeur(att.ChampType, parent.GetType().ToString());
                    vals.SetValeur(att.ChampId, ((CObjetDonneeAIdNumerique)parent).Id);
                    resVals.DataType = vals;
                    return resVals;
                }
            }

            resVals.EmpileErreur(I.T("Can not define parent filter for property @1|20098", Propriete.Nom));
            return resVals;
        }

    }
}
