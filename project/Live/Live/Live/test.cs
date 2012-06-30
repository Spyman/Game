using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Live
{
    class Test
    {
        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;
        public float proportionS = 1f;
        //testing 
        Field testfield;

        //closeTesting
        
        public Test()
        {
            ///
            testfield = new Field();
            ///
        }

        MouseState mouse;
        Vector2 screenSenter;
        Vector2 mouseVector; 
        public void Initialize()
        {
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
            testfield.Initialize();
            screenSenter = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
        }

        public void LoadContent(ContentManager Content)
        {
            testfield.LoadContent(Content);
        }

        int scrollPosition;
        double oneCircleTime = 0; 
        public void Update(GameTime gameTime)
        {
            oneCircleTime += gameTime.ElapsedGameTime.Milliseconds;
            if (oneCircleTime > 10)
            {
                oneCircleTime = 0;
                testfield.Update(gameTime.ElapsedGameTime.Milliseconds); 
            }
            mouse = Mouse.GetState();

            if (mouse.ScrollWheelValue > scrollPosition)
            {
                testfield.IncProportion();
            }
            if (mouse.ScrollWheelValue < scrollPosition)
            {
                testfield.DecProportion();
            }
            scrollPosition = mouse.ScrollWheelValue;
            mouseVector.X = mouse.X - screenSenter.X;
            mouseVector.Y = mouse.Y - screenSenter.Y;
            if (mouseVector.Length() > 200 * proportionS)
            {
                mouseVector.X = mouseVector.X * Math.Abs(mouseVector.X) * 0.0001f;
                mouseVector.Y = mouseVector.Y * Math.Abs(mouseVector.Y) * 0.0001f;
                testfield.Move(mouseVector);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            testfield.Draw(spriteBatch);
        }
    }
}
