using System;
using System.Collections.Generic;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de IEnumeratorBiSens.
	/// </summary>
	public interface IEnumeratorBiSens : System.Collections.IEnumerator
	{
		bool MovePrev();
		int CurrentIndex { get; set; }
	}

    public interface IEnumeratorBiSensGenerique<TypeObjets> : IEnumerator<TypeObjets>
    {
        bool MovePrev();
        int CurrentIndex { get;set;}
    }


	public interface IEnuerableBiSens : System.Collections.IEnumerable
	{
		IEnumeratorBiSens GetEnumeratorBiSens();
	}

    public interface IEnumerableBiSensGenerique<TypeObjets> : IEnumerable<TypeObjets>
    {
        IEnumerableBiSensGenerique<TypeObjets> GetEnumeratorBiSens();
    }
}
