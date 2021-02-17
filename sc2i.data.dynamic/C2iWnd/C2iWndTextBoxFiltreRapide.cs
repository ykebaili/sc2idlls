using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using System.Drawing;
using System.IO;

#if PDA
#else
using System.Drawing.Design;
#endif

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;

using sc2i.formulaire;

using sc2i.data;
using sc2i.formulaire.datagrid;
using sc2i.formulaire.datagrid.Filters;

namespace sc2i.data.dynamic
{
    
    [WndName("Select entity")]
    [AWndIcone("ico_txtBox")]
    [Serializable]
    public class C2iWndTextBoxFiltreRapide : C2iWndComposantFenetre, IWndIncluableDansDataGrid
    {


        public const string c_strIdEvenementValueChanged = "SELCHG";

        public enum TypeAlignement
        {
            Gauche = 0,
            Droite = 1,
            Centre = 2
        }

        private bool m_bAutoSetValue = false;
        
        private CFiltreDynamique m_filter;

        private TypeAlignement m_alignement = TypeAlignement.Gauche;

        private CDefinitionProprieteDynamique m_property;

        private bool m_bUseCombo = false;

        public C2iWndTextBoxFiltreRapide()
		{
			
		}

        /// //////////////////////////////////////////////////
        public bool AutoSetValue
        {
            get
            {
                return m_bAutoSetValue;
            }
            set
            {
                m_bAutoSetValue = value;
            }
        }


        [Editor(typeof(CDefinitionFiltreDynamiqueEditor), typeof(UITypeEditor))]
        public CFiltreDynamique Filter
        {
            get
            {
                Type typeElements = null;
                if (Property != null)
                    typeElements = Property.TypeDonnee.TypeDotNetNatif;
                if ( m_filter    == null )
                    m_filter = new CFiltreDynamique();
                if ( typeElements != null )
                    m_filter.TypeElements = typeElements;
                return m_filter;
            }

            set
            {
                m_filter = value;
            }
        }

        public bool UseCombo
        {
            get
            {
                return m_bUseCombo;
            }
            set
            {
                m_bUseCombo = value;
            }
        }

        [Editor(typeof(CDefinitionProprieteDynamiqueEditor), typeof(UITypeEditor))]
        public CDefinitionProprieteDynamique Property
        {
            get
            {
                return m_property;
            }

            set
            {
                m_property = value;
            }
        }


        public TypeAlignement Alignement
        {
            get
            {
                return m_alignement;
            }
            set
            {
                m_alignement = value;
            }
        }


        private int GetNumVersion()
        {

            return 2;
            //1 : ajout de UseCombo
            //2 : AutoSetValue

        }

        public override bool CanBeUseOnType(Type tp)
        {
            return true;
        }

        /// ///////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;



            result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_property);
            if (!result)
                return result;

            result = serializer.TraiteObject<CFiltreDynamique>(ref m_filter);
            if (!result)
                return result;
                   
            int nAlignement = (int)m_alignement;
            serializer.TraiteInt(ref nAlignement);
            m_alignement = (TypeAlignement)nAlignement;

            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bUseCombo);
            if (nVersion >= 2)
                serializer.TraiteBool(ref m_bAutoSetValue);
            return result;
        }


