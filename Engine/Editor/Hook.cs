using escape_aliens.Engine;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Editor 
{
    public class Hook : GameObject, IRenderable
    {

        private SDL2.SDL.SDL_Color _color;
        private int _size;
        private int _ZValue;
        private bool _visible;
        private Point2D[] _points;
        private Point2D[] _rectanglePoints;

        public Hook()
        {
            _size = 20;
            CalculatePoints();
        }

        public void SetSize(int newSizePixels) {
            _size = newSizePixels;
            CalculatePoints();
        }

        int IRenderable.ZValue {get {return _ZValue;}}

        bool IRenderable.DoRender {get {return _visible;}}

        Transformation2D IRenderable.Transformation {get {return this.Transformation;}}

        void IRenderable.Render(Renderer renderer)
        {
            renderer.SetColor(_color.r, _color.g, _color.b, _color.a);

            renderer.DrawLines(_points, Transformation);
        }

        public void MouseMove(int x, int y, int dx, int dy, int dw) 
        {
            //Point2D worldCoords = this.
        }

        private void CalculatePoints()
        {
            _points = new Point2D[5];
            _points[0] = new Point2D(0,0);
            _points[1] = new Point2D(_size,0);
            _points[2] = new Point2D(_size,_size);
            _points[3] = new Point2D(0,_size);
            _points[4] = new Point2D(0,0);
        }

    }
}