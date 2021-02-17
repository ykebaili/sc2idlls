using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{

	public interface IDocElementAMenuItems
	{
		int Position { get; set; }
		List<DocMenuItem> Enfants { get;set;}
		string Nom { get; set;}
		string ID { get;set;}
		string Description { get;set;}

		bool SupprimerUnFils(IDocElementAMenuItems fils);
		IDocElementAMenuItems GetFatherOf(IDocElementAMenuItems fils);

		IDocElementAMenuItems Clone();

		void MonterFils(DocMenuItem fils);
		bool MonterFilsOuPetitFils(DocMenuItem fils);

		void OrganiserFils();

		void AjouterFils(DocMenuItem fils);
		void AjouterFils(DocMenuItem fils, int pos);

	}



}
