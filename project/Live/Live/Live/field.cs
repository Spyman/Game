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
        protected float _zoomMin = 0.2f;


        const short _fieldWhidth = 1000;
        const short _fieldHeight = 1000; 
        const int _typeQty = 10; 
        Cell[,] net;
        Random random;
        Texture2D oneCell;
        Rectangle sourceRectangle, destinationRectangle;
        Color[,] colorMap;
        short[,] weightMap; 
        //----------------------------
        int SCREEN_HEIGHT = 600;
        int SCREEN_WIDTH = 800; 
        //----------------------------

        public Field()
        {
            //по-умочанию
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;


            random = new Random();
            colorMap = new Color[_fieldWhidth, _fieldHeight];
            net = new Cell[_fieldWhidth, _fieldHeight];
            for (int i = 0; i < _fieldWhidth; i++)
                for (int j = 0; j < _fieldHeight; j++)
                {
                    { net[i, j] = new Cell(); }
                }
            weightMap = new short[_fieldWhidth, _fieldHeight]; 
        }


        public void Initialize()
        {
            RandomGeneration(0);
            Repain(); 
        }

        public void Repain()
        {
            for (int i = 0; i < _fieldWhidth; i++)
            {
                for (int j = 0; j < _fieldHeight; j++)
                {
                    switch (net[i, j].Type)
                    {
                        case 0:
                            colorMap[i, j] = new Color(255 - weightMap[i, j], 255 - weightMap[i, j], 255 - weightMap[i, j], weightMap[i, j]);
                            break;
                        case 1:
                            colorMap[i, j] = new Color(10, weightMap[i, j], 10, weightMap[i, j]);
                            break;
                        case 2:
                            colorMap[i, j] = new Color(10, 10, weightMap[i, j], weightMap[i, j]);
                            break;
                        case 3:
                            colorMap[i, j] = new Color(weightMap[i, j], 10, 10, weightMap[i, j]);
                            break;
                        case 4:
                            colorMap[i, j] = new Color(weightMap[i, j], weightMap[i, j], 10, weightMap[i, j]);
                            break;
                        case 5:
                            colorMap[i, j] = new Color(10, weightMap[i, j], weightMap[i, j], weightMap[i, j]);
                            break;
                        case 6:
                            colorMap[i, j] = new Color(weightMap[i, j], weightMap[i, j], weightMap[i, j], weightMap[i, j]);
                            break;
                        case 7:
                            colorMap[i, j] = Color.YellowGreen;
                            break;
                        case 8:
                            colorMap[i, j] = Color.Aqua;
                            break;
                        case 9:
                            colorMap[i, j] = Color.Beige;
                            break;
                        case 10:
                            colorMap[i, j] = Color.AntiqueWhite;
                            break;
                    }
                   
                }
            }
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

        public void Update(float gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _fieldWhidth; i++)
            {
                for (int j = 0; j < _fieldHeight; j++)
                {
                    if ((destinationRectangle.X + destinationRectangle.Width * i > _pos.X - SCREEN_WIDTH / (2*_zoom)) && (destinationRectangle.X + (destinationRectangle.Width * i) < _pos.X + SCREEN_WIDTH * 3 / (2*_zoom)))
                    {
                        if ((destinationRectangle.Y + destinationRectangle.Height * j > _pos.Y - SCREEN_HEIGHT / (2 * _zoom)) && (destinationRectangle.Y + (destinationRectangle.Height * j) < _pos.Y + SCREEN_HEIGHT * 3 / (2 * _zoom)))
                        {
                            spriteBatch.Draw(oneCell,
                            new Rectangle(destinationRectangle.X + (destinationRectangle.Width) * i, destinationRectangle.Y + (destinationRectangle.Height) * j, destinationRectangle.Width, destinationRectangle.Height),
                            sourceRectangle, colorMap[i, j], 0, new Vector2(sourceRectangle.Width, sourceRectangle.Height), SpriteEffects.None, 0);
                        }
                    }
                }
            }
        }


        IntellRandomMap generator; 
        public void RandomGeneration(int level)
        {
            generator = new IntellRandomMap();
            generator.Read(level);
            generator.netGeneration();
            net = generator.net;
            short[] intelWeight = new short[_typeQty];
            random = new Random(); 
            for (int i = 0; i < _typeQty; i++)
            {
                intelWeight[i] = (short)random.Next(0, 32); 
            }
            weightMap = generator.weightMap(intelWeight);
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

      //  public void Move(Vector2 speed)
     //   {
     //       condensator -= speed;
    //        if (condensator.X >= 1) { destinationRectangle.X += (int)condensator.X; condensator.X -= (int)condensator.X; }
     //       if (condensator.X <= -1) { destinationRectangle.X += (int)condensator.X;  condensator.X -= (int)condensator.X; }
    //        if (condensator.Y >= 1) { destinationRectangle.Y += (int)condensator.Y; condensator.Y -= (int)condensator.Y; }
      //      if (condensator.Y <= -1) { destinationRectangle.Y += (int)condensator.Y; condensator.Y -= (int)condensator.Y; }
     //   }
    

    }
}
