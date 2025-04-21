namespace FlappyBirdGame.Component.MovementStrategy.Concrete;

using Input;

public class SideScrollingMovement : IMovementStrategy {
	public bool IsMove {
		get {
			var isLeftPressed = InputMapping.IsPressed(EInputMapping.Left);
			var isRightPressed = InputMapping.IsPressed(EInputMapping.Right);
			var isHorizontalLock = isLeftPressed && isRightPressed;

			return (isLeftPressed || isRightPressed) && !isHorizontalLock;
		}
	}

	public Vector2 Move(Vector2 velocity) {
		if (IsMove) {
			velocity.X = InputMapping.GetHorizontalAxis();
		}

		return velocity;
	}
}
