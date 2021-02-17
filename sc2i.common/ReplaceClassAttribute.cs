using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace sc2i.common
{
	/// <summary>
	/// UTilis� pour indiquer qu'une classe remplace une autre classe qui a �t�
	/// s�par�e. Lorsque le C2iSerializer chercher � allouer la classe remplac�e,
	/// et qu'il �choue, il alloue la classe rempla�ante
	/// </summary>
	[AutoExec("Autoexec")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class ReplaceClassAttribute : Attribute
	{
		public readonly string URITypeRemplace;
		public ReplaceClassAttribute(string strURITypeRemplace)
		{
			URITypeRemplace = strURITypeRemplace;
		}

		public static void Autoexec()
		{
			foreach (Assembly ass in CGestionnaireAssemblies.GetAssemblies())
			{
				foreach (Type tp in ass.GetTypes())
				{
					object[] attrs = tp.GetCustomAttributes(typeof(ReplaceClassAttribute), false);
					if (attrs != null)
					{
						foreach (ReplaceClassAttribute attr in attrs)
						{
							CActivatorSurChaine.RegisterTypeObsolete(attr.URITypeRemplace, tp);
						}
					}
				}
			}
		}

	}
}
