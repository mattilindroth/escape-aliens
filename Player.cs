using SDL2;
using System;
using escape_aliens.Engine.Math;
using escape_aliens.Engine.Interfaces;
namespace escape_aliens
{
    public class Player : Engine.GameObject, IUpdatable 
    {
        
        private double _rotationSpd;
        private double _spd;

        public Player(){
            _rotationSpd = 0;
            _spd = 0;
        }

        void IUpdatable.Update(double timeStepMilliseconds) {
            this.Transformation.RotationRadians += _rotationSpd;
            if(_spd != 0) {
                Vector2D spd = new Vector2D();
                spd.X = System.Math.Cos(this.Transformation.RotationRadians);
                spd.Y = System.Math.Sin(this.Transformation.RotationRadians);
                spd = spd * _spd;
                this.Transformation.Position += spd;
            }
        }

        public void Forward(SDL.SDL_Scancode code, bool isDown) {
            if(isDown) {
                _spd = 1;
            } else {
                _spd = 0;
            }
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