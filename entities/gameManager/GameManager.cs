using System;
using System.Collections.Generic;
using System.Linq;
using Cheese.Players;
using Godot;

public class GameManager : Node2D
{
    [Signal]
    public delegate void PlayerColorSet(PlayerNumber playerNumber, Color color);

    public List<Player> players = new List<Player>();

    public GameManager()
    {
        AddUserSignal(nameof(PlayerColorSet));
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        players = new Godot.Collections.Array<Player>(GetTree().GetNodesInGroup("player")).ToList();

        // Set color of player one
        Color color = players.First((p) => p.PlayerNumber == PlayerNumber.One).PlayerColor;
        EmitSignal(nameof(PlayerColorSet), PlayerNumber.One, color);

        // Set color of player two
        color = players.First((p) => p.PlayerNumber == PlayerNumber.Two).PlayerColor;
        EmitSignal(nameof(PlayerColorSet), PlayerNumber.Two, color);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}
