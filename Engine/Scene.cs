using System;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine 
{
    public class Scene 
    {

        private IRenderer _renderer;
        public Scene(IRenderer renderer) 
        {
            _renderer = renderer;
        }


        public void Update(double timeStep) 
        {

        }

        public void Render() 
        {

        }

    }
}