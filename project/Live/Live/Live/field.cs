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
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation

        protected float _zoomMax = 2f;
        protected float _zoomMin = 0.5f;

        const int _typeQty = 10;
        Cell[,] net;
        Random random;
        Texture2D oneCell;
        Rectangle sourceRectangle, destinationRectangle;
        Color[,] colorMap;

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
                }
            }
            //---------/
        }

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
        }

        public void LoadContent(ContentManager Content)
        {
            oneCell = Content.Load<Texture2D>("oneCell");
            sourceRectangle.Height = oneCell.Height;
            sourceRectangle.Width = oneCell.Width;
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

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {

                    spriteBatch.Draw(oneCell,
                    new Rectangle(destinationRectangle.X + ((int)(destinationRectangle.Width) * i), destinationRectangle.Y + ((int)(destinationRectangle.Height) * j), (int)(destinationRectangle.Width), (int)(destinationRectangle.Height)),
                    sourceRectangle, colorMap[i, j], 0, new Vector2(sourceRectangle.Width, sourceRectangle.Height), SpriteEffects.None, 0);

                }
            }
        }

        public void RandomGeneration(int level)
        {
            random = new Random();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    net[i, j].Type = (short)random.Next(0, _typeQty);
                }
            }
        }
    }
}
