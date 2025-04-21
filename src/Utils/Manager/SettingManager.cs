namespace FlappyBirdGame.Utils.Manager;

public enum EDimension {
	TopDown,
	SideScrolling,
	Isometric
}

public class SettingManager : Singleton<SettingManager> {
	public EDimension Dimension { get; set; } = EDimension.TopDown;
}
