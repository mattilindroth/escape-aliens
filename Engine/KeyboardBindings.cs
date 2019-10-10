using SDL2;
using System;
using System.Collections.Generic;

namespace escape_aliens.Engine 
{
    public delegate void KeyStatusChange(SDL.SDL_Keycode KeyCode, bool isDown);

    public class KeyboardBindings {
        
        private Dictionary<SDL.SDL_Keycode, KeyStatusChange> _keyMappings;
        private int[] _previousKeyStates;

        public KeyboardBindings() {
            int numKeys ;
            _keyMappings = new Dictionary<SDL.SDL_Keycode, KeyStatusChange>();
            IntPtr previousKeyStates = SDL.SDL_GetKeyboardState(out numKeys);
            _previousKeyStates = new int[numKeys];
            System.Runtime.InteropServices.Marshal.Copy(previousKeyStates,_previousKeyStates, 0, numKeys);
        }

        public void AddMapping(SDL.SDL_Keycode code, KeyStatusChange keyboardEventHandler) {
            if(_keyMappings.ContainsKey(code))
                _keyMappings.Remove(code);
            _keyMappings.Add(code, keyboardEventHandler);
        }

        public void RemoveMapping(SDL.SDL_Keycode code) {
            if(_keyMappings.ContainsKey(code))
                _keyMappings.Remove(code);
        }

        public void ExperimentalKeyHandling(SDL.SDL_Event e) {
            SDL.SDL_Keycode keyCode = e.key.keysym.sym;
            if(_keyMappings.ContainsKey(keyCode))
                _keyMappings[keyCode].Invoke(keyCode, e.type == SDL.SDL_EventType.SDL_KEYDOWN);
            switch (e.key.keysym.sym)
                {
                    
                }        
        }

         public void UpdateStateAndDispatchEvents() {
             int numKeys = 0;
             IntPtr currentKeyStates = SDL.SDL_GetKeyboardState(out numKeys);
             if(currentKeyStates == IntPtr.Zero)
                 Console.WriteLine(SDL.SDL_GetError());
             int[] keyStates = new int[numKeys];
             System.Runtime.InteropServices.Marshal.Copy(currentKeyStates, keyStates, 0, numKeys);
          // Console.WriteLine("Number of keys {0}. Key state of A is {1}", numKeys, keyStates[(int)SDL.SDL_Scancode.SDL_SCANCODE_A]);

             for(int i = 0; i < numKeys; i++) {
                 if(keyStates[i] == 1) { //_previousKeyStates[i] != 
                     SDL.SDL_Scancode code = (SDL.SDL_Scancode)i;
                     Console.WriteLine("Button {0} is down", code.ToString());
                     //_keyMappings[key].Invoke(key, keyStates[(int)key] == 1);
                 }
             }
             _previousKeyStates = keyStates;
         }
    }
}