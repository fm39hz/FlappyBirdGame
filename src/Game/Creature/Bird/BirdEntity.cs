namespace FlappyBirdGame.Game.Creature;

using Entity.Behaviour;
using Entity.Creature;
using Utils.Extension;

public interface IBirdEntity : IStateMachineEntity<BirdStateMachine>;

[Id(nameof(BirdEntity))]
[Meta(typeof(IAutoNode))]
public partial class BirdEntity : AnimalEntity, IBirdEntity {
	public BirdStateMachine StateMachine { get; set; } = null!;

	[Node] public Timer Timer { get; set; } = null!;
	[Node] public Sprite2D Sprite2D { get; set; } = null!;

	public void OnProvided() {
		this.ResolveComponent();
		StateMachine = this.RegisterToStateMachine(new BirdStateMachine());
		StateMachine.Set(Timer);
		using var binding = StateMachine.Bind();
		binding.Handle((in BirdStateMachine.Output.ChangeRotation rotation) => {
			Sprite2D.Rotation += rotation.rotation;
		});
		StateMachine.Start();
		GD.Print("");
	}
}
