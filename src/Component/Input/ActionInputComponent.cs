namespace FlappyBirdGame.Component.Input;

using Chickensoft.Collections;
using Entity.Creature;

public interface IActionInputComponent : IComponentRepo<IActionInputRepo, IAnimalEntity>;

[GlobalClass]
[Id(nameof(ActionInputComponent))]
[Meta(typeof(IAutoNode))]
public partial class ActionInputComponent : Node, IActionInputComponent {
	#region Data

	public void OnWireUp() {
		// TODO: wiring component
	}

	public IActionInputRepo Repo { get; } = new ActionInputRepo();

	#endregion

	#region AutoInject

	public IAnimalEntity Entity => EntityTable.Get<IAnimalEntity>(GetParent()!.Name)!;
	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public override void _Notification(int what) => this.Notify(what);

	#endregion
}
