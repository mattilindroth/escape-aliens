using escape_aliens.Engine.MathExtra;

namespace escape_aliens.Engine.Interfaces
{
    public interface IPhysicalObject
    {
        Vector2D Speed {get; set;}
        Vector2D Acceleration {get;}
        bool DoUpdate {get;}
    }
}