using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using System.Collections;
using sc2i.win32.common;

namespace sc2i.formulaire.win32
{
	public partial class CPanelAffectationsProprietes : UserControl
	{
		public CPanelAffectationsProprietes()
		{
			InitializeComponent();
		}

		public void Init(
			CAffectationsProprietes affectations,
			IFournisseurProprietesDynamiques fournisseur,
			Type typeElementModifie,
			CObjetPourSousProprietes objetSource)
		{
			ArrayList lstControles = new ArrayList ( m_panelControles.Controls );
			foreach (Control ctrl in lstControles)
			{
				ctrl.Visible = false;
				m_panelControles.Controls.Remove(ctrl);
				ctrl.Dispose();
			}
			CDefinitionProprieteDynamique[] defs = fournisseur.GetDefinitionsChamps(typeElementModifie, 0);
			m_panelControles.SuspendDrawing();
            List<CDefinitionProprieteDynamique> lstDefs = new List<CDefinitionProprieteDynamique>(defs);
            lstDefs.Sort((x, y) => x.Nom.CompareTo(y.Nom));
			foreach ( CDefinitionProprieteDynamique def in lstDefs )
			{
				if (!def.IsReadOnly)
				{
					CPanelAffectationPropriete panel = new CPanelAffectationPropriete();
					panel.Dock = DockStyle.Top;
					m_panelControles.Controls.Add(panel);
					C2iExpression formule = affectations.GetFormuleFor(def);
                    panel.Init(def, formule, fournisseur, objetSource);
                    m_panelControles.BringToFront();
				}
			}
			m_panelControles.ResumeDrawing();
		}

		public CAffectationsProprietes GetAffectations()
		{
			CAffectationsProprietes affectation = new CAffectationsProprietes();
			foreach (Control ctrl in m_panelControles.Controls)
			{
				CPanelAffectationPropriete panel = ctrl as CPanelAffectationPropriete;
				if (panel != null)
				{
					affectation.SetAffectation(panel.Propriete, panel.Formule);
				}
			}
			return affectation;
		}
	}
}
