using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Live
{
    class Amimation
    {
        Texture2D texture;
        public Color color = Color.White;
        //    double updateTime = 17;
        public Rectangle destinationRectangle;
        public Vector2 orign = new Vector2(0, 0); // Центр вращения!
        public Rectangle sourceRectangle;
        public float rotation = 0;
        public int frame = 0;
        public int allFrame = 0;
        public short type = 0;
        public float layoutDeath = 0; // слой глубина
        public float proportion = 1;
        public Vector2 condensator;
        public SpriteEffects spriteEffect = SpriteEffects.None;
        //  double passive = 0; ??? 

        public struct AnimationType
        {
            public const short Circle = 0;
            public const short UpAndDown = 1;
            public const short OneCircle = 2;
            public const short Revers = 3;
        }

        public Amimation()
        {

        }

        public void Initializate(float proportion)
        {
            this.proportion = proportion;
        }

        public void Initializate(Vector2 position, float proportion)
        {
            destinationRectangle.X = (int)position.X;
            destinationRectangle.Y = (int)position.Y;
            this.proportion = proportion;
            sourceRectangle.X = (int)position.X;
            sourceRectangle.Y = (int)position.Y;
        }

        public void Load(ContentManager Content, string path)
        {
            texture = Content.Load<Texture2D>(path);
            destinationRectangle.Height = (int)(texture.Height * proportion);
            destinationRectangle.Width = (int)((texture.Width * proportion) / allFrame);
            sourceRectangle.Height = (int)(texture.Height);
            sourceRectangle.Width = (int)((texture.Width) / allFrame);
            orign.X = destinationRectangle.Center.X;
            orign.Y = destinationRectangle.Center.Y;
        }

        public void Update()
        {
            switch (type)
            {
                case 0:
                    if (frame < allFrame - 1)
                    {
                        frame++;
                    }
                    else
                    {
                        frame = 0;
                    }
                    break;
                case 1:
                    if (frame < allFrame - 1)
                    {
                        frame++;
                    }
                    else
                    {
                        type = -1; frame--;
                    }
                    break;
                case -1:
                    if (frame > 0)
                    {
                        frame--;
                    }
                    else
                    {
                        type = 1; frame++;
                    }
                    break;
                case 2:
                    if (frame < allFrame - 1)
                    {
                        frame++;
                    }
                    break;
                case 3:
                    if (frame > 0)
                    {
                        frame--;
                    }
                    else
                    {
                        frame = allFrame - 1;
                    }
                    break;
            }
            if (condensator.X >= 1) { destinationRectangle.X += (int)condensator.X; sourceRectangle.X += (int)condensator.X; condensator.X -= (int)condensator.X; }
            if (condensator.X <= -1) { destinationRectangle.X += (int)condensator.X; sourceRectangle.X += (int)condensator.X; condensator.X -= (int)condensator.X; }
            if (condensator.Y >= 1) { destinationRectangle.Y += (int)condensator.Y; condensator.Y -= (int)condensator.Y; }
            if (condensator.Y <= -1) { destinationRectangle.Y += (int)condensator.Y; condensator.Y -= (int)condensator.Y; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sourceRectangle.X = sourceRectangle.Width * frame;
            spriteBatch.Draw(texture, 
                new Rectangle(destinationRectangle.X, destinationRectangle.Y, (int)(destinationRectangle.Width/proportion), (int)(destinationRectangle.Height / proportion)),
                sourceRectangle, color, rotation, orign, spriteEffect, layoutDeath);
        }

    }
}
