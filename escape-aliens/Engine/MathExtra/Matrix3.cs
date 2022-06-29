namespace escape_aliens.Engine.MathExtra
{

    using static System.Math;
    public class Matrix3 {

        public Matrix3() 
        {
            ToUnitMatrix();
        }

        public void ToUnitMatrix() {
            a11 = 1;
            a12 = 0;
            a13 = 0;
            a21 = 0;
            a22 = 1;
            a23 = 0;
            a31 = 0;
            a32 = 0;
            a33 = 1;
        }

        public void Rotate(double radians)
        {
            double cosphi = Cos(radians);
            double sinphi = Sin(radians);

            a11 *= cosphi;
            a12 *= -sinphi;
            a21 *= sinphi;
            a22 *= cosphi;
        }

        public void Scale(double scaleFactor)
        {
            a11 *= scaleFactor;
            a22 *= scaleFactor;
        }

        public void Translate(Vector2D v) {
            a13 += v.X;
            a23 += v.Y;
        }

        public void Invert()
        {
            double na11, na12, na13, na21, na22, na23, na31, na32, na33;
            double det = Determinant();
            if (det == 0)
                throw new System.Exception("Cannot calculate inverse matrix. Determinant is zero.");
            na11 = a22 * a33 - a32 * a23;
            na12 = -(a21 * a33 - a31 * a23);
            na13 = a21 * a32 - a31 * a22;
            na21 = -(a12 * a33 - a32 * a13);
            na22 = a11 * a33 - a31 * a13;
            na23 = -(a11 * a32 - a31 * a12);
            na31 = a12 * a23 - a22 * a13;
            na32 = -(a11 * a23 - a21 * a13);
            na33 = a11 * a22 - a21 * a12;

            a11 = na11 / det; a12 = na12 / det; a13 = na13 / det;
            a21 = na21 / det; a22 = na22 / det; a23 = na23 / det;
            a31 = na31 / det; a32 = na32 / det; a33 = na33 / det;

            Transpose();
        }

        public void Transpose()
        {
            double temp;
            temp = a21;
            a21 = a12;
            a12 = temp;

            temp = a31;
            a31 = a13;
            a13 = temp;

            temp = a32;
            a32 = a23;
            a23 = temp;
        }

        public bool CanInvert()
        {
            return Determinant() != 0;
        }

        public double Determinant()
        {
            double det = a11 * (a22 * a33 - a32 * a23) - a12 * (a21 * a33 - a31 * a23) + a13 * (a21 * a32 - a31 * a22);
            return det;
        }

        //This is just a tiny bit retarded but makes working with points easier.
        public static Point2D operator * (Matrix3 m, Point2D p)
        {
            return new Point2D(m.a11 * p.X + m.a12 * p.Y + m.a13, m.a21 * p.X + m.a22 * p.Y + m.a23);
        }
        public static Vector2D operator * (Matrix3 m, Vector2D v) {
            return new Vector2D(m.a11 * v.X + m.a12 * v.Y + m.a13, m.a21 * v.X + m.a22 * v.Y + m.a23);
        }

        public static Matrix3 operator * (Matrix3 m1, Matrix3 m2) {
            var res = new Matrix3();

            res.a11 = m1.a11 * m2.a11 + m1.a12 * m2.a21 + m1.a13 * m2.a31;
            res.a12 = m1.a11 * m2.a12 + m1.a12 * m2.a22 + m1.a13 * m2.a32;
            res.a13 = m1.a11 * m2.a13 + m1.a12 * m2.a23 + m1.a13 * m2.a33;

            res.a21 = m1.a21 * m2.a11 + m1.a22 * m2.a21 + m1.a23 * m2.a31;
            res.a22 = m1.a21 * m2.a12 + m1.a22 * m2.a22 + m1.a23 * m2.a32;
            res.a23 = m1.a21 * m2.a13 + m1.a22 * m2.a23 + m1.a23 * m2.a33;

            res.a31 = m1.a31 * m2.a11 + m1.a32 * m2.a21 + m1.a33 * m2.a31;
            res.a32 = m1.a31 * m2.a12 + m1.a32 * m2.a22 + m1.a33 * m2.a32;
            res.a33 = m1.a31 * m2.a13 + m1.a32 * m2.a23 + m1.a33 * m2.a33;

            return res;
        }

        public override string ToString()
        {
            string s = string.Format("{0} {1} {2}\n{3} {4} {5}\n{6} {7} {8}", a11, a12, a13, a21, a22, a23, a31, a32, a33);
            return s;
        }

        public static Matrix3 CreateInverse(Matrix3 m)
        {
            Matrix3 inv = new Matrix3();
            inv.a11 = m.a11; inv.a12 = m.a12; inv.a13 = m.a13;
            inv.a21 = m.a21; inv.a22 = m.a22; inv.a23 = m.a23;
            inv.a31 = m.a31; inv.a32 = m.a32; inv.a33 = m.a33;

            inv.Invert();
            return inv;
        }

        public static Matrix3 CreateRotationMatrix(double radians) {
            Matrix3 rotated = new Matrix3();
            rotated.a11 = Cos(radians);
            rotated.a12 = -Sin(radians);
            rotated.a21 = -rotated.a12; 
            rotated.a22 = rotated.a11;
            return rotated;
        }

        public static Matrix3 CreateTranslationMatrix(Vector2D translate) {
            Matrix3 translated = new Matrix3();
            translated.a13 = translate.X;
            translated.a23 = translate.Y;
            return translated;
        }

        public static Matrix3 CreateScalingMatrix(double scaleFactor) {
            Matrix3 scaled = new Matrix3();
            scaled.a11 = scaleFactor;
            scaled.a22 = scaleFactor;
            return scaled;
        }

        public double a11 {get;set;}
        public double a12 {get;set;}
        public double a13 {get;set;}

        public double a21 {get;set;}
        public double a22 {get;set;}
        public double a23 {get;set;}

        public double a31 {get;set;}
        public double a32 {get;set;}
        public double a33 {get;set;}
    }

}