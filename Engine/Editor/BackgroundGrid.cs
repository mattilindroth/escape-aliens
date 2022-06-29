using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine.MathExtra;
using escape_aliens.Engine.Input;
using escape_aliens.Engine;
using System;

namespace escape_aliens.Editor
{
    public class BackgroundGrid : GameObject, IRenderable {
        
        Transformation2D _transformation;
        LineSegment2D[] _verticalGridLines;
        LineSegment2D[] _horizontalGridLines;
        int _minSpacing = 10;
        int _maxSpacing = 100;
        int _spacingStep = 10;
        int _spacing;
        // SDL2Timer _blinkTimer;
        // bool _timerUp;

        Rectangle2D<int> _worldSize;
        Rectangle2D<int> _viewPort;
        Point2D _mouseLocation;

        public BackgroundGrid(Rectangle2D<int> worldSize, Rectangle2D<int> viewPort)
        {
            _spacing = (_maxSpacing - _minSpacing)/2;
            IsVisible = true;
            _transformation = new Transformation2D();
            _viewPort = viewPort;
            _worldSize = worldSize;
            // _blinkTimer = new SDL2Timer();
            _mouseLocation = new Point2D(0,0);
            CalculateGrid();
        }

        public void IncreaSpacing()
        {
            _spacing = Math.Min(Spacing + _spacingStep, _maxSpacing);
            CalculateGrid();
        }

        public void DecreaseSpecing()
        {
            _spacing = Math.Max(Spacing - _spacingStep, _minSpacing);
            CalculateGrid();
        }

        public int Spacing {get {return _spacing;}}

        public void MouseMove(int x, int y, int dx, int dy, int dw)
        {
            _mouseLocation.X = x;
            _mouseLocation.Y = y;
        }

        public bool IsVisible {get;set;}

        public void Render(Renderer renderer) 
        {
            Point2D p1;
            Point2D p2;
            int x1, x2, y1, y2;
            //Vector2D transfr = this.Transformation.Position;
            //double scale = this.Transformation.Size;
            Matrix3 transfr = Matrix3.CreateTranslationMatrix(Transformation.Position);
            transfr.Scale(Transformation.Size);

            for (int i = 0; i < _verticalGridLines.Length; i++)
            {
                p1 = transfr * _verticalGridLines[i].P1;
                p2 = transfr * _verticalGridLines[i].P2;
                if(Math.Abs(p1.X   - _mouseLocation.X) < (_spacing * Transformation.Size / 2))
                    renderer.SetColor(0,0, 255,255);
                else
                    renderer.SetColor(200,16,16, 255);
                x1 = (int)(p1.X );
                y1 = (int)(p1.Y );
                x2 = (int)(p2.X );
                y2 = (int)(p2.Y );
                renderer.DrawLine(x1,y1,x2,y2);
            }
            for (int i = 0; i < _horizontalGridLines.Length; i++)
            {
                p1 = transfr * _horizontalGridLines[i].P1;
                p2 = transfr * _horizontalGridLines[i].P2;
                if(Math.Abs(p1.Y - _mouseLocation.Y) < (_spacing * Transformation.Size / 2))
                    renderer.SetColor(0,0, 255,255);
                else
                    renderer.SetColor(200,16,16, 255);
                x1 = (int)(p1.X );
                y1 = (int)(p1.Y );
                x2 = (int)(p2.X );
                y2 = (int)(p2.Y );
                renderer.DrawLine(x1,y1,x2,y2);
            }
        }
        
        public int ZValue {get;}
        public bool DoRender{get {return IsVisible;}}

        private void CalculateGrid()
        {
            int ny = (int)(_worldSize.Width / _spacing) + 1;
            int nx = (int)(_worldSize.Height / _spacing) + 1;
            _verticalGridLines = new LineSegment2D[ny];
            _horizontalGridLines = new LineSegment2D[nx];
            Point2D p1;
            Point2D p2;

            for (int i = 0; i < _verticalGridLines.Length; i++)
            {
                p1 = new Point2D((i * _spacing), _worldSize.Y);
                p2 = new Point2D((i * _spacing), (nx-1) * _spacing);
                _verticalGridLines[i] = new LineSegment2D(p1 ,p2);
            }
            for (int i = 0; i < _horizontalGridLines.Length; i++)
            {
                p1 = new Point2D(_worldSize.X, (i * _spacing));
                p2 = new Point2D((ny - 1) * _spacing, (i * _spacing));
                _horizontalGridLines[i] = new LineSegment2D(p1 ,p2);
            }
        }

    }
}