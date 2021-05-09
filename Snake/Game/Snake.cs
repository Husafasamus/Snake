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

        public void AddBody(int x=0, int y=0)
        {
            if (_body.Count == 0)
            {
                _body.Add(new Position(x, y));
                return;
            }
            _body.Add(new Position(GetEnd().X, GetEnd().Y));
        }

        public void UpDatePositions(int newX, int newY)
        {                     
            if (_body.Count > 1)
            {
                for (int i = _body.Count - 1; i > 0; i--)
                {
                    _body[i].X = _body[i - 1].X;
                    _body[i].Y = _body[i - 1].Y;
                }
            }

            _body[0].X = newX;
            _body[0].Y = newY;
        }

        public Position GetBody(int index)
        {
            return _body[index];
        }

        public int Count()
        {
            return _body.Count;
        }

        public Position GetEnd()
        {
            return _body[_body.Count - 1];
        }

    }
}
