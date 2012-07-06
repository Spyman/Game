using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Live
{
    class MainMenu
    {
        Texture2D background;
        private TextButton btnExit;
        private TextButton btnStartGame;
        private SpriteFont segoe24;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background,new Rectangle(0,0,Game1.SCREEN_WIDTH,Game1.SCREEN_HEIGHT),Color.White);
            btnStartGame.Draw(spriteBatch, 130, 300);
            btnExit.Draw(spriteBatch, 130, 400);
        }

        public void LoadContent(ContentManager Content)
        {
            segoe24 = Content.Load<SpriteFont>("Segoe UI 24");
            background = Content.Load<Texture2D>("bluebgr");
            btnStartGame = new TextButton("start", segoe24, btnStartGame);
            btnExit = new TextButton("exit", segoe24, btnExit);
        }

        public int Update()
        {
            if (btnStartGame.OnMouseClicked())
            {
                return State.Game;
            }
            if (btnExit.OnMouseClicked())
            {
                return State.Exit;
            }
            return State.Menu;
        }
    }
}
