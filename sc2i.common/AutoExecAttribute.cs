using System;

namespace sc2i.common
{
	/// <summary>
	/// Indique un nom de fonction statique à executer lors de l'initialisation de cette classe
	/// A utiliser conjointement avec ce CAutoexecuteurClasses
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public sealed class AutoExecAttribute : Attribute
	{
        public const string BackGroundService = "BACKGROUND_SERVICE";


		public readonly string FonctionAutoexec;
        public readonly string Attribut;
		public AutoExecAttribute( string strFonctionStatiqueToRun )
		{
			FonctionAutoexec = strFonctionStatiqueToRun;
            Attribut = "";
		}

        public AutoExecAttribute(string strFonctionStatiqueToRun, string strAttribute)
        {
            FonctionAutoexec = strFonctionStatiqueToRun;
            Attribut = strAttribute;
        }

        public bool HasAttribute(string strAttibute)
        {
            return Attribut.ToUpper() == strAttibute.ToUpper();
        }
	}
}
