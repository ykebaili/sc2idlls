using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Design;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using sc2i.formulaire;
using System.Collections.Generic;
using System.ComponentModel;

namespace sc2i.data.dynamic
{

	/// <summary>
	/// Description résumée de C2iFenetre.
	/// </summary>
    [Serializable]
    [WndName("Cube view")]
    public class C2iWndDonneePrecalculee : C2iWndComposantFenetre
    {
        private CParametreVisuDonneePrecalculee m_parametre = null;

        /// ///////////////////////////////////////
        public C2iWndDonneePrecalculee()
        {

            LockMode = ELockMode.Independant;
        }

        /// ///////////////////////////////////////
        public override bool CanBeUseOnType(Type tp)
        {
            return true;
        }

        /// ///////////////////////////////////////
        [System.ComponentModel.Browsable(false)]
        public override bool AcceptChilds
        {
            get
            {
                return false;
            }
        }

        /// ///////////////////////////////////////
        [System.ComponentModel.Editor(typeof(CEditeurParametreVisuDonneePrecalculee), typeof(UITypeEditor))]
        public CParametreVisuDonneePrecalculee Parametre
        {
            get
            {
                return m_parametre;
            }
            set
            {
                m_parametre = value;
            }
        }




        /// ///////////////////////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            Graphics g = ctx.Graphic;
			Brush b = new SolidBrush(BackColor);
			Rectangle rect = new Rectangle(Position, Size);
            g.DrawImage(global::sc2i.data.dynamic.Resource.gros_cube, rect);
			
			b.Dispose();

			base.MyDraw(ctx);
        }

        /// ///////////////////////////////////////
        public override void DrawInterieur(CContextDessinObjetGraphique ctx)
        {
        }


        /// ///////////////////////////////////////
        private int GetNumVersion()
        {
            return 0;
            //0 ; Version initiale

        }


        /// ///////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            result = serializer.TraiteObject<CParametreVisuDonneePrecalculee>(ref m_parametre);
            return result;
        }

        //-------------------------------------------------------
        public override void OnDesignSelect(
            Type typeEdite,
            object elementEdite,
            IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, elementEdite, fournisseurProprietes);
            /*m_typeSource = typeEdite;
            CProprieteAffectationsProprietesEditor.SetTypeSource(m_typeSource);
            if (SourceFormula != null)
            {
                CProprieteAffectationsProprietesEditor.SetTypeElementAffecte(SourceFormula.TypeDonnee.TypeDotNetNatif);
                CProprieteFiltreDynamiqueEditor.SetTypeElement(SourceFormula.TypeDonnee.TypeDotNetNatif);
                CSelecteurDeFormEdition.SetTypeElement(SourceFormula.TypeDonnee.TypeDotNetNatif);
                CProprieteExpressionEditor.TypeElements = SourceFormula.TypeDonnee.TypeDotNetNatif;
            }
            CProprieteAffectationsProprietesEditor.FournisseurProprietes = fournisseurProprietes;

            CColumnsPropertyEditor.ListeEditee = this;*/



        }


        //-------------------------------------------------------
        public override void OnDesignCreate(Type typeEdite)
        {

        }






    }

	//-----------------------------------------------------
	public interface IEditeurParametreVisuDonneePrecalculee
	{
        CParametreVisuDonneePrecalculee EditeParametre(CParametreVisuDonneePrecalculee parametre);
	}
	/// <summary>
	/// Sélectionneur de IReferenceFormEdition
	/// </summary>
	public class CEditeurParametreVisuDonneePrecalculee : UITypeEditor
	{
		private static Type m_typeEditeur;


		public static void SetTypeEditeur(Type typeEditeur)
		{
			m_typeEditeur = typeEditeur;

		}

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
            IEditeurParametreVisuDonneePrecalculee ed = (IEditeurParametreVisuDonneePrecalculee)Activator.CreateInstance(m_typeEditeur);
            return ed.EditeParametre(value as CParametreVisuDonneePrecalculee);
		}

		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if (m_typeEditeur != null)
				return UITypeEditorEditStyle.Modal;
			else
				return UITypeEditorEditStyle.None;
		}
	}
}