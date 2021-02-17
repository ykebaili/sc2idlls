using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using sc2i.common.unites;
using System.Drawing;

namespace sc2i.common
{

	/// <summary>
	/// Description résumée de I2iSerializable.
	/// </summary>
	public interface I2iSerializable
	{
		CResultAErreur Serialize ( C2iSerializer serializer );
	}

	/// //////////////////////////////////////
	public enum ModeSerialisation
	{
		Lecture = 0,
		Ecriture
	};
	public abstract class C2iSerializer
	{

        //Liste des éléments qui trackent des dbKey
        internal static List<CDbKeyReaderTracker> m_listeDbKeyTrackers = new List<CDbKeyReaderTracker>();

		public enum EnumTypeSimple
		{
			Inconnu = -1,
			Null=0,
			String ,
			Int,
			Double,
			Bool,
			Byte,
			DateTime,
			Type,
			Long,
			Decimal,
            Serializable
		};

        private bool m_bIsForClone = false;

        /// <summary>
        /// Retourne true si le serializer a été alloué pour faire un cloneage d'objet
        /// </summary>
        public bool IsForClone
        {
            get
            {
                if (Mode == ModeSerialisation.Ecriture)
                    return false;
                return m_bIsForClone;
            }
            set
            {
                m_bIsForClone = value;
            }
        }

		public abstract ModeSerialisation Mode{get;}

		public abstract void TraiteString ( ref string strChaine );
		public abstract void TraiteInt ( ref int nVal );
		public abstract void TraiteDouble ( ref double fVal );
		public abstract void TraiteBool ( ref bool bVal );
		public abstract void TraiteByte ( ref Byte btVal );
		public abstract void TraiteByteArray ( ref Byte[] bts );
		public abstract void TraiteLong ( ref long nVal );
		public abstract void TraiteFloat ( ref float fVal );
		public abstract void TraiteDecimal(ref Decimal dVal);

        //----------------------------------------------------
        public delegate void AfterReadOldDbKeyCompatibleDelegate(CDbKey key, Type typeObjet);

        private static List<AfterReadOldDbKeyCompatibleDelegate> m_listeTraitementsSupplementairesAfterReadOldId = new List<AfterReadOldDbKeyCompatibleDelegate>();

        public static void RegisterAfterReadOldDbKeyFunction(AfterReadOldDbKeyCompatibleDelegate function)
        {
            m_listeTraitementsSupplementairesAfterReadOldId.Add(function);
        }

        //----------------------------------------------------
        public void ReadDbKeyFromOldId(ref CDbKey key, Type typeObjet)
        {
            CDbKey.ReadFromOldId(this, ref key);
            if (key != null)
            {
                foreach (AfterReadOldDbKeyCompatibleDelegate f in m_listeTraitementsSupplementairesAfterReadOldId)
                    f(key, typeObjet);
                TrackDbKeyReaded(key);
            }
            
        }

        //----------------------------------------------------
        public void TrackDbKeyReaded ( CDbKey key )
        {
            if (key != null)
            {
                foreach (CDbKeyReaderTracker tracker in m_listeDbKeyTrackers)
                    tracker.RegisterDbKey(key);
            }
        }

        //----------------------------------------------------
        public void TrackDbKeyReaded ( string strUniversalId )
        {
            if ( strUniversalId != null )
            {
                CDbKey key= CDbKey.GetNewDbKeyOnUniversalIdANePasUtiliserEnDehorsDeCDbKeyAddOn(strUniversalId);
                TrackDbKeyReaded(key);
            }
        }

        //----------------------------------------------------
        public void TraiteDbKey(ref CDbKey key)
        {
            CDbKey.SerializeKey(this, ref key);
            if (Mode == ModeSerialisation.Lecture)
                TrackDbKeyReaded(key);
        }
        
