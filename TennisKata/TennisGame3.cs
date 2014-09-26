using System;

namespace Tennis
{
    public class TennisGame3 : TennisGame
    {
        private int scorePlayer2;
        private int scorePlayer1;
        private string player1Name;
        private string player2Name;

        public TennisGame3(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
        }

        public string GetScore()
        {
            string s;
            if ((scorePlayer1 < 4 && scorePlayer2 < 4) && (scorePlayer1 != 3 || scorePlayer2 != 3))
            {
                string[] p = new String[] { "Love", "Fifteen", "Thirty", "Forty" };
                s = p[scorePlayer1];
                return (scorePlayer1 == scorePlayer2) ? s + "-All" : s + "-" + p[scorePlayer2];
            }

            if (scorePlayer1 == scorePlayer2)
                return "Deuce";
            s = scorePlayer1 > scorePlayer2 ? player1Name : player2Name;
            return (Math.Abs(scorePlayer1 - scorePlayer2) == 1) ? "Advantage " + s : "Win for " + s;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
                this.scorePlayer1++;
            else
                this.scorePlayer2++;
        }

    }
}

