using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    public interface ITableDefinition : I2iSerializable, IElementDeQuerySource
    {
        /// <summary>
        /// retourne l'id de la connexion qui permet de remplir cette table
        /// </summary>
        string  SourceId { get; set; }

        string Id { get; }

        string TableName { get; set; }

        string FolderId { get; set; }

        
        IEnumerable<IColumnDefinition> Columns { get; }
        void AddColumn(IColumnDefinition column);
        void RemoveColumn(IColumnDefinition column);

        IColumnDefinition GetColumn(string strIdOrName);

        CEasyQuerySource Base { get; set; }

        IObjetDeEasyQuery GetObjetDeEasyQueryParDefaut();

        /// <summary>
        /// Retourne les données de la table
        /// </summary>
        /// <returns>
        /// Le data du result contient un datatable en cas de succès
        /// </returns>
        /// 
        CResultAErreur GetDatas( CEasyQuerySource source, params string[] strIdsColonnesSources);
    }
}
