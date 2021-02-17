using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{

	public interface IDocElementALiensRessource
	{
		string ID { get;set;}
		string Description { get;set;}
		List<DocLienRessource> Ressources { get;set;}
		int Position { get;set;}
	}



}
