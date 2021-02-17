using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;
using System.Resources;
using System.Reflection;

namespace sc2i.common
{
	public class CInfoClasseDynamique
	{
		public Type m_classe;
		public  string m_strNom;
        /// ////////////////////////////////////////////////////
        public CInfoClasseDynamique(Type tp, string strNom)
		{
			m_classe = tp;
			m_strNom = strNom;
		}

		/// ////////////////////////////////////////////////////
		public Type Classe
		{
			get
			{
				return m_classe;
			}
		}

		/// ////////////////////////////////////////////////////
		public string Nom
		{
			get
			{
				return m_strNom;
			}
		}

		/// ////////////////////////////////////////////////////
		public override bool Equals(object obj)
        {
            CInfoClasseDynamique info = (obj as CInfoClasseDynamique);
			if ( info == null )
				return false;
			if ( info.Classe == null && Classe == null )
				return true;
			if ( info.Classe == null || Classe == null )
				return false;
			return Classe == info.Classe;
		}

		/// ////////////////////////////////////////////////////
		public override int GetHashCode()
		{
			if ( Classe != null )
				return Classe.GetHashCode();
			return 0;
		}


	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class DynamicClassAttribute : Attribute
	{
		private static Dictionary<Type, string> m_cacheNomsConviviaux = new Dictionary<Type, string>();
        private static Dictionary<Type, Image> m_cacheImages = new Dictionary<Type, Image>();
		
		public readonly string NomConvivial;


        /// <summary>
		/// 
		/// </summary>
		/// <param name="strNomConvivial"> Penser à mettre les noms au pluriel </param>
		public DynamicClassAttribute( string strNomConvivial )
		{
			NomConvivial = strNomConvivial;
		}

        /// ////////////////////////////////////////////////////////////////
        public static Image GetImage(Type tp)
        {
            if (tp == null)
                return null;
            Image img = null;
            if (m_cacheImages.TryGetValue(tp, out img))
                return img;
            object[] attribs = tp.GetCustomAttributes(typeof(DynamicIconAttribute), true);
            if (attribs != null && attribs.Length > 0)
            {
                DynamicIconAttribute att = attribs[0] as DynamicIconAttribute;
                if (att != null)
                {
                    img = att.GetImage();
                    if (img != null)
                        m_cacheImages[tp] = img;
                    return img;
                }
            }
            string[] strNoms = tp.ToString().Split('.');
            string strNomImage = "."+strNoms[strNoms.Length - 1].ToUpper()+".";
            string strNomClasse = strNoms[strNoms.Length - 1];
            ResourceManager rm = new ResourceManager("MyResource", tp.Assembly);
            try
            {
                //Cherche l'image du nom de la classe
                foreach (string strResource in tp.Assembly.GetManifestResourceNames())
                {
                    System.Console.WriteLine(strResource);
                    ManifestResourceInfo info = tp.Assembly.GetManifestResourceInfo(strResource);
                    bool bPrendre = strResource == strNomImage;
                    if (!bPrendre)
                    {
                        int nIndex = strResource.ToUpper().IndexOf(strNomImage);
                        if (nIndex >= 0)
                        {
                            string strSuite = strResource.Substring(nIndex + strNomImage.Length).ToUpper();
                            if ("JPG PNG BMP GIF".Contains(strSuite))
                                bPrendre = true;
                        }
                    }
                    if (bPrendre)
                    {
                        try
                        {
                            img = new Bitmap(tp.Assembly.GetManifestResourceStream(strResource));
                            if (img != null)
                            {
                                m_cacheImages[tp] = img;
                                return img;
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
            m_cacheImages[tp] = null;
            return img;

        }
		/// ////////////////////////////////////////////////////////////////
		public static string GetNomConvivial ( Type tp )
		{
			if ( tp == null )
				return "";
			string strNom = null;
			if (m_cacheNomsConviviaux.TryGetValue(tp, out strNom))
				return strNom;
			object[] attribs = tp.GetCustomAttributes ( typeof(DynamicClassAttribute), false );
            if (attribs.Length != 0)
            {
                DynamicClassAttribute att = attribs[0] as DynamicClassAttribute;
                if (att != null)
                {
                    m_cacheNomsConviviaux[tp] = att.NomConvivial;
                    return att.NomConvivial;
                }
            }
            string[] strNoms = tp.ToString().Split('.');
            m_cacheNomsConviviaux[tp] = strNoms[strNoms.Length - 1];
            return strNoms[strNoms.Length - 1];
		}

		/// ////////////////////////////////////////////////////////////////
		public static Type GetTypeFromNomConvivial ( string strNomConvivial )
		{
			strNomConvivial = strNomConvivial.ToUpper(CultureInfo.CurrentCulture);
			foreach ( CInfoClasseDynamique info in GetAllDynamicClass() )
				if ( info.Nom.ToUpper(CultureInfo.CurrentCulture) == strNomConvivial )
					return info.Classe;
			return null;
		}

		/// ////////////////////////////////////////////////////////////////
		private class CInfoClassComparer : IComparer
		{
			public int Compare( object o1, object o2 )
			{
                CInfoClasseDynamique info1 = (o1 as CInfoClasseDynamique);
                CInfoClasseDynamique info2 = (o2 as CInfoClasseDynamique);
				if ( (info1 == null) || (info2 == null) )
					return -1;
                return String.Compare(info1.Nom, info2.Nom,
                    StringComparison.CurrentCulture);
			}
		}

		/// ////////////////////////////////////////////////////////////////
		public static CInfoClasseDynamique[] GetAllDynamicClass ( params Type[] typesAttributsNecessaires )
		{
			ArrayList lstVals = new ArrayList();

			foreach ( System.Reflection.Assembly ass in CGestionnaireAssemblies.GetAssemblies() )
			{
				try
				{
					foreach ( Type tp in ass.GetTypes() )
					{
						try
						{
							object[] attribs = tp.GetCustomAttributes( typeof(DynamicClassAttribute), false);
							if ( attribs != null && attribs.Length == 1 )
							{
								DynamicClassAttribute dynamicClass = (DynamicClassAttribute)attribs[0];
								bool bToAdd = true;
								foreach ( Type tpAttrib in typesAttributsNecessaires )
								{
									attribs = tp.GetCustomAttributes ( tpAttrib, false );
									if ( attribs == null || attribs.Length == 0 )
										bToAdd = false;
								}
								if ( bToAdd )
									lstVals.Add ( new CInfoClasseDynamique ( tp, dynamicClass.NomConvivial));
							}
						}
						catch{}
					}
				}
				catch
				{
					System.Console.WriteLine(I.T("Type loading error|30108"));
				}
			}
			lstVals.Sort(new CInfoClassComparer());
			return (CInfoClasseDynamique[])lstVals.ToArray( typeof(CInfoClasseDynamique) );
		}

		/// ////////////////////////////////////////////////////////////////
		public static CInfoClasseDynamique[] GetAllDynamicClassHeritant(params Type[] typesHeritageNecessaires)
		{
			ArrayList lstVals = new ArrayList();

			foreach (System.Reflection.Assembly ass in CGestionnaireAssemblies.GetAssemblies())
			{
				try
				{
					foreach (Type tp in ass.GetTypes())
					{
						try
						{
							bool bOk = true;
							foreach ( Type tpHeritant in typesHeritageNecessaires )
								if (!tpHeritant.IsAssignableFrom(tp))
								{
									bOk = false;
									break;
								}
							if (bOk)
								lstVals.Add(new CInfoClasseDynamique (
                                    tp, 
                                    DynamicClassAttribute.GetNomConvivial (tp)));
						}
						catch { }
					}
				}
				catch
				{
                    System.Console.WriteLine(I.T("Type loading error|30108"));
				}
			}
			lstVals.Sort(new CInfoClassComparer());
			return (CInfoClasseDynamique[])lstVals.ToArray(typeof(CInfoClasseDynamique));
		}
	}
}
