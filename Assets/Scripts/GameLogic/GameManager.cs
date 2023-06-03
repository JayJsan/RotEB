using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private StateType m_currentGameState = StateType.DEFAULT;
    
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
                m_currentGameState = StateType.PLAYERRESPAWN;
            break;

            case StateType.PLAYERRESPAWN:
                PlayerManager.Instance.DisablePlayerControl();
                PlayerManager.Instance.RespawnPlayer();
                m_currentGameState = StateType.PLAYERTURN;
            break;

            case StateType.PLAYERTURN:
                PlayerManager.Instance.EnablePlayerControl();
            break;
            default:

            break;
        }
    }

    public void SetGameState(StateType state) {
        m_currentGameState = state;
    }
}
