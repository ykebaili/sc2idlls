using sc2i.common;
using sc2i.data.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data
{
    /// <summary>
    /// Indique que la propriété décode un blob
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BlobDecoderAttribute : Attribute
    {
    }

    [AutoExec("Autoexec")]
    public class CFournisseurDependancesInBlob : IFournisseurDependancesObjetDonnee
    {
        //--------------------------------------------------------------
        public static void Autoexec()
        {
            CEntitiesManager.RegisterFournisseurDependances(typeof(CFournisseurDependancesInBlob));
        }

        //--------------------------------------------------------------
        public List<CReferenceObjetDependant> GetDependances(CEntitiesManager manager, CObjetDonnee objet)
        {
            objet.Refresh();
            List<CReferenceObjetDependant> lst = new List<CReferenceObjetDependant>();
            if (objet == null)
                return lst;
            Type tp = objet.GetType();
            foreach ( PropertyInfo info in tp.GetProperties())
            {
                if ( info.GetCustomAttribute<BlobDecoderAttribute>(true) != null)
                {
                    try
                    {
                        DynamicFieldAttribute df = info.GetCustomAttribute<DynamicFieldAttribute>();
                        string strNomProp = df == null?info.Name:df.NomConvivial;
                        MethodInfo methode = info.GetGetMethod();
                        List<CDbKey> lstKeys = new List<CDbKey>();
                        using (CDbKeyReaderTracker tracker = new CDbKeyReaderTracker())
                        {
                            if (methode != null)
                                methode.Invoke(objet, new object[0]); ;
                            lstKeys.AddRange(tracker.TrackedKeys);
                        }
                        foreach ( CDbKey key in lstKeys )
                        {
                            Type tpChild = manager.GetTypeForUniversalId(key.StringValue);
                            if (tpChild != null && !manager.ConfigurationRecherche.IsIgnore(tpChild ))
                            {
                                lst.Add(new CReferenceObjetDependant(strNomProp, tpChild, key));
                            }
                        }

                    }
                    catch { }
                }
            }
            return lst;                 
        }
        
    }

}
