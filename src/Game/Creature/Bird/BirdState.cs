namespace FlappyBirdGame.Game.Creature;

using Chickensoft.Log;
using Chickensoft.LogicBlocks;
using Component.Animation;
using Component.Input;
using FlappyBirdGame.Map.Levels;
using JetBrains.Annotations;

public partial class BirdStateMachine {
	private void Flap(bool isPressed) {
		if (isPressed) {
			Input(new Input.Fall());
		}

		Input(new Input.Flap());
	}

	public override Transition GetInitialState() {
		using (var actionInput = Get<IActionInputRepo>()) {
			actionInput.ActionButton.IsPressed.Changed += Flap;
		}

		return To<State.Wait>();
	}

	public abstract record State : StateLogic<State> {
		private readonly Log _logger = new(nameof(BirdStateMachine));

		private State(string animationName) {
			this.OnEnter(() => {
				_logger.Print($"On State: {GetType().Name}");
				Get<IAnimationRepo>().ChangeAnimation(animationName);
				_logger.Print($"Animated: {animationName}");
			});
		}

		/// <summary>
		///     The bird flap in the air, wait for user's first Tap/Input
		/// </summary>
		public sealed record Wait() : State("Flap"), IGet<Input.Flap> {
			public Transition On(in Input.Flap input) {
				var game = Get<GameLevel>();
				game.Start();
				return To<Alive.Flap>();
			}
		}

		/// <summary>
		///     The Bird is alive
		/// </summary>
		public abstract record Alive([UsedImplicitly] string AnimationName)
			: State(AnimationName), IGet<Input.Collide> {
			public Transition On(in Input.Collide input) {
				var game = Get<GameLevel>();
				game.Stop();
				return To<Dead>();
			}

			/// <summary>
			///     When Player is not tap, the bird will fall down
			/// </summary>
			public sealed record Fall : Alive, IGet<Input.Flap>, IState {
				public Fall() : base("RESET") {
					this.OnEnter(() => {
						Output(new Output.RotationChange(25));
					});
				}

				public void Run() => Output(new Output.FallDown());

				public Transition On(in Input.Flap input) => To<Flap>();
			}

			/// <summary>
			///     When Player tap, the bird will flap and fly up
			/// </summary>
			public sealed record Flap : Alive, IGet<Input.Fall>, IState {
				public Flap() : base("Flap") {
					this.OnEnter(() => {
						Output(new Output.RotationChange(-25));
					});
					OnAttach(OnSetTime);
					OnDetach(() => Get<Timer>().Timeout -= OnTimeOut);
				}

				public void Run() => Output(new Output.FlyUp());

				public Transition On(in Input.Fall input) => To<Fall>();

				private void OnSetTime() {
					var timer = Get<Timer>();
					timer.Start(0.2);
					timer.Timeout += OnTimeOut;
				}

				private void OnTimeOut() => Input(new Input.Fall());
			}
		}

		/// <summary>
		///     The bird's dead, constantly falling out of screen
		/// </summary>
		public sealed record Dead() : State("RESET"), IGet<Input.Flap> {
			public Transition On(in Input.Flap input) {
				Output(new Output.Reset());
				var game = Get<GameLevel>();
				game.Reset();
				return To<Wait>();
			}
		}
	}
}
