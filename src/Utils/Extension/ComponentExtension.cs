namespace FlappyBirdGame.Utils.Extension;

using Chickensoft.Collections;

public static class ComponentExtensions {
	public static T GetEntity<T>(this IComponent repo) where T : class, IEntity {
		if (repo is not IAutoNode dependent) {
			return null!;
		}

		var parent = repo.GetParent();
		return dependent.DependOn<EntityTable>().Get<T>(parent.Name)!;
	}
}
