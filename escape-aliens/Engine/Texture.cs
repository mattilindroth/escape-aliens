using System;
using SDL2;

namespace escape_aliens.Engine 
{
    public class Texture 
    {
        IntPtr _sdlTexture;
        IntPtr _sdlSurface;

        SDL.SDL_Rect _sourceRectangle;
        SDL.SDL_Rect _renderRectangle;

        public Texture(IntPtr texture, IntPtr surface) {
            _sdlTexture = texture;
            _sdlSurface = surface;
        }     

        public IntPtr SDLTexture
        {
            get {return _sdlTexture;}
            set {_sdlTexture = value;}
        }

        public SDL.SDL_Rect RenderRectangle {
            get { return _renderRectangle; }
            set { _renderRectangle = value; }
        }

        public SDL.SDL_Rect SourceRectangle {
            get {return _sourceRectangle;}
            set {_sourceRectangle = value;}
        }

        public IntPtr SDLSurface
        {
            get {return _sdlSurface;}
            set {_sdlSurface = value;}
        }

    }    
}