using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using System.IO;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Contient un param�trage de filtre � utiliser dans les zones de recherche rapide.
	/// </summary>
    /// <remarks>
    /// Par d�faut, le syst�me g�n�re automatiquement le filtre qui est utilis� dans les zones de recherche
    /// rapide. L'entit� "Easy search filter setup" correspond � un param�trage sp�cifique de l'application pour utiliser
    /// un filtre particulier en lieu et place du filtre syst�me d�finit en standard par l'application.
    /// <p>Une zone de recherche rapide est toujours constitu�e d'une zone de texte. Lors de la cr�ation d'un filtre
    /// pour recherche rapide, la valeur tap�e par l'utilisateur dans cette zone correspond toujours au param�tre @1 
    /// du filtre parametr�.</p>
    /// <p>Le param�trage d'un filtre de recherche rapide n�c�ssite une bonne connaissance du param�trage
    /// et de la structure de l'application, faut de quoi, certaines fen�tre pourraient devenir inutilisables</p>
    /// </remarks>
	[Table(CPreferenceFiltreRapide.c_nomTable, CPreferenceFiltreRapide.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CPreferenceFiltreRapideServeur")]
	[DynamicClass("Easy search filter setup")]
    [AutoExec("Autoexec")]
	public class CPreferenceFiltreRapide : CObjetDonneeAIdNumeriqueAuto, IObjetALectureTableComplete
	{
		public const string c_nomTable = "EASY_SEARCH";
		public const string c_champId = "EASFI_ID";
        public const string c_champFiltre = "EASFI_FILTER";
		public const string c_champTypeObjets = "EASFI_OBJECT_TYPE";

		public const string c_champCacheFiltre = "CALCFLD_FORMULA_CACHE";

#if PDA
		/// ///////////////////////////////////////////////////////
		public CPreferenceFiltreRapide(  )
			:base()
		{
		}
#endif
		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CPreferenceFiltreRapide( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CPreferenceFiltreRapide ( DataRow row )
			:base(row)
		{
		}

        /// ///////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CFournisseurFiltreRapide.SetFiltreDelegate(new GetFiltreRapideDelegate(GetFiltreRapide));
        }


		/// //////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
                return I.T("Easy search filter setup for @1|20037",
                    TypeObjets == null ? "?" : DynamicClassAttribute.GetNomConvivial(TypeObjets));
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}


		/// //////////////////////////////////////////////////
		[TableFieldProperty(CPreferenceFiltreRapide.c_champFiltre, NullAutorise=true)]
		public CDonneeBinaireInRow DataFiltre
		{
            get
            {
                if (Row[c_champFiltre] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champFiltre);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champFiltre, donnee);
                }
                return ((CDonneeBinaireInRow)Row[c_champFiltre]).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champFiltre] = value;
            }
		}

        /// //////////////////////////////////////////////////
        [TableFieldProperty(CPreferenceFiltreRapide.c_champCacheFiltre, IsInDb = false)]
        [BlobDecoder]
        public CFiltreData FiltrePrefere
        {
            get
            {
                CFiltreData filtre = Row[c_champCacheFiltre] as CFiltreData;
                if (filtre != null)
                    return filtre;
                if (DataFiltre.Donnees != null)
                {
                    MemoryStream stream = new MemoryStream(DataFiltre.Donnees);
                    BinaryReader reader = new BinaryReader(stream);
                    CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                    CResultAErreur result = serializer.TraiteObject<CFiltreData>(ref filtre);
                    if (!result)
                        filtre = null;

                    reader.Close();
                    stream.Close();
                }
                return filtre;
            }
            set
            {
                if (value == null)
                {
                    CDonneeBinaireInRow data = DataFiltre;
                    data.Donnees = null;
                    DataFiltre = data;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    CFiltreData filtre = value;
                    CResultAErreur result = serializer.TraiteObject<CFiltreData>(ref filtre);
                    if (result)
                    {
                        CDonneeBinaireInRow data = DataFiltre;
                        data.Donnees = stream.GetBuffer();
                        DataFiltre = data;
                    }
                    Row[c_champCacheFiltre] = DBNull.Value;

                    writer.Close();
                    stream.Close();
                }
            }
        }
		

		/// //////////////////////////////////////////////////
		///<summary>
        ///Type des objets dont le filtre de recherche rapide est remplac� par celui-ci
        /// </summary>
        [DynamicField("Object type")]
		public string TypeObjetsConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial(TypeObjets);
			}
		}

		/// //////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeObjets, 1024)]
		public string TypeObjetsString
		{
			get
			{
				return (string)Row[c_champTypeObjets];
			}
			set
			{
				Row[c_champTypeObjets] = value;
			}
		}

        /// //////////////////////////////////////////////////
        public override void AfterRead()
        {
            base.AfterRead();
            CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheFiltre, DBNull.Value);
        }

		/// //////////////////////////////////////////////////
		public Type TypeObjets
		{
			get
			{
				return CActivatorSurChaine.GetType(TypeObjetsString);
			}
			set
			{
				if(  value == null )
					TypeObjetsString = "";
				else
					TypeObjetsString = value.ToString();
			}
		}

        //-----------------------------------------------------------------------
        public static CPreferenceFiltreRapide GetPreferenceForType(Type typeElements)
        {
            CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
            CPreferenceFiltreRapide prefFiltre = new CPreferenceFiltreRapide(contexte);
            if (prefFiltre.ReadIfExists(new CFiltreData(
                CPreferenceFiltreRapide.c_champTypeObjets + "=@1",
                typeElements.ToString())))
            {
                return prefFiltre;
            }
            return null;
        }

        //-----------------------------------------------------------------------
        public static void GetFiltreRapide(Type typeElements, ref CFiltreData filtreRapide)
        {
            if (filtreRapide != null)
                return;
            CPreferenceFiltreRapide prefFiltre = GetPreferenceForType(typeElements);
            if (prefFiltre != null)
            {
                CFiltreData filtre = prefFiltre.FiltrePrefere;
                if (filtre != null)
                {
                    filtreRapide = filtre;
                    return;
                }

            }

        }

		
	}
		
}
