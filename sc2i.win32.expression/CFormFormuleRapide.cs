using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;

namespace timos
{
	public partial class CFormFormuleRapide : Form
	{
		private object m_objet;
        C2iExpression m_formule = null;
        private static Hashtable m_tableLastFormuleDuType = new Hashtable();
        private static int m_nLastIndex = 0;

		//-----------------------------------------------------------
		public CFormFormuleRapide()
		{
			InitializeComponent();
		}

	

		//-----------------------------------------------------------
		/// <summary>
		/// Permet d'évaluer une formule à partir de l'objet édité
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		public static void EditerFormule(object objet)
		{
            if (objet == null)
                return;

            
			CFormFormuleRapide form = new CFormFormuleRapide();
			form.m_objet = objet;
			
            form.ShowDialog();
            form.Dispose();
			
		}

		//-----------------------------------------------------------
		private void CFormFormuleRapide_Load(object sender, EventArgs e)
		{
			sc2i.win32.common.CWin32Traducteur.Translate(this);
			m_lblEntite.Text = m_objet.ToString();

            Type tp = m_objet.GetType();
            m_wndAideFormule.FournisseurProprietes = new CFournisseurGeneriqueProprietesDynamiques();
			m_wndAideFormule.ObjetInterroge = tp;
			m_txtFormule.Init(m_wndAideFormule.FournisseurProprietes, m_wndAideFormule.ObjetInterroge);

            ArrayList tableauChaines = (m_tableLastFormuleDuType[tp]) as ArrayList;
            if (tableauChaines != null  && tableauChaines.Count > 0)
            {
                m_nLastIndex = tableauChaines.Count - 1;
                m_txtFormule.Text = (string) tableauChaines[m_nLastIndex];
                
            }
            else
            {
                m_txtFormule.Text = "\"\"";
            }


		}

        //-----------------------------------------------------------------------------
        private void m_btnEvaluer_Click(object sender, EventArgs e)
        {
            Evaluer();
        }

        //-----------------------------------------------------------------------------
        private void Evaluer()
        {
            CResultAErreur result = CResultAErreur.True;

            if (m_objet == null)
                return;


            // Analyser l'expression
            Type tp = m_objet.GetType();
            CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression(new CFournisseurGeneriqueProprietesDynamiques(), tp);
            CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);


            try
            {
                result = analyseur.AnalyseChaine(m_txtFormule.Text);
                if (result)
                    m_formule = (C2iExpression)result.Data;
                else
                {
                    result.EmpileErreur(I.T("Formula error|1422"));
                    CFormAlerte.Afficher(result.Erreur);
                    return;
                }

                // Evaluer l'expression
                if (m_formule != null)
                {
                    CContexteEvaluationExpression contexte = new CContexteEvaluationExpression(m_objet);
                    result = m_formule.Eval(contexte);
                    if (!result)
                    {
                        CFormAlerte.Afficher(result.Erreur);
                        return;
                    }
                    if (result.Data == null)
                        m_txtResultat.Text += "> " + "null";
                    else
                        m_txtResultat.Text += "> " + result.Data.ToString();

                    m_txtResultat.Text += Environment.NewLine;

                    // Ajoute la formule à la liste des dernières formules utilisées
                    ArrayList tableauFormules = m_tableLastFormuleDuType[tp] as ArrayList;
                    if (tableauFormules == null)
                        tableauFormules = new ArrayList();

                    if(tableauFormules.Count == 0 || m_txtFormule.Text != (string)tableauFormules[tableauFormules.Count-1])
                        tableauFormules.Add(m_txtFormule.Text);
                    m_nLastIndex = tableauFormules.Count - 1;
                    m_tableLastFormuleDuType[tp] = tableauFormules;
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                CFormAlerte.Afficher(result.Erreur);
            }
        }
        
        
        //-----------------------------------------------------------------------------
        private void m_wndAideFormule_OnSendCommande(string strCommande, int nPosCurseur)
        {
            m_wndAideFormule.InsereInTextBox(m_txtFormule.TextBox, nPosCurseur, strCommande);
        }


        //-----------------------------------------------------------------------------
        private void m_btnEvaluer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    e.Handled = true;
                    Evaluer();
                    break;
           }
        }

        //-----------------------------------------------------------------------------
        private void m_btnClear_Click(object sender, EventArgs e)
        {
            m_txtResultat.Clear();
        }

        
        //-----------------------------------------------------------------------------
        private void m_btnPrevious_Click(object sender, EventArgs e)
        {
            
            Type tp = m_objet.GetType();

            ArrayList tableauFormules = (m_tableLastFormuleDuType[tp]) as ArrayList;
            if (tableauFormules != null && tableauFormules.Count > 0)
            {
                if(m_nLastIndex > 0)
                    m_txtFormule.Text = (string)tableauFormules[--m_nLastIndex];
            }
        }

        //-----------------------------------------------------------------------------
        private void m_btnNext_Click(object sender, EventArgs e)
        {

            Type tp = m_objet.GetType();

            ArrayList tableauFormules = (m_tableLastFormuleDuType[tp]) as ArrayList;
            if (tableauFormules != null && tableauFormules.Count > 0)
            {
                if (m_nLastIndex < tableauFormules.Count - 1 )
                    m_txtFormule.Text = (string)tableauFormules[++m_nLastIndex];
            }

        }
         
	}
}