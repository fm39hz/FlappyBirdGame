namespace FlappyBirdGame.Contract;

using Chickensoft.Log;

/// <summary>
///     Repository for pieces
/// </summary>
public interface IRepository : IDisposable;

/// <summary>
///     Base Repository for every Repo
/// </summary>
/// <param name="name"></param>
public abstract class BaseRepository(string name) : IRepository {
	#if DEBUG
	protected readonly Log _logger = new(name);
	#endif
	protected bool _isDisposed;

	public virtual void Dispose() {
		if (!_isDisposed) {
			_isDisposed = true;
			#if DEBUG
			_logger.Print("Complete disposal");
			#endif
		}
		GC.SuppressFinalize(this);
	}
}
