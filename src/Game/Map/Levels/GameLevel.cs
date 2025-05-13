namespace FlappyBirdGame.Map.Levels;

using Game.Environment.Pipe;
using Game.Map;

[Id(nameof(GameLevel))]
[GlobalClass]
[Meta(typeof(IAutoNode))]
public partial class GameLevel : World {
	private Timer _spawnTimer = null!;
	private readonly Random _random = new();

	[Export] public PackedScene PipeScene { get; set; } = null!;
	[Export] public float PipeSpawnInterval { get; set; } = 3.0f;
	[Export] public float PipeSpeed { get; set; } = 100f;
	[Export] public float GapSize { get; set; } = 100f;

	public override void _Ready() {
		base._Ready();

		_spawnTimer = new Timer();
		_spawnTimer.WaitTime = PipeSpawnInterval;
		_spawnTimer.Timeout += SpawnPipeSet;
		AddChild(_spawnTimer);
		_spawnTimer.Start();
	}

	private void SpawnPipeSet() {
		if (PipeScene == null) {
			GD.PrintErr("PipeScene not assigned!");
			return;
		}

		var screenSize = GetViewportRect().Size;
		var spawnX = screenSize.X + 50;
		const float pipeGapOffset = -190f;
		var minScreenGapY = (GapSize / 2) + 50f;
		var maxScreenGapY = screenSize.Y - (GapSize / 2) - 50f;
		var screenGapY = ((float)_random.NextDouble() * (maxScreenGapY - minScreenGapY)) + minScreenGapY;
		var pipe = PipeScene.Instantiate<PipeEntity>();
		pipe.Position = new Vector2(spawnX, screenGapY - pipeGapOffset);

		AddChild(pipe);
		MovePipe(pipe);
	}

	private void MovePipe(Node2D pipe) {
		var mover = new PipeMover(pipe, PipeSpeed);
		pipe.AddChild(mover);
	}

	private void StartLevel() => _spawnTimer.Start();

	private void StopLevel() => _spawnTimer.Stop();

	internal partial class PipeMover(Node2D pipe, float speed) : Node {
		public override void _Ready() => SetPhysicsProcess(true);

		public override void _PhysicsProcess(double delta) {
			pipe.Position += Vector2.Left * speed * (float)delta;

			if (pipe.Position.X < -100) {
				pipe.QueueFree();
			}
		}
	}
}
