using System;
using System.Collections.Generic;
namespace escape_aliens.Engine 
{
    public abstract class GameObject 
    {

        private List<Component> _components;
        private Transformation2D _transformation;
        private Scene _scene;

        public GameObject() 
        {
            _components = new List<Component>();
            _transformation = new Transformation2D();
        }

        public Transformation2D Transformation {
            get {return _transformation;}
            set {_transformation = value;}
        }

        public Object GetComponent(Type componentType) {
            foreach(Object c in _components) {
                if(c.GetType() == componentType) {
                    return c;
                }
            }
            return null;
        }

        public void AddedToScene(Scene scene) 
        {
            _scene = scene;
        }
        
        public void AddComponent(Component component) {
            _components.Add(component);
            component.AddedToGameObject(this);
        }
    }
}
