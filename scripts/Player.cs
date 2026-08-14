using Godot;

/// <summary>
/// Player movement class
/// </summary>
public partial class Player : CharacterBody2D {
	public const float Speed = 180.0f;
	public const float JumpVelocity = -300.0f;

	public override void _PhysicsProcess(double delta) {
		Vector2 vel = Velocity;

		// Add the gravity.
		if (!IsOnFloor()) {
			vel += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor()) {
			vel.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		float direction = Input.GetAxis("moveLeft", "moveRight");
		if (direction != 0){
			vel.X = direction * Speed;
		} else {
			// decelerate when not moving
			vel.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = vel;
		MoveAndSlide();
	}
}
