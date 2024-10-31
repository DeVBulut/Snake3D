using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Movement : MonoBehaviour
{
    public float MoveSpeed = 5;
    public int Gap = 10;

    public GameObject BodyPrefab;

    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
    private List<Quaternion> RotationsHistory = new List<Quaternion>();

    private Vector3 currentDirection = Vector3.forward; 

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

        if (Input.GetKeyDown(KeyCode.G))
        {
            GrowSnake();
        }

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


        transform.position += currentDirection * MoveSpeed * Time.deltaTime;


        transform.forward = currentDirection;

        PositionsHistory.Insert(0, transform.position);
        RotationsHistory.Insert(0, transform.rotation);

        if (PositionsHistory.Count > BodyParts.Count * Gap)
        {
            PositionsHistory.RemoveAt(PositionsHistory.Count - 1);
            RotationsHistory.RemoveAt(RotationsHistory.Count - 1);
        }

        for (int i = 0; i < BodyParts.Count; i++)
        {
            int historyIndex = Mathf.Clamp(i * Gap, 0, PositionsHistory.Count - 1);
            BodyParts[i].transform.position = PositionsHistory[historyIndex];
            BodyParts[i].transform.rotation = RotationsHistory[historyIndex];
        }
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            Debug.Log("Snake hit the border. Game Over!");
            MoveSpeed = 0;
        }
    }
}