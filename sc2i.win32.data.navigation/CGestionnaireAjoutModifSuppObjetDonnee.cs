using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.win32.navigation;
using sc2i.win32.common;
using sc2i.formulaire;
using System.Collections.Generic;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CGestionnaireAjoutModifSuppObjetDonnee.
	/// </summary>
	public delegate void AfterCreateFormEditionEventHandler ( object sender, CFormEditionStandard form );

	public class CGestionnaireAjoutModifSuppObjetDonnee
	{
		private CFormNavigateur m_navigateur = CSc2iWin32DataNavigation.Navigateur;
		private CObjetDonneeAIdNumeriqueAuto m_objetContainer = null;
		private string m_strChampLienParent = "";
		private Type m_typeObjet = typeof(CObjetDonneeAIdNumeriqueAuto);
		private CReferenceTypeForm m_referenceForm = null;
		
		//Indique la propriété à appeller sur l'objet passé à NewCFormEdition
		//Pour obtenir l'objet à éditer.
		//Par défaut "", c'est l'objet lui-même qui est édité.
		private string m_strProprieteObjetAEditer = "";

		/// //////////////////////////////////////////////////////////////
		public CGestionnaireAjoutModifSuppObjetDonnee
			(
			Type typeObjets,
			CReferenceTypeForm referenceForm )
		{
			m_typeObjet = typeObjets;
			m_referenceForm = referenceForm;											
		}
		/// //////////////////////////////////////////////////////////////
		public CGestionnaireAjoutModifSuppObjetDonnee
			(
			Type typeObjets,
			CReferenceTypeForm referenceFormEdition, 
			CObjetDonneeAIdNumeriqueAuto objetContainer,
			string strChampLienParent )
		{
			m_typeObjet = typeObjets;
			m_referenceForm = referenceFormEdition;
			m_objetContainer = objetContainer;
			m_strChampLienParent = strChampLienParent;
		}
		
		/// //////////////////////////////////////////////////////////////
		public string ChampLienParent
		{
			get
			{
				return m_strChampLienParent;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public string ProprieteObjetAEditer
		{
			get
			{
				return m_strProprieteObjetAEditer;
			}
			set
			{
				m_strProprieteObjetAEditer = value;
			}
		}
		
			/// //////////////////////////////////////////////////////////////
			public CFormNavigateur Navigateur
		{
			get
			{
				return m_navigateur;
			}
			set
			{
				m_navigateur = value;
			}
		}
		/// //////////////////////////////////////////////////////////////
		public CObjetDonneeAIdNumeriqueAuto ObjetContainer
		{
			get
			{
				return m_objetContainer;
			}
			set
			{
				m_objetContainer = value;
			}
		}

		public event EventHandler BeforeAfficheForm;
		public event AfterCreateFormEditionEventHandler AfterCreateFormEdition;
		//---------------------------------------------------------------------------
		private CFormEditionStandard NewCFormEdition(CObjetDonneeAIdNumeriqueAuto objet, CListeObjetsDonnees liste)
		{
			if ( m_strProprieteObjetAEditer.Trim() != "" )
			{
				objet = (CObjetDonneeAIdNumeriqueAuto)CInterpreteurTextePropriete.GetValue ( objet, m_strProprieteObjetAEditer );
				liste = liste.GetDependances ( m_strProprieteObjetAEditer );
			}
            CFormEditionStandard frm = null;

            // YK 20/02/09 : On utilise ici le nouveau CFormFinder
            if (m_referenceForm == null)
            {
                m_referenceForm = CFormFinder.GetRefFormToEdit(m_typeObjet);
            }
            if (m_referenceForm != null)
                frm = (CFormEditionStandard)m_referenceForm.GetForm(objet, liste);
			if (frm != null)
			{
				if ( AfterCreateFormEdition != null )
					AfterCreateFormEdition(this, frm);
				frm.AfterSuppression += new ObjetDonneeEventHandler(OnSuppressionDansFormEdition);
				frm.AfterValideModification += new ObjetDonneeEventHandler(OnValidationDansFormEdition);
				frm.AfterAnnulationModification += new ObjetDonneeEventHandler(OnAnnulationDansFormEdition);
			}
			return frm;
		}

		public event ObjetDonneeEventHandler AfterSuppressionDansFormEdition;
		/// //////////////////////////////////////////////////////////////
		private void OnSuppressionDansFormEdition ( object sender, CObjetDonneeEventArgs args )
		{
			if ( AfterSuppressionDansFormEdition != null )
				AfterSuppressionDansFormEdition ( sender, args );
		}

		public event ObjetDonneeEventHandler AfterAnnulationDansFormEdition;
		/// //////////////////////////////////////////////////////////////
		private void OnAnnulationDansFormEdition ( object sender, CObjetDonneeEventArgs args )
		{
			if ( AfterAnnulationDansFormEdition != null )
				AfterAnnulationDansFormEdition ( sender, args );
		}

		public event ObjetDonneeEventHandler AfterValidationDansFormEdition;
		/// //////////////////////////////////////////////////////////////
		private void OnValidationDansFormEdition ( object sender, CObjetDonneeEventArgs args )
		{
			if ( AfterValidationDansFormEdition != null )
				AfterValidationDansFormEdition ( sender, args );
		}


        /// <summary>
        /// Evenement lancé après la création d'un nouvel objet. Il
        /// est alors possible au handler d'évenement de modifier l'élément ajouté
        /// </summary>
		public event OnNewObjetDonneeEventHandler OnNewObjetDonnee;
		

		public ObjetDonneeEventHandler AfterValideCreateObjetDonnee;
		/// //////////////////////////////////////////////////////////////
		public CResultAErreur Ajouter( CListeObjetsDonnees listePourFormEdition, 
            IEnumerable<CAffectationsProprietes> affectations)
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteDonnee ctx = null;
			if (ObjetContainer!=null)
				ctx = ObjetContainer.ContexteDonnee;
			else
				ctx = CSc2iWin32DataClient.ContexteCourant;
			CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto) Activator.CreateInstance( m_typeObjet , new object[] {ctx} );
			objet.CreateNew();

			if(ObjetContainer != null)
			{
				PropertyInfo prop = objet.GetType().GetProperty(m_strChampLienParent);
				if ( prop == null )
                    throw new Exception(I.T("The property @1 has not been found in the class @2 |30025",m_strChampLienParent,objet.GetType().ToString()));
					//throw new Exception("La propriété '"+m_strChampLienParent+"' n'a pas"+
					//	" été trouvée dans la classe "+objet.GetType());
				MethodInfo method = prop.GetSetMethod();
				object[] container = {ObjetContainer};
				method.Invoke(objet, container);
			}
            bool bCancel = false;
			if ( OnNewObjetDonnee != null )
				OnNewObjetDonnee ( this, objet, ref bCancel );

            if (bCancel)
            {
                objet.CancelCreate();
                result.EmpileErreur(I.T("Creation canceled|20007"));
                return result;
            }

			

			if (BeforeAfficheForm!=null)
				BeforeAfficheForm(null, null);
			IFormNavigable frm = (IFormNavigable) NewCFormEdition(objet, listePourFormEdition);
			if ( frm == null )
			{
				result.EmpileErreur(I.T("Impossible to add an element|30026"));
				return result;
			}
            if (frm is CFormEditionStandard)
            {
                ((CFormEditionStandard)frm).AfterValideModification += new ObjetDonneeEventHandler(frm_AfterValideCreateObjetDonnee);
                ((CFormEditionStandard)frm).AffectationsPourNouvelElement = affectations;
            }
			Navigateur.AffichePage( frm );

			if (!Navigateur.Visible)
			{
                CSc2iWin32DataNavigation.PushNavigateur(Navigateur);
				Navigateur.ShowDialog();
                CSc2iWin32DataNavigation.PopNavigateur();
			}
			return result;
		}

		/// //////////////////////////////////////////////////////////////
		private	void frm_AfterValideCreateObjetDonnee ( object sender, CObjetDonneeEventArgs args )
		{
			if ( AfterValideCreateObjetDonnee != null )
				AfterValideCreateObjetDonnee ( sender, args );
		}

		/// //////////////////////////////////////////////////////////////
		public CResultAErreur Supprimer ( ArrayList listeElementsASupprimer )
		{
			using ( CWaitCursor waiter = new CWaitCursor() )
			{
				CResultAErreur result = CResultAErreur.True;
				if (listeElementsASupprimer.Count <= 0)
					return result;

				string strListeIds = "";
				CObjetDonneeAIdNumeriqueAuto objetPourSuppressionGlobale = null;
				//Crée une liste de tous les ids éléments  à supprimer
                try
                {
                    foreach (CObjetDonneeAIdNumeriqueAuto objet in listeElementsASupprimer)
                    {
                        strListeIds += objet.Id.ToString() + ",";
                        if (objetPourSuppressionGlobale == null)
                            objetPourSuppressionGlobale = objet;
                    }
                    strListeIds = strListeIds.Substring(0, strListeIds.Length - 1);

                    using (CContexteDonnee contexteToDelete = new CContexteDonnee(objetPourSuppressionGlobale.ContexteDonnee.IdSession, true, false))
                    {
                        result = contexteToDelete.SetVersionDeTravail(objetPourSuppressionGlobale.ContexteDonnee.IdVersionDeTravail, true);
                        if (!result)
                            return result;
                        contexteToDelete.BeginModeDeconnecte();


                        CListeObjetsDonnees liste = new CListeObjetsDonnees(contexteToDelete,
                            objetPourSuppressionGlobale.GetType(), false);

                        liste.Filtre = new CFiltreData(
                            objetPourSuppressionGlobale.GetChampId() + " in (" +
                            strListeIds + ")");

                        result = CObjetDonneeAIdNumeriqueAuto.Delete(liste);
                        if (!result)
                        {
                            contexteToDelete.CancelModifsEtEndModeDeconnecte();
                            result.EmpileErreur(I.T("Error while deleting element|30032"));
                            return result;
                        }
                        else
                            result = contexteToDelete.CommitModifsDeconnecte();
                    }
                }
                catch
                {
                    foreach (CObjetDonnee objet in listeElementsASupprimer)
                    {
                        result = objet.Delete();
                        if (!result)
                        {
                            CSc2iWin32DataClient.ContexteCourant.CancelModifsEtEndModeDeconnecte();
                            result.EmpileErreur(I.T("Error while deleting element|30032"));
                            break;
                        }
                    }
                    if (result)
                        result = CSc2iWin32DataClient.ContexteCourant.CommitModifsDeconnecte();
                }
				return result;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public CResultAErreur Modifier ( 
            CObjetDonneeAIdNumeriqueAuto objet, 
            CListeObjetsDonnees listePourFormEdition,
            IEnumerable<CAffectationsProprietes> affectations
            )
		{
			CResultAErreur result = CResultAErreur.True;
			if (BeforeAfficheForm!=null)
				BeforeAfficheForm(null, null);
			IFormNavigable form = (IFormNavigable) NewCFormEdition(objet, listePourFormEdition) ;
			if ( form == null )
			{
				result.EmpileErreur(I.T("Impossible to modify the element|30033"));
				return result;
			}
            CFormEditionStandard frmStd = form as CFormEditionStandard;
            if (frmStd != null)
                frmStd.AffectationsPourNouvelElement = affectations;
			if (!Navigateur.IsHandleCreated)
			{
				Navigateur.CreateControl();
			}
				
			Navigateur.AffichePage( form );
			
			if (!Navigateur.Visible)
			{
                CSc2iWin32DataNavigation.PushNavigateur(Navigateur);
				Navigateur.ShowDialog();
                CSc2iWin32DataNavigation.PopNavigateur();
			}

			return result;
		}

	}
}
