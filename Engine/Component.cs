using System;
using System.Collections.Generic;


namespace escape_aliens.Engine 
{
    public abstract class Component 
    {
        private List<GameObject> _gameObjects;

        public Component() 
        {
            _gameObjects = new List<GameObject>();
        }

        public List<GameObject> AttachedGameObjects {get {return _gameObjects;}}

        public void AddedToGameObject(GameObject gameObject) 
        {
            _gameObjects.Add(gameObject);
        }

    }
}