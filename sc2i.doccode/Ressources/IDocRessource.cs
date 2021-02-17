using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{

	public interface IDocRessource
	{
		string ID { get;set;}
		string Nom { get; set;}
		string Description { get;set;}
		string Path { get;set;}
		EDocRessourceType TypeRessource { get;set;}
	}



}
