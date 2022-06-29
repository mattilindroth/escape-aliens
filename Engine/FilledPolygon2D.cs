using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine.MathExtra;
using SDL2;
using SDL2_gfx;

namespace escape_aliens.Engine
{
    public class FilledPolygon2D: Component, IRenderable  
    {
        private int _zValue;
        private Polygon2D _polygon;
        private Image _texture;
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

        public FilledPolygon2D(Polygon2D polygon, Image texture) {
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
        public Image Texture {get {return _texture; } set {_texture = value;}}
        
        void IRenderable.Render(Renderer renderer) {
            int i;
            Point2D p1;
            int[] x  = new int[_polygon.Count]; 
            int[] y  = new int[_polygon.Count]; 

            Matrix3 m = Matrix3.CreateTranslationMatrix(_transformation.Position);
            m.Rotate(_transformation.RotationRadians);
            m.Scale(_transformation.Size);
            for(i = 0; i < _polygon.Count; i++)
            { 
                p1 = m * _polygon.Point(i);
                x[i] = (int)p1.X;
                y[i] = (int)p1.Y;

            }
            renderer.TexturedPolygon(x, y, _texture.SDL_Surface, 0, 0);
        }

        void FillPolygon(Renderer renderer) {
            SDL.SDL_Rect boundingRect = _polygon.GetBoundingRectangle();
            
            for(int y = boundingRect.y; y < (boundingRect.y + boundingRect.h); y++) {
                
            }
        }
    }    
}