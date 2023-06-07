using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int m_playerLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        TextManager.Instance.UpdateLivesTextAmount(m_playerLives);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreasePlayerLives(int amount) {
        if (amount > m_playerLives) {
            m_playerLives = 0;
        } else {
            m_playerLives = m_playerLives - amount;
            TextManager.Instance.UpdateLivesTextAmount(m_playerLives);
        }
    }

    public void IncreasePlayerLives(int amount) {
        m_playerLives = m_playerLives + amount;
        TextManager.Instance.UpdateLivesTextAmount(m_playerLives);
    }

    public void SetPlayerLives(int amount) {
        m_playerLives = amount;
    }

    public int GetPlayerLives() {
        return m_playerLives;
    }
}
