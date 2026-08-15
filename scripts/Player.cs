using Godot;

/// <summary>
/// Player movement class
/// </summary>
public partial class Player : CharacterBody2D {
	private const string IdleAnimation = "idle";
	private const string WalkAnimation = "walk";
	private AnimatedSprite2D _animatedSprite;

	[Export]
	public float speed = 180.0f;

	[Export]
	public float jumpVelocity = -300.0f;

    public override void _Ready() {
        base._Ready();

		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }


	public override void _PhysicsProcess(double delta) {
		Vector2 vel = Velocity;

		// Add the gravity.
		if (!IsOnFloor()) {
			vel += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor()) {
			vel.Y = jumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		float direction = Input.GetAxis("moveLeft", "moveRight");
		if (direction != 0){
			vel.X = direction * speed;

			// animation
			_animatedSprite.Play(WalkAnimation);
			_animatedSprite.FlipH = direction < 0;
		} else {
			// decelerate when not moving
			vel.X = Mathf.MoveToward(Velocity.X, 0, speed);

			// animation
			_animatedSprite.Play(IdleAnimation);
		}

		Velocity = vel;
		MoveAndSlide();
	}
}
