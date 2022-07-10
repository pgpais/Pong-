using System;
using System.Collections.Generic;
using Cheese.Players;
using Godot;

[Tool]
public class Goal : Area2D
{
    public static List<Goal> Goals = new List<Goal>();

    [Signal]
    public delegate void GoalScored(PlayerNumber goalAffected);

    [Export]
    public PlayerNumber PlayerNumber { get; private set; }

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    public Vector2 Size
    {
        get => size;
        private set { size = value; _resizer?.SetObjectSize(size); }
    }


    private Vector2 size = new Vector2(10, 10);

    #region References
    ResizeRectanglePolygonToCollider _resizer;
    #endregion

    public override void _EnterTree()
    {
        base._EnterTree();
        Goals.Add(this);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnBodyEntered));

        var _collider = GetNode<CollisionShape2D>("Collider");
        var _shape = GetNode<Polygon2D>("Shape");
        _resizer = new ResizeRectanglePolygonToCollider(_collider, _shape);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Goals.Remove(this);
    }

    public void OnBodyEntered(Node body)
    {
        if (body is Ball)
        {
            var ball = body as Ball;
            GD.Print($"Ball {ball.Name} scored!");
            EmitSignal(nameof(GoalScored), PlayerNumber);
        }
    }
}
