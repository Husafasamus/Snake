﻿namespace Snake.Game
{
    public class Snail
    {
        public Position Place { get; set; }

        public Snail()
        {
            Place = new Position(-1, -1);
        }

        public void SetPosition(int x, int y)
        {
            Place.X = x;
            Place.Y = y;
        }
    }
}
