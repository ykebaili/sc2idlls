using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace sc2i.common
{
    /// <summary>
    /// Pour tous les éléments clonables par serializer, qui doivent effectuer un traitement après clonage
    /// </summary>
    public interface I2iCloneableAvecTraitementApresClonage
    {
        void TraiteApresClonage(I2iSerializable source);
    }
	/// <summary>
	/// Clone tout objet implémentant I2iSerializable
	/// </summary>
	public sealed class CCloner2iSerializable
	{
		private CCloner2iSerializable()
		{
			
		}

		
		public static void CopieTo ( I2iSerializable source, I2iSerializable cible )
		{
			CopieTo(source, cible, null);
		}

		public static void CopieTo(I2iSerializable source, I2iSerializable cible,
			Dictionary<Type, object> tableObjetsPourSerializer)
		{
			try
			{
				if ( source == null )
					return;
				if ( cible == null )
					return;
				MemoryStream stream = new MemoryStream();
				BinaryWriter writer = new BinaryWriter ( stream );
				C2iSerializer serializer = new CSerializerSaveBinaire(writer);
				CResultAErreur result = source.Serialize(serializer);
				if ( result )
				{
					stream.Seek(0, SeekOrigin.Begin );
					BinaryReader reader = new BinaryReader ( stream );
					serializer = new CSerializerReadBinaire(reader);
					if (tableObjetsPourSerializer != null)
					{
						foreach (KeyValuePair<Type, object> keyVal in tableObjetsPourSerializer)
							serializer.AttacheObjet(keyVal.Key, keyVal.Value);
					}
					result = cible.Serialize ( serializer );
					reader.Close();
				}
				writer.Close();
                I2iCloneableAvecTraitementApresClonage clone = cible as I2iCloneableAvecTraitementApresClonage;
                if (clone != null)
                    clone.TraiteApresClonage(source);
			}
			catch ( Exception e )
			{
				string strTmp = e.ToString();
				Console.WriteLine(I.T("Serializable copy error @1|30043 ",strTmp));
			}
		}

        public static T CloneGeneric<T>(T objetSource)
            where T : I2iSerializable
        {
            return (T)Clone(objetSource);
        }

		public static I2iSerializable Clone ( I2iSerializable objetSource )
		{
			return Clone ( objetSource, null);
		}

		public static I2iSerializable Clone ( I2iSerializable objetSource,  Dictionary<Type, object> tableObjetsPourSerializer )
		{
            return Clone(objetSource, tableObjetsPourSerializer, null);
		}

        public static I2iSerializable Clone(I2iSerializable objetSource, Dictionary<Type, object> tableObjetsPourSerializer, object[] parametresConstructeur)
        {
            if (objetSource == null)
                return null;
            I2iSerializable objetClone;
            if ( parametresConstructeur != null )
             objetClone = (I2iSerializable)Activator.CreateInstance(objetSource.GetType(), parametresConstructeur);
            else
                objetClone = (I2iSerializable)Activator.CreateInstance(objetSource.GetType(), new object[0]);
            CopieTo(objetSource, objetClone, tableObjetsPourSerializer);
            return objetClone;
        }
	}
}
