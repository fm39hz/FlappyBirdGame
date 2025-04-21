namespace FlappyBirdGame.Component.MovementStrategy;

using Chickensoft.Collections;
using Action = Action;

/// <summary>
///     MovementStrategyRepo is a repository for handling movement strategy.
/// </summary>
public interface IMovementStrategyRepo : IRepository {
	/// <summary>
	///     Movement Strategy
	/// </summary>
	public IAutoProp<IMovementStrategy> Strategy { get; }

	/// <summary>
	///     Velocity of the entity
	/// </summary>
	public IAutoProp<Vector2> Velocity { get; }

	/// <summary>
	///     Event triggered when the entity is moved.
	/// </summary>
	/// <param name="velocity"></param>
	public void PressButton(Vector2 velocity);

	/// <summary>
	///     Event determining which direction the entity is moving.
	/// </summary>
	public event Action<Vector2>? IsMoved;

	/// <summary>
	///     Event triggered when the entity stops moving.
	/// </summary>
	public event Action? StopMovement;
}

public interface IMovementStrategy {
	public bool IsMove { get; }

	public Vector2 Move(Vector2 velocity);
}

public class MovementStrategyRepo : BaseRepository, IMovementStrategyRepo {
	#region Actions

	public void PressButton(Vector2 velocity) {
		_velocity.OnNext(_strategy.Value.Move(velocity));
		if (_strategy.Value.IsMove) {
			IsMoved?.Invoke(_velocity.Value);
			return;
		}

		StopMovement?.Invoke();
	}

	#endregion Actions

	#region GC

	public override void Dispose() {
		if (!_isDisposed) {
			_strategy.OnCompleted();
			_strategy.Dispose();
		}

		GC.SuppressFinalize(this);
		base.Dispose();
	}

	#endregion GC

	#region Props

	private readonly AutoProp<IMovementStrategy> _strategy;
	public IAutoProp<IMovementStrategy> Strategy => _strategy;
	private readonly AutoProp<Vector2> _velocity;
	public IAutoProp<Vector2> Velocity => _velocity;
	public event Action<Vector2>? IsMoved;
	public event Action? StopMovement;

	internal MovementStrategyRepo(IMovementStrategy strategy) : base(nameof(MovementStrategyRepo)) {
		_strategy = new AutoProp<IMovementStrategy>(strategy);
		_velocity = new AutoProp<Vector2>(Vector2.Zero);
	}

	#endregion Props
}
