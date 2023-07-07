using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private GameObject m_playerPrefab;
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
        DontDestroyOnLoad(gameObject);
    }   

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_player = Instantiate(m_playerPrefab, Vector3.zero, Quaternion.identity);
        if (!System.Object.ReferenceEquals(GameObject.FindGameObjectWithTag("Player"), null)) {
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_playerStats = m_player.GetComponent<PlayerStats>();
            m_playerControl = m_player.GetComponent<BallControl>();
        } else {
            Debug.Log("Player Missing!");
        }
        m_player.SetActive(false);
        m_pockets = GameObject.FindGameObjectsWithTag("Pocket");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region LIVES AND RESPAWN
    public int GetPlayerLives() { return m_playerStats.GetPlayerLives(); }

    public void RespawnPlayer() {
        m_player.transform.position = new Vector3 (-3,0,0);
        m_player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ReactivatePlayer();
        StartCoroutine(PlayerRespawning());
    }
    
    public void RespawnPlayer(Vector3 respawnPosition) {
        m_player.transform.position = respawnPosition;
        ReactivatePlayer();
        StartCoroutine(PlayerRespawning());
    }

    private IEnumerator PlayerRespawning() {
        PlayerManager.Instance.DisablePlayerControl();
        int currentFlashes = 0;
        int maxFlashes = 4;
        string livesText = "Respawning";

        m_player.GetComponent<CircleCollider2D>().enabled = false;
        while (currentFlashes < maxFlashes) {
            UIManager.Instance.UpdateLivesTextStatus(livesText);
            livesText = livesText + ".";
            m_player.GetComponent<SpriteRenderer>().color = Color.clear;
            yield return new WaitForSeconds(0.2f);
            m_player.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.2f);
            currentFlashes++;
        }
        m_player.GetComponent<CircleCollider2D>().enabled = true;
        UIManager.Instance.UpdateLivesTextAmount();

        // FIX THIS -- 17/06/23 -- DO NOT UPDATE GAME STATE IN ENUMRATOR OR ATLEAST CHECK IF CURRENT STATE IS NOT GAMEPLAY

        if (PlayerManager.Instance.GetPlayerLives() == 0) {
            GameManager.Instance.UpdateGameState(StateType.GAME_OVER);
        } else {
            GameManager.Instance.UpdateGameState(StateType.PLAYER_TURN);
        }
    }
    #endregion

    #region PLAYER INPUT
    public void EnablePlayerControl() { m_playerControl.EnableBallControls(); }
    public void DisablePlayerControl() { m_playerControl.DisableBallControls(); } 
    #endregion

    public void DeactivatePlayer() {
        m_player.SetActive(false);
        m_player.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void ReactivatePlayer() {
        m_player.SetActive(true);
        m_player.GetComponent<CircleCollider2D>().enabled = true;
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

    public Transform GetPlayerTransform() {
        return m_player.transform;
    }

    public GameObject GetPlayerGameObject() {
        return m_player;
    }
}
