namespace FlappyBirdGame.Component.BirdKillerComponent;

using Chickensoft.Collections;
using Entity.Behaviour;
using Entity.Creature;
using Entity.Environment;
using Game.Creature;

public interface IBirdKillerComponent : IArea2D;

[GlobalClass]
[Id(nameof(BirdKillerComponent))]
[Meta(typeof(IAutoNode))]
public partial class BirdKillerComponent : Area2D, IBirdKillerComponent {

	public override void _Ready() {
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private static void OnBodyEntered(Node2D body) {
		if (body is not IBirdEntity bird) {
			return;
		}

		bird.Collide();
	}

	private static void OnBodyExited(Node2D body) {
		if (body is not IBirdEntity bird) {
			return;
		}
		GD.Print("shit");
		//TODO: Tạo gửi signal bird đã tèo
	}

	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public IEnvironmentEntity Entity => EntityTable.Get<IEnvironmentEntity>(GetParent().Name)!;

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
