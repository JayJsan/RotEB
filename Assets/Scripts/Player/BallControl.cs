using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken and modified from https://youtu.be/-cEFB5prG3Y
public class BallControl : MonoBehaviour
{
    // 7/06/23 - NEED TO IMPROVE 
    // 13/06/23 - MIGRATE INTO playerinput.cs
    public Material material;
    public float startWidth = 0.2f;
    public float endWidth = 0.0f;
    public Color startColor = Color.white;
    public Color endColor = Color.clear;

    private LineRenderer m_playerLineRenderer;
    [SerializeField]
    private LineRenderer m_trajectoryLineRenderer;
    private Rigidbody2D m_rb2D;
    private Vector3 m_initialMousePosition;
    private bool m_isEnabled = true;
    private bool m_inCooldown = false;
    bool buttonClicked = false;
    bool buttonDown = false;

    // Start is called before the first frame update
    void Awake() {
        m_playerLineRenderer = GetComponent<LineRenderer>();

        LineRenderer[] lineRenderers = GetComponentsInChildren<LineRenderer>(true);

        foreach (LineRenderer lr in lineRenderers) {
            if (lr.CompareTag("TrajectoryLine")) {
                m_trajectoryLineRenderer = lr;
            }
        }
        if (m_trajectoryLineRenderer == null) {
            Debug.LogWarning("BallControl could not find the TrajectoryLine component!");
        }
        
    }

    void Start()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_playerLineRenderer = GetComponent<LineRenderer>();

        if (m_rb2D == null) {
            m_rb2D = gameObject.AddComponent<Rigidbody2D>();
        }
        if (m_playerLineRenderer == null) {
            m_playerLineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        if (m_trajectoryLineRenderer == null) {
            m_trajectoryLineRenderer = GameObject.FindWithTag("TrajectoryLine").GetComponent<LineRenderer>();
        }

        SetupPlayerLineControl();
        SetupGuideLine();
    }
    
    private void OnEnable() {
        m_inCooldown = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isEnabled && !m_inCooldown) {
            HandleBallInput();
        }
    }
            

    private void SetupPlayerLineControl() {
        m_playerLineRenderer.enabled = false;
        m_playerLineRenderer.positionCount = 2;
        m_playerLineRenderer.material = material;

        m_playerLineRenderer.startWidth = startWidth;
        m_playerLineRenderer.endWidth = endWidth;

        m_playerLineRenderer.startColor = startColor;
        m_playerLineRenderer.endColor = endColor;
        m_playerLineRenderer.numCapVertices = 20;
    }

    private void SetupGuideLine() {
        m_trajectoryLineRenderer.enabled = false;
        m_trajectoryLineRenderer.positionCount = 2;
        m_trajectoryLineRenderer.material = material;

        m_trajectoryLineRenderer.startWidth = 0.2f;
        m_trajectoryLineRenderer.endWidth = 0f;

        m_trajectoryLineRenderer.startColor = Color.white;
        m_trajectoryLineRenderer.endColor = Color.white;
        m_trajectoryLineRenderer.numCapVertices = 20;
    }

    private void HandleBallInput() {
        // Ensures that the other statements are only executed if the first if statement is executed

        if (Input.GetMouseButtonDown(0)) {
            // ---- Player Control Line -----
            m_initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_initialMousePosition.z = 0;

            Vector3 startPos = m_initialMousePosition + Vector3.forward;
            m_playerLineRenderer.SetPosition(0, startPos);
            m_playerLineRenderer.SetPosition(1, startPos);
            m_playerLineRenderer.enabled = true; 
            // ---- Player Control Line -----

            // ----     Guide Line      -----
            m_trajectoryLineRenderer.SetPosition(0, transform.position);
            m_trajectoryLineRenderer.SetPosition(1, transform.position);
            m_trajectoryLineRenderer.enabled = true;
            // ----     Guide Line      -----

            buttonClicked = true;
        }

        if (Input.GetMouseButton(0) && buttonClicked) {
            // ---- Player Control Line -----
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0;
            Vector3 endPos = currentMousePosition + Vector3.forward;
            m_playerLineRenderer.SetPosition(1, endPos);
            // ---- Player Control Line -----

            // ----     Guide Line      -----
            Vector3 trajectoryDirection = -(currentMousePosition - m_initialMousePosition);
            Vector3 trajectoryEndPos = transform.position + trajectoryDirection;
            // m_trajectoryLineRenderer.SetPosition(0, transform.position);
            // m_trajectoryLineRenderer.SetPosition(1, trajectoryDirection * 2);
            m_trajectoryLineRenderer.SetPosition(0, transform.position);
            m_trajectoryLineRenderer.SetPosition(1, trajectoryEndPos);

            // ----     Guide Line      -----
            buttonDown = true;
        } else {
            buttonClicked = false;
        }

        if (Input.GetMouseButtonUp(0) && buttonDown) {
            // ---- Player Control Line -----
            m_playerLineRenderer.enabled = false;
            Vector3 inputForce = m_playerLineRenderer.GetPosition(0) - m_playerLineRenderer.GetPosition(1);
            m_rb2D.velocity = Vector2.zero;
            Vector3 playerShootPower = inputForce.normalized * 2f * Mathf.Clamp(inputForce.magnitude, 0, 
                PlayerStatManager.Instance.GetCurrentMaxShootPower());

            m_rb2D.AddForce(playerShootPower, ForceMode2D.Impulse);

            // ---- Player Control Line -----
            Debug.Log("Force Applied: " + inputForce);
            // ----     Guide Line      -----
            m_trajectoryLineRenderer.enabled = false;
            // ----     Guide Line      -----
            buttonDown = false;
            buttonClicked = false;
            StartCoroutine(Reload());
        } 
    }
    public void EnableBallControls() { 
        m_isEnabled = true; 
    }

    public IEnumerator Reload() {
        m_inCooldown = true;
        SpriteRenderer playerSprite = PlayerManager.Instance.GetPlayerGameObject().GetComponent<SpriteRenderer>();
        // Since attack speed is attacks per second. Seconds to wait is 1 / attacks per second
        float secondsToWait = 1f / PlayerStatManager.Instance.GetCurrentAttackSpeed();

        playerSprite.color = Color.black;
        yield return new WaitForSeconds(secondsToWait / 3);
        playerSprite.color = Color.gray;
        yield return new WaitForSeconds(secondsToWait / 3);
        playerSprite.color = new Color(0.75f, 0.75f, 0.75f, 1);
        yield return new WaitForSeconds(secondsToWait / 3);
        playerSprite.color = Color.white;
        m_inCooldown = false;
    }

    public void DisableBallControls() { 
        m_isEnabled = false; 
        m_playerLineRenderer.enabled = false;
        m_trajectoryLineRenderer.enabled = false;        
    }

}
