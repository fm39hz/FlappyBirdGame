namespace FlappyBirdGame.Utils.Manager;

using System.Collections.Generic;
using System.Linq;
using Chickensoft.Log;
using JetBrains.Annotations;
using NodeExtra;

public class SceneManager : Singleton<SceneManager> {
#if DEBUG
	private readonly Log _log = new(nameof(SceneManager));
#endif
	private Node CurrentScene { get; set; } = new();
	private Node GameContainer { get; set; } = null!;

	[UsedImplicitly]
	public static void LoadContainer(string path, List<Node> carryOverEntities = null!) =>
		LoadContainer(GD.Load<PackedScene>(path), carryOverEntities);

	public static void LoadContainer(PackedScene nextScene, List<Node> carryOverEntities = null!) =>
		LoadContainer(nextScene.Instantiate(), carryOverEntities);

	private static void LoadContainer(Node nextScene, List<Node> carryOverEntities = null!) {
		_instance.GameContainer ??= nextScene;
		if (carryOverEntities != null) {
			foreach (var entity in carryOverEntities) {
				nextScene.AddChild(entity);
			}
		}

		_instance._tree.Root.AddChild(nextScene);
#if DEBUG
		_instance._log.Print($"Loaded container: {nextScene.Name}");
#endif
	}

	[UsedImplicitly]
	public static void GotoScene(string path, List<Node2D> carryOverEntities = null!) =>
		GotoScene(GD.Load<PackedScene>(path), carryOverEntities);

	public static void GotoScene(PackedScene nextScene, List<Node2D> carryOverEntities = null!) =>
		GotoScene(nextScene.Instantiate(), carryOverEntities);

	private static void GotoScene(Node level, List<Node2D> carryOverEntities = null!) {
		if (_instance.CurrentScene == level) {
#if DEBUG
			_instance._log.Print($"Already in level: {_instance.CurrentScene.Name}");
#endif
			return;
		}

		if (carryOverEntities != null) {
			foreach (var entity in carryOverEntities) {
				if (level.GetChildren().FirstOrDefault(child =>
							child is SpawningPoint spawnPoint && spawnPoint.TargetEntity == entity.GetType()
						) is SpawningPoint spawningPoint) {
					entity.Position = spawningPoint.Position;
					spawningPoint.QueueFree();
				}

				level.AddChild(entity);
			}
		}

		_instance.GameContainer.AddChild(level);
		_instance.CurrentScene.QueueFree();
		_instance.CurrentScene = level;
#if DEBUG
		_instance._log.Print($"Switched to level: {level.Name}");
#endif
	}
}
