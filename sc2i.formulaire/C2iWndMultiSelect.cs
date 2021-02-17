using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Drawing.Design;
using System.Data;

using sc2i.common;
using sc2i.expression;
using sc2i.drawing;
using System.ComponentModel;




namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de C2iPanel.
	/// </summary>
	[WndName("MultiSelect")]
	[Serializable]
	public class C2iWndMultiSelect : C2iWndComposantFenetre
	{
        //------------------------------------------------------------
        public class CColonneMultiSelect : I2iSerializable
        {
            private string m_strNom = "";
            private C2iExpression m_formule = null;
            private int m_nLargeur = 150;

            public CColonneMultiSelect()
            {
            }

            public string Nom
            {
                get
                {
                    return m_strNom;
                }
                set
                {
                    m_strNom = value;
                }
            }

            public C2iExpression Formule
            {
                get
                {
                    return m_formule;
                }
                set
                {
                    m_formule = value;
                }
            }

            public int Largeur
            {
                get
                {
                    return m_nLargeur;
                }
                set
                {
                    m_nLargeur = value;
                }
            }

            private int GetNumVersion()
            {
                return 0;
            }

            public CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion(ref nVersion);
                if (!result)
                    return result;
                serializer.TraiteString(ref m_strNom);
                result = serializer.TraiteObject<C2iExpression>(ref m_formule);
                if (!result)
                    return result;
                serializer.TraiteInt(ref m_nLargeur);
                return result;
            }
        }

        //------------------------------------------------------------
        public class CConfigMultiSelect : I2iSerializable
        {
            private C2iExpression m_formuleSelectedValue = null;
            private List<CColonneMultiSelect> m_listeColonnes = new List<CColonneMultiSelect>();

            public CConfigMultiSelect()
            {
            }

            public C2iExpression FormuleSelectedValue
            {
                get
                {
                    return m_formuleSelectedValue;
                }
                set
                {
                    m_formuleSelectedValue = value;
                }
            }

            public IEnumerable<CColonneMultiSelect> Colonnes
            {
                get
                {
                    return m_listeColonnes.AsReadOnly();
                }
            }

            public void AddColonne(CColonneMultiSelect col)
            {
                m_listeColonnes.Add(col);
            }

            public void RemoveColonne(CColonneMultiSelect col)
            {
                m_listeColonnes.Remove(col);
            }

            public void ClearColonnes()
            {
                m_listeColonnes.Clear();
            }

            private int GetNumVersion()
            {
                return 0;
            }

            public CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion(ref nVersion);
                if (!result)
                    return result;
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleSelectedValue);
                if (result)
                    result = serializer.TraiteListe<CColonneMultiSelect>(m_listeColonnes);
                if (!result)
                    return result;
                return result;
            }
        }




		private C2iExpression m_formuleSourceDonnees;

        public static string c_strIdEvenementCheckChanged = "OnChanged";

        private CConfigMultiSelect m_configMultiSelect = new CConfigMultiSelect();
		

		public C2iWndMultiSelect()
            :base()
		{
            LockMode = ELockMode.Independant;
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		//----------------------------------------------
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		public C2iExpression SourceFormula
		{
			get
			{
				return m_formuleSourceDonnees;
			}
			set
			{
				m_formuleSourceDonnees = value;
			}
		}

        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
            Type tp = typeof(int);
            C2iExpression formule = Setup.FormuleSelectedValue;
            if ( formule != null )
                tp = formule.TypeDonnee.TypeDotNetNatif;
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "CheckedItems",
                "Checked_Items",
                new CTypeResultatExpression(tp, true),
                false,
                false,
                ""));


            return lst.ToArray();
                
        }

        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementCheckChanged, "CheckChanged",
                I.T("Occurs when the item check changed|20031")));
            return lst.ToArray();
        }

		//----------------------------------------------
        [System.ComponentModel.Editor(typeof(CConfigMultiSelectEditor), typeof(UITypeEditor))]
        public CConfigMultiSelect Setup
        {
            get
            {
                return m_configMultiSelect;
            }
            set
            {
                if (value != null)
                    m_configMultiSelect = value;
            }
        }

		/// ///////////////////////
		[System.ComponentModel.Browsable(false)]
		public override bool AcceptChilds
		{
			get
			{
				return false;
			}
		}

		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle ( Position , Size );
			//rect = contexte.ConvertToAbsolute(rect);
			g.FillRectangle(b, rect);
			b.Dispose();
			DrawCadre ( g );
			base.MyDraw ( ctx );
		}

		

		
	
		
		/// /////////////////////////////////////////////////
		protected void DrawCadre ( Graphics g )
		{
			Rectangle rect = new Rectangle ( Position, Size);
			Brush br;
			br = new SolidBrush(BackColor);
			g.FillRectangle(br, Position.X , Position.Y , ClientSize.Width , ClientSize.Height);
			Pen pen = new Pen ( ForeColor );
            g.DrawRectangle(pen, rect);
			br.Dispose();
			pen.Dispose();
		}

		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

            result = serializer.TraiteObject<C2iExpression>(ref m_formuleSourceDonnees);
            if (!result)
                return result;
            result = serializer.TraiteObject<CConfigMultiSelect>(ref m_configMultiSelect);
            if (!result)
                return result;

			return result;
		}

		/// //////////////////////////////////////////////////////////////////
		public IEnumerable GetListeSource(CContexteEvaluationExpression contexte)
		{
			if (m_formuleSourceDonnees != null)
			{
				CResultAErreur result = m_formuleSourceDonnees.Eval(contexte);
				if (result)
				{
                    if (result.Data is IEnumerable)
					{
                        return (IEnumerable)result.Data;
					}
				}
			}
			return null;
		}

		public override void OnDesignSelect(
			Type typeEdite, 
			object objetEdite,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            Type tpObjets = null;
            if ( m_formuleSourceDonnees != null )
                tpObjets = m_formuleSourceDonnees.TypeDonnee.TypeDotNetNatif;
            CConfigMultiSelectEditor.SetTypeElements(tpObjets);
		}


	}

    //---------------------------------------------------------
    public interface IEditeurConfigMultiSelect
    {
        C2iWndMultiSelect.CConfigMultiSelect EditeConfig(
            Type typeElements,
            C2iWndMultiSelect.CConfigMultiSelect config);
    }

    /// <summary>
    /// Description résumée de CProprieteChampCustomEditor.
    /// </summary>
    public class CConfigMultiSelectEditor : UITypeEditor
    {
        private static IEditeurConfigMultiSelect m_editeur = null;
        private static Type m_typeEditeur = null;

        private static Type m_typeElements = typeof(string);

        private static C2iWndListe m_listeEditee = null;

        /// ///////////////////////////////////////////
        public CConfigMultiSelectEditor()
        {
        }

        /// ///////////////////////////////////////////
        public static void SetEditeur(IEditeurConfigMultiSelect editeur)
        {
            m_editeur = editeur;
        }

        /// ///////////////////////////////////////////
        public static void SetTypeEditeur(Type tp)
        {
            m_typeEditeur = tp;
        }

        /// ///////////////////////////////////////////
        public static void SetTypeElements(Type tp)
        {
            m_typeElements = tp;
        }

        /// ///////////////////////////////////////////
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,
            System.IServiceProvider provider,
            object value)
        {
            IEditeurConfigMultiSelect editeur = m_editeur;
            if (editeur == null && m_typeEditeur != null)
            {
                editeur = (IEditeurConfigMultiSelect)Activator.CreateInstance(m_typeEditeur);
            }
            object retour = editeur.EditeConfig(m_typeElements, value as C2iWndMultiSelect.CConfigMultiSelect);
            if (m_editeur == null && editeur is IDisposable)
                ((IDisposable)editeur).Dispose();
            return retour;
        }

        /// ///////////////////////////////////////////
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (m_editeur == null && m_typeEditeur == null)
                return UITypeEditorEditStyle.None;
            return UITypeEditorEditStyle.Modal;
        }


    }
}
