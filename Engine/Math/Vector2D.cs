using System;
using SDL2;
namespace escape_aliens.Engine.Math {
    public class Vector2D {
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
