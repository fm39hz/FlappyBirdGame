namespace FlappyBirdGame.Game.Map;

using Chickensoft.Collections;

public interface IWorld : INode2D, IProvide<EntityTable>;

[GlobalClass]
[Id(nameof(World))]
[Meta(typeof(IAutoNode))]
public partial class World : Node2D, IWorld {
	public EntityTable EntityTable { get; private set; } = new();

	public virtual void OnReady() => this.Provide();

	public override void _Notification(int what) => this.Notify(what);

	EntityTable IProvide<EntityTable>.Value() => EntityTable;
}
