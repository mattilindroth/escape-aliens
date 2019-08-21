using System;

namespace escape_aliens.Engine.Interfaces 
{
    public interface ITimer {
        void Start();
        uint GetElapsedTicks();
        void Sleep(uint ticks);
    }
}