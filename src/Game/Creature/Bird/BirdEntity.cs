namespace FlappyBirdGame.Game.Creature;

using Component.Animation;
using Entity.Behaviour;
using Entity.Creature;
using Utils.Extension;

public interface IBirdEntity : IStateMachineEntity<BirdStateMachine>;

[Id(nameof(BirdEntity))]
[Meta(typeof(IAutoNode))]
public partial class BirdEntity : AnimalEntity, IBirdEntity {
	public BirdStateMachine StateMachine => new();

	[Node] public IAnimationComponent AnimationComponent { get; set; } = null!;

	public void OnProvided() {
		this.ResolveComponent();
		this.RegisterToStateMachine(StateMachine);
		StateMachine.Start();
		AnimationComponent.Play("Flap");
	}
}
