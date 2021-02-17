using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{

	public static class CUtilStockageValeurChamp
	{
		public static void StockValeur(
			DataRow row,
			int idSession,
			string strChampType,
			string strChampBlob,
			string strChampString,
			object val)
		{
			if (val is IDifferencesBlob)
			{
				MemoryStream stream = new MemoryStream();
				BinaryWriter writer = new BinaryWriter(stream);
				CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
				I2iSerializable serTmp = (I2iSerializable)val;
				CResultAErreur result = serializer.TraiteObject(ref serTmp);
				if (!result)
					throw new Exception(I.T("Can not serialize blob differences|192"));
				row[strChampType] = typeof(IDifferencesBlob).ToString();
				CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(idSession, row, strChampBlob);
				donnee.Donnees = stream.GetBuffer();
				row[strChampBlob] = donnee;

                writer.Close();
                stream.Close();
			}
			else if (val is byte[])
			{
				CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(idSession, row, strChampBlob);
				donnee.Donnees = (byte[])val;
				row[strChampBlob] = donnee;
				row[strChampType] = typeof(byte[]).ToString();
			}
			else if (val != null && val.GetType() == typeof(CDonneeBinaireInRow))
			{
				row[strChampBlob] = val;
				row[strChampType] = typeof(byte[]).ToString();
			}
            else if (val is CObjetDonneeAIdNumerique)
            {
                row[strChampString] = ((CObjetDonneeAIdNumerique)val).Id;
                row[strChampString] = val.GetType().ToString();
            }
            else
            {
                row[strChampString] = CUtilTexte.ToUniversalString(val);
                if (val != null)
                    row[strChampType] = val.GetType().ToString();
                //else
                //    row[strChampType] = "";
            }
		}

		public static CDonneeBinaireInRow GetBlob(DataRow row, string strChampBlob, CContexteDonnee ctx)
		{
			if (row[strChampBlob] == DBNull.Value)
			{
				CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ctx.IdSession, row, strChampBlob);
				CContexteDonnee.ChangeRowSansDetectionModification(row, strChampBlob, donnee);
			}
			object obj = row[strChampBlob];
			return (CDonneeBinaireInRow)obj;
		}
		public static object GetValeur(
			DataRow row,
			string strChampString,
			string strChampBlob,
			string strChampType,
			CContexteDonnee ctx)
		{
			Type t = CActivatorSurChaine.GetType((string)row[strChampType]);
			if (t == typeof(IDifferencesBlob))
			{
				CDonneeBinaireInRow blob = GetBlob(row, strChampBlob, ctx);
				MemoryStream stream = new MemoryStream(blob.Donnees);
                BinaryReader reader = new BinaryReader(stream);
				CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
				I2iSerializable objet = null;
				CResultAErreur result = serializer.TraiteObject(ref objet);

                reader.Close();
                stream.Close();
                
                if (result)
				{
					return objet;
				}
				System.Console.WriteLine(I.T("Error while deserializing blob diffs|30000"));
                return null;
			}
			else if (t == typeof(byte[]))
			{
				CDonneeBinaireInRow blob = GetBlob(row, strChampBlob, ctx);
				if (blob != null)
					return blob.Donnees;
				else
					return null;
			}
            else if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(t))
            {
                int nId = Int32.Parse((string)row[strChampString]);
                CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(t, new object[] { ctx });
                if (obj.ReadIfExists(nId))
                    return obj;
                return null;
            }
            else if (t != null && row[strChampString] != DBNull.Value)
            {

                return CUtilTexte.FromUniversalString((string)row[strChampString], t);
            }
			return null;
		}

	}

}
