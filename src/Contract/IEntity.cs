namespace FlappyBirdGame.Contract;

using System.Collections.Generic;
using Chickensoft.Collections;
using Utils.Extension;

/// <summary>
///     Base Entity declaration
/// </summary>
public interface IEntity : INode, IProvide<EntityTable> {
	/// <summary>
	///     List of Component, along with its type
	/// </summary>
	public Dictionary<Type, IComponent> Components { get; set; }

	public EntityTable EntityTable { get; }

	/// <summary>
	///     Place where components get each other
	/// </summary>
	public void ResolveComponent() {
		EntityTable.Set(Name, this);
		Components = this.ScanComponent();
		foreach (var component in Components.Values) {
			component.OnWireUp();
		}
	}
}
