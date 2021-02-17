using System;
using System.Collections.Generic;
using System.Text;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	public interface IPanelEditTableExport: IControlALockEdition
	{
		//------------------------------------------
		CResultAErreur InitChamps(
			ITableExport table, 
			ITableExport tableParente, 
			C2iStructureExport structure,
			IElementAVariablesDynamiquesAvecContexteDonnee eltAVariablesPourFiltre);

		//------------------------------------------
		CResultAErreur MajChamps();

		//------------------------------------------
		ITableExport TableEditee { get;}

	}


	/// <summary>
	/// Définit un éditeur de ITableExport
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class EditeurTableExportAttribute : Attribute
	{
		public readonly Type TypeEdite;

		public EditeurTableExportAttribute ( Type typeEdite )
		{
			TypeEdite = typeEdite;
		}
	}
}
