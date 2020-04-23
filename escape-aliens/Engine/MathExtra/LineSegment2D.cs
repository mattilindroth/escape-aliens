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
            Vector2D r = new Vector2D(P2.X - P1.X, P2.Y - P1.Y);
            Vector2D s = new Vector2D(another.P1.X - another.P2.X, another.P1.Y - another.P2.Y);

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
                if((s * r) < 0) {
                    var temp = t1;
                    t1 = t0;
                    t0 = t1;
                }

                if((t0 >= 0 && t0 <= 1) || (t1 >= 0 && t1 <= 1)) {
                    var res = (s - q) * t0; 
                    return new Point2D(res.X, res.Y); //colinear lines....
                }
            }
            return null;

        }

        public bool AreIntersecting(Point2D A, Point2D B, Point2D C, Point2D D) {
            Point2D CmP = new Point2D(C.X - A.X, C.Y - A.Y);
            Point2D r = new Point2D(B.X - A.X, B.Y - A.Y);
            Point2D s = new Point2D(D.X - C.X, D.Y - C.Y);
    
            double CmPxr = CmP.X * r.Y - CmP.Y * r.X;
            double CmPxs = CmP.X * s.Y - CmP.Y * s.X;
            double rxs = r.X * s.Y - r.Y * s.X;
    
            if (CmPxr == 0f)
            {
                // Lines are collinear, and so intersect if they have any overlap
    
                return ((C.X - A.X < 0f) != (C.X - B.X < 0f))
                    || ((C.Y - A.Y < 0f) != (C.Y - B.Y < 0f));
            }
    
            if (rxs == 0f)
                return false; // Lines are parallel.
    
            double rxsr = 1f / rxs;
            double t = CmPxs * rxsr;
            double u = CmPxr * rxsr;
                
            return (t >= 0f) && (t <= 1f) && (u >= 0f) && (u <= 1f);
        }
    }
}