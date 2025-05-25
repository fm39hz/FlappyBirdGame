namespace FlappyBirdGame.Game.Environment.Pipe;

using Entity.Environment;
using Utils.Extension;

public interface IPipeEntity : IEnvironmentEntity;

[Id(nameof(PipeEntity))]
[Meta(typeof(IAutoNode))]
public partial class PipeEntity : EnvironmentEntity, IPipeEntity {
	[Node] public CollisionShape2D Top { get; set; } = null!;
	[Node] public CollisionShape2D Bottom { get; set; } = null!;
	[Node] public Sprite2D TopPipe { get; set; } = null!;
	[Node] public Sprite2D BottomPipe { get; set; } = null!;

	public bool IsCollided { get; set; }

	[Export] public float Speed { get; set; } = 100f;

	public void OnProvided() => this.ResolveComponent();

	public override void _PhysicsProcess(double delta) {
		if (IsCollided) {
			return;
		}

		Position += Vector2.Left * Speed * (float)delta;

		if (Position.X < -100) {
			QueueFree();
		}
	}
}
