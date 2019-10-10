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
		private KeyboardBindings _keyboardBindings;
		private Updater _updater;

    	public Game(Scene scene, ITimer timer) 
    	{
			_scene = scene;
			_timer = timer;
			_keyboardBindings = new KeyboardBindings();
			_updater = new Updater();
    	}

		public void LoadResource() {
			Player p1 = new Player();
			SpriteComponent sprite = new SpriteComponent(p1.Transformation, 3);
			ThrustComponent thrust = new ThrustComponent(30, 2);
			sprite.AddedToGameObject(p1);
			thrust.AddedToGameObject(p1);
			
			Texture texture = _scene.Renderer.LoadTexture(@"C:\Source\escape-aliens\Resources\SpaceShipRed.png");
			SDL.SDL_Rect renderRect;
			SDL.SDL_Rect sourceRect;
			renderRect.x = 240;
			renderRect.y = 240;
			renderRect.w = 80;
			renderRect.h = 80;
			
			sourceRect.w = 167;
			sourceRect.h = 170;
			sourceRect.x = 0;
			sourceRect.y = 0;

			texture.RenderRectangle = renderRect;
			texture.SourceRectangle = sourceRect;
			sprite.AddAnimationFrame(texture);
			_scene.AddRenderable(sprite);
			_keyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_A, p1.RotateLeft);
			_keyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_D, p1.RotateRight);
			_updater.AddUpdatable(p1);
		}
    	public void Run(uint desiredFps) 
    	{
			bool quit = false;	
			uint ticksPerFrame = FPSToTicksPerFrame(desiredFps);
			uint ticks;
			
			while(!quit) {
				_timer.Start();
				SDL.SDL_Event e;
				while(SDL.SDL_PollEvent (out e)!= 0){
					switch(e.type) {
						case SDL.SDL_EventType.SDL_QUIT:
							quit = true;
							break;
						// case SDL.SDL_EventType.SDL_KEYDOWN:
						// 	_keyboardBindings.ExperimentalKeyHandling(e);

						// 	break;
						// case SDL.SDL_EventType.SDL_KEYUP:
						// 	_keyboardBindings.ExperimentalKeyHandling(e);
						// 	break;
					}
				}
				_keyboardBindings.UpdateStateAndDispatchEvents();
				_scene.Render();
				
				ticks = _timer.GetElapsedTicks();
				if(ticks < ticksPerFrame) {
					_timer.Sleep((ticksPerFrame - ticks));
				}
				_updater.UpdateObjects(_timer.GetElapsedTicks() * 10000);
			}

    	}

		private uint FPSToTicksPerFrame(uint fps) {

			double desiredFpms = ((int)fps) / 1000d;
			return (uint) (10000 * 1 / desiredFpms);
		}

    }
}