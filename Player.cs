using SDL2;
using System;

namespace escape_aliens
{
    public class Player : Engine.GameObject
    {
        
        public void  RotateLeft(SDL.SDL_Keycode code, bool isDown) {
            Console.WriteLine("Rotating left");
            this.Transformation.RotationRadians -= 0.1;
        }

        public void RotateRight(SDL.SDL_Keycode code, bool isDown) {
            Console.WriteLine("Rotating Right");
            this.Transformation.RotationRadians += 0.1;
        }

    }
}