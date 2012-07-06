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
        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 600;
        public float proportionS = 1f; 
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public SpriteFont segoe24;
        private bool gameRunning = false;
        private bool gameStarted = false;
        private KeyboardState oldState;
        private MainMenu menu;
        private PauseMenu pause;
        public int state;


        MouseState mouse;
        Vector2 screenCenter;
        Vector2 mouseVector;

        int scrollPosition;
        double oneCircleTime = 0; 


        //testing 
        Field testfield;
        //closeTesting
        public Game1()
        {
            state = State.Menu;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;

            //test
            testfield = new Field(); 
        }

        protected override void Initialize()
        {
            menu = new MainMenu();
            pause = new PauseMenu();
            oldState = Keyboard.GetState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            segoe24 = Content.Load<SpriteFont>("Segoe UI 24");
            testfield.LoadContent(Content);
            menu.LoadContent(Content);
            pause.LoadContent(Content,graphics);
        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            this.IsMouseVisible = true;
            switch (state)
            {
                case State.Menu:
                    {
                        state = menu.Update();
                        break;
                    } 
                case State.Game:
                    {
                        gameStarted = true;
                        state = UpdateGame(gameTime);
                        break;
                    }
                case State.Pause:
                    {
                        state = pause.Update();
                        break;
                    }
                case State.Exit:
                    {
                        this.Exit();
                        break;
                    } 
            }

            base.Update(gameTime);
        }

        //обновление игрового процесса
        private int UpdateGame(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
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
            mouseVector.X = mouse.X - SCREEN_WIDTH/2;
            mouseVector.Y = mouse.Y - SCREEN_HEIGHT/2;
            if (mouseVector.Length() > 200 * proportionS)
            {
                mouseVector.X = mouseVector.X * Math.Abs(mouseVector.X) * 0.0001f;
                mouseVector.Y = mouseVector.Y * Math.Abs(mouseVector.Y) * 0.0001f; 
                testfield.Move(mouseVector);
            }
            testfield.Update();
            
            if (newState.IsKeyDown(Keys.Escape) && gameStarted)
            {              
                if (!oldState.IsKeyDown(Keys.Escape))
                {
                    return State.Pause;
                }
            }
            oldState = newState;
            return State.Game;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            switch (state)
            {
                case State.Game:
                    {
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, null, null, null, testfield.GetTransform(GraphicsDevice));
                        DrawGame();
                        spriteBatch.End();
                        break;
                    }
                case State.Menu:
                    {
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, null, null, null);
                        menu.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }
                case State.Pause:
                    {
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, null, null, null, testfield.GetTransform(GraphicsDevice));
                        DrawGame();
                        spriteBatch.End();
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, null, null, null);
                        pause.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }
            }
            base.Draw(gameTime);
        }


        private void DrawGame()
        {        
            testfield.Draw(spriteBatch);
        }
    }
}
