using UnityEngine;
using System.Collections;

public class FruitManager : MonoBehaviour
{
    public Transform Edge1;
    public Transform Edge2;
    public GameObject[] FruitPrefabs;
    public float spawnInterval = 5f;

    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnFruitAtIntervals());
    }

    IEnumerator SpawnFruitAtIntervals()
    {
        while (true)
        {
            InstantiateRandomFruit();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void InstantiateRandomFruit()
    {
        float randomX = Random.Range(Edge2.position.x, Edge1.position.x);
        float yPosition = Edge1.position.y;
        float randomZ = Random.Range(Edge2.position.z, Edge1.position.z);

        Vector3 randomPosition = new Vector3(randomX, yPosition, randomZ);

        int randomIndex = Random.Range(0, FruitPrefabs.Length);
        GameObject fruitPrefab = FruitPrefabs[randomIndex];

        Instantiate(fruitPrefab, randomPosition, Quaternion.identity);
    }

    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnFruitAtIntervals());
    }
}
