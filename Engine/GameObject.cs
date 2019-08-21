using System;

namespace escape_aliens.Engine 
{
    public abstract class GameObject 
    {
        private Scene _scene;
        public GameObject(Scene scene) 
        {
            _scene = scene;
        }

        public void Render() 
        {
            
        }
    }
}
