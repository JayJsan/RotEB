using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStatManager : AbstractStatManager
{
    // 8/06/23 - NEED TO IMPROVE 
    public static PlayerStatManager Instance { get; private set; }


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


}
