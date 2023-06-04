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
    [SerializeField]
    private BallControl m_playerControl;
    [SerializeField]
    private GameObject[] m_pockets;
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
            m_playerControl = m_player.GetComponent<BallControl>();
        } else {
            Debug.Log("Player Missing!");
        }

        m_pockets = GameObject.FindGameObjectsWithTag("Pocket");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPlayerLives() { return m_playerStats.GetPlayerLives(); }
    public void EnablePlayerControl() { m_playerControl.EnableBallControls(); }
    public void DisablePlayerControl() { m_playerControl.DisableBallControls(); } 
    public void DecreasePlayerLives(int amount) { 
        m_playerStats.DecreasePlayerLives(amount); 
        if (GetPlayerLives() == 0) {
            GameManager.Instance.SetGameState(StateType.GAMEOVER);
        }
    }
    
    public void RespawnPlayer() {
        m_player.transform.position = new Vector3 (-3,0,0);
        ReactivatePlayer();
        StartCoroutine(PlayerRespawning());
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
        PlayerManager.Instance.DisablePlayerControl();
        int currentFlashes = 0;
        int maxFlashes = 4;
        string livesText = "Respawning";
        while (currentFlashes < maxFlashes) {
            TextManager.Instance.UpdateLivesTextStatus(livesText);
            livesText = livesText + ".";
            m_player.GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(0.2f);
            m_player.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.2f);
            currentFlashes++;
        }
        GameManager.Instance.SetGameState(StateType.PLAYERTURN);
        TextManager.Instance.UpdateLivesTextAmount(GetPlayerLives());
    }

    public GameObject GetNearestPocketToPlayer() {
        float closestDistance = 999999f;
        GameObject closestPocket = m_pockets[0]; // Top left by default, should never happen

        // Finds the smallest distance between two points.
        foreach (GameObject pocket in m_pockets) {
            float pocketDistance = Vector3.Distance(pocket.transform.position, m_player.transform.position);
            if (pocketDistance < closestDistance) {
                closestDistance = pocketDistance;
                closestPocket = pocket;
            }
        }

        //Debug.Log("Closest Pocket to Player: " + closestPocket.name);
        return closestPocket;
    }
}
