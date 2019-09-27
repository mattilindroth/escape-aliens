using System;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine 
{
    public class Scene 
    {

        private SceneRenderer _renderer;
        private List<GameObject> _gameObjects;

        public Scene(SceneRenderer renderer) 
        {
            _renderer = renderer;
            _gameObjects = new List<GameObject>();
        }

        public void AddToScene(GameObject gameObject) {
            _gameObjects.Add(gameObject);
        }

        public void Update(double timeStep)
        {

        }

        public void Render() 
        {
            _renderer.SetColor(0,255,255, 255);
            _renderer.BeginRender();
            
            _renderer.EndRender();
        }

    }
}