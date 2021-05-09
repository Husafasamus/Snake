﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Snake.Game
{
    public class GameField
    {
        public const int CubeWidth = 40;
        public const int CubeHeight = 40;

        public int cRectanglesOnWidth;
        public int cRectanglesOnHeight;

        public Cube[,] gameField { get; set; }

        public GameField(int width, int height)
        {
            cRectanglesOnWidth = width / CubeWidth;
            cRectanglesOnHeight = height / CubeHeight;

            gameField = new Cube[cRectanglesOnWidth, cRectanglesOnHeight];
            InitalizeGameField();
        }

        private void InitalizeGameField()
        {
            for (int x = 0; x < cRectanglesOnWidth; x++)
            {
                for (int y = 0; y < cRectanglesOnHeight; y++)
                {
                    Rectangle cubeRectangle = new Rectangle()
                    {
                        Width = CubeWidth,
                        Height = CubeHeight,
                        Fill = Brushes.Black
                    };

                    gameField[x, y] = new Cube(CubeWidth * x, CubeHeight * y, cubeRectangle, GameInfo.NaN);
                }
            }
        }

        public void ResetField() 
        {
            InitalizeGameField();
        }
    }
}