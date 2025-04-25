namespace FlappyBirdGame.Component.Input;

using Chickensoft.GoDotCollections;

public interface IActionInputRepo : IRepository {
	public IAutoProp<ISingleInputRepo> ActionButton { get; }
}

public class ActionInputRepo() : BaseRepository(nameof(ActionInputRepo)), IActionInputRepo {
	private readonly AutoProp<ISingleInputRepo> _actionButton = new(new SingleInputRepo(EInputMapping.Action));
	public IAutoProp<ISingleInputRepo> ActionButton => _actionButton;
}
