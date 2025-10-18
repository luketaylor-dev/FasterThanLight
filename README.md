# FTL Combat Foundation

A Unity-based clone of the ship combat mechanics from _Faster Than Light (FTL)_, focusing on clean architecture and design patterns for portfolio demonstration.

## Project Goals

This project serves as a **portfolio piece** demonstrating:

- **Game Development Skills**: Unity, C#, component-based architecture
- **Design Patterns**: State Machine, Observer, Command, Factory, Component, Object Pool, Decorator, Dependency Injection
- **System Architecture**: Modular, scalable code structure
- **Game Mechanics**: Real-time combat with tactical depth

## Features

### Current Implementation (Stage 1)

- **Ship Combat**: Two ships attacking each other in real-time
- **Room-Based Targeting**: Click to target specific enemy ship rooms
- **Layered Shields**: Multi-layer shield system that blocks projectiles
- **Weapon Charging**: State-based weapon system with charge times
- **Projectile System**: Object-pooled projectiles with collision detection
- **Simple AI**: Enemy ship with random targeting behavior
- **Pause Functionality**: Tactical pause to issue commands
- **Visual Feedback**: Health bars, shield indicators, targeting reticles

### Planned Features (Future Stages)

- Power/reactor management system
- Crew movement and room assignments
- Multiple weapon types (missiles, beams, ions)
- System repairs and maintenance
- Advanced AI behaviors
- Procedural ship generation

## Architecture Overview

### Design Patterns Used

| Pattern                  | Purpose                  | Implementation                                       |
| ------------------------ | ------------------------ | ---------------------------------------------------- |
| **State Machine**        | Game flow control        | Game states (Setup, Combat, Paused, Victory, Defeat) |
| **Component**            | Modular ship systems     | Ship composed of Room components                     |
| **Observer**             | Event-driven responses   | CombatEvents for damage, UI updates                  |
| **Command**              | Player actions           | TargetCommand for room targeting                     |
| **Factory**              | Object creation          | WeaponFactory for different weapon types             |
| **Object Pool**          | Performance optimization | ProjectilePool for efficient projectile management   |
| **Decorator**            | Layered mechanics        | ShieldLayer decorators for shield system             |
| **Dependency Injection** | Loose coupling           | ServiceLocator for system dependencies               |

### Project Structure

```
Assets/
├── Scripts/
│   ├── Combat/           # Core combat systems
│   │   ├── Ships/        # Ship and Room components
│   │   ├── Weapons/      # Weapon state machine and factory
│   │   ├── Shields/      # Layered shield system
│   │   ├── Projectiles/  # Projectile pool and movement
│   │   ├── Targeting/    # Player targeting and commands
│   │   └── CombatManager.cs
│   ├── Core/             # Foundation systems
│   │   ├── StateMachine/ # Game state management
│   │   ├── Events/       # Event system
│   │   └── Services/     # Service locator
│   └── AI/               # Enemy AI behaviors
├── Prefabs/              # Ship and projectile prefabs
├── ScriptableObjects/    # Ship and weapon configurations
└── Scenes/               # Combat test scene
```

## How to Play

1. **Targeting**: Click on enemy ship rooms to target them
2. **Weapons**: Weapons charge automatically and fire when ready
3. **Shields**: Enemy shields block projectiles (multiple layers)
4. **Strategy**: Target enemy weapons to reduce their firepower
5. **Pause**: Press Space to pause and plan your moves
6. **Victory**: Destroy the enemy ship to win

## Technical Details

### Core Systems

#### Ship Architecture

- Ships are composed of modular `Room` components
- Each room has health, type, and position
- Room damage affects system performance
- Supports future crew assignments and system interactions

#### Weapon System

- State-based weapon charging (Idle → Charging → Ready → Firing → Cooldown)
- Weapons linked to specific weapon rooms
- Factory pattern for creating different weapon types
- Extensible for missiles, beams, and ion weapons

#### Shield System

- Multi-layer shield bubbles using Decorator pattern
- Each layer blocks one projectile hit
- Shield strength based on shield room health
- Automatic recharge with configurable timing

#### Combat Flow

- Real-time combat with tactical pause
- Event-driven damage system
- Win/lose conditions based on ship destruction
- State machine manages game flow

### Performance Considerations

- Object pooling for projectiles to avoid GC spikes
- Event-driven architecture reduces coupling
- Component-based design enables efficient updates
- Service locator pattern facilitates testing

## Getting Started

### Prerequisites

- Unity 2022.3 LTS or later
- Basic knowledge of C# and Unity

### Installation

1. Clone the repository
2. Open the project in Unity
3. Load the `CombatTest` scene
4. Press Play to start combat

### Development Setup

1. Follow the project structure outlined above
2. Implement systems in the suggested order:
   - Core Architecture (State Machine, Events, Services)
   - Ship Systems (Ship, Room components)
   - Combat Systems (Weapons, Shields, Projectiles)
   - Combat Flow (Manager, Targeting, AI)
   - Polish (UI, Visual Effects, Testing)

## Testing

The project includes a testing strategy focusing on:

- Unit tests for individual systems
- Integration tests for combat scenarios
- Mock services for isolated testing
- Performance testing for projectile pooling

## Project Management

This project uses **Codecks.io** for task management with:

- **Cards** for individual features and tasks
- **Decks** organizing cards by system category
- **Complexity estimates** using Fibonacci scale (0,1,2,3,5,8)
- **Priority levels** for implementation order

## Visual Style

- **Top-down 2D view** showing ship interiors
- **Room-based layout** with clear boundaries
- **Real-time indicators** for weapon charge, shield status, room health
- **Tactical UI** with health bars and system status
- **Visual feedback** for hits, shield impacts, and targeting

## Future Development

### Stage 2: Power Management

- Reactor power allocation system
- Power distribution between systems
- Power management UI

### Stage 3: Crew System

- Crew movement between rooms
- Crew assignments to boost system effectiveness
- Crew health and oxygen management

### Stage 4: Advanced Combat

- Multiple weapon types and effects
- System repairs and maintenance
- Advanced AI behaviors
- Procedural ship generation

## Learning Outcomes

This project demonstrates:

- **Game Architecture**: Component-based design, state management
- **Design Patterns**: Practical application of common patterns
- **Unity Best Practices**: Prefabs, ScriptableObjects, event systems
- **Performance Optimization**: Object pooling, efficient updates
- **Code Organization**: Modular, testable, maintainable code

## Contributing

This is a portfolio project, but suggestions and improvements are welcome:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is for educational and portfolio purposes. FTL is a trademark of Subset Games.

---

**Note**: This project is inspired by _Faster Than Light_ by Subset Games. It's created for educational purposes to demonstrate game development skills and design patterns.
