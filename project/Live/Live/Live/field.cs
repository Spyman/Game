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
        int SCREEN_HEIGHT = 600;
        int SCREEN_WIDTH = 800; 
        //----------------------------

        public Field()
        {
            random = new Random(); 
            colorMap = new Color[100, 100]; 
            net = new Cell[100,100];
            // for test/ 
            for(int i = 0; i<100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    colorMap[i,j] = new Color(random.Next(100,255),random.Next(100,255),random.Next(100,255));
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

        float kpl = 0.01f; 
        public void Update(float gameTime)
        {
            //smoth scroll !!!
            if (proportion > endProportion)
            {
                proportion -= kpl;
              //  kpl = (float)(Math.Pow(proportion*0.1f,2));
            }
            if (proportion < endProportion)
            {
                proportion += kpl;
            //    kpl = (float)(Math.Pow(proportion*0.1f,2));
            }

            //close
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                  //  if ((destinationRectangle.X + ((int)(destinationRectangle.Width / proportion) * i) > -300) && // Возможно не работает. Возможно аппаратно будет обрезаться и без условий
                  //      ((destinationRectangle.X + ((int)(destinationRectangle.Width / proportion))) < SCREEN_WIDTH)) // через T&L ? тогда будет работать быстрее без них. 
                  //  {
                     //   if ((destinationRectangle.Y + ((int)(destinationRectangle.Height / proportion) * i) > -300) &&
                     //   ((destinationRectangle.Y + ((int)(destinationRectangle.Height / proportion))) < SCREEN_HEIGHT))
                     //   {
                            spriteBatch.Draw(oneCell,
                            new Rectangle(destinationRectangle.X + ((int)(destinationRectangle.Width / proportion) * i), destinationRectangle.Y + ((int)(destinationRectangle.Height / proportion) * j), (int)(destinationRectangle.Width / proportion), (int)(destinationRectangle.Height / proportion)),
                            sourceRectangle, colorMap[i, j], 0, Vector2.Zero, SpriteEffects.None, 0);
                     //   }
                  //  }
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

        public void Move(Vector2 speed)
        {
            condensator -= speed;
            if (condensator.X >= 1) { destinationRectangle.X += (int)condensator.X; condensator.X -= (int)condensator.X; }
            if (condensator.X <= -1) { destinationRectangle.X += (int)condensator.X;  condensator.X -= (int)condensator.X; }
            if (condensator.Y >= 1) { destinationRectangle.Y += (int)condensator.Y; condensator.Y -= (int)condensator.Y; }
            if (condensator.Y <= -1) { destinationRectangle.Y += (int)condensator.Y; condensator.Y -= (int)condensator.Y; }
        }
    }
}
