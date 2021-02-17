using System;
using System.Drawing;
using System.Collections;
using System.Drawing.Design;


using sc2i.common;
using sc2i.formulaire;
using System.Collections.Generic;
using sc2i.expression;
using sc2i.formulaire.datagrid;
using System.ComponentModel;

namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iWndLink.
	/// </summary>
	[WndName("Link")]
    public class C2iWndLink : C2iWndLabel
	{
        public enum C2iLinkBehavior
	    {
            SystemDefault = 0,
            AlwaysUnderline = 1,
            HoverUnderline = 2,
            NeverUnderline = 3,
	    }

		public class CListeGroupesVoyants : I2iSerializable
		{
			private ArrayList m_listeGroupesVisible = new ArrayList();


			public CListeGroupesVoyants()
			{
			}

			public ArrayList ListeIdsGroupesVoyants
			{
				get
				{
					return m_listeGroupesVisible;
				}
			}

 			private int GetNumVersion()
			{
                return 0;
			}

			public CResultAErreur Serialize ( C2iSerializer serializer )
			{
				int nVersion = GetNumVersion();
				CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
				if ( !result )
					return result;
				int nNb = m_listeGroupesVisible.Count;
				serializer.TraiteInt ( ref nNb );
				switch ( serializer.Mode )
				{
					case ModeSerialisation.Ecriture :
						foreach ( int nGroupe in m_listeGroupesVisible )
						{
							int nTmp = nGroupe;
							serializer.TraiteInt ( ref nTmp );
						}
						break;
					case ModeSerialisation.Lecture:
						m_listeGroupesVisible.Clear();
						for ( int nGroupe = 0; nGroupe < nNb; nGroupe++ )
						{
							int nTmp = 0;
							serializer.TraiteInt ( ref nTmp );
							m_listeGroupesVisible.Add ( nTmp );
						}
						break;
				}
				return result;
			}
		}

        private CListeGroupesVoyants m_listeGroupes = new CListeGroupesVoyants();
        private CActionSur2iLink m_action = null;
        private C2iLinkBehavior m_comportement = C2iLinkBehavior.AlwaysUnderline;


		public C2iWndLink()
		{
			Font = new Font ( "Arial", 8);
			ForeColor = Color.Blue;
            LockMode = ELockMode.Independant;
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

        public C2iLinkBehavior LinkBehavior
        {
            get
            {
                return m_comportement;
            }
            set
            {
                m_comportement = value;
            }
        }


		/// ////////////////////////////////////
		private int GetNumVersion()
		{
			//return 0;
            //return 1; // ajout du comportement du lien LinkBehavior
            return 2; //La couleur du lien n'est plus bleue par défaut
        }

		/// ////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( result )
				result = base.MySerialize(serializer);
			if ( !result )
				return result;
			I2iSerializable objet = m_action;
			result = serializer.TraiteObject ( ref objet );
			m_action = (CActionSur2iLink)objet;
			if ( !result )
				return result;

			objet = m_listeGroupes;
			result = serializer.TraiteObject ( ref objet );
			m_listeGroupes = (CListeGroupesVoyants)objet;
			if ( !result )
				return result;

            if (nVersion >= 1)
            {
                int nTemp = (int)m_comportement;
                serializer.TraiteInt(ref nTemp);
                m_comportement = (C2iLinkBehavior)nTemp;
            }
            if (serializer.Mode == ModeSerialisation.Lecture && nVersion >= 2)
                ForeColor = Color.Blue;
			
			return result;
		}

		/////////////////////////////////////////////////////////////////
        [TypeConverter(typeof(CActionSur2iLinkConvertor))]
        [System.ComponentModel.Editor(typeof(CActionSur2iLinkEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(null)]
        public CActionSur2iLink Action
		{
			get
			{
				return m_action;
			}
			set
			{
				m_action = value;
			}
		}

		/////////////////////////////////////////////////////////////////
		public CListeGroupesVoyants ListeGroupes
		{
			get
			{
				return m_listeGroupes;
			}
			set
			{
				m_listeGroupes = value;
			}
		}

        protected override void DrawString(Graphics g, StringFormat format)
        {
            Brush br = new SolidBrush(ForeColor);
            g.DrawString(Text, new Font(Font, FontStyle.Underline) , br, ClientRect, format);
            br.Dispose();
        }


		/////////////////////////////////////////////////////////////////
		public override void OnDesignSelect(
			Type typeEdite, 
			object objetEdite,
			sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            CActionSur2iLinkEditor.SetObjet(GetObjetPourAnalyseFormule(typeEdite));
		}

		// Evenements déportées
        public const string c_strIdEvenementLinkClicked = "LNKCLICK";

        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>();
            lst.AddRange(base.GetDescriptionsEvenements());

            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementLinkClicked,
                "Link clicked", I.T("Occurs when the Link is clicked|10000")));
            return lst.ToArray();
        }
    }

	/// ///////////////////////////////////////////
	public interface IEditeurActionSur2iLink
	{
		/// <summary>
		/// Edite une action. Si typeObjetForce est null, le type par défaut de l'éditeur est 
		/// utilisé
		/// </summary>
		/// <param name="action"></param>
		/// <param name="typeObjetForce"></param>
		void EditeAction ( ref CActionSur2iLink action );
        CObjetPourSousProprietes ObjetEdite{get;set;}
		
	}

    public class CActionSur2iLinkConvertor : CGenericObjectConverter<CActionSur2iLink>
    {
        public override string GetString(CActionSur2iLink value)
        {
            if (value != null)
                return "Action";
            return "";
        }
    }

	public class CActionSur2iLinkEditor : UITypeEditor
	{
		private static IEditeurActionSur2iLink m_editeur = null;

        private static CObjetPourSousProprietes m_objetPourSousProprietes = null;
		private static Type m_typeEditeur = null;

		/// ///////////////////////////////////////////
		public static void SetEditeur ( IEditeurActionSur2iLink editeur )
		{
			m_editeur = editeur;
		}

		/// ///////////////////////////////////////////
		public static void SetTypeEditeur(Type tp)
		{
			m_typeEditeur = tp;
		}

		/// ///////////////////////////////////////////
		public static void SetObjet(CObjetPourSousProprietes objetPourSousProprietes)
		{
            m_objetPourSousProprietes = objetPourSousProprietes;
		}

		/// ///////////////////////////////////////////
		public override object EditValue ( System.ComponentModel.ITypeDescriptorContext context,
			System.IServiceProvider provider,
			object value )
		{
			IEditeurActionSur2iLink editeur = m_editeur;
			if (editeur == null)
			{
				editeur = (IEditeurActionSur2iLink)Activator.CreateInstance(m_typeEditeur);
				editeur.ObjetEdite = m_objetPourSousProprietes;
			}
			CActionSur2iLink action = (CActionSur2iLink)value;
			editeur.EditeAction ( ref action );
			if (m_editeur == null && editeur is IDisposable)
				((IDisposable)editeur).Dispose();
			return action;
		}

		/*/// ///////////////////////////////////////////
		public static void EditeAction(ref CActionSur2iLink action, Type typeObjetEdite)
		{
			IEditeurActionSur2iLink editeur = m_editeur;
			if (editeur == null)
			{
				editeur = (IEditeurActionSur2iLink)Activator.CreateInstance(m_typeEditeur);
			}
			Type oldType = editeur.TypeEdite;
			try
			{
				editeur.TypeEdite = typeObjetEdite;
				editeur.EditeAction(ref action);
			}
			catch
			{
			}
			editeur.TypeEdite = oldType;
			if (m_editeur == null && editeur is IDisposable)
				((IDisposable)editeur).Dispose();
		}*/

        /// ///////////////////////////////////////////
        public static void EditeAction(ref CActionSur2iLink action, CObjetPourSousProprietes objet)
        {
            IEditeurActionSur2iLink editeur = m_editeur;
            if (editeur == null)
            {
                editeur = (IEditeurActionSur2iLink)Activator.CreateInstance(m_typeEditeur);
            }
            CObjetPourSousProprietes oldObjet = editeur.ObjetEdite;
            try
            {
                editeur.ObjetEdite = objet;
                editeur.EditeAction(ref action);
            }
            catch
            {
            }
            editeur.ObjetEdite = oldObjet;
            if (m_editeur == null && editeur is IDisposable)
                ((IDisposable)editeur).Dispose();
        }
			

		/// ///////////////////////////////////////////
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
