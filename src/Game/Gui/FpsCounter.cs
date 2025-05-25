namespace FlappyBirdGame.Game.Gui;

public partial class ScoreCounter : Label {
	public override void _Process(double delta) => Text = $"FPS: {Engine.GetFramesPerSecond()}";
}
