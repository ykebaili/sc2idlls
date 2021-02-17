using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection.Emit;
using sc2i.common;

namespace sc2i.expression.Listes
{
    /// <summary>
    /// Object hashtable permet de stocker des couples "Clé/valeur".
    /// </summary>
    [DynamicClass("Object hashtable")]
    [VariableAutoAllouee]
    public class CHashtable : Hashtable, IObjetAMethodesDynamiquesSurListe
    {
        [DynamicMethod("Set a value for specified key")]
        public void SetValue(object key, object value)
        {
            if (key != null)
                this[key] = value;
        }

        [DynamicMethod("Get associated key value")]
        public object GetValue(object key)
        {
            return this[key];
        }

        [DynamicMethod("Clear hastable")]
        public void ClearTable()
        {
            Clear();
        }

        [DynamicMethod("Return a list of all keys")]
        public CObjectList GetKeys()
        {
            CObjectList lst = new CObjectList();
            lst.AddRange(Keys);
            return lst;
        }

        [DynamicMethod("Return a list of all values")]
        public CObjectList GetValues()
        {
            CObjectList lst = new CObjectList();
            lst.AddRange(Values);
            return lst;
        }
    }
}
