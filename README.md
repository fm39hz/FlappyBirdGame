# Flappy Bird Game

- A FlappyBird Clone using [Godot](https://godotengine.org/) and [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)

## Architecture

- Component-Entity architecture
- Based on ECS, MVVM and DDD as reference

![ECS](docs/architecture/ecs.png)

### Core Aspect

- **Entity**: Act as a container for components
- **Component**: Provide specific functionality for its entity
- **Repository**: Manage component's states & data
- **EntityTable**: Register center for all entities
- **StateMachine**: Manage state of entity

### Life Cycle

![LifeCycle](docs/architecture/lifecycle.png)

1. Entity register into EntityTable
2. Entity scan an initialize components
3. Component makes connection and register event

### Benefits

- **Modular**: Component is reusable
- **Loosely Coupled**: Component depends on UI
- **Extensible**: Easy to add a new component
- **Testable**: Easy to mock for testing
- **Clean Architecture**: Repository separate state and behavior

## Features

- Flappy Bird clone
- High score
- Leaderboard
