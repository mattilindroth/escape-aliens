using System;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine
{
    public class ThrustComponent: Component, IRenderable
    {
        private class Particle 
        {
            public Math.Vector2D _position;
            public Math.Vector2D _speed;
            public byte r, g, b, a;
            public double _lifeTimeMilliseconds;
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
            _numParticles = numParticles;
            for(int i = 0; i < numParticles; i++) {
                Particle p = new Particle() {
                    _position = new Math.Vector2D(),
                    _speed = new Math.Vector2D(),
                    r = 255,
                    g = 255,
                    b = 0,
                    _lifeTimeMilliseconds = 2000, //2 secs
                };
                p._position.X = this.AttachedGameObjects[0].Transformation.Position.X;
                p._position.Y =  this.AttachedGameObjects[0].Transformation.Position.Y;
                //p._speed
            }
        }

        bool IRenderable.DoRender
        {
            get {return true;}
        }

        void IRenderable.Render(Renderer renderer) 
        {

        }

        int IRenderable.ZValue {
            get { return _ZValue;}
        }
    }
}