        //----------------------------------------------------
        public void TraiteIntNullable(ref int? nVal)
        {
            bool bHasVal = nVal != null;
            TraiteBool(ref bHasVal);
            if (bHasVal)
            {
                int valNonNulle = nVal != null ? nVal.Value : 0;
                TraiteInt(ref valNonNulle);
                if (Mode == ModeSerialisation.Lecture)
                    nVal = valNonNulle;
            }
        }

        //----------------------------------------------------
        public void TraiteDoubleNullable(ref double? nVal)
        {
            bool bHasVal = nVal != null;
            TraiteBool(ref bHasVal);
            if (bHasVal)
            {
                double valNonNulle = nVal != null ? nVal.Value : 0;
                TraiteDouble(ref valNonNulle);
                if (Mode == ModeSerialisation.Lecture)
                    nVal = valNonNulle;
            }
        }

        //----------------------------------------------------
        public void TraiteBoolNullable(ref bool? nVal)
        {
            bool bHasVal = nVal != null;
            TraiteBool(ref bHasVal);
            if (bHasVal)
            {
                bool valNonNulle = nVal != null ? nVal.Value : false;
                TraiteBool(ref valNonNulle);
                if (Mode == ModeSerialisation.Lecture)
                    nVal = valNonNulle;
            }
        }

        //----------------------------------------------------
        public void TraiteLongNullable(ref long? nVal)
        {
            bool bHasVal = nVal != null;
            TraiteBool(ref bHasVal);
            if (bHasVal)
            {
                long valNonNulle = nVal != null ? nVal.Value : 0;
                TraiteLong(ref valNonNulle);
                if (Mode == ModeSerialisation.Lecture)
                    nVal = valNonNulle;
            }
        }

        //----------------------------------------------------
        public void TraiteFloatNullable(ref float? nVal)
        {
            bool bHasVal = nVal != null;
            TraiteBool(ref bHasVal);
            if (bHasVal)
            {
                float valNonNulle = nVal != null ? nVal.Value : 0;
                TraiteFloat(ref valNonNulle);
                if (Mode == ModeSerialisation.Lecture)
                    nVal = valNonNulle;
            }
        }

        //----------------------------------------------------
        public void TraiteDecimalNullable(ref Decimal? nVal)
        {
            bool bHasVal = nVal != null;
            TraiteBool(ref bHasVal);
            if (bHasVal)
            {
                Decimal valNonNulle = nVal != null ? nVal.Value : 0;
                TraiteDecimal(ref valNonNulle);
                if (Mode == ModeSerialisation.Lecture)
                    nVal = valNonNulle;
            }
        }

        //----------------------------------------------------
        public void TraiteDateTimeNullable(ref DateTime? dtVal)
        {
            bool bHasVal = dtVal != null;
            TraiteBool(ref bHasVal);
            if (bHasVal)
            {
                DateTime dtNonNulle = dtVal != null ? dtVal.Value : new DateTime();
                TraiteDate(ref dtNonNulle);
                if (Mode == ModeSerialisation.Lecture)
                    dtVal = dtNonNulle;
            }
        }

        //----------------------------------------------------
        private void TraiteSimple<T>(ref object obj)
        {
            if ( typeof(T) == typeof(string ) )
            {
                string strVal = (string)obj;
                TraiteString ( ref strVal );
                obj = strVal;
            }
            else if (typeof(T) == typeof(int))
            {
                int nVal = (int)obj;
                TraiteInt(ref nVal);
                obj = nVal;
            }
            else if (typeof(T) == typeof(double))
            {
                double fVal = (double)obj;
                TraiteDouble(ref fVal);
                obj = fVal;
            }
            else if (typeof(T) == typeof(bool))
            {
                bool bVal = (bool)obj;
                TraiteBool(ref bVal);
                obj = bVal;
            }
            else if (typeof(T) == typeof(DateTime))
            {
                DateTime dt = (DateTime)obj;
                TraiteDate(ref dt);
                obj = dt;
            }
            else if (typeof(T) == typeof(Byte))
            {
                Byte bt = (Byte)obj;
                TraiteByte(ref bt);
                obj = bt;
            }
            else if (typeof(T) == typeof(byte[]))
            {
                byte[] bts = (byte[])obj;
                TraiteByteArray(ref bts);
                obj = bts;
            }
            else if (typeof(T) == typeof(long))
            {
                long nVal = (long)obj;
                TraiteLong(ref nVal);
                obj = nVal;
            }
            else if (typeof(T) == typeof(float))
            {
                float fVal = (float)obj;
                TraiteFloat(ref fVal);
                obj = fVal;
            }
            else if (typeof(T) == typeof(decimal))
            {
                Decimal dec = (Decimal)obj;
                TraiteDecimal(ref dec);
                obj = dec;
            }
            else
            {
                throw new Exception("Serialize T is not allowed for type " + typeof(T).ToString());
            }
        }

