using System;
using Godot;
using Pong.Scripts.Helpers;

namespace Pong.Entities
{
    public class Ball : RigidBody2D
    {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";
        [Export]
        public float StartForce { get; private set; } = 500;
        [Export]
        public float ForceIncreaseWithBounce { get; private set; } = 10; //TODO: figure out a better name for this (that can fit in inspector)

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
        }

        public void Launch()
        {
            ApplyImpulse(Vector2.Zero, RandomHelper.RandomInsideUnitCircle() * StartForce);
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        //  public override void _Process(float delta)
        //  {

        //  }

        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
        }

        public override void _IntegrateForces(Physics2DDirectBodyState state)
        {
            base._IntegrateForces(state);

            RotationDegrees = 0;
            if (state.GetContactCount() > 0)
            {
                var vel = LinearVelocity;
                GD.Print($"Current velocity: {vel}, mag:{vel.Length()} | Increasing velocity because of bounce!");
                ApplyImpulse(Vector2.Zero, vel.Normalized() * ForceIncreaseWithBounce);
                // LinearVelocity += LinearVelocity.Normalized() * ForceIncreaseWithBounce;
            }
        }
    }
}