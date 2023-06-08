using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBallStatManager : MonoBehaviour
{
    // 8/06/23 - NEED TO IMPROVE 
    public static AbstractBallStatManager Instance { get; private set; }
    protected float m_totalAttackSpeedFromItems;
    protected float m_totalMaxShootForceFromItems;
    protected float m_totalAccuracyFromItems;
    protected virtual void Awake() {
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

    public abstract void ModifyAttackSpeed(float newAttackSpeed);
    public abstract void ModifyMaxShootPower(float newMaxShootPower);
    public abstract void ModifyAccuracy(float newAccuracy);
    public abstract void CalculateTotalStatsFromItems();
}
