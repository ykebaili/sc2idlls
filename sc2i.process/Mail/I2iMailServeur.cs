using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;
using sc2i.common;

namespace sc2i.process.Mail
{
    public interface I2iMailServeur : IObjetServeur
    {
        CResultAErreur RetrieveMessageComplet(int nIdMail);
    }
}