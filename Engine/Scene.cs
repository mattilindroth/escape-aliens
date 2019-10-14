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
            _renderer = renderer;
            _renderables = new List<IRenderable>();
        }

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
            _renderer.SetColor(255,255,255, 255);
            _renderer.BeginRender();
            
            foreach(var renderable in _renderables) {
                if(renderable.DoRender)
                    renderable.Render(_renderer);
            }
            _renderer.EndRender();
        }

        private static int ZValueComparer(IRenderable first, IRenderable second) {
            return first.ZValue - second.ZValue;
        }

    }
}