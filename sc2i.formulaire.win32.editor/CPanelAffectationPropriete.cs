using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;

namespace sc2i.formulaire.win32
{
	public partial class CPanelAffectationPropriete : UserControl
	{
		private CDefinitionProprieteDynamique m_propriete;
		private C2iExpression m_formule = null;
		
		public CPanelAffectationPropriete()
		{
			InitializeComponent();
		}

		public void Init(
			CDefinitionProprieteDynamique def,
			C2iExpression formule,
			IFournisseurProprietesDynamiques fournisseurProprietes,
			CObjetPourSousProprietes objetSource)
		{
			m_propriete = def;
			m_formule = formule;
			m_libelle.Text = def.Nom;
            m_txtFormule.Init(fournisseurProprietes, objetSource);
			m_txtFormule.Formule = formule;
		}

		public CDefinitionProprieteDynamique Propriete
		{
			get
			{
				return m_propriete;
			}
		}

		public C2iExpression Formule
		{
			get
			{
				return m_txtFormule.Formule;
			}
		}
	}
}
