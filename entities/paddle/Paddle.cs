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
        private set { size = value; SetObjectSize(size); }
    }


    private Vector2 size = new Vector2(10, 10);
    Vector2 inputDirection;

    #region References
    private CollisionShape2D _collider;
    private Polygon2D _shape;
    #endregion

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _collider = GetNode<CollisionShape2D>("Collider");
        _shape = GetNode<Polygon2D>("Shape");
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

        if (Input.IsActionPressed("move_up"))
        {
            inputDirection.y -= 1;
        }
        if (Input.IsActionPressed("move_down"))
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

    private void SetObjectSize(Vector2 size)
    {
        if (_collider != null)
        {
            var colliderShape = _collider.Shape as RectangleShape2D;
            colliderShape.Extents = new Vector2(size.x / 2, size.y / 2);
        }

        if (_shape != null)
        {
            var newPolygon = new Vector2[]
            {
            new Vector2(-size.x / 2, -size.y / 2),
            new Vector2(size.x / 2, -size.y / 2),
            new Vector2(size.x / 2, size.y / 2),
            new Vector2(-size.x / 2, size.y / 2)
            };


            _shape.Polygon = newPolygon;
        }
    }

    private void EditorPhysicsProcess(float delta)
    {

    }
}