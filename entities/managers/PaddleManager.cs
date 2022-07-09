using System.Linq;
using Cheese.Players;
using Godot;

public class PaddleManager : Node
{
    public void SetPaddleColor(PlayerNumber playerNumber, Color color)
    {
        Paddle.Paddles.First((p) => p.PlayerNumber == playerNumber).SetColor(color);
    }
    public void SetAllPaddleColor(Color color)
    {
        Paddle.Paddles.ForEach((p) => p.SetColor(color));
    }
}