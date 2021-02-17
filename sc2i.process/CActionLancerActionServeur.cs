using System;
using System.Drawing;
using System.Collections;

using sc2i.common;
using sc2i.multitiers.client;
using sc2i.expression;
using sc2i.data;

namespace sc2i.process
{
	#region CInfoActionServeur
	[Serializable]
	public class CInfoActionServeur
	{
		private string m_strCode = "";
		private string m_strLibelle = "";
		private string m_strDescription = "";
		private string[] m_strNomsParametres = new string[0];
		
		public CInfoActionServeur()
		{
		}

		public CInfoActionServeur ( 
			string strCode, 
			string strLibelle, 
			string strDescription,
			string[] strNomsParametres)
		{
			m_strCode = strCode;
			m_strLibelle = strLibelle;
			m_strDescription = strDescription;
			m_strNomsParametres = strNomsParametres;
		}

		public string Code
		{
			get
			{
				return m_strCode;
			}
		}
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return m_strLibelle;
			}
		}

		public string Description
		{
			get
			{
				return m_strDescription;
			}
		}

		public string[] NomsParametres
		{
			get
			{
				return m_strNomsParametres;
			}
		}
	}
	#endregion
	/// <summary>
	/// Déclenche une action sur le serveur.
	/// Une action sur le serveur est une action qui est enregistrée sur le serveur
	/// mais inconnue du client (plugin en particulier)
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionLancerActionServeur : CActionLienSortantSimple
	{
		private string m_strCodeActionServeur = "";

		//Nom du paramètre->expression donnant la valeur
		private Hashtable m_tableValeursParametres = new Hashtable();
		/// //////////////////////////////////////////////////////////////
		public CActionLancerActionServeur(CProcess process)
			:base ( process )
		{
			Libelle = I.T("Start server action|184");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Start server action|184"),
				I.T("Execute a known action in the server (plugin for example)|185"),
				typeof(CActionLancerActionServeur),
				CGestionnaireActionsDisponibles.c_categorieDeroulement );
		}


		/// /////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
		{
			foreach (C2iExpression expression in m_tableValeursParametres.Values)
				AddIdVariablesExpressionToHashtable(expression, table);
		}

		/// /////////////////////////////////////////
		public void ResetFormules()
		{
			m_tableValeursParametres.Clear();
		}

		/// /////////////////////////////////////////
		public void SetFormuleForParametre(string strParametre, C2iExpression formule)
		{
			m_tableValeursParametres[strParametre] = formule;
		}

		/// /////////////////////////////////////////
		public C2iExpression GetFormuleForParametre(string strParametre)
		{
			return (C2iExpression)m_tableValeursParametres[strParametre];
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// //////////////////////////////////////////////////////////////
		public string CodeAction
		{
			get
			{
				return m_strCodeActionServeur;
			}
			set
			{
				m_strCodeActionServeur = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
			//Version 1: ajout de paramètres
		}
		
		/// //////////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize ( serializer );
			if ( !result )
				return result;

			serializer.TraiteString ( ref m_strCodeActionServeur );

			if (nVersion >= 1)
			{
				bool bHasHadObjetContexte = false;
				if (serializer.GetObjetAttache(typeof(CContexteDonnee)) == null)
				{
					bHasHadObjetContexte = true;
					serializer.AttacheObjet(typeof(CContexteDonnee), Process.ContexteDonnee);
				}

				int nNbCouplesChampExpression = 0;
				foreach (object valeur in m_tableValeursParametres.Values)
					if (valeur != null)
						nNbCouplesChampExpression++;
				serializer.TraiteInt(ref nNbCouplesChampExpression);

				switch (serializer.Mode)
				{
					case ModeSerialisation.Ecriture:
						foreach (DictionaryEntry entry in m_tableValeursParametres)
						{
							if (entry.Value != null)
							{
								string str = (string)entry.Key;
								serializer.TraiteString(ref str);
								I2iSerializable ser = (I2iSerializable)entry.Value;
								result = serializer.TraiteObject(ref ser, null);
								if (!result)
									return result;
							}
						}
						break;
					case ModeSerialisation.Lecture:
						m_tableValeursParametres.Clear();
						for (int nVal = 0; nVal < nNbCouplesChampExpression; nVal++)
						{
							string strProp = "";
							serializer.TraiteString(ref strProp);
							I2iSerializable ser = null;
							result = serializer.TraiteObject(ref ser);
							if (!result)
								return result;
							m_tableValeursParametres[strProp] = ser;
						}
						break;
				}

				if (bHasHadObjetContexte)
					serializer.DetacheObjet(typeof(CContexteDonnee), Process.ContexteDonnee);
			
			}
			return result;
		}


	
		/// ////////////////////////////////////////////////////////
		public static CInfoActionServeur[] GetListeActionsPossibles ( int nIdSession )
		{
			IDeclencheurActionSurServeur declencheur = (IDeclencheurActionSurServeur)C2iFactory.GetNewObjetForSession("CDeclencheurActionSurServeur", typeof(IDeclencheurActionSurServeur), nIdSession );
			return declencheur.ActionsDisponibles;
		}

		/// //////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			CInfoActionServeur[] infos = GetListeActionsPossibles ( this.Process.IdSession );
			bool bTrouve = false;
			foreach ( CInfoActionServeur info in infos )
				if ( info.Code == m_strCodeActionServeur )
				{
					bTrouve = true;
					break;
				}
			if ( !bTrouve )
			{
				result.EmpileErreur(I.T("No action with the '@1' code|186",m_strCodeActionServeur));
			}
			return result;
		}


		/// //////////////////////////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			IDeclencheurActionSurServeur declencheur = (IDeclencheurActionSurServeur)C2iFactory.GetNewObjetForSession("CDeclencheurActionSurServeur", typeof(IDeclencheurActionSurServeur), contexte.IdSession );
			if ( declencheur == null )
			{
				result.EmpileErreur(I.T("Release action allocation server fail|187"));
				return result;
			}

			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(Process);
			contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);

			Hashtable valeursParametres = new Hashtable();
			foreach (DictionaryEntry entry in m_tableValeursParametres)
			{
				string strProp = (string)entry.Key;
				C2iExpression expression = (C2iExpression)entry.Value;

				//Cherche la propriété
				result = expression.Eval(contexteEval);
				if (!result)
				{
					result.EmpileErreur(I.T( "Error during @1 value evaluation|188", entry.Key.ToString()));
					return result;
				}
				valeursParametres[entry.Key] = result.Data;
			}

			return declencheur.ExecuteAction ( m_strCodeActionServeur, valeursParametres );
		}
	}

	public interface IActionSurServeur
	{
		CResultAErreur Execute ( int nIdSession, Hashtable valeursParametres );

		string CodeType{get;}
		string Libelle{get;}
		string Description{get;}
		string[] NomsParametres { get;}
	}


	public interface IDeclencheurActionSurServeur
	{
		CInfoActionServeur[] ActionsDisponibles{get;}
		CResultAErreur ExecuteAction ( string strCodeAction, Hashtable parametres );
	}
}
