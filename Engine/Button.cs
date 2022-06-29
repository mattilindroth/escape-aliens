using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine.MathExtra;

namespace escape_aliens.Engine
{
    public enum eButtonState
    {
        mouseOver,
        mouseOutside,
        mouseButtonDown,
    };

    public class MouseEventArgs
    {

    }

    public abstract class  Button : GameObject, IRenderable
    {
        private bool _isMouseOn;
        private int _ZValue;

        private Rectangle2D<double> _rect;
        private eButtonState _buttonState;

        public delegate void MouseEvent(MouseEventArgs e);

        public event MouseEvent MouseEnter;
        public event MouseEvent MouseLeave;
        public event MouseEvent MouseDown;
        public event MouseEvent MouseUp;

        public Button(double x, double y, double width, double height)
        {
            _rect = new Rectangle2D<double>(x, y, width, height);
        }

        int IRenderable.ZValue {get;}

        bool IRenderable.DoRender {get;}

        Transformation2D IRenderable.Transformation {get {return this.Transformation;}}

        public virtual void RenderButton(Renderer renderer)  
        {

        }

        void IRenderable.Render(Renderer renderer)
        {
            this.RenderButton(renderer);
        }

        public void MouseMove(int x, int y, int dx, int dy, int dw) 
        {
            Matrix3 m = this.Transformation.CreateInvertTransformationMatrix();

            var upperLeft = m * (new Point2D(_rect.X, _rect.Y));
            var lowerRight = m * (new Point2D(_rect.X + _rect.Width, _rect.Y + _rect.Height));

            var isMouseIn = false;

            if(x > upperLeft.X && x < lowerRight.X)
            {
                if (y < lowerRight.Y && y >= upperLeft.Y)
                { 
                    if (!this._isMouseOn)
                    {
                        isMouseIn = true;
                        this._isMouseOn = true;
                        this._buttonState = eButtonState.mouseOver;
                        this.MouseEnter?.Invoke(new MouseEventArgs());
                    }
                }
            }
            if(this._isMouseOn && !isMouseIn)
            {
                this.MouseLeave?.Invoke(new MouseEventArgs());
            }

        }
    }
}