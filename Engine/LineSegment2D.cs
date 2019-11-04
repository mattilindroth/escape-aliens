namespace escape_aliens.Engine
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
        }
    }
}