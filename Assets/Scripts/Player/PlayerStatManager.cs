using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : AbstractStatManager
{
    // 8/06/23 - NEED TO IMPROVE 
    public static PlayerStatManager Instance { get; set; }
    private PlayerStats m_playerStats;

    protected override void Awake() {
    // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        DontDestroyOnLoad(gameObject);
    }   

    void Start() {
        m_playerStats = PlayerManager.Instance.GetPlayerGameObject().GetComponent<PlayerStats>();
    }

    
    public override void ModifyAttackSpeed(float newAttackSpeed) {
        m_playerStats.SetCurrentAttackSpeed(newAttackSpeed);
    }

    public override void ModifyMaxShootPower(float newMaxShootPower) {
        m_playerStats.SetCurrentMaxShootPower(newMaxShootPower);
    }

    public override void ModifyAccuracy(float newAccuracy) {
        m_playerStats.SetCurrentAccuracy(newAccuracy);
    }

    public override void CalculateTotalStatsFromItems() {
        // CHANGE WHEN ITEMS ARE IMPLEMENTED
        m_totalAttackSpeedFromItems = 2;
        m_totalMaxShootForceFromItems = 200;
        m_totalAccuracyFromItems = 100;
    }

    public void UpdateAllStats() {
        CalculateTotalStatsFromItems();
        ModifyAttackSpeed(m_totalAttackSpeedFromItems);
        ModifyMaxShootPower(m_totalMaxShootForceFromItems);
        ModifyAccuracy(m_totalAccuracyFromItems);
    }

    public void DecreasePlayerLives(int amount) {
        int playerLives = m_playerStats.GetPlayerLives();
        if (amount >= playerLives) {
            playerLives = 0;
        } else {
        playerLives = playerLives - amount;
        }
        m_playerStats.SetPlayerLives(playerLives);
    }

    public void IncreasePlayerLives(int amount) {
        int playerLives = m_playerStats.GetPlayerLives();
        playerLives = playerLives + amount;
        m_playerStats.SetPlayerLives(playerLives);
    }

    public int GetPlayerLives() {
        return m_playerStats.GetPlayerLives();
    }

    public void SetPlayerLives(int amount) {
        m_playerStats.SetPlayerLives(amount);
    }

    public float GetCurrentAttackSpeed() {
        return m_playerStats.GetCurrentAttackSpeed();
    }
    public float GetCurrentMaxShootPower() {
        return m_playerStats.GetCurrentMaxShootPower();
    }
     public float GetCurrentAccuracy() {
        return m_playerStats.GetCurrentAccuracy();
    }
}
