namespace FlappyBirdGame.Game.Creature;

using Entity.Behaviour;
using Entity.Creature;
using Utils.Extension;

public interface IBirdEntity : IStateMachineEntity<IBirdStateMachine>;

[Id(nameof(BirdEntity))]
[Meta(typeof(IAutoNode))]
public partial class BirdEntity : AnimalEntity, IBirdEntity {
	public IBirdStateMachine StateMachine => this.RegisterToStateMachine(new BirdStateMachine());

	[Node] public Timer Timer { get; set; } = null!;
	[Node] public Sprite2D Sprite2D { get; set; } = null!;

	public void OnProvided() {
		this.ResolveComponent();
		StateMachine.Set(Timer);
		using var binding = StateMachine.Bind();
		binding.Handle((in BirdStateMachine.Output.ChangeRotation rotation) => {
			Sprite2D.Rotation += rotation.rotation;
		});
		StateMachine.Start();
		GD.Print("");
	}
}
