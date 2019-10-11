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
            var scene = new Scene(new Engine.Renderer(window));
            var game = new Game(scene, new SDL2Timer());
            game.LoadResource();

            game.Run(65);
            
            SDL.SDL_Quit();
        }
    }
}
