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

    [WndName("Check Box")]
     [Serializable]
    public class C2iWndCheckBox : C2iWndComposantFenetre, IWndIncluableDansDataGrid
    {
        public const string c_strIdEvenementCheckChanged = "CHKCHG";

        private CDefinitionProprieteDynamique m_property;

        private string m_strText="";

        private bool m_bAutoSetValue = false;
       
        public C2iWndCheckBox()
        {
            Size = new Size(Size.Width, 22);
        }

       
        public string Text
        {
            get
            {
                return m_strText;
            }

            set
            {
                m_strText = value;
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

       

        private int GetNumVersion()
        {

            return 2;
            //2 : AutoSetValue

        }

        public override bool CanBeUseOnType(Type tp)
        {
            return true;
        }

        /// /////////////////
        //////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;



            result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_property);
            if (!result)
                return result;


            if (nVersion >= 1)
            {
               
               serializer.TraiteString(ref m_strText);
                if (!result)
                    return result;
            }

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

            DrawCheckBox(rect, g);

        }

        /// //////////////////////////////////////////////////
        private void DrawCheckBox(Rectangle rect, Graphics g)
        {
            g.FillRectangle(new SolidBrush(BackColor), rect);
            Rectangle checkRect = new Rectangle(rect.Left, rect.Top + rect.Height / 2 - 6, 13, 13);
            g.FillRectangle(SystemBrushes.Window, checkRect);
            DrawCadre3D(checkRect, true, g);
            rect.Width -= 21;
            rect.Height -= 4;
            rect.Offset(19, 2);
            DrawNomChamp(rect, g);
        }


        /// //////////////////////////////////////////////////
        private void DrawNomChamp(Rectangle rect, Graphics g)
        {
            if (Font == null)
                return;
            Region oldClip = g.Clip;
            g.Clip = new Region(rect);
            string strNom = Text;
            SizeF sz = g.MeasureString(strNom, Font);
            Point pt = new Point(rect.Left, rect.Top);
            pt.X = rect.Left - 2;
            pt.Y = rect.Top + 2;
            g.DrawString(strNom, Font, new SolidBrush(ForeColor), pt);



        }
#endif



        public override void OnDesignSelect(Type typeEdite, object objetEdite, IFournisseurProprietesDynamiques fournisseurProprietes)
        {


            CDefinitionProprieteDynamiqueEditor.SetBAffectable(true);
            CDefinitionProprieteDynamiqueEditor.SetFournisseur(fournisseurProprietes);
            CDefinitionProprieteDynamiqueEditor.SetTypeAutorises(new Type[] { typeof(bool) });
            CDefinitionProprieteDynamiqueEditor.SetObjetPourSousProprietes(GetObjetPourAnalyseThis(typeEdite));


            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
        }

        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "Checked", "Checked",
                new CTypeResultatExpression(typeof(bool), false),
                false,
                false,
                ""));
            return lst.ToArray();
        }

        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
            lst.Add(new CDescriptionEvenementParFormule(c_strIdEvenementCheckChanged, "CheckChanged", I.T("Occurs when check state of the control changes|20004")));
            return lst.ToArray();
        }







        #region IWndIncluableDansDataGrid Membres

        public Type ValueTypeForGrid
        {
            get { return typeof(bool); }
        }

        //-----------------------------------------------------------------------------
        public string ConvertObjectValueToStringForGrid(object val)
        {
            if ( val is bool )
            {
                    return ((bool)val) ? I.T("Yes|20024") : I.T("No|20025");
            }
            return "";
        }

        //-----------------------------------------------------------------------------
        public object GetObjectValueForGrid(object element)
        {
            if (m_property != null)
            {
                CResultAErreur result = CInterpreteurProprieteDynamique.GetValue(element, m_property);
                if (result && result.Data is bool)
                {
                    return result.Data;
                }
            }
            return null;
        }

        //-------------------------------------------------------------------------
        public IEnumerable<CGridFilterForWndDataGrid> GetPossibleFilters()
        {
            return CGridFilterChecked.GetFiltresBool();
        }


        #endregion
    }
}