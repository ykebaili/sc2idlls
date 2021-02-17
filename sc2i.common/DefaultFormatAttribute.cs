using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de DefaultFormatAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DefaultFormatAttribute : Attribute
	{
		public readonly string Format;

		public DefaultFormatAttribute(string strFormat)
		{
			Format = strFormat;
		}
	}
}
