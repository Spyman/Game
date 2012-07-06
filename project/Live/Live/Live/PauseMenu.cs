using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Live
{
    class PauseMenu
    {
        Texture2D background;
        private TextButton btnExit;
        private TextButton btnResume;
        private SpriteFont segoe24;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT), Color.Black);
            btnResume.Draw(spriteBatch, 130, 300);
            btnExit.Draw(spriteBatch, 130, 400);
        }

        public void LoadContent(ContentManager Content, GraphicsDeviceManager graphics)
        {
            background = new Texture2D(graphics.GraphicsDevice,1,1,false,SurfaceFormat.Color);
            background.SetData<Color>(new Color[] { new Color(0, 0, 0, 170) });
            segoe24 = Content.Load<SpriteFont>("Segoe UI 24");
            btnResume = new TextButton("resume", segoe24, btnResume);
            btnExit = new TextButton("exit", segoe24, btnExit);
        }

        public int Update()
        {
            if (btnResume.OnMouseClicked())
            {
                return State.Game;
            }
            if (btnExit.OnMouseClicked())
            {
                return State.Exit;
            }
            return State.Pause;
        }
    }
}
