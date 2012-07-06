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
using System.IO; 


namespace Live
{
    class IntellRandomMap
    {
        const short _qtyOfEllementTable = 10;
        const string _PATH = "gp";
        short[,] data;
        const short _fieldWhidth = 1000;
        const short _fieldHeight = 1000;
        public Cell[,] net;
        Random random; 
        public IntellRandomMap()
        {
            net = new Cell[_fieldWhidth, _fieldHeight];
            data = new short[_qtyOfEllementTable,_qtyOfEllementTable];
            random = new Random(); 
        }


        public void Read(int levelNum)
        {
            string[] levelStr;
            levelStr = File.ReadAllLines("Content//LevelTable//gp" + Convert.ToString(levelNum));
            for (int i = 0; i < _qtyOfEllementTable; i++)
            {
                string[] buff = levelStr[i].Split('\t');
                for (int j = 0; j < _qtyOfEllementTable; j++)
                {
                    data[i, j] = Convert.ToInt16(buff[j]); 
                }
            }
        }

        short vRandom(short[] neighbours)
        {
            int resultType = 0;
            int r;
            int resultScore = random.Next(0, neighbours[0]+40); 
            for (int i = 0; i < _qtyOfEllementTable; i++)
            {
                r = random.Next(0, neighbours[i]);
                if (r > resultScore)
                {
                    resultScore = r; resultType = i; 
                }
            }
            return (short)resultType; 
        }

        public void netGeneration()
        {
            for (int i = 0; i < _fieldHeight; i++) // Только для квадратных полей!!!
            {
                net[0, i] = new Cell(0, 200); 
            }
            for (int j = 0; j < _fieldWhidth; j++)
            {
                net[j, 0] = new Cell(0, 200); 
            }

            for (int i = 1; i < _fieldHeight; i++)
            {
                for (int j = 1; j < _fieldWhidth; j++)
                {
                    short[] neighbours = new short[_qtyOfEllementTable]; 
                    for (int z = 0; z < _qtyOfEllementTable; z++)
                    {
                        neighbours[z] += data[net[i - 1, j - 1].Type, z];
                        neighbours[z] += data[net[i - 0, j - 1].Type, z];
                        neighbours[z] += data[net[i - 1, j - 0].Type, z];
                    }
                    net[i, j] = new Cell(vRandom(neighbours), 0); 
                }
            }
        }

        public short[,] weightMap(short[] intelWeight)
        {
            short[,] weightMap = new short[_fieldWhidth, _fieldHeight]; 
            for (int i = 1; i < _fieldWhidth-1; i++)
            {
                for (int j = 1; j < _fieldHeight-1; j++)
                {
                    weightMap[i,j] = (short)(intelWeight[net[i - 1, j - 1].Type] + intelWeight[net[i, j - 1].Type]
                        + intelWeight[net[i + 1, j - 1].Type] + intelWeight[net[i - 1, j].Type]
                        + intelWeight[net[i + 1, j].Type] + intelWeight[net[i + 1, j - 1].Type]
                        + intelWeight[net[i, j - 1].Type] + intelWeight[net[i + 1, j - 1].Type]); 
                }
            }
            return weightMap;
        }
    }
}
