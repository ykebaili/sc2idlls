using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using sc2i.common;

namespace sc2i.expression.Listes
{
    [DynamicClass("Object list")]
    [VariableAutoAllouee]
    public class CObjectList : ArrayList, IObjetAMethodesDynamiquesSurListe
    {
        public CObjectList()
        {
        }

        [DynamicMethod("Add an object to the list")]
        public void AddObject(object obj)
        {
            Add(obj);
        }

        [DynamicMethod("Add an object if it's not yet in the list")]
        public void AddObjectIfAbsent(object obj)
        {
            if (!Contains(obj))
                Add(obj);
        }

        [DynamicMethod("returns true if the list contains the object")]
        public bool ContainsObject (object obj )
        {
            return base.Contains(obj);
        }

        [DynamicMethod("Remove an object from the list")]
        public void RemoveObject(object obj)
        {
            Remove(obj);
        }

        [DynamicMethod("Add a range of objects to the list")]
        public void AddObjectRange(IEnumerable<object> source)
        {
            if ( source != null )
                foreach (object obj in source)
                    Add(obj);
        }

        [DynamicMethod("Clear list")]
        public void ClearList()
        {
            Clear();
        }
    }
}
