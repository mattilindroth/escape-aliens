namespace escape_aliens.Engine
{
    public class Color 
    {

        public Color() {
            A = 255;
            B = 0;
            G = 0;
            R = 0;
        }

        public Color(byte a, byte r, byte g, byte b)
        {
            A = a;
            B = b;
            G = g;
            R = r;
        }

        public static Color Blue {get {
            return new Color(255, 0, 0, 255);
        }}

        public static Color Red {get {
            return new Color(255, 255, 0,0);
        }}

        public static Color Green {get {
            return new Color(255,0,255,0);
        }}

        public static Color Black {get {
            return new Color(0,0,0,0);
        }}

        public byte A {get;set;}
        public byte R {get;set;}
        public byte G {get;set;}
        public byte B {get;set;}
    }
}