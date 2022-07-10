using Cheese.Players;
using Godot;

public class ScoreManager : Node
{
    public int[] Score { get; private set; } = { 0, 0 };

    public ScoreManager()
    {
        Score = new int[] { 0, 0 };
    }

    void AddScore(PlayerNumber playerNumber)
    {
        Score[(int)playerNumber]++;
    }

    void ResetScore()
    {
        Score = new int[] { 0, 0 };
    }

    void SetScore(int[] score)
    {
        Score = score;
    }
}