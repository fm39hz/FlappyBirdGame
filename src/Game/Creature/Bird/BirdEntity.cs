namespace FlappyBirdGame.Game.Creature;

using Chickensoft.Log;
using Component.ScoreCounter;
using Entity.Behaviour;
using Entity.Creature;
using FlappyBirdGame.Map.Levels;
using Utils.Extension;

public interface IBirdEntity : IStateMachineEntity<BirdStateMachine>, IAnimalEntity {
	public void Collide();
	public void IncreaseScore();
}

[Id(nameof(BirdEntity))]
[Meta(typeof(IAutoNode))]
public partial class BirdEntity : AnimalEntity, IBirdEntity {
	private readonly Log _logger = new(nameof(BirdEntity));
	[Node] private Timer Timer { get; set; } = null!;
	[Node] private Sprite2D Sprite2D { get; set; } = null!;
	[Node] private ScoreCounterComponent ScoreCounterComponent { get; set; } = null!;
	private bool IsCollided { get; set; }

	[Export] public float Gravity { get; set; } = 5f;
	[Export] public float JumpForce { get; set; } = 5f;
	public BirdStateMachine StateMachine { get; private set; } = null!;

	private Vector2 StartPosition { get; set; } = Vector2.Zero;

	public void IncreaseScore() {
		ScoreCounterComponent.Repo.Increase();
		_logger.Print($"Score: {ScoreCounterComponent.Repo.Score.Value}");
	}

	public void Collide() {
		_logger.Print("Game Ended");
		IsCollided = true;
		StateMachine.Input(new BirdStateMachine.Input.Collide());
	}

	public void OnProvided() {
		this.ResolveComponent();
		StateMachine = this.RegisterToStateMachine(new BirdStateMachine());
		StateMachine.Set(EntityTable.Get<GameLevel>(nameof(GameLevel))!);
		StateMachine.Set(Timer);
		StartPosition = Position;
		var binding = StateMachine.Bind();
		binding.Handle((in BirdStateMachine.Output.RotationChange output) => {
			Sprite2D.RotationDegrees += output.Degree;
		}).Handle((in BirdStateMachine.Output.FlyUp _) => {
			Velocity += (Vector2.Up * JumpForce) + (Vector2.Down * Gravity);
		}).Handle((in BirdStateMachine.Output.FallDown _) => {
			Velocity += Vector2.Down * Gravity;
		}).Handle((in BirdStateMachine.Output.Reset _) => {
			Velocity = Vector2.Zero;
			Sprite2D.RotationDegrees = 0;
			Position = StartPosition;
			IsCollided = false;
		});
		StateMachine.Start();
	}

	public override void _PhysicsProcess(double delta) {
		if (IsCollided) {
			return;
		}

		if (StateMachine.Value is IState state) {
			state.Run();
		}
		MoveAndSlide();
	}
}
