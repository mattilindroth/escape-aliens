using System;

namespace escape_aliens.Engine.Interfaces
{
    public interface IRenderable 
    {
        void Render(Renderer renderer);
        int ZValue {get;}
        bool DoRender{get;}
    }       
}

