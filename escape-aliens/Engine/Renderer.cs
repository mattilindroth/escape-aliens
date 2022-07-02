using System;
using SDL2;

namespace escape_aliens.Engine 
{
    public class Renderer 
    {
        private GameWindow _window;
        private IntPtr _renderer; 
        private const double _rad2deg = (180.0f / System.Math.PI);
        public Renderer(GameWindow window) 
        {
            _window = window;
            _renderer = window.CreateRenderer();
            if((int)SDL_image.IMG_InitFlags.IMG_INIT_PNG != SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG))
            {
                Console.WriteLine("Error initializing SDL_IMG");
            }
        }

        internal int WindowWidth {get {return _window.GetWidth;}}
        internal int WindowHeight {get {return _window.GetHeight;}}

        private IntPtr LoadSurface(string imgSource) {
            IntPtr SDLsurface = SDL_image.IMG_Load( imgSource );
            return SDLsurface;
        }

        public Texture LoadTexture(string imgSource, bool createBitmap) {
            IntPtr texture = SDL_image.IMG_LoadTexture(_renderer, imgSource);
            System.Drawing.Bitmap bitmap = null;
            if(texture == IntPtr.Zero)
                Console.WriteLine("Could not load texture. {0}", SDL.SDL_GetError());
            if (createBitmap)
                bitmap = new System.Drawing.Bitmap(imgSource);
            // if(createSurface) {
            //     surface = LoadSurface( imgSource );
            // }
            return new Texture(texture, bitmap);
        }

        // public Color GetSurfacePixelColor(IntPtr surface, int x, int y) {
        //     var surfStruct = System.Marshal.PtrToStructure(surface, typeof(SDL_Surface));
        //     var pixelFormat = System.Marshal.PtrToStructure(surfStruct.format, typeof(SDL_PixelFormat));

        //     int bpp = pixelFormat.BytesPerPixel;
            
        //     /* Here p is the address to the pixel we want to retrieve */
        //     int p = surfStruct.pixels + y * surfStruct.pitch + x * bpp;

        //     switch (bpp)
        //     {
        //         case 1:
        //             return *p;
        //             break;

        //         case 2:
        //             return *(Uint16 *)p;
        //             break;

        //         case 3:
        //             if (SDL_BYTEORDER == SDL_BIG_ENDIAN)
        //                 return p[0] << 16 | p[1] << 8 | p[2];
        //             else
        //                 return p[0] | p[1] << 8 | p[2] << 16;
        //             break;

        //         case 4:
        //             return *(Uint32 *)p;
        //             break;

        //         default:
        //             return 0;       /* shouldn't happen, but avoids warnings */
        //     }
        // }
        

        public void DrawPixel(int x, int y) {
            SDL.SDL_RenderDrawPoint(_renderer,x, y);
        }

        public void DrawLine(int x1, int y1, int x2, int y2) 
        {
            SDL.SDL_RenderDrawLine(_renderer, x1, y1, x2, y2);
        }

        public void DrawTexture(Texture texture, Transformation2D transformation, bool flip) 
        {
            SDL.SDL_Point center;
            SDL.SDL_Rect sourceRect = texture.SourceRectangle;
            SDL.SDL_Rect renderRect = texture.RenderRectangle;
            double angleDegrees = transformation.RotationRadians * _rad2deg;
            renderRect.w = (int)(renderRect.w * transformation.Size);
            renderRect.h = (int)(renderRect.h * transformation.Size);
            renderRect.x = (int)(transformation.Position.X + (renderRect.w / 2));
            renderRect.y = (int)(transformation.Position.Y + (renderRect.h / 2));
            
            center.x = (renderRect.w) / 2;
            center.y = (renderRect.h) / 2;
            SDL.SDL_RendererFlip renderFlip = flip ? SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL : SDL.SDL_RendererFlip.SDL_FLIP_NONE;
            SDL.SDL_RenderCopyEx(_renderer, texture.SDLTexture, ref sourceRect, ref renderRect, angleDegrees, ref center, renderFlip);
        }

        public void DrawRectangle(int x, int y, int width, int height)
        {
            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = width;
            rect.h = height;
            SDL.SDL_RenderDrawRect(_renderer, ref rect);
        }

        public void SetColor(byte r, byte g, byte b, byte a) 
        {
            SDL.SDL_SetRenderDrawColor(_renderer, r, g, b, a);
        } 

        public void SetColor(Color c) {
            SDL.SDL_SetRenderDrawColor(_renderer, c.R, c.G, c.B, c.A);
        }

        internal void BeginRender()
        {
            SDL.SDL_RenderClear(_renderer);
        }

        internal void EndRender()
        {
            SDL.SDL_RenderPresent(_renderer);
        }
    }
}