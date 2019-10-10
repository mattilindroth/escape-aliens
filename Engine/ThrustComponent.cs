using System;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine
{
    public class ThrustComponent: Component, IRenderable, IUpdatable
    {
        private class Particle 
        {
            public Math.Vector2D _position;
            public Math.Vector2D _speed;
            public byte r, g, b, a;
            public double _lifeTimeMilliseconds;
            public long _timeOfBirthTicks;
        }

        private List<Particle> _particles;
        private bool _isVisible;
        int _numParticles;
        private int _ZValue ;

        public ThrustComponent(int ZValue = 0) {

            _particles = new List<Particle>();
            _isVisible  = true;
            _ZValue = ZValue;
        }

        public void GenerateParticles(int numParticles) {
            if(this.AttachedGameObjects.Count == 0) {
                Console.WriteLine("No game objects attached to ThrustComponent. This component needs at least one gameObject." );
                return;
            }
            _numParticles = numParticles;
           
            for(int i = 0; i < numParticles; i++) {
                _particles.Add(GenerateOneParticle());
            }
        }

        private Particle GenerateOneParticle() 
        {
            double maxSpd = 1.0; //pixels / frame
            double minSdp = 0.5; //pixels / frame
            double maxLifeTimeMillis = 10000.0; //2 secs
            double minLifeTimeMillis = 5000.0; //0.5 secs
            double angleRads = System.Math.PI / 6; // 30 degs.
            Random rand = new Random(DateTime.Now.Millisecond);

            Particle p = new Particle() {
                _position = new Math.Vector2D(),
                _speed = new Math.Vector2D(),
                r = 255,
                g = 255,
                b = 100,
                a = 255,
            };
            p._position.X = this.AttachedGameObjects[0].Transformation.Position.X;
            p._position.Y =  this.AttachedGameObjects[0].Transformation.Position.Y;
            double spd = minSdp + ((maxSpd - minSdp) * rand.NextDouble()); 
            double angle = -angleRads + (2 * angleRads * rand.NextDouble()); 
            angle = angle + this.AttachedGameObjects[0].Transformation.RotationRadians + System.Math.PI;
            p._speed.X = System.Math.Cos(angle);
            p._speed.Y = System.Math.Sin(angle);
            p._speed = p._speed * spd;
            p._lifeTimeMilliseconds = minLifeTimeMillis + ((maxLifeTimeMillis - minLifeTimeMillis) * rand.NextDouble());
            p._timeOfBirthTicks = DateTime.Now.Ticks;
            return p;
        }

        void IUpdatable.Update(double timeStepMilliseconds)
        {
            for(int i = 0; i < _particles.Count; i++) {
                var p = _particles[i];
                // long age = (long)((p._timeOfBirthTicks / 10000) + timeStepMilliseconds); //10000 ticks in a ms
                // if(p._lifeTimeMilliseconds > age) {
                //     _particles.Remove(p);
                //     p = GenerateOneParticle();
                //     _particles.Add(p);
                // }
                p._position += (/*timeStepMilliseconds **/ p._speed);

                if(p.b > 10) 
                    p.b = (byte)(p.b - 10);
                if(p.b > 0)
                    p.b = 0;
                if(p.g > 110)
                    p.g = (byte)(p.g - 10);
                if (p.g > 100)
                    p.g  = 100;
            }
        }

        bool IRenderable.DoRender
        {
            get {return _isVisible;}
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