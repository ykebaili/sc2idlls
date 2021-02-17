using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// 
    /// </summary>
    public class CDefinisseurChampsPourTypeSansDefinisseur : IDefinisseurChampCustom
    {
        private CContexteDonnee m_contexte = null;
        private CRoleChampCustom m_roleType = null;

        public CDefinisseurChampsPourTypeSansDefinisseur(CContexteDonnee ctx, CRoleChampCustom role)
        {
            m_contexte = ctx;
            m_roleType = role;
        }

        #region IDefinisseurChampCustom Membres

        public CContexteDonnee ContexteDonnee
        {
            get { return m_contexte; }
        }

        public string DescriptionElement
        {
            get { return ""; }
        }

        public int Id
        {
            get { return 0; }
        }

        public IRelationDefinisseurChamp_ChampCustom[] RelationsChampsCustomDefinis
        {
            get
            {
                return new IRelationDefinisseurChamp_ChampCustom[0];
            }
        }

        public IRelationDefinisseurChamp_Formulaire[] RelationsFormulaires
        {
            get
            {
                // Retourne la liste des relations formulaires
                List<IRelationDefinisseurChamp_Formulaire> listeRelations = new List<IRelationDefinisseurChamp_Formulaire>();
                CListeObjetsDonnees listeFormulaires = CFormulaire.GetListeFormulairesForRole(m_contexte, m_roleType.CodeRole);

                foreach (CFormulaire formulaire in listeFormulaires)
                {
                    CRelationDefinisseurChampPourTypeSansDefinisseur_Formulaire rel =
                        new CRelationDefinisseurChampPourTypeSansDefinisseur_Formulaire(this, formulaire);
                    listeRelations.Add(rel);
                }

                return listeRelations.ToArray();
            }
        }

        public CRoleChampCustom RoleChampCustomDesElementsAChamp
        {
            get
            {
                return m_roleType;
            }
        }

        public CChampCustom[] TousLesChampsAssocies
        {
            get
            {
                List<CChampCustom> listeChamps = new List<CChampCustom>();
                foreach (IRelationDefinisseurChamp_Formulaire rel in RelationsFormulaires)
                {
                    foreach (CRelationFormulaireChampCustom relationForm in rel.Formulaire.RelationsChamps)
                    {
                        listeChamps.Add(relationForm.Champ);
                    }
                }
                return listeChamps.ToArray();
            }
        }

        #endregion
    }

    /////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    public class CRelationDefinisseurChampPourTypeSansDefinisseur_Formulaire : IRelationDefinisseurChamp_Formulaire
    {
        private IDefinisseurChampCustom m_definisseur = null;
        private CFormulaire m_formulaire = null;

        public CRelationDefinisseurChampPourTypeSansDefinisseur_Formulaire(IDefinisseurChampCustom definisseur, CFormulaire form)
        {
            m_definisseur = definisseur;
            m_formulaire = form;
        }

        public IDefinisseurChampCustom Definisseur
        {
            get
            {
                return m_definisseur;
            }
            set
            {
                m_definisseur = value;
            }
        }

        public CFormulaire Formulaire
        {
            get
            {
                return m_formulaire;
            }
            set
            {
                m_formulaire = value;
            }
        }

    }
}
