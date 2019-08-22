using System;
using SDL2;

namespace escape_aliens.Engine
{
    public class GameWindow 
    {
        public int GetWidth {get;}
        public int GetHeight {get;}
        public bool IsFullScreen {get;}

        private IntPtr _windowPtr;
        private IntPtr _windowSurface;
        
        public GameWindow(string windowCaption) 
        {
            SDL.SDL_DisplayMode currentDisplayMode;
            if(SDL.SDL_GetCurrentDisplayMode(0, out currentDisplayMode) < 0) {
                Console.WriteLine("Unable to get current display mode. {0}", SDL.SDL_GetError());
            }
            GetHeight = currentDisplayMode.h;
            GetWidth = currentDisplayMode.w;
            IsFullScreen = true;

            _windowPtr = SDL.SDL_CreateWindow(windowCaption, 0, 0, GetWidth, GetHeight, SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);

            _windowSurface = SDL.SDL_GetWindowSurface(_windowPtr);

        }

        public IntPtr CreateRenderer() 
        {
            IntPtr renderer = SDL.SDL_CreateRenderer(_windowPtr, 0, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            return renderer;
        }
        
        public IntPtr WindowSurface
        {
            get {return _windowSurface;}
        }

    }
}