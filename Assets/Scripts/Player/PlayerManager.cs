using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    private GameObject m_player;
    private PlayerStats m_playerStats;

    private void Awake() {
    // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }   

    // Start is called before the first frame update
    void Start()
    {
        if (!System.Object.ReferenceEquals(GameObject.FindGameObjectWithTag("Player"), null)) {
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_playerStats = m_player.GetComponent<PlayerStats>();
        } else {
            Debug.Log("Player Missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreasePlayerLives(int amount) { m_playerStats.DecreasePlayerLives(amount); }
}