        //---------------------------------------------------
        public void TraiteListeInt(List<int> lst)
        {
            TraiteList<int>(lst);
        }

        //---------------------------------------------------
        public void TraiteListString(List<string> lst)
        {
            TraiteList<string>(lst);
        }

        //---------------------------------------------------
        public void TraiteListDouble(List<double> lst)
        {
            TraiteList<double>(lst);
        }

        //---------------------------------------------------
        public void TraiteListBool(List<bool> lst)
        {
            TraiteList<bool>(lst);
        }

        //---------------------------------------------------
        public void TraiteListByte(List<byte> lst)
        {
            TraiteList<byte>(lst);
        }

        //---------------------------------------------------
        public void TraiteListLong(List<long> lst)
        {
            TraiteList<long>(lst);
        }

        //---------------------------------------------------
        public void TraiteListFloat(List<float> lst)
        {
            TraiteList<float>(lst);
        }

        //---------------------------------------------------
        public void traiteListDecimal(List<Decimal> lst)
        {
            TraiteList<Decimal>(lst);
        }
        
        //---------------------------------------------------
        private void TraiteList<T>(List<T> lst)
        {
            bool bHasList = lst != null;
            TraiteBool(ref bHasList);
            if (bHasList)
            {
                int nNb = lst.Count;
                TraiteInt(ref nNb);
                switch (Mode)
                {
                    case ModeSerialisation.Ecriture:
                        foreach (T val in lst)
                        {
                            object copie = val;
                            TraiteSimple<T>(ref copie );
                        }
                        break;
                    case ModeSerialisation.Lecture:
                        lst.Clear();
                        for (int n = 0; n < nNb; n++)
                        {
                            object copie = default(T);
                            TraiteSimple<T>(ref copie);
                            lst.Add((T)copie);
                        }
                        break;
                }
            }
        }

        public void TraiteColor ( ref Color couleur )
        {
            int nCol = 0;
            if (Mode == ModeSerialisation.Ecriture)
                nCol = couleur.ToArgb();
            TraiteInt(ref nCol);
            if (Mode == ModeSerialisation.Lecture)
                couleur = Color.FromArgb(nCol);
        }

        /// //////////////////////////////////////////////
        public void TraiteColor(ref Color? couleur)
        {
            bool bHasColor = couleur != null;
            TraiteBool(ref bHasColor);
            if (bHasColor)
            {
                Color cTmp = Color.White;
                if (Mode == ModeSerialisation.Ecriture)
                    cTmp = couleur.Value;
                TraiteColor(ref cTmp);
                couleur = cTmp;
            }
            else if (Mode == ModeSerialisation.Lecture)
                couleur = null;
        }

