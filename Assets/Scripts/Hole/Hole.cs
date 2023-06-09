using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private CircleCollider2D m_circleCollider2D;
    private bool m_hasEntered = false;
    // Start is called before the first frame update
    void Start()
    {
        m_circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        GameObject collidedObject = collider.gameObject;
        collider.enabled = false;
        

        if (!m_hasEntered) {
            Debug.Log(collidedObject.name + " has fallened into the " + this.name +"!");
            switch (collidedObject.tag) {
                case "Player":
                    SunkPlayer();
                break;
                case "Enemy":
                    SunkEnemy(collidedObject);
                break;
                default:
                    Debug.Log("wtf did you sink?");
                break;
            }
            m_circleCollider2D.enabled = true;
            m_hasEntered = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider) {
        m_hasEntered = false;
        m_circleCollider2D.enabled = true;
    }

    private void SunkPlayer() {
        Debug.Log("Player sunk!");
        PlayerStatManager.Instance.DecreasePlayerLives(1);
        PlayerManager.Instance.DeactivatePlayer();
        GameManager.Instance.UpdateGameState(StateType.PLAYER_SUNK);
    }

    private void SunkEnemy(GameObject enemy) {
        Debug.Log("Enemy sunk!");
        enemy.SetActive(false);
        EnemyManager.Instance.CheckEnemiesAlive();
    }
}
