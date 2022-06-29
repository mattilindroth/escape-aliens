using System;
using SDL2;
using escape_aliens.Engine;
using escape_aliens.Engine.Input;
using escape_aliens.Engine.MathExtra;
using System.Diagnostics;

namespace escape_aliens.Editor
{
    public enum eEditorMouseMode
    {
        None,
        MoveViewport,
        MoveObject,
    }
    public class Editor {

        Game _game ;
        BackgroundGrid _backgroundGrid;

        private eEditorMouseMode _editorMouseMode;

        public Editor (Game game) {
            _editorMouseMode = eEditorMouseMode.None;
            _game = game;
            _backgroundGrid = new BackgroundGrid(game.Scene.WorldSize, game.Scene.ViewPort);
            _game.AddObject(_backgroundGrid);
            _game.Input.MouseBindings.RegisterMouseMovementListener(_backgroundGrid.MouseMove);
            _game.Input.MouseBindings.RegisterMouseMovementListener(this.MouseMove);
            _game.Input.MouseBindings.RegisterMouseButtonListener(this.MouseButtonStatusChange);
            _game.Input.KeyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_KP_PLUS, this.KeyStatusChange);
            _game.Input.KeyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_KP_MINUS, this.KeyStatusChange);
        }

        public void MouseButtonStatusChange(eMouseButton button, bool isDown)
        {
            if(isDown) {
                if(button == eMouseButton.Left) {
                    _editorMouseMode = eEditorMouseMode.MoveViewport;
                }
            } else {
                _editorMouseMode = eEditorMouseMode.None;
            }
        }
        public void MouseMove(int x, int y, int dx, int dy, int dw)
        {
            if(_editorMouseMode == eEditorMouseMode.MoveViewport) {
                _game.Scene.ViewPort.X -= dx;
                _game.Scene.ViewPort.Y -= dy; 
            }
            if (dw != 0)
                _game.Scene.ViewportZoom += 0.05 * dw;
        }

        public void KeyStatusChange(SDL.SDL_Scancode ScanCode, bool isDown)
        {
            if(isDown) {
                switch(ScanCode) {
                    case SDL.SDL_Scancode.SDL_SCANCODE_KP_MINUS:
                    _backgroundGrid.DecreaseSpecing();
                    break;
                    case SDL.SDL_Scancode.SDL_SCANCODE_KP_PLUS:
                    _backgroundGrid.IncreaSpacing();
                    break;
                }
            }
        }

        public void Run()
        {
            _game.Run(65);
        }
    }
}