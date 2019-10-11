using System;
using SDL2;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine
{
    public class ThrustComponent: Component, IRenderable, IUpdatable
    {
        private class Particle 
        {
            public MathExtra.Vector2D _position;
            public MathExtra.Vector2D _speed;
            public byte r, g, b, a;
            public double _lifeTimeMilliseconds;
            public long _timeOfBirthTicks;
        }

        private List<Particle> _particles;
        private bool _isVisible;
        int _numParticles;
        private int _ZValue ;
        private bool _thrustOn;

        public ThrustComponent(int ZValue = 0) {

            _particles = new List<Particle>();
            _isVisible  = true;
            _ZValue = ZValue;
            _thrustOn = false;
        }

        public void GenerateParticles(int numParticles) {
            if(this.AttachedGameObjects.Count == 0) {
                Console.WriteLine("No game objects attached to ThrustComponent. This component needs at least one gameObject." );
                return;
            }
            _numParticles = numParticles;
           
            for(int i = 0; i < numParticles; i++) {
                _particles.Add(GenerateOneParticle(MathExtra.Singletons.Random()));
            }
        }

        public void ToggleThrust(bool isOn) {
            _thrustOn = isOn;
        }

        private Particle GenerateOneParticle(Random rand) 
        {
            double maxSpd = 5.0; //pixels / frame
            double minSdp = 2.0; //pixels / frame
            double maxLifeTimeMillis = 700.0; 
            double minLifeTimeMillis = 500.0;
            double angleRads = System.Math.PI / 6; // 30 degs.

            Particle p = new Particle() {
                _position = new MathExtra.Vector2D(),
                _speed = new MathExtra.Vector2D(),
                r = 255,
                g = 255,
                b = 100,
                a = 255,
            };
            p._position.X = this.AttachedGameObjects[0].Transformation.Position.X + 45;
            p._position.Y =  this.AttachedGameObjects[0].Transformation.Position.Y + 45;
            double spd = minSdp + ((maxSpd - minSdp) * rand.NextDouble()); 
            double angle = -angleRads + (2 * angleRads * rand.NextDouble()); 
            angle = angle + this.AttachedGameObjects[0].Transformation.RotationRadians + (System.Math.PI / 2);
            p._speed.X = System.Math.Cos(angle);
            p._speed.Y = System.Math.Sin(angle);
            p._speed = p._speed * spd;
            p._lifeTimeMilliseconds = minLifeTimeMillis + ((maxLifeTimeMillis - minLifeTimeMillis) * rand.NextDouble());
            p._position += p._speed;
            p._timeOfBirthTicks = DateTime.Now.Ticks;
            return p;
        }

        void IUpdatable.Update(double timeStepMilliseconds)
        {
            if(_thrustOn) {
                while(_particles.Count < _numParticles) {
                    var p = GenerateOneParticle(MathExtra.Singletons.Random());
                    _particles.Add(p);
                }
            }

            for(int i = 0; i < _particles.Count; i++) {
                var p = _particles[i];
                double age = ((DateTime.Now.Ticks - p._timeOfBirthTicks) / 10000); //10000 ticks in a ms
                if(p._lifeTimeMilliseconds < age ) {
                    _particles.Remove(p);
                }
                p._position += (/*timeStepMilliseconds **/ p._speed);

                 if(p.b > 10) 
                     p.b = (byte)(p.b - 1);
                 if(p.b > 0)
                     p.b = 0;
                 if(p.g > 110)
                     p.g = (byte)(p.g - 1);
                 if (p.g > 100)
                     p.g  = 100;
            }
        }

        bool IRenderable.DoRender
        {
            get 
            {
                _isVisible = _particles.Count > 0;
                return _isVisible;
            }
        }

        void IRenderable.Render(Renderer renderer) 
        {
            foreach(var p in _particles) {
                renderer.SetColor(p.r, p.g, p.b, p.a);
                renderer.DrawPixel((int)p._position.X, (int)p._position.Y);
            }
        }

        int IRenderable.ZValue {
            get { return _ZValue;}
        }
    }
}