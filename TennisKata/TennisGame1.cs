using System;
using System.Collections.Generic;

namespace Tennis
{
  public class TennisGame1 : TennisGame
  {
    private int m_score1 = 0;
    private int m_score2 = 0;
    private string player1Name;
    private string player2Name;

    private static Dictionary<int, string> SCORES = new Dictionary<int,string>() 
    { 
        { 0, "Love" }, 
        { 1, "Fifteen" }, 
        { 2, "Thirty" }, 
        { 3, "Forty" } 
    };

    public TennisGame1 (string player1Name, string player2Name)
    {
      this.player1Name = player1Name;
      this.player2Name = player2Name;
    }

    public void WonPoint (string playerName)
    {
        if (playerName == player1Name)
            m_score1 += 1;
        else if (playerName == player2Name)
            m_score2 += 1;
        else
            throw new ArgumentException("Unknown player");
    }

    public string GetScore ()
    {
        if (m_score1==m_score2)
        {
            return GetEqualScore();
        }
        if (m_score1>=4 || m_score2>=4)
        {
            return GetScoreOverFour();      
        }
        return SCORES[m_score1] + "-" + SCORES[m_score2];
    }

    private string GetScoreOverFour()
    {
        int minusResult = m_score1 - m_score2;
        switch (minusResult)
        {
            case -1:
                return "Advantage " + player2Name;
            case 1:
                return "Advantage " + player1Name;
            default:
                return minusResult > 0
                    ? "Win for " + player1Name
                    : "Win for " + player2Name;
        }
    }

    private string GetEqualScore()
    {
        return m_score1 > 2
            ? "Deuce"
            : SCORES[m_score1] + "-All";
    }
  }

}

