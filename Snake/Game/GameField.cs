namespace Snake.Game
{
    public class GameField
    {
        public const int CubeWidth = 40;
        public const int CubeHeight = 40;

        private int cRectanglesOnWidth;
        private int cRectanglesOnHeight;

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
                    gameField[x, y] = Cube.GetNaNImage(x, y);
                }
            }
        }

        public int GetRectanglesOnWidth()
        {
            return cRectanglesOnWidth;
        }

        public int GetRectanglesOnHeight()
        {
            return cRectanglesOnHeight;
        }

        public void ResetField()
        {
            InitalizeGameField();
        }
    }
}
