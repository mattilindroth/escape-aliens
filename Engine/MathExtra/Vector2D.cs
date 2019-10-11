using System;
using SDL2;
namespace escape_aliens.Engine.MathExtra {
    public class Vector2D {

		public Vector2D() {
			X = 0;
			Y = 0;
		}

		public Vector2D(double x, double y) {
			X = x;
			Y = y;
		}

		// public Vector2D(double length, float angleRadians) {
		// 	X = length * Math.Cos(angleRadians);
		// 	Y = length * Math.Sin(angleRadians);
		// }
		public double Length 
		{
			get 
			{
				return Math.Sqrt((X*X)+(Y*Y));
			}
		}
        public double X {get;set;}
        public double Y {get;set;} 

        public static double operator * (Vector2D v1, Vector2D v2) {
        	double result;

        	result = v1.X * v2.X;
        	result = result + (v1.Y * v2.Y);

        	return result;
        }

        public static Vector2D operator *(double val, Vector2D v) {
        	return new Vector2D
        	{
        		X = val * v.X,
        		Y = val * v.Y,
        	};
        }

        public static Vector2D operator *(Vector2D v, double val) {
        	return new Vector2D
        	{
        		X = val * v.X,
        		Y = val * v.Y,
        	};
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2 ) 
        {

        	var result = new Vector2D
        	{
        		X = v1.X - v2.X, 
        		Y = v1.Y - v2.Y
        	};

        	return result;
        }
        public static Vector2D operator -(Vector2D v) {
        	return new Vector2D {
        		X = -v.X,
        		Y = v.Y,
        	};
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2 ) 
        {
        	Vector2D result;

        	result = new Vector2D
        	{
        		X = v1.X + v2.X, 
        		Y = v1.Y + v2.Y
        	};

        	return result;
        }
    }
}
