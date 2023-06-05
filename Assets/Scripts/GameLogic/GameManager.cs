using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private StateType m_currentGameState = StateType.DEFAULT;
    public GameObject panel;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_currentGameState) {
            case StateType.DEFAULT:

            break;

            case StateType.PLAYERSUNK:
                SetGameState(StateType.PLAYERRESPAWN);
            break;

            // Initial player respawn
            case StateType.PLAYERRESPAWN:
                PlayerManager.Instance.DisablePlayerControl();
                PlayerManager.Instance.RespawnPlayer();
                SetGameState(StateType.PLAYERRESPAWNING);
            break;

            // Player is respawning
            case StateType.PLAYERRESPAWNING:
                PlayerManager.Instance.DisablePlayerControl();
            break;

            case StateType.PLAYERTURN:
                PlayerManager.Instance.EnablePlayerControl();
            break;
            default:

            break;
        }




        if (Input.GetKey(KeyCode.Space)) {
            DoSlowMotion();
        } else {
            Time.timeScale += (1f / 0.5f) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            panel.SetActive(false);
        }
    }

    public void SetGameState(StateType state) {
        m_currentGameState = state;
    }

    public void DoSlowMotion() {
        panel.SetActive(true);
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
