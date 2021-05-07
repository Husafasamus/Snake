using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Game
{
    class Snake
    {
        public Cube Head { get; set; }
        public List<Cube> Body { get; set; }

        public Snake()
        {
        }
    }
}
