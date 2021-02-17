using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	/// <summary>
	/// Attribut indiquant l'adresse relative au fichier projet du fichier de documentation
	/// </summary>
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public class DocInfoPath : Attribute
	{
		#region :: Propriétés ::
		private string _path;
		#endregion
		#region ++ Constructeur ++
		public DocInfoPath(string path)
		{
			_path = path;
		}
		#endregion
		#region >> Assesseurs <<
		public string Path
		{
			get
			{
				return _path;
			}
			set { _path = value; }
		}
		#endregion
	}




}
