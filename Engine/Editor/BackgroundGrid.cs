using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine.Input;
using escape_aliens.Engine;
using System;

namespace escape_aliens.Editor
{
    public class BackgroundGrid : GameObject, IRenderable {
        
        Transformation2D _transformation;

        int _minSpacing = 10;
        int _maxSpacing = 100;
        int _spacingStep = 10;
        int _spacing;
        SDL2Timer _blinkTimer;
        bool _timerUp;

        Rectangle2D _worldSize;
        Rectangle2D _viewPort;
        Point2D _mouseLocation;

        public BackgroundGrid(Rectangle2D worldSize, Rectangle2D viewPort)
        {
            _spacing = (_maxSpacing - _minSpacing)/2;
            IsVisible = true;
            _transformation = new Transformation2D();
            _viewPort = viewPort;
            _worldSize = worldSize;
            _blinkTimer = new SDL2Timer();
            _mouseLocation = new Point2D(0,0);
        }

        public void IncreaSpacing()
        {
            _spacing = Math.Min(Spacing + _spacingStep, _maxSpacing);
        }

        public void DecreaseSpecing()
        {
            _spacing = Math.Max(Spacing - _spacingStep, _minSpacing);
        }

        public int Spacing {get {return _spacing;}}

        public void MouseMove(int x, int y, int dx, int dy)
        {
            _mouseLocation.X = x;
            _mouseLocation.Y = y;
        }

        public void MouseButtonStatusChange(eMouseButton button, bool isDown)
        {

        }

        public bool IsVisible {get;set;}

        public void Render(Renderer renderer) 
        {
            int n = (int)(_worldSize.Y - _viewPort.Y) / _spacing;
            int firstY = n * _spacing;
            n = (int)(_worldSize.X - _viewPort.X) / _spacing;
            int firstX = n * _spacing;
            Point2D p1;
            Point2D p2;
            var count = (int)(_viewPort.Width / _spacing);
            //renderer.SetColor(200,16,16, 255);
            for (int i = 0; i < count; i++)
            {
                p1 = new Point2D(firstX + (i * _spacing), _worldSize.Y);
                p2 = new Point2D(firstX + (i * _spacing), _worldSize.Height);
                if(Math.Abs(p1.X - _mouseLocation.X) < (_spacing / 2))
                    renderer.SetColor(0,0, 255,255);
                else
                    renderer.SetColor(200,16,16, 255);
                renderer.DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);
            }
            count = (int)(_viewPort.Height / _spacing);
            for (int i = 0; i < count; i++)
            {
                p1 = new Point2D(_worldSize.X, firstY + (i * _spacing));
                p2 = new Point2D(_worldSize.Width, firstY + (i * _spacing));
                if(Math.Abs(p1.Y - _mouseLocation.Y) < (_spacing / 2))
                    renderer.SetColor(0,0, 255,255);
                else
                    renderer.SetColor(200,16,16, 255);
                renderer.DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);
            }
        }
        
        public int ZValue {get;}
        public bool DoRender{get {return IsVisible;}}

    }
}