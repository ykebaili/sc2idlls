using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using sc2i.expression.FonctionsDynamiques;

namespace sc2i.formulaire.win32.controles2iWnd
{

	public class CUtilControlesWnd
	{
		public static CResultAErreur DeclencheEvenement(
			string strIdEvenement,
            IControleWndFor2iWnd ctrl)
		{
			CHandlerEvenementParFormule handler = null; 
			if (ctrl!=null && ctrl.WndAssociee != null)
			{
				handler = ctrl.WndAssociee.GetHandler(strIdEvenement);
				if (handler != null)
				{
					return CUtilControlesWnd.DeclencheEvenement(ctrl, handler);
				}
			}
			return CResultAErreur.True;
		}

        public static CContexteEvaluationExpression GetContexteEval ( 
            IControleWndFor2iWnd ctrl, 
            object objetPrincipal)
        {
            return new CContexteEvaluationExpression ( 
                GetObjetForEvalFormuleParametrage ( ctrl, objetPrincipal ) );
        }

        public static object GetObjetForEvalFormuleParametrage ( 
            IControleWndFor2iWnd ctrl,
            object objetPrincipal )
        {

            if ( ctrl == null )
                return objetPrincipal;
            IControleWndFor2iWnd ctrlParent = ctrl;
            while (ctrlParent.WndContainer != null && !ctrlParent.IsRacineForEvenements)
			    ctrlParent = ctrlParent.WndContainer as IControleWndFor2iWnd;
            return new CDefinitionMultiSourceForExpression ( 
                objetPrincipal,
                new CSourceSupplementaire( "CurrentWindow", new CEncaspuleurControleWndForFormules(ctrlParent) ));
        }


		private static CResultAErreur DeclencheEvenement(
			IControleWndFor2iWnd ctrl,
			CHandlerEvenementParFormule handler)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if (handler.FormuleEvenement != null)
				{
					IControleWndFor2iWnd ctrlParent = ctrl;
                    while (ctrlParent.WndContainer != null && !ctrlParent.IsRacineForEvenements)
						ctrlParent = ctrlParent.WndContainer as IControleWndFor2iWnd;
					CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(new CEncaspuleurControleWndForFormules(ctrlParent));
					result = handler.FormuleEvenement.Eval(ctx);
					if (ctrl.Control != null)
						ctrl.Control.Refresh();
				}
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}

