# 2D Platformer: Feature Update

A side-scrolling 2D platformer built in Unity 6 (6000.5.10f1). The player runs through a level full of enemies, collects coins, crosses water using stepping stones and fights a boss at the end. This assignment started from an existing platformer project with missing logic and bugs. My job was to fix the movement and camera, build the UI and add a game manager that handles respawning.

## How to run

1. Open the project in Unity 6000.5.10f1 or later.
2. Open `Assets/Scenes/StartScene.unity`.
3. Press Play.

The build order is `StartScene` → `GameScene-ALU` → `EndScene`.

## Controls

| Action | Key |
|---|---|
| Move left / right | A / D or ← / → |
| Jump | Space |
| Shoot | J |

## Game flow

1. **Start screen**: Play, Settings and Quit. Settings opens a panel with a volume slider, a mute toggle and a Back button.
2. **Level**: you start with 3 lives and a 3-minute timer. Lives and coins are shown top-left, and the timer top-right.
3. **End screen**: shows one of three results, each with Replay and Quit buttons:
   - **You Win!** after defeating the boss
   - **Game Over** after losing all lives
   - **Time's Up!** when the timer reaches 00:00

## What I did

### Design

- **HUD**: the life and coin counters sit next to their icons, anchored with the top-left preset. The timer is a TextMeshPro text anchored top-right with a small offset on both axes (-20, -20).
- **Canvas**: every canvas uses a Canvas Scaler set to *Scale With Screen Size*, so the UI keeps its layout at different resolutions.
- **Start screen**: a title, a highlighted Play button, Settings and Quit buttons, and a settings panel. The main buttons are hidden while settings are open so the two layers don't overlap.
- **End scene**: I duplicated the start scene to keep the same look, added a dark overlay to dim the background, and replaced the buttons with Replay and Quit.

### Development

**Camera follow**: the `target` variable in `CameraFollow` stays `private` with no `[SerializeField]`. The script finds the player in `Start()` using `GameObject.FindGameObjectWithTag("Player")`, then follows them horizontally with `SmoothDamp`.

**PlayerMovement**:
- `CheckIfGrounded()` and `PlayerJump()` run every frame in `Update()`.
- Horizontal input uses `Input.GetAxis("Horizontal")`, which covers both A/D and the arrow keys.
- Jumping uses `Input.GetKeyDown(KeyCode.Space)`, so one press gives one jump, and only while the player is grounded.

**Movement bug**:

> ✏️ *TODO: describe the bug that stopped the player from moving, how you found it and how you fixed it.*

**GameManager and respawn**:
- Every water tile has a small `WaterTrigger` script. When the player touches it, it calls `GameManager.PlayerFellInWater()`.
- The GameManager takes away a life using the existing `PlayerDamage.DealDamage()`. If the player still has lives, it respawns them. When lives run out, `PlayerDamage` loads the end scene.
- **Respawn near the water (extra mark)**: `PlayerMovement` records `LastGroundedPosition` on every frame the player is standing on ground. When the player falls in, they reappear on the last piece of ground they stood on, which is the edge of the gap they just fell into, not the start of the level.

**Start game UI**: the Play button loads `GameScene-ALU` through `SceneManager.LoadScene`.

## Additional features

| Feature | How it works |
|---|---|
| Respawn next to the water | Uses the player's last grounded position instead of the level start |
| Working countdown timer | `CountdownTimer` counts down from 03:00, turns red for the last 30 seconds and ends the game at 00:00 |
| Boss fight and win condition | The boss takes 5 hits with a 2-second invulnerability window between them. Defeating it shows a "You Win!" screen |
| Three different endings | `EndMenuController.SetResult()` passes a title, message and colour to the end scene, so one scene covers win, game over and time up |
| Volume and mute settings | Uses `AudioListener.volume`, saved with `PlayerPrefs` so the setting carries into the game scene and the next session |
| More enemies | The level now has 15 enemies (snails, beetles, spiders, frogs, birds and the boss) spread across the whole map |

## Bugs I found and fixed

| Problem | Cause | Fix |
|---|---|---|
| Couldn't jump off the stepping stones or climb the block staircase | Those objects were on the Default layer, and the ground check only looks at the Ground layer | Moved the platforms, blocks and staircase onto the Ground layer |
| 12 coins couldn't be collected | They sat above blocks that were too high to land on | Moved them under the blocks so you collect them while bumping the block |
| End screen buttons didn't respond | A full-screen dark overlay was the last child of the Canvas, so it was drawn on top and caught every click | Moved the overlay to the top of the Canvas hierarchy and turned off Raycast Target |
| Losing all lives caused an error | `PlayerDamage` tried to load a scene called "Gameplay", which doesn't exist | Pointed it at `EndScene` |
| Snails stopped reacting after turning around | `SnailScript` stored its side collision points in world space, so they snapped back to the spawn point | Switched to `localPosition` |
| Beating the boss showed "Game Over" | The end scene's text fields weren't assigned on the controller | Linked the Title and Subtitle texts in the Inspector |

## Key takeaways

- **Layers control gameplay, not just rendering.** Most of my "the player is stuck" problems came from objects on the wrong layer. A platform can have a perfectly good collider and still not count as ground.
- **Hierarchy order matters for UI.** Unity draws UI children from top to bottom, so the last child is on top. That one rule explained both the overlapping settings panel and the buttons I couldn't click.
- **Anchors and the Canvas Scaler.** Setting an anchor preset and *Scale With Screen Size* keeps the HUD in the right corner at any resolution, instead of drifting when the window changes size.
- **Reuse code instead of duplicating it.** The GameManager handles respawning but reuses `PlayerDamage` for the life count. One `EndScene` handles all three endings.
- **Test the whole flow, not just one feature.** Several bugs only showed up when I played from the start menu all the way to the end screen and pressed Replay.
- **Starter code has bugs too.** The snail bug and the missing scene name were already in the project. Reading the existing scripts carefully was as important as writing new ones.

## Challenges

✏️ *TODO: add the parts you personally found hardest and how you worked through them.*

## Possible improvements

- A pause menu (Esc) with Resume and Quit
- A visual flash when the boss or player takes damage
- Show the final coin count and remaining time on the end screen
- Move from the old `Input` class to the new Input System actions that are already in the project

## Project structure

```
Assets/
├── Animations/        animation clips and controllers
├── Prefabs/           player, enemies, collectables
├── Scenes/            StartScene, GameScene-ALU, EndScene
├── Scripts/
│   ├── Boss Scripts/        BossScript, BossHealth, StoneScript
│   ├── Camera Scripts/      CameraFollow
│   ├── Controller Scripts/  GameManager, CountdownTimer, MainMenuController, EndMenuController
│   ├── Enemy Scripts/       Snail, Spider, Frog, Bird, Egg
│   ├── Helper Scripts/      MyTags, WaterTrigger
│   └── Player Scripts/      PlayerMovement, PlayerDamage, PlayerShoot, FireBullet, ScoreManager
├── Sounds/
└── Sprites/
```
