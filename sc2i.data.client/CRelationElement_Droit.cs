using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.client;

#if !PDA_DATA

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CRelationElement_Droit.
	/// </summary>
	public abstract class CRelationElement_Droit : CObjetDonneeAIdNumeriqueAuto
	{
		/// //////////////////////////////////////////////////
		public CRelationElement_Droit(CContexteDonnee ctx)
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CRelationElement_Droit(DataRow row )
			:base ( row )
		{
		}


		/// //////////////////////////////////////////////////
		protected abstract int NumVersionData {get;}
		/// //////////////////////////////////////////////////
		public abstract string Data{get;set;}

		/// //////////////////////////////////////////////////
		public abstract IElementDefinissantDesDroits ElementDefinisseur{get;set;}


		/// //////////////////////////////////////////////////
		[TableFieldProperty(CDroitUtilisateur.c_champCode, 256)]
		public string CodeDroit
		{
			get
			{
				return (string)Row[CDroitUtilisateur.c_champCode];
			}
			set
			{
				Row[CDroitUtilisateur.c_champCode] = value;
			}
		}
		
		/// //////////////////////////////////////////////////
		public CDroitUtilisateur Droit
		{
			get
			{
				return (CDroitUtilisateur)GetParent ( CDroitUtilisateur.c_champCode, typeof(CDroitUtilisateur));
			}
			set
			{
				SetParent ( CDroitUtilisateur.c_champCode, value );
			}
		}

		/// //////////////////////////////////////////////////
		public abstract int OptionsInt{get;set;}

		/// //////////////////////////////////////////////////
		public OptionsDroit Options
		{
			get
			{
				return (OptionsDroit)OptionsInt;
			}
			set
			{
				OptionsInt = (int)value;
			}
		}
		

		/// //////////////////////////////////////////////////
		public bool HasOption ( OptionsDroit nOption )
		{
			return (nOption & Options) == nOption;
		}

		/// //////////////////////////////////////////////////
		public void AddOption ( OptionsDroit option )
		{
			if ( !HasOption ( option ) )
				Options |= option;
				
		}

		/// //////////////////////////////////////////////////
		public void RemoveOption ( OptionsDroit option )
		{
			if ( HasOption ( option ) )
				Options&= ~option;
		}


		/// //////////////////////////////////////////////////
		///Liste des élements particuliers sélectionnés pour ce droit
		public CObjetDonneeAIdNumerique[] ListeObjetsOption
		{
			get
			{
				Type type = CActivatorSurChaine.GetType( Droit.TypeAssocieURI );
				if ( type == null )
					return new CObjetDonneeAIdNumerique[0];
				string strData = Data;
				if ( strData.Length == 0 )
					return new CObjetDonneeAIdNumerique[0];
				string[] strChamps = strData.Split('~');
				try
				{
					int nVersion = Int32.Parse(strChamps[0]);
					if ( nVersion > NumVersionData )
						return new CObjetDonneeAIdNumerique[0];//ne sait pas lire;
					//Lit les ids des éléments
					ArrayList lstObjs = new ArrayList();
					for ( int n = 1; n < strChamps.Length; n++ )
					{
						if ( strChamps[n] != "" )
						{
							int nId = Int32.Parse(strChamps[n]);
							CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(type, new object[]{ContexteDonnee});
							if ( obj.ReadIfExists(nId) )
								lstObjs.Add ( obj );
						}
					}
					return (CObjetDonneeAIdNumerique[])lstObjs.ToArray(typeof(CObjetDonneeAIdNumerique));
				}
				catch
				{
				}
				return new CObjetDonneeAIdNumerique[0];
			}
			set
			{
				if ( value == null )
					Data = "";
				string strListe = NumVersionData+"~";
				foreach ( CObjetDonneeAIdNumerique obj in value )
				{
					strListe+= obj.Id+"~";
				}
				Data = strListe;
			}
		}

		
	}
}
#endif