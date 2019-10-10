using SDL2;
using System;
using escape_aliens.Engine.Interfaces;
namespace escape_aliens
{
    public class Player : Engine.GameObject, IUpdatable 
    {
        
        private double _rotationSpd;

        public Player(){
            _rotationSpd = 0;
        }

        void IUpdatable.Update(double timeStepMilliseconds) {
            this.Transformation.RotationRadians += _rotationSpd;
        }

        public void  RotateLeft(SDL.SDL_Scancode code, bool isDown) {
            if(isDown )
                _rotationSpd = -0.06;
            else
                _rotationSpd = 0;
        }

        public void RotateRight(SDL.SDL_Scancode code, bool isDown) {
            if (isDown)
                _rotationSpd = 0.06;
            else
                _rotationSpd = 0;
        }
    }
}