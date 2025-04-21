namespace FlappyBirdGame.Contract;

using Chickensoft.Collections;
using JetBrains.Annotations;

public interface IComponent : INode {
	/// <summary>
	///     Entity Table of this component
	/// </summary>
	[UsedImplicitly]
	public EntityTable EntityTable { get; }

	/// <summary>
	///     Wire up this component with godot built-in node & signals
	/// </summary>
	public void OnWireUp();
}

/// <summary>
///     Base Component declaration
/// </summary>
/// <typeparam name="TRepo">Type of Data this component contain</typeparam>
/// <typeparam name="TEntity">Type of Entity this component should belong to</typeparam>
public interface IComponentRepo<out TRepo, out TEntity> : IComponent
	where TRepo : IRepository where TEntity : IEntity {
	/// <summary>
	///     Entity of this component
	/// </summary>
	[UsedImplicitly]
	public TEntity Entity { get; }

	/// <summary>
	///     Component's data
	/// </summary>
	public TRepo Repo { get; }
}
