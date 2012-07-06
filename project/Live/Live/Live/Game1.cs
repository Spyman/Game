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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont segoe24;
        private bool gameRunning = false;
        private bool gameStarted = false;
        private KeyboardState oldState;
        //testing 
        Test tTest; 
        private TextButton btnStartGame;
        private TextButton btnResume;
        private TextButton btnExit;
        //closeTesting
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;
            tTest = new Test(); 
        }

        protected override void Initialize()
        {
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
            tTest.Initialize(); 
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
            oldState = Keyboard.GetState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            segoe24 = Content.Load<SpriteFont>("Segoe UI 24");
            tTest.LoadContent(Content);

            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
            btnStartGame = new TextButton("start", segoe24, btnStartGame);
            btnResume = new TextButton("resume", segoe24, btnResume);
            btnExit = new TextButton("exit", segoe24, btnExit);
            //TESTING !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TESTING//
        }
        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {

            this.IsMouseVisible = true;
            UpdateInput(gameTime);
            if (gameRunning)
            {
                tTest.Update(gameTime); 
            }
            base.Update(gameTime);
        }

        //обновление клавиатуры, мыши, геймпада
        private void UpdateInput(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
          
            if (newState.IsKeyDown(Keys.Escape) && gameStarted)
            {
                // If not down last update, key has just been pressed.                
                if (!oldState.IsKeyDown(Keys.Escape) && gameRunning)
                {
                    gameRunning = false;
                }
                else
                    if (!oldState.IsKeyDown(Keys.Escape) && !gameRunning )
                    {
                        gameRunning = true;
                    }
            }

            if(btnStartGame.OnMouseClicked())
            {
                gameStarted = true;
                gameRunning = true;
            }
            if (btnResume.OnMouseClicked())
            {
                gameRunning = true;
            }
            if(btnExit.OnMouseClicked())
            {
                this.Exit();
            }
            //обновляем состояние клавиатуры
            oldState = newState;

        } 

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if(gameRunning && gameStarted)
            {
                DrawGame();
            }
            if(!gameStarted)
            {
                DrawMainMenu();
            }
            if (gameStarted && !gameRunning)
            {
                DrawPauseMenu();
            }

            base.Draw(gameTime);
        }

        private void DrawPauseMenu()
        {
            spriteBatch.Begin();
            btnResume.Draw(spriteBatch, 100, 100);
            btnExit.Draw(spriteBatch, 100, 150);
            spriteBatch.End();
        }

        private void DrawMainMenu()
        {
            spriteBatch.Begin();
            btnStartGame.Draw(spriteBatch,100,100);

            btnExit.Draw(spriteBatch, 100, 150);
            spriteBatch.End();
        }

        private void DrawGame()
        {
            
            tTest.Draw(spriteBatch, GraphicsDevice); 
            
        }
    }
}
