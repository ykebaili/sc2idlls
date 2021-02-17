using System;
using sc2i.common;
using System.Collections.Generic;
using System.Drawing;
namespace sc2i.drawing
{
    public interface I2iObjetGraphique: I2iSerializable, ICloneable
    {
        bool AcceptChilds { get; }
        bool AddChild(I2iObjetGraphique child);
        System.Collections.ArrayList AllChilds();
        void BringToFront(I2iObjetGraphique child);
        event EventHandlerChild ChildAdded;
        event EventHandlerChild ChildRemoved;
        I2iObjetGraphique[] Childs { get; }
        System.Drawing.Rectangle ClientRect { get; }
        System.Drawing.Rectangle ClientToGlobal(System.Drawing.Rectangle rect);
        System.Drawing.Point ClientToGlobal(System.Drawing.Point pt);
        System.Drawing.Point[] ClientToGlobal(System.Drawing.Point[] pts);
        object Clone();
        bool LockChilds { get; set; }
        bool ContainsChild(I2iObjetGraphique child);
        void DeleteChild(I2iObjetGraphique child);
        void Draw(CContextDessinObjetGraphique ctx);
        void DrawSelected(System.Drawing.Graphics g);
        void FrontToBack(I2iObjetGraphique child);
        System.Drawing.Bitmap GetBitmapCopie(int nTailleImage, bool bChildsOnly);
        System.Drawing.Point GlobalToClient(System.Drawing.Point pt);
        System.Drawing.Point[] GlobalToClient(System.Drawing.Point[] pts);
        System.Drawing.Rectangle GlobalToClient(System.Drawing.Rectangle rect);
        bool IsAChildOf(I2iObjetGraphique parent, I2iObjetGraphique supposedChild);
        bool IsChildOf(I2iObjetGraphique wnd);
        bool IsLock { get; set; }
        bool IsPointIn(System.Drawing.Point pt);
        event EventHandlerObjetGraphique LocationChanged;
        System.Drawing.Point Magnetise(System.Drawing.Point pt);
        bool NoDelete { get; }
        bool NoMove { get; }
        bool NoPoignees { get; }
        bool NoRectangleSelection { get; }
        void OnDesignDoubleClick(System.Drawing.Point ptAbsolu);
        I2iObjetGraphique Parent { get; set; }
        System.Drawing.Point Position { get; set; }
        System.Drawing.Point PositionAbsolue { get; set; }
        System.Drawing.Rectangle RectangleAbsolu { get; }
        void RemoveChild(I2iObjetGraphique child);
        void RepositionneChilds();
        void ResumeLayout();
        I2iObjetGraphique SelectionnerElementApresElement(System.Drawing.Point pt, I2iObjetGraphique element);
        I2iObjetGraphique SelectionnerElementAvantElement(System.Drawing.Point pt, I2iObjetGraphique element);
        I2iObjetGraphique SelectionnerElementConteneurDuDessus(System.Drawing.Point pt, I2iObjetGraphique elementToIgnore);
        I2iObjetGraphique SelectionnerElementConteneurDuDessus(System.Drawing.Point pt, System.Collections.Generic.List<I2iObjetGraphique> elementsToIgnore);
        I2iObjetGraphique SelectionnerElementConteneurDuDessus(System.Drawing.Point pt);
        I2iObjetGraphique SelectionnerElementDuDessus(System.Drawing.Point pt);
        I2iObjetGraphique SelectionnerElementDuDessus(System.Drawing.Point pt, I2iObjetGraphique elementToIgnore);
        System.Collections.Generic.List<I2iObjetGraphique> SelectionnerElements(System.Drawing.Point pt);
        CResultAErreur Serialize(C2iSerializer serializer);
        System.Drawing.Size Size { get; set; }
        //Indique que l'élément ajuste sa taille en fonction de la taille de ses enfants
        bool AutoExpandFromChildren { get; }
        event EventHandlerObjetGraphique SizeChanged;
        void SuspendLayout();


        //Appelé par les fonctions de drag and drop, pour cloner l'élément en vue de mettre la copie dans le parent demandé.
        I2iObjetGraphique GetCloneAMettreDansParent(I2iObjetGraphique parent, Dictionary<Type, object> dicObjetsPourCloner);
        
        //Indique que l'objet cloné doit être annulé. Utiliser cette méthode pour liberer des ressources
        void CancelClone();

        string TooltipText { get; }

        //Retourne true s'il faut redessiner
        bool OnDesignerMouseDown(Point ptLocal);
    }
}
