using System;
using SDL2;

namespace escape_aliens.Engine
{
    public class GameWindow 
    {
        public int GetWidth {get;}
        public int GetHeight {get;}
        public bool IsFullScreen {get;}

        private IntPtr _windowPtr = IntPtr.Zero;
        private IntPtr _windowSurface = IntPtr.Zero;
        
        public GameWindow(string windowCaption) 
        {
            SDL.SDL_DisplayMode currentDisplayMode;
            if(SDL.SDL_GetCurrentDisplayMode(0, out currentDisplayMode) < 0) {
                Console.WriteLine("Unable to get current display mode. {0}", SDL.SDL_GetError());
            }
            GetHeight = currentDisplayMode.h;
            GetWidth = currentDisplayMode.w;
            IsFullScreen = true;

            _windowPtr = SDL.SDL_CreateWindow(windowCaption, 3, 27, GetWidth/2, GetHeight/2, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE); //SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);

            //_windowSurface = SDL.SDL_GetWindowSurface(_windowPtr);

        }

        public IntPtr CreateRenderer() 
        {
            IntPtr renderer = SDL.SDL_CreateRenderer(_windowPtr, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if(renderer == null || renderer == IntPtr.Zero) {
                Console.WriteLine("Renderer pointer is zero!. {0}", SDL.SDL_GetError());
            }
            return renderer;
        }
        
        // public IntPtr WindowSurface
        // {
        //     get {return _windowSurface;}
        // }

    }
}