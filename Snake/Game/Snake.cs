using System.Collections.Generic;

namespace Snake.Game
{
    public class Snake
    {
        private List<Position> _body;

        private int x_lastAdd = 0;
        private int y_lastAdd = 0;
        private Direction dir_lastAdd = Direction.Up;

        public Snake()
        {
            _body = new List<Position>();
        }

        public Snake(Snake other)
        {
            _body = new List<Position>();
            for (int i = 0; i < other.Count(); i++)
            {
                _body.Add(new Position(other.GetBody(i).X, other.GetBody(i).Y));
            }
        }

        public Position GetHead()
        {
            return _body[0];
        }

        public void AddBody(int x = 0, int y = 0, Direction direction = Direction.Up)
        {
            if (_body.Count == 0)
            {
                _body.Add(new Position(x, y, direction));
                return;
            }
            _body.Add(new Position(x_lastAdd, y_lastAdd, dir_lastAdd));
        }

        public void UpDatePositions(int newX, int newY, Direction direction)
        {
            x_lastAdd = GetEnd().X;
            y_lastAdd = GetEnd().Y;
            dir_lastAdd = GetEnd().Facing;

            if (_body.Count > 1)
            {
                for (int i = _body.Count - 1; i > 0; i--)
                {
                    _body[i].X = _body[i - 1].X;
                    _body[i].Y = _body[i - 1].Y;
                    _body[i].Facing = _body[i - 1].Facing;
                }
            }

            if (newX == -1)
                newX = 19;
            if (newX == 20)
                newX = 0;
            if (newY == -1)
                newY = 19;
            if (newY == 20)
                newY = 0;

            _body[0].X = newX;
            _body[0].Y = newY;
            _body[0].Facing = direction;
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
