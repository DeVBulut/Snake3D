using UnityEngine;
using System.Collections;

public class FruitManager : MonoBehaviour
{
    [Header("Spawn Boundaries")]
    [Tooltip("Edge 1 is Top Right Side")]
    public Transform Edge1;
    [Tooltip("Edge 2 is Bottom Left Side")]
    public Transform Edge2;
    [Header("Fruit Prefab List")]
    public GameObject[] FruitPrefabs;
    [Header("Fruit Spawn Interval")]
    public float spawnInterval = 5f;


    private GameManager gameManager;
    private Coroutine spawnCoroutine;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        spawnCoroutine = StartCoroutine(SpawnFruitAtIntervals());
    }

    IEnumerator SpawnFruitAtIntervals()
    {
        while (true)
        {
            if (gameManager != null && gameManager.runGame)
            {
                InstantiateRandomFruit();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void InstantiateRandomFruit()
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
        spawnInterval = Mathf.Max(2f, newInterval);
    }
}
