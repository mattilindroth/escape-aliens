using System;
using Xunit;
using escape_aliens;
using escape_aliens.Engine.MathExtra;
using escape_aliens.Engine;

namespace escape_aliens
{
    public class LineSegmentIntersectionTests
    {
        [Theory]
        [InlineData(0.0, 0, 10, 0, 0, 0.0, 0, 10)]
        public void TestOverlappingLines(double x1s,double  y1s,double  x1e,double y1e, double x2s, double y2s, double x2e, double y2e)
        {
            Point2D l1s = new Point2D(x1s, y1s);
            Point2D l1e = new Point2D(x1e, y1e);
            Point2D l2s  =new Point2D(x2s, y2s);
            Point2D l2e = new Point2D(x2e, y2e);
            LineSegment2D l1 = new LineSegment2D(l1s, l1e);
            LineSegment2D l2 = new LineSegment2D(l2s, l2e);

            var intersection = l1.AreIntersecting(l1.P1, l1.P2, l2.P1, l2.P2);
            Assert.True(intersection);

            var res = l1.CalculateIntersection(l2);
            Assert.NotNull(res);
        }

        [Theory]
        [InlineData(0.1, 0.1, 10, 0, 0, 0.0, 0, 10)]
        public void TestNonOverlappingLines(double x1s,double  y1s,double  x1e,double y1e, double x2s, double y2s, double x2e, double y2e)
        {
            Point2D l1s = new Point2D(x1s, y1s);
            Point2D l1e = new Point2D(x1e, y1e);
            Point2D l2s  =new Point2D(x2s, y2s);
            Point2D l2e = new Point2D(x2e, y2e);
            LineSegment2D l1 = new LineSegment2D(l1s, l1e);
            LineSegment2D l2 = new LineSegment2D(l2s, l2e);

            var intersection = l1.AreIntersecting(l1.P1, l1.P2, l2.P1, l2.P2);
            Assert.False(intersection);

            var res = l1.CalculateIntersection(l2);
            Assert.Null(res);
        }

        // [Theory]
        // [InlineData(0.1, 0.1, 10, 0, 0, 0.0, 0, 10)]
        // public void TestNonOverlappingLines(double x1s,double  y1s,double  x1e,double y1e, double x2s, double y2s, double x2e, double y2e)
        // {
        //     Point2D l1s = new Point2D(x1s, y1s);
        //     Point2D l1e = new Point2D(x1e, y1e);
        //     Point2D l2s  =new Point2D(x2s, y2s);
        //     Point2D l2e = new Point2D(x2e, y2e);
        //     LineSegment2D l1 = new LineSegment2D(l1s, l1e);
        //     LineSegment2D l2 = new LineSegment2D(l2s, l2e);

        //     var intersection = l1.AreIntersecting(l1.P1, l1.P2, l2.P1, l2.P2);
        //     Assert.False(intersection);

        //     var res = l1.CalculateIntersection(l2);
        //     Assert.Null(res);
        // } 

    }
}
