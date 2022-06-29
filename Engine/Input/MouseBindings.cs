using SDL2;
using System;
using System.Collections.Generic;

namespace escape_aliens.Engine.Input 
{
    public enum eMouseButton
    {
        Left,
        Right,
        Middle,
        Any
    }
    public delegate void MouseMove(int x, int y, int dx, int dy, int dw);
    public delegate void MouseButtonStatusChange(eMouseButton button, bool isDown );
    public class MouseBindings {
        private List<MouseMove> _mouseMoveReceivers;
        private List<MouseButtonStatusChange> _mouseButtonReceivers;
        private int _prevMouseX, _prevMouseY; 
        public MouseBindings() {
            _mouseMoveReceivers = new List<MouseMove>();
            _mouseButtonReceivers = new List<MouseButtonStatusChange>();
            _prevMouseX = -1;
            _prevMouseY = -1;
        }

        public void RegisterMouseMovementListener(MouseMove mouseMoveDelegate) {
            _mouseMoveReceivers.Add(mouseMoveDelegate);
        }

        public void UnregisterMouseMovementListener(MouseMove mouseMoveDelegate) {
            _mouseMoveReceivers.Remove(mouseMoveDelegate);
        }

        public void RegisterMouseButtonListener(MouseButtonStatusChange buttonStatusChange) {
            _mouseButtonReceivers.Add(buttonStatusChange);
        }

        public void UnregisterMouseButtonListener(MouseButtonStatusChange buttonStatusChange) {
            _mouseButtonReceivers.Remove(buttonStatusChange);
        }

        internal void UpdateStateAndDispatchEvents(int dw) {
            int x, y;
            SDL.SDL_GetMouseState(out x, out y);
            if(_prevMouseY < 0 || _prevMouseX < 0) {
                _prevMouseY = y;
                _prevMouseX = x;
                return;
            }
            if((_prevMouseX - x) != 0 || (_prevMouseY - y) != 0 || dw != 0) {
                DispatchMouseMovement(x, y, dw);
                _prevMouseX = x;
                _prevMouseY = y;
            }
        }

        private void DispatchMouseMovement(int x, int y, int dw) {
            foreach(var receiver in _mouseMoveReceivers) {
                receiver.Invoke( x, y, _prevMouseX - x, _prevMouseY - y, dw);
            }
        }

        internal void UpdateAndDispatchMouseButton(eMouseButton button, bool isDown) {
            foreach(var receiver in _mouseButtonReceivers) {
                receiver.Invoke(button, isDown);
            }
        }
    }
}