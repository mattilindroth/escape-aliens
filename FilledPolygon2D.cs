using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine;
namespace escape_aliens
{
    public class FilledPolygon2D: Component, IRenderable  
    {
        private int _zValue;
        private Polygon2D _polygon;
        private Texture _texture;
        private Color _color;

        public FilledPolygon2D() {
            _zValue = 2;
            _polygon = new Polygon2D();
            _texture = null;
            _color = Color.Blue;
        }

        int IRenderable.ZValue {get {return _zValue; }}

        bool IRenderable.DoRender {get {return true;}}

        void IRenderable.Render(Renderer renderer) {

        }
    }    
}