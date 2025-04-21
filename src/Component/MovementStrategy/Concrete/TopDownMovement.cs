namespace FlappyBirdGame.Component.MovementStrategy.Concrete;

using Input;

public class TopDownMovement : IMovementStrategy {
	public bool IsMove {
		get {
			var isUpPressed = InputMapping.IsPressed(EInputMapping.Up);
			var isDownPressed = InputMapping.IsPressed(EInputMapping.Down);
			var isLeftPressed = InputMapping.IsPressed(EInputMapping.Left);
			var isRightPressed = InputMapping.IsPressed(EInputMapping.Right);
			var isHorizontalLock = isLeftPressed && isRightPressed;
			var isVerticalLock = isUpPressed && isDownPressed;

			return isHorizontalLock && !isVerticalLock
				? isUpPressed || isDownPressed
				: isVerticalLock && !isHorizontalLock
					? isLeftPressed || isRightPressed
					: (isUpPressed || isDownPressed || isLeftPressed || isRightPressed) &&
					!(isHorizontalLock || isVerticalLock);
		}
	}

	public Vector2 Move(Vector2 velocity) {
		if (IsMove) {
			velocity = InputMapping.GetVector();
		}

		return velocity;
	}
}
