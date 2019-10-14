using System.Collections.Generic;

namespace escape_aliens.Engine
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

        public bool IsInside(Point2D p) {
            bool inside = false;
            for ( int i = 0, j = _points.Count - 1 ; i < _points.Count ; j = i++ )
            {
                if ( ( _points[ i ].Y > p.Y ) != ( _points[ j ].Y > p.Y ) &&
                    p.X < ( _points[ j ].X - _points[ i ].X ) * ( p.Y - _points[ i ].Y ) / ( _points[ j ].Y - _points[ i ].Y ) + _points[ i ].X )
                {
                    inside = !inside;
                }
            }
            return inside;
        }       

    }
}