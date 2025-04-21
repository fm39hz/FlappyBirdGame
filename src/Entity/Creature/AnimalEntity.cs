namespace FlappyBirdGame.Entity.Creature;

using System.Collections.Generic;
using Behaviour;
using Chickensoft.Collections;

public interface IAnimalEntity : IMoveableEntity, IAnimatedEntity;

[GlobalClass]
[Id(nameof(AnimalEntity))]
[Meta(typeof(IAutoNode))]
public partial class AnimalEntity : CharacterBody2D, IAnimalEntity {
	public Dictionary<Type, IComponent> Components { get; set; } = [];

	#region AutoInject

	EntityTable IProvide<EntityTable>.Value() => EntityTable;

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	public void OnResolved() => this.Provide();

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
