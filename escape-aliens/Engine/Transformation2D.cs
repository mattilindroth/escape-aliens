using escape_aliens.Engine.MathExtra;

namespace escape_aliens.Engine 
{
    public class Transformation2D : Component
    {
        public Transformation2D() {
            Position = new Vector2D();
        }
        public Vector2D Position {get;set;}
        public double RotationRadians {get;set;}
        public double Size {get;set;}
    }

}