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
        Field testfield;
       // Texture2D background; // Удалить!!!

        public Test()
        {
            testfield = new Field();
        }

        MouseState mouse;
        Vector2 screenSenter;
        Vector2 mouseVector;
        int scrollPosition;
        double oneCircleTime = 0; 

        public void Initialize()
        {
            testfield.Initialize();
            screenSenter = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
        }

        public void LoadContent(ContentManager Content)
        {
            testfield.LoadContent(Content);
            //background = Content.Load<Texture2D>("lum"); // Удалить!!!
        }

       
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
                testfield.Zoom += 0.2f;
            }
            if (mouse.ScrollWheelValue < scrollPosition)
            {
                testfield.Zoom -= 0.2f;
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

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, null, null, null, testfield.GetTransform(graphicsDevice));
           // spriteBatch.Draw(background, new Rectangle((int)testfield._pos.X- SCREEN_WIDTH/2, (int)testfield._pos.Y - SCREEN_HEIGHT/2, background.Width, background.Height), Color.White); 
            testfield.Draw(spriteBatch);
            spriteBatch.End(); 
        }
    }
}
