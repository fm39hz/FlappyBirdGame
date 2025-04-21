namespace FlappyBirdGame.Component.Input;

using Input = Godot.Input;

public enum EInputType {
	PathFollow,
	User
}

public enum EInputView {
	TopDown,
	SideScrolling
}

public enum EInputMapping {
	Left = 0,
	Right = 1,
	Up = 2,
	Down = 3,
	Exit = 4,
	Action = 5,
	Jump = 6
}

public class InputMapping {
	private static readonly string[] _keyMap = [
		"ui_left",
		"ui_right",
		"ui_up",
		"ui_down",
		"ui_cancel",
		"ui_accept",
		"ui_space"
	];

	private static string GetMappingValue(EInputMapping input) =>
		input switch {
			EInputMapping.Left => _keyMap[0],
			EInputMapping.Right => _keyMap[1],
			EInputMapping.Up => _keyMap[2],
			EInputMapping.Down => _keyMap[3],
			EInputMapping.Exit => _keyMap[4],
			EInputMapping.Action => _keyMap[5],
			EInputMapping.Jump => _keyMap[6],
			_ => throw new NotImplementedException()
		};

	public static bool IsPressed(EInputMapping eInput) => Input.IsActionPressed(GetMappingValue(eInput));

	public static bool IsJustPressed(EInputMapping eInput) => Input.IsActionJustPressed(GetMappingValue(eInput));

	public static Vector2 GetVector() =>
		Input.GetVector(
			GetMappingValue(EInputMapping.Left),
			GetMappingValue(EInputMapping.Right),
			GetMappingValue(EInputMapping.Up),
			GetMappingValue(EInputMapping.Down));

	public static float GetHorizontalAxis() =>
		Input.GetAxis(
			GetMappingValue(EInputMapping.Left),
			GetMappingValue(EInputMapping.Right));

	public static float GetVerticalAxis() =>
		Input.GetAxis(
			GetMappingValue(EInputMapping.Up),
			GetMappingValue(EInputMapping.Down));
}
