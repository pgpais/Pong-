using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using Pong.Entities;
using Pong.Scripts.Helpers;

namespace Pong.Entities.Managers
{
    public class GameManager : Node2D
    {
        [Export]
        public int GameStartCountdownTime { get; private set; } = 5;
        [Export]
        public float StartTextHoldTime { get; private set; } = 0.2f;
        [Export]
        public NodePath PaddleManagerPath { get; private set; }
        [Export]
        public NodePath ScoreManagerPath { get; private set; }
        [Export]
        public NodePath HUDPath { get; private set; }
        [Export]
        public PackedScene BallScene { get; private set; }

        public List<Player> players = new List<Player>();

        private Map map;
        private ScoreManager scoreManager;
        private HeadsUpDisplay hud;
        private Ball ball;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GetReferences();
            SpawnBall();

            SetupPlayerColors();
            SetupSignals();

            StartGameCountdown(PlayerNumber.One);
        }

        private void GetReferences()
        {
            map = new Array<Map>(GetTree().GetNodesInGroup("map")).First();
            scoreManager = GetNode<ScoreManager>(ScoreManagerPath);
            hud = GetNode<HeadsUpDisplay>(HUDPath);
        }

        private void SetupPlayerColors()
        {
            players = new Array<Player>(GetTree().GetNodesInGroup("player")).ToList();

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
                scoreManager.ConnectGoalSignal(goal);
                goal.Connect(nameof(Goal.GoalScored), this, nameof(OnGoalScored));
            });

            hud.ConnectScoreSignal(scoreManager);
        }

        public async void StartGameCountdown(PlayerNumber playerToLaunchTo)
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
            StartGame(playerToLaunchTo);
        }

        private void StartGame(PlayerNumber playerToLaunchTo)
        {
            LaunchBall(playerToLaunchTo);
        }

        private void SpawnBall()
        {
            ball = BallScene.Instance<Ball>();
            map.CallDeferred("add_child", ball);
            ball.GlobalPosition = map.BallSpawnPosition.GlobalPosition;
            ball.GlobalRotation = map.BallSpawnPosition.GlobalRotation;
        }

        public void LaunchBall(PlayerNumber playerToLaunchTo)
        {
            switch (playerToLaunchTo)
            {
                case PlayerNumber.One:
                    // Launch ball towards player 1
                    ball.LaunchToLeftSide();
                    break;
                case PlayerNumber.Two:
                    // Launch ball towards player 2
                    ball.LaunchToRightSide();
                    break;
                default:
                    break;
            }
        }

        public void DestroyBall()
        {
            ball.QueueFree();
            ball = null;
        }

        private void OnGoalScored(PlayerNumber playerNumber)
        {
            GD.Print($"Goal scored by Player {playerNumber.Next()}");
            DestroyBall();
            SpawnBall();
            StartGameCountdown(playerNumber);
        }
    }

}