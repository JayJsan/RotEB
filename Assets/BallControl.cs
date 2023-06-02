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

    private LineRenderer _lineRenderer;
    [SerializeField]
    private LineRenderer _guideLine;
    private Rigidbody2D _rb2D;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();

        if (_rb2D == null) {
            _rb2D = gameObject.AddComponent<Rigidbody2D>();
        }
        if (_lineRenderer == null) {
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        _lineRenderer.enabled = false;
        _lineRenderer.positionCount = 2;
        _lineRenderer.material = material;

        _lineRenderer.startWidth = startWidth;
        _lineRenderer.endWidth = endWidth;

        _lineRenderer.startColor = startColor;
        _lineRenderer.endColor = endColor;
        _lineRenderer.numCapVertices = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward;
            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, startPos);
            _lineRenderer.enabled = true; 
        }

        if (Input.GetMouseButton(0)) {
            Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward;
            _lineRenderer.SetPosition(1, endPos);
        }

        if (Input.GetMouseButtonUp(0)) {
            _lineRenderer.enabled = false;

            Vector3 inputForce = _lineRenderer.GetPosition(0) - _lineRenderer.GetPosition(1);
            _rb2D.AddForce(inputForce, ForceMode2D.Impulse);
        }
    }
}
