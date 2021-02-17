using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.data
{
    /// <summary>
    /// Indique que les données de cette table doivent être inserées après toutes
    /// les autres tables
    /// ATTENTION, une classe qui a cet attribut ne devrait pas avoir de relation
    /// vers des classes parentes !!!! ni filles !!!!
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InsertEnFinAttribute : Attribute
    {
    }
}
