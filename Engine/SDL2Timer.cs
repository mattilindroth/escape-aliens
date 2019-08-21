using System;
using escape_aliens.Engine.Interfaces;
using SDL2;

namespace escape_aliens.Engine {
    public class SDL2Timer : ITimer {

        private uint _start;

        void ITimer.Start() 
        {
            _start = SDL.SDL_GetTicks();
        }

        uint ITimer.GetElapsedTicks() {
            return SDL.SDL_GetTicks() - _start;
        }

        void ITimer.Sleep(uint ticks) {
            SDL.SDL_Delay(ticks / 10000);
        }
    }
}