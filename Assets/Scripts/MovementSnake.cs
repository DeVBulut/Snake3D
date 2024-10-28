using System.Collections.Generic;
using UnityEngine;

public class MovementSnake : MonoBehaviour
{
    public float gridSize = 1f;
    public float tailGap = 3f;
    public Transform snakeHead;
    public Transform tailPrefab;
    public List<Transform> tail = new List<Transform>();
    private Vector3 direction = Vector3.forward;
    private Vector3 previousPosition;
    private float moveTimer;
    public float moveInterval = 0.2f;

    void Start()
    {
        tail.Add(snakeHead);
    }

    void Update()
    {
        HandleInput();
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0f;
            MoveSnake();
        }
        HandleTestInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.back)
        {
            direction = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.forward)
        {
            direction = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.right)
        {
            direction = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.left)
        {
            direction = Vector3.right;
        }
    }

    void HandleTestInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Grow();
        }
    }

    void MoveSnake()
    {
        previousPosition = snakeHead.position;
        snakeHead.position += direction * gridSize;
        UpdateTail();
    }

    void UpdateTail()
    {
        Vector3 lastPosition = previousPosition;
        for (int i = 1; i < tail.Count; i++)
        {
            Vector3 temp = tail[i].position;
            tail[i].position = lastPosition;
            lastPosition = temp;
        }
    }

    public void Grow()
    {
        Transform newTailSegment = Instantiate(tailPrefab);
        Vector3 tailPosition = tail[tail.Count - 1].position - (direction * tailGap);
        newTailSegment.position = tailPosition;
        tail.Add(newTailSegment);
    }
}