using SDL2;
using System;
using escape_aliens.Engine.MathExtra;
using escape_aliens.Engine.Interfaces;
namespace escape_aliens
{
    public class Player : Engine.GameObject, IUpdatable 
    {
        private bool _rotateLeft, _rotateRight;

        private const double _rotationSpd = 0.06;
        private double _spd;

        public Player(){
            _spd = 0;
            _rotateLeft = false;
            _rotateRight = false;
        }

        void IUpdatable.Update(double timeStepMilliseconds) {
            if(_rotateLeft)
                this.Transformation.RotationRadians -= _rotationSpd;
            if(_rotateRight)
                this.Transformation.RotationRadians += _rotationSpd;
            if(_spd != 0) {
                Vector2D spd = new Vector2D();
                spd.X = System.Math.Sin(this.Transformation.RotationRadians);
                spd.Y = -System.Math.Cos(this.Transformation.RotationRadians);
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
            var thrustComponent = (escape_aliens.Engine.ThrustComponent)this.GetComponent(typeof(escape_aliens.Engine.ThrustComponent));
            thrustComponent.ToggleThrust(isDown);
            
        }

        public void  RotateLeft(SDL.SDL_Scancode code, bool isDown) {
            _rotateLeft = isDown;
            if(_rotateRight && _rotateLeft)
                _rotateRight = false;
        }

        public void RotateRight(SDL.SDL_Scancode code, bool isDown) {
            _rotateRight = isDown;
            if(_rotateLeft && _rotateRight)
                _rotateLeft = false;
        }
    }
}