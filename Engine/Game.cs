using System;
using SDL2;
using escape_aliens.Engine.Input;
using escape_aliens.Engine.Interfaces;
using escape_aliens.Editor;

namespace escape_aliens.Engine
{
    public class Game 
    {
		
		private ITimer _timer;
		private Scene _scene;
		private Input.Input _input;
		private Updater _updater;
		private Physics _physics;

		private eEditorMouseMode _editorMouseMode;

		public Game(Scene scene, ITimer timer) 
    	{
			_scene = scene;
			_timer = timer;
			_input = new Input.Input();
			_updater = new Updater();
			_physics = new Physics();
			_input.MouseBindings.RegisterMouseMovementListener(this.MouseMove);
			_input.MouseBindings.RegisterMouseButtonListener(this.MouseButtonStatusChange);
		}

		public Texture LoadTexture(string fileName) {
			return _scene.Renderer.LoadTexture(fileName);
		}
		public Image LoadImage(string filename)
		{
			return _scene.Renderer.LoadImage(filename);
		}
		public void MouseButtonStatusChange(eMouseButton button, bool isDown)
		{
			if (isDown)
			{
				if (button == eMouseButton.Left)
				{
					_editorMouseMode = eEditorMouseMode.MoveViewport;
				}
			}
			else
			{
				_editorMouseMode = eEditorMouseMode.None;
			}
		}
		public void MouseMove(int x, int y, int dx, int dy, int dw)
		{
			if (_editorMouseMode == eEditorMouseMode.MoveViewport)
			{
				_scene.ViewPort.X -= dx;
				_scene.ViewPort.Y -= dy;
			}
		}

		public Scene Scene {get {return _scene;}}

		public Physics Physics {get {return _physics;}}

		public void AddObject(GameObject gameObject) 
		{
			for(int i = 0; i < gameObject.CountOfComponents; i++) {
				var component  = gameObject.GetComponent(i);
				if(component is IRenderable && (!_scene.Contains((IRenderable)component))) {
					_scene.AddRenderable((IRenderable)component);
				}
				if(component is IUpdatable && (!_updater.Contains((IUpdatable)component))) {
					_updater.AddUpdatable((IUpdatable)component);
				}
				if(component is IPhysicalObject && (!_physics.Contains((IPhysicalObject)component))) {
					_physics.AddPhysicalObject((IPhysicalObject)component);
				}
			}

			if(gameObject is IRenderable && (!_scene.Contains((IRenderable)gameObject))) {
				_scene.AddRenderable((IRenderable)gameObject);
			}
			if(gameObject is IUpdatable && (!_updater.Contains((IUpdatable)gameObject))) {
				_updater.AddUpdatable((IUpdatable)gameObject);
			}
			if(gameObject is IPhysicalObject && (!_physics.Contains((IPhysicalObject)gameObject))) {
				_physics.AddPhysicalObject((IPhysicalObject)gameObject);
			}
		}

		public Input.Input Input {get {return _input;}}

    	public void Run(uint desiredFps) 
    	{
			bool quit = false;	
			uint ticksPerFrame = FPSToTicksPerFrame(desiredFps);
			uint ticks;
			
			while(!quit) {
				_timer.Start();
				SDL.SDL_Event e;
				while(SDL.SDL_PollEvent (out e)!= 0) {
					switch(e.type) {
						case SDL.SDL_EventType.SDL_QUIT:
							quit = true;
							break;
						case SDL.SDL_EventType.SDL_KEYDOWN:
							_input.KeyboardBindings.UpdateStateAndDispatchEvents();
							break;
						case SDL.SDL_EventType.SDL_KEYUP:
							_input.KeyboardBindings.UpdateStateAndDispatchEvents();
							break;
						case SDL.SDL_EventType.SDL_MOUSEWHEEL:
							this.HandleMouseWheelEvent(e);
							break;
						case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
							this.HandleMouseButtonEvent(e);
							break;
						case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
							this.HandleMouseButtonEvent(e);
							break;
						case SDL.SDL_EventType.SDL_MOUSEMOTION:
							this.HandleMouseMoveEvent(e);
							break;
					}
				}
				_scene.Render();
				
				ticks = _timer.GetElapsedTicks();
				if(ticks < ticksPerFrame) {
					_timer.Sleep((ticksPerFrame - ticks));
				}
				double elapsedMillisecods = _timer.GetElapsedTicks() / 10000.0;
				_physics.Update(elapsedMillisecods);
				 elapsedMillisecods = _timer.GetElapsedTicks() / 10000.0;
				_updater.UpdateObjects(elapsedMillisecods);
			}
    	}
		private void HandleMouseWheelEvent(SDL.SDL_Event e) {
			_input.MouseBindings.UpdateStateAndDispatchEvents(e.wheel.y);
		}
		private void HandleMouseMoveEvent(SDL.SDL_Event e) {	
			_input.MouseBindings.UpdateStateAndDispatchEvents(0);
		}
		private void HandleMouseButtonEvent(SDL.SDL_Event e) {
			bool isDown = false;
			eMouseButton button = eMouseButton.Any;
			if(e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
				isDown = true;

			if(e.button.button == SDL.SDL_BUTTON_LEFT)
				button = eMouseButton.Left;
			else if(e.button.button == SDL.SDL_BUTTON_MIDDLE)
				button = eMouseButton.Middle;
			else if(e.button.button == SDL.SDL_BUTTON_RIGHT)
				button = eMouseButton.Right;

			_input.MouseBindings.UpdateAndDispatchMouseButton(button, isDown);
		}
		private uint FPSToTicksPerFrame(uint fps) {

			double desiredFpms = ((int)fps) / 1000d;
			return (uint) (10000 * 1 / desiredFpms);
		}

    }
}