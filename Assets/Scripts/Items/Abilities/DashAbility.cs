using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Taken and modified from https://youtu.be/ry4I6QyPw4E
[CreateAssetMenu(fileName = "DashAbility", menuName = "RotEB/Abilities/Dash Ability")]
public class DashAbility : AbstractItemAbility
{
    // public string abilityName;
    // public float cooldownTime;
    // public float activeTime;
    public override void Activate(GameObject player) {
        Rigidbody2D playerRB2D = player.GetComponent<Rigidbody2D>();
        
        playerRB2D.AddForce(playerRB2D.velocity.normalized * 20f, ForceMode2D.Impulse);
        Debug.Log("Dash Ability Activated!");

    }
}
