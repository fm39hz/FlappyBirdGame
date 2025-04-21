namespace FlappyBirdGame.Component.Logic;

using Chickensoft.Collections;

public interface IStateMachineComponent : IComponentRepo<IStateMachineRepo, IEntity>;

[GlobalClass]
[Id(nameof(StateMachineComponent))]
[Meta(typeof(IAutoNode))]
public partial class StateMachineComponent : Node, IStateMachineComponent {
	[Signal]
	public delegate void StateChangedEventHandler(string name);

	#region Data

	public void OnWireUp() {
		// TODO: Implement connection
	}

	public IStateMachineRepo Repo { get; } = new StateMachineRepo();
	public IEntity Entity => EntityTable.Get<IEntity>(GetParent().Name)!;

	#endregion

	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
