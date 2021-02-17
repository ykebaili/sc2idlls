using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.common;
using System.Collections.Generic;

namespace sc2i.formulaire.win32.editor
{
	/// <summary>
	/// Description résumée de CPanelListe2iWnd.
	/// </summary>
	public class CPanelListe2iWnd : System.Windows.Forms.UserControl
	{
		private Hashtable m_tableAssembliesCharges = new Hashtable();
		private const int c_nElementHeight = 24;
		private const int c_nEcartYElements = 2;

		private Type m_typeEdite = null;

		private ArrayList m_listeElements = new ArrayList();

		private List<Type> m_listeTypesControlesConnus = new List<Type>();

		#region Component Designer generated code

		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;




		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// CPanelListe2iWnd
			// 
			this.AutoScroll = true;
			this.Name = "CPanelListe2iWnd";
			this.Size = new System.Drawing.Size(152, 368);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CPanelListe2iWnd_DragDrop);
			this.ResumeLayout(false);

		}
		#endregion

		public CPanelListe2iWnd()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Indique pour l'édition de quel type d'élément les contrôles vont
		/// être utilisés
		/// </summary>
		/// <param name="tp"></param>
		public void SetTypeEdite(Type tp)
		{
			m_typeEdite = tp;
			RefreshControls();
		}

		//----------------------------------------------------------
		public void AddAssembly(System.Reflection.Assembly assembly)
		{
			if (m_tableAssembliesCharges[assembly] != null)
				return;
			m_tableAssembliesCharges[assembly] = true;
			foreach (Type tp in assembly.GetExportedTypes())
			{
				if (typeof(I2iWndComposantFenetre).IsAssignableFrom ( tp ) && !tp.IsAbstract)
				{
					AddTypeControl(tp);
				}
			}
		}

		//----------------------------------------------------------
		//Remet à jour la liste des contrôles disponibles et compatibles avec le type édité
		public void RefreshControls()
		{
			m_listeElements.Clear();
			this.SuspendDrawing();
			Dictionary<Type, bool> typesTraites = new Dictionary<Type, bool>();
			ArrayList lstControles = new ArrayList ( Controls );
			foreach (Control ctrl in lstControles)
			{
				CElementListeWnd elt = ctrl as CElementListeWnd;
				typesTraites.Add(elt.TypeAssocie, true);
				if (elt != null)
				{
					
					C2iWnd wnd = (C2iWnd)Activator.CreateInstance(elt.TypeAssocie);
					if (!wnd.CanBeUseOnType(m_typeEdite))
					{
						elt.Visible = false;
						elt.Parent.Controls.Remove(elt);
						elt.Dispose();
					}
					else
						m_listeElements.Add(elt);
				}
				else
					m_listeElements.Add(elt);
			}
			m_listeTypesControlesConnus.Sort  ( new TypeSorter() );

			foreach (Type tp in m_listeTypesControlesConnus)
			{
				if (!typesTraites.ContainsKey(tp))
				{
					if (!tp.IsAbstract)
					{
						C2iWnd wnd = (C2iWnd)Activator.CreateInstance(tp);
						if (!wnd.CanBeUseOnType(m_typeEdite))
							continue;
					}

					CElementListeWnd elt = new CElementListeWnd();
					elt.Image = C2iWnd.GetImage(tp);
					/*object[] atts = tp.GetCustomAttributes(typeof(AWndIcone), false);

					if (atts.Length >= 1)
					{
						AWndIcone att = (AWndIcone)atts[0];
						elt.Image = att.Icone;
					}*/
                    Controls.Add(elt);
					elt.TypeAssocie = tp;
					elt.Size = new Size(ClientRectangle.Width, c_nElementHeight);
					elt.Left = 0;
					elt.Top = m_listeElements.Count * (c_nElementHeight + c_nEcartYElements) + c_nEcartYElements;
					elt.CreateControl();
					elt.Dock = DockStyle.Top;
					elt.SendToBack();
					elt.Visible = true;
					m_listeElements.Add(elt);

				}
			}
			this.ResumeDrawing();
		}

		private class TypeSorter : IComparer<Type>
		{
			public int Compare(Type tp1, Type tp2)
			{
			
				string strNom1 = DynamicClassAttribute.GetNomConvivial(tp1);
				string strNom2 = DynamicClassAttribute.GetNomConvivial(tp2);
				object[] attribs = tp1.GetCustomAttributes(typeof(WndNameAttribute), false);
				if (attribs.Length != 0)
					strNom1 = ((WndNameAttribute)attribs[0]).Name;
				attribs = tp2.GetCustomAttributes(typeof(WndNameAttribute), false);
				if(  attribs.Length != 0 )
					strNom2 = ((WndNameAttribute)attribs[0]).Name;
				return strNom2.CompareTo(strNom1);
			}
		}

		public void AddTypeControl(Type tp)
		{
			if (!typeof ( I2iWndComposantFenetre ).IsAssignableFrom ( tp ))
				return;
			if ( !m_listeTypesControlesConnus.Contains ( tp ) )
				m_listeTypesControlesConnus.Add(tp);
		}

		//----------------------------------------------------------
		public void AddAllLoadedAssemblies()
		{
			foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
				AddAssembly(ass);
			RefreshControls();
		}

		public void CPanelListe2iWnd_DragDrop(object sender, DragEventArgs e)
		{
			foreach (CElementListeWnd ele in m_listeElements)
				ele.CElementListeWnd_DragDrop(sender, e);
		}
	}
}
