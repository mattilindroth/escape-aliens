using System;
using System.Collections.Generic;
namespace escape_aliens.Engine 
{
    public abstract class GameObject 
    {

        private List<Component> _components;
        private Scene _scene;
        public GameObject() 
        {
            _components = new List<Component>();
            this.AddComponent(new Math.Transformation2D());
        }

        public void AddedToScene(Scene scene) 
        {
            _scene = scene;
        }
        
        void AddComponent(Component component) {
            _components.Add(component);
            component.AddedToGameObject(this);
        }
    }
}
