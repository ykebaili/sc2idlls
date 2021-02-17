using System;

using sc2i.data;

#if !PDA_DATA

namespace sc2i.data
{
	/// <summary>
	/// Description r�sum�e de IElementDefinissantDesDroits.
	/// </summary>
	public interface IElementDefinissantDesDroits
	{
		CListeObjetsDonnees RelationsDroits{get;}
		CRelationElement_Droit GetNewObjetRelationDroit();
	}
}
#endif