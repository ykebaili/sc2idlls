using System;

namespace sc2i.drawing
{
	/// <summary>
	/// Description résumée de C2iObjetGraphiqueSansChilds.
	/// </summary>
	public abstract class C2iObjetGraphiqueSansChilds : C2iObjetGraphique
	{
		/// ////////////////////////////////////////////////////////
		public C2iObjetGraphiqueSansChilds()
		{
		}

		/// ////////////////////////////////////////////////////////
		public override I2iObjetGraphique[] Childs
		{
			get
			{
				return new I2iObjetGraphique[0];
			}
		}

		/// ////////////////////////////////////////////////////////
		public override bool AddChild(I2iObjetGraphique child)
		{
			return false;
		}

		/// ////////////////////////////////////////////////////////
		public override bool ContainsChild(I2iObjetGraphique child)
		{
			return false;
		}


		/// ////////////////////////////////////////////////////////
		public override void RemoveChild(I2iObjetGraphique child)
		{
		}


		public override void BringToFront(I2iObjetGraphique child)
		{
			
		}
		public override void FrontToBack(I2iObjetGraphique child)
		{
			
		}


	}
}
