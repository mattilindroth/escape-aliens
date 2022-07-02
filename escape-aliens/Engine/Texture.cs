using System;
using SDL2;

namespace escape_aliens.Engine 
{
    public class Texture 
    {
        IntPtr _sdlTexture;
        int _bitmapWidth, _bitmapHeight;
        Color[,] _bitmap;

        SDL.SDL_Rect _sourceRectangle;
        SDL.SDL_Rect _renderRectangle;

        public Texture(IntPtr texture, System.Drawing.Bitmap bitmap) {
            _sdlTexture = texture;
            if(bitmap != null) {
                _bitmap = new Color[bitmap.Width,bitmap.Height];
                _bitmapWidth = bitmap.Width;
                _bitmapHeight = bitmap.Height;
                for(int x = 0; x < bitmap.Width; x++) {
                    for (int y = 0; y < bitmap.Height; y++) {
                        System.Drawing.Color c = bitmap.GetPixel(x, y);
                        _bitmap[x, y] = new Color(c.A, c.R, c.G, c.B);
                    }
                }
            }
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

        public int BitmapWidth { get {return _bitmapWidth;}}
        public int BitmapHeight { get {return _bitmapHeight;}}
        
        public Color[,] BitmapColorArray
        {
            get {return _bitmap;}
            set {_bitmap = value;}
        }

    }    
}