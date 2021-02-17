using System;
using System.Collections.Generic;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Les Form ayants cet attrinut seront disponibles dynamiquement dans Timos sur les actions
    /// "Afficher un Formulaire Standard" par exemple.
    /// ATTENTION: Quand un formulaire a l'attribut DynamicForm il doit implémenter une Fonction statique: 
    /// public static CInfoParametreDynamicForm[] GetInfosParametres(){}
    /// voir exemple sur CFormVisualisationRapport
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class DynamicFormAttribute : Attribute
	{
		public readonly string Libelle;
        // Le nom de la méthode static qui retrourne les infos sur les paramètres du formulaire
        public readonly string NomMethodeGetInfosParametres;


        public DynamicFormAttribute(string strLibelle)
        {
            Libelle = strLibelle;
            NomMethodeGetInfosParametres = "";
        }

        public DynamicFormAttribute(string strLibelle, string strNomMethodeGetInfosParametres)
		{
			Libelle = strLibelle;
            NomMethodeGetInfosParametres = strNomMethodeGetInfosParametres;
		}
	}

    //***************************************************************
    /// <summary>
    /// 
    /// </summary>
    public class CInfoParametreDynamicForm
    {
        private string m_strNomParametre;
        private Type m_typeParametre;
        private string m_strDescription;

        public CInfoParametreDynamicForm(string strNom, Type tp, string strDesc)
        {
            Nom = strNom;
            TypeParametre = tp;
            Description = strDesc;
        }

        //-----------------------------------------------------
        public string Nom
        {
            get { return m_strNomParametre; }
            set { m_strNomParametre = value; }
        }

        //-----------------------------------------------------
        public Type TypeParametre
        {
            get { return m_typeParametre; }
            set { m_typeParametre = value; }
        }

        //-----------------------------------------------------
        public string Description
        {
            get { return m_strDescription; }
            set { m_strDescription = value; }
        }

    }


    //*********************************************************************************
    /// <summary>
    /// 
    /// </summary>
    public interface IFormDynamiqueAParametres
    {
        CResultAErreur SetParametres(Dictionary<string, object> dicParametres);
    }
}
