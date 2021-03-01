using System;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine 
{
    public class Scene 
    {

        private Renderer _renderer;
        private List<IRenderable> _renderables;

        public Scene(Renderer renderer) 
        {
            if(renderer == null) throw new ArgumentNullException(nameof(renderer) + " cannot be NULL");
            _renderer = renderer;
            _renderables = new List<IRenderable>();
            WorldSize = _renderer.GetWindowSize();
            ViewPort = _renderer.GetWindowSize();
        }

        public Rectangle2D WorldSize {get;set;}
        public Rectangle2D ViewPort {get;set;}

        public Renderer Renderer {
            get {return _renderer;}
        }


        public bool Contains(IRenderable renderable) {
            return _renderables.Contains(renderable);
        }

        public void AddRenderable(IRenderable renderable) {
            _renderables.Add(renderable);
            _renderables.Sort(ZValueComparer);
        }

        public void Render() 
        {
            var transformationVector = new MathExtra.Vector2D(ViewPort.X - WorldSize.X, ViewPort.Y - WorldSize.Y);
            _renderer.SetColor(255,255,255, 255);
            _renderer.BeginRender();
            
            foreach(var renderable in _renderables) {
                if(renderable.DoRender) {
                    renderable.Transformation.Position += transformationVector;
                    renderable.Render(_renderer);
                    renderable.Transformation.Position -= transformationVector;
                }
            }
            _renderer.EndRender();
        }

        private static int ZValueComparer(IRenderable first, IRenderable second) {
            return first.ZValue - second.ZValue;
        }

    }
}