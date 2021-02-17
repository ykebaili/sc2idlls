using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.data;

namespace sc2i.process.Mail
{
    public interface ICompteMailServeur : IObjetServeur
    {
        CResultAErreur RetrieveMails(int nIdCompteMail);
    }
}
