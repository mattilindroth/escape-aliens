namespace escape_aliens.Engine.MathExtra
{
    public class LineSegment2D {

        public LineSegment2D(Point2D p1, Point2D p2) {
            P1  = p1;
            P2 = p2;
        }

        public Point2D P1 {get;set;}
        public Point2D P2 {get;set;}

        public Point2D CalculateIntersection(LineSegment2D another) {

            //https://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect
            Vector2D q = new Vector2D(P1.X, P1.Y);
            Vector2D p = new Vector2D(another.P1.X, another.P1.Y );
            Vector2D s = new Vector2D(P2.X - P1.X, P2.Y - P1.Y);
            Vector2D r = new Vector2D(another.P1.X - another.P2.X, another.P1.Y - another.P2.Y);

            var v = q - p;
            var rxs = r.CrossProduct(s);
            var qpxr = v.CrossProduct(r);

            double t = v.CrossProduct(s) / (r.CrossProduct(s));
            double u = (p-q).CrossProduct(r) / (r.CrossProduct(s));

            if(rxs == 0 && qpxr != 0) {
                return null;
            } else if (rxs != 0 && t >= 0 && t <= 1 && u >= 0 && u <= 1){
                var tr = t * r;
                return new Point2D(P1.X + tr.X, P1.Y + tr.Y);
            } else if (rxs == 0 && qpxr == 0) {
                var t0 = (q - p) * r / (r * r);
                var t1 = t0 + (s * r)/(r * r);
                if((t0 >= 0 && t0 <= 1) || (t1 >= 0 && t1 <= 1)) {
                    return something //colinear lines....
                }
            }
            return null;

        }
    }
}