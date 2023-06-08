using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbstractBallStats
{
    #region CURRENT STATS
    protected float m_currentAttackSpeed = 1f; // The rate the ball can be shot per second.
    protected float m_currentMaxShootPower = 3f; // The maximum force the ball will can be shot with 
    protected float m_currentAccuracy = 100f; // The accuracy of the shot relative to a straight shot. 100% = Perfect straight line. -- MIGHT NOT BE FINAL
    #endregion

    #region CURRENT STATS
    protected float m_baseAttackSpeed {get; private set;} = 1f; // The base rate the ball can be shot per second.
    protected float m_baseMaxShootPower {get; private set;} = 3f; // The maximum force the ball will can be shot with 
    protected float m_baseAccuracy {get; private set;} = 100f; // The base accuracy of the shot relative to a straight shot. 100% = Perfect straight line. -- MIGHT NOT BE FINAL
    #endregion

    #region GET CURRENT STATS METHODS

    public virtual float GetCurrentAttackSpeed() {
        return m_currentAttackSpeed;
    }

    public virtual float GetCurrentMaxShootPower() {
        return m_currentMaxShootPower;
    }

    public virtual float GetCurerntAccuracy() {
        return m_currentAccuracy;
    }

    #endregion

    #region MODIFY CURRENT STATS METHODS

    public virtual void SetCurrentAttackSpeed(float newAttackSpeed) {
        m_currentAttackSpeed = newAttackSpeed;
    }

    public virtual void SetCurrentMaxShootPower(float newMaxShootPower) {
        m_currentMaxShootPower = newMaxShootPower;
    }

    public virtual void SetCurrentAccuracy(float newAccuracy) {
        m_currentAccuracy = newAccuracy;
    }

    /// <summary>
    /// Resets the attackSpeed to the base speed
    /// 
    /// </summary>
    public virtual void ResetCurrentAttackSpeed() {
        m_currentAttackSpeed = m_baseAttackSpeed;
    }
    
    public virtual void ResetCurrentMaxShootPower() {
        m_currentMaxShootPower = m_baseMaxShootPower;
    }
    
    public virtual void ResetCurrentAccuracy() {
        m_currentAccuracy = m_baseAccuracy;
    }

    public virtual void ResetStatsToDefault() {
        ResetCurrentAttackSpeed();
        ResetCurrentMaxShootPower();
        ResetCurrentAccuracy();
    }
    #endregion
}
