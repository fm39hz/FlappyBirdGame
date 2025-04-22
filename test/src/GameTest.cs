namespace FlappyBirdGame.Test;

using System.Threading.Tasks;
using Chickensoft.GoDotTest;
using Chickensoft.GodotTestDriver;
using Component.Animation;
using Game.Creature;
using JetBrains.Annotations;
using Map.Levels;
using Shouldly;


public class GameTest(Node testScene) : TestClass(testScene) {
	private Fixture _fixture = null!;
	[UsedImplicitly] private GameLevel _world = null!;


	[SetupAll]
	public async Task Setup() {
		_fixture = new Fixture(TestScene.GetTree());
		_world = await _fixture.LoadAndAddScene<GameLevel>();
	}

	[CleanupAll]
	public void Cleanup() => _fixture.Cleanup();

	[Test]
	public void TestBird() {
		var npc = _world.EntityTable.Get<BirdEntity>(nameof(BirdEntity));
		npc.ShouldNotBeNull();
		npc.Components.ShouldContainKey(typeof(AnimationComponent));
	}
}
