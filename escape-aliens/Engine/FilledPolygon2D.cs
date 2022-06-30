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

        void IRenderable.Render(Renderer renderer)
        {
            const int MAX_POLY_CORNERS = 100;
            int  nodes, pixelX, pixelY, i, j, swap ;
            int[] nodeX = new int[MAX_POLY_CORNERS];

            var boundingRect = _polygon.GetBoundingRectangle();
            int IMAGE_TOP = boundingRect.y;
            int IMAGE_BOTTOM = boundingRect.y + boundingRect.h;
            int IMAGE_LEFT = boundingRect.x;
            int IMAGE_RIGHT = boundingRect.x + boundingRect.w;

            renderer.SetColor(_color);

            //Draw the bounding lines
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


            // //  Loop through the rows of the image.
            // for (pixelY=IMAGE_TOP; pixelY<IMAGE_BOT; pixelY++) {
            for (pixelY = IMAGE_TOP; pixelY < IMAGE_BOTTOM; pixelY++)
            {
                //  Build a list of nodes.
                nodes = 0; j =_polygon.Count - 1;
                for (i = 0; i < _polygon.Count; i++) {
                    if (_polygon.Point(i).Y < (double) pixelY && _polygon.Point(j).Y >= (double) pixelY
                        ||  _polygon.Point(j).Y < (double) pixelY && _polygon.Point(i).Y >= (double) pixelY) {
                        nodeX[nodes++]=(int) (_polygon.Point(i).X + (pixelY - _polygon.Point(i).Y) / (_polygon.Point(j).Y - _polygon.Point(i).Y) * (_polygon.Point(j).X - _polygon.Point(i).X)); 
                    }
                j = i; 
                }
                //  Sort the nodes, via a simple “Bubble” sort.
                i = 0;
                while (i < nodes - 1) {
                if (nodeX[i]>nodeX[i+1]) {
                    swap=nodeX[i]; nodeX[i]=nodeX[i+1]; nodeX[i+1]=swap; 
                    if (i != 0) i--; 
                }
                else {
                  i++; 
                }
            }

              //  Fill the pixels between node pairs.
                for ( i = 0; i < nodes; i += 2) {
                    if(nodeX[i  ] >= IMAGE_RIGHT) break;
                    if(nodeX[i+1] > IMAGE_LEFT ) {
                        if(nodeX[i  ] < IMAGE_LEFT ) nodeX[i] = IMAGE_LEFT ;
                        if(nodeX[i+1] > IMAGE_RIGHT) nodeX[i+1] = IMAGE_RIGHT;
                        for (pixelX = nodeX[i]; pixelX < nodeX[i + 1]; pixelX++) 
                            renderer.DrawPixel(pixelX,pixelY); 
                    }
                }
            }
            
        }

        // void IRenderable.Render(Renderer renderer) {
        //     int i;
        //     Point2D p1, p2;
        //     renderer.SetColor(_color);

        //     //Draw the outline of the polygon
        //     for(i = 0; i < _polygon.Count-1; i++)
        //     { 
        //         p1 = _polygon.Point(i);
        //         p2 = _polygon.Point(i+1);
        //         renderer.DrawLine((int)p1.X, (int)p1.Y ,(int)p2.X, (int)p2.Y);   
        //     }
        //     p1 = _polygon.Point(_polygon.Count -1 );
        //     p2 = _polygon.Point(0);
        //     renderer.DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);

        //     //Draw the inside of the polygon
        //     var boundingRect = _polygon.GetBoundingRectangle();

        //     int x1, y1; 
        //     for (y1 = boundingRect.y; y1 < (boundingRect.y + boundingRect.h); y1++) {
        //         for (x1 = boundingRect.x; x1 < (boundingRect.x + boundingRect.w); x1++)
        //         {
        //             if(_polygon.IsInside(new Point2D(x1, y1)))
        //                 renderer.DrawPixel(x1, y1);
        //         }
        //     }
            
        // }

        void FillPolygon(Renderer renderer) {
            SDL.SDL_Rect boundingRect = _polygon.GetBoundingRectangle();
            
            for(int y = boundingRect.y; y < (boundingRect.y + boundingRect.h); y++) {
                
            }
        }
    }    
}