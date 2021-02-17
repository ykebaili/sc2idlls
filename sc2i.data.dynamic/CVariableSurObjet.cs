using System;
using System.Collections;

using sc2i.data;
using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// D�finit une variable qui peut �tre associ�e � n'importe quel objet.
	/// </summary>
	/// <remarks>
	/// Les variables sur objets permettent d'associer une valeur � n'importe
	/// quelle entit� m�tier.<BR></BR>
	/// <P>
	/// La valeur associ�e est toujours une valeur de type texte, il n'existe aucun
	/// contr�le sur la valeur toutes les valeurs sont accept�es.<BR/>
	/// La longueur maximale d'une valeur de variable est 4000 caract�res
	/// </P>
	/// <P>
	/// La valeur par d�faut d'une variable est toujours la chaine vide.<BR></BR>
	/// Les variables sur objet sont essentiellement faites pour g�rer des �tats sur
	/// les objets, l'acc�s aux valeurs de variables peut �tre couteux
	/// en ressources machines.
	/// </P>
	/// <P>
	/// Contrairement aux champs personnalis�s, les variables sont des entit�s 'sans filet'.
	/// Il est possible de supprimer une variable alors qu'il existe des valeurs
	/// pour celle ci dans la base. Il n'existe pas de lien d'int�grit� r�f�rentielle
	/// entre les valeurs de variable et les variables. <BR></BR>
	/// Si le nom d'une variable est chang�e, il ne se passe rien pour les valeurs,
	/// tout se passe comme si on avait supprim� l'ancienne variable et cr�� une nouvelle.
	/// </P>
	/// </remarks>
	[ObjetServeurURI("CVariableSurObjetServeur")]
	[Table(CVariableSurObjet.c_nomTable,CVariableSurObjet.c_champId,true)]
		[DynamicClass("Object variable")]
	public class CVariableSurObjet : CObjetDonneeAIdNumeriqueAuto
	{
		#region D�claration des constantes
		public const string c_nomTable = "VARIABLES";
		public const string c_champId = "VARI_ID";
		public const string c_champNom = "VARI_NAME";
		public const string c_champDescription = "VARI_DESCRIPTION";
		#endregion

#if PDA
		/// ///////////////////////////////////////////////////////
		public CVariableSurObjet(  )
			:base()
		{
		}
#endif
		/// ///////////////////////////////////////////////////////
		public CVariableSurObjet( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// ///////////////////////////////////////////////////////
		public CVariableSurObjet ( System.Data.DataRow row )
			:base(row)
		{
		}


		/// ///////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// ///////////////////////////////////////////////////////
		public override string GetChampId()
		{
			return c_champId;
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[] {c_champNom};
		}


		/// ///////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The variable '@1'|241", Nom);
			}
		}

		////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return c_nomTable;
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Nom unique de la variable
		/// </summary>
		[TableFieldProperty(c_champNom, 128),
		DynamicField("Name")]
		public string Nom
		{
			get
			{
				return (string)Row[c_champNom];
			}
			set
			{
				Row[c_champNom] = value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Description de la variable
		/// </summary>
		[TableFieldProperty(c_champDescription, 1024),
		DynamicField("Description")]
		public string Description
		{
			get
			{
				return (string)Row[c_champDescription];
			}
			set
			{
				Row[c_champDescription] = value;
			}
		}
	}
}
