using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.expression;
using sc2i.win32.common;
using sc2i.common;
using System.Collections.Generic;

namespace sc2i.win32.expression
{
	/// <summary>
	/// Permet de sélectionner une couleur par la boite de dialogue standard des couleurs,
    /// ainsi qu'une formule qui retourne une couleur
	/// </summary>
	public class CControlSelectColorByFormule : System.Windows.Forms.UserControl, IControlALockEdition
	{
        private C2iColorSelect m_colorSelect;
        private CTextBoxZoomFormule m_txtFormuleCouleur;
        private CExtModeEdition m_extModeEdition;
        private CToolTipTraductible m_toolTipTraductible;
        private Label label1;
        private IContainer components;

        public CControlSelectColorByFormule()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_colorSelect = new sc2i.win32.common.C2iColorSelect();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_txtFormuleCouleur = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_toolTipTraductible = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_colorSelect
            // 
            this.m_colorSelect.BackColor = System.Drawing.Color.White;
            this.m_colorSelect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_colorSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_colorSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_colorSelect.Location = new System.Drawing.Point(0, 0);
            this.m_colorSelect.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_colorSelect, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_colorSelect.Name = "m_colorSelect";
            this.m_colorSelect.SelectedColor = System.Drawing.Color.White;
            this.m_colorSelect.Size = new System.Drawing.Size(22, 22);
            this.m_colorSelect.TabIndex = 0;
            this.m_colorSelect.OnChangeSelectedColor += new System.EventHandler(this.m_colorSelect_OnChangeSelectedColor);
            // 
            // m_txtFormuleCouleur
            // 
            this.m_txtFormuleCouleur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormuleCouleur.Formule = null;
            this.m_txtFormuleCouleur.Location = new System.Drawing.Point(54, 0);
            this.m_txtFormuleCouleur.LockEdition = false;
            this.m_txtFormuleCouleur.LockZoneTexte = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtFormuleCouleur, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFormuleCouleur.Name = "m_txtFormuleCouleur";
            this.m_txtFormuleCouleur.Size = new System.Drawing.Size(188, 22);
            this.m_txtFormuleCouleur.TabIndex = 1;
            this.m_toolTipTraductible.SetToolTip(this.m_txtFormuleCouleur, "Color formula|10000");
            this.m_txtFormuleCouleur.OnChangeTexteFormule += new System.EventHandler(this.m_txtFormuleCouleur_OnChangeTexteFormule);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "f(x) =";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CControlSelectColorByFormule
            // 
            this.Controls.Add(this.m_txtFormuleCouleur);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_colorSelect);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControlSelectColorByFormule";
            this.Size = new System.Drawing.Size(242, 22);
            this.ResumeLayout(false);

		}
		#endregion


		public void Init(IFournisseurProprietesDynamiques fournisseur, Type type)
		{
			Init ( fournisseur, new CObjetPourSousProprietes (type));
		}

		public void Init(IFournisseurProprietesDynamiques fournisseur, CObjetPourSousProprietes objetAnalyse)
		{
			m_txtFormuleCouleur.Init(fournisseur, objetAnalyse);
        }


        //-------------------------------------------------------------------------------------
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public C2iExpression FormuleCouleur
		{
			get
			{
				return m_txtFormuleCouleur.Formule;
			}
			set
			{
				m_txtFormuleCouleur.Formule = value;
                // Initialise si possible la zone de couleur avec la formule
                if(!DesignMode)
                    UpdateColorFromFormule();
                
			}
		}

        private void UpdateColorFromFormule()
        {
            if (FormuleCouleur != null)
            {
                C2iExpressionColor col = FormuleCouleur as C2iExpressionColor;
                if (col != null)
                {
                    List<int> valeurs = new List<int>();
                    bool bAllInt = true;
                    foreach (C2iExpression param in col.Parametres2i)
                    {
                        C2iExpressionConstante cst = param as C2iExpressionConstante;
                        if (cst == null)
                        {
                            bAllInt = false;
                            break;
                        }
                        try
                        {
                            valeurs.Add(Convert.ToInt32(cst.Valeur));
                        }
                        catch
                        {
                            bAllInt = false;
                            break;
                        }
                    }
                    if (bAllInt)
                    {
                        try
                        {
                            if (valeurs.Count == 3)
                                m_colorSelect.SelectedColor = Color.FromArgb(valeurs[0], valeurs[1], valeurs[2]);
                            else
                                m_colorSelect.SelectedColor = Color.FromArgb(valeurs[0], valeurs[1], valeurs[2], valeurs[3]);
                        }
                        catch
                        {
                            m_colorSelect.SelectedColor = Color.White;
                        }
                    }
                }
                else
                {
                    m_colorSelect.SelectedColor = Color.White;
                }
            }
            else
            {
                m_colorSelect.SelectedColor = Color.White;
            }
        }

        //-------------------------------------------------------------------------------------
        public Color SelectedColor
        {
            get
            {
                return m_colorSelect.SelectedColor;
            }
            set
            {
                m_colorSelect.SelectedColor = value;
            }
        }



		#region Membres de IControlALockEdition

		public bool LockEdition
		{
			get
			{
                return m_txtFormuleCouleur.LockEdition;
			}
			set
			{
                m_txtFormuleCouleur.LockEdition = value;
                m_colorSelect.LockEdition = value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs()) ;
			}
		}

		public event System.EventHandler OnChangeLockEdition;

		#endregion

        private bool m_bCouleurEnSelection = false;
        private void m_colorSelect_OnChangeSelectedColor(object sender, EventArgs e)
        {
            m_bCouleurEnSelection = true;
            Color couleur = m_colorSelect.SelectedColor;
            C2iExpressionColor col = FormuleCouleur as C2iExpressionColor;
            if (FormuleCouleur != null && FormuleCouleur.GetString() != string.Empty &&  col == null)
            {
                // Ce n'est pas une formule simple de type Color(xxx; yyy; zzz)
                if (CFormAlerte.Afficher(I.T("Replace current Color Formula ?|10001"), EFormAlerteBoutons.OuiNon, EFormAlerteType.Question) == DialogResult.No)
                {
                    UpdateColorFromFormule();
                    return;
                }
            }

            m_txtFormuleCouleur.Formule = GetFormuleCouleur(couleur);
            m_bCouleurEnSelection = false;
        }

        //---------------------------------------------------------------------
        public C2iExpression GetFormuleCouleur(Color c)
        {
            return GetFormuleCouleur(c.R, c.G, c.B);
        }

        //---------------------------------------------------------------------
        public C2iExpression GetFormuleCouleur(int r, int g, int b)
        {
            C2iExpressionColor co = new C2iExpressionColor();
            co.Parametres.Add(new C2iExpressionConstante(r));
            co.Parametres.Add(new C2iExpressionConstante(g));
            co.Parametres.Add(new C2iExpressionConstante(b));
            return co;
        }

        private void m_txtFormuleCouleur_OnChangeTexteFormule(object sender, EventArgs e)
        {
            if(!m_bCouleurEnSelection)
                UpdateColorFromFormule();
        }




        
	}
}
