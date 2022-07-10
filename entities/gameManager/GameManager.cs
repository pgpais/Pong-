using System;
using System.Collections.Generic;
using System.Linq;
using Cheese.Players;
using Godot;

public class GameManager : Node2D
{
    [Export]
    public NodePath PaddleManagerPath { get; private set; }

    public List<Player> players = new List<Player>();

    public GameManager()
    {

    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetupPlayerColors();

        SetupSignals();
    }

    private void SetupPlayerColors()
    {
        players = new Godot.Collections.Array<Player>(GetTree().GetNodesInGroup("player")).ToList();

        Color colorPlayer1 = players.First((p) => p.PlayerNumber == PlayerNumber.One).PlayerColor;
        Color colorPlayer2 = players.First((p) => p.PlayerNumber == PlayerNumber.Two).PlayerColor;

        if (PaddleManagerPath != null && !PaddleManagerPath.IsEmpty())
        {
            var paddleManager = GetNode<PaddleManager>(PaddleManagerPath);
            paddleManager.SetPaddleColor(PlayerNumber.One, colorPlayer1);
            paddleManager.SetPaddleColor(PlayerNumber.Two, colorPlayer2);
        }
        else
        {
            GD.PushWarning("PaddleManagerPath is empty, will not set color");
        }
    }

    private void SetupSignals()
    {
        Goal.Goals.ForEach((goal) =>
        {
            goal.Connect(nameof(Goal.GoalScored), this, nameof(OnGoalScored));
        });
    }

    private void OnGoalScored(PlayerNumber playerNumber)
    {
        GD.Print($"Goal scored by Player {playerNumber.Next()}");
    }
}
