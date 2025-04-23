namespace FlappyBirdGame.Component.Animation;

using System.Linq;
using System.Text.RegularExpressions;
using Chickensoft.Collections;
using Entity.Behaviour;

public interface IAnimationComponent : IAnimationPlayer, IComponentRepo<IAnimationRepo, IAnimatedEntity> {
	public bool IsFourDirection { get; set; }

	public void Animate(string animation);
}

[GlobalClass]
[Id(nameof(AnimationComponent))]
[Meta(typeof(IAutoNode))]
public partial class AnimationComponent : AnimationPlayer, IAnimationComponent {
	public void Animate(string animation) => Play(animation);

	#region Data

	[Export] public bool IsFourDirection { get; set; }

	/// <summary>
	///     Regex template for Animations key
	///     Any key in animation keys has 2 digit after "_" will
	///     be guessed that animation has 8 direction
	/// </summary>
	[GeneratedRegex(@"_\w{2}$")]
	private static partial Regex Has8Direction();

	public void OnWireUp() {
		if (IsFourDirection) {
			IsFourDirection = !GetAnimationList().Any(static s => Has8Direction().IsMatch(s));
		}

		Repo.AnimationChanged += Animate;
		// DirectionRepo.DirectionChanged += Repo.ChangeDirection;
	}

	public IAnimationRepo Repo { get; } = new AnimationRepo();

	#endregion

	#region AutoInject

	[Dependency] public EntityTable EntityTable => this.DependOn<EntityTable>();

	// [Dependency] public IDirectionRepo DirectionRepo => this.DependOn<IDirectionRepo>();
	public IAnimatedEntity Entity => EntityTable.Get<IAnimatedEntity>(GetParent().Name)!;

	public override void _Notification(int what) => this.Notify(what);

	#endregion AutoInject
}
