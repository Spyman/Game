﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Live
{
    class Animation
    {
        Texture2D texture;
        private Color color = Color.White;
        //    double updateTime = 17;
        private Rectangle destinationRectangle;
        public Vector2 origin = new Vector2(0, 0); // Центр вращения!
        private Rectangle sourceRectangle;
        public float rotation = 0;
        private int frame = 0;
        private int frameQty = 0;
        private short type = 0;
        public float layoutDepth = 0; // слой глубина
        private float proportion = 1;
        public Vector2 condensator; // wtf?
        public SpriteEffects spriteEffect = SpriteEffects.None;
        private float endProportion = 1; 
        //  double passive = 0; ??? 

        public struct AnimationType
        {
            public const short Circle = 0;
            public const short UpAndDown = 1;
            public const short OneCircle = 2;
            public const short Revers = 3;
        }

        public Animation()
        {

        }

        public void Initializate(float proportion)
        {
            this.proportion = proportion;
            endProportion = proportion; 
        }

        public void Initializate(Vector2 position, float proportion)
        {
            destinationRectangle.X = (int)position.X;
            destinationRectangle.Y = (int)position.Y;
            this.proportion = proportion;
            endProportion = proportion; 
            sourceRectangle.X = (int)position.X;
            sourceRectangle.Y = (int)position.Y;
        }

        public void Load(ContentManager Content, string path, int frameQty)
        {
            frame = 0; 
            this.frameQty = frameQty; 
            texture = Content.Load<Texture2D>(path);
            destinationRectangle.Height = (int)(texture.Height / proportion);
            destinationRectangle.Width = (int)((texture.Width /proportion) / frameQty);
            sourceRectangle.Height = (int)(texture.Height);
            sourceRectangle.Width = (int)((texture.Width) / frameQty);
            origin.X = destinationRectangle.Center.X;
            origin.Y = destinationRectangle.Center.Y;
        }

        public void Update()
        {
            switch (type)
            {
                case 0:
                    if (frame < frameQty - 1)
                    {
                        frame++;
                    }
                    else
                    {
                        frame = 0;
                    }
                    break;
                case 1:
                    if (frame < frameQty - 1)
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
                    if (frame < frameQty - 1)
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
                        frame = frameQty - 1;
                    }
                    break;
            }
            if (condensator.X >= 1) { destinationRectangle.X += (int)condensator.X; sourceRectangle.X += (int)condensator.X; condensator.X -= (int)condensator.X; }
            if (condensator.X <= -1) { destinationRectangle.X += (int)condensator.X; sourceRectangle.X += (int)condensator.X; condensator.X -= (int)condensator.X; }
            if (condensator.Y >= 1) { destinationRectangle.Y += (int)condensator.Y; condensator.Y -= (int)condensator.Y; }
            if (condensator.Y <= -1) { destinationRectangle.Y += (int)condensator.Y; condensator.Y -= (int)condensator.Y; }
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
            sourceRectangle.X = sourceRectangle.Width * frame;
            spriteBatch.Draw(texture, 
                new Rectangle(destinationRectangle.X, destinationRectangle.Y, (int)(destinationRectangle.Width/proportion), (int)(destinationRectangle.Height / proportion)),
                sourceRectangle, color, rotation, origin, spriteEffect, layoutDepth);
        }

        public void SetCoordinates(int X, int Y)
        {
            destinationRectangle.X = X;
            destinationRectangle.Y = Y; 
        }

        public void SetCoordinates(Vector2 coordinates)
        {
            destinationRectangle.X = (int)coordinates.X;
            destinationRectangle.Y = (int)coordinates.Y; 
        }

        public Vector2 GetCoordinates()
        {
            return new Vector2(destinationRectangle.X, destinationRectangle.Y); 
        }
        
        public void IncProportion()
        {
            endProportion+=0.1f; 
        }

        public void DecProportion()
        {
            endProportion-=0.1f;
        }

        public void SetProportion(float nProportion)
        {
            endProportion = nProportion;
        }

        public void SetOrign()
        {
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
        }
        public void SetOrign(Vector2 orign)
        {
            this.origin = orign; 
        }

        public void SetFrame(int frame)
        {
            this.frame = frame; 
        }
        public void SetType(short type)
        {
            this.type = type; 
        }

        public void IncRotation(float increment)
        {
            rotation += increment; 
        }

        public void SetColor(Color color)
        {
            this.color = color; 
        }

    }
}
