using SDL2;
using System;
using System.Collections.Generic;

namespace escape_aliens.Engine.Input 
{
    public delegate void MouseMove(int x, int y);
    public delegate void MouseButtonStatusChange( bool isDown );

}