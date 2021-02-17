using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using System.ComponentModel;
using sc2i.expression;
using System.Drawing.Design;

namespace sc2i.formulaire.datagrid
{
    public class C2iWndDataGridColumn : C2iWnd, I2iSerializable
    {
        private string m_strLabel = "";
        private int m_nColumnWidth = 100;
        private bool m_bMultiThread = false;
        private bool m_bCanFilter = true;
        private bool m_bCanSort = true;
        private C2iExpression m_formuleElementEdite = null;

        //------------------------------------
        public C2iWndDataGridColumn()
        {
        }

        //------------------------------------
        public string Text
        {
            get
            {
                return m_strLabel;
            }
            set
            {
                m_strLabel = value;
            }
        }

        //------------------------------------
        public int ColumnWidth
        {
            get
            {
                return m_nColumnWidth;
            }
            set
            {
                m_nColumnWidth = value;
            }
        }

        //------------------------------------
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
        public C2iExpression AlternativeEditedElement
        {
            get
            {
                return m_formuleElementEdite;
            }
            set
            {
                m_formuleElementEdite = value;
            }
        }

        //------------------------------------
        public bool MultiThread
        {
            get
            {
                return m_bMultiThread;
            }
            set
            {
                m_bMultiThread = value;
            }
        }

        //------------------------------------
        public bool AllowSort
        {
            get
            {
                return m_bCanSort && !MultiThread;
            }
            set
            {
                m_bCanSort = value;
            }
        }

        //------------------------------------
        public bool AllowFilter
        {
            get
            {
                return m_bCanFilter && !MultiThread;
            }
            set
            {
                m_bCanFilter = value;
            }

        }


        //------------------------------------
        [Browsable(false)]
        public C2iWnd Control
        {
            get
            {
                if (Childs.Count() > 0)
                    return Childs[0] as C2iWnd;
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    while (Childs.Count() != 0)
                        RemoveChild(Childs[0]);

                    AddChild(value);
                    Size = new Size(Size.Width, value.Size.Height);
                }
            }
        }

        //------------------------------------
        public override System.Drawing.Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        //------------------------------------
        public override void RemoveChild(sc2i.drawing.I2iObjetGraphique child)
        {
            C2iWndDataGrid grid = Parent as C2iWndDataGrid;
            if (grid != null)
                grid.RemoveChild(this);
        }

        //------------------------------------
        public override void DeleteChild(sc2i.drawing.I2iObjetGraphique child)
        {
            RemoveChild(child);
        }

        //------------------------------------
        private int GetNumVersion()
        {
            return 3;
            //1 : ajout de multithread
            //2 : CanFilter et CanSort
            //3 : ajout alternative edited element
        }

        //------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strLabel);
            serializer.TraiteInt(ref m_nColumnWidth);
            if ( nVersion >= 1 )
                serializer.TraiteBool(ref m_bMultiThread);
            if (nVersion >= 2)
            {
                serializer.TraiteBool(ref m_bCanSort);
                serializer.TraiteBool(ref m_bCanFilter);
            }
            if (nVersion >= 3)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleElementEdite);
            if (!result)
                return result;

            return result;
        }

        
        //------------------------------------
        public override bool CanBeUseOnType(Type tp)
        {
            return false;
        }

        //------------------------------------
        public override bool AcceptChilds
        {
            get
            {
                return false;
            }
        }

        


        //------------------------------------
        public override EDockStyle DockStyle
        {
            get
            {
                return EDockStyle.Top;
            }
            set
            {
            }
        }

        //------------------------------------
        public override void Draw(sc2i.drawing.CContextDessinObjetGraphique ctx)
        {
            if (Control != null && Control.Parent != this)
            {
                Control.Parent = this;
                RepositionneChilds();
            }
            base.Draw(ctx);
        }

        //------------------------------------
        private bool m_bIsRepositionneChild = false;
        protected override void MyRepositionneChilds()
        {
            if (m_bIsRepositionneChild)
                return;
            m_bIsRepositionneChild = true;
            C2iWnd control = Control;
            if (control != null)
            {
                control.Size = new Size(ClientSize.Width * 2 / 3, ClientSize.Height);
                control.Position = new Point(ClientSize.Width / 3, 0);
            }
            m_bIsRepositionneChild = false;
        }

        //------------------------------------
        public override void DrawInterieur(sc2i.drawing.CContextDessinObjetGraphique ctx)
        {
            Rectangle rct = new Rectangle(0, 0, Size.Width, Size.Height);
            Brush br = new SolidBrush ( BackColor );
            ctx.Graphic.FillRectangle(br, rct);
            br.Dispose();
            br = new SolidBrush(ForeColor);
            rct = new Rectangle(0, 0, Size.Width / 3, Size.Height);
            ctx.Graphic.DrawString(m_strLabel, Font, br, rct);
            br.Dispose();
        }

        /// ///////////////////////////////////////
        public override CObjetPourSousProprietes GetObjetAnalysePourFils(CObjetPourSousProprietes objetRacine)
        {
            C2iWndDataGrid grid = Parent as C2iWndDataGrid;
            if (m_formuleElementEdite != null)
                return m_formuleElementEdite.TypeDonnee.TypeDotNetNatif;
            if (grid != null)
            {
                if (grid.SourceFormula != null)
                    return grid.SourceFormula.TypeDonnee.TypeDotNetNatif;
            }
            return base.GetObjetAnalysePourFils(objetRacine);
        }

        /// ///////////////////////////////////////
        public override void OnDesignSelect(Type typeEdite, object objetEdite, sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            C2iWndDataGrid grid = Parent as C2iWndDataGrid;
            if (grid != null)
            {
                if (grid.SourceFormula != null)
                    CProprieteExpressionEditor.ObjetPourSousProprietes = grid.SourceFormula.TypeDonnee.TypeDotNetNatif;
            }
        }
    }
}
