namespace GameBoilerPlate.Component.Movement;

using Chickensoft.Collections;
using FlappyBirdGame.Component.MovementStrategy;
using FlappyBirdGame.Entity.Behaviour;
using FlappyBirdGame.Utils.Extension;

public interface IMovementComponent : IComponentRepo<IMovementRepo, IMoveableEntity> {
	public void Move(Vector2 velocity);

	public void StopMovement();
}

[GlobalClass]
[Id(nameof(MovementComponent))]
[Meta(typeof(IAutoNode))]
public partial class MovementComponent : Node, IMovementComponent {
	public void Move(Vector2 velocity) {
		if (!Repo.IsMoveable.Value) {
			return;
		}

		Entity.Velocity = velocity * Repo.Speed.Value;
		Entity.MoveAndSlide();
		Repo.Move(Entity.Velocity.IsEqualApprox(Vector2.Zero));
	}

	public void StopMovement() {
		Entity.Velocity = Vector2.Zero;
		Repo.Move(false);
	}

	#region Data

	public void OnWireUp() {
		using var repo = Entity.GetComponent<IMovementStrategyComponent>().Repo;
		repo.IsMoved += Move;
		repo.StopMovement += StopMovement;
	}

	public IMovementRepo Repo { get; } = new MovementRepo();
	public IMoveableEntity Entity => EntityTable.Get<IMoveableEntity>(GetParent().Name)!;

	#endregion

	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
