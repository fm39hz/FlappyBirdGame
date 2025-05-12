namespace FlappyBirdGame.Utils.Creation;

using System.Collections.Generic;
using System.Linq;
using Extension;

[GlobalClass]
public partial class SpawningPoint : Area2D {
	private const int MAX_ATTEMPTS = 10;

	private readonly Queue<Node2D> _entities = new();
	[Export] public PackedScene TargetEntity { get; set; } = null!;
	[Export] public Vector2 SpawnInterval { get; set; }
	[Export] public int MaxSpawnCount { get; set; }
	private bool Initialized => _entities.Count <= 0;

	public override void _Ready() {
		for (var i = 0; i <= MaxSpawnCount; i++) {
			_entities.Enqueue(TargetEntity.Instantiate<Node2D>());
		}
	}

	public override void _PhysicsProcess(double delta) {
		if (Initialized) {
			return;
		}

		Spawn();
	}

	private void Spawn() {
		while (!Initialized && _entities.Dequeue() is { } entity) {
			var parent = GetParent();
			var children = parent.GetChildren<Node2D>();
			var positions = children.Select(static child => child.Position).ToList();

			Vector2 spawnPosition;
			bool validPositionFound;
			var attempts = 0;
			do {
				spawnPosition = CorrectPosition(GetRandomPosition());
				validPositionFound = true;
				foreach (var pos in positions) {
					if (spawnPosition.DistanceTo(pos) < SpawnInterval.Length()) {
						validPositionFound = false;
						break;
					}
				}

				attempts++;
			} while (!validPositionFound && attempts < MAX_ATTEMPTS);

			if (validPositionFound) {
				GD.Print(spawnPosition);
				entity.Position += spawnPosition;
				parent.AddChild(entity);
			}
		}
	}

	private Vector2 CorrectPosition(Vector2 targetPosition) =>
		new(
			targetPosition.X + Position.X,
			targetPosition.Y - Position.Y
			);

	private Vector2 GetRandomPosition() {
		var rng = new Random();
		var size = this.GetFirstChild<CollisionShape2D>().Shape.GetRect().Size;

		return new Vector2(
			(float)(rng.NextDouble() * size.X),
			(float)(rng.NextDouble() * size.Y)
			);
	}
}