        /// //////////////////////////////////////////////
        public void TraiteBitmap(ref Bitmap bmp, bool bDisposeOnRead)
        {
            bool bHasImage = bmp != null;
            TraiteBool(ref bHasImage);
            if (bHasImage)
            {
                switch (Mode)
                {
                    case ModeSerialisation.Lecture:
                        Byte[] bt = null;
                        TraiteByteArray(ref bt);
                        if (bmp != null)
                            bmp.Dispose();
                        bmp = null;
                        MemoryStream stream = new MemoryStream(bt);
                        try
                        {
                            bmp = (Bitmap)Bitmap.FromStream(stream);
                        }
                        catch
                        {
                            bmp = null;
                        }
                        stream.Close();
                        break;

                    case ModeSerialisation.Ecriture:
                        MemoryStream streamSave = new MemoryStream();
                        try
                        {
                            Bitmap copie = new Bitmap(bmp);
                            copie.Save(streamSave, System.Drawing.Imaging.ImageFormat.Png);
                            copie.Dispose();
                        }
                        catch (Exception e)
                        {
                            string strVal = e.ToString();
                        }
                        Byte[] buf = streamSave.GetBuffer();
                        TraiteByteArray(ref buf);
                        streamSave.Close();
                        break;

                }
            }
        }


        /// //////////////////////////////////////////////
        public void TraiteEnum<T>(ref T valeur)
            where T : struct
        {
            int nVal = Convert.ToInt32(valeur);
            TraiteInt(ref nVal);
            try
            {
                valeur = (T)Enum.ToObject(typeof(T), nVal);
            }
            catch { }
        }
                    


		//Pile d'objets attachés par type d'objets
		private Hashtable m_tableElementsAttaches = new Hashtable();

		/// //////////////////////////////////////////////
		public object GetObjetAttache ( Type type )
		{
			Stack st = (Stack)m_tableElementsAttaches[type];
			if ( st != null && st.Count > 0 )
				return st.Peek();
			return null;
		}


		/// //////////////////////////////////////////////
		/// Attache un objet externe au serializer
		/// Utilisé par exemple si un sous objet d'un objet a besoin
		/// de connaitre l'objet principal, on passe l'objet
		/// principal comme objet attaché et on va le
		/// rechercher dans l'objet secondaire.
		public void AttacheObjet ( Type type, object obj )
		{
			Stack st = (Stack)m_tableElementsAttaches[type];
			if ( st == null )
			{
				st = new Stack();
				m_tableElementsAttaches[type] = st;
			}
			st.Push ( obj );
		}

		/// //////////////////////////////////////////////
		public void DetacheObjet ( Type type, object obj )
		{
			Stack st = (Stack)m_tableElementsAttaches[type];
			if ( st != null )
				st.Pop();
		}

		/// //////////////////////////////////////////////
		public static void CloneTo ( I2iSerializable objSource, I2iSerializable objDest )
		{
			CStringSerializer serializer = new CStringSerializer(ModeSerialisation.Ecriture);
			objSource.Serialize ( serializer );
			string strData = serializer.String;
			serializer = new CStringSerializer ( strData, ModeSerialisation.Lecture );
			objDest.Serialize ( serializer );
		}
		
		/// //////////////////////////////////////////////
		public virtual void TraiteDate ( ref DateTime dt )
		{
			int nYear = dt.Year;
			TraiteInt ( ref nYear );

			int nMonth = dt.Month;
			TraiteInt ( ref nMonth );

			int nDay = dt.Day;
			TraiteInt ( ref nDay );

			int nHour = dt.Hour;
			TraiteInt ( ref nHour );

			int nMin =  dt.Minute;
			TraiteInt ( ref nMin );
			
			int nSec = dt.Second;
			TraiteInt ( ref nSec );
			
			int nMilli = dt.Millisecond;
			TraiteInt ( ref nMilli );

			if ( Mode == ModeSerialisation.Lecture )
				dt = new DateTime ( nYear, nMonth, nDay, nHour, nMin, nSec, nMilli );
		}

