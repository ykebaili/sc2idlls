using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using sc2i.common;
using System.Drawing;
using sc2i.expression;
using System.Collections;
using sc2i.expression.Debug;


namespace sc2i.expression
{
    [Serializable]
    public class C2iExpressionGraphique : C2iExpression, I2iObjetGraphique, IFournisseurProprietesDynamiques
    {
        public const string c_cleFichier = "FUT_GRAF_FORMULA";
        //-----------------------------------------------------
        private List<CRepresentationExpressionGraphique> m_listeRepresentations = new List<CRepresentationExpressionGraphique>();
        private List<CDefinitionProprieteDynamiqueVariableFormule> m_listeVariables = new List<CDefinitionProprieteDynamiqueVariableFormule>();

        private IFournisseurProprietesDynamiques m_fournisseurPrincipal = null;

        private string m_strIdStartPoint = "";

        private Size m_size = new Size(1000, 1000);

        private C2iExpression m_formuleFinale = null;

        private bool m_bDebug = false;

        public C2iExpressionGraphique()
            : base()
        {
            Size = new Size(1000, 1000);
        }

        //-----------------------------------------------
        public void InitForAnalyse(IFournisseurProprietesDynamiques fournisseurOriginal)
        {
            m_fournisseurPrincipal = fournisseurOriginal;
        }

        //-----------------------------------------------
        public I2iObjetGraphique[] Childs
        {
            get { return m_listeRepresentations.ToArray(); }
        }

        //-----------------------------------------------
        public bool Debug
        {
            get
            {
                return m_bDebug;
            }
            set
            {
                m_bDebug = value;
            }
        }

        //-----------------------------------------------
        public CRepresentationExpressionGraphique StartPoint
        {
            get
            {
                return GetFormule(m_strIdStartPoint);
            }
            set
            {
                if (value == null)
                    m_strIdStartPoint = "";
                else
                    m_strIdStartPoint = value.Id;
            }
        }

        //-----------------------------------------------
        public bool AddChild(I2iObjetGraphique child)
        {
            if ( child is CRepresentationExpressionGraphique && !m_listeRepresentations.Contains ( (CRepresentationExpressionGraphique)(child) ))
            {
                m_listeRepresentations.Add ( (CRepresentationExpressionGraphique)child );
                return true;
            }
            return false;               
        }

        //-----------------------------------------------
        public bool ContainsChild(I2iObjetGraphique child)
        {
            return m_listeRepresentations.Contains(child as CRepresentationExpressionGraphique);
        }

        //-----------------------------------------------
        public IEnumerable<CDefinitionProprieteDynamiqueVariableFormule> Variables
        {
            get
            {
                return m_listeVariables;
            }
        }

        //-----------------------------------------------
        public void AddVariable(CDefinitionProprieteDynamiqueVariableFormule variable)
        {
            if ( m_listeVariables.FirstOrDefault ( v=>v.Nom.ToUpper() == variable.Nom.ToUpper()) == null )
                m_listeVariables.Add ( variable );
        }

        //-----------------------------------------------
        public void RemoveVariable(CDefinitionProprieteDynamiqueVariableFormule variable)
        {
            m_listeVariables.Remove(variable);
        }

        //-----------------------------------------------
        public void ReplaceVariable(CDefinitionProprieteDynamiqueVariableFormule oldVar,
            CDefinitionProprieteDynamiqueVariableFormule newVar)
        {
            int? nIndex = null;
            if (oldVar != null)
            {
                nIndex = m_listeVariables.IndexOf(oldVar);
                if (nIndex < 0)
                    nIndex = null;
                m_listeVariables.Remove(oldVar);
            }
            if (nIndex == null || m_listeRepresentations.Count - 1 < nIndex.Value)
                m_listeVariables.Add(newVar);
            else
                m_listeVariables.Insert(nIndex.Value, newVar);
        }



