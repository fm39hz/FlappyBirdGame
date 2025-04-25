namespace FlappyBirdGame.Utils.Extension;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Chickensoft.Log;
using Chickensoft.LogicBlocks;

public static class EntityExtension {
	/// <summary>
	///     Scan Component this entity Contains
	/// </summary>
	/// <returns>Scanned Component</returns>
	public static Dictionary<Type, IComponent> ScanComponent(this IEntity entity) {
		var components = new Dictionary<Type, IComponent>();
		foreach (var child in entity.GetChildren()) {
			if (child is not IComponent component) {
				continue;
			}

			components.TryAdd(component.GetType(), component);
		}

		return components;
	}

	/// <summary>
	///     Get specific component from current IEntity
	/// </summary>
	/// <param name="entity">this IEntity</param>
	/// <param name="required">If required, the function will throw exception when not found, and return null if not</param>
	/// <typeparam name="T">Type of component</typeparam>
	/// <returns></returns>
	/// <exception cref="MissingMemberException">Emit when cannot found the component</exception>
	public static T GetComponent<T>(this IEntity entity, bool required = false) where T : IComponent {
		foreach (var component in entity.Components.Values) {
			if (component is T matchingComponent) {
				return matchingComponent;
			}
		}

		if (!required) {
			return default!;
		}
		#if DEBUG
		var logger = new Log(nameof(GetComponent));
		logger.Err($"Component of type {typeof(T)} not found");
		#endif
		throw new MissingMemberException($"Component of type {typeof(T)} not found");
	}

	/// <summary>
	///     Place where components get each other
	/// </summary>
	public static void ResolveComponent(this IEntity entity) => entity.ResolveComponent();

	/// <summary>
	///     Register all components to the state machine
	/// </summary>
	/// <param name="entity">this IEntity</param>
	/// <param name="stateMachine">target stateMachine of entity</param>
	public static TStateMachine RegisterToStateMachine<TStateMachine>(this IEntity entity,
		in TStateMachine stateMachine) where TStateMachine : IStateMachine {
		stateMachine.Set(entity);
		var setMethod = stateMachine.GetType().BaseType?
		.GetMethods(BindingFlags.Public | BindingFlags.Instance)
		.FirstOrDefault(static m => m is { Name: "Set", IsGenericMethod: true });

		if (setMethod == null) {
			throw new UnreachableException();
		}

		var invokeEntity = setMethod.MakeGenericMethod(entity.GetType());
		invokeEntity.Invoke(stateMachine, [entity]);

		#if DEBUG
		var logger = new Log(entity.GetType().Name);
		#endif
		foreach (var component in entity.Components) {
			var genericMethod = setMethod.MakeGenericMethod(component.Key);
			genericMethod.Invoke(stateMachine, [component.Value]);

			var repoProperty = component.Key.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.FirstOrDefault(static p => typeof(IRepository).IsAssignableFrom(p.PropertyType) && p.Name == "Repo");

			if (repoProperty == null) {
				continue;
			}

			var repo = repoProperty.GetValue(component.Value);
			if (repo == null) {
				continue;
			}

			var repoInterfaces = repo.GetType().GetInterfaces();
			foreach (var repoInterface in repoInterfaces) {
				// Skip the base IRepository interface and register only more specific interfaces
				if (!typeof(IRepository).IsAssignableFrom(repoInterface) || repoInterface == typeof(IRepository)) {
					continue;
				}

				var repoMethod = setMethod.MakeGenericMethod(repoInterface);
				repoMethod.Invoke(stateMachine, [repo]);
				#if DEBUG
				logger.Print($"Registered {repoInterface.Name} to State Machine");
				#endif
			}
		}
		return stateMachine;
	}
}
