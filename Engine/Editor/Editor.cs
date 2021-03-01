using System;
using SDL2;
using escape_aliens.Engine;
using escape_aliens.Engine.MathExtra;

namespace escape_aliens.Editor
{
    public class Editor {

        Game _game ;
        BackgroundGrid _backgroundGrid;
        public Editor (Game game) {
            _game = game;
            _backgroundGrid = new BackgroundGrid(game.Scene.WorldSize, game.Scene.ViewPort);
            _game.AddObject(_backgroundGrid);
            _game.Input.MouseBindings.RegisterMouseMovementListener(_backgroundGrid.MouseMove);
        }

        public void Run()
        {
            _game.Run(65);
        }
    }
}