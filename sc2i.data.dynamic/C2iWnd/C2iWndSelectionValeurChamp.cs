using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using sc2i.common;
using sc2i.drawing;
using sc2i.formulaire;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.expression;
using System.ComponentModel;
using System.Text;


namespace sc2i.data.dynamic
{
    public enum ETypeRemplissageControl
    {
        Horizontale,
        Verticale
    }


    [WndName("Quick Select Field Value")]
    [Serializable]
    public class C2iWndSelectionValeurChamp : C2iWndComposantFenetre ,I2iSerializable
    {
        public const string c_strIdEvenementValueChanged = "WND_EVENT_ON_VALUE_CHANGED";

        private CChampCustom m_champCustom = null;
        // TESTDBKEYOK
        private int m_nButtonWidth = 100;
        private int m_nButtonHeight = 50;
        private int m_nMargeVertical = 10;
        private int m_nButtonHorizontalMargin = 10;
        private Color m_buttonColor = Color.LightGreen;
        private Color m_buttonSelectedColor = Color.Tomato;
        private Color m_buttonInvalidateColor = Color.LightGray;

        private List<string> m_listeLimitToValues = new List<string>();

        public C2iWndSelectionValeurChamp()
			: base()
		{
      
		}


        /// //////////////////////////////////////////////////
        public override bool CanBeUseOnType(Type tp)
        {
            if (tp == null)
                return false;
            return typeof(IElementAChamps).IsAssignableFrom(tp);
        }

        /// //////////////////////////////////////////////////
        public int ButtonHeight
        {
            get
            {
                return m_nButtonHeight;
            }
            set
            {
                m_nButtonHeight = value;
            }
        }

        /// //////////////////////////////////////////////////
        public int ButtonWidth
        {
            get
            {
                return m_nButtonWidth;
            }
            set
            {
                m_nButtonWidth = value;
            }
        }

        /// //////////////////////////////////////////////////
        public int ButtonVerticalMargin
        {
            get
            {
                return m_nMargeVertical;
            }
            set
            {
                m_nMargeVertical = value;
            }
        }

        /// //////////////////////////////////////////////////
        public int ButtonHorizontalMargin
        {
            get
            {
                return m_nButtonHorizontalMargin;
            }
            set
            {
                m_nButtonHorizontalMargin = value;
            }
        }

        /// //////////////////////////////////////////////////
        public Color ButtonColor
        {
            get
            {
                return m_buttonColor;
            }
            set
            {
                m_buttonColor = value;
            }
        }

        /// //////////////////////////////////////////////////
        public Color ButtonSelectedColor
        {
            get
            {
                return m_buttonSelectedColor;
            }
            set
            {
                m_buttonSelectedColor = value;
            }
        }

        /// //////////////////////////////////////////////////
        public Color ButtonInvalideColor
        {
            get
            {
                return m_buttonInvalidateColor;
            }
            set
            {
                m_buttonInvalidateColor = value;
            }
        }

        /// //////////////////////////////////////////////////
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        /// //////////////////////////////////////////////////
        [System.ComponentModel.Browsable(false)]
        public IEnumerable<string> LimitToValuesList
        {
            get
            {
                return m_listeLimitToValues.AsReadOnly();
            }
            set
            {
                m_listeLimitToValues = new List<string>();
                if (value != null)
                    m_listeLimitToValues.AddRange(value);
            }
        }

        /// //////////////////////////////////////////////////
        [System.ComponentModel.Description("Enter displayed value separated by semicolon, of empty for all values")]
        public string LimitToValues
        {
            get
            {
                StringBuilder bl = new StringBuilder();
                foreach (string strValue in LimitToValuesList)
                {
                    bl.Append(strValue);
                    bl.Append(";");
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 1, 1);
                return bl.ToString();
            }
            set
            {
                List<string> lstValues = new List<string>();
                if (value != null && value.Length > 0)
                {
                    string[] strVales = value.Split(';');
                    lstValues.AddRange(strVales);

                }
                m_listeLimitToValues = lstValues;
            }
        }


