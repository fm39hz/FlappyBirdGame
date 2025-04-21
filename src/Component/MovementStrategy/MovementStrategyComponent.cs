namespace FlappyBirdGame.Component.MovementStrategy;

using Chickensoft.Collections;
using Entity.Creature;
using Input;

public interface IMovementStrategyComponent : IComponentRepo<IMovementStrategyRepo, IAnimalEntity>;

[GlobalClass]
[Id(nameof(MovementStrategyComponent))]
[Meta(typeof(IAutoNode))]
public partial class MovementStrategyComponent : Node, IMovementStrategyComponent {
	public override void _Ready() => Repo = new MovementStrategyRepo(MovementStrategyFactory.Create(MovementType));

	public override void _PhysicsProcess(double delta) => Repo.PressButton(Entity.Velocity);

	#region Data

	[Export] public EInputType MovementType { get; set; }

	public void OnWireUp() {
		// TODO: Add documentation, and add the documentation to the class
	}

	public IAnimalEntity Entity => EntityTable.Get<IAnimalEntity>(GetParent().Name)!;

	public IMovementStrategyRepo Repo { get; private set; } = null!;

	#endregion

	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
