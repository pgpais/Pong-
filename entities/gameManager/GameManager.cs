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
        players = new Godot.Collections.Array<Player>(GetTree().GetNodesInGroup("player")).ToList();

        // Set color of player one
        Color color1 = players.First((p) => p.PlayerNumber == PlayerNumber.One).PlayerColor;
        // Set color of player two
        Color color2 = players.First((p) => p.PlayerNumber == PlayerNumber.Two).PlayerColor;

        if (PaddleManagerPath != null && !PaddleManagerPath.IsEmpty())
        {
            var paddleManager = GetNode<PaddleManager>(PaddleManagerPath);
            paddleManager.SetPaddleColor(PlayerNumber.One, color1);
            paddleManager.SetPaddleColor(PlayerNumber.Two, color2);
        }
        else
        {
            GD.PushWarning("PaddleManagerPath is empty, will not set color");
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}
