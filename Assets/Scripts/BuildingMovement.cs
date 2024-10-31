using System.Collections;
using UnityEngine;

public class MoveAndRespawn : MonoBehaviour
{
    public float speed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float targetZ;
    public float respawnZ;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        speed += speedIncreaseRate * Time.deltaTime;
        transform.position += Vector3.back * speed * Time.deltaTime;
        if (transform.position.z <= targetZ)
        {
            transform.position = new Vector3(startPosition.x, startPosition.y, respawnZ);
        }
    }
}
