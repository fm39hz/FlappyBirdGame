namespace FlappyBirdGame.Utils.NodeExtra;

public abstract partial class SpawningPoint : Area2D {
	public abstract Type TargetEntity { get; }
}
