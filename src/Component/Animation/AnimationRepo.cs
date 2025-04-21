namespace FlappyBirdGame.Component.Animation;

using Chickensoft.Collections;
using Utils.Converter;

public interface IAnimationRepo : IRepository {
	public IAutoProp<bool> Has8Direction { get; }
	public IAutoProp<string> AnimatedStateName { get; }
	public IAutoProp<EDirection> Direction { get; }
	public event Action<string>? AnimationChanged;

	public void ChangeState(string state);

	public void ChangeDirection(EDirection direction);
}

public class AnimationRepo : BaseRepository, IAnimationRepo {
	#region GC

	public override void Dispose() {
		if (!_isDisposed) {
			_has8Direction.OnCompleted();
			_has8Direction.Dispose();
		}

		GC.SuppressFinalize(this);
		base.Dispose();
	}

	#endregion GC

	#region Props

	private readonly AutoProp<bool> _has8Direction;
	public IAutoProp<bool> Has8Direction => _has8Direction;
	private readonly AutoProp<string> _animatedStateName;
	public IAutoProp<string> AnimatedStateName => _animatedStateName;
	private readonly AutoProp<EDirection> _direction;
	public IAutoProp<EDirection> Direction => _direction;

	public AnimationRepo() : base(nameof(AnimationRepo)) {
		_has8Direction = new AutoProp<bool>(false);
		_animatedStateName = new AutoProp<string>("Idle");
		_direction = new AutoProp<EDirection>(EDirection.South);
	}

	internal AnimationRepo(AutoProp<bool> has8Direction, AutoProp<string> animatedStateName,
		AutoProp<EDirection> direction) : base(nameof(AnimationRepo)) {
		_has8Direction = has8Direction;
		_animatedStateName = animatedStateName;
		_direction = direction;
	}

	public event Action<string>? AnimationChanged;

	#endregion Props

	#region Actions

	public void ChangeState(string state) {
		_animatedStateName.OnNext(state);
		AnimationChanged?.Invoke($"{_animatedStateName.Value}_{_direction.Value.Convert()}");
	}

	public void ChangeDirection(EDirection direction) {
		_direction.OnNext(direction);
		AnimationChanged?.Invoke($"{_animatedStateName.Value}_{_direction.Value.Convert()}");
	}

	#endregion Actions
}
