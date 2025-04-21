namespace FlappyBirdGame.Utils.Manager;

public abstract class Singleton<T> where T : Singleton<T>, new() {
	protected static readonly T _instance = new Lazy<T>(static () => new T()).Value;
	protected readonly SceneTree _tree = (SceneTree)Engine.GetMainLoop();
}
