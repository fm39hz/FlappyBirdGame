namespace FlappyBirdGame.Component.Input;

public interface IActionInputRepo : IRepository {
	public ISingleInputRepo ActionButton { get; }
	public ISingleInputRepo ResetButton { get; }
}

public class ActionInputRepo() : BaseRepository(nameof(ActionInputRepo)), IActionInputRepo {
	public ISingleInputRepo ActionButton { get; } = new SingleInputRepo(EInputMapping.Action);
	public ISingleInputRepo ResetButton { get; } = new SingleInputRepo(EInputMapping.Exit);
}
