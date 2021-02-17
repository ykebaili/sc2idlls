using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using sc2i.expression;
using sc2i.common;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace sc2i.expression
{
    [Serializable]
    public class CRepresentationExpressionGraphique : C2iObjetGraphique
    {
        private C2iExpression m_formule = null;
        private string m_strLastErreur = null;
        private string m_strId = Guid.NewGuid().ToString();
        private string m_strIdNext = "";
        private string m_strIdPrev = "";

        //Liste des paramètres sortis : point sur les ids des éléments graphiques liés
        private Dictionary<int, string> m_dicParametresGraphiques = new Dictionary<int, string>();

        //----------------------------------
        public CRepresentationExpressionGraphique()
            : base()
        {
            Size = new Size(80, 50);
        }

        //----------------------------------
        public CRepresentationExpressionGraphique(C2iExpression formule)
        {
            Size = new Size(80, 50);
            m_formule = formule;
        }

        //----------------------------------
        public string LastErreur
        {
            get
            {

                return m_strLastErreur == null?"":m_strLastErreur;
            }
            set
            {
                m_strLastErreur = value;
            }
        }

        //----------------------------------
        public C2iExpression Formule
        {
            get
            {
                return m_formule;
            }
            set
            {
                m_formule = value;
                CResultAErreur result = m_formule.VerifieParametres();
                if (!result)
                    LastErreur = result.Erreur.ToString();
                else
                    LastErreur = "";
                C2iExpressionGraphique rep = RepresentationRacine;
                if (value is C2iExpressionBegin && rep != null)
                {
                    CRepresentationExpressionGraphique prev = this;
                    if (value.Parametres2i.Count() < 1)
                        m_formule = new C2iExpressionNull();
                    else
                        m_formule = value.Parametres2i[0];
                    int nLength = value.Parametres.Count;
                    for ( int nParametre = 1; nParametre < nLength; nParametre++ )
                    {
                        CRepresentationExpressionGraphique ext = prev.Next;
                        if (ext == null)
                        {
                            ext = new CRepresentationExpressionGraphique();
                            Point pt = Position;
                            if (prev == null)
                                pt.Offset(2 * Size.Width, 0);
                            else
                            {
                                pt = prev.Position;
                                pt.Offset(0, 2 * prev.Size.Height);
                            }
                            ext.Position = pt;
                            rep.AddChild(ext);
                            ext.Parent = rep;
                        }
                        ext.Formule = value.Parametres[nParametre] as C2iExpression;
                        if (prev != null)
                            prev.Next = ext;
                        prev = ext;
                    }
                    if (prev != null)
                        prev.Next = null;
                    foreach (KeyValuePair<int, string> kv in m_dicParametresGraphiques.ToArray())
                    {
                        if (kv.Key >= value.Parametres.Count)
                            m_dicParametresGraphiques.Remove(kv.Key);
                    }
                    if (rep != null)
                        rep.OnChangeFormule(this);

                }
                else
                {
                    
                    if (rep != null)
                    {
                        foreach (KeyValuePair<int, string> kv in m_dicParametresGraphiques.ToArray())
                        {
                            if (kv.Key >= 0 && kv.Key < m_formule.Parametres.Count)
                            {
                                CRepresentationExpressionGraphique exp = rep.GetFormule(kv.Value);
                                if (exp != null)
                                    exp.Formule = m_formule.Parametres2i[kv.Key];
                            }
                            else
                                SetExterne(kv.Key, null);
                        }
                        rep.OnChangeFormule(this);
                    }
                }

            }
        }

        //----------------------------------
        public C2iExpression FormuleFinale
        {
            get
            {
                if (Prev != null)
                    return Prev.FormuleFinale;
                if (Next == null)
                    return Formule;
                C2iExpressionBegin begin = new C2iExpressionBegin();
                CRepresentationExpressionGraphique next = this;
                while (next != null)
                {
                    begin.Parametres.Add(next.Formule);
                    next = next.Next;
                }
                return begin;
            }
        }

        //----------------------------------
        public override bool AcceptChilds
        {
            get
            {
                return true;
            }
        }

        //----------------------------------
        public override I2iObjetGraphique[] Childs
        {
            get { return new I2iObjetGraphique[0]; }
        }

        //----------------------------------
        public override bool AddChild(I2iObjetGraphique child)
        {
            return false;
        }

        //----------------------------------
        public override bool ContainsChild(I2iObjetGraphique child)
        {
            return false;
        }

        //----------------------------------
        public override void RemoveChild(I2iObjetGraphique child)
        {
            
        }

        //----------------------------------
        public override void BringToFront(I2iObjetGraphique child)
        {
            
        }

        //----------------------------------
        public override void FrontToBack(I2iObjetGraphique child)
        {
            
        }

        //----------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------
        public string Id
        {
            get
            {
                return m_strId;
            }
        }

        //-----------------------------------------------------------
        public string[] IdElementsUtilises
        {
            get
            {
                return m_dicParametresGraphiques.Values.ToArray();
            }
        }

        //-----------------------------------------------
        public bool IsUtilisateur(string strId)
        {
            return m_dicParametresGraphiques.Values.Contains(strId);
        }

        //-----------------------------------------------
        protected void ClearExternes()
        {
            foreach (KeyValuePair<int, string> kv in m_dicParametresGraphiques.ToArray())
            {
                SetExterne(kv.Key, null);
            }
            m_dicParametresGraphiques.Clear();
        }

        //-----------------------------------------------
        public void StopUseExterne(CRepresentationExpressionGraphique exp)
        {
            OnChangeFormule(exp);
            foreach (KeyValuePair<int, string> kv in m_dicParametresGraphiques.ToArray())
            {
                if (kv.Value == exp.Id)
                {
                    m_dicParametresGraphiques.Remove(kv.Key);
                }
            }
        }


        //-----------------------------------------------
        public void OnChangeFormule(CRepresentationExpressionGraphique exp)
        {
            foreach (KeyValuePair<int, string> kv in m_dicParametresGraphiques)
            {
                if (kv.Value == exp.Id)
                {
                    if (Formule != null)
                        Formule.SetParametre(kv.Key, exp.FormuleFinale);
                }
            }
        }
                

        //-----------------------------------------------
        public void SetExterne(int nNumParametre, CRepresentationExpressionGraphique exp)
        {
            if (exp != null)
            {
                while (exp.Prev != null)
                    exp = exp.Prev;
                m_dicParametresGraphiques[nNumParametre] = exp.Id;
                if (m_formule != null)
                    m_formule.SetParametre(nNumParametre, exp.FormuleFinale);
            }
            else
            {
                C2iExpressionGraphique rep = RepresentationRacine;
                CRepresentationExpressionGraphique oldExt = GetExterne(nNumParametre);
                if (m_dicParametresGraphiques.ContainsKey(nNumParametre))
                    m_dicParametresGraphiques.Remove(nNumParametre);
                if (oldExt != null && oldExt.Next == null && oldExt.Prev == null)
                {
                    if (rep != null && rep.GetUtilisateurs(oldExt.Id).Count() == 0)
                    {
                        rep.RemoveChild(oldExt);
                        oldExt.ClearExternes();
                    }
                }
            }
        }

        //-----------------------------------------------
        public CRepresentationExpressionGraphique GetExterne(int nNumParametre)
        {
            string strId = null;
            if ( m_dicParametresGraphiques.TryGetValue ( nNumParametre, out strId ))
            {
                C2iExpressionGraphique rep = RepresentationRacine;
                if ( rep != null )
                    return rep.GetFormule ( strId );
            }
            return null;
        }


        //-----------------------------------------------
        public CRepresentationExpressionGraphique Next
        {
            get
            {
                C2iExpressionGraphique rep = RepresentationRacine;
                if (rep != null)
                    return rep.GetFormule(m_strIdNext);
                return null;
            }
            set
            {
                if (value != null)
                {
                    //Vérifie qu'il n'y a pas une boucle
                    CRepresentationExpressionGraphique prev = Prev;
                    while (prev != null)
                    {
                        if (prev == value)
                            return;
                        prev = prev.Prev;
                    }
                }
                if (Next != null)
                    Next.Prev = null;
                m_strIdNext = value == null ? "" : value.Id;
                if (value != null && value.Prev != this)
                    value.Prev = this;
                C2iExpressionGraphique rep = RepresentationRacine;
                if (rep != null)
                {
                    rep.OnChangeFormule(this);
                }
            }
        }

        //-----------------------------------------------
        public CRepresentationExpressionGraphique Prev
        {
            get
            {
                C2iExpressionGraphique rep = RepresentationRacine;
                if (rep != null)
                    return rep.GetFormule(m_strIdPrev);
                return null;
            }
            protected set
            {
                m_strIdPrev = value == null?"" : value.Id;
                if ( value != null && value.Next != this )
                    value.Next = this;
            }
        }

        //-----------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<C2iExpression>(ref m_formule);

            int nNb = m_dicParametresGraphiques.Count();
            serializer.TraiteString(ref m_strId);
            serializer.TraiteString(ref m_strIdNext);
            serializer.TraiteString(ref m_strIdPrev);
            serializer.TraiteInt(ref nNb);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (KeyValuePair<int, string> kv in m_dicParametresGraphiques)
                    {
                        int nVal = kv.Key;
                        string strVal = kv.Value;
                        serializer.TraiteInt(ref nVal);
                        serializer.TraiteString(ref strVal);
                    }
                    break;

                case ModeSerialisation.Lecture:
                    m_dicParametresGraphiques.Clear();
                    for (int i = 0; i < nNb; i++)
                    {
                        int nVal = 0;
                        string strVal = "";
                        serializer.TraiteInt(ref nVal);
                        serializer.TraiteString(ref strVal);
                        m_dicParametresGraphiques[nVal] = strVal;
                    }
                    break;
            }

            return result;
        }

        //-------------------------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            CDessineurExpressionGraphique dessinateur = CDessineurExpressionGraphique.GetDessineur(this);
            if (dessinateur != null)
                dessinateur.DrawExpression(ctx, this);
        }

        //-----------------------------------------------
        public C2iExpressionGraphique RepresentationRacine
        {
            get
            {
                I2iObjetGraphique parent = Parent;
                while (parent != null && !(parent is C2iExpressionGraphique))
                {
                    parent = parent.Parent;
                }
                return parent as C2iExpressionGraphique;
            }
        }

        //-----------------------------------------------
        public CResultAErreur RefreshParametres()
        {
            CResultAErreur result = CResultAErreur.True;
            foreach (KeyValuePair<int, string> kv in m_dicParametresGraphiques)
            {

                CRepresentationExpressionGraphique rep = RepresentationRacine.GetFormule(kv.Value);
                CRepresentationExpressionGraphique graf = rep;
                while ( graf != null )
                {
                    result = graf.RefreshParametres();
                    if (!result)
                        return result;
                    if (graf.LastErreur != "")
                    {
                        result.EmpileErreur(graf.LastErreur);
                        return result;
                    }
                    else
                    {
                        if (graf.Formule == null)
                        {
                            LastErreur = I.T("Bad parameter @1|20104", kv.Key.ToString());
                            result.EmpileErreur(LastErreur);
                            return result;
                        }
                    }
                    graf = graf.Next;
                }
                m_formule.SetParametre(kv.Key,rep.FormuleFinale);
            }
            return result;
        }
       
    }
}
