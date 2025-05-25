namespace FlappyBirdGame.Component.ScoreCounter;

using Chickensoft.GoDotCollections;

public interface IScoreCounterRepo : IRepository {
	/// <summary>
	/// Represents the score property that keeps track of the current score
	/// accumulated during the game session. The value increases incrementally
	/// as the game progresses and is reset upon starting a new session unless
	/// explicitly managed otherwise.
	/// </summary>
	public IAutoProp<int> Score { get; }

	/// <summary>
	/// Represents the high-score property that maintains the record
	/// of the highest score achieved in the game.
	/// The value is initialized upon creation and does not decrease
	/// unless explicitly modified.
	/// </summary>
	public IAutoProp<int> HighScore { get; }

	/// <summary>
	/// Increases the current score by one. Update the high-score if necessary.
	/// </summary>
	public void Increase();
}

public class ScoreCounterRepo(int highScore) : BaseRepository(nameof(ScoreCounterRepo)), IScoreCounterRepo {
	private readonly AutoProp<int> _score = new(0);
	public IAutoProp<int> Score => _score;
	private readonly AutoProp<int> _highScore = new(highScore);
	public IAutoProp<int> HighScore => _highScore;

	public void Increase() => _score.OnNext(_score.Value + 1);
}
