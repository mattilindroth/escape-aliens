﻿using System;
using SDL2;
using escape_aliens.Engine;
using escape_aliens.Engine.MathExtra;

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
            p1.Transformation.Size = 1.0;
            SpriteComponent sprite = new SpriteComponent(p1.Transformation, 3);
			ThrustComponent thrust = new ThrustComponent(2);
			p1.AddComponent(thrust);
			p1.AddComponent(sprite);
			thrust.GenerateParticles(600);
			Texture texture = game.LoadTexture(@"C:\Source\escape-aliens\Resources\SpaceShipRed.png", false);
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
            game.Input.KeyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_KP_MINUS, p1.Shrink);
            game.Input.KeyboardBindings.AddMapping(SDL.SDL_Scancode.SDL_SCANCODE_KP_PLUS, p1.Enlarge);
            game.AddObject(p1);

            Polygon2D poly = new Polygon2D();
            poly.AddPoint(0, 0);
            poly.AddPoint(100,100);
            poly.AddPoint(300, 100);
            // poly.AddPoint(game.Scene.Renderer.WindowWidth, 0);
            // poly.AddPoint(game.Scene.Renderer.WindowWidth, game.Scene.Renderer.WindowHeight);
            // poly.AddPoint(0,game.Scene.Renderer.WindowHeight);
            
            Texture polyText = game.LoadTexture(@"C:\Source\escape-aliens\Resources\MapForeground.png", true); 
            FilledPolygon2D filledPolygon = new FilledPolygon2D(poly, polyText);
            game.Input.MouseBindings.RegisterMouseMovementListener(filledPolygon.MouseMove);

            Asteroid asteroid = new Asteroid();
            asteroid.AddComponent(filledPolygon);
            game.AddObject(asteroid);
        }
    }
}
