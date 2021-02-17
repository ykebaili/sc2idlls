using System;

using sc2i.common;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// Lorsqu'une propriété a cet attribut, cela signifie qu'il correspond à 
	/// une relation parente, mais gerée par le framework SC2I, c'est à dire,
	/// qui utilise le type et l'identifiant d'un élément pour le retrouver.
	/// Cet attribut est utile pour créer des relations filles sur toutes les 
	/// tables de la base.
	/// Cet attribut doit être applique à la propriété qui retourne l'élément lié
	/// </summary>
	/// <remarks>
	/// Cet attribut est abstrait et doit donc être hérité pour chaque relation<BR/>
	/// <P>
	/// Une relation TypeId est toujours une relation fille qui peut potentiellement
	/// s'appliquer à toutes les tables du système.
	/// LA fonction virtuelle IsAppliqueToType retourne vrai si la relation s'applique
	/// à un type particulier et false sinon.
	/// </P>
	/// <P>
	/// Si la relation est une composition, les elements fils (de la table possèdant l'attribut)
	/// seront supprimés lors d'une suppression de l'élément parent.
	/// </P>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public abstract class RelationTypeIdAttribute : Attribute, I2iSerializable
	{
		public RelationTypeIdAttribute()
		{
		}

		private string m_strIdRelation = null;
		public string IdRelation
		{
			get
			{
				if ( m_strIdRelation == null )
					m_strIdRelation = "#REL_DYNC_"+MyIdRelation;
				return m_strIdRelation;
			}
		}

        public static string GetNomColDepLue(string strTableFille)
        {
            return "RELDYN" + strTableFille;
        }

		public abstract string TableFille{get;}
		public abstract string ChampType{get;}
		public abstract string ChampId{get;}
		public abstract bool Composition{get;}
		public abstract bool CanDeleteToujours{get;}
		protected abstract string MyIdRelation{get;}
		public abstract string NomConvivialPourParent{get;}

		/// <summary>
		/// Si false, aucune contrainte d'intégrité référentielle n'est appliqué sur l'élément
		/// </summary>
		public virtual bool AppliquerContrainteIntegrite
		{
			get
			{
				return true;
			}
		}
		
		//En élément de prorité n peut être lié à des éléments 
		//de priorité inférieure
		public abstract int Priorite{get;}

		//Optim : Mise en cache des types appliqués
		private Dictionary<Type, bool> m_tableTypesToApplication = new Dictionary<Type, bool>();
		public bool IsAppliqueToType(Type tp)
		{
			bool bReponse = false;
			if (m_tableTypesToApplication.TryGetValue(tp, out bReponse))
				return bReponse;

			object[] attributes = tp.GetCustomAttributes(typeof(NoRelationTypeIdAttribute), true);
			if (attributes.Length > 0)
			{
				m_tableTypesToApplication[tp] = false;
				return false;
			}
			bReponse = MyIsAppliqueToType(tp);
			m_tableTypesToApplication[tp] = bReponse;
			return bReponse;
		}

		/// <summary>
		/// Retourne vrai si le type demandé se voit appliqué cette relation dynamique
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		/// 
		protected abstract bool MyIsAppliqueToType (Type tp );

		private int GetNumVersion()
		{
			return 0;
		}

		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			return result;
		}


        public string GetNomColDepLue()
        {
            return GetNomColDepLue(TableFille);
        }
    }
	[AttributeUsage(AttributeTargets.Class)]
	public class NoRelationTypeIdAttribute : Attribute
	{
	}

    /// <summary>
    /// Indique que les données de ces tables doivent être sauvegardées après les relation typeId
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InsertAfterRelationTypeIdAttribute : Attribute
    {
    }



}
