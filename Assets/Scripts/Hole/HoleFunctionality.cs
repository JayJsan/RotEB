using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleFunctionality : MonoBehaviour
{
    private CircleCollider2D m_circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        GameObject collidedObject = collider.gameObject;
        collider.enabled = false;

        Debug.Log(collidedObject.name + " has fallened into the " + this.name +"!");


        switch (collidedObject.tag) {
            case "Player":
                PlayerManager.Instance.DecreasePlayerLives(1);    
            break;
            case "Enemy":
                Debug.Log("Enemy sunk!");
            break;
            default:
                Debug.Log("wtf did you sink?");
            break;
        }
        collidedObject.SetActive(false);
    }
}
