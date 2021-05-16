using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace Snake.Game
{
    // TODO: 4. ScoreBoard

    public class PlayerScore
    {
        private string _name;
        private int _size;

        public PlayerScore(string name, int size)
        {
            _name = name;
            _size = size;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetSize()
        {
            return _size;
        }

        public override string ToString()
        {
            return $"{_name}: {_size}";
        }
    }

    public class ScoreBoard
    {
        List<PlayerScore> scoreBoard = null;

        public ScoreBoard()
        {
            this.scoreBoard = new List<PlayerScore>();
            LoadFromCsv();
        }

        public void AddPlayerScore(string name, int size)
        {
            if (scoreBoard.Count > 39)
            {
                var lastPlayer = this.scoreBoard.OrderBy(x => x.GetSize()).FirstOrDefault();
                if (size > lastPlayer.GetSize())
                {
                    int i = this.scoreBoard.FindLastIndex(x => x.GetSize() == lastPlayer.GetSize() && x.GetName() == lastPlayer.GetName());
                    scoreBoard[i] = new PlayerScore(name, size);
                }
                return;
            }
            scoreBoard.Add(new PlayerScore(name, size));
        }

        public IEnumerable<PlayerScore> GetSortedByScore()
        {
            return this.scoreBoard.OrderByDescending(x => x.GetSize());
        }

        public List<PlayerScore> GetScoreBoard()
        {    
            return scoreBoard;
        }

        public override string ToString()
        {
            string tmp = "";
            for (int i = 0; i < scoreBoard.Count; i++)
            {
                tmp += $"{i}." + scoreBoard[i].ToString() + "\n";
            }
            
            return tmp;
        }

        private void LoadFromCsv()
        {
            var scoreBoardCsv = File.ReadAllLines("ScoreBoard.txt");
            for (int i = 1; i < scoreBoardCsv.Length; i++)
            {
                var splitedLine = scoreBoardCsv[i].Split(';');
                AddPlayerScore(splitedLine[0], int.Parse(splitedLine[1]));
            }
        }

        public void PrintToCsv()
        {
            using (StreamWriter outputFile = new StreamWriter("ScoreBoard.txt"))
            {
                outputFile.WriteLine("player;score");
                foreach (var player in this.GetSortedByScore())
                {
                    outputFile.WriteLine($"{player.GetName()};{player.GetSize()}");
                }
            }
        }
    }
}
