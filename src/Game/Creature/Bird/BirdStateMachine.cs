namespace FlappyBirdGame.Game.Creature;

using Chickensoft.LogicBlocks;

public interface IBirdStateMachine : IStateMachine;

/// <summary>
///     State machine for the Bird Entity
/// </summary>
[Meta]
[LogicBlock(typeof(State), Diagram = true)]
public sealed partial class BirdStateMachine : LogicBlock<BirdStateMachine.State>, IBirdStateMachine {
	public static class Input {
		public readonly record struct Flap;

		public readonly record struct Fall;

		public readonly record struct Collide;
	}

	public static class Output {
		public readonly record struct Reset;
		public readonly record struct RotationChange(int Degree);

		public readonly record struct FlyUp;

		public readonly record struct FallDown;
	}
}
