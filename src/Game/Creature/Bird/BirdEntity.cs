namespace FlappyBirdGame.Game.Creature;

using Entity.Behaviour;
using Entity.Creature;
using Utils.Extension;

public interface IBirdEntity : IStateMachineEntity<IBirdStateMachine>;

[Id(nameof(BirdEntity))]
[Meta(typeof(IAutoNode))]
public partial class BirdEntity : AnimalEntity, IBirdEntity {
	public IBirdStateMachine StateMachine => new BirdStateMachine();

	[Node] public Timer Timer { get; set; } = null!;

	public void OnProvided() {
		this.ResolveComponent();
		this.RegisterToStateMachine(StateMachine);
		StateMachine.Set(Timer);
		StateMachine.Start();
	}
}
