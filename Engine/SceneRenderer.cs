using System;
using SDL2;

namespace escape_aliens.Engine 
{
    public class SceneRenderer 
    {
        private GameWindow _window;
        private IntPtr _screenSurface;
        private IntPtr _renderer; 
        public SceneRenderer(GameWindow window) 
        {
            _window = window;
            _screenSurface = window.WindowSurface;
            _renderer = window.CreateRenderer();
        }

       
        public void DrawLine(int x1, int y1, int x2, int y2) 
        {
            SDL.SDL_RenderDrawLine(_renderer, x1, y1, x2, y2);
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

    }
}