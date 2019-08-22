using System;
using SDL2;
using escape_aliens.Engine;

namespace escape_aliens
{
    class Program
    {
        static void Main(string[] args)
        {
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0) {
                Console.WriteLine("Unable to initialize SDL. Error: {0}", SDL.SDL_GetError());
            }
            var window = new GameWindow("Escape Aliens!");
            Console.WriteLine("Window is {0}x{1}", window.GetWidth, window.GetHeight);
            var scene = new Scene(new Engine.SceneRenderer(window));
            var game = new Game(scene, new SDL2Timer());

            //game.Run(65);
            // var window = IntPtr.Zero;                        
            // window = SDL.SDL_CreateWindow(".NET Core SDL2-CS Tutorial",
            //     SDL.SDL_WINDOWPOS_CENTERED,
            //     SDL.SDL_WINDOWPOS_CENTERED,
            //     1440,
            //     900,
            //     SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN
            // );

            // if (window == IntPtr.Zero)
            // {
            //     Console.WriteLine("Unable to create a window. SDL. Error: {0}", SDL.SDL_GetError());  
            // }

            // // SDL.SDL_Delay(5000);
            // SDL.SDL_Event e;
            // bool quit = false;                        
            // while (!quit)
            // {
            //     while (SDL.SDL_PollEvent(out e) != 0)
            //     {
            //         switch (e.type)
            //         {
            //            case SDL.SDL_EventType.SDL_QUIT:
            //                quit = true;
            //                break;
            //             case SDL.SDL_EventType.SDL_KEYDOWN:
            //                 switch (e.key.keysym.sym)
            //                 {
            //                     case SDL.SDL_Keycode.SDLK_q:
            //                         quit = true;
            //                         break;
            //                 }
            //                 break;
            //         }
            //     }
            // }
            // SDL.SDL_DestroyWindow(window);    
            SDL.SDL_Quit();
        }
    }
}
