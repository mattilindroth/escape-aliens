using System;

namespace escape_aliens.Engine.Interfaces
{
    public interface IRenderer 
    {
        
        void DrawRectangle(double x, double y, double width, double height);

        void DrawLine(double x1, double y1, double x2, double y2);

        void DrawCircle(double x, double y, double r);
    }       
}

