using System;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CWaitCursor.
	/// </summary>
	public class CWaitCursor : IDisposable
	{
		private Cursor m_oldCursor;

		public CWaitCursor()
		{
			m_oldCursor = Cursor.Current;
            if (m_oldCursor == Cursors.WaitCursor)
            {
                m_oldCursor = null;
                return;
            }
			Cursor.Current = Cursors.WaitCursor;
		}

		public void Dispose()
		{
            if ( m_oldCursor != null )
			    Cursor.Current = m_oldCursor;
			GC.SuppressFinalize(this);
		}
	}
}
