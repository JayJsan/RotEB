using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : AbstractBallStats
{
    private int m_playerLives = 10;

    public void SetPlayerLives(int amount) {
        m_playerLives = amount;
    }

    public int GetPlayerLives() {
        return m_playerLives;
    }
}
