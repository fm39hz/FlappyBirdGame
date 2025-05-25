namespace GameBoilerPlate.Component.Movement;

using Chickensoft.Collections;
using Action = System.Action;

/// <summary>
///     MovementRepo is a repository for handling movement events.
/// </summary>
public interface IMovementRepo : IRepository {
	/// <summary>
	///     Determines if the entity is moveable.
	/// </summary>
	public IAutoProp<bool> IsMoveable { get; }

	/// <summary>
	///     The speed of the entity.
	/// </summary>
	public IAutoProp<float> Speed { get; }

	/// <summary>
	///     The acceleration of the entity.
	/// </summary>
	public IAutoProp<float> Acceleration { get; }

	/// <summary>
	///     The friction of the entity.
	/// </summary>
	public IAutoProp<float> Friction { get; }

	/// <summary>
	///     Event triggered when the entity starts moving.
	/// </summary>
	public event Action? StartMoving;

	/// <summary>
	///     Event triggered when the entity stops moving.
	/// </summary>
	public event Action? StopMoving;

	/// <summary>
	///     Move the entity.
	/// </summary>
	/// <param name="isMoving"></param>
	public void Move(bool isMoving);
}

public class MovementRepo : BaseRepository, IMovementRepo {
	#region Actions

	public void Move(bool isMoving) {
		if (!_isMoveable.Value) {
			return;
		}

		if (isMoving) {
			StartMoving?.Invoke();
			return;
		}

		StopMoving?.Invoke();
	}

	#endregion Actions

	#region GC

	public override void Dispose() {
		if (!_isDisposed) {
			_isMoveable.OnCompleted();
			_isMoveable.Dispose();
			_speed.OnCompleted();
			_speed.Dispose();
			_acceleration.OnCompleted();
			_acceleration.Dispose();
			_friction.OnCompleted();
			_friction.Dispose();
		}

		GC.SuppressFinalize(this);
		base.Dispose();
	}

	#endregion GC

	#region Props

	private readonly AutoProp<bool> _isMoveable;
	public IAutoProp<bool> IsMoveable => _isMoveable;
	private readonly AutoProp<float> _speed;
	public IAutoProp<float> Speed => _speed;
	private readonly AutoProp<float> _acceleration;
	public IAutoProp<float> Acceleration => _acceleration;
	private readonly AutoProp<float> _friction;
	public IAutoProp<float> Friction => _friction;

	public event Action? StartMoving;
	public event Action? StopMoving;

	public MovementRepo() : base(nameof(MovementRepo)) {
		_isMoveable = new AutoProp<bool>(true);
		_speed = new AutoProp<float>(100);
		_acceleration = new AutoProp<float>(0);
		_friction = new AutoProp<float>(0);
	}

	#endregion Props
}
