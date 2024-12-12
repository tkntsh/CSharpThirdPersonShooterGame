# Unity Game Project

This repository contains the scripts for my Unity-based third-person action game. Below is an overview of the core scripts and their functionality. This project is currently in **Version 1**, and more features will be added in future updates.

---

## Scripts Overview

### 1. **PlayerMovement.cs**
This script handles the player's movement and interactions.
- **Features:**
  - smooth movement using keyboard inputs (WASD).
  - player rotates to face the direction of movement.
  - triggers animations for idle, running, and shooting states.
  - integrates with the `PlayerShooting` script to allow shooting functionality.

### 2. **PlayerShooting.cs**
This script manages the player's shooting mechanics.
- **Features:**
  - detects input for shooting (default: `K` key).
  - handles interactions with enemy objects when bullets collide.
  - future expansion includes implementing damage mechanics.

### 3. **EnemyMovement.cs**
This script controls enemy behavior and movement.
- **Features:**
  - enemies follow the player using smooth pathfinding.
  - can be extended for attack mechanics and increased difficulty in future updates.

### 4. **CameraFollow.cs**
This script ensures that the camera dynamically follows the player.
- **Features:**
  - keeps the camera positioned behind the player.
  - rotates to maintain a view of the player's back, even when the player turns.

### 5. **AnimatorController.cs**
This script handles animation transitions for the player and enemies.
- **Features:**
  - uses Unity's Animator Controller to trigger animations based on player input and movement.
  - supports idle, running, and shooting animations.

---

## Future Updates
Planned features for upcoming versions:
- adding UI elements (start menu, instruction menu, pause screen).
- introducing multiple levels and unique arenas.
- implementing increased enemy difficulty and AI enhancements.
- refining visual effects and adding new animations.

---

## How to Run the Game
1. Clone this repository to your local machine.
2. Open the project in Unity (tested with Unity 2022 or later).
3. Attach the scripts to the appropriate GameObjects in your scene:
   - **PlayerMovement** to the player GameObject.
   - **EnemyMovement** to enemy GameObjects.
   - **CameraFollow** to the main camera.
   - **AnimatorController** to objects with animation components.
4. Press **Play** in Unity to start the game.
