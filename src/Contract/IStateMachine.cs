namespace FlappyBirdGame.Contract;

using Chickensoft.LogicBlocks;

public interface IStateMachine : ILogicBlockBase {
	/// <summary>
	/// Starts the logic block by entering the current state. If the logic block
	/// is already started, nothing happens. If the logic block
	/// has not initialized its underlying state, it will initialize it by calling
	/// </summary>
	public void Start();


	/// <summary>
	/// Adds data to the blackboard so that it can be looked up by its
	/// compile-time type.
	/// <br />
	/// Data is retrieved by its type, so do not add more than one piece of data
	/// with the same type.
	/// </summary>
	/// <param name="data">Data to write to the blackboard.</param>
	/// <typeparam name="TData">Type of the data to add.</typeparam>
	public void Set<TData>(TData data) where TData : class;
}
