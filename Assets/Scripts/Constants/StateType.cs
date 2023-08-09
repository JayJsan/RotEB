using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType {
    DEFAULT,      // Fall-back state, should never happen
    PLAYER_TURN,   // Cooldown has finished, time is frozen, player can move
    PLAYER_WAIT,   // Time is unfrozen, Waiting for cooldown to finish or for ball to stop
    PLAYER_RESPAWN,// Player needs to respawn.
    PLAYER_RESPAWNING,// Player is respawning.
    PLAYER_SUNK,   // Player has been sunk.
    PLAYER_COOLDOWN, // Player has taken a shot.
    GAME_OVER,
    GAME_WIN,
    GAME_START,
    SHOP_MENU,       // Player is viewing shop menu
    PAUSE_MENU,     // Player is viewing in-game menu
    MAIN_MENU       // SCENE IS IN MAIN MENU
}

