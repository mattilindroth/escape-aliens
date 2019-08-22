using System;
using System.Collections.Generic;

 namespace escape_aliens.Engine
 {
     public class EventDispatcher 
     {
        private List<ComponentRenderer> _componentRenderers;
        private List<ComponentInputListener> _componentInputListeners;

        public EventDispatcher() 
        {
            _componentRenderers = new List<ComponentRenderer>();
            _componentInputListeners = new List<ComponentInputListener>();
        }

        public void DispatchRender(SceneRenderer renderer) 
        {
            foreach(var componentRenderer in _componentRenderers)  
            {
                componentRenderer.RenderComponent(renderer);
            }
        }

        public void DispatchInputEvent(KeyboardState keyboardState) 
        {
            foreach(var inputListener in _componentInputListeners)
            {
                inputListener.HandleInput(keyboardState);
            }
        }

        public void  AddRenderer(ComponentRenderer componentRenderer)
        {
            _componentRenderers.Add(componentRenderer);
        }

        public void AddInputListener(ComponentInputListener listener) {
            _componentInputListeners.Add(listener);
        }

        public void RemoveRenderer(ComponentRenderer componentRenderer)
        {
            _componentRenderers.Remove(componentRenderer);
        }

        public void RemoveInputListener(ComponentInputListener inputListener) {
            _componentInputListeners.Remove(inputListener);
        }

     }
 }