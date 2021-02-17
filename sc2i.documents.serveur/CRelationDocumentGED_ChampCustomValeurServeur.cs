using System;
using System.IO;
using System.Collections;
using System.Threading;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.documents;
using sc2i.data.dynamic.loader;

using sc2i.multitiers.server;


namespace sc2i.documents.serveur
{
	/// <summary>
	/// Description résumée de CRelationDocumentGED_ChampCustomValeurServeur.
	/// </summary>
    public class CRelationDocumentGED_ChampCustomValeurServeur : CRelationElementAChamp_ChampCustomServeur
	{
   		//-------------------------------------------------------------------
		public CRelationDocumentGED_ChampCustomValeurServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationDocumentGED_ChampCustomValeur.c_nomTable;
		}
		
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CRelationDocumentGED_ChampCustomValeur);
		}


	}
}
