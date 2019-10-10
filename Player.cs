using SDL2;
using System;

namespace escape_aliens
{
    public class Player : Engine.GameObject
    {
        
        public void  RotateLeft(SDL.SDL_Scancode code, bool isDown) {
            this.Transformation.RotationRadians -= 0.1;
        }

        public void RotateRight(SDL.SDL_Scancode code, bool isDown) {
            this.Transformation.RotationRadians += 0.1;
        }

    }
}