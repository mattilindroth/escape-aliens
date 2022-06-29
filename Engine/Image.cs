using System;
using SDL2;

namespace escape_aliens.Engine
{
    public class Image {
        private IntPtr _surface;
        public Image (IntPtr surface) 
        {
            _surface = surface;
        }

        public IntPtr SDL_Surface {
            get {return _surface;}
            set {_surface = value;}
        }
    }
}