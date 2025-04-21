namespace FlappyBirdGame.Entity.Behaviour;

public interface IStateMachineEntity<out TStateMachine> : IEntity where TStateMachine : IStateMachine {
	public TStateMachine StateMachine { get; }
}
