using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;

namespace sc2i.win32.common
{
	#region Helper Classes

	public enum ChangedTypes { GeneralInvalidate = 0, SubItemChanged = 1, ItemChanged = 2, ItemCollectionChanged = 3, ColumnChanged = 4, ColumnCollectionChanged = 5, FocusedChanged = 6 };

	public class ChangedEventArgs : EventArgs
	{
		private string				m_strColumnName = "";
		private ChangedTypes		m_ctType = ChangedTypes.GeneralInvalidate;

		public ChangedEventArgs( ChangedTypes ctType, string strColumnName )
		{
			m_strColumnName = strColumnName;
			m_ctType = ctType;
		}

		public string ColumnName
		{
			get	{ return m_strColumnName; }
		}

		public ChangedTypes ChangedType
		{
			get { return m_ctType; 	}
		}
	}


	public class GLStringHelpers
	{
		/// <summary>
		/// 
		/// 
		/// this function also handles truncation of multiline strings
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="nWidth"></param>
		/// <param name="subDC"></param>
		/// <param name="font"></param>
		/// <returns></returns>
		public static string TruncateString( string strText, int nWidth, Graphics subDC, Font font )
		{
			//DW("TuncateString");
			string strTruncated = "";

			SizeF sizeString = MeasureMultiLineString( strText, subDC, font );
			if ( sizeString.Width < nWidth )
				return strText;				// this doesnt need any work, bail out

			int strTDotSize;
			strTDotSize = (int)subDC.MeasureString( "...", font ).Width;
			if ( strTDotSize > nWidth )
				return "";					// Cant even fit the triple dots here


			StringReader r = new StringReader(strText); 
			string line; 
			while ((line = r.ReadLine()) != null) 
			{
				if ( subDC.MeasureString( line, font ).Width < nWidth )
				{	// original sub line is fine, doesn't need truncation
					strTruncated += line + "\n";
				}
				else
				{	// sub line needs to be truncated
					for ( int index=line.Length; index!=0; index-- )
					{
						string tmpString;
						tmpString = line.Substring( 0, index ) + "...";

						//DW("Truncating string to " + strText );

						if ( subDC.MeasureString( tmpString, font ).Width < nWidth )
						{
							strTruncated += tmpString + "\n";
							break;			// stop the for loop so we can test more strings
						}
					}
				}
			}

			// remove the trailing linefeed for the last line in a sequence (because its not needed and woudl possibly mess things up
			if ( strTruncated.Length > 1 )
				strTruncated.Remove( strTruncated.Length-1, 1 );

			return strTruncated;
		}

		/// <summary>
		/// Measure a multi lined string
		/// </summary>
		/// <param name="strText"></param>
		/// <param name="mDC"></param>
		/// <param name="font"></param>
		/// <returns></returns>
		public static SizeF MeasureMultiLineString( string strText, Graphics mDC, Font font )
		{
			StringReader r = new StringReader(strText); 
			SizeF sizeStr = new SizeF(0,0);

			string line; 
			while ((line = r.ReadLine()) != null) 
			{
				SizeF tsize = mDC.MeasureString( line, font );

				sizeStr.Height += tsize.Height;
				if ( sizeStr.Width < tsize.Width )
					sizeStr.Width = tsize.Width;
			}

			return sizeStr;
		}



		public static StringAlignment ConvertContentAlignmentToVerticalStringAlignment( ContentAlignment alignment )
		{
			StringAlignment sa = StringAlignment.Near;

			switch ( alignment )
			{
				case ContentAlignment.TopLeft:
				case ContentAlignment.TopCenter:
				case ContentAlignment.TopRight:
				{
					sa = StringAlignment.Near;
					break;
				}

				case ContentAlignment.MiddleLeft:
				case ContentAlignment.MiddleCenter:
				case ContentAlignment.MiddleRight:
				{
					sa = StringAlignment.Center;
					break;
				}

				case ContentAlignment.BottomLeft:
				case ContentAlignment.BottomCenter:
				case ContentAlignment.BottomRight:
				{
					sa = StringAlignment.Far;
					break;
				}
			}

			return sa;
		}

		public static StringAlignment ConvertContentAlignmentToHorizontalStringAlignment( ContentAlignment alignment )
		{
			StringAlignment sa = StringAlignment.Near;

			switch ( alignment )
			{
				case ContentAlignment.TopLeft:
				case ContentAlignment.MiddleLeft:
				case ContentAlignment.BottomLeft:
				{
					sa = StringAlignment.Near;
					break;
				}

				case ContentAlignment.TopCenter:
				case ContentAlignment.MiddleCenter:
				case ContentAlignment.BottomCenter:
				{
					sa = StringAlignment.Center;
					break;
				}

				case ContentAlignment.TopRight:
				case ContentAlignment.MiddleRight:
				case ContentAlignment.BottomRight:
				{
					sa = StringAlignment.Far;
					break;
				}
			}

			return sa;
		}


	}

	public class ClickEventArgs : EventArgs
	{
		private int m_nItemIndex;
		private int m_nColumnIndex;

		public ClickEventArgs( int itemindex, int columnindex )
		{
			m_nItemIndex = itemindex;
			m_nColumnIndex = columnindex;
		}

		public int ItemIndex
		{
			get
			{
				return m_nItemIndex;
			}
		}
		public int ColumnIndex
		{
			get
			{
				return m_nColumnIndex;
			}
		}
	}


	#endregion
}
