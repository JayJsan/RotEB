using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken and modified from https://youtu.be/-cEFB5prG3Y
public class BallControl : MonoBehaviour
{
    public Material material;
    public float startWidth = 0.2f;
    public float endWidth = 0.0f;
    public Color startColor = Color.white;
    public Color endColor = Color.clear;

    private LineRenderer _playerLineRenderer;
    [SerializeField]
    private LineRenderer _trajectoryLineRenderer;
    private Rigidbody2D _rb2D;


    private Vector3 initialMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _playerLineRenderer = GetComponent<LineRenderer>();

        if (_rb2D == null) {
            _rb2D = gameObject.AddComponent<Rigidbody2D>();
        }
        if (_playerLineRenderer == null) {
            _playerLineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        if (_trajectoryLineRenderer == null) {
            _trajectoryLineRenderer = GameObject.FindWithTag("TrajectoryLine").GetComponent<LineRenderer>();
        }

        SetupPlayerLineControl();
        SetupGuideLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            // ---- Player Control Line -----
            initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            initialMousePosition.z = 0;

            Vector3 startPos = initialMousePosition + Vector3.forward;
            _playerLineRenderer.SetPosition(0, startPos);
            _playerLineRenderer.SetPosition(1, startPos);
            _playerLineRenderer.enabled = true; 
            // ---- Player Control Line -----

            // ----     Guide Line      -----
            _trajectoryLineRenderer.SetPosition(0, transform.position);
            _trajectoryLineRenderer.SetPosition(1, transform.position);
            _trajectoryLineRenderer.enabled = true;
            // ----     Guide Line      -----

            
        }

        if (Input.GetMouseButton(0)) {
            // ---- Player Control Line -----
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0;
            Vector3 endPos = currentMousePosition + Vector3.forward;
            _playerLineRenderer.SetPosition(1, endPos);
            // ---- Player Control Line -----

            // ----     Guide Line      -----
            Vector3 trajectoryDirection = -(currentMousePosition - initialMousePosition);
            Vector3 trajectoryEndPos = transform.position + trajectoryDirection;
            // _trajectoryLineRenderer.SetPosition(0, transform.position);
            // _trajectoryLineRenderer.SetPosition(1, trajectoryDirection * 2);
            _trajectoryLineRenderer.SetPosition(0, transform.position);
            _trajectoryLineRenderer.SetPosition(1, trajectoryEndPos);

            // ----     Guide Line      -----
        }

        if (Input.GetMouseButtonUp(0)) {
            // ---- Player Control Line -----
            _playerLineRenderer.enabled = false;
            Vector3 inputForce = _playerLineRenderer.GetPosition(0) - _playerLineRenderer.GetPosition(1);
            _rb2D.velocity = Vector2.zero;
            _rb2D.AddForce(inputForce, ForceMode2D.Impulse);
            // ---- Player Control Line -----

            // ----     Guide Line      -----
            _trajectoryLineRenderer.enabled = false;
            // ----     Guide Line      -----
        }
    }

    private void SetupPlayerLineControl() {
        _playerLineRenderer.enabled = false;
        _playerLineRenderer.positionCount = 2;
        _playerLineRenderer.material = material;

        _playerLineRenderer.startWidth = startWidth;
        _playerLineRenderer.endWidth = endWidth;

        _playerLineRenderer.startColor = startColor;
        _playerLineRenderer.endColor = endColor;
        _playerLineRenderer.numCapVertices = 20;
    }

    private void SetupGuideLine() {
        _trajectoryLineRenderer.enabled = false;
        _trajectoryLineRenderer.positionCount = 2;
        _trajectoryLineRenderer.material = material;

        _trajectoryLineRenderer.startWidth = 0.2f;
        _trajectoryLineRenderer.endWidth = 0f;

        _trajectoryLineRenderer.startColor = Color.white;
        _trajectoryLineRenderer.endColor = Color.white;
        _trajectoryLineRenderer.numCapVertices = 20;
    }
}
