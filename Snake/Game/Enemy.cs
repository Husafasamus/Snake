namespace Snake.Game
{
    public class Enemy
    {
        public Position Place { get; set; }
        public bool Break { get; set; }

        public Enemy()
        {
            Place = new Position(-1, -1);
            Break = false;
        }

        public void SetPosition(int x, int y)
        {
            if (x < 0)
                x = 19;
            if (x > 19)
                x = 0;
            if (y < 0)
                y = 19;
            if (y > 19)
                y = 0;

            Place.X = x;
            Place.Y = y;
        }
    }
}
