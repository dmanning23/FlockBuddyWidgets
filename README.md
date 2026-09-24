# FlockBuddyWidgets

A set of [MenuBuddy](https://github.com/dmanning23/MenuBuddy) screens and widgets for building and tuning [FlockBuddy](https://github.com/dmanning23/FlockBuddy) boid simulations inside a MonoGame app, while it runs.

Use it to add and remove flocks, spawn boids, turn steering behaviors on and off, adjust behavior weights and boid physics, and set predator/prey/VIP relationships between flocks, all without restarting the game.

## Installation

```
dotnet add package FlockBuddyWidgets
```

- Targets `net8.0`
- Depends on `FlockBuddy` 5.x and `MenuBuddy` 5.x
- Uses MonoGame 3.8 (DesktopGL). The MonoGame reference is private, so your game supplies its own MonoGame package.

## Quick start

Everything works on a shared `List<FlockManager>`. Add a `DisplayScreen` to run and draw the flocks, then add a `FlocksScreen` on top of it to edit them:

```csharp
using FlockBuddy;
using FlockBuddyWidgets;

var flocks = new List<FlockManager>();

// Updates every flock each frame and draws the boids and cell-space grid for debugging
ScreenManager.AddScreen(new DisplayScreen(flocks));

// The editing panel: one row per flock, plus an "Add Flock" button
ScreenManager.AddScreen(new FlocksScreen(flocks));
```

Pass the same list to both screens. When the editor adds or removes a flock, the display screen picks up the change on its next update.

## Screens

| Screen | Purpose |
| --- | --- |
| `DisplayScreen` | Updates every flock with its own `GameClock` and draws the boids (in each flock's `DebugColor`) and the first flock's cell-space grid. Subclass it to change drawing. `DrawDebugBoids` and `DrawDebugCells` are protected properties, and the `SpriteBatchBegin` overloads are virtual. |
| `FlocksScreen` | Top-level list of flocks. Click a flock's name to open its `FlockScreen`, click **X** to delete it, or add a new flock. New flocks use world wrap and an 80×16×32 cell-space partition. |
| `FlockScreen` | Controls for one flock: rename it, add or remove boids with **+**/**−**, and buttons to open **Manage Flock Groups**, **Behaviors**, and **Boids**. |
| `BehaviorsScreen` | Lists the flock's steering behaviors, each with an editable weight and a remove button. **Add Behavior** opens `AddBehaviorMessageBox`, which offers only the behaviors the flock doesn't have yet. |
| `BoidsScreen` | Number fields for boid parameters: radius, mass, min, walk and max speed, laziness, max turn rate, max force, the neighbor, predator, prey, VIP and wall query radii, and retarget time. |
| `FlockGroupsMessageBox` | A grid of every other flock with Predator / Prey / VIP checkboxes, used to set how the current flock sees each of them. Changes are applied when you press OK. |

All editing screens derive from `BaseTab`, which provides the right-hand tool column and helpers for headers, buttons, and spacing.

## Controls

- `FlockControl`: a flock row with a name button and a delete button, used by `FlocksScreen`.
- `BehaviorControl`: a behavior row with a name, a weight field, and a delete button, used by `BehaviorsScreen`.
- `FlockGroupControl`: one row of the flock-groups grid, or its header row.
- `WallsDropdown`: a dropdown of `DefaultWalls` values that applies the selected walls to a flock using `Resolution.ScreenArea` from ResolutionBuddy. No built-in screen uses it; add it to your own screen if you need it.

## Layout assumptions

The widgets are laid out for a **1280×720** virtual resolution:

- The tool column is placed at `(910, 72)` and is about 360px wide.
- New boids spawn at random positions inside `0–1280 × 0–720`.

If your game uses a different virtual resolution, you may need to subclass the screens or change these values.

## Building

```
dotnet build FlockBuddyWidgets.sln
```

The project sets `GeneratePackageOnBuild`, so building also produces a `.nupkg` and a `.snupkg` in `bin/`.

`FlockBuddyWidgets/build.sh` is the maintainer's release script. It builds in Release, copies the packages to `~/Documents/Source/Nugets`, and pushes to NuGet.org using `$NUGET_API_KEY`.

## License

MIT
