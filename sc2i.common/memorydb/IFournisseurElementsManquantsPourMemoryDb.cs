using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.memorydb
{
    /// <summary>
    /// fournit à la demande des éléments à un mémoryDb quand
    /// celui ci en a besoin
    /// </summary>
    public interface IFournisseurElementsManquantsPourMemoryDb
    {
        Type[] TypesFournis { get; }
        CEntiteDeMemoryDb GetEntite(Type typeEntite, string strId, CMemoryDb db);
    }
}