		/// //////////////////////////////////////////////
		public void TraiteType ( ref Type tp )
		{
			string strType = "";
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					//Stef, 25/07/08, stocke le nom de l'assembly avec
					strType = CActivatorSurChaine.GetNomTypeAvecAssembly(tp);
					TraiteString( ref strType );
					break;

				case ModeSerialisation.Lecture :
					TraiteString ( ref strType );
					try
					{
						tp = null;
						tp = CActivatorSurChaine.GetType(strType);
					}
					catch
					{
					}
					if (tp == null && strType.Length > 0)
					{
                       Console.WriteLine(I.T("Cannot allocate type @1 in C2iSerializer.TraiteType|30110",strType));
					}
					break;
			}
		}

		/// //////////////////////////////////////////////
		public static EnumTypeSimple GetTypeSimpleObjet ( object obj )
		{
			if ( obj == null )
				return EnumTypeSimple.Null;
			return GetTypeSimple ( obj.GetType() );
			
		}

		/// //////////////////////////////////////////////
		public static EnumTypeSimple GetTypeSimple ( Type tp )
		{
            if (typeof(Nullable).IsAssignableFrom(tp))
                tp = tp.GetGenericArguments()[0];
            if (tp == typeof(string))
                return EnumTypeSimple.String;
            else if (tp == typeof(int))
                return EnumTypeSimple.Int;
            else if (tp == typeof(double))
                return EnumTypeSimple.Double;
            else if (tp == typeof(bool))
                return EnumTypeSimple.Bool;
            else if (tp == typeof(byte))
                return EnumTypeSimple.Byte;
            else if (tp == typeof(DateTime))
                return EnumTypeSimple.DateTime;
            else if (tp == typeof(Type))
                return EnumTypeSimple.Type;
            else if (tp == typeof(long))
                return EnumTypeSimple.Long;
            else if (tp == typeof(Decimal))
                return EnumTypeSimple.Decimal;
            else if ( typeof(I2iSerializable).IsAssignableFrom ( tp ) )
                return EnumTypeSimple.Serializable;

			return EnumTypeSimple.Inconnu;
		}

