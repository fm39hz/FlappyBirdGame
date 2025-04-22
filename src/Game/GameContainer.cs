namespace FlappyBirdGame.Game;

using Utils.Manager;

public interface IGameContainer : IControl;

public partial class GameContainer : Control, IGameContainer {
	[Export] public PackedScene? Level { get; set; }
	[Export] public PackedScene? Player { get; set; }

	public override void _Ready() {
		if (Level == null || Player == null) {
			Quit();
			return;
		}
		var player = Player.Instantiate<Node2D>();
		player.Position = GetViewportRect().Size / 2;
		SceneManager.GotoScene(Level, [player]);
	}

	public void Quit() {
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
		GetTree().CallDeferred(SceneTree.MethodName.Quit);
	}
}
