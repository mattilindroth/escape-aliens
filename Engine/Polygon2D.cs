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

        

    }
}