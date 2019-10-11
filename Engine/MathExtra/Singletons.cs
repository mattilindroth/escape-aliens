using System;

namespace escape_aliens.Engine.MathExtra
{
    public class Singletons 
    {
        private static Random _random;
        public static Random Random() 
        {
            if(_random == null)
            _random = new Random(DateTime.Now.Millisecond);
            return _random;
        }
    }
}