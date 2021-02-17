using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace sc2i.common
{
    /// <summary>
    /// Utilisé pour indiquer une demande de fermeture d'application du framework
    /// En fin d'application, utiliser AppStopper.Set();
    /// Dans tous les threads fils, utiliser C2iStopApp.AppStopper.WaitOne(1, false);
    /// Si le résultat est true, une demande de fin générale est demandée
    /// </summary>
    public static class C2iStopApp
    {
        public static ManualResetEvent AppStopper = new ManualResetEvent(false);
    }
}