        /// //////////////////////////////////////////////////
        [System.ComponentModel.Editor(typeof(CProprieteChampCustomEditor), typeof(UITypeEditor))]
        public CChampCustom ChampCustom
        {
            get
            {
                return m_champCustom;
            }
            set
            {
                m_champCustom = value;
            }
        }

      
        /// //////////////////////////////////////////////////
        private int GetNumVersion()
        {
            //return 1;
            //return 2;//Ajout des couleurs de boutons
            //return 3;//Ajout des valeurs limitées
            return 4;//Passage de Ids Champ Custom à DbKey
            
        }

        /// //////////////////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;


            serializer.TraiteInt(ref m_nButtonHorizontalMargin);
            serializer.TraiteInt(ref m_nMargeVertical);
            serializer.TraiteInt(ref m_nButtonWidth);
            serializer.TraiteInt(ref m_nButtonHeight);

            if (nVersion < 2)
            {
                int nTmp = 0;
                serializer.TraiteInt(ref nTmp);
            }

            CDbKey key = null;
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    if (m_champCustom != null)
                        key = m_champCustom.DbKey;
                    serializer.TraiteDbKey(ref key);
                    break;
                case ModeSerialisation.Lecture:
                    if (nVersion < 4)
                        // TESTDBKEYOK
                        serializer.ReadDbKeyFromOldId(ref key, typeof(CChampCustom));
                    else
                        serializer.TraiteDbKey(ref key);
                    if (key != null)
                    {
                        CContexteDonnee ctx = (CContexteDonnee)serializer.GetObjetAttache(typeof(CContexteDonnee));
                        if (ctx == null)
                            ctx = CContexteDonneeSysteme.GetInstance();
                        m_champCustom = new CChampCustom(ctx);
                        if (!m_champCustom.ReadIfExists(key))
                            m_champCustom = null;
                    }
                    else
                        m_champCustom = null;
                    break;
            }
            if (nVersion >= 2)
            {
                int nTmp = m_buttonColor.ToArgb();
                serializer.TraiteInt(ref nTmp);
                m_buttonColor = Color.FromArgb(nTmp);

                nTmp = m_buttonInvalidateColor.ToArgb();
                serializer.TraiteInt(ref nTmp);
                m_buttonInvalidateColor = Color.FromArgb(nTmp);

                nTmp = m_buttonSelectedColor.ToArgb();
                serializer.TraiteInt(ref nTmp);
                m_buttonSelectedColor = Color.FromArgb(nTmp);
            }

            if (nVersion >= 3)
            {
                serializer.TraiteListString(m_listeLimitToValues);
            }
                

            return result;
        }


        //-----------------------------------
        public override void DrawInterieur(CContextDessinObjetGraphique ctx)
        {
            base.DrawInterieur(ctx);

            int nNbCols = ClientSize.Width / ButtonWidth;
            int nNbLignes = ClientSize.Height / ButtonHeight;

            Brush br = new SolidBrush(ButtonColor);
            for (int nLigne = 0; nLigne < nNbLignes; nLigne++)
                for (int nCol = 0; nCol < nNbCols; nCol++)
                {
                    int nX = nCol * ButtonWidth + ButtonHorizontalMargin;
                    int nY = nLigne * ButtonHeight + ButtonVerticalMargin;
                    Rectangle rct = new Rectangle(nX, nY,
                        ButtonWidth - ButtonHorizontalMargin * 2,
                        ButtonHeight - ButtonVerticalMargin * 2);
                    ctx.Graphic.FillRectangle(br, rct);
                    ctx.Graphic.DrawRectangle(Pens.Black, rct);
                }
            br.Dispose();
        }


        /// //////////////////////////////////////////////////
		public override void OnDesignSelect(
			Type typeEdite, 
			object objetEdite,
			sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
			CProprieteChampCustomEditor.SetTypeElementAChamp ( GetObjetPourAnalyseThis(typeEdite).TypeAnalyse );
		}

        /// //////////////////////////////////////////////////
        public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
        {
            List<CDescriptionEvenementParFormule> listEvents = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
            listEvents.Add(new CDescriptionEvenementParFormule(
                c_strIdEvenementValueChanged,
                "ValueChanged",
                "Event occurs when control value has change"));

            return listEvents.ToArray();
        }

        public override CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>(base.GetProprietesInstance());
            lst.Add(new CDefinitionProprieteDynamiqueDeportee(
                "SelectedValue", "SelectedValue",
                new CTypeResultatExpression(typeof(object), false),
                true,
                true,
                ""));

            return lst.ToArray();
        }




       
        

    }
}
