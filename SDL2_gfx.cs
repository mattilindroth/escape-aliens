#region Using Statements
using System;
using System.Runtime.InteropServices;
using SDL2;
#endregion

namespace SDL2_gfx
{
	public static class SDL_gfx
	{
		#region SDL2_gfx# Variables

		private const string nativeLibName = "SDL2_gfx";

		#endregion


		[DllImport(nativeLibName, EntryPoint = "texturedPolygon",CallingConvention = CallingConvention.StdCall)]
		public static extern int texturedPolygon(IntPtr renderer, [In] Int16[] vx, [In] Int16[] vy, int n, IntPtr texture, int texture_dx, int texture_dy);
        
    }
}