using System;
using Godot;

[Tool]
public class Goal : Area2D
{
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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var _collider = GetNode<CollisionShape2D>("Collider");
        var _shape = GetNode<Polygon2D>("Shape");
        _resizer = new ResizeRectanglePolygonToCollider(_collider, _shape);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //
    //  }

    public void OnGoalBodyEntered(Node body)
    {
        if (body is Ball)
        {
            var ball = body as Ball;
            GD.Print($"Ball {ball.Name} scored!");
        }
    }
}
