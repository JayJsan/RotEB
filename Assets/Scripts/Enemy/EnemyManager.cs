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
    public static EnemyManager Instance { get; private set; }
    public GameObject[] uniqueEnemiesToSpawn;

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
        Instantiate(uniqueEnemiesToSpawn[Random.Range(0,uniqueEnemiesToSpawn.Length)], position, Quaternion.identity);
    }
}
