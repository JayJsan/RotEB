using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType {
    DEFAULT,      // Fall-back state, should never happen
    PLAYERTURN,   // Cooldown has finished, time is frozen, player can move
    PLAYERWAIT,   // Time is unfrozen, Waiting for cooldown to finish or for ball to stop
    PLAYERRESPAWN,// Player needs to respawn.
    PLAYERRESPAWNING,// Player is respawning.
    PLAYERSUNK,   // Player has been sunk.
    BUYING,       // Buying something new
    GAMEOVER,
    GAMESTART,
    PAUSEMENU     // Player is viewing in-game menu
}

