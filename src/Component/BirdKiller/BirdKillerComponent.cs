namespace FlappyBirdGame.Component.BirdKillerComponent;

using Chickensoft.Collections;
using Game.Creature;

public interface IBirdKillerComponent : IComponent, IArea2D;

[GlobalClass]
[Id(nameof(BirdKillerComponent))]
[Meta(typeof(IAutoNode))]
public partial class BirdKillerComponent : Area2D, IBirdKillerComponent {
	public void OnWireUp() => BodyEntered += OnBodyEntered;

	private static void OnBodyEntered(Node2D body) {
		if (body is not IBirdEntity bird) {
			return;
		}

		bird.Collide();
	}

	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
