namespace FlappyBirdGame.Map.Levels;

using Chickensoft.LogicBlocks;

public interface IGameStateMachine : IStateMachine;

[Meta]
[LogicBlock(typeof(State), Diagram = true)]
public partial class GameStateMachine : LogicBlock<GameStateMachine.State>, IGameStateMachine {
	public override Transition GetInitialState() => To<State.Wait>();

	public record Input {
		public readonly record struct Start;

		public readonly record struct Stop;

		public readonly record struct Reset;
	}

	public record State : StateLogic<State> {
		public record Wait : State, IGet<Input.Start> {
			public Transition On(in Input.Start input) => To<Play>();
		}

		public record Play : State, IGet<Input.Stop> {
			public Play() {
				using var spawnTimer = new Timer();
				var world = Get<GameLevel>();
				world.AddChild(spawnTimer);
				spawnTimer.Start();
			}

			public Transition On(in Input.Stop input) => To<End>();
		}

		public record End : State, IGet<Input.Reset> {
			public Transition On(in Input.Reset input) => To<Wait>();
		}
	}
}
