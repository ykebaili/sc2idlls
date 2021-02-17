using sc2i.common;
using sc2i.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.process.workflow.gels
{
    /// /////////////////////////////////////////////
    public interface IElementGelable : IObjetDonneeAIdNumeriqueAuto
    {
        CListeObjetsDonnees Gels { get; }
        bool EstGelee { get; }
        CResultAErreur Geler(DateTime dateDebut, CCauseGel cause, string strInfo);
        CResultAErreur Degeler(DateTime dateFin, string strInfoFinGel);
        CResultAErreur Geler(DateTime dateDebut, CCauseGel cause, string strInfo, CDbKey keyResponsableDebutGel);
        CResultAErreur Degeler(DateTime dateFin, string strInfoFinGel, CDbKey keyResponsableFinGel);
    }
}
