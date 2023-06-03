using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField]
    private GameObject m_player;
    [SerializeField]
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
        Instance = this;
        
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

    public int GetPlayerLives() { return m_playerStats.GetPlayerLives(); }
    public void DecreasePlayerLives(int amount) { m_playerStats.DecreasePlayerLives(amount); }
    
    public void RespawnPlayer() {
        m_player.transform.position = new Vector3 (-3,0,0);
        ReactivatePlayer();
    }

    public void RespawnPlayer(Vector3 respawnPosition) {
        m_player.transform.position = respawnPosition;
        ReactivatePlayer();
        StartCoroutine(PlayerRespawning());
    }

    public void DeactivatePlayer() {
        m_player.SetActive(false);
        m_player.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void ReactivatePlayer() {
        m_player.SetActive(true);
        m_player.GetComponent<CircleCollider2D>().enabled = true;
    }

    private IEnumerator PlayerRespawning() {
        int currentFlashes = 0;
        int maxFlashes = 3;
        while (currentFlashes < maxFlashes) {
            m_player.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.2f);
            m_player.GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(0.2f);
            currentFlashes++;
        }
        GameManager.Instance.SetGameState(StateType.PLAYERTURN);
    }
}
