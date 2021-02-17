using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de NonCloneableAttribute.
	/// </summary>
	[AttributeUsage ( AttributeTargets.Property )]
    public sealed class NonCloneableAttribute : Attribute
	{
		public NonCloneableAttribute()
		{
			
		}
	}


    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NonClonableObjectAttribute : Attribute
    {
        public NonClonableObjectAttribute()
        {
        }
    }

}
