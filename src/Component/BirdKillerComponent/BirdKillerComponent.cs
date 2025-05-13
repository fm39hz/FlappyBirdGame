namespace FlappyBirdGame.Component.BirdKillerComponent;

using Chickensoft.Collections;
using Entity.Environment;

public interface IBirdKillerComponent : IArea2D;

[GlobalClass]
[Id(nameof(BirdKillerComponent))]
[Meta(typeof(IAutoNode))]
public partial class BirdKillerComponent : Area2D, IBirdKillerComponent {
	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public IEnvironmentEntity Entity => EntityTable.Get<IEnvironmentEntity>(GetParent().Name)!;

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
