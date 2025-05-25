namespace FlappyBirdGame.Map.Levels;

using System.Collections.Generic;
using System.Linq;
using Game.Environment.Pipe;
using Game.Map;

public interface IGameLevel : IWorld;

[Id(nameof(GameLevel))]
[GlobalClass]
[Meta(typeof(IAutoNode))]
public partial class GameLevel : World, IGameLevel {
	private readonly Queue<PipeEntity> _pipeQueue = new();
	private readonly Random _random = new();
	[Node] private Timer Timer { get; set; } = null!;

	[Export] public PackedScene PipeScene { get; set; } = null!;
	[Export] public float PipeSpawnInterval { get; set; } = 3.0f;
	[Export] public float GapSize { get; set; } = 100f;

	public override void _Ready() {
		Timer.WaitTime = PipeSpawnInterval;
		Timer.Timeout += SpawnPipeSet;
		EntityTable.Set(nameof(GameLevel), this);
	}

	private void SpawnPipeSet() {
		if (PipeScene == null) {
			GD.PrintErr("PipeScene not assigned!");
			return;
		}

		var screenSize = GetViewportRect().Size;
		var spawnX = screenSize.X + 10;
		const float pipeGapOffset = -190f;
		var minScreenGapY = (GapSize / 2) + 50f;
		var maxScreenGapY = screenSize.Y - (GapSize / 2) - 50f;
		var screenGapY = ((float)_random.NextDouble() * (maxScreenGapY - minScreenGapY)) + minScreenGapY;
		var pipe = PipeScene.Instantiate<PipeEntity>();
		pipe.Position = new Vector2(spawnX, screenGapY - pipeGapOffset);

		_pipeQueue.Enqueue(pipe);
		AddChild(pipe);
	}

	public void Start() => Timer.Start();
	public void Stop() => Timer.Stop();

	public void Reset() {
		Timer.Stop();
		while (_pipeQueue.Count > 0) {
			var pipe = _pipeQueue.Dequeue();
			if (IsInstanceValid(pipe)) {
				pipe.QueueFree();
			}
		}
	}
}