        /*public static object GetParameter ( IControleWndFor2iWnd ctrl,
            string strParameter )
        {*/
            
	}


	//-----------------------------------------------------------------------------------
	public class CEncaspuleurControleWndForFormules : 
        IElementAProprietesDynamiquesDeportees, 
		IWndAChildNomme, 
        IWndAContainer,
		IAllocateurSupprimeurElements,
		IInterpreteurMethodeDynamique,
        IElementAFonctionsDynamiques,
		IAttacheurObjetsAContexteEvaluationExpression
	{
		private IControleWndFor2iWnd m_controleWnd = null;

		public CEncaspuleurControleWndForFormules(IControleWndFor2iWnd controleWnd)
		{
			m_controleWnd = controleWnd;
		}


		//-----------------------------------------------------------
		public object GetValeurDynamiqueDeportee(string strPropriete)
		{
			if ( m_controleWnd != null && m_controleWnd.Control != null)
			{
                for (int nPasse = 0; nPasse < 2; nPasse++)
                {
                    object objDest = nPasse == 0 ? (object)m_controleWnd.Control : (object)m_controleWnd;
                    Type tp = objDest.GetType();
                    List<PropertyInfo> lstInfos = new List<PropertyInfo>();
                    try
                    {
                        PropertyInfo info = tp.GetProperty(strPropriete);
                        if (info != null)
                            lstInfos.Add(info);
                    }
                    catch
                    {
                        //Erreur, peut être plusieurs candidats
                        foreach (PropertyInfo info in tp.GetProperties())
                        {
                            if (info.Name == strPropriete)
                                lstInfos.Add(info);
                        }
                    }
                    foreach (PropertyInfo info in lstInfos)
                    {
                        if (info != null)
                        {
                            MethodInfo infoMeth = info.GetGetMethod();
                            if (infoMeth != null)
                                try
                                {
                                    return infoMeth.Invoke(objDest, null);
                                }
                                catch { }
                        }
                    }
                }
                Control ctrl = m_controleWnd.Control;
				if (strPropriete == "X")
					return ctrl.Location.X;
                if (strPropriete == "HelpText")
                {
                    if (m_controleWnd.Tooltip != null && m_controleWnd.Control != null)
                        return m_controleWnd.Tooltip.GetToolTip(m_controleWnd.Control);
                }
				if (strPropriete == "Y")
					return ctrl.Location.Y;
				if (strPropriete == "FontBold")
					return ctrl.Font.Bold;
				if (strPropriete == "FontItalic")
					return ctrl.Font.Italic;
				if (strPropriete == "FontStrikeOut")
					return ctrl.Font.Strikeout;
				if (strPropriete == "FontName")
					return ctrl.Font.Name;
				if (strPropriete == "FontSize")
					return (double)ctrl.Font.Size;

                if (m_controleWnd is IElementAProprietesDynamiquesDeportees)
                    return ((IElementAProprietesDynamiquesDeportees)m_controleWnd).GetValeurDynamiqueDeportee(strPropriete);
			}
			return null;
		}

		//-----------------------------------------------------------
		public void SetValeurDynamiqueDeportee(string strPropriete, object valeur)
		{
			if ( m_controleWnd != null && m_controleWnd.Control!=null )
			{
                for (int nPasse = 0; nPasse < 2; nPasse++)
                {
                    object objDest = nPasse == 0 ? (object)m_controleWnd.Control : (object)m_controleWnd;
                    Type tp = objDest.GetType();
                    List<PropertyInfo> lstInfos = new List<PropertyInfo>();
                    try
                    {
                        PropertyInfo info = tp.GetProperty(strPropriete);
                        if (info != null)
                            lstInfos.Add(info);
                    }
                    catch
                    {
                        //Erreur, peut être plusieurs candidats
                        foreach (PropertyInfo info in tp.GetProperties())
                        {
                            if (info.Name == strPropriete)
                                lstInfos.Add(info);
                        }
                    }
                    foreach (PropertyInfo info in lstInfos)
                    {
                        if (info != null)
                        {
                            MethodInfo infoMeth = info.GetSetMethod();
                            if (infoMeth != null)
                            {
                                try
                                {
                                    infoMeth.Invoke(objDest, new object[] { valeur });
                                    return;
                                }
                                catch ( Exception e1 )
                                {
                                    try
                                    {
                                        infoMeth.Invoke(objDest, new object[] { C2iConvert.ChangeType(valeur, info.PropertyType) });
                                        return;
                                    }
                                    catch(Exception e2)
                                    {
                                        //On n'y arrive pas
                                    }
                                }
                            }
                        }
                    }
                }
				
				//Implémentation de méthode communes;
				Control ctrl = m_controleWnd.Control;
				if (strPropriete == "X" && valeur is int)
                {
					ctrl.Location = new System.Drawing.Point((int)valeur, ctrl.Location.Y);
                    return;
                }
                if ( strPropriete== "HelpText"  )
                {
                    if ( m_controleWnd.Tooltip != null && m_controleWnd.Control != null )
                        m_controleWnd.Tooltip.SetToolTip ( m_controleWnd.Control, valeur==null?"":valeur.ToString() );
                }

				if (strPropriete == "Y" && valeur is int)
                {
					ctrl.Location = new System.Drawing.Point(ctrl.Location.X, (int)valeur);
                    return;
                }
				if ( strPropriete.IndexOf("Font") == 0 )
				{
					bool bBold = ctrl.Font.Bold;
					bool bItalic = ctrl.Font.Italic;
					bool bStrikeOut = ctrl.Font.Strikeout;
					bool bUnderline = ctrl.Font.Underline;
					string strName = ctrl.Font.Name;
					double fSize = ctrl.Font.Size;	
					if ( strPropriete == "FontBold" && valeur is bool )
						bBold = (bool)valeur;
					if ( strPropriete == "FontItalic" && valeur is bool )
						bItalic = (bool)valeur;
					if ( strPropriete == "FontStrikeOut" && valeur is bool )
						bStrikeOut = (bool)valeur;
					if(  strPropriete == "FontUnderline" && valeur is bool )
						bUnderline = (bool)valeur;
					if ( strPropriete == "FontName" && valeur is string )
						strName = valeur.ToString();
					if (strPropriete == "FontSize" && (valeur is int || valeur is double))
						fSize = Convert.ToDouble(valeur);
					FontStyle style = FontStyle.Regular;
					if ( bBold )
						style |= FontStyle.Bold;
					if ( bItalic )
						style|= FontStyle.Italic;
					if ( bStrikeOut )
						style |= FontStyle.Strikeout;
					if ( bUnderline )
						style |= FontStyle.Underline;
					ctrl.Font = new Font ( strName, (float)fSize, style);
                    return;
				}
                //On n'a toujours pas trouvé !
                if (m_controleWnd is IElementAProprietesDynamiquesDeportees)
                    ((IElementAProprietesDynamiquesDeportees)m_controleWnd).SetValeurDynamiqueDeportee(strPropriete, valeur);
			}
		}
	
		//------------------------------------------------------------
		public IWndAChildNomme  GetChildFromName(string strName)
		{
 			if ( m_controleWnd != null )
			{
				foreach ( IControleWndFor2iWnd ctrl in m_controleWnd.Childs )
				{
					if ( ctrl.WndAssociee != null && ctrl.WndAssociee.Name.ToUpper() == strName.ToUpper() )
						return new CEncaspuleurControleWndForFormules ( ctrl );
				}
                foreach (IControleWndFor2iWnd ctrl in m_controleWnd.Childs)
                {
                    IWndAChildNomme trouve = new CEncaspuleurControleWndForFormules(ctrl).GetChildFromName(strName);
                    if (trouve != null)
                        return trouve;
                }
			}
			return null;
		}

        //------------------------------------------------------------
        /// <summary>
        /// Récupère la valeur d'un paramètre
        /// </summary>
        /// <param name="strParametre"></param>
        /// <returns></returns>
        /// <remarks>
        /// Chaque fenêtre peut posséder des paramètres.
        /// La valeur par défaut du paramètre peut être défini via une formule,
        /// cependant, en appellant "SetParameterValue" sur le C2iWnd, la valeur
        /// par défaut peut être remplacée par une valeur spécifique.<BR></BR>
        /// Si une fenêtre ne possède pas un paramètre, elle remonte sur les 
        /// fenêtre parentes pour tenter de trouver la valeur du paramètre
        /// </remarks>
        public object GetParameter(string strParametre)
        {
            C2iWndFenetre wndFenetre = m_controleWnd != null ? m_controleWnd.WndAssociee as C2iWndFenetre : null;
            if (wndFenetre != null)
            {
                object val = wndFenetre.GetValeurForceeParametre(strParametre);
                if (val != null)
                    return val;
                strParametre = strParametre.ToUpper();
                foreach (CFormuleNommee f in wndFenetre.Parameters)
                {
                    if (f.Libelle.ToUpper() == strParametre && f.Formule != null)
                    {
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(m_controleWnd.EditedElement);
                        CResultAErreur result = f.Formule.Eval(ctx);
                        if (result)
                            return result.Data;
                    }
                }
            }

            CEncaspuleurControleWndForFormules ctrl = WndContainer as CEncaspuleurControleWndForFormules;
            if ( ctrl != null )
                return ctrl.GetParameter ( strParametre );
            return null;
        }

		//------------------------------------------------------------
		public void SetError(string strErreur)
		{
			if (m_controleWnd == null)
				return;
			Control ctrl = m_controleWnd.Control;
			IControleWndFor2iWnd ctrlWnd = m_controleWnd;
			while ( ctrlWnd != null && !typeof(IWndErrorProvider).IsAssignableFrom (ctrlWnd.GetType() ))
				ctrlWnd = ctrlWnd.WndContainer as IControleWndFor2iWnd;
			IWndErrorProvider provider = ctrlWnd as IWndErrorProvider;
			if ( provider != null && provider.ErrorProvider != null )
				provider.ErrorProvider.SetError ( ctrl, strErreur );
		}

		public CResultAErreur AlloueElement(Type tp)
		{
			if (m_controleWnd != null)
			{
				IAllocateurSupprimeurElements allocateur = m_controleWnd.EditedElement as IAllocateurSupprimeurElements;
				if (allocateur != null)
					return allocateur.AlloueElement(tp);
			}
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Unavailable Function|20008"));
			return result;
		}

		public CResultAErreur SupprimeElement(object elt)
		{
			CResultAErreur result = CResultAErreur.True;
			result.EmpileErreur(I.T("Unavailable Function|20008"));
			return result;
		}

		public void UpdateValeursCalculees()
		{
			if (m_controleWnd != null)
				m_controleWnd.UpdateValeursCalculees();
		}



		public bool GetMethodInfo(string strMethode, ref MethodInfo methode, ref object objetSource)
		{
			if (m_controleWnd != null )
			{
                Type tp = null;
                MethodInfo info = null;
                if (m_controleWnd.Control != null)
                {
                    tp = m_controleWnd.Control.GetType();
                    try
                    {
                        info = tp.GetMethod(strMethode);
                    }
                    catch { }
                    if (info != null)
                    {
                        methode = info;
                        objetSource = m_controleWnd.Control;
                        return true;
                    }
                }
				tp = m_controleWnd.GetType();
				info = tp.GetMethod(strMethode);
				if (info != null)
				{
					methode = info;
					objetSource = m_controleWnd;
					return true;
				}
			}
            
			return false;
		}
		public void AttacheObjetsAContexteEvaluation(CContexteEvaluationExpression ctx)
		{
			if (m_controleWnd != null)
			{
				IAttacheurObjetsAContexteEvaluationExpression attacheur = m_controleWnd.EditedElement as IAttacheurObjetsAContexteEvaluationExpression;
				if (attacheur != null)
					attacheur.AttacheObjetsAContexteEvaluation(ctx);
			}			
		}

        #region IWndAContainer Membres

        public IWndAContainer WndContainer
        {
            get
            {
                if (m_controleWnd != null)
                {
                    if (m_controleWnd.WndContainer != null && m_controleWnd.WndContainer is IControleWndFor2iWnd)
                        return new CEncaspuleurControleWndForFormules(m_controleWnd.WndContainer as IControleWndFor2iWnd);
                }
                return null;
            }
            set { }
                
        }

        #endregion


        #region IElementAFonctionsDynamiques Membres

        public IEnumerable<CFonctionDynamique> FonctionsDynamiques
        {
            get
            {
                IElementAFonctionsDynamiques elt = m_controleWnd as IElementAFonctionsDynamiques;
                if (elt != null)
                    return elt.FonctionsDynamiques;
                return new CFonctionDynamique[0]; ;
            }
        }

        public CFonctionDynamique GetFonctionDynamique(string strIdFonction)
        {
            return FonctionsDynamiques.FirstOrDefault(f => f.IdFonction == strIdFonction);
        }

        #endregion
    }
}