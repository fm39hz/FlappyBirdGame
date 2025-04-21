namespace FlappyBirdGame;

#if DEBUG
using System.Reflection;
using Chickensoft.GoDotTest;
using Utils.Manager;
#endif

// This entry-point file is responsible for determining if we should run tests.
//
// If you want to edit your game's main entry-point, please see Game.tscn and
// Game.cs instead.

public partial class Main : Node2D {
	#if DEBUG
	private TestEnvironment _environment = null!;
	#endif
	[Export] public PackedScene GameContainer { get; set; } = null!;
	[Export] public PackedScene Gui { get; set; } = null!;

	public override void _Ready() {
		#if DEBUG
		// If this is a debug build, use GoDotTest to examine the
		// command line arguments and determine if we should run tests.
		_environment = TestEnvironment.From(OS.GetCmdlineArgs());
		if (_environment.ShouldRunTests) {
			CallDeferred(MethodName.RunTests);
			return;
		}
		#endif
		// If we don't need to run tests, we can just switch to the game scene.
		CallDeferred(MethodName.RunScene);
	}

	#if DEBUG
	private void RunTests() => _ = GoTest.RunTests(Assembly.GetExecutingAssembly(), this, _environment);
	#endif

	private void RunScene() {
		SceneManager.LoadContainer(GameContainer, [Gui.Instantiate()]);
		QueueFree();
	}
}
