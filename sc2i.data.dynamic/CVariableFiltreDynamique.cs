using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Classe conservée pour compatibilité, ne plus utiliser !!!
	/// </summary>
	/// ///////////////////////////////////////////
	internal abstract class CVariableFiltreDynamique : CVariableDynamique, I2iSerializable, IVariableDynamique
	{
		/// ///////////////////////////////////////////
		public CVariableFiltreDynamique( )
			:base()
		{

		}

		/// ///////////////////////////////////////////
		public CVariableFiltreDynamique( IElementAVariablesDynamiques element )
			:base ( element )
		{
			
		}

	}

	/// ///////////////////////////////////////////
	internal class CVariableFiltreDynamiqueSelectionObjetDonnee : CVariableDynamiqueSelectionObjetDonnee
	{
		public CVariableFiltreDynamiqueSelectionObjetDonnee()
			:base()
		{
		}

		public CVariableFiltreDynamiqueSelectionObjetDonnee ( IElementAVariablesDynamiques element )
			:base ( element )
		{
			
		}
	}

	/// ///////////////////////////////////////////
	internal class CVariableFiltreCalculee : CVariableDynamiqueCalculee
	{
		public CVariableFiltreCalculee()
			:base()
		{
		}

		public CVariableFiltreCalculee ( IElementAVariablesDynamiques element )
			:base ( element )
		{
			
		}
	}
	/// ///////////////////////////////////////////
	internal class CValeurVariableFiltreSaisie : CValeurVariableDynamiqueSaisie
	{
		/// /////////////////////////////////////////////////////////////
		public CValeurVariableFiltreSaisie ( )
			:base()
		{
		}

		/// /////////////////////////////////////////////////////////////
		public CValeurVariableFiltreSaisie ( object valeur, string strDisplay )
			:base ( valeur, strDisplay )
		{
		}
	}

	/// ///////////////////////////////////////////
	internal class CVariableFiltreSaisie : CVariableDynamiqueSaisie
	{
		/// /////////////////////////////////////////////////////////////
		public CVariableFiltreSaisie ( )
			:base()
		{
		}

		/// /////////////////////////////////////////////////////////////
		public CVariableFiltreSaisie ( IElementAVariablesDynamiques elementAVariables )
			:base ( elementAVariables )
		{
		}
	}
}
