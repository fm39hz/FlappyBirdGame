namespace FlappyBirdGame.Utils.Extension;

using System.Collections.Generic;
using System.Linq;

public static class NodeExtension {
	/// <summary>
	///     Get the first child node of type T
	/// </summary>
	/// <typeparam name="T">type of Children</typeparam>
	/// <returns>The first child node of type T</returns>
	public static T GetFirstChild<T>(this Node target) where T : Node {
		T targetChild = null!;
		for (var i = 0; i < target.GetChildCount(); i++) {
			if (target.GetChildOrNull<T>(i) == null) {
				continue;
			}

			targetChild = target.GetChild<T>(i);
			break;
		}

		return targetChild;
	}

	/// <summary>
	///     Get children of type T
	/// </summary>
	/// <typeparam name="T">type of children</typeparam>
	/// <returns>All the child node of type T</returns>
	public static List<T> GetChildren<T>(this Node target) where T : Node => [.. target.GetChildren().OfType<T>()];
}
