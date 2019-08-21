using System;
using escape_aliens.Engine.Interfaces;

namespace escape_aliens
{
    public class Game 
    {
		private IRenderer _renderer;
		private ITimer _timer;

    	public Game(IRenderer renderer, ITimer timer) 
    	{
			_renderer = renderer;
			_timer = timer;
    	}

    	public void Run(uint desiredFps) 
    	{
			bool quit = false;	
			uint ticksPerFrame = FPSToTicksPerFrame(desiredFps);
			uint ticks;
			
			while(!quit) {
				_timer.Start();

				

				ticks = _timer.GetElapsedTicks();
				if(ticks < ticksPerFrame) {
					_timer.Sleep((ticksPerFrame - ticks));
				}
			}

    	}

		private uint FPSToTicksPerFrame(uint fps) {

			double desiredFpms = ((int)fps) / 1000d;
			return (uint) (10000 * 1 / desiredFpms);
		}

    }
}