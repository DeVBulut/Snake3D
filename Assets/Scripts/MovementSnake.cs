using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementSnake : MonoBehaviour
{   [Header("Grid and Movement Variables")]
    // Movement Settings
    [Tooltip("Grid size for each movement step.")]
    public float gridSize = 1f;
    [Tooltip("Time interval between each movement.")]
    public float moveInterval = 0.13f;
    [Tooltip("Time between destroying each tail segment when dying.")]
    public float timeBetweenDeath = 0.15f;

    [Header("Prefabs")]
    // Snake Components
    [Tooltip("Reference to the snake head transform.")]
    public Transform snakeHead;
    [Tooltip("Prefab for the tail segments.")]
    public Transform tailPrefab;

    [Header("References")]
    [Tooltip("Reference to the GameManager script.")]
    public GameManager gameManager;

    // Runtime Variables
    [Tooltip("List of tail segments including the head.")]
    public List<Transform> tail = new List<Transform>();

    private Vector3 direction = Vector3.forward;
    private Vector3 previousPosition;
    private float moveTimer;
    public bool dead;
    public GameObject gameOverUI;

    void Start()
    {
        dead = false;
        tail.Add(snakeHead);
        Grow();
        Grow();
        Grow();
    }

    void Update()
    {
        if (gameManager != null && gameManager.runGame)
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
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.back)
        {
            direction = Vector3.forward;
            MoveSnake();
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.forward)
        {
            direction = Vector3.back;
            MoveSnake();
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.right)
        {
            direction = Vector3.left;
            MoveSnake();
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.left)
        {
            direction = Vector3.right;
            MoveSnake();
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
        Vector3 tailPosition = tail[tail.Count - 1].position - direction * gridSize;
        newTailSegment.position = tailPosition;
        newTailSegment.SetParent(transform);
        tail.Add(newTailSegment);

        // Disable collision temporarily to avoid immediate collision with the head
        Collider newSegmentCollider = newTailSegment.GetComponent<Collider>();
        if (newSegmentCollider != null)
        {
            newSegmentCollider.enabled = false;
            StartCoroutine(EnableColliderAfterDelay(newSegmentCollider, 0.5f));
        }
    }

    private System.Collections.IEnumerator EnableColliderAfterDelay(Collider collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }

    public void Die()
    {
        if (!dead)
        {
            dead = true;
            moveInterval = 99999999;
            StartCoroutine(GameOverSequence());
            StartCoroutine(DestroyTailCoroutine());
            DestroyAllBuildings();
        }
    }
    private void DestroyAllBuildings()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject building in buildings)
        {
            Destroy(building);
        }
    }
     private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(1);
        gameOverUI.SetActive(true);
    }

    private System.Collections.IEnumerator DestroyTailCoroutine()
    {
        for (int i = tail.Count - 1; i > 0; i--)
        {
            Destroy(tail[i].gameObject);
            tail.RemoveAt(i);
            yield return new WaitForSeconds(timeBetweenDeath);
        }
    }
}
