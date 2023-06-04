using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject m_closestPocket;
    private Rigidbody2D rb2D;
    private CircleCollider2D cc2D;
    [SerializeField]
    private ContactFilter2D contactPredictionFilter;

    public Vector3 angleAdjustment = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            GetAngleToHitPlayer();
        }
    }

    private void FixedUpdate() {
        m_closestPocket = PlayerManager.Instance.GetNearestPocketToPlayer();
    }

    private void GetAngleToHitPlayer() {
        Transform playerTransform = PlayerManager.Instance.GetPlayerTransform();
        Transform closestPocketTransform = m_closestPocket.transform;



        Vector2 directionToHitPlayerIntoPocket = closestPocketTransform.position - playerTransform.position;
        Vector2 directionTowardsPlayerBall = playerTransform.position - transform.position + angleAdjustment;

        RaycastHit2D[] hits = {new RaycastHit2D()};
        //ContactFilter2D filter = new ContactFilter2D().NoFilter();
        int numberOfHits = Physics2D.CircleCast(transform.position, cc2D.radius, directionTowardsPlayerBall, contactPredictionFilter, hits, 20f);

        // Line from ball to player ball
        Debug.DrawLine(transform.position, playerTransform.position + angleAdjustment, Color.green, 2f);

        if (numberOfHits >= 1) {
            
            // Normal of point of contact from playerball.
            Debug.DrawLine(hits[0].point, hits[0].point - hits[0].normal * 8, Color.cyan, 2f);

            // Line from playerBall to pocket
            Debug.DrawLine(playerTransform.position, closestPocketTransform.position, Color.magenta, 2f);

            foreach (RaycastHit2D hit in hits) {
                Debug.Log(hit.collider.name + " has been hit by the raycast!");
            }
        }
    }
}
