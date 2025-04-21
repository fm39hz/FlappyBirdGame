namespace FlappyBirdGame.Entity.Creature;

using System.Collections.Generic;
using Behaviour;
using Chickensoft.Collections;

public interface IBotanicalEntity : IStaticEntity;

[GlobalClass]
[Id(nameof(BotanicalEntity))]
[Meta(typeof(IAutoNode))]
public partial class BotanicalEntity : StaticBody2D, IBotanicalEntity {
	public Dictionary<Type, IComponent> Components { get; set; } = [];

	#region AutoInject

	EntityTable IProvide<EntityTable>.Value() => EntityTable;

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public void OnResolved() => this.Provide();

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
