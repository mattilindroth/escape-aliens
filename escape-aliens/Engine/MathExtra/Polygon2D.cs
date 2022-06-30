using System.Collections.Generic;

namespace escape_aliens.Engine.MathExtra
{
    public class Polygon2D 
    {
        private List<Point2D> _points;

        public Polygon2D() {
            _points = new List<Point2D>();
        }

        public void AddPoint(Point2D point) {
            _points.Add(point);
        }

        public void AddPoint(double X, double Y) 
        {
            _points.Add(new Point2D(X, Y));
        }

        public void AddPointToIndex(Point2D point, int index) {
            _points.Insert(index, point);
        }

        public int Count { get {return _points.Count;}}

        public Point2D Point(int index) 
        {
            return _points[index];
        }

        public void RemovePoint(Point2D p) {
            _points.Remove(p);
        }

        public void RemovePointAt(int index) 
        {
            _points.RemoveAt(index);
        }

        public SDL2.SDL.SDL_Rect GetBoundingRectangle()
        {
            double minX , minY, maxX, maxY;
            minX = _points[0].X;
            maxX = minX;
            minY = _points[0].Y;
            maxY = minY;
            for(int i = 1; i < _points.Count; i++) {
                var p = _points[i];
                minX = System.Math.Min(minX, p.X);
                minY = System.Math.Min(minY, p.Y);
                maxX = System.Math.Max(maxX, p.X);
                maxY = System.Math.Max(maxY, p.Y);
            }
            
            return new SDL2.SDL.SDL_Rect() {x = (int)minX, y = (int)minY, h = (int)(maxY - minY), w = (int)(maxX - minX)};
        }

        public bool IsInside(Point2D p) {
            bool inside = false;
            for ( int i = 0, j = _points.Count - 1 ; i < _points.Count ; i++ )
            {
                if ( ( _points[ i ].Y > p.Y ) != ( _points[ j ].Y > p.Y ) &&
                    (p.X < ( _points[ j ].X - _points[ i ].X ) * ( p.Y - _points[ i ].Y ) / ( _points[ j ].Y - _points[ i ].Y ) + _points[ i ].X ))
                {
                    inside = !inside;
                }
                j = i;
            }
            return inside;
        }       
    }
}