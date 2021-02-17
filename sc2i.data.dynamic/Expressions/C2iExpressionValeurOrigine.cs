using System;
using System.Collections;
using System.Reflection;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.expression;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Retourne la valeur d'origine (avant modification) d'une donnée. Cette fonction n'est 
	/// valable que pour certains objets
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class C2iExpressionValeurOrigine : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionValeurOrigine()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateur2iExpression.Register2iExpression ( new C2iExpressionValeurOrigine().IdExpression,
				typeof(C2iExpressionValeurOrigine) );
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "GetOriginalValue", 
				new CTypeResultatExpression(typeof(object), false),
                I.TT(GetType(), "GetOriginalValue( object, property name (text) )\n Returns the original (string) value of the property of the required object. The original value is the value of the property before modification. This function works only with some objects: it is necessary that the property corresponds to a database field|232"),
				CInfo2iExpression.c_categorieDivers);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(CObjetDonnee), typeof(string) ) );
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 2;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			CResultAErreur result = CResultAErreur.True;

			try
			{
				if ( !(listeParametres[0] is CObjetDonnee) )
				{
					result.EmpileErreur(I.T("The first argument does not return a valid object for this operation|230"));
					return result;
				}
				string strChamp = listeParametres[1].ToString();
				//Identifie le champ
				Type tp = listeParametres[0].GetType();

				CObjetDonnee objet = (CObjetDonnee)listeParametres[0];

				//Est-ce une propriété ?
				PropertyInfo propInteressante = tp.GetProperty(strChamp);

				string strUpper = strChamp.ToUpper(); ;

				if ( propInteressante == null )
				{
					//Recherche sur DynamicField ou TableField;
					foreach ( PropertyInfo info in tp.GetProperties() )
					{
						if ( info.Name.ToUpper() == strUpper )
						{
							propInteressante = info;
							break;
						}
						object[] attribs = info.GetCustomAttributes ( typeof ( DynamicFieldAttribute), true );
						if ( attribs.Length > 0 )
						{
							if ( ((DynamicFieldAttribute)(attribs[0])).NomConvivial.ToUpper() == strUpper )
							{
								propInteressante = info;
								break;
							}
						}
						attribs = info.GetCustomAttributes ( typeof ( TableFieldPropertyAttribute ), true );
						if ( attribs.Length > 0)
						{
							if ( ((TableFieldPropertyAttribute)attribs[0]).NomChamp.ToUpper() == strUpper )
							{
								propInteressante = info;
								break;
							}
						}
					}
				}
				if ( propInteressante == null )
				{
					result.EmpileErreur(I.T("The property @1 isn't found in the @2 type|231",strChamp,objet.GetType().ToString()));
					return result;
				}
				DataRowVersion oldVersion = objet.VersionToReturn;
				try
				{
					if ( objet.Row.HasVersion ( DataRowVersion.Original ) )
						objet.VersionToReturn = System.Data.DataRowVersion.Original;
					result.Data = propInteressante.GetGetMethod(true).Invoke(objet, null);
					return result;
				}
				catch 
				{
				}
				finally
				{
					objet.VersionToReturn = oldVersion;
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				return result;
			}
			return result;
		}
	}
}
