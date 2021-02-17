using System;
namespace sc2i.formulaire.win32.editor
{
	interface ICSelectionElementsFormulaire
	{
		void Add(C2iWnd wnd);
		void Clear();
		CPanelEditionFormulaire ControlParent { get; }
		int Count { get; }
		void Draw(System.Drawing.Graphics g, bool bAvecPoignees);
		void MouseDown(System.Drawing.Point pt);
		void MouseMove(System.Drawing.Point pt);
		void MouseUp(System.Drawing.Point pt);
		System.Drawing.Rectangle RectangleEnglobant { get; }
		void RemoveAt(int n);
		event EventHandler SelectionChanged;
		C2iWnd this[int n] { get; set; }
	}
}
