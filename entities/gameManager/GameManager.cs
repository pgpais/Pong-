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
    [Export]
    public float StartTextHoldTime { get; private set; } = 0.2f;
    [Export]
    public PackedScene BallScene { get; private set; }



    public List<Player> players = new List<Player>();

    private Map map;
    private Ball ball;

    public GameManager()
    {

    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetReferences();
        SpawnBall();

        SetupPlayerColors();
        SetupSignals();

        StartGameCountdown();
    }

    private void GetReferences()
    {

        map = new Array<Map>(GetTree().GetNodesInGroup("map")).First();
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

    public async void StartGameCountdown()
    {
        int countdown = GameStartCountdownTime;
        while (countdown > 0)
        {
            GD.Print($"{countdown}");
            await ToSignal(GetTree().CreateTimer(1), "timeout"); // wait one second
            countdown--;
        }
        GD.Print("Start!");
        await ToSignal(GetTree().CreateTimer(StartTextHoldTime), "timeout"); // show start for a bit
        GD.Print("Game started");
        StartGame();
    }

    private void StartGame()
    {
        LaunchBall();
    }

    private void SpawnBall()
    {
        ball = BallScene.Instance<Ball>();
        ball.Name = "ball";
        map.AddChild(ball);
        ball.GlobalPosition = map.BallSpawnPosition.GlobalPosition;
        ball.GlobalRotation = map.BallSpawnPosition.GlobalRotation;
    }

    public void LaunchBall()
    {
        ball.Launch();
    }

    public void DestroyBall()
    {
        ball.QueueFree();
        ball = null;
    }

    private void OnGoalScored(PlayerNumber playerNumber)
    {
        GD.Print($"Goal scored by Player {playerNumber.Next()}");

    }
}
