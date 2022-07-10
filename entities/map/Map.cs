using System;
using Godot;

public class Map : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    public NodePath BallSpawnPath { get; private set; }

    public Position2D BallSpawnPosition { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        BallSpawnPosition = GetNode<Position2D>(BallSpawnPath);
    }
}
