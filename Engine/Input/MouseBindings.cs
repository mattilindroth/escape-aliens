using SDL2;
using System;
using System.Collections.Generic;

namespace escape_aliens.Engine.Input 
{
    public delegate void MouseMove(int x, int y);
    public delegate void MouseButtonStatusChange( bool isDown );
    public class MouseBindings {
        private List<MouseMove> _mouseMoveReceivers;

        public MouseBindings() {
            _mouseMoveReceivers = new List<MouseMove>();
        }

        public void RegisterMouseMovementListener(MouseMove mouseMoveDelegate) {
            _mouseMoveReceivers.Add(mouseMoveDelegate);
        }

        public void UnregisterMouseMovementListener(MouseMove mouseMoveDelegate) {
            _mouseMoveReceivers.Remove(mouseMoveDelegate);
        }

        internal void DispatchMouseMovement(int x, int y) {
            foreach(var receiver in _mouseMoveReceivers) {
                receiver.Invoke(x,y);
            }
        }
    }
}