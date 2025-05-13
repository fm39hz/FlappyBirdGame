namespace FlappyBirdGame.Entity.Environment;

using System.Collections.Generic;
using Behaviour;
using Chickensoft.Collections;

public interface IEnvironmentEntity : IStaticEntity, IAnimatedEntity;

[GlobalClass]
[Id(nameof(EnvironmentEntity))]
[Meta(typeof(IAutoNode))]
public partial class EnvironmentEntity : StaticBody2D, IEnvironmentEntity {
	public Dictionary<Type, IComponent> Components { get; set; } = [];

	#region AutoInject

	EntityTable IProvide<EntityTable>.Value() => EntityTable;

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public void OnResolved() => this.Provide();

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
