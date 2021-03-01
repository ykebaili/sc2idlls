using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Data;

using sc2i.common;
using sc2i.drawing;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.multitiers.client;
using sc2i.expression;
using sc2i.common.recherche;
using sc2i.process.recherche;
using System.Runtime.Remoting.Lifetime;


namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CProcess.
	/// </summary>
	public class CProcess : 
        C2iObjetGraphique, 
        IElementAVariablesDynamiquesAvecContexteDonnee
	{
        
        public const string c_idFichier = "2I_PROCESS";


		public const string c_nomVariableElement = "Target_element";
		public const string c_strIdVariableElement = "0";

		private ArrayList m_listeActions = new ArrayList();
		private ArrayList m_listeLiensActions = new ArrayList();

		private string m_strLibelle = "";

		//Id champ->Valeur
		private Hashtable m_tableValeursChamps = new Hashtable();
		
        protected const int c_nNbVariableMaxParProcess = 100000;
		
		private ArrayList m_listeVariables = new ArrayList();

		private int m_nIdNextObjet = 0;
		private CContexteDonnee m_contexteDonnee = null;

		private Type m_typeCible = null;
		private bool m_bSurTableauDeCible = false;

		private CVariableProcessTypeComplexe m_variableCible = null;

		//Contexte executant le process
		private CContexteExecutionAction m_contexteExecutionEnCours = null;

		//Indique si le process s'execute dans en asynchrone
		//ou non
		private bool m_bModeAsynchrone = false;

        private bool m_bModeTransactionnel = false;

		private CInfoDeclencheurProcess m_infoDeclencheur = new CInfoDeclencheurProcess();

		private string m_strLastErreur = "";

		/// <summary>
		/// Objets triés suivant l'ordre des Z
		/// </summary>
		private List<IObjetDeProcess> m_ordreZ = new List<IObjetDeProcess>();

		/// <summary>
		/// Si <>-1, indique que le process peut être vu comme une fonction, et que
		/// la variable m_nIdvariableRetour contient le résultat de la fonction.
		/// </summary>
        private string m_strIdVariableRetour = "";

        /// <summary>
        /// Indique à quel process parent appartient ce process (quand un process
        /// est dans un autre process)
        /// </summary>
        private CProcess m_processParent = null;


		/// ////////////////////////////////////////////////////////
		public CProcess ()
		{
		}

		/// ////////////////////////////////////////////////////////
		public CProcess( CContexteDonnee contexte )
		{
			m_contexteDonnee = contexte;
			Position = new Point ( 0, 0 );
			Size = new Size ( 3000, 3000 );
			m_listeActions.Add ( new CActionDebut(this) );
			m_ordreZ.Add((IObjetDeProcess)m_listeActions[0]);
		}

        /// ////////////////////////////////////////////////////////
        public CProcess(CProcess processParent)
        {
            m_contexteDonnee = processParent.ContexteDonnee;
            Position = new Point(0, 0);
            Size = new Size(3000, 3000);
            m_listeActions.Add(new CActionDebut(this));
            m_ordreZ.Add((IObjetDeProcess)m_listeActions[0]);
            m_processParent = processParent;
        }

        /// ////////////////////////////////////////////////////////
        ///Utilisé quand le process est à l'intérieur d'un autre process
        ///
        public CProcess ProcessParent
        {
            get
            {
                return m_processParent;
            }
            set
            {
                m_processParent = value;
            }
        }

        /// ////////////////////////////////////////////////////////
        public int GetProfondeurProcess()
        {
            if (ProcessParent == null)
                return 0;
            return ProcessParent.GetProfondeurProcess() + 1;
        }

    
        /// ////////////////////////////////////////////////////////
        public bool IsVariableDeProcessParent(IVariableDynamique variable)
        {
            return m_listeVariables.Contains(variable.IdVariable);
        }

		/// /////////////////////////////////////////////
		public string GetNewIdForVariable()
		{
            return CUniqueIdentifier.GetNew();
		}


		/// ////////////////////////////////////////////////////////
		public int IdSession
		{
			get
			{
				if ( m_contexteDonnee != null )
					return m_contexteDonnee.IdSession;
				return -1;
			}
		}

		/// ////////////////////////////////////////////////////////
		///Si vrai, indique qu'une telle action peut être executée
		///directement sur le poste client. Sinon, a besoin d'un contexte
		///séparé pour l'execution du process qui utilise cette action
		public bool PeutEtreExecuteSurLePosteClient
		{
			get
			{
                if (ModeTransactionnel)
                    return false;
				foreach (CAction action in this.ListeActions)
					if (!action.PeutEtreExecuteSurLePosteClient)
						return false;
				return true;
			}
		}

		/// ////////////////////////////////////////////////////////
		public CContexteDonnee ContexteDonnee
		{
			get
			{
				return m_contexteDonnee;
			}
			set
			{
				m_contexteDonnee = value;

				//Stef le 22/6/08 : si le process change de contexte de données,
				//il ne faut pas que des valeurs de variables pointent sur l'autre contexte,
				//Donc, on les réinitialise pour qu'elles soient réévaluées
				foreach (IVariableDynamique variable in m_listeVariables)
				{
					if (variable is CVariableDynamiqueListeObjets)
						m_tableValeursChamps.Remove(variable.IdVariable);
					if (variable is CVariableProcessTypeComplexe)
						m_tableValeursChamps.Remove(variable.IdVariable);
				}
			}
		}

        //-----------------------------------------------------------------------------
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return ContexteDonnee;
            }
        }

		/// ////////////////////////////////////////////////////////
		public int GetIdNouvelObjetDeProcess()
		{
			return m_nIdNextObjet++;
		}	

		/// ////////////////////////////////////////////////////////
		public ArrayList ListeActions 
		{
			get
			{
				return m_listeActions;
			}
		}

		/// /////////////////////////////////////////////
		public IVariableDynamique[] ListeVariables
		{
			get
			{
                AssureVariableCible();
                List<IVariableDynamique> lst = new List<IVariableDynamique>();
                foreach (IVariableDynamique variable in m_listeVariables)
                    lst.Add(variable);
                if (ProcessParent != null)
                    lst.AddRange(ProcessParent.ListeVariables);

                return lst.ToArray();
			}
		}

        /// /////////////////////////////////////////////
        public void AddVariable(IVariableDynamique variable)
        {
            m_listeVariables.Add(variable);
        }

        /// /////////////////////////////////////////////
        public void RemoveVariable(IVariableDynamique variable)
        {
            m_listeVariables.Remove(variable);
        }

		/// /////////////////////////////////////////////
		public void ReinitVariables()
		{
            m_tableValeursChamps = new Hashtable();
		}

		/// /////////////////////////////////////////////
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return m_strLibelle;
			}
			set
			{
				m_strLibelle = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public bool ModeAsynchrone
		{
			get
			{
				return m_bModeAsynchrone;
			}
			set
			{
				m_bModeAsynchrone = value;
			}
		}

        /// ////////////////////////////////////////////////////////
        public bool ModeTransactionnel
        {
            get
            {
                return m_bModeTransactionnel;
            }
            set
            {
                m_bModeTransactionnel = value;
            }
        }

		/// /////////////////////////////////////////////
        public CVariableDynamique GetVariable(string strIdVariable)
        {
            AssureVariableCible();
            foreach (CVariableDynamique variable in ListeVariables)
            {
                if (variable.IdVariable == strIdVariable)
                    return variable;
            }
            return null;
        }

		/// ////////////////////////////////////////////////////////
		public Type TypeCible
		{
			get
			{
				return m_typeCible;
			}
			set
			{
				m_typeCible = value;
				AssureVariableCible();
				
			}
		}

		/// ////////////////////////////////////////////////////////
		public bool SurTableauDeTypeCible
		{
			get
			{
				return m_bSurTableauDeCible;
			}
			set
			{
				m_bSurTableauDeCible = value;
				AssureVariableCible();
				
			}
		}

		/// ////////////////////////////////////////////////////////
		private void AssureVariableCible()
		{
            if (ProcessParent != null)
            {
                ProcessParent.AssureVariableCible();
                return;
            }
			//SC le 27/4/07, si le type cible est null, il faut supprimer la variable de type cible
			/*if( m_typeCible == null )
				return;*/
			if ( m_variableCible != null && (m_variableCible.TypeDonnee.TypeDotNetNatif != m_typeCible ||
				m_variableCible.TypeDonnee.IsArrayOfTypeNatif != m_bSurTableauDeCible ))
			{
				m_listeVariables.Remove ( m_variableCible );
				m_variableCible = null;
			}
			if ( m_typeCible != null && m_variableCible == null )
			{
				m_variableCible = new CVariableProcessTypeComplexe ( this );
                //TESTDBKEYOK
                m_variableCible.IdVariable = c_strIdVariableElement;
				m_variableCible.Nom = c_nomVariableElement;
				m_variableCible.SetTypeDonnee ( new CTypeResultatExpression(m_typeCible, m_bSurTableauDeCible) );
				m_listeVariables.Add ( m_variableCible );
			}
		}

		/// ////////////////////////////////////////////////////////
		public CAction[] Actions
		{
			get
			{
				return (CAction[])m_listeActions.ToArray(typeof(CAction));
			}
		}

		public event EventHandler OnChangeListeActions;
		/// ////////////////////////////////////////////////////////
		public void AddAction ( CAction action )
		{
			if ( action is CActionDebut && GetActionDebut() != null )
				return;
			ListeActions.Add ( action );
			m_ordreZ.Add(action);
			if ( OnChangeListeActions != null )
				OnChangeListeActions ( this, new EventArgs() );
		}

		/// ////////////////////////////////////////////////////////
		public CActionDebut GetActionDebut()
		{
			foreach ( CAction action in ListeActions )
				if ( action is CActionDebut )
					return (CActionDebut)action;
			return null;
		}

		/// ////////////////////////////////////////////////////////
		public void RemoveAction ( CAction action )
		{
			if ( action is CActionDebut )
				return;
			foreach ( CLienAction lien in Liens )
			{
				if ( lien.IdActionArrivee == action.IdObjetProcess ||
					lien.IdActionDepart == action.IdObjetProcess )
					RemoveLien ( lien );
			}
			ListeActions.Remove ( action );
			if (m_ordreZ.Contains ( action ) )
				m_ordreZ.Remove(action);
			if ( OnChangeListeActions != null )
				OnChangeListeActions ( this, new EventArgs() );
		}

		/// ////////////////////////////////////////////////////////
		private ArrayList ListeLiens
		{
			get
			{
				return m_listeLiensActions;
			}
		}

		/// ////////////////////////////////////////////////////////
		public CLienAction[] Liens
		{
			get
			{
				return (CLienAction[])m_listeLiensActions.ToArray(typeof(CLienAction));;
			}
		}

		/// ////////////////////////////////////////////////////////
		public void AddLien ( CLienAction lien )
		{
			ListeLiens.Add ( lien );
			m_ordreZ.Insert(0,lien);
		}

		/// ////////////////////////////////////////////////////////
		public void RemoveLien ( CLienAction lien )
		{
			ListeLiens.Remove ( lien );
			if ( m_ordreZ.Contains ( lien ) )
				m_ordreZ.Remove(lien );
		}

		/// ////////////////////////////////////////////////////////
		public CLienAction[] GetLiensForAction ( CAction action, bool bArrivant )
		{
			return GetLiensForAction ( action, bArrivant, false );
		}		

		/// ////////////////////////////////////////////////////////
		public CLienAction[] GetLiensForAction ( CAction action, bool bArrivant, bool bHorsErreur )
		{
			ArrayList lstLiens = new ArrayList();
			foreach ( CLienAction lien in ListeLiens )
			{
				if ( !bHorsErreur || !(lien is CLienErreur ) )
				{
					if ( bArrivant && lien.IdActionArrivee == action.IdObjetProcess )
						lstLiens.Add ( lien );
					if ( !bArrivant && lien.IdActionDepart == action.IdObjetProcess )
						lstLiens.Add ( lien );
				}
			}
			return (CLienAction[])lstLiens.ToArray(typeof(CLienAction));
		}

		#region I2iObjetGraphique

		/// ////////////////////////////////////////////////////////
		public override I2iObjetGraphique[] Childs
		{
			get
			{
				if (m_ordreZ.Count != ListeActions.Count + ListeLiens.Count)
				{
					m_ordreZ.Clear();
					foreach (CLienAction lien in m_listeLiensActions)
						m_ordreZ.Add(lien);
                    foreach (CAction action in m_listeActions)
                        m_ordreZ.Add(action);
				}
				List<I2iObjetGraphique> lst = new List<I2iObjetGraphique>();
				foreach (IObjetDeProcess objet in m_ordreZ)
					lst.Add((I2iObjetGraphique)objet);
				return lst.ToArray();
				/*ArrayList lst = new ArrayList();
				lst.AddRange ( m_listeActions );
				lst.AddRange ( m_listeLiensActions );
				List<I2iObjetGraphique> lstTriee = new List<I2iObjetGraphique>();
				foreach(int n in m_ordreZ)
					foreach (I2iObjetGraphique ele in lst)
						if (ele is IObjetDeProcess && ((IObjetDeProcess)ele).IdObjetProcess == n)
						{
							lstTriee.Add(ele);
							break;
						}

				//A ENLEVER ?
				if (lst.Count != lstTriee.Count)
					return (I2iObjetGraphique[])lst.ToArray(typeof(I2iObjetGraphique));

				return lstTriee.ToArray();*/
			}
		}

		public override void BringToFront(I2iObjetGraphique child)
		{
			if (child is IObjetDeProcess)
			{
				IObjetDeProcess ele = (IObjetDeProcess)child;
				if (m_ordreZ.Contains(ele))
					m_ordreZ.Remove(ele);

				m_ordreZ.Add(ele);
			}
		}
		public override void FrontToBack(I2iObjetGraphique child)
		{
			if (child is IObjetDeProcess)
			{
				IObjetDeProcess ele = (IObjetDeProcess)child;
				if (m_ordreZ.Contains(ele))
					m_ordreZ.Remove(ele);

				m_ordreZ.Insert(0, ele);
			}
		}

		public override bool IsLock
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		/// ////////////////////////////////////////////////////////
		public override bool AcceptChilds
		{
			get
			{
				return true;
			}
		}
		


		/// ////////////////////////////////////////////////////////
		public override bool AddChild(I2iObjetGraphique child)
		{
			if ( child is CAction )
				AddAction ( (CAction)child);
			if ( child is CLienAction )
				RemoveAction ((CAction)child );
			return true;
		}

		/// ////////////////////////////////////////////////////////
		public override bool ContainsChild(I2iObjetGraphique child)
		{
			if (m_listeActions.Contains(child) )
				return true;
			if ( m_listeLiensActions.Contains(child))
				return true;
			return false;
		}


		/// ////////////////////////////////////////////////////////
		public override void RemoveChild(I2iObjetGraphique child)
		{
			if ( child is CAction )
				RemoveAction ( (CAction)child );
			if ( child is CLienAction )
				RemoveLien ( (CLienAction)child );
		}

		/// ////////////////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
		}

		/// ////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			//V6 : Ajout de l'info de délcencheur
			//V7 : Process sur liste d'objets
			//V8 : Correction bug sur SerializeObjetAIdAuto : stock le type de l'objet
			//V9 : Ajout de m_lastErreur
			//V10 : Ajout de l'id de la variable de retour
			//V11 : sortie de la serialization des variables dans le CSerializerVariablesProcess
			//V12 : l'id est remonté aux IObjetDeProcess et sert à déterminer l'ordre Z
            //V13 : Ajout du mode transactionnel
            return 14; // Passage de Id Variable de retour à String

        }

		/// ////////////////////////////////////////////////////////
		public CAction GetActionFromPoint ( Point pt )
		{
			foreach ( CAction action in ListeActions )
			{
				if ( action.RectangleAbsolu.Contains ( pt.X, pt.Y ) )
					return action;
			}
			return null;
		}

		/// ////////////////////////////////////////////////////////
		public override Point Magnetise ( Point pt )
		{
			pt.X = (int)((double)pt.X/8)*8;
			pt.Y = (int)((double)pt.Y/8)*8;
			return pt;
		}

		#endregion
		
		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if ( !result )
				return result;

            if (serializer.Mode == ModeSerialisation.Lecture &&
                serializer.GetObjetAttache(typeof(CContexteDonnee)) != null)
                ContexteDonnee = (CContexteDonnee)serializer.GetObjetAttache(typeof(CContexteDonnee));
            else
                ContexteDonnee = CContexteDonneeSysteme.GetInstance();
			
			
			serializer.TraiteInt ( ref m_nIdNextObjet );

			if ( nVersion > 0 )
			{
				bool bHasType = TypeCible != null;
				serializer.TraiteBool ( ref bHasType );
				if ( bHasType )
					serializer.TraiteType( ref m_typeCible );
				else
					m_typeCible = null;
			}
			else
				m_typeCible = null;

			//Lit les variables en premier car on en a besoin dans les actions
			serializer.AttacheObjet ( typeof(IElementAVariablesDynamiquesBase), this );
			if ( result )
				result = serializer.TraiteArrayListOf2iSerializable ( m_listeVariables, this );

            foreach (IVariableDynamique variable in m_listeVariables)
                if (variable.IdVariable == c_strIdVariableElement) //TESTDBKEYOK
                    m_variableCible = (CVariableProcessTypeComplexe)variable;

			if ( serializer.Mode == ModeSerialisation.Lecture )
				foreach ( IVariableDynamique var in m_listeVariables )
				{
					if ( var.IdVariable == c_strIdVariableElement && var is CVariableProcessTypeComplexe)
					{
						m_variableCible = (CVariableProcessTypeComplexe)var;
						m_typeCible = m_variableCible.TypeDonnee.TypeDotNetNatif;
						m_bSurTableauDeCible = m_variableCible.TypeDonnee.IsArrayOfTypeNatif;
					}
				}

			

			result = serializer.TraiteArrayListOf2iSerializable ( m_listeActions, this );
			if ( result )
				result = serializer.TraiteArrayListOf2iSerializable ( m_listeLiensActions, this );
			serializer.DetacheObjet ( typeof(IElementAVariablesDynamiquesBase), this );

			//Lit les valeurs des variables
			if ( nVersion > 2 )
				result = SerializeValeursVariables( nVersion, serializer );

			if ( nVersion >= 3 )
				serializer.TraiteBool ( ref m_bModeAsynchrone );
			else
				m_bModeAsynchrone = false;

			if ( nVersion >= 4 )
				serializer.TraiteString ( ref m_strLibelle );

			AssureVariableCible();

			if ( nVersion >= 5 )
			{
				I2iSerializable objet = m_infoDeclencheur;
				serializer.TraiteObject ( ref objet );
				m_infoDeclencheur = (CInfoDeclencheurProcess)objet;
			}

			if ( nVersion >= 7 )
				serializer.TraiteBool ( ref m_bSurTableauDeCible );
			else
				m_bSurTableauDeCible = false;

			if ( nVersion >= 9 )
				serializer.TraiteString ( ref m_strLastErreur );
			else
				m_strLastErreur = "";

            if (nVersion >= 10)
            {
                if (nVersion < 14 && serializer.Mode == ModeSerialisation.Lecture)
                {
                    int nIdTemp = -1;
                    serializer.TraiteInt(ref nIdTemp);
                    m_strIdVariableRetour = nIdTemp.ToString();
                }
                else
                    serializer.TraiteString(ref m_strIdVariableRetour);
            }

			Dictionary<int, I2iObjetGraphique> dicIdToObjet = null;

			if (nVersion >= 12)
			{
				int nbEle = m_ordreZ.Count;
				serializer.TraiteInt(ref nbEle);
				if (serializer.Mode == ModeSerialisation.Lecture)
				{
					dicIdToObjet = new Dictionary<int, I2iObjetGraphique>();
					foreach (CAction action in ListeActions)
						dicIdToObjet[action.IdObjetProcess] = action;
					foreach (CLienAction lien in ListeLiens)
						dicIdToObjet[lien.IdObjetProcess] = lien;
					m_ordreZ.Clear();
					for (int n = 0; n < nbEle; n++)
					{
						int nId = 0;
						serializer.TraiteInt(ref nId);
						I2iObjetGraphique objTmp = null;
						if ( dicIdToObjet.TryGetValue ( nId, out objTmp ) )
							m_ordreZ.Add((IObjetDeProcess)objTmp);
					}
				}
				else if (serializer.Mode == ModeSerialisation.Ecriture)
				{
					foreach (IObjetDeProcess objet in m_ordreZ)
					{
						int nCopy = objet.IdObjetProcess;
						serializer.TraiteInt(ref nCopy);
					}
				}
			}
			else
			{
				m_ordreZ = new List<IObjetDeProcess>();
				foreach (CAction action in ListeActions)
					m_ordreZ.Add(action);
				foreach (CLienAction lien in ListeLiens)
					m_ordreZ.Add(lien);
			}
            if (nVersion >= 13)
                serializer.TraiteBool(ref m_bModeTransactionnel);
            else
                m_bModeTransactionnel = false;


			return result;
		}

		/// ////////////////////////////////////////////////////////
		public CVariableDynamique VariableDeRetour
		{
			get
			{
				if ( m_strIdVariableRetour != "" )
                    return GetVariable(m_strIdVariableRetour);
				return null;
			}
			set
			{
				if ( value == null || !(value is CVariableDynamiqueSaisie))
                    m_strIdVariableRetour = "";
				else
                    m_strIdVariableRetour = value.IdVariable;
			}
		}

		/// ////////////////////////////////////////////////////////
		protected CResultAErreur SerializeValeursVariables( int nVersion, C2iSerializer serializer )
		{
			CResultAErreur result = CResultAErreur.True;
			foreach ( CVariableDynamique variable in m_listeVariables )
			{
				object val = null;
				if (serializer.Mode == ModeSerialisation.Ecriture)
				{
					if (!(variable is CVariableDynamiqueCalculee))
					{
						try
						{
							val = GetValeurChamp(variable);
						}
						catch { }
					}
				}
				result = CSerializerValeursVariables.SerializeValeurVariable(ref val, variable, serializer, ContexteDonnee, nVersion <= 10);
				if (serializer.Mode == ModeSerialisation.Lecture)
					SetValeurChamp(variable, val);
//				result = SerializeValeurVariable ( nVersion, variable, serializer );
				if ( !result )
					return result;
			}
			return result;
		}

		///////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne un proxy permettant d'accéder à distance aux valeurs des
		/// variables de ce process
		/// </summary>
		/// <returns></returns>
		public CProxyElementAVariables GetProxyElementAVariables()
		{
			return new CProxyElementAVariables(this, ContexteDonnee);
		}


        public CProxyElementAVariables GetProxyElementAVariables(C2iSponsor sponsor)
        {
            CProxyElementAVariables proxy = new CProxyElementAVariables(this, ContexteDonnee, sponsor);
            sponsor.Register(proxy);

            return proxy;

        }

		/*/// ////////////////////////////////////////////////////////
		public byte[] GetSerialisationValeurVariable(int nId)
		{
			CVariableDynamique variable = GetVariable(nId);
			if (variable == null)
				return null;
			object val = GetValeurChamp(variable);
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);
			CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
			CResultAErreur result = CSerializerValeursVariablesProcess.SerializeValeurVariable(ref val, variable, serializer, ContexteDonnee);
			if (result)
				return stream.GetBuffer();
			return null;
		}*/

	/*	/// ////////////////////////////////////////////////////////
		protected CResultAErreur SerializeValeurVariable ( int nVersion, CVariableDynamique variable, C2iSerializer serializer )
		{
			CResultAErreur result = CResultAErreur.True;
			object valeur = null;
			if ( serializer.Mode == ModeSerialisation.Ecriture )
				valeur = GetValeurChamp( variable );
			if ( variable is CVariableDynamiqueSaisie || 
				variable is CVariableDynamiqueSelectionObjetDonnee )
			{
				result = serializer.TraiteObjetSimple ( ref valeur );
				if ( serializer.Mode == ModeSerialisation.Lecture )
					SetValeurChamp ( variable, valeur );
			}
			if ( variable is CVariableProcessTypeComplexe )
			{
				Type tp = variable.TypeDonnee.TypeDotNetNatif;
				if ( tp == typeof(int) || tp == typeof(double) || tp == typeof(string) ||
					tp == typeof(DateTime) || tp == typeof ( bool ) )
				{
					if ( nVersion >= 5 )
					{
						object val = GetValeurChamp ( variable );
						serializer.TraiteObjetSimple ( ref val );
						SetValeurChamp ( variable.Id, val );
					}
				}
				else if ( typeof(IObjetDonneeAIdNumeriqueAuto).IsAssignableFrom (tp) )
					#region ObjetAIdNumerique AUTO
				
				{
					if ( !variable.TypeDonnee.IsArrayOfTypeNatif )
					{
						IObjetDonneeAIdNumeriqueAuto objet = (IObjetDonneeAIdNumeriqueAuto)valeur;
						SerializeObjetAIdAuto ( nVersion, objet == null?tp:objet.GetType(), ref objet, serializer );
						if ( serializer.Mode == ModeSerialisation.Lecture )
							SetValeurChamp ( variable, objet );
					}
					else
					{
						int nNb = 0;
						if ( valeur!= null )
							nNb = ((IList)valeur).Count;
						serializer.TraiteInt ( ref nNb );
						switch ( serializer.Mode )
						{
							case ModeSerialisation.Ecriture :
								if ( valeur != null )
								{
									foreach ( IObjetDonneeAIdNumeriqueAuto objet in (IList)valeur )
									{
										IObjetDonneeAIdNumeriqueAuto objetForRef = objet;
										SerializeObjetAIdAuto ( nVersion, objetForRef == null?tp:objetForRef.GetType(), ref objetForRef, serializer );
									}
								}
								break;
							case ModeSerialisation.Lecture :
								ArrayList lst = new ArrayList();
								for ( int nElt = 0; nElt < nNb; nElt++ )
								{
									IObjetDonneeAIdNumeriqueAuto element = null;
									SerializeObjetAIdAuto ( nVersion, tp, ref element, serializer );
									if ( element != null )
										lst.Add ( element );
								}
								SetValeurChamp ( variable, (IObjetDonneeAIdNumeriqueAuto[])lst.ToArray(typeof(IObjetDonneeAIdNumeriqueAuto) ) );
								break;
						}
					}
				}
				#endregion
			}
			return result;
		}

		/// ////////////////////////////////////////////////////////
		private void SerializeObjetAIdAuto ( int nVersion, Type typeObjet, ref IObjetDonneeAIdNumeriqueAuto valeur, C2iSerializer serializer )
		{
			int nId = -1;
			if ( valeur != null && (!(valeur is CObjetDonnee) || ((CObjetDonnee)valeur).Row.RowState != DataRowState.Deleted) )
				nId = valeur.Id;
			serializer.TraiteInt ( ref nId );
			if ( nVersion >= 8 && nId >= 0 )
				serializer.TraiteType ( ref typeObjet );
			if ( nId != -1 && serializer.Mode == ModeSerialisation.Lecture )
			{
				valeur = (IObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance ( typeObjet, new object[]{ContexteDonnee} );
				if(  !valeur.ReadIfExists ( nId ) )
					valeur = null;
			}
		}
*/
		/// ////////////////////////////////////////////////////////
		public CAction GetActionFromId ( int nIdAction )
		{
			foreach ( CAction action in m_listeActions )
				if ( action.IdObjetProcess == nIdAction )
					return action;
			return null;
		}

		/// ////////////////////////////////////////////////////////
		public CResultAErreur SetValeurChamp ( string strIdVariable, object valeur )
		{
            bool bVariableIsMine = ProcessParent == null;;
            if (!bVariableIsMine)
            {
                foreach (CVariableDynamique variable in m_listeVariables)
                    if (variable.IdVariable == strIdVariable)
                    {
                        bVariableIsMine = true;
                        break;
                    }
                if (!bVariableIsMine && ProcessParent != null)
                    return ProcessParent.SetValeurChamp(strIdVariable, valeur);
            }
			object valeurCorrigee = valeur;
            if (valeur != null && strIdVariable == c_strIdVariableElement &&
				m_bSurTableauDeCible && !(valeur.GetType().IsArray) && 
				!typeof(IList).IsAssignableFrom ( valeur.GetType() ) )
			{
				ArrayList lst = new ArrayList();
				lst.Add ( valeur );
				valeurCorrigee = lst.ToArray();
			}
            CObjetDonnee objDonnee = valeurCorrigee as CObjetDonnee;
            if (objDonnee != null && ContexteDonnee != null)
                valeurCorrigee = objDonnee.GetObjetInContexte(ContexteDonnee);
            m_tableValeursChamps[strIdVariable] = valeurCorrigee;


			return CResultAErreur.True;
		}


		/// ////////////////////////////////////////////////////////
		public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
		{
			if ( variable != null )
			{
                return SetValeurChamp(variable.IdVariable, valeur);
			}
			return CResultAErreur.True;
		}

		/// /////////////////////////////////////////////
        public object GetValeurChamp(IVariableDynamique variable)
        {

            if (variable == null)
                return null;

            object valRetour = null;

            if (variable is CVariableDynamiqueCalculee)
            {
                CVariableDynamiqueCalculee variableCalculee = (CVariableDynamiqueCalculee)variable;
                return variableCalculee.GetValeur(this);
            }
            else
            {
                object val = m_tableValeursChamps[variable.IdVariable];
                if (val == null && variable is CVariableDynamiqueSaisie)
                    if(ProcessParent != null)
                        return ProcessParent.GetValeurChamp(variable);
                    else
                        m_tableValeursChamps[variable.IdVariable] = ((CVariableDynamiqueSaisie)variable).GetValeurParDefaut();
                if (val == null && variable is CVariableProcessTypeComplexe)
                    if (ProcessParent != null)
                        return ProcessParent.GetValeurChamp(variable);
                    else
                        m_tableValeursChamps[variable.IdVariable] = ((CVariableProcessTypeComplexe)variable).GetValeurParDefaut(ContexteDonnee);
                
                return m_tableValeursChamps[variable.IdVariable];
            }
        }

		/// /////////////////////////////////////////////
        public object GetValeurChamp(string strIdVariable)
        {
            foreach (CVariableDynamique variable in m_listeVariables)
                if (variable.IdVariable == strIdVariable)
                    return GetValeurChamp(variable);

            if (ProcessParent != null)
                return ProcessParent.GetValeurChamp(strIdVariable);
            
            return null;
        }

		/// /////////////////////////////////////////////
		public string[] GetCategoriesChamps ( Type tp )
		{
			return new string[0];
		}

		/// ///////////////////////////////////////////////////////////
		public virtual CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nProfondeur )
		{
			return GetDefinitionsChamps ( tp, nProfondeur, null );

		}

		/// /////////////////////////////////////////////
		public CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			foreach (CVariableDynamique variable in ListeVariables)
			{
				bool bHasSubs = CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(variable.TypeDonnee.TypeDotNetNatif);
				CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueVariableDynamique(variable, bHasSubs);
				lst.Add(def);
			}
			return lst.ToArray();
		}


		/// /////////////////////////////////////////////
		/// Implémentation du IFournisseurProprietesDynamiques
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
		{
			CFournisseurPropDynStd four = new CFournisseurPropDynStd ( true );
			ArrayList lst = new ArrayList();
			lst.AddRange ( four.GetDefinitionsChamps ( tp, nNbNiveaux, defParente ));
			if ( tp == typeof(CProcess ))
				lst.AddRange(GetProprietesInstance());
			return (CDefinitionProprieteDynamique[])lst.ToArray ( typeof(CDefinitionProprieteDynamique) );
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			if (objet != null)
			{
				lst.AddRange(GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente));
                try
                {
                    if (objet.ElementAVariableInstance != null)
                        lst.AddRange(objet.ElementAVariableInstance.GetProprietesInstance());
                }
                catch { }
			}
			return lst.ToArray();
		}

		/// /////////////////////////////////////////////
		public void OnChangeVariable ( IVariableDynamique variable )
		{
		}

		/// /////////////////////////////////////////////
		public bool IsVariableUtilisee ( IVariableDynamique variable )
		{
			return true;
		}

		/// /////////////////////////////////////////////
		[DynamicField("Release info", "Running action")]
		public CInfoDeclencheurProcess InfoDeclencheur
		{
			get
			{
				return m_infoDeclencheur;
			}
			set
			{
				m_infoDeclencheur = value;
			}
		}

		/// /////////////////////////////////////////////
		[DynamicField("Process", "Running action")]
		public CProcessEnExecutionInDb ProcessInDb
		{
			get
			{
                if(m_contexteExecutionEnCours != null)
				    return m_contexteExecutionEnCours.ProcessEnExecution;
                return null;
			}
		}

		/// /////////////////////////////////////////////
		[DynamicField("Last error")]
		public string LastErreur
		{
			get
			{
				return m_strLastErreur;
			}
			set
			{
				m_strLastErreur = value;
			}
		}

		/// /////////////////////////////////////////////
		[DynamicMethod("ReadRegistre", "Key to read", "Default value")]
		public CCasteurValeurString ReadRegistre ( string strCle, string strValeurParDefaut )
		{
			IDatabaseRegistre registre = (IDatabaseRegistre)C2iFactory.GetNew2iObjetServeur(typeof(IDatabaseRegistre), IdSession);
			return new CCasteurValeurString ( strCle, registre.GetValeurString(strCle, strValeurParDefaut) );
		}


		/// /////////////////////////////////////////////
		[DynamicMethod("Return the process current data version if one. If the contexte works in default version, the result is null")]
		public int? GetCurrentDataVersionId()
		{
			return ContexteDonnee.IdVersionDeTravail;
		}

		/// /////////////////////////////////////////////
		[DynamicField("User key")]
        [ReplaceField("IdUtilisateur","User id")]
		public CDbKey KeyUtilisateur
		{
			get
			{
				try
				{
                    //TESTDBKEYOK
					return m_contexteExecutionEnCours.Branche.KeyUtilisateur;
				}
				catch{}
				return null;
			}
		}
		
		/// /////////////////////////////////////////////
		/// permet d'ajouter des méthodes dynamiques qui utilisent le contexte d'execution
		public CContexteExecutionAction ContexteExecution
		{
			get
			{
				return m_contexteExecutionEnCours;
			}
			set
			{
				m_contexteExecutionEnCours = value;
			}
		}

        /// /////////////////////////////////////////////
        public CActionPointEntree[] PointsEntreeAlternatifs
        {
            get
            {
                List<CActionPointEntree> lst = new List<CActionPointEntree>();
                foreach (CAction action in Actions)
                {
                    if (action is CActionPointEntree)
                        lst.Add(action as CActionPointEntree);
                }
                return lst.ToArray();
            }
        }

        /// <summary>
        /// Cherche un objet dans le process
        /// </summary>
        /// <param name="objetCherche"></param>
        /// <param name="resultat"></param>
        public void ChercheObjet(object objetCherche, CResultatRequeteRechercheObjet resultat)
        {
            //Cherche dans les actions
            foreach (CAction action in Actions)
            {
                if ( action.UtiliseObjet(objetCherche) )
                    resultat.AddResultat(new CNoeudRecherche_Action(action));
            }
        }


        public override int GetHashCode()
        {
            if (Libelle != String.Empty)
            {
                string strHashCode = this.GetType().ToString() + "_" + Libelle;
                return strHashCode.GetHashCode();
            }
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CProcess))
                return false;
            return (this.GetHashCode() == obj.GetHashCode());
        }
    }
}
