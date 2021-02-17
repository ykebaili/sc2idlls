using System;

namespace sc2i.process
{
	/// <summary>
	/// Classe bidon retournée par le result.data du ExecuteAction d'un action
	/// Lorsque execute Action retourne un objet de cette classe,
	/// le process est mis en pause !
	/// </summary>
	public class CMetteurDeProcessEnPause
	{
		public CMetteurDeProcessEnPause()
		{
		}
	}
}
