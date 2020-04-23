using System;

namespace escape_aliens.Engine
{
    public class Point2D 
    {
        public Point2D(double x, double y) 
        {
            X = x;
            Y = y;
        }

        public double X {get;set;}
        public double Y {get;set;}

        public double DistanceFrom(Point2D p) {
            double dx = p.X - X;
            double dy = p.Y - Y;
            return Math.Sqrt((dx * dx) + (dy * dy));
        }
    }
}