#if PDA
#else
        /// //////////////////////////////////////////////////
        public override void DrawInterieur(CContextDessinObjetGraphique ctx)
        {
            Graphics g = ctx.Graphic;
            Rectangle rect = new Rectangle(new Point(0, 0), Size);

            DrawTextBoxFiltre(rect, g);

        }

        /// //////////////////////////////////////////////////
        private void DrawTextBoxFiltre(Rectangle rect, Graphics g)
        {

           g.FillRectangle ( new SolidBrush(BackColor), rect );
			DrawCadre3D ( rect, true, g );
            Pen pen = new Pen(ForeColor);

            DrawNomChamp(rect, g);

			/*Rectangle rectBouton = new Rectangle(rect.Right-19, rect.Top+2,	17, 18);
            Rectangle rectBouton2 = new Rectangle(rect.Right-38, rect.Top+2,17, 18);
		
			//g.FillRectangle ( SystemBrushes.Control, rectBouton );
            g.DrawRectangle(pen,rectBouton);
			//DrawCadre3D ( rectBouton, false, g );

            g.DrawRectangle(pen,rectBouton2);

            */

		/*	//Dessin de la flêche
			for ( int n = 0; n < 4; n++ )
				g.DrawLine(Pens.Black, new Point ( rectBouton.Left+5+n, rectBouton.Top+7+n),
					new Point ( rectBouton.Left+11-n, rectBouton.Top+7+n ) );
			g.DrawLine ( Pens.Black, rectBouton.Left+8, rectBouton.Top+9, rectBouton.Left+8, rectBouton.Top+10 );
			rect.Width -= 21;
			rect.Height-=4;
			rect.Offset(2,2);
			DrawNomChamp ( rect, g );
		}*/
        }



        /// //////////////////////////////////////////////////
        private void DrawNomChamp(Rectangle rect, Graphics g)
        {
            if (Font == null)
                return;
            Region oldClip = g.Clip;
            g.Clip = new Region(rect);
            string strNom = Name;
            SizeF sz = g.MeasureString(strNom, Font);
            Point pt = new Point(rect.Left, rect.Top);
            switch (Alignement)
            {
                case TypeAlignement.Gauche:
                    pt.X = rect.Left;
                    break;
                case TypeAlignement.Droite:
                    pt.X = rect.Right - (int)sz.Width;
                    break;
                case TypeAlignement.Centre:
                    pt.X = rect.Left + rect.Width / 2 - (int)sz.Width / 2;
                    break;
            }
            g.DrawString(strNom, Font, new SolidBrush(ForeColor), pt);



        }
#endif




        public override void OnDesignSelect(Type typeEdite, object objetEdite, IFournisseurProprietesDynamiques fournisseurProprietes)
        {


            CDefinitionProprieteDynamiqueEditor.SetBAffectable(true);
            CDefinitionProprieteDynamiqueEditor.SetFournisseur(fournisseurProprietes);
            CDefinitionProprieteDynamiqueEditor.SetTypeAutorises(new Type[] { typeof(CObjetDonnee) });
            CDefinitionProprieteDynamiqueEditor.SetObjetPourSousProprietes(GetObjetPourAnalyseThis(typeEdite).TypeAnalyse);

            CElementAVariablesDynamiques element = new CElementAVariablesDynamiques();
            CVariableDynamiqueStatique var = new CVariableDynamiqueStatique(element);
            var.Nom = "EditedElement";
            var.IdVariable = "0";//Compatiblité avant DbKey
            var.SetTypeDonnee(new CTypeResultatExpression(GetObjetPourAnalyseThis(typeEdite).TypeAnalyse, false));
            element.AddVariable(var);
            CDefinitionFiltreDynamiqueEditor.SetElementAVariablesExternes(element);

            I2iObjetGraphique parent = this;
            while ( parent.Parent != null )
                parent = parent.Parent;

            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
        }




        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {

            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());

            Type typeObjet;

            if (m_filter != null)
            {
                typeObjet = m_filter.TypeElements;
            }
            else
                typeObjet = typeof(CObjetDonnee);



            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "SelectedElement", "ElementSelectionne",
                new CTypeResultatExpression(typeObjet, false),
                true,
                false,
                ""));

            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "DescriptionElement", "DescriptionElement",
                new CTypeResultatExpression(typeof(string), false),
                false,
                false,
                ""));
            if (Filter != null && Filter.TypeElements != null)
            {
                lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                    "Source", "Source",
                    new CTypeResultatExpression(Filter.TypeElements, true),
                    true,
                    false,
                    ""));
            }
            return lst.ToArray();
        }


        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>();
            lst.AddRange(base.GetDescriptionsEvenements());



            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementValueChanged,
                "ElementChanged", I.T("Occurs when the selected entity has changed|30001")));
            return lst.ToArray();
        }




        #region IWndIncluableDansDataGrid Membres

        public Type ValueTypeForGrid
        {
            get
            {
                if (m_filter != null)
                    return m_filter.TypeElements;
                return typeof(string);
            }
        }

        public string ConvertObjectValueToStringForGrid(object valeur)
        {
            if (valeur is CObjetDonnee)
            {
                DescriptionFieldAttribute.GetDescription((CObjetDonnee)valeur);
            }
            return "";
        }

        public object GetObjectValueForGrid(object element)
        {
            if (m_property != null)
            {
                CResultAErreur result = CInterpreteurProprieteDynamique.GetValue(element, m_property);
                return result.Data;
            }
            return null;
        }

        public IEnumerable<CGridFilterForWndDataGrid> GetPossibleFilters()
        {
            return CGridFilterTextComparison.GetFiltresTexte();
        }

       
        #endregion
    }
}
