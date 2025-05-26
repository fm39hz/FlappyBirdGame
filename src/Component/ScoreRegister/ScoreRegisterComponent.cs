namespace FlappyBirdGame.Component.ScoreRegister;

using Chickensoft.Collections;
using Game.Creature;

public interface IScoreRegisterComponent : IArea2D, IComponent;

[GlobalClass]
[Id(nameof(ScoreRegisterComponent))]
[Meta(typeof(IAutoNode))]
public partial class ScoreRegisterComponent : Area2D, IScoreRegisterComponent {
	private void OnBodyEntered(Node2D body) {
		if (body is not IBirdEntity bird) {
			return;
		}

		bird.IncreaseScore();
		QueueFree();
	}

	#region Data

	public void OnWireUp() => BodyEntered += OnBodyEntered;

	#endregion

	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();
	public override void _Notification(int what) => this.Notify(what);

	#endregion
}
