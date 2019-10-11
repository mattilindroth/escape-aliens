using SDL2;
using System;
using System.Collections.Generic;

namespace escape_aliens.Engine.Input
{
    public delegate void KeyStatusChange(SDL.SDL_Scancode ScanCode, bool isDown);

    public class KeyboardBindings {
        
        private Dictionary<SDL.SDL_Scancode, KeyStatusChange> _keyMappings;
        private byte[] _previousKeyStates;

        public KeyboardBindings() {
            int numKeys ;
            _keyMappings = new Dictionary<SDL.SDL_Scancode, KeyStatusChange>();
            IntPtr previousKeyStates = SDL.SDL_GetKeyboardState(out numKeys);
            _previousKeyStates = new byte[numKeys];
            System.Runtime.InteropServices.Marshal.Copy(previousKeyStates,_previousKeyStates, 0, numKeys);
        }

        public void AddMapping(SDL.SDL_Scancode code, KeyStatusChange keyboardEventHandler) {
            if(_keyMappings.ContainsKey(code))
                _keyMappings.Remove(code);
            _keyMappings.Add(code, keyboardEventHandler);
        }

        public void RemoveMapping(SDL.SDL_Scancode code) {
            if(_keyMappings.ContainsKey(code))
                _keyMappings.Remove(code);
        }

         public void UpdateStateAndDispatchEvents() {
             int numKeys = 0;
             IntPtr keyStatesPtr = SDL.SDL_GetKeyboardState(out numKeys);
             if(keyStatesPtr == IntPtr.Zero)
                 Console.WriteLine(SDL.SDL_GetError());
             
             byte[] keyStates = new byte[numKeys];
             System.Runtime.InteropServices.Marshal.Copy(keyStatesPtr, keyStates, 0, numKeys);

             foreach(var key in _keyMappings.Keys) {
                 int keyIndex = (int)key;
                 //if(keyStates[keyIndex] != _previousKeyStates[keyIndex]) 
                _keyMappings[key].Invoke(key, keyStates[keyIndex] == 1);
            }
            _previousKeyStates = keyStates;
        }
            
    }
}
