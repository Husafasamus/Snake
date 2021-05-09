using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Snake.Game
{
    public class Snake
    {
        private List<Position> _body;

        public Snake()
        {
            _body = new List<Position>();
        }

        public Position GetHead()
        {
            return _body[0];
        }

        public void AddBody(Position bodyPosition)
        {
            _body.Add(bodyPosition);
        }

        public void UpDatePositions(int newX, int newY)
        {
            _body[0].X = newX;
            _body[0].Y = newY;
        }
        

    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
