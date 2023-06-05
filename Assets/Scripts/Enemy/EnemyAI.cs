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
    private bool inCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        m_closestPocket = PlayerManager.Instance.GetNearestPocketToPlayer();

        if (!inCooldown) {
            StartCoroutine(Cooldowm());
        }
    }

    private IEnumerator Cooldowm() {
        inCooldown = true;
        GetAngleToHitPlayer();
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        inCooldown = false;
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
        //Debug.DrawLine(transform.position, playerTransform.position + angleAdjustment, Color.green, 2f);

        if (numberOfHits >= 1) {
            
            // Normal of point of contact from playerball.
            //Debug.DrawLine(hits[0].point, hits[0].point - hits[0].normal * 8, Color.cyan, 2f);

            // Line from playerBall to pocket
            //Debug.DrawLine(playerTransform.position, closestPocketTransform.position, Color.magenta, 2f);

            foreach (RaycastHit2D hit in hits) {
                //Debug.Log(hit.collider.name + " has been hit by the raycast!");
            }
        }

        Vector3 angleAdjustmentPrediction = Vector3.zero;
        Vector3 currentPredictionToHitPlayerIntoPocket = playerTransform.position - transform.position + angleAdjustmentPrediction;;
        Vector3 closestDirectionTowardsPlayerBallToHitPlayerIntoPocket = currentPredictionToHitPlayerIntoPocket; 
        float closestPredictionAngle = 9999f; //
        RaycastHit2D[] castHits = {new RaycastHit2D()};

        // change angle of adjustment in x direction 10 times
        // find closest match to directionToHitPlayerIntoPocket
        // --> check normal[0] is closest to directionToHitPlayerIntoPocket
        for (int i = -5; i < 5; i++) {
            angleAdjustmentPrediction.x = i / 10f;
            currentPredictionToHitPlayerIntoPocket = playerTransform.position - transform.position + angleAdjustmentPrediction;
            Physics2D.CircleCast(transform.position, cc2D.radius, currentPredictionToHitPlayerIntoPocket, 
                contactPredictionFilter, castHits, 20f);

            float predictionAngleDiscrepancy = Vector3.Angle(hits[0].normal, currentPredictionToHitPlayerIntoPocket);
            // check if normal is closest to directionToHitPlayerIntoPocket
            // --> i.e. check if angle of normal is closest to currently closest direction
            if (predictionAngleDiscrepancy < closestPredictionAngle) {
                closestPredictionAngle = predictionAngleDiscrepancy;
                closestDirectionTowardsPlayerBallToHitPlayerIntoPocket = currentPredictionToHitPlayerIntoPocket;
            }
            //Debug.DrawLine(castHits[0].point, castHits[0].point - castHits[0].normal * 8, Color.white, 2f);
            // If so, set closestDirectionTowardsPlayerBallToHitPlayerIntoPocket to currentPrediction.
        }

        // change angle of adjustment in y direction 10 times
        // find closest match to directionToHitPlayerIntoPocket
        // --> check normal[0] is closest to directionToHitPlayerIntoPocket

        for (int i = -5; i < 5; i++) {
            angleAdjustmentPrediction.y = i / 10f;
            currentPredictionToHitPlayerIntoPocket = playerTransform.position - transform.position + angleAdjustmentPrediction;
            Physics2D.CircleCast(transform.position, cc2D.radius, currentPredictionToHitPlayerIntoPocket, 
                contactPredictionFilter, castHits, 20f);

            float predictionAngleDiscrepancy = Vector3.Angle(hits[0].normal, currentPredictionToHitPlayerIntoPocket);
            // check if normal is closest to directionToHitPlayerIntoPocket
            // --> i.e. check if angle of normal is closest to currently closest direction
            if (predictionAngleDiscrepancy < closestPredictionAngle) {
                closestPredictionAngle = predictionAngleDiscrepancy;
                closestDirectionTowardsPlayerBallToHitPlayerIntoPocket = currentPredictionToHitPlayerIntoPocket;
            }
            //Debug.DrawLine(castHits[0].point, castHits[0].point - castHits[0].normal * 8, Color.white, 2f);
            // If so, set closestDirectionTowardsPlayerBallToHitPlayerIntoPocket to currentPrediction.
        }
        //Debug.DrawLine(transform.position, closestDirectionTowardsPlayerBallToHitPlayerIntoPocket, Color.yellow, 2f);
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(closestDirectionTowardsPlayerBallToHitPlayerIntoPocket * 3f, ForceMode2D.Impulse);
    }
}
