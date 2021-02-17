using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CChampCustomServeur : CObjetServeurAvecBlob, IObjetServeur
	{

#if PDA
		///////////////////////////////////////////////////
		public CChampCustomServeur (  )
			:base (  )
		{
		}
#endif
		///////////////////////////////////////////////////
		public CChampCustomServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CChampCustom.c_nomTable;
		}

		/// ////////////////////////////////////////////////
		protected override bool UseCache
		{
			get
			{
				return true;
			}
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CChampCustom champ = (CChampCustom)objet;
			
				if (champ.Nom == "")
					result.EmpileErreur(I.T("The custom field name cannot be empty|108"));
				if (!CObjetDonneeAIdNumerique.IsUnique(champ, CChampCustom.c_champNom, champ.Nom))
					result.EmpileErreur(I.T("A custom field with this name already exist|109"));

				if ( champ.Role == null )
					result.EmpileErreur(I.T("The field usage cannot be empty|110"));

				//Vérifie que toutes les valeurs sont du type du champ et qu'elles sont uniques
				C2iTypeDonnee typeDonnee = champ.TypeDonneeChamp;
				Hashtable tableValeurs = new Hashtable();
				Hashtable tableDisplay = new Hashtable();
				foreach (CValeurChampCustom valeur in champ.ListeValeurs)
				{
                    object val = valeur.Value;
                    if (val == null)
                        val = DBNull.Value;
					if ( tableValeurs[val] != null )
						result.EmpileErreur(I.T("The value '@1' appear several times in the value list|111", val.ToString()));
					else
                        tableValeurs[val] = valeur;

					if ( tableDisplay[valeur.Display] != null )
						result.EmpileErreur(I.T("The displayed value '@1' appear several times in the displayed values list|112", valeur.Display));
					else
						tableDisplay[valeur.Display] = valeur;

					if ( !typeDonnee.IsDuBonType ( valeur.Value ) )
						result.EmpileErreur(I.T("The value '@1' is not a @2 type|113", valeur.Value.ToString(), typeDonnee.Libelle));
					
				}

				//Vérifie que la valeur par défaut fait bien partie des valeurs possibles et qu'elle est du bon type
				if ( champ.ValeurParDefaut != null && champ.ValeurParDefaut.ToString() != C2iTypeDonnee.c_ConstanteNull)
				{
					if ( !typeDonnee.IsDuBonType(champ.ValeurParDefaut) )
						result.EmpileErreur(I.T("The default value '@1' is not a @2 type|114", champ.ValeurParDefaut.ToString(), typeDonnee.Libelle));
					if ( tableValeurs[champ.ValeurParDefaut] == null && (tableValeurs.Count > 0) )
						result.EmpileErreur(I.T("The default value is not in the possible values list|115"));
				}

				if (champ.TypeDonneeChamp.TypeDonnee == TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
				{
					if (champ.LibellePourObjetParent.Trim() == "")
					{
						result.EmpileErreur(I.T("Indicate the label for the field seen by the parental object|116"));
					}
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CChampCustom);
		}
	}
}
