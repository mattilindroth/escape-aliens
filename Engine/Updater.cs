using System;
using System.Collections.Generic;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens.Engine
{
    public class Updater
    {
        private List<IUpdatable> _updatables;

        public Updater() 
        {
            _updatables = new List<IUpdatable>();
        }

        public void AddUpdatable(IUpdatable updatable) {
            _updatables.Add(updatable);
        }

        public void RemvoeUpdatable(IUpdatable updatable) {
            _updatables.Remove(updatable);
        }

        public void UpdateObjects(double timeStepMilliseconds) 
        {
            foreach(var updatable in _updatables) {
                updatable.Update(timeStepMilliseconds);
            }
        }
    }
}