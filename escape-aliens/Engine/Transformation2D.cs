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

        public Point2D Transform(Point2D p, Point2D rotationCenter) {
            var p1 = new Point2D(0, 0);
            p1.X = (p.X - rotationCenter.X) * System.Math.Cos(RotationRadians) - (p.Y - rotationCenter.Y) * System.Math.Sin(RotationRadians);
            p1.Y = (p.X - rotationCenter.X) * System.Math.Sin(RotationRadians) + (p.Y - rotationCenter.Y) * System.Math.Cos(RotationRadians);
            p1.X += Position.X;
            p1.Y += Position.Y;
            p1.X *= Size;
            p1.Y *= Size;
            return p1;
        }

        public Point2D InvertTransform(Point2D p, Point2D rotationCenter) {
            var p1 = new Point2D(0, 0);
            p1.X /= Size;
            p1.Y /= Size;
            p1.X -= Position.X;
            p1.Y -= Position.Y;
            p1.X = (p.X - rotationCenter.X) * System.Math.Cos(-RotationRadians) - (p.Y - rotationCenter.Y) * System.Math.Sin(-RotationRadians);
            p1.Y = (p.X - rotationCenter.X) * System.Math.Sin(-RotationRadians) + (p.Y - rotationCenter.Y) * System.Math.Cos(-RotationRadians);
                        
            return p1;
        }

        public Matrix3 CreateTransformationMatrix()
        {
            var m = Matrix3.CreateRotationMatrix(RotationRadians);
            m.Translate(Position);
            m.Scale(Size);

            return m;
        }

        public Matrix3 CreateInvertTransformationMatrix()
        {
            var m = CreateTransformationMatrix();
            m.Invert();
            return m;
        }

    }

}