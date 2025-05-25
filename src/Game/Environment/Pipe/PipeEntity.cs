namespace FlappyBirdGame.Game.Environment.Pipe;

using Entity.Behaviour;
using Entity.Environment;
using Utils.Extension;

public interface IPipeEntity : IMoveableEntity;

[Id(nameof(PipeEntity))]
[Meta(typeof(IAutoNode))]
public partial class PipeEntity : MoveableEnvironmentEntity, IPipeEntity {
	[Node] public CollisionShape2D Top { get; set; } = null!;
	[Node] public CollisionShape2D Bottom { get; set; } = null!;
	[Node] public Sprite2D TopPipe { get; set; } = null!;
	[Node] public Sprite2D BottomPipe { get; set; } = null!;

	public void OnProvided() => this.ResolveComponent();
}
