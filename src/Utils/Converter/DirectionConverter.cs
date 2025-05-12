namespace FlappyBirdGame.Utils.Converter;

using System.Linq;
using Godot.Collections;

public enum EDirection {
	South,
	North,
	East,
	West,
	SouthEast,
	SouthWest,
	NorthEast,
	NorthWest
}

public static class DirectionConverter {
	public static EDirection GetDirection(Vector2 input, bool isFourDirection) =>
		(from _direction in isFourDirection? CrossDirection : RoundDirection
		where MathF.Round(input.AngleTo(_direction.Key)) == 0
		select _direction.Value).FirstOrDefault(EDirection.South);

	public static string Convert(this EDirection direction) =>
		direction switch {
			EDirection.South => "S",
			EDirection.North => "N",
			EDirection.East => "E",
			EDirection.West => "W",
			EDirection.SouthEast => "SE",
			EDirection.SouthWest => "SW",
			EDirection.NorthEast => "NE",
			EDirection.NorthWest => "NW",
			_ => throw new ArgumentException("Argument out of bound")
		};

	#region Store

	private static Dictionary<Vector2, EDirection> RoundDirection =>
		new() {
			{ Vector2.Down, EDirection.South },
			{ Vector2.Right, EDirection.East },
			{ Vector2.Up, EDirection.North },
			{ Vector2.Left, EDirection.West },
			{ Vector2.Down + Vector2.Right, EDirection.SouthEast },
			{ Vector2.Right + Vector2.Up, EDirection.NorthEast },
			{ Vector2.Up + Vector2.Left, EDirection.NorthWest },
			{ Vector2.Left + Vector2.Down, EDirection.SouthWest }
		};

	private static Dictionary<Vector2, EDirection> CrossDirection =>
		new() {
			{ Vector2.Down, EDirection.South },
			{ Vector2.Right, EDirection.East },
			{ Vector2.Up, EDirection.North },
			{ Vector2.Left, EDirection.West },
			{ Vector2.Down + Vector2.Right, EDirection.East },
			{ Vector2.Right + Vector2.Up, EDirection.East },
			{ Vector2.Up + Vector2.Left, EDirection.West },
			{ Vector2.Left + Vector2.Down, EDirection.West }
		};

	#endregion Store
}
