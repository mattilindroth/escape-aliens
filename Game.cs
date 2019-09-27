using System;
using SDL2;
using escape_aliens.Engine.Interfaces;
using escape_aliens.Engine;

namespace escape_aliens
{
    public class Game 
    {
		
		private ITimer _timer;
		private Scene _scene;

    	public Game(Scene scene, ITimer timer) 
    	{
			_scene = scene;
			_timer = timer;
    	}

    	public void Run(uint desiredFps) 
    	{
			bool quit = false;	
			uint ticksPerFrame = FPSToTicksPerFrame(desiredFps);
			uint ticks;
			
			Player player = new Player();
			PlayerRenderer playerRenderer = new PlayerRenderer();
			playerRenderer.AddedToGameObject(player);

			while(!quit) {
				_timer.Start();
				SDL.SDL_Event e;
				while(SDL.SDL_PollEvent (out e)!= 0){
					switch(e.type) {
						case SDL.SDL_EventType.SDL_QUIT:
							quit = true;
							break;
						case SDL.SDL_EventType.SDL_KEYDOWN:
							if(e.key.keysym.sym == SDL.SDL_Keycode.SDLK_q)
								quit = true;
							break;
					}
				}

				_scene.Render();

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