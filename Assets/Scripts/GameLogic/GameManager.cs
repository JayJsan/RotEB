using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 7/06/23 - NEED TO IMPROVE 
    public static GameManager Instance { get; private set; }
    private StateType m_currentGameState = StateType.DEFAULT;
    public GameObject panel;
    private int numberOfEnemiesToSpawn = 1;
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
        
    }

    // Update is called once per frame
    void Update()
    {
    
        // TEMPORARY SLOW DOWN ABILITY IN
        if (Input.GetKey(KeyCode.Space)) {
            DoSlowMotion();
        } else {
            Time.timeScale += (1f / 0.5f) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            panel.SetActive(false);
        }
    }

    public void UpdateGameState(StateType state) {
        m_currentGameState = state;
         switch (state) {
            case StateType.DEFAULT:
                CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
            break;

            case StateType.PLAYER_SUNK:
                UpdateGameState(StateType.PLAYER_RESPAWN);
            break;

            // Initial player respawn
            case StateType.PLAYER_RESPAWN:
                PlayerManager.Instance.RespawnPlayer();
                UpdateGameState(StateType.PLAYER_RESPAWNING);
            break;

            // Player is respawning
            case StateType.PLAYER_RESPAWNING:
                PlayerManager.Instance.DisablePlayerControl();
                if (PlayerManager.Instance.GetPlayerLives() == 0) {
                    GameManager.Instance.UpdateGameState(StateType.GAME_OVER);
                }
            break;

            case StateType.PLAYER_TURN:
                UIManager.Instance.UpdateStatsTextAmount();
                CanvasManager.Instance.SwitchCanvas(CanvasType.GameUI);
                PlayerManager.Instance.EnablePlayerControl();
            break;

            case StateType.MAIN_MENU:
                CanvasManager.Instance.SwitchCanvas(CanvasType.MainMenu);
            break;

            case StateType.GAME_OVER:
                PlayerManager.Instance.DeactivatePlayer();
                CanvasManager.Instance.SwitchCanvas(CanvasType.EndScreen);
            break;

            case StateType.GAME_WIN:
                PlayerManager.Instance.DisablePlayerControl();
                CanvasManager.Instance.SwitchCanvas(CanvasType.WinScreen);
            break;

            case StateType.SHOP_MENU:
                PlayerManager.Instance.DeactivatePlayer();
                CanvasManager.Instance.SwitchCanvas(CanvasType.ShopScreen);
                ShopManager.Instance.RandomizeShop();
            break;


            default:

            break;
        }
    }

    // public void SetGameState(StateType state) {
    //     m_currentGameState = state;
    // }

    public void DoSlowMotion() {
        panel.SetActive(true);
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void StartGame() {
  
    }

    public void RestartGame() {
        PlayerStatManager.Instance.ResetAllStats();
        EnemyManager.Instance.SetNumberOfEnemiesToSpawn(1);
        PlayerStatManager.Instance.SetPlayerLives(3);
        EnemyManager.Instance.ClearAllEnemies();
        EnemyManager.Instance.SpawnRandomEnemy(new Vector3(8,0,0));
        UpdateGameState(StateType.PLAYER_RESPAWN);
    }

    public void ContinueGame() {
        numberOfEnemiesToSpawn *= 2;
        EnemyManager.Instance.ClearAllEnemies();
        EnemyManager.Instance.SetNumberOfEnemiesToSpawn(numberOfEnemiesToSpawn);
        EnemyManager.Instance.SpawnRandomEnemy(new Vector3(8,0,0));
        PlayerManager.Instance.ReactivatePlayer();
    }
    
    public bool IsGameStateThis(StateType stateType) {
        return stateType == m_currentGameState;
    }
}
