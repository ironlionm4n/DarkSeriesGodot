using System.Diagnostics;
using Godot;

namespace DarkSeriesGodot.Scripts;

public partial class Player : CharacterBody2D
{
	[Export] private const float Speed = 300.0f;
	[Export] private float JumpVelocity = -150.0f;
	[Export] private float runSpeed = 5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		if (Input.IsActionPressed("MoveLeft"))
		{
			GD.Print("Hello left");
			velocity.X = Mathf.MoveToward(Velocity.X, -runSpeed, Speed);
		} else if (Input.IsActionPressed("MoveRight"))
		{
			GD.Print("Hello right");
			velocity.X = Mathf.MoveToward(Velocity.X, runSpeed, Speed);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}