        //-----------------------------------------------
        public void RemoveChild(I2iObjetGraphique child)
        {
            CRepresentationExpressionGraphique c = child as CRepresentationExpressionGraphique;
            if (c != null)
            {
                foreach (CRepresentationExpressionGraphique user in GetUtilisateurs(c.Id))
                    user.StopUseExterne(c);

                if (c.Prev != null)
                    c.Prev.Next = null;
                CRepresentationExpressionGraphique next = c.Next;
                if (next != null)
                {
                    c.Next = null;
                    RemoveChild(next);
                }
                m_listeRepresentations.Remove(c);
                
                
            }
        }

        //-----------------------------------------------
        public void OnChangeFormule(CRepresentationExpressionGraphique expression)
        {
            if (expression != null)
            {
                while (expression.Prev != null)
                    expression = expression.Prev;
                foreach (CRepresentationExpressionGraphique user in GetUtilisateurs(expression.Id))
                    user.OnChangeFormule(expression);
            }
        }

        //-----------------------------------------------
        public void BringToFront(I2iObjetGraphique child)
        {
            CRepresentationExpressionGraphique exp = child as CRepresentationExpressionGraphique;
            if (m_listeRepresentations.Contains(exp))
            {
                m_listeRepresentations.Remove(exp);
                m_listeRepresentations.Add(exp);
            }
            
            
        }

        //-----------------------------------------------
        public void FrontToBack(I2iObjetGraphique child)
        {
            CRepresentationExpressionGraphique exp = child as CRepresentationExpressionGraphique;
            if (m_listeRepresentations.Contains(exp))
            {
                m_listeRepresentations.Remove(exp);
                if (m_listeRepresentations.Count > 0)
                    m_listeRepresentations.Insert(0, exp);
                else
                    m_listeRepresentations.Add(exp);
            }
        }

        //-----------------------------------------------
        private int GetNumVersion()
        {
            return 1;
            //1 : ajout de debug
        }

        //-----------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            result = serializer.TraiteListe<CRepresentationExpressionGraphique>(m_listeRepresentations);
            if (serializer.Mode == ModeSerialisation.Lecture)
                foreach (CRepresentationExpressionGraphique graf in m_listeRepresentations)
                    graf.Parent = this;

            if (result)
                result = serializer.TraiteListe<CDefinitionProprieteDynamiqueVariableFormule>(m_listeVariables);
            
            if (!result)
                return result;

            serializer.TraiteString(ref m_strIdStartPoint);
            
            int nWidth = Size.Width;
            int nHeight = Size.Height;
            serializer.TraiteInt(ref nWidth);
            serializer.TraiteInt(ref nHeight);
            Size = new Size(nWidth, nHeight);

