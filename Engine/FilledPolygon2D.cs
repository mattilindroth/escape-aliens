using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine.MathExtra;
using SDL2;

namespace escape_aliens.Engine
{
    public class FilledPolygon2D: Component, IRenderable  
    {
        private int _zValue;
        private Polygon2D _polygon;
        private Texture _texture;
        private Color _color;
        private Transformation2D _transformation;

        public FilledPolygon2D() {
            _zValue = 2;
            _polygon = new Polygon2D();
            _texture = null;
            _color = Color.Blue;
            _transformation = new Transformation2D();
        }

        public FilledPolygon2D(Polygon2D polygon) {
            _zValue = 2;
            _polygon = polygon;
            _texture = null;
            _color = Color.Blue;
            _transformation = new Transformation2D();
        }

        public FilledPolygon2D(Polygon2D polygon, Texture texture) {
            _zValue = 2;
            _polygon = polygon;
            _texture = texture;
            _color = Color.Blue;            
            _transformation = new Transformation2D();
        }

        int IRenderable.ZValue {get {return _zValue; }}

        bool IRenderable.DoRender {get {return true;}}

        Transformation2D IRenderable.Transformation {get {return _transformation;}}

        public Polygon2D Polygon {get {return _polygon; } set{_polygon = value;}}
        public Texture Texture {get {return _texture; } set {_texture = value;}}
        
        void IRenderable.Render(Renderer renderer) {
            int i;
            Point2D p1, p2;
            renderer.SetColor(_color);
            for(i = 0; i < _polygon.Count-1; i++)
            { 
                p1 = _polygon.Point(i);
                p2 = _polygon.Point(i+1);
                renderer.DrawLine((int)p1.X, (int)p1.Y ,(int)p2.X, (int)p2.Y);   
            }
            p1 = _polygon.Point(_polygon.Count -1 );
            p2 = _polygon.Point(0);
            renderer.DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);
        }

        void FillPolygon(Renderer renderer) {
            SDL.SDL_Rect boundingRect = _polygon.GetBoundingRectangle();
            
            for(int y = boundingRect.y; y < (boundingRect.y + boundingRect.h); y++) {
                
            }
        }
    }    
}