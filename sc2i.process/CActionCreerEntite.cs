using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.data;
using sc2i.process;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.formulaire;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionModifierPropriete.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionCreerEntite : CActionFonction
	{
		private Type m_typeEntiteACreer = typeof(string);

		//Nom de la propriété->expression donnant la valeur
		private Hashtable m_tableValeursProprietes = new Hashtable();
		

		/// /////////////////////////////////////////
		public CActionCreerEntite( CProcess process )
			:base(process)
		{
			Libelle = I.T("Create an entity|137");
			VariableRetourCanBeNull = true;
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Create an entity|137"),
				I.T( "Allows the entity creation|138"),
				typeof(CActionCreerEntite),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

		/// /////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeResultat
		{
			get
			{
				return new CTypeResultatExpression(
					m_typeEntiteACreer,
					false );
			}
		}


		/// /////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
		{
			base.AddIdVariablesNecessairesInHashtable ( table );
			foreach ( C2iExpression expression in m_tableValeursProprietes.Values )
				AddIdVariablesExpressionToHashtable ( expression, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// /////////////////////////////////////////
		public Type TypeEntiteACreer
		{
			get
			{
				return m_typeEntiteACreer;
			}
			set
			{
				m_typeEntiteACreer = value;
			}
		}

		/// /////////////////////////////////////////
		public void ResetFormules()
		{
			m_tableValeursProprietes.Clear();
		}

		/// /////////////////////////////////////////
		public void SetFormuleForPropriete ( string strPropriete, C2iExpression formule )
		{
			m_tableValeursProprietes[strPropriete] = formule;
		}

		/// /////////////////////////////////////////
		public C2iExpression GetFormuleForPropriete ( string strPropriete )
		{
			return ( C2iExpression ) m_tableValeursProprietes[strPropriete];
		}



		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize( serializer );
			
			if ( !result )
				return result;

			bool bHasHadObjetContexte = false;
			if ( serializer.GetObjetAttache ( typeof(CContexteDonnee) )==null )
			{
				bHasHadObjetContexte = true;
				serializer.AttacheObjet ( typeof(CContexteDonnee), Process.ContexteDonnee );
			}

			serializer.TraiteType ( ref m_typeEntiteACreer );

			int nNbCouplesChampExpression = 0;
			foreach ( object valeur in m_tableValeursProprietes.Values )
				if ( valeur != null )
					nNbCouplesChampExpression++;
			serializer.TraiteInt ( ref nNbCouplesChampExpression );

			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach ( DictionaryEntry entry in m_tableValeursProprietes )
					{
						if ( entry.Value != null )
						{
							string str = (string)entry.Key;
							serializer.TraiteString ( ref str );
							I2iSerializable ser = (I2iSerializable)entry.Value;
							result = serializer.TraiteObject ( ref ser, null );
							if ( !result )
								return result;
						}
					}
					break;
				case ModeSerialisation.Lecture :
					m_tableValeursProprietes.Clear();
					for ( int nVal = 0; nVal < nNbCouplesChampExpression; nVal++ )
					{
						string strProp = "";
						serializer.TraiteString ( ref strProp );
						I2iSerializable ser = null;
						result = serializer.TraiteObject ( ref ser );
						if ( !result )
							return result;
						m_tableValeursProprietes[strProp] = ser;
					}
					break;
			}
			
			if ( bHasHadObjetContexte )
				serializer.DetacheObjet ( typeof(CContexteDonnee), Process.ContexteDonnee );
			return result;
		}

		

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = base.VerifieDonnees();

			if ( m_typeEntiteACreer == null )
			{
				result.EmpileErreur (I.T("The type of entity to be created should not be null|139"));
				return result;
			}

			foreach ( C2iExpression expression in m_tableValeursProprietes.Values )
			{
				if ( expression != null )
				{
					result =  expression.VerifieParametres();
					if ( !result )
						return result;
				}
			}

			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance ( m_typeEntiteACreer, new object[]{contexte.ContexteDonnee} );
			objet.CreateNewInCurrentContexte();

			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process);
			contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );

			foreach ( DictionaryEntry entry in m_tableValeursProprietes )
			{
				string strProp = (string)entry.Key;
				C2iExpression expression = (C2iExpression)entry.Value;
				
				//Cherche la propriété
				PropertyInfo info = m_typeEntiteACreer.GetProperty ( strProp );
				string strNomConvivial = strProp;
				object[] attr = info.GetCustomAttributes ( typeof (DynamicFieldAttribute), true );
				if ( attr.Length > 0 )
					strNomConvivial = ((DynamicFieldAttribute)attr[0]).NomConvivial;
				MethodInfo methode = info.GetSetMethod();
				if ( methode != null )
				{
					//Evalue la valeur
					result = expression.Eval ( contexteEval );
					if ( !result )
					{
						result.EmpileErreur(I.T("Error during the @1 value evaluation|140",strNomConvivial));
						return result;
					}
					try
					{
						methode.Invoke ( objet, new object[]{result.Data} );
					}
					catch (Exception e)
					{
						result.EmpileErreur (I.T("Error during the assignment of @1 property|141", strNomConvivial));
                        result.EmpileErreur(e.Message);
						return result;
					}
				}
			}
			if ( VariableResultat != null )
				Process.SetValeurChamp ( VariableResultat, objet );
			CLienAction[] liens = GetLiensSortantHorsErreur();
			if ( liens.Length > 0 )
				result.Data = liens[0];
			return result;
		}
	}
}
