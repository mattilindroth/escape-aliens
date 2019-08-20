using System;
using SDL2;
namespace escape_aliens
{
    public class Game 
    {
        private int _screenWidth;
        private int _screenHeight;

    	public Game(int initialScreenWidth = 1440, int initialScreenHeight = 900) 
    	{
			_screenHeight = initialScreenHeight;
			_screenWidth = initialScreenWidth;
    	}

    	public void Run(float desiredFps) 
    	{

    	}

    }
}