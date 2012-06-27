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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //testing 
        Amimation testAnimation; 
        //closeTesting
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            testAnimation = new Amimation(); 
        }

        protected override void Initialize()
        {
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
            testAnimation.allFrame = 10;
            testAnimation.Initializate(1f);
            testAnimation.type = Amimation.AnimationType.Circle;
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
            testAnimation.Load(Content, "Converted");
            testAnimation.condensator += new Vector2(300, 300);
            testAnimation.Update();
            testAnimation.frame = 0;
            testAnimation.orign = new Vector2(testAnimation.sourceRectangle.Width/2, testAnimation.sourceRectangle.Height/2);
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
        }

        protected override void UnloadContent()
        {

        }

        int mouseLook;
        double oneCircleTime = 0; 

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

           
            if (Mouse.GetState().ScrollWheelValue > mouseLook)
            {
                testAnimation.proportion += 0.1f; 
            }
            if (Mouse.GetState().ScrollWheelValue < mouseLook)
            {
                testAnimation.proportion -= 0.1f; 
            }


            mouseLook = Mouse.GetState().ScrollWheelValue;
            oneCircleTime += gameTime.ElapsedGameTime.Milliseconds;
            if (oneCircleTime > 10)
            {
                testAnimation.Update();
                oneCircleTime = 0; 
            }
            testAnimation.rotation += 0.01f;
            testAnimation.destinationRectangle.X = Mouse.GetState().X;
            testAnimation.destinationRectangle.Y = Mouse.GetState().Y; 
            base.Update(gameTime);
        }
        Vector2 speed; 

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(); 
            testAnimation.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
