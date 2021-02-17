using System;

using sc2i.common;

using sc2i.data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CDroitDeBaseSC2IServeur.
	/// </summary>
	[AutoExec("RegisterDroits")]
	public class CDroitDeBaseSC2IServeur
	{
		public CDroitDeBaseSC2IServeur()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

		public static void RegisterDroits()
		{
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			CGestionnaireDroitsUtilisateurs.RegisterDroit
				(
				CDroitDeBaseSC2I.c_droitAdministration,
				I.T("Administration|60"),
				0,
				"",
				I.T("Gives all administration rights of the application|164" ));
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			CGestionnaireDroitsUtilisateurs.RegisterDroit
				(
				CDroitDeBaseSC2I.c_droitAdministrationSysteme,
				I.T("System|62"),
				0,
				CDroitDeBaseSC2I.c_droitAdministration,
				I.T("Systems elements administration|165" ));
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			CGestionnaireDroitsUtilisateurs.RegisterDroit
				(
				CDroitDeBaseSC2I.c_droitInterface,
				I.T("Interface|63"),
				0,
				CDroitDeBaseSC2I.c_droitAdministration,
				I.T("User right concerning application interface|166"));
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            CGestionnaireDroitsUtilisateurs.RegisterDroit
                (
                CDroitDeBaseSC2I.c_droitImport,
                I.T("Import data|20008"),
                0,
                CDroitDeBaseSC2I.c_droitAdministrationSysteme,
                I.T("Allows the user to directly import data|20009"));
		}

	}
}
