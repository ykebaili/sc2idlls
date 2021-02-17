using System;

namespace sc2i.common
{
	/// <summary>
	/// Description r�sum�e de NonCloneableAttribute.
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
