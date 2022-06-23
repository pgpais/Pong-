using System;
using Godot;
using Godot.Collections;

public class GameManager : Node
{
    [Signal]
    public delegate void PlayerColorSet(int playerNumber, Color color);
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    public Dictionary<int, Color> PlayerColors { get; private set; } = new Dictionary<int, Color>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        EmitSignal(nameof(PlayerColorSet), 1, PlayerColors[0]);
        EmitSignal(nameof(PlayerColorSet), 2, PlayerColors[1]);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }
}
