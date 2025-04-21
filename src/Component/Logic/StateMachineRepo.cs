namespace FlappyBirdGame.Component.Logic;

using Chickensoft.Collections;
using Action = Action;

public interface IStateMachineRepo : IRepository {
	public IAutoProp<string> CurrentState { get; }
	public IAutoProp<string> PreviousState { get; }

	public event Action? StateChanged;

	public void ChangeState(IState state);
}

public class StateMachineRepo : BaseRepository, IStateMachineRepo {
	private readonly AutoProp<string> _currentState;
	private readonly AutoProp<string> _previousState;

	public StateMachineRepo() : base(nameof(StateMachineRepo)) {
		_currentState = new AutoProp<string>("Idle");
		_previousState = new AutoProp<string>("Idle");
	}

	internal StateMachineRepo(string currentState, string previousState) : base(nameof(StateMachineRepo)) {
		_currentState = new AutoProp<string>(currentState);
		_previousState = new AutoProp<string>(previousState);
	}

	internal StateMachineRepo(AutoProp<string> currentState, AutoProp<string> previousState) : base(
		nameof(StateMachineRepo)) {
		_currentState = currentState;
		_previousState = previousState;
	}

	public IAutoProp<string> CurrentState => _currentState;
	public IAutoProp<string> PreviousState => _previousState;

	public event Action? StateChanged;

	public void ChangeState(IState state) {
		_previousState.OnNext(_currentState.Value);
		_currentState.OnNext(state.GetType().Name);
		StateChanged?.Invoke();
	}

	#region GC

	public override void Dispose() {
		if (!_isDisposed) {
			_currentState.OnCompleted();
			_currentState.Dispose();
			_previousState.OnCompleted();
			_previousState.Dispose();
		}

		GC.SuppressFinalize(this);
		base.Dispose();
	}

	#endregion GC
}
