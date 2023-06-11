using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken and modified from https://youtu.be/vmKxLibGrMo
public class EnemyManager : MonoBehaviour
{
    // 7/06/23 - NEED TO IMPROVE 
    // - NEED AN ENEMY CLASS
    // - WAVE MANAGER
    // - ENEMY STATS
    // - NEED TO ADD ENEMY POOLING
    public static EnemyManager Instance { get; private set; }
    public GameObject[] uniqueEnemiesToSpawn;
    public List<GameObject> currentEnemiesAlive;
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
        if (uniqueEnemiesToSpawn.Length <= 0) {
            Debug.LogWarning("No enemies have been assgined to EnemyManager!");
        }
    }

    public void SpawnRandomEnemy(Vector3 position) {
        // SPAWN RANDOM ENEMIES FROM ARRAY, CURRENTLY USING A PREFAB
        for (int i = 0; i < numberOfEnemiesToSpawn; i++) {
        currentEnemiesAlive.Add(Instantiate(uniqueEnemiesToSpawn[Random.Range(0,uniqueEnemiesToSpawn.Length)], position, Quaternion.identity));
        }
    }

    public void ClearAllEnemies() {
        // REALLY NEED TO ADD ENEMY POOLING
        foreach (GameObject enemy in currentEnemiesAlive) {
            if (!System.Object.ReferenceEquals(enemy, null)) {
                Destroy(enemy);
            }
        }
        currentEnemiesAlive.Clear();
    }

    public int CheckEnemiesAlive() {
        int enemiesAlive = 0;
        foreach (GameObject enemy in currentEnemiesAlive) {
            if (enemy.activeInHierarchy == true) {
                enemiesAlive++;
            }
        }

        if (enemiesAlive == 0) {
            Debug.Log("All enemies sunk!");
            GameManager.Instance.UpdateGameState(StateType.SHOP_MENU);
        }
        
        return enemiesAlive;
    }

    public void SetNumberOfEnemiesToSpawn(int amount) {
        numberOfEnemiesToSpawn = amount;
    }
}
