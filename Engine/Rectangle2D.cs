namespace escape_aliens.Engine
{
    public class Rectangle2D<T> where T : System.IComparable
    {
        public Rectangle2D(T x, T y, T w, T h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
        public T X {get;set;}
        public T Y {get;set;}
        public T Height {get;set;}
        public T Width {get;set;}

        
    }
}