namespace FlappyBirdGame.Game.Creature;

using Chickensoft.Log;
using Chickensoft.LogicBlocks;
using Component.Animation;

public partial class BirdStateMachine {
	public override Transition GetInitialState() => To<State.Wait>();

	public abstract record State : StateLogic<State> {
		private readonly Log _logger = new(nameof(BirdStateMachine));

		protected State() {
			this.OnEnter(() => _logger.Print($"On State: {GetType().Name}"));
		}
		/// <summary>
		/// The bird flap in the air, wait for user's first Tap/Input
		/// </summary>
		public sealed record Wait : State, IGet<Input.Flap> {
			public Transition On(in Input.Flap input) => To<Fly.Flap>();
		}

		/// <summary>
		/// The Bird is alive
		/// </summary>
		public abstract record Fly : State, IGet<Input.Collide> {
			/// <summary>
			/// When User don't tap, the bird will fall down
			/// </summary>
			public sealed record Fall : Fly, IGet<Input.Flap> {
				public Transition On(in Input.Flap input) => To<Flap>();
			}

			/// <summary>
			/// When User tap, the bird will flap and fly up
			/// </summary>
			public sealed record Flap : Fly, IGet<Input.Fall> {
				private void OnSetTime() {
					var timer = Get<Timer>();
					timer.Start(0.1);
					timer.Timeout += OnTimeOut;
				}

				public void OnTimeOut() => Input(new Input.Fall());

				public Flap() {
					OnAttach(OnSetTime);
					OnDetach(() => Get<Timer>().Timeout -= OnTimeOut);
				}

				public Transition On(in Input.Fall input) => To<Fall>();
			}

			public Transition On(in Input.Collide input) => To<Dead>();
		}

		/// <summary>
		/// The bird's dead, constantly falling out of screen
		/// </summary>
		public sealed record Dead : State;
	}
}
