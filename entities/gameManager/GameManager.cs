using System;
using System.Collections.Generic;
using System.Linq;
using Cheese.Players;
using Godot;
using Godot.Collections;

public class GameManager : Node2D
{
    [Export]
    public NodePath PaddleManagerPath { get; private set; }
    [Export]
    public int GameStartCountdownTime { get; private set; } = 5;



    public List<Player> players = new List<Player>();

    private Map map;

    public GameManager()
    {

    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetReferences();
        SetupPlayerColors();

        SetupSignals();
    }

    private void GetReferences()
    {
        map = new Array<Map>(GetTree().GetNodesInGroup("Map")).First();
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

    public async void StartGameCountdown()
    {
        int countdown = GameStartCountdownTime;
        while (countdown > 0)
        {
            await
            countdown--;
        }
    }
}
