using System;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine 
{
    public class Scene 
    {

        private Renderer _renderer;
        private List<IRenderable> _renderables;
        private List<IUpdatable> _updatables;
        private List<GameObject> _gameObjects;

        public Scene(Renderer renderer) 
        {
            _renderer = renderer;
            _gameObjects = new List<GameObject>();
            _renderables = new List<IRenderable>();
            _updatables = new List<IUpdatable>();
        }

        public Renderer Renderer {
            get {return _renderer;}
        }

        public void AddGameObject(GameObject gameObject) {
            _gameObjects.Add(gameObject);
        }

        public void AddRenderable(IRenderable renderable) {
            _renderables.Add(renderable);
            _renderables.Sort(ZValueComparer);
        }

        public void AddUpdatable(IUpdatable updatable) {
            _updatables.Add(updatable);
        }

        public void Update(double timeStep)
        {
            foreach(var updatable in _updatables) {
                updatable.Update(timeStep);
            }
        }

        public void Render() 
        {
            _renderer.SetColor(0,255,255, 255);
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