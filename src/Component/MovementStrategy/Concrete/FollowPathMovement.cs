namespace FlappyBirdGame.Component.MovementStrategy.Concrete;

public class FollowPathMovement : IMovementStrategy {
	public Vector2 Move(Vector2 velocity) => Vector2.Zero;

	public bool IsMove { get; }
}
