using System;
using SDL2;

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