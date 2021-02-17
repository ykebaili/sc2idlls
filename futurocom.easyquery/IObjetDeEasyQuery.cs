using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using sc2i.common;
using sc2i.expression;
using futurocom.easyquery.CAML;

namespace futurocom.easyquery
{
    public interface IObjetDeEasyQuery : I2iObjetGraphique, IFournisseurProprietesDynamiques
    {
        string Id { get; }
        string NomFinal { get; set; }

        /// <summary>
        /// Le data du result contient un datatable,
        /// les colonnes du datatable ont une extendedproperty (CODEQBase.c_extPropColonneId)= id de la
        /// colonne
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        CResultAErreur GetDatas(CListeQuerySource sources);

        IEnumerable<CCAMLItemField> CAMLFields { get; }

        IEnumerable<IColumnDeEasyQuery> Columns { get;}

        /// <summary>
        /// Retourne un nom du type d'objet convivial et traduit
        /// </summary>
        string TypeName { get; }

        CEasyQuery Query { get; }

        void ClearCache();
    }

}
