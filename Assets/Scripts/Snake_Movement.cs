using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float gridSize = 1f;
    public float moveSpeed = 0.2f;
    public Transform segmentPrefab;
    private Vector3 direction = Vector3.forward;
    private float moveTimer;
    private List<Transform> segments = new List<Transform>();
    private Vector3 planeOffset = new Vector3(75, 86.5f, 0);
    private Vector3 lastDirection = Vector3.forward;

    void Start()
    {
        segments.Add(this.transform);
        transform.position = planeOffset;
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;
        if (moveTimer >= moveSpeed)
        {
            moveTimer = 0;
            Move();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && lastDirection != Vector3.right)
            direction = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.S) && lastDirection != Vector3.left)
            direction = Vector3.right;
        else if (Input.GetKeyDown(KeyCode.A) && lastDirection != Vector3.forward)
            direction = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.D) && lastDirection != Vector3.back)
            direction = Vector3.forward;
    }

    void Move()
    {
        Vector3 nextPosition = transform.position + direction * gridSize;
        nextPosition.x = Mathf.Clamp(nextPosition.x, planeOffset.x - 10, planeOffset.x + 10);
        nextPosition.z = Mathf.Clamp(nextPosition.z, planeOffset.z - 10, planeOffset.z + 10);
        nextPosition.y = planeOffset.y;

        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = nextPosition;
        lastDirection = direction;
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
}

