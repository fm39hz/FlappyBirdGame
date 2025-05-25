namespace FlappyBirdGame.Component.ScoreCounter;

using Chickensoft.Collections;
using Entity.Environment;
using Game.Creature;

public interface IScoreCounterComponent : IComponentRepo<IScoreCounterRepo, IBirdEntity>;

[GlobalClass]
[Id(nameof(ScoreCounterComponent))]
[Meta(typeof(IAutoNode))]
public partial class ScoreCounterComponent : Node, IScoreCounterComponent {
	#region Data

	public void OnWireUp() { }

	public IBirdEntity Entity => EntityTable.Get<IBirdEntity>(GetParent().Name)!;

	//TODO: Implement high-score
	public IScoreCounterRepo Repo { get; } = new ScoreCounterRepo(1000);

	#endregion

	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();
	public override void _Notification(int what) => this.Notify(what);

	#endregion
}
