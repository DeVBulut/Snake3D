using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Movement : MonoBehaviour
{
    // Settings
    public float MoveSpeed = 5;
    public int Gap = 10;

    // References
    public GameObject BodyPrefab;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
    private List<Quaternion> RotationsHistory = new List<Quaternion>();

    // Direction tracking
    private Vector3 currentDirection = Vector3.forward; // Start moving forward

    void Start()
    {
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
    }

    void Update()
    {
        //Test Growth
        if (Input.GetKeyDown(KeyCode.G))
        {
            GrowSnake();
        }

        // Handle input for direction change
        if (Input.GetKeyDown(KeyCode.W) && currentDirection != Vector3.back)
        {
            currentDirection = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentDirection != Vector3.forward)
        {
            currentDirection = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A) && currentDirection != Vector3.right)
        {
            currentDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentDirection != Vector3.left)
        {
            currentDirection = Vector3.right;
        }

        // Move in the current direction
        transform.position += currentDirection * MoveSpeed * Time.deltaTime;

        // Rotate the head to face the direction of movement
        transform.forward = currentDirection;

        // Store position and rotation history
        PositionsHistory.Insert(0, transform.position);
        RotationsHistory.Insert(0, transform.rotation);

        // Limit the history size to save memory
        if (PositionsHistory.Count > BodyParts.Count * Gap)
        {
            PositionsHistory.RemoveAt(PositionsHistory.Count - 1);
            RotationsHistory.RemoveAt(RotationsHistory.Count - 1);
        }

        // Move body parts
        for (int i = 0; i < BodyParts.Count; i++)
        {
            // Use the exact position and rotation from history
            int historyIndex = Mathf.Clamp(i * Gap, 0, PositionsHistory.Count - 1);
            BodyParts[i].transform.position = PositionsHistory[historyIndex];
            BodyParts[i].transform.rotation = RotationsHistory[historyIndex];
        }
    }

    private void GrowSnake()
    {
        // Instantiate body instance and add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the snake collides with the border
        if (other.CompareTag("Border"))
        {
            Debug.Log("Snake hit the border. Game Over!");
            MoveSpeed = 0; // Pauses the game
        }
    }
}
