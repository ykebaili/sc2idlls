using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using sc2i.data.serveur;
using System.Data;
using sc2i.common;
using sc2i.data;

namespace sc2i.data.serveur
{
	public class CGestionnaireHookTraitementAvantSauvegarde
	{
		//Nom de la table->Liste des traintements
		public static Dictionary<string, List<TraitementSauvegardeExterne>> m_listeHooksParType = new Dictionary<string, List<TraitementSauvegardeExterne>>();

        public static CResultAErreur DoTraitementAvantSauvegarde( CContexteDonnee contexte, string strTable, Hashtable tableData )
        {
            CResultAErreur result = CResultAErreur.True;
            DataTable table = contexte.Tables[strTable];
            if ( table == null )
                return result;
            List<TraitementSauvegardeExterne> lst = null;
            if (m_listeHooksParType.TryGetValue(strTable, out lst))
			{
				foreach (TraitementSauvegardeExterne traitement in lst)
				{
					result = traitement(contexte, tableData);
					if (!result)
						return result;
				}
            }
            return result;
		}

		public static void RegisterHook(Type tp, TraitementSauvegardeExterne traitement)
		{
			List<TraitementSauvegardeExterne> lst = null;
			string strNomTable = CContexteDonnee.GetNomTableForType(tp);
			if (strNomTable != null && strNomTable != "")
			{
				if (!m_listeHooksParType.TryGetValue(strNomTable, out lst))
				{
					lst = new List<TraitementSauvegardeExterne>();
					m_listeHooksParType[strNomTable] = lst;
				}
				if (!lst.Contains(traitement))
					lst.Add(traitement);
			}
		}
	}
}
