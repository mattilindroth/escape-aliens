using escape_aliens.Engine.Interfaces;
using System.Collections.Generic;
using escape_aliens.Engine.MathExtra;

namespace escape_aliens.Engine
{
    public class Physics
    {
        private Vector2D _gravity;
        private List<IPhysicalObject> _physicalObjects;
        
        public Physics() {
            _gravity = new Vector2D(0, 12);
            _physicalObjects = new List<IPhysicalObject>();
        }

        public void AddPhysicalObject(IPhysicalObject physicalObject)
        {
            _physicalObjects.Add(physicalObject);
        }

        public void RemovePhysicalObject(IPhysicalObject physicalObject)
        {
            _physicalObjects.Remove(physicalObject);
        }

        public void Update(double timeStepMilliseconds) {
            foreach(var physicalObject in _physicalObjects) {
                if(physicalObject.DoUpdate) {
                    if(physicalObject.Acceleration.Length != 0) {
                        physicalObject.Speed += physicalObject.Acceleration;
                    } else {
                        physicalObject.Speed += _gravity; 
                    }
                }
            }
        }
    }
}