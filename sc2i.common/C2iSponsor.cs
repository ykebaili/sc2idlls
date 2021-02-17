using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Proxies;
using System.Threading;
using System.Collections.Generic;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CIpmSponsor.
	/// </summary>
	public class C2iSponsor : MarshalByRefObject, ISponsor, IDisposable
	{
		
		private const int c_nbSecondes = 120;
		private const int c_delaiGarbageSuspectSecondes = 300;

		//variables statiques pour sécurité maximale !!!
		private static ArrayList m_gl_listeSponsors = new ArrayList(); //Liste de tous les sponsors créés
		private static Timer	 m_gl_timerSecurite = null;//Timer utilisant renewOnCall time pour renouveller les baux

		public static int m_nNbObjetsSponsorises = 0;

        private Dictionary<string, CProxy2iMarshal> m_dicObjetsSponsor = new Dictionary<string, CProxy2iMarshal>();

		private ArrayList	m_listeObjetsSponsorises;
		private string		m_strTraceHeader = "";
		private CSuspectGarbage m_suspectGarbage;

        private class CProxy2iMarshal
        {
            public I2iMarshalObject Objet{get;set;}
            public string UniqueId{get;set;}

            public CProxy2iMarshal(I2iMarshalObject objet )
            {
                Objet = objet;
                UniqueId = objet.UniqueId;
            }
        }

		
		//////////////////////////////////////////////////////////////////////////////////
		///Gère la suppression des éléments qui ne répondent plus
		private class CSuspectGarbage
		{
			//Table DateHeure->ArrayList de suspects
			Hashtable m_tableListeSuspects = new Hashtable();

			private ArrayList m_listeEnCours = null;

			private C2iSponsor m_sponsor = null;

			//////////////////////////////////////////////////////////////////////////////////
			public CSuspectGarbage( C2iSponsor sponsor )
			{
				m_sponsor = sponsor;
			}

			//////////////////////////////////////////////////////////////////////////////////
			public void CreateGeneration()
			{
				DateTime dt = DateTime.Now;
				if ( m_tableListeSuspects[dt] != null) 
				{
					m_listeEnCours = (ArrayList)m_tableListeSuspects[dt];
				}
				else
				{
					m_listeEnCours = new ArrayList();
					m_tableListeSuspects[dt] = m_listeEnCours;
				}
			}

			//////////////////////////////////////////////////////////////////////////////////
			public void RegisterSuspect ( object obj )
			{
				m_listeEnCours.Add ( obj );
			}

			//////////////////////////////////////////////////////////////////////////////////
			public void RenouvelleBailParAppel()
			{
				ArrayList lstToRemove = new ArrayList();
				foreach ( DateTime dt in m_tableListeSuspects.Keys )
				{
					ArrayList lst = (ArrayList)m_tableListeSuspects[dt];
					if ( dt.AddSeconds ( c_delaiGarbageSuspectSecondes )< DateTime.Now )
					{
						lstToRemove.Add ( dt );
						if ( lst != null )
						{
							m_nNbObjetsSponsorises-=lst.Count;
							if ( lst.Count > 0 )
								Console.WriteLine(I.T("Suspects suppression : @1 sponsor objects|103",m_nNbObjetsSponsorises.ToString() ));
							
						}
					}
					else
					{
						if ( lst != null )
						{
							foreach ( object obj in lst.ToArray() )
							{
								if ( obj is I2iMarshalObject )
								{
									try
									{
										((I2iMarshalObject)obj).RenouvelleBailParAppel();
										//C'est passé, on le réabilite
										m_sponsor.Register ( obj );
										lst.Remove(obj);
									}
									catch{}
								}

							}
						}
					}
				}
				foreach ( DateTime dt in lstToRemove )
				{
					try
					{
						m_tableListeSuspects.Remove(dt);
					}
					catch
					{}
				}
			}
		}
		
		//////////////////////////////////////////////////////////////////////////////////
		public C2iSponsor()
		{
			m_listeObjetsSponsorises = new ArrayList();
			m_suspectGarbage = new CSuspectGarbage(this);
			m_gl_listeSponsors.Add ( this );
		}

		//////////////////////////////////////////////////////////////////////////////////
		public static bool IsSecuriteEnabled
		{
			get
			{
				return m_gl_timerSecurite != null;
			}
		}

		//////////////////////////////////////////////////////////////////////////////////
		public static void EnableSecurite()
		{
			if ( m_gl_timerSecurite == null )
			{
				/*int nDuree =(int)( LifetimeServices.RenewOnCallTime.TotalSeconds / 2);
				TimeSpan duree = TimeSpan.FromSeconds(Math.Max ( nDuree, 5 ));*/
				TimeSpan duree = TimeSpan.FromMinutes(1);
				m_gl_timerSecurite = new Timer ( new TimerCallback ( FonctionSecuriteParTimer ), null, duree, duree );	
			}
		}

		//////////////////////////////////////////////////////////////////////////////////
		public static void StopSecurite()
		{
			if ( m_gl_timerSecurite != null )
			{
				m_gl_timerSecurite.Dispose();
				m_gl_timerSecurite = null;
			}
		}


		/////////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			UnregisterAll();
            m_listeObjetsSponsorises.Clear();
            m_dicObjetsSponsor.Clear();
            m_suspectGarbage = null;
            m_gl_listeSponsors.Remove(this);
		}

		//////////////////////////////////////////////////////////////////////////////////
		public string TraceHeader
		{
			get { return m_strTraceHeader;}
			set { m_strTraceHeader = value;}
		}

		///////////////////////////////////////////////////////////////////////////////////
		public TimeSpan Renewal ( ILease lease )
		{
            //return TimeSpan.FromSeconds(c_nbSecondes);
            return TimeSpan.FromMinutes(2);
        }
        
                
        //********** Informations de déboguage **********
        public string Label = "";

        public override string ToString()
        {
            return Label;
        }
        //***********************************************

		///////////////////////////////////////////////////////////////////////////////////
		public void Register ( object obj )
		{
			if ( obj == null )
				return;
			if ( !(obj is MarshalByRefObject ))
				return;
			MarshalByRefObject marshalObj = (MarshalByRefObject)obj;
			//ObjRef laRef = marshalObj.CreateObjRef ( obj.GetType() );
            ILease lease = (ILease)RemotingServices.GetLifetimeService(marshalObj);
			if ( lease == null )
				return;
            //lease.Register(this, TimeSpan.FromSeconds(c_nbSecondes));
            lease.Register(this, TimeSpan.FromMinutes(2));

			m_nNbObjetsSponsorises++;
            I2iMarshalObject obj2i = obj as I2iMarshalObject;
            if (obj2i != null)
                m_dicObjetsSponsor[obj2i.UniqueId] = new CProxy2iMarshal(obj2i);
            else
			    m_listeObjetsSponsorises.Add ( obj );
		}

		///////////////////////////////////////////////////////////////////////////////////
		public void Unregister ( object obj )
		{
            DateTime dt = DateTime.Now;
            TimeSpan sp;
			if ( obj == null )
				return;
			try
			{
                I2iMarshalObject obj2i = obj as I2iMarshalObject;
                if (obj2i != null)
                {
                    string strId = obj2i.UniqueId;
                    if (m_dicObjetsSponsor.ContainsKey(strId))
                    {
                        m_dicObjetsSponsor.Remove(strId);
                        m_nNbObjetsSponsorises--;
                    }
                }
                else
                {
                    if (m_listeObjetsSponsorises.Contains(obj))
                    {
                        m_listeObjetsSponsorises.Remove(obj);
                        m_nNbObjetsSponsorises--;
                    }
                }
                sp = DateTime.Now - dt;
                Console.WriteLine("sponsor unregister 1:" + sp.TotalMilliseconds);
			}
			catch
			{
                sp = DateTime.Now - dt;
                Console.WriteLine("sponsor unregister 2:" + sp.TotalMilliseconds);
				/*//Probablement un objet remote qui ne répond plus
				try
				{
					RealProxy proxy = RemotingServices.GetRealProxy( obj );
					int nIndex = -1;
					foreach ( object objInListe in m_listeObjetsSponsorises )
					{
						try
						{
							nIndex++;
							RealProxy prox2 = RemotingServices.GetRealProxy( objInListe );
							if ( prox2.Equals ( proxy ) )
							{
								m_nNbObjetsSponsorises--;
								m_listeObjetsSponsorises.RemoveAt ( nIndex );
								break;
							}
						}
						catch
						{}
					}
				}
				catch
				{
				}*/
			}
			try
			{
				ILease lease = (ILease)RemotingServices.GetLifetimeService ( (MarshalByRefObject)obj );
                sp = DateTime.Now - dt;
                Console.WriteLine("sponsor unregister 3 :" + sp.TotalMilliseconds);
				if ( lease == null )
					return;
				lease.Unregister ( this );
                sp = DateTime.Now - dt;
                Console.WriteLine("sponsor unregister 4 :" + sp.TotalMilliseconds);
			}
			catch{
                sp = DateTime.Now - dt;
                Console.WriteLine("sponsor unregister 5 :" + sp.TotalMilliseconds);
            }
			
		}

		///////////////////////////////////////////////////////////////////////////////////
		private static bool m_bDejaEnSecurite = false;
		public static void FonctionSecuriteParTimer( object state )
		{
			if ( m_bDejaEnSecurite )
				return;
			try
			{
				m_bDejaEnSecurite = true;
				foreach ( C2iSponsor sponsor in m_gl_listeSponsors.ToArray() )
				{
                    string strLibelle = sponsor.Label; // Pour debuguer uniquement
					try
					{
						sponsor.m_suspectGarbage.CreateGeneration();
						if ( sponsor.m_listeObjetsSponsorises != null )
						{
                            List<object> lstObjets = new List<object>(sponsor.m_listeObjetsSponsorises.ToArray());
                            foreach (object obj in sponsor.m_dicObjetsSponsor.Values)
                                lstObjets.Add(obj);
							int nIndex = 0;
							sponsor.m_suspectGarbage.RenouvelleBailParAppel();
                            foreach (object obj in lstObjets)
                            {
                                I2iMarshalObject obj2i = obj as I2iMarshalObject;
                                CProxy2iMarshal proxy = null;
                                if (obj2i == null)
                                {
                                    proxy = obj as CProxy2iMarshal;
                                    if (proxy != null)
                                        obj2i = proxy.Objet;
                                }
                                if (proxy == null)
                                    nIndex++;
                                if (obj2i != null)
                                {
                                    try
                                    {
                                        Type tp = obj2i.GetType();

                                        ILease lease = (ILease)RemotingServices.GetLifetimeService((MarshalByRefObject)obj2i);
                                        if (lease != null)
                                            lease.Renew(TimeSpan.FromMinutes(2));

                                        obj2i.RenouvelleBailParAppel();
                                        nIndex++;
                                    }
                                    catch
                                    {
                                        sponsor.m_suspectGarbage.RegisterSuspect(obj2i);

                                        if (proxy != null)
                                        {
                                            sponsor.m_dicObjetsSponsor.Remove(proxy.UniqueId);
                                        }
                                        else
                                            sponsor.m_listeObjetsSponsorises.RemoveAt(nIndex);
                                    }
                                }
                            }
						}
					}
					catch{}
				}
			}
			catch
			{}
			finally
			{
				m_bDejaEnSecurite = false;
			}
		}


		///////////////////////////////////////////////////////////////////////////////////
		public void UnregisterAll ()
		{
			try
			{
				ArrayList lstTmp = (ArrayList)m_listeObjetsSponsorises.Clone();
                foreach (CProxy2iMarshal p in m_dicObjetsSponsor.Values)
                    if (p.Objet is MarshalByRefObject)
                        lstTmp.Add(p.Objet);
				foreach ( MarshalByRefObject obj in lstTmp )
					Unregister ( obj );
                
			}
			catch ( Exception ) {}
		}


		
	}
}
