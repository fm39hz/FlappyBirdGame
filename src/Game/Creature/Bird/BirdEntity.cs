namespace FlappyBirdGame.Game.Creature;

using Component.Animation;
using Entity.Behaviour;
using Entity.Creature;
using Utils.Extension;

public interface IBirdEntity : IStateMachineEntity<IBirdStateMachine>;

[Id(nameof(BirdEntity))]
[Meta(typeof(IAutoNode))]
public partial class BirdEntity : AnimalEntity, IBirdEntity {
	public IBirdStateMachine StateMachine => new BirdStateMachine();

	public const string ANIMATION_NAME = "Flap";

	[Node] public IAnimationComponent AnimationComponent { get; set; } = null!;
	[Node] public Timer Timer { get; set; } = null!;

	public void OnProvided() {
		this.ResolveComponent();
		AnimationComponent.Play(ANIMATION_NAME);
		this.RegisterToStateMachine(StateMachine);
		StateMachine.Set(Timer);
		StateMachine.Start();
	}
}
