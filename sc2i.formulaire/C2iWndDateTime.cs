using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using System.Drawing;
using System.IO;
using System.Drawing.Design;
using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using sc2i.formulaire;
using sc2i.formulaire.datagrid;
using sc2i.formulaire.datagrid.Filters;


namespace sc2i.formulaire
{

    [WndName("Date Time")]
    [Serializable]
    public class C2iWndDateTime : C2iWndComposantFenetre, IWndIncluableDansDataGrid
    {
        public const string c_strIdEvenementValueChanged = "VALCHG";

        public enum TypeAlignement
        {
            Gauche = 0,
            Droite = 1,
            Centre = 2
        }


        private CDefinitionProprieteDynamique m_property;
        private TypeAlignement m_alignement = TypeAlignement.Gauche;

        private bool m_bAutoSetValue = false;
       
        
        public C2iWndDateTime()
        {
          //  Size = new Size(Size.Width, Size.Height);
        }

        /// //////////////////////////////////////////////////
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

      

        private int GetNumVersion()
        {

            return 1;
            //1 : AutosetValue

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

            

            int nAlignement = (int)m_alignement;
            serializer.TraiteInt(ref nAlignement);
            m_alignement = (TypeAlignement)nAlignement;

            if (nVersion >= 1)
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

            DrawTextBox(rect, g);

        }

        /// //////////////////////////////////////////////////
        private void DrawTextBox(Rectangle rect, Graphics g)
        {

            g.FillRectangle(new SolidBrush(BackColor), rect);
            DrawCadre3D(rect, true, g);
            rect.Width -= 4;
            rect.Height -= 4;
            rect.Offset(2, 2);
            DrawNomChamp(rect, g);
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
            CDefinitionProprieteDynamiqueEditor.SetTypeAutorises(new Type[] { typeof(DateTime),typeof(DateTime?),typeof(CDateTimeEx) });
            CDefinitionProprieteDynamiqueEditor.SetObjetPourSousProprietes(GetObjetPourAnalyseThis(typeEdite));


            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
        }



        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "Value", "Value",
                new CTypeResultatExpression(typeof(DateTime?), false),
                false,
                false,
                ""));

         

            

            return lst.ToArray();
        }


        #region IElementAEvenementParFormule Membres

        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
		{
			List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>();
			lst.AddRange(base.GetDescriptionsEvenements());

			lst.Add ( new CDescriptionEvenementParFormule ( c_strIdEvenementValueChanged,
				"Value changed", I.T("Occurs when the date value has changed|30001") ));
			return lst.ToArray();
        }



#endregion


        #region IWndIncluableDansDataGrid Membres

        public Type ValueTypeForGrid
        {
            get { return typeof(DateTime); }
        }

        public string ConvertObjectValueToStringForGrid(object val)
        {
            if (val is DateTime)
            {
                return ((DateTime)val).ToShortDateString();
            }
            return "";
        }

        public object GetObjectValueForGrid(object element)
        {
            if (m_property != null)
            {
                CResultAErreur result = CInterpreteurProprieteDynamique.GetValue(element, m_property);
                if (result && result.Data is DateTime)
                {
                    return (DateTime)result.Data;
                }
            }
            return null;
        }

        public IEnumerable<CGridFilterForWndDataGrid> GetPossibleFilters()
        {
            return CGridFilterDateComparison.GetFiltresDate();
        }

        #endregion
    }


   
		
}
