using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    private float movementSpeed = 100;
    Vector3 lastBallVelocity;
     
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(20 * Time.deltaTime * movementSpeed, 20 * Time.deltaTime * movementSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        // lastBallVelocity = rb.velocity;
    }

    // Ball Bounce on Collision
    // private void OnCollisionEnter2D(Collision2D collision) {
    //     var speed = lastBallVelocity.magnitude;
    //     var direction = Vector3.Reflect(lastBallVelocity.normalized, collision.contacts[0].normal);
    //     rb.velocity = direction * Mathf.Max(speed, 0f);
    // }
}
