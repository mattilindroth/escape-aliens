using System;
using SDL2;

namespace escape_aliens.Engine 
{
    public class KeyboardState
    {
        public KeyboardState() 
        {
            
        }

        IntPtr GetKeyboardState() {
            int numKeys;
            return SDL.SDL_GetKeyboardState( out numKeys );
        }

    }
}