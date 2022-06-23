using System;
using Godot;

[Tool]
public class Paddle : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    public float Speed { get; private set; } = 400;
    [Export]
    public Vector2 Size
    {
        get => size;
        private set { size = value; _resizer?.SetObjectSize(size); }
    }
    private Vector2 size = new Vector2(10, 10);

    [Export]
    public int PlayerNumber { get; private set; } = 1;



    Vector2 inputDirection;

    #region References
    private Polygon2D _shape;
    private ResizeRectanglePolygonToCollider _resizer;
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var _collider = GetNode<CollisionShape2D>("Collider");
        _shape = GetNode<Polygon2D>("Shape");
        _resizer = new ResizeRectanglePolygonToCollider(_collider, _shape);
    }

    public override void _Process(float delta)
    {
        if (Engine.EditorHint)
        {
            EditorProcess(delta);
        }
        else
        {
            GameProcess(delta);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (Engine.EditorHint)
        {
            EditorPhysicsProcess(delta);
        }
        else
        {
            GamePhysicsProcess(delta);
        }
    }

    private void GameProcess(float delta)
    {
        // Called every frame.
        // Update game logic here.
        HandleInput();
    }

    private void HandleInput()
    {
        inputDirection = new Vector2();

        if (Input.IsActionPressed($"move_up-{PlayerNumber}"))
        {
            inputDirection.y -= 1;
        }
        if (Input.IsActionPressed($"move_down-{PlayerNumber}"))
        {
            inputDirection.y += 1;
        }

        inputDirection = inputDirection.Normalized();
    }

    private void GamePhysicsProcess(float delta)
    {
        MoveAndSlide(inputDirection * Speed);
    }

    private void EditorProcess(float delta)
    {


    }

    private void EditorPhysicsProcess(float delta)
    {

    }

    //Godot signal function called OnGameManagerPlayerColorSet
    public void OnGameManagerPlayerColorSet(int playerNumber, Color color)
    {
        if (playerNumber == PlayerNumber)
        {
            _shape.Color = color;
        }
    }

}