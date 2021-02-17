using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using System.Drawing;
using sc2i.data;
using sc2i.common;
using sc2i.expression;
using System.Drawing.Design;

namespace sc2i.process.workflow.dessin
{
    public class CWorkflowEtapeDessin : C2iObjetGraphique, IWorflowDessin
    {
        private CTypeEtapeWorkflow m_typeEtape = null;
        private string m_strIdUniverselEtape = "";

        private Color m_backColor = Color.White;
        private Color m_foreColor = Color.Black;

        private Font m_font = null;

        //------------------------------------------------------
        public CWorkflowEtapeDessin()
            :base()
        {
            Size = new Size(25, 20);
        }

        //------------------------------------------------------
        public string Text
        {
            get
            {
                if (TypeEtape != null)
                    return TypeEtape.Libelle;
                return "";
            }
            set
            {
                if (TypeEtape != null)
                    TypeEtape.Libelle = value;
            }
        }

        //---------------------------------------------------
        public virtual bool Autoexec
        {
            get
            {
                if (TypeEtape != null)
                    return TypeEtape.ExecutionAutomatique;
                return false;
            }
            set
            {
                if (TypeEtape != null)
                    TypeEtape.ExecutionAutomatique = value;
            }
        }

        //------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public string IdUniversel
        {
            get
            {
                return m_strIdUniverselEtape;
            }
        }

        //------------------------------------------------------
        public Color ForeColor
        {
            get
            {
                return m_foreColor;
            }
            set
            {
                m_foreColor = value;
            }
        }

        //------------------------------------------------------
        public Color BackColor
        {
            get
            {
                return m_backColor;
            }
            set
            {
                m_backColor = value;
            }
        }

        //------------------------------------------------------
        public Font Font
        {
            get
            {
                return m_font;
            }
            set
            {
                m_font = value;
            }
        }

        //------------------------------------------------------
        public override I2iObjetGraphique[] Childs
        {
            get
            {
                return new I2iObjetGraphique[0];
            }
        }

        //------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public string IdUniverselEtape
        {
            get
            {
                return m_strIdUniverselEtape;
            }
            set
            {
                m_strIdUniverselEtape = value;
                m_typeEtape = null;
            }
        }

        //------------------------------------------------------
        public string StepId
        {
            get
            {
                return IdUniverselEtape;
            }
        }

        //------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public CTypeEtapeWorkflow TypeEtape
        {
            get
            {
                if (m_typeEtape == null)
                {
                    CWorkflowDessin dessin = Parent as CWorkflowDessin;
                    if (dessin != null && dessin.TypeWorkflow != null)
                    {
                        CListeObjetsDonnees lst = dessin.TypeWorkflow.Etapes;
                        lst.Filtre = new CFiltreData(CObjetDonnee.c_champIdUniversel + "=@1",
                            IdUniverselEtape);
                        if (lst.Count > 0)
                            m_typeEtape = lst[0] as CTypeEtapeWorkflow;
                    }
                }
                return m_typeEtape;
            }
            set
            {
                if (value != null)
                {
                    m_typeEtape = value;
                    IdUniverselEtape = m_typeEtape.IdUniversel;
                }
                else
                {
                    m_typeEtape = null;
                    IdUniverselEtape = "";
                }
            }
        }

        //------------------------------------------------------
        public override bool AcceptChilds
        {
            get
            {
                return false;
            }
        }

        //------------------------------------------------------
        public override bool AddChild(I2iObjetGraphique child)
        {
            return false;
        }

        //------------------------------------------------------
        public override bool ContainsChild(I2iObjetGraphique child)
        {
            return false;
        }

        //------------------------------------------------------
        public override void RemoveChild(I2iObjetGraphique child)
        {
            
        }

        //------------------------------------------------------
        public override void BringToFront(I2iObjetGraphique child)
        {
            
        }

        //------------------------------------------------------
        public override void FrontToBack(I2iObjetGraphique child)
        {
            
        }

        //------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------------
        protected override sc2i.common.CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strIdUniverselEtape);
            if ( serializer.Mode == ModeSerialisation.Lecture  && m_strIdUniverselEtape.Length > 0)
            {
                serializer.TrackDbKeyReaded(m_strIdUniverselEtape);

            }
            if (serializer.Mode == ModeSerialisation.Lecture)
                m_typeEtape = null;

            int nCol = m_backColor.ToArgb();
            serializer.TraiteInt(ref nCol);
            m_backColor = Color.FromArgb(nCol);

            nCol = m_foreColor.ToArgb();
            serializer.TraiteInt(ref nCol);
            m_foreColor = Color.FromArgb(nCol);

            
            return result;
        }

        //------------------------------------------------------
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            if (TypeEtape != null)
                TypeEtape.Bloc.Draw(ctx, this);
        }

        //------------------------------------------------------
        public Point[] GetPolygoneDessin()
        {
            if (TypeEtape != null)
                return TypeEtape.Bloc.GetPolygoneDessin(this);
            return new Point[]{
                new Point ( Position.X, Position.Y ),
                new Point ( Position.X + Size.Width, Position.Y ),
                new Point ( Position.X + Size.Width, Position.Y + Size.Height),
                new Point ( Position.X, Position.Y + Size.Height )};
        }

        //------------------------------------------------------
        public CResultAErreur Delete()
        {
            CResultAErreur result = CResultAErreur.True;
            //Suppression des liens
            if (TypeEtape != null && Parent != null)
            {
                //Vérifie qu'aucun autre objet ne référence ce type d'étape
                foreach (I2iObjetGraphique objet in Parent.Childs)
                {
                    if (objet != this)
                    {
                        CWorkflowEtapeDessin etapeExistante = objet as CWorkflowEtapeDessin;
                        if (etapeExistante != null && etapeExistante.IdUniverselEtape == IdUniverselEtape)
                            return result;
                    }
                }

                result = TypeEtape.CanDelete();
                if (!result)
                    return result;
                //Supprime les objets lien
                CWorkflowDessin dessin = Parent as CWorkflowDessin;
                if (dessin != null)
                {
                    foreach (CWorkflowLienDessin lien in dessin.GetLiensEntrants(this))
                        dessin.RemoveChild(lien);
                    foreach (CWorkflowLienDessin lien in dessin.GetLiensSortants(this))
                        dessin.RemoveChild(lien);
                }
                result = TypeEtape.Delete(true);
            }
            return result;
        }

        //------------------------------------------------------
        [System.ComponentModel.Editor(typeof(CParametresInitialisationEtapeEditor), typeof(UITypeEditor))]
        public CParametresInitialisationEtape Initializations
        {
            get
            {
                if (TypeEtape != null)
                    return TypeEtape.ParametresInitialisation;
                return new CParametresInitialisationEtape();
            }
            set
            {
                if (TypeEtape != null)
                    TypeEtape.ParametresInitialisation = value;
            }
        }

       

        //------------------------------------------------------
        [System.ComponentModel.Editor(typeof(CDefinisseurEvenementEditor), typeof(UITypeEditor))]
        public CDefinisseurEvenementsEditable EventsSetup
        {
            get
            {
                if (TypeEtape != null)
                    return new CDefinisseurEvenementsEditable(TypeEtape);
                return null;
            }
            set
            {
            }
        }
        


        //----------------------------------------------
        ///Ne sert à rien
        /*public bool AsynchronousMode
        {
            get
            {
                if (TypeEtape != null)
                    return TypeEtape.ModeAsynchrone;
                return false;
            }
            set
            {
                if (TypeEtape != null)
                    TypeEtape.ModeAsynchrone = value;
            }
        }*/

        
    }
}
