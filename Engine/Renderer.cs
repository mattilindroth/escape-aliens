using System;
using SDL2;
using SDL2_gfx;

namespace escape_aliens.Engine 
{
    public class Renderer 
    {
        private GameWindow _window;
        // private IntPtr _screenSurface;
        private IntPtr _renderer; 
        private const double _rad2deg = (180.0f / System.Math.PI);
        public Renderer(GameWindow window) 
        {
            _window = window;
            _renderer = window.CreateRenderer();
        }

        public Texture LoadTexture(string imgSource) {
            IntPtr texture = SDL_image.IMG_LoadTexture(_renderer, imgSource);
            if(texture == IntPtr.Zero)
                Console.WriteLine("Could not load texture. {0}", SDL.SDL_GetError());
            return new Texture(texture);
        }
        
        public Image LoadImage(string imgSource) {
            
            IntPtr surfacePtr = SDL_image.IMG_Load(imgSource);
            if(surfacePtr == IntPtr.Zero) {
                Console.WriteLine("Could not load image. {0}", SDL.SDL_GetError());
            }
            uint pxlFormatEnum = SDL.SDL_GetWindowPixelFormat(this._window.SDL_Window);
            IntPtr pxlFormat = SDL.SDL_AllocFormat(pxlFormatEnum);
            IntPtr convertedSurface = SDL.SDL_ConvertSurface(surfacePtr, pxlFormat, 0);
            SDL.SDL_FreeSurface(surfacePtr);
            return new Image(convertedSurface);
        }

        public Rectangle2D<int> GetWindowSize() 
        {
            return new Rectangle2D<int>(0, 0, _window.GetWidth, _window.GetHeight);
        }

        public void DrawPixel(int x, int y, Transformation2D transformation) {
            SDL.SDL_RenderDrawPoint(_renderer,x + (int)transformation.Position.X, y + (int)transformation.Position.Y);
        }

        public void DrawLine(int x1, int y1, int x2, int y2) 
        {
            SDL.SDL_RenderDrawLine(_renderer, x1, y1, x2, y2);
        }

        public void TexturedPolygon( int[] x, int[] y, IntPtr image, int dx, int dy)
        {
            Int16[] xs = new Int16[x.Length];
            Int16[] ys = new Int16[x.Length];
            for(int i = 0 ; i < x.Length ; i++){
                xs[i] = (Int16)x[i];
                ys[i] = (Int16)y[i];
            }
            SDL2_gfx.SDL_gfx.texturedPolygon(_renderer, xs, ys, x.Length, image, dx, dy);
        }

        public void DrawLines(Point2D[] points, Transformation2D transformation)
        {
            SDL.SDL_FPoint[] SDLPoints = new SDL.SDL_FPoint[points.Length];
            int index = 0;
            foreach(var p in points) {

                SDLPoints[index].x = (float)p.X * (float)transformation.Size + (float)transformation.Position.X;
                SDLPoints[index].y = (float)p.Y * (float)transformation.Size + (float)transformation.Position.Y;

            }
            SDL.SDL_RenderDrawLinesF(_renderer, SDLPoints, points.Length);
        }

        public void DrawTexture(Texture texture, Transformation2D transformation, bool flip) 
        {
            SDL.SDL_Point center;
            SDL.SDL_Rect sourceRect = texture.SourceRectangle;
            SDL.SDL_Rect renderRect = texture.RenderRectangle;
            double angleDegrees = transformation.RotationRadians * _rad2deg;
            renderRect.w = (int)(renderRect.w * transformation.Size);
            renderRect.h = (int)(renderRect.h * transformation.Size);
            renderRect.x = (int)transformation.Position.X;
            renderRect.y = (int)transformation.Position.Y;
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