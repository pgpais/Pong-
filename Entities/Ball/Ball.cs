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

        public void Launch(Vector2 direction)
        {
            ApplyImpulse(Vector2.Zero, direction * StartForce);
        }

        public void LaunchToLeftSide()
        {
            var direction = RandomHelper.RandomVector2(225, 135);
            Launch(direction);
        }

        public void LaunchToRightSide()
        {
            var direction = RandomHelper.RandomVector2(45, -45);
            Launch(direction);
        }

        public void RandomLaunch()
        {
            Launch(RandomHelper.RandomInsideUnitCircle());
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
                //TODO: gotta be the one coding the physics
                if (state.GetContactColliderObject(0) is Paddle)
                {
                    Paddle paddle = state.GetContactColliderObject(0) as Paddle;
                    HandlePaddleCollision(paddle, state.GetContactColliderPosition(0));
                }
                var vel = LinearVelocity;
                GD.Print($"Current velocity: {vel}, mag:{vel.Length()} | Increasing velocity because of bounce!");
                ApplyImpulse(Vector2.Zero, vel.Normalized() * ForceIncreaseWithBounce);
                // LinearVelocity += LinearVelocity.Normalized() * ForceIncreaseWithBounce;
            }
        }

        private void HandlePaddleCollision(Paddle paddle, Vector2 collisionPosition)
        {
            bool isBallHigherThanPaddle = collisionPosition.y > paddle.GlobalPosition.y;
            Vector2 ballLaunchDirection = paddle.Transform.x;
            float velocity = LinearVelocity.Length();
            StopMovement();
            if (isBallHigherThanPaddle)
            {
                ballLaunchDirection = ballLaunchDirection.Rotated(Mathf.Pi / 4);
            }
            else
            {
                ballLaunchDirection = ballLaunchDirection.Rotated(-Mathf.Pi / 4);
            }
            LinearVelocity = ballLaunchDirection * velocity;
        }

        private void StopMovement()
        {
            LinearVelocity = Vector2.Zero;
        }
    }
}