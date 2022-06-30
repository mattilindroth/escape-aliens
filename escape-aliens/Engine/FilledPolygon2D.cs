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

        public FilledPolygon2D() {
            _zValue = 2;
            _polygon = new Polygon2D();
            _texture = null;
            _color = Color.Black;
        }

        public FilledPolygon2D(Polygon2D polygon) {
            _zValue = 2;
            _polygon = polygon;
            _texture = null;
            _color = Color.Black;
        }

        public FilledPolygon2D(Polygon2D polygon, Texture texture) {
            _zValue = 2;
            _polygon = polygon;
            _texture = texture;
            _color = Color.Black;            
        }

        int IRenderable.ZValue {get {return _zValue; }}

        bool IRenderable.DoRender {get {return true;}}

        public Polygon2D Polygon {get {return _polygon; } set{_polygon = value;}}
        public Texture Texture {get {return _texture; } set {_texture = value;}}
        
        public void MouseMove(int x, int y, int dx, int dy) {
            if(_polygon.IsInside(new Point2D(x, y))) {
                _color = Color.Red;
            } else {
                _color = Color.Blue;
            }
        }


        void IRenderable.Render(Renderer renderer) {
            int i;
            Point2D p1, p2;
            renderer.SetColor(_color);

            //Draw the outline of the polygon
            for(i = 0; i < _polygon.Count-1; i++)
            { 
                p1 = _polygon.Point(i);
                p2 = _polygon.Point(i+1);
                renderer.DrawLine((int)p1.X, (int)p1.Y ,(int)p2.X, (int)p2.Y);   
            }
            p1 = _polygon.Point(_polygon.Count -1 );
            p2 = _polygon.Point(0);
            renderer.DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);

            //Draw the inside of the polygon
            var boundingRect = _polygon.GetBoundingRectangle();

            int x1, y1; //The running X for each line
            // int x = (int)lowestPointAndIndex.point.X; 
            // int y = (int)lowestPointAndIndex.point.Y;
            // int currentIndex = lowestPointAndIndex.index; //Index of current poly point
            // Point2D currentPoint = lowestPointAndIndex.point; //Current point

            // int nextIndex = (currentIndex + 1) >= _polygon.Count ?  0: currentIndex + 1; //Index of next poly point
            // Point2D nextPoint = _polygon.Point(nextIndex); //Next poly point
            
            // //How much do we need to move x-wise if we move vertical 
            // float dx = (float)((currentPoint.Y - nextPoint.Y) == 0 ? 0: (x - nextPoint.X) / (currentPoint.Y - nextPoint.Y));
            //Loop
            for (y1 = boundingRect.y; y1 < (boundingRect.y + boundingRect.h); y1++) {
                for (x1 = boundingRect.x; x1 < (boundingRect.x + boundingRect.w); x1++)
                {
                    if(_polygon.IsInside(new Point2D(x1, y1)))
                        renderer.DrawPixel(x1, y1);
                     
                    // if(y >= nextPoint.Y) { //If we go beying the next point Y boundary, we must do some action
                    //     nextIndex = (nextIndex + 1) >= _polygon.Count ?  0: nextIndex + 1;// calculate the next index and current index again
                    //     currentIndex = (currentIndex + 1) >= _polygon.Count ? 0 : currentIndex + 1;
                    //     nextPoint = _polygon.Point(nextIndex);
                    //     currentPoint = _polygon.Point(currentIndex);
                    //     x = (int)nextPoint.X; //Set the starting point
                    //     //Calculate new dx
                    //     dx = (float)((currentPoint.Y - nextPoint.Y) == 0 ? 0: (x - nextPoint.X) / (currentPoint.Y - nextPoint.Y));
                }
            }
            
        }

        void FillPolygon(Renderer renderer) {
            SDL.SDL_Rect boundingRect = _polygon.GetBoundingRectangle();
            
            for(int y = boundingRect.y; y < (boundingRect.y + boundingRect.h); y++) {
                
            }
        }
    }    
}