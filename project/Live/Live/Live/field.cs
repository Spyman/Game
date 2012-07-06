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
<<<<<<< HEAD
        const int _typeQty = 10; 
=======
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation

        protected float _zoomMax = 2f;
        protected float _zoomMin = 0.5f;

        const int _typeQty = 10;
>>>>>>> final
        Cell[,] net;
        Random random;
        Texture2D oneCell;
        Rectangle sourceRectangle, destinationRectangle;
        Color[,] colorMap;
<<<<<<< HEAD
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
=======

        public Field()
        {
            //по-умочанию
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;

            random = new Random();
            colorMap = new Color[100, 100];
            net = new Cell[100, 100];
            // for test/ 
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    colorMap[i, j] = new Color(random.Next(100, 255), random.Next(100, 255), random.Next(100, 255));
>>>>>>> final
                }
            }
            //---------/
        }

<<<<<<< HEAD

        public void Initialize()
        {

=======
        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = MathHelper.Clamp(value, _zoomMin, _zoomMax); } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
>>>>>>> final
        }

        public void LoadContent(ContentManager Content)
        {
            oneCell = Content.Load<Texture2D>("oneCell");
            sourceRectangle.Height = oneCell.Height;
            sourceRectangle.Width = oneCell.Width;
<<<<<<< HEAD
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
=======
            destinationRectangle.Height = (int)(oneCell.Height / _zoom);
            destinationRectangle.Width = (int)(oneCell.Width / _zoom);
        }

        public Matrix GetTransform(GraphicsDevice graphicsDevice)
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(Game1.SCREEN_WIDTH * 0.5f, Game1.SCREEN_HEIGHT * 0.5f, 0));
            return _transform;
        }

        public void Update()
        {

>>>>>>> final
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
<<<<<<< HEAD
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
=======

                    spriteBatch.Draw(oneCell,
                    new Rectangle(destinationRectangle.X + ((int)(destinationRectangle.Width) * i), destinationRectangle.Y + ((int)(destinationRectangle.Height) * j), (int)(destinationRectangle.Width), (int)(destinationRectangle.Height)),
                    sourceRectangle, colorMap[i, j], 0, new Vector2(sourceRectangle.Width, sourceRectangle.Height), SpriteEffects.None, 0);

>>>>>>> final
                }
            }
        }

        public void RandomGeneration(int level)
        {
<<<<<<< HEAD
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
=======
            random = new Random();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    net[i, j].Type = (short)random.Next(0, _typeQty);
                }
            }
        }
>>>>>>> final
    }
}
