namespace FlappyBirdGame.Component.Input;

using Chickensoft.Collections;

/// <summary> SingleInputRepo is a repository for handling single input events. </summary>
public interface ISingleInputRepo : IRepository {
	/// <summary> Allocate whether the button is pressed or not. </summary>
	public IAutoProp<bool> IsPressed { get; }

	/// <summary> The input mapping for the repository. </summary>
	public IAutoProp<EInputMapping> InputMap { get; }

	/// <summary> Presses the button associated with the input event. </summary>
	public void PressButton(InputEvent inputEvent);
}

public class SingleInputRepo : BaseRepository, ISingleInputRepo {
	#region Actions

	public void PressButton(InputEvent inputEvent) {
		_isPressed.OnNext(inputEvent.IsPressed() && InputMapping.IsJustPressed(_inputMap.Value));
#if DEBUG
		if (!_isPressed.Value) {
			return;
		}

		_logger.Print("Button pressed");
#endif
	}

	#endregion Actions

	#region GC

	public override void Dispose() {
		if (!_isDisposed) {
			_inputMap.OnCompleted();
			_inputMap.Dispose();
		}

		GC.SuppressFinalize(this);

		base.Dispose();
	}

	#endregion GC

	#region Props

#if DEBUG
#endif

	private readonly AutoProp<bool> _isPressed;
	public IAutoProp<bool> IsPressed => _isPressed;
	private readonly AutoProp<EInputMapping> _inputMap;
	public IAutoProp<EInputMapping> InputMap => _inputMap;

	internal SingleInputRepo(EInputMapping inputMap) : base(inputMap + nameof(SingleInputRepo)) {
		_inputMap = new AutoProp<EInputMapping>(inputMap);
		_isPressed = new AutoProp<bool>(false);
	}

	internal SingleInputRepo(AutoProp<EInputMapping> inputMap, AutoProp<bool> isPressed) :
		base(inputMap.Value + nameof(SingleInputRepo)) {
		_inputMap = inputMap;
		_isPressed = isPressed;
	}

	#endregion Props
}
