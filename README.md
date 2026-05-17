# Slot Machine

A Unity-based slot machine game built with WebGL support for browser deployment.

## Features

- Slot machine gameplay with spinning reels.
- Lever interaction for starting spins.
- Reel configuration and game logic handled in C#.
- Built for WebGL deployment.

## Tech Stack

- Unity
- C#
- WebGL
- GitHub
- Vercel for hosting

## Project Structure

- `Assets/` — Unity assets, scenes, scripts, sounds, and settings.
- `ProjectSettings/` — Unity project configuration.
- `Packages/` — Unity package dependencies.
- `Build/WebGL/` — WebGL build output for deployment.

## How to Run Locally

1. Open the project in Unity.
2. Make sure the WebGL platform module is installed.
3. Open the main scene from `Assets/Scenes/`.
4. Press Play in the Unity editor to test the game.

## How to Build WebGL

1. Open **File > Build Settings**.
2. Select **WebGL** as the target platform.
3. Click **Build**.
4. Choose `Build/WebGL/` as the output folder inside the project.
5. Wait for Unity to generate the WebGL files.

## Deployment Notes

- The WebGL output folder should contain `index.html`.
- If deploying on Vercel, set the Output Directory to `Build/WebGL`.
- Redeploy after every new WebGL build.

## Scripts

- `SlotGameManager.cs` — Main gameplay logic.
- `LeverControl.cs` — Controls lever interaction.
- `LeverSwap.cs` — Handles lever state or animation swapping.
- `ReelConfig.cs` — Stores reel configuration data.

## Notes

- Do not commit temporary or accidental files.
- Keep Unity-generated `.meta` files with their corresponding assets.
- Make sure the WebGL build folder is included if you want to deploy the game.

## License

This project is for educational/demo purposes unless stated otherwise.
