using System;
namespace  escape_aliens.Engine
{
    public abstract class ComponentInputListener 
    {
        public abstract void HandleInput(KeyboardState KeyboardState);
    }
}