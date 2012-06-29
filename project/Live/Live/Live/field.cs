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
    class Field
    {
        const int _typeQty = 10; 
        Cell[,] net;
        Random random;
        Texture2D oneCell;
        Rectangle sourceRectangle, destinationRectangle;
        Color[,] colorMap;
        float proportion = 1f;
        float endProportion = 1f;
        Vector2 condensator; 
        //----------------------------
        int resolutionHeight = 600;
        int resolutionWidth = 800; 
        //----------------------------

        public Field()
        {
            colorMap = new Color[100, 100]; 
            net = new Cell[100,100];
            // for test/ 
            for(int i = 0; i<100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    colorMap[i,j] = Color.White;
                }
            }
            //---------/
        }


        public void Initialize()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            oneCell = Content.Load<Texture2D>("oneCell");
            sourceRectangle.Height = oneCell.Height;
            sourceRectangle.Width = oneCell.Width;
            destinationRectangle.Height = (int)(oneCell.Height / proportion);
            destinationRectangle.Width = (int)(oneCell.Width / proportion);
        }

        public void Update(float gameTime)
        {
            //smoth scroll !!!
            if (proportion > endProportion)
            {
                proportion -= 0.01f;
            }
            if (proportion < endProportion)
            {
                proportion += 0.01f;
            }
            //close
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if ((destinationRectangle.X + ((int)(destinationRectangle.Width / proportion) * i) > -150) && // Возможно не работает. Возможно аппаратно будет обрезаться и без условий
                        ((destinationRectangle.X + ((int)(destinationRectangle.Width / proportion))) < resolutionWidth)) // через T&L ? тогда будет работать быстрее без них. 
                    {
                        if ((destinationRectangle.Y + ((int)(destinationRectangle.Height / proportion) * i) > -150) &&
                        ((destinationRectangle.Y + ((int)(destinationRectangle.Height / proportion))) < resolutionHeight))
                        {
                            spriteBatch.Draw(oneCell,
                            new Rectangle(destinationRectangle.X + ((int)(destinationRectangle.Width / proportion) * i), destinationRectangle.Y + ((int)(destinationRectangle.Height / proportion) * j), (int)(destinationRectangle.Width / proportion), (int)(destinationRectangle.Height / proportion)),
                            sourceRectangle, colorMap[i, j], 0, Vector2.Zero, SpriteEffects.None, 0);
                        }
                    }
                }
            }
        }

        public void RandomGeneration(int level)
        {
            random = new Random(); 
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j<100; j++)
                {
                    net[i, j].Type = (short)random.Next(0, _typeQty); 
                }
            }
        }

        public void IncProportion()
        {
            endProportion += 0.1f;
        }

        public void DecProportion()
        {
            endProportion -= 0.1f;
        }

        public void SetProportion(float nProportion)
        {
            endProportion = nProportion;
        }

        public void ShiftLeft(float speed)
        {
            condensator.X -= speed;
            if (condensator.X <= -1)
            {
                destinationRectangle.X += (int)condensator.X;
                condensator.X -= (int)condensator.X; 
            }
        }
        public void ShiftRight(float speed)
        {
            condensator.X += speed; 
            if (condensator.X >= 1)
            {
                destinationRectangle.X += (int)condensator.X;
                condensator.X -= (int)condensator.X;
            }
        }
    }
}
