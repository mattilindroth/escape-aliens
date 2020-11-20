using escape_aliens.Engine.MathExtra;

namespace escape_aliens.Engine.Interfaces 
{
    public interface ICollidable 
    {
        Transformation2D Transformation {get;}

        Polygon2D ConvexPolygon {get;}

        void CollisionDetected();
    }
}