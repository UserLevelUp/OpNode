using System;
using System.Runtime.InteropServices;

namespace pWordLib.Win32
{
	/// <summary>
	/// Summary description for Win32.
	/// </summary>


		public class Win32 
		{
			[DllImport("user32.dll", CharSet=CharSet.Auto)]
			public static extern int MessageBox(int hWnd, String text, 
				String caption, uint type);



			public const int GMMP_USE_DISPLAY_POINTS     = 1;
			public const int GMMP_USE_HIGH_RESOLUTION_POINTS = 2;

			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)] // For GetMouseMovePointsEx

			public struct MOUSEMOVEPOINT 
			{
				public int x ;            //Specifies the x-coordinate of the mouse
				public int y ;            //Specifies the x-coordinate of the mouse
				public int time ;             //Specifies the time stamp of the mouse coordinate
				public IntPtr dwExtraInfo;        //Specifies extra information associated with this coordinate. 
			}

			[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern int GetMouseMovePointsEx(
				uint  cbSize,
				[In] ref MOUSEMOVEPOINT pointsIn,
				[Out] MOUSEMOVEPOINT[] pointsBufferOut,
				int nBufPoints,
				uint resolution
				);



		}




}

