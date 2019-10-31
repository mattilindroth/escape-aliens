using System;
using SDL2;
using escape_aliens.Engine;

namespace escape_aliens
{
    class Program
    {
        static void Main(string[] args)
        {
           Program prog = new Program();
           prog.Run();
        }

        public void Run()
        {
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0) {
                Console.WriteLine("Unable to initialize SDL. Error: {0}", SDL.SDL_GetError());
            }
            var window = new GameWindow("Escape Aliens!");
            Console.WriteLine("Window is {0}x{1}", window.GetWidth, window.GetHeight);
            var scene = new Scene(new Engine.Renderer(window));
            var game = new Game(scene, new SDL2Timer());
			game.Physics.Gravity.X = 0;
			game.Physics.Gravity.Y = 0;
            LoadResource(game);

            game.Run(65);
            
            SDL.SDL_Quit();
        }

        public void LoadResource(Game game) {
            Player p1 = new Player();
			p1.Transformation.Position.X = 300;
			p1.Transformation.Position.Y = 300;
            SpriteComponent sprite = new SpriteComponent(p1.Transformation, 3);
			ThrustComponent thrust = new ThrustComponent(2);
			p1.AddComponent(thrust);
			p1.AddComponent(sprite);
			thrust.GenerateParticles(600);
			Texture texture = game.LoadTexture(@"C:\Source\escape-aliens\Resources\SpaceShipRed.png");
			SDL.SDL_Rect renderRect;
			SDL.SDL_Rect sourceRect;
			renderRect.x = 0;
			renderRect.y = 0;
			renderRect.w = 80;
			renderRect.h = 80;
			
			sourceRect.w = 167;
			sourceRect.h = 170;
			sourceRect.x = 0;
			sourceRect.y = 0;

			texture.RenderRectangle = renderRect;
			texture.SourceRectangle = sourceRect;
			sprite.AddAnimationFrame(texture);
			
			game.Input.KeyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_D, p1.RotateRight);
			game.Input.KeyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_A, p1.RotateLeft);
			game.Input.KeyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_W, p1.Forward);
            game.AddObject(p1);

            Polygon2D poly = new Polygon2D();
            poly.AddPoint( 300, 300);
            poly.AddPoint(600, 400);
            poly.AddPoint(400,500);
            Texture polyText = game.LoadTexture(""); //TODO Continues from here.
            FilledPolygon2D filledPolygon = new FilledPolygon2D(poly);
            Asteroid asteroid = new Asteroid();
            asteroid.AddComponent(filledPolygon);
            game.AddObject(asteroid);
        }
    }
}