            result = serializer.TraiteObject<C2iExpression>(ref m_formuleFinale);
            if (!result)
                return result;
            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bDebug);
            return result;
        }

        //-----------------------------------------------
        public void Draw(CContextDessinObjetGraphique ctx)
        {
            MyDraw(ctx);
            foreach (CRepresentationExpressionGraphique rep in m_listeRepresentations)
                rep.Draw(ctx);
            
            CRepresentationExpressionGraphique start = StartPoint;
            if (start != null)
            {
                Rectangle rctStart = start.RectangleAbsolu;
                ctx.Graphic.DrawImageUnscaled(Resources.Start, rctStart.Left, rctStart.Top);
            }
        }

        //-----------------------------------------------
        protected void MyDraw(CContextDessinObjetGraphique ctx)
        {
            Rectangle rct = RectangleAbsolu;
            ctx.Graphic.FillRectangle(Brushes.White, rct);
            ctx.Graphic.DrawRectangle(Pens.Black, rct);
        }

        //-----------------------------------------------
        public CRepresentationExpressionGraphique FindFormule(C2iExpression formule)
        {
            if (formule == null)
                return null;
            string strSearch = formule.GetString().ToUpper().Trim();
            foreach (CRepresentationExpressionGraphique r in m_listeRepresentations)
            {
                if (r.Formule != null && r.Formule.GetString().ToUpper().Trim() == strSearch)
                    return r;
            }
            return null;
        }

        //-----------------------------------------------
        public CRepresentationExpressionGraphique GetFormule(string strId)
        {
            return m_listeRepresentations.FirstOrDefault(f => f.Id == strId);
        }

        //-----------------------------------------------
        /// <summary>
        /// Retourne la liste des éléments qui utilisent un objet
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public IEnumerable<CRepresentationExpressionGraphique> GetUtilisateurs(string strId)
        {
            return from obj in m_listeRepresentations where obj.IsUtilisateur(strId) select obj;
        }

        //-----------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux)
        {
            return GetDefinitionsChamps ( typeInterroge, nNbNiveaux, null );
        }

        //-----------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            if ( m_fournisseurPrincipal != null )
                lst.AddRange ( m_fournisseurPrincipal.GetDefinitionsChamps ( typeInterroge, nNbNiveaux, defParente ));
            foreach (CDefinitionProprieteDynamiqueVariableFormule var in m_listeVariables)
            {
                var.Rubrique = I.T("Variables|20085");
                lst.Add(var);
            }

            return lst.ToArray();

        }

        //-----------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
        {
            return GetDefinitionsChamps(objet, null);
        }

        //-----------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
            if ( m_fournisseurPrincipal != null )
                lst.AddRange ( m_fournisseurPrincipal.GetDefinitionsChamps ( objet, defParente ));
            if (defParente == null)
            {
                foreach (CDefinitionProprieteDynamiqueVariableFormule var in m_listeVariables)
                {
                    var.Rubrique = I.T("Variables|20085");
                    lst.Add(var);
                }
            }
            return lst.ToArray();
        }

        //-----------------------------------------------
        public C2iExpression FormuleFinale
        {
            get
            {
                if (m_formuleFinale != null)
                    return m_formuleFinale;
                RefreshFormuleFinale();
                return m_formuleFinale;
            }
        }

        //-----------------------------------------------
        public CResultAErreur RefreshFormuleFinale()
        {
            m_formuleFinale = null;
            CResultAErreur result = CResultAErreur.True;
            C2iExpressionBegin formule = new C2iExpressionBegin();
            foreach (CDefinitionProprieteDynamiqueVariableFormule def in m_listeVariables)
            {
                C2iExpressionVariable var = new C2iExpressionVariable();
                C2iExpression exp = new C2iExpressionChamp();
                ((C2iExpressionChamp)exp).DefinitionPropriete = def;
                var.Parametres.Add(exp);
                exp = new C2iExpressionConstante(def.TypeDonnee.TypeDotNetNatif.ToString());
                var.Parametres.Add(exp);
                if (def.TypeDonnee.IsArrayOfTypeNatif)
                {
                    exp = new C2iExpressionVrai();
                    var.Parametres.Add(exp);
                }
                formule.Parametres.Add(var);
            }
            CRepresentationExpressionGraphique graf = StartPoint;
            if (graf == null)
            {
                result.EmpileErreur(I.T("No start point|20103"));
                return result;
            }
                
            while (graf != null)
            {
                result = graf.RefreshParametres();
                if ( !result )
                    return result;
                if (graf.Formule != null)
                    formule.Parametres.Add(graf.Formule);
                graf = graf.Next;
            }
            m_formuleFinale = formule;
            return CResultAErreur.True;
        }

        //-----------------------------------------------
        public bool AcceptChilds
        {
            get { return true; }
        }

        //-----------------------------------------------
        public ArrayList AllChilds()
        {
            ArrayList lst = new ArrayList();
            lst.AddRange(m_listeRepresentations);
            return lst;
        }

        //-----------------------------------------------
        public event EventHandlerChild ChildAdded;

        //-----------------------------------------------
        public event EventHandlerChild ChildRemoved;

        //-----------------------------------------------
        public Rectangle ClientRect
        {
            get { return new Rectangle(0, 0, Size.Width, Size.Height); }
        }

        //-----------------------------------------------
        public Rectangle ClientToGlobal(Rectangle rect)
        {
            return rect;
        }

        //-----------------------------------------------
        public Point ClientToGlobal(Point pt)
        {
            return pt;
        }

        //-----------------------------------------------
        public Point[] ClientToGlobal(Point[] pts)
        {
            return pts;
        }

        //-----------------------------------------------
        public object Clone()
        {
            return CCloner2iSerializable.Clone(this);
        }

        //-----------------------------------------------
        public bool LockChilds
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        //-----------------------------------------------
        public void DeleteChild(I2iObjetGraphique child)
        {
            RemoveChild(child);
        }

        //-----------------------------------------------
        public void DrawSelected(Graphics g)
        {
            
        }

        //-----------------------------------------------
        public Bitmap GetBitmapCopie(int nTailleImage, bool bChildsOnly)
        {
            return null;
        }

        //-----------------------------------------------
        public Point GlobalToClient(Point pt)
        {
            return pt;
        }

        //-----------------------------------------------
        public Point[] GlobalToClient(Point[] pts)
        {
            return pts;
        }

        //-----------------------------------------------
        public Rectangle GlobalToClient(Rectangle rect)
        {
            return rect;
        }

        //-----------------------------------------------
        public bool IsAChildOf(I2iObjetGraphique parent, I2iObjetGraphique supposedChild)
        {
            if (parent == this)
            {
                CRepresentationExpressionGraphique rep = supposedChild as CRepresentationExpressionGraphique;
                if (rep != null)
                    return m_listeRepresentations.Contains(rep);
            }
            return false;
        }

        //-----------------------------------------------
        public bool IsChildOf(I2iObjetGraphique wnd)
        {
            return false;
        }

        //-----------------------------------------------
        public bool IsLock
        {
            get
            {
                return true;
            }
            set
            {
                
            }
        }

        //-----------------------------------------------
        public bool IsPointIn(Point pt)
        {
            return RectangleAbsolu.Contains(pt);
        }

        //-----------------------------------------------
        public event EventHandlerObjetGraphique LocationChanged;

        //-----------------------------------------------
        public Point Magnetise(Point pt)
        {
            return pt;
        }

        //-----------------------------------------------
        public bool NoDelete
        {
            get { return true; }
        }

        //-----------------------------------------------
        public bool NoMove
        {
            get { return true; }
        }

        //-----------------------------------------------
        public bool NoPoignees
        {
            get { return true; }
        }

        //-----------------------------------------------
        public bool NoRectangleSelection
        {
            get { return true; }
        }

        //-----------------------------------------------
        public void OnDesignDoubleClick(Point ptAbsolu)
        {
            
        }

        //-----------------------------------------------
        public I2iObjetGraphique Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        //-----------------------------------------------
        public Point Position
        {
            get
            {
                return new Point(0, 0);
            }
            set
            {
            }
        }

        //-----------------------------------------------
        public Point PositionAbsolue
        {
            get
            {
                return Position;
            }
            set
            {
            }
        }

        //-----------------------------------------------
        public Rectangle RectangleAbsolu
        {
            get { return new Rectangle(PositionAbsolue, Size); }
        }

        //-----------------------------------------------
        public void RepositionneChilds()
        {
            
        }

        //-----------------------------------------------
        public void ResumeLayout()
        {
            
        }

        //-----------------------------------------------
        public I2iObjetGraphique SelectionnerElementApresElement(Point pt, I2iObjetGraphique element)
        {
            List<I2iObjetGraphique> selection = SelectionnerElements(pt);
            int nIdxElement = selection.IndexOf(element);
            if (nIdxElement < selection.Count - 1)
                return selection[nIdxElement + 1];
            return null;
        }

        //-----------------------------------------------
        public I2iObjetGraphique SelectionnerElementAvantElement(Point pt, I2iObjetGraphique element)
        {
            List<I2iObjetGraphique> selection = SelectionnerElements(pt);
            selection.Reverse();
            int nIdxElement = selection.IndexOf(element);
            if (nIdxElement > 0)
                return selection[nIdxElement - 1];
            return null;
        }

        //-----------------------------------------------
        public I2iObjetGraphique SelectionnerElementConteneurDuDessus(Point pt)
        {
            I2iObjetGraphique ele = null;
            return SelectionnerElementConteneurDuDessus(pt, ele);
        }

        //-----------------------------------------------
        public I2iObjetGraphique SelectionnerElementConteneurDuDessus(Point pt, List<I2iObjetGraphique> elementsToIgnore)
        {
            List<I2iObjetGraphique> selection = SelectionnerElements(pt);
            foreach (I2iObjetGraphique ele in selection)
            {
                if (ele.AcceptChilds)
                {
                    bool bOk = true;
                    if (elementsToIgnore != null)
                    {
                        if (elementsToIgnore.Contains(ele))
                            bOk = false;
                        else
                            foreach (I2iObjetGraphique eleToIgnore in elementsToIgnore)
                                if (IsAChildOf(eleToIgnore, ele))
                                {
                                    bOk = false;
                                    break;
                                }
                    }
                    if (bOk)
                        return ele;
                }
            }
            return null;
        }

        //-----------------------------------------------
        //Ignore l'élément et ces fils
        public I2iObjetGraphique SelectionnerElementConteneurDuDessus(Point pt, I2iObjetGraphique elementToIgnore)
        {
            List<I2iObjetGraphique> selection = SelectionnerElements(pt);
            foreach (I2iObjetGraphique ele in selection)
                if (ele.AcceptChilds && (elementToIgnore == null || (!IsAChildOf(elementToIgnore, ele) && elementToIgnore != ele)))
                    return ele;
            return null;
        }

        //-----------------------------------------------
        public I2iObjetGraphique SelectionnerElementDuDessus(Point pt)
        {
            return SelectionnerElementDuDessus(pt, null);
        }

        //-----------------------------------------------
        public I2iObjetGraphique SelectionnerElementDuDessus(Point pt, I2iObjetGraphique elementToIgnore)
        {
            List<I2iObjetGraphique> selection = SelectionnerElements(pt);
            foreach (I2iObjetGraphique ele in selection)
                if (elementToIgnore == null || ele != elementToIgnore)
                    return ele;
            return null;
        }

        //-----------------------------------------------
        public List<I2iObjetGraphique> SelectionnerElements(Point pt)
        {
            List<I2iObjetGraphique> retour = new List<I2iObjetGraphique>();
            foreach (CRepresentationExpressionGraphique rep in m_listeRepresentations)
                if (rep.IsPointIn(pt))
                    retour.Add(rep);
            return retour;
        }

        //-----------------------------------------------
        public virtual bool AutoExpandFromChildren
        {
            get
            {
                return false;
            }
        }

        //-----------------------------------------------
        public Size Size
        {
            get
            {
                return m_size;
            }
            set
            {
                m_size = value;
            }
        }

        //-----------------------------------------------
        public event EventHandlerObjetGraphique SizeChanged;

        //-----------------------------------------------
        public void SuspendLayout()
        {
        }

        //-----------------------------------------------
        public I2iObjetGraphique GetCloneAMettreDansParent(I2iObjetGraphique parent, Dictionary<Type, object> dicObjetsPourCloner)
        {
            return (I2iObjetGraphique)CCloner2iSerializable.Clone(this, dicObjetsPourCloner);
        }

        //-----------------------------------------------
        public void CancelClone()
        {
        }

        //-----------------------------------------------
        public string TooltipText
        {
            get { return ""; }
        }

        //-----------------------------------------------
        public override string IdExpression
        {
            get { return "GRAF_EXP"; }
        }

        //-----------------------------------------------
        public override CTypeResultatExpression TypeDonnee
        {
            get {
                if (FormuleFinale != null)
                    return FormuleFinale.TypeDonnee;
                return new CTypeResultatExpression(typeof(string), false);

            }
        }

        //-----------------------------------------------
        protected override CResultAErreur ProtectedEval(CContexteEvaluationExpression ctx)
        {
            CResultAErreur result = CResultAErreur.True;
            if (FormuleFinale == null)
            {
                result = RefreshFormuleFinale();
                if (!result)
                    return result;
            }
            if (FormuleFinale != null)
            {
                C2iExpression formuleToEval = FormuleFinale;
                if (Debug)
                {
                    formuleToEval = new C2iExpressionDebug();
                    formuleToEval.Parametres.Add(FormuleFinale);
                }
                return formuleToEval.Eval(ctx);
            }
            result.EmpileErreur(I.T("Can not create formula from graphical|20086"));
            return result;
        }

        //-----------------------------------------------
        public override int GetNbParametresNecessaires()
        {
            return 0;
        }

        public override string GetString()
        {
            if (FormuleFinale != null)
                return FormuleFinale.GetString();
            return "";
        }

        public override CResultAErreur VerifieParametres()
        {
            return CResultAErreur.True;
        }

        //--------------------------------------------------
        public bool OnDesignerMouseDown(Point ptLocal)
        {
            return false;
        }


        //--------------------------------------------------
        /// <summary>
        /// crée les éléments graphiques d'une formule
        /// </summary>
        /// <param name="formule"></param>
        public void InitFromFormule(C2iExpression formule)
        {
            m_listeRepresentations.Clear();
            m_listeVariables.Clear();
            if (formule == null)
                return;
            ArrayList lst = formule.ExtractExpressionsType(typeof(C2iExpressionVariable));
            foreach (C2iExpressionVariable variable in lst)
            {
                if (variable.Parametres.Count > 0)
                {
                    C2iExpressionChamp champ = variable.Parametres[0] as C2iExpressionChamp;
                    if (champ != null)
                    {
                        CDefinitionProprieteDynamiqueVariableFormule def = champ.DefinitionPropriete as CDefinitionProprieteDynamiqueVariableFormule;
                        if (def != null)
                            m_listeVariables.Add(def);
                    }
                }
            }
            C2iExpressionBegin begin = formule as C2iExpressionBegin;
            m_strIdStartPoint = "";
            int nX = 100;
            int nY = 20;

            if ( begin  != null )
            {
                CRepresentationExpressionGraphique previous = null;
                foreach ( C2iExpression action in begin.Parametres )
                {
                    if ( !(action is C2iExpressionVariable ) )
                    {
                        CRepresentationExpressionGraphique rep = new CRepresentationExpressionGraphique ();
                        rep.Position = new Point ( nX, nY );
                        AddChild ( rep );
                        rep.Parent = this;
                        rep.Formule = action;
                        nY = rep.RectangleAbsolu.Bottom;
                        foreach ( CRepresentationExpressionGraphique repTmp in m_listeRepresentations )
                            if ( repTmp.RectangleAbsolu.Bottom > nY )
                                nY = rep.RectangleAbsolu.Bottom;
                        nY += 20;
                        if (previous == null)
                            StartPoint = rep;
                        else
                            previous.Next = rep;
                        previous = rep;
                    }
                }
            }
            else
            {
                CRepresentationExpressionGraphique rep = new CRepresentationExpressionGraphique();
                rep.Position = new Point ( nX, nY );
                AddChild ( rep );
                rep.Parent = this;
                rep.Formule = formule;
                StartPoint = rep;
            }
        }
    }
}
