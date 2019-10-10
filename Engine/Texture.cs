using System;
using SDL2;

namespace escape_aliens.Engine 
{
    public class Texture 
    {
        IntPtr _sdlTexture;
        SDL.SDL_Rect _sourceRectangle;
        SDL.SDL_Rect _renderRectangle;

        public Texture(IntPtr texture) {
            _sdlTexture = texture;
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
    }    
}