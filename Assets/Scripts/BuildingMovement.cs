using System.Collections;
using UnityEngine;

public class MoveAndRespawn : MonoBehaviour
{
    public float speed = 5f; // Speed of movement along the x-axis
    public float targetX; // x position where the object should reset
    public float respawnX; // x position where the object respawns after reaching the target

    private Vector3 startPosition;

    private void Start()
    {
        // Save the starting position as the object's initial placement
        startPosition = transform.position;
    }

    private void Update()
    {
        // Move the object along the x-axis
        transform.position += Vector3.right * speed * Time.deltaTime;

        // Check if the object has reached the target point
        if (transform.position.x >= targetX)
        {
            // Respawn the object at the specified respawn point, keeping its y and z coordinates the same
            transform.position = new Vector3(respawnX, startPosition.y, startPosition.z);
        }
    }
}
