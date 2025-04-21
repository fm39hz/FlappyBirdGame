namespace FlappyBirdGame.Component.MovementStrategy;

using Concrete;
using Input;

public static class MovementStrategyFactory {
	public static IMovementStrategy Create(EInputType inputType) =>
		inputType switch {
			EInputType.User => new TopDownMovement(),
			EInputType.PathFollow => new FollowPathMovement(),
			_ => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null)
		};
}