		/// //////////////////////////////////////////////
		/// <summary>
		/// Serialize une liste d'objets simples
		/// </summary>
		/// <param name="lst"></param>
		/// <returns></returns>
		public virtual CResultAErreur TraiteListeObjetsSimples ( ref IList lst )
		{

            int nNb = 0;
            if ( lst != null )
                nNb = lst.Count;
			TraiteInt ( ref nNb );
			CResultAErreur result = CResultAErreur.True;
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
                    if (lst != null)
                    {
                        foreach (object obj in lst)
                        {
                            object objTmp = obj;
                            result = TraiteObjetSimple(ref objTmp);
                            if (!result)
                                return result;
                        }
                    }
					break;
				case ModeSerialisation.Lecture :
					lst = new ArrayList();
					for ( int nObj = 0; nObj < nNb; nObj++ )
					{
						object objTmp = null;
						result = TraiteObjetSimple ( ref objTmp );
						if ( !result )
							return result;
						lst.Add ( objTmp );
					}
					break;
			}
			return result;
		}
		
		/// //////////////////////////////////////////////
		public static bool IsObjetSimple ( object valeur )
		{
			return GetTypeSimpleObjet ( valeur ) != EnumTypeSimple.Inconnu;
		}

		/// //////////////////////////////////////////////
		/// <summary>
		/// Sauve un objet de type simple (int, double, string, bool, byte, DateTime ou Type)
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public virtual CResultAErreur TraiteObjetSimple ( ref object obj )
		{
			CResultAErreur result = CResultAErreur.True;
			int nType = (int)EnumTypeSimple.String;
			if ( Mode == ModeSerialisation.Ecriture )
			{
				nType = (int)GetTypeSimpleObjet ( obj );
				if ( nType == (int)EnumTypeSimple.Inconnu )
				{
					result.EmpileErreur (I.T("Impossible to serialize non-simple type @1|30111",obj.GetType().ToString()) );
					return result;
				}
			}
			TraiteInt ( ref nType );
			return TraiteObjetSimple ( ref obj, (EnumTypeSimple)nType );
		}

		// //////////////////////////////////////////////
		public virtual CResultAErreur TraiteObjetSimple ( ref object obj, EnumTypeSimple typeSimple )
		{
			CResultAErreur result = CResultAErreur.True;
			switch ( typeSimple )
			{
				case EnumTypeSimple.Bool :
					bool bVal = true;
					if ( Mode == ModeSerialisation.Ecriture )
						bVal = (bool)obj;
					TraiteBool ( ref bVal );
					obj = bVal;
					break;
				case EnumTypeSimple.Byte :
					byte btVal = 0;
					if ( Mode == ModeSerialisation.Ecriture )
						btVal = (byte)obj;
					TraiteByte(ref btVal );
					obj =btVal;
					break;
				case EnumTypeSimple.DateTime :
					DateTime dtVal = DateTime.Now;
					if ( Mode == ModeSerialisation.Ecriture )
						dtVal = (DateTime)obj;
					TraiteDate ( ref dtVal );
					obj = dtVal;
					break;
				case EnumTypeSimple.Double :
					double dVal = 0;
					if ( Mode == ModeSerialisation.Ecriture )
						dVal = (double)obj;
					TraiteDouble ( ref dVal );
					obj = dVal;
					break;
				case EnumTypeSimple.Int :
					int nVal = 0;
					if ( Mode == ModeSerialisation.Ecriture )
						nVal = (int)obj;
					TraiteInt ( ref nVal );
					obj = nVal;
					break;
				case EnumTypeSimple.Null :
					obj = null;
					break;
				case EnumTypeSimple.String :
					string strVal = "";
					if ( Mode == ModeSerialisation.Ecriture )
						strVal = (string)obj;
					TraiteString ( ref strVal );
					obj = strVal;
					break;
				case EnumTypeSimple.Type :
					Type tp = GetType();
					if ( Mode == ModeSerialisation.Ecriture )
						tp = (Type)obj;
					TraiteType ( ref tp );
					obj = tp;
					break;
				case EnumTypeSimple.Long :
					long lVal = 0;
					if ( Mode == ModeSerialisation.Ecriture )
						lVal = (long)obj;
					TraiteLong ( ref lVal );
					obj = lVal;
					break;
				case EnumTypeSimple.Decimal :
					Decimal dcVal = 0;
					if (Mode == ModeSerialisation.Ecriture)
						dcVal = (Decimal)obj;
					TraiteDecimal(ref dcVal);
					obj = dcVal;
					break;
                case EnumTypeSimple.Serializable:
                    I2iSerializable ser = obj as I2iSerializable;
                    result = TraiteObject ( ref ser );
                    obj = ser;
                    break;

			}
			return result;
		}

		/// //////////////////////////////////////////////
		public virtual CResultAErreur TraiteObject<T>( ref T objet, params object[] parametresConstructeur )
			where T : I2iSerializable
		{
			I2iSerializable objSer = (I2iSerializable)objet;
			CResultAErreur result = TraiteObject(ref objSer, parametresConstructeur);
			if (!result)
				return result;
			objet = (T)objSer;
			return result;
		}

		/// //////////////////////////////////////////////
		public virtual CResultAErreur TraiteObject ( ref I2iSerializable objet, params object[] parametresDeConstructeur )
		{
			return TraiteObject(null, ref objet, parametresDeConstructeur);
		}

		public virtual CResultAErreur TraiteObject(Type newType, ref I2iSerializable objet, params object[] parametresDeConstructeur)
		{
			CResultAErreur result = CResultAErreur.True;
			bool bHasObjet = objet != null;
			TraiteBool(ref bHasObjet);
			switch (Mode)
			{
				case ModeSerialisation.Ecriture:
					if (bHasObjet)
					{
						Type tp = objet.GetType();
						TraiteType(ref tp);
						result = objet.Serialize(this);
					}
					break;
				case ModeSerialisation.Lecture:
					if (!bHasObjet)
						objet = null;
					else
					{
						Type tp = null;
						if (newType == null)
							TraiteType(ref tp);
						else
						{
							string strOldType = "";
							TraiteString(ref strOldType);
							tp = newType;
						}
						try
						{
#if PDA
							objet = (I2iSerializable)Activator.CreateInstance(tp);
#else
							objet = (I2iSerializable)Activator.CreateInstance(tp, parametresDeConstructeur);
#endif
							result = objet.Serialize(this);
						}
						catch (Exception e)
						{
							result.EmpileErreur(new CErreurException(e));
							result.EmpileErreur(I.T("Impossible to allocate an object of type @1|30112" , tp.ToString()));
							return result;
						}
					}
					break;
			}
			return result;
		}

		/// //////////////////////////////////////////////
		public virtual CResultAErreur TraiteVersion ( ref int nVersion )
		{
			CResultAErreur result = CResultAErreur.True;
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					TraiteInt ( ref nVersion );
					break;
				case ModeSerialisation.Lecture :
					int nVersionEnCours = nVersion;
					TraiteInt ( ref nVersion );
					if ( nVersion > nVersionEnCours )
					{
						result.EmpileErreur(I.T("Incorrect version number|30113"));
					}
					break;
			}
			return result;
		}


		//---------------------------------------------------------
		public virtual CResultAErreur TraiteListe(List<I2iSerializable> lstVals, params object[] parametresDeConstructeur)
		{
			return TraiteListe(null, lstVals, parametresDeConstructeur);
		}
		public virtual CResultAErreur TraiteListe (Type newType, List<I2iSerializable> lstVals, params object[] parametresDeConstructeur )
		{
			CResultAErreur result = CResultAErreur.True;
			int nNb = lstVals.Count;
			TraiteInt(ref nNb);
			switch (Mode)
			{
				case ModeSerialisation.Ecriture:
					foreach (I2iSerializable obj in lstVals)
					{
						I2iSerializable dummy = obj;
						result = TraiteObject(ref dummy, parametresDeConstructeur);
						if (!result)
							return result;
					}
					break;
				case ModeSerialisation.Lecture:
					lstVals.Clear();
					for (int nElt = 0; nElt < nNb; nElt++)
					{
						I2iSerializable obj = null;
						if(newType == null)
							result = TraiteObject(ref obj, parametresDeConstructeur);
						else
							result = TraiteObject(newType, ref obj, parametresDeConstructeur);

						if (!result)
							return result;
						lstVals.Add(obj);
					}
					break;
			}
			return result;
		}

		/// //////////////////////////////////////////////
		public virtual CResultAErreur TraiteArrayListOf2iSerializable(ArrayList lstVals, params object[] parametresDeConstructeur)
		{
			return TraiteArrayListOf2iSerializable(null, lstVals, parametresDeConstructeur);
		}
		public virtual CResultAErreur TraiteArrayListOf2iSerializable (Type newType, ArrayList lstVals, params object[] parametresDeConstructeur )
		{
			CResultAErreur result = CResultAErreur.True;
			int nNb = lstVals.Count;
			TraiteInt( ref nNb );
			switch ( Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach ( I2iSerializable obj in lstVals )
					{
						I2iSerializable dummy = obj;
						result = TraiteObject ( ref dummy, parametresDeConstructeur );
						if ( !result )
							return result;
					}
					break;
				case ModeSerialisation.Lecture :
					lstVals.Clear();
					for ( int nElt = 0; nElt < nNb; nElt ++)
					{
						I2iSerializable obj = null;
						if(newType == null)
							result = TraiteObject( ref obj, parametresDeConstructeur );
						else
							result = TraiteObject(newType, ref obj, parametresDeConstructeur);

						if ( !result )
							return result;
						lstVals.Add ( obj );
					}
					break;
			}
			return result;
		}

		/// //////////////////////////////////////////////
		public CResultAErreur SerializeObjet(ref I2iSerializable objet)
		{
			return SerializeObjet(ref objet, null);
		}

		/// //////////////////////////////////////////////
		public CResultAErreur SerializeObjet(ref I2iSerializable objet, Type typeSpecifie)
		{
			bool bNull = false;
			Type tp = null;
			switch (this.Mode)
			{
				case ModeSerialisation.Ecriture:
					bNull = objet == null;
					TraiteBool(ref bNull);
					if (!bNull)
					{
						tp = objet.GetType();
						TraiteType(ref tp);
						return objet.Serialize(this);
					}
					break;

				case ModeSerialisation.Lecture:
					bNull = false;
					TraiteBool(ref bNull);
					if (bNull)
					{
						objet = null;
						return CResultAErreur.True;
					}
					tp = null;
					if (typeSpecifie == null)
						TraiteType(ref tp);
					else
					{
						string strOldType = "";
						TraiteString(ref strOldType);
						tp = typeSpecifie;
					}

					try
					{
						objet = (I2iSerializable)Activator.CreateInstance(tp);
						return objet.Serialize(this);
					}
					catch (Exception e)
					{
						CResultAErreur result = CResultAErreur.True;
						result.EmpileErreur(new CErreurException(e));
						return result;
					}
			}
			return CResultAErreur.True;
		}


		private int GetNumVersionListeGenerique()
		{
			return 0;
		}

		public CResultAErreur TraiteListe<T>(List<T> listeToSerialize, params object[] parametresConstructeur) where T : I2iSerializable
		{
			CResultAErreur result = CResultAErreur.True;
			int nVersion = GetNumVersionListeGenerique();
			result = TraiteVersion(ref nVersion);
			int nNb = listeToSerialize.Count;
			TraiteInt(ref nNb);
			switch (Mode)
			{
				case ModeSerialisation.Ecriture:
					foreach (I2iSerializable objet in listeToSerialize)
					{
						I2iSerializable tmp = objet;
						result = TraiteObject(ref tmp);
						if (!result)
							return result;
					}
					break;
				case ModeSerialisation.Lecture:
					List<T> lstTmp = new List<T>();
					for (int nObjet = 0; nObjet < nNb; nObjet++)
					{
						I2iSerializable tmp = null;
						result = TraiteObject(ref tmp, parametresConstructeur);
						if (!result)
							return result;
						lstTmp.Add((T)tmp);
					}
					listeToSerialize.Clear();
					listeToSerialize.AddRange(lstTmp);
					break;
			}
			return result;
		}

        /// //////////////////////////////////////////////
        public CResultAErreur TraiteSerializable(ref object obj)
        {
            CResultAErreur result = CResultAErreur.True;
            switch (Mode)
            {
                case ModeSerialisation.Ecriture:
                    MemoryStream stream = new MemoryStream();
                    BinaryFormatter formatter = new BinaryFormatter();
                    try
                    {
                        formatter.Serialize(stream, obj);
                    }
                    catch (Exception e)
                    {
                        result.EmpileErreur(new CErreurException(e));
                    }
                    stream.Close();
                    if (result)
                    {
                        byte[] bt = stream.GetBuffer();
                        TraiteByteArray(ref bt);
                    }
                    break;
                case ModeSerialisation.Lecture:
                    byte[] btRead = null;
                    TraiteByteArray(ref btRead);
                    if (btRead != null)
                    {
                        MemoryStream reader = new MemoryStream(btRead);
                        BinaryFormatter formatterRead = new BinaryFormatter();
                        try
                        {
                            obj = formatterRead.Deserialize(reader);
                        }
                        catch (Exception e)
                        {
                            result.EmpileErreur(new CErreurException(e));
                        }
                        reader.Close();
                    }
                    break;
            }
            return result;
        }

                        
	}
}
