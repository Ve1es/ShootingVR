# ShootingVR
 
A multiplayer duel on a map. The maximum number of players is 2. Players connect to a session and are divided into two teams: red and blue. Before the match starts, there is a 30-second warm-up, followed by a 5-minute game match. At the end of the match, the winning team is displayed (the team with the most kills). The game is played exclusively with controllers.

## Mechanics
- Item Belt.
The player has a "belt" around their waist, holding a magazine with bullets and a pistol. The player can grab the pistol and bullets from the belt with either hand. If the player drops the pistol, it reappears on the belt.

- Shooting.
The player can only shoot if they have a loaded pistol in hand. Shooting is done using the front trigger. Each bullet deals 10 HP damage.

- Reloading.
The player can press X or A at any time to drop the magazine from the pistol. When the magazine is absent, the player can take a new one from the belt and insert it into the pistol (using Snap interactions).

- Movement and Turning.
Movement is controlled with the left joystick. Turning is controlled with the right joystick.

- Health.
The player has 100 HP, which does not regenerate. When HP reaches 0, the player dies.

- Death.
The player is sent back to their base.

## Multiplayer
- Connection.
Players join a session, which is in Shared mode (server-client architecture).

- Disconnection.
If a player leaves the game, the session ends with the remaining player as the winner.
