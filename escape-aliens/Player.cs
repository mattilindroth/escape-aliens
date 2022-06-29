using SDL2;
using System;
using escape_aliens.Engine.MathExtra;
using escape_aliens.Engine.Interfaces;
namespace escape_aliens
{
    public class Player : Engine.GameObject, IUpdatable , IPhysicalObject
    {
        private bool _rotateLeft, _rotateRight;

        private const double _rotationSpd = 0.06;
        private const double _spd = 26;
        private Vector2D _speed;
        private Vector2D _acceleration;

        public Player(){
            _speed = new Vector2D(0,0);
            _acceleration = new Vector2D(0,0);
            _rotateLeft = false;
            _rotateRight = false;
        }

        bool IPhysicalObject.DoUpdate
        {
            get 
            {
                return true;
            }
        }

        Vector2D IPhysicalObject.Acceleration 
        {
            get {return this._acceleration;}
        }

        Vector2D IPhysicalObject.Speed 
        {
            get {return this._speed;}
            set {this._speed = value;}
        }

        void IUpdatable.Update(double timeStepMilliseconds) {
            if(_rotateLeft)
                this.Transformation.RotationRadians -= _rotationSpd;
            if(_rotateRight)
                this.Transformation.RotationRadians += _rotationSpd;
            if(_speed.Length != 0) {
                this.Transformation.Position += (_speed * timeStepMilliseconds);
            }
        }

        public void Forward(SDL.SDL_Scancode code, bool isDown) {
            if(isDown) {
                _acceleration.X = Math.Sin(this.Transformation.RotationRadians);
                _acceleration.Y = -Math.Cos(this.Transformation.RotationRadians);
                _acceleration = _acceleration * _spd;
            } else {
                _acceleration.X = 0;
                _acceleration.Y = 0;
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

        public void Shrink(SDL.SDL_Scancode code, bool isDown) {
            if(isDown)
                this.Transformation.Size = this.Transformation.Size - 0.01;
        }
        public void Enlarge(SDL.SDL_Scancode code, bool isDown) {
            if(isDown)
                this.Transformation.Size = this.Transformation.Size + 0.01;
        }
    }
}