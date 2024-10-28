using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FruitManager fruitManager;
    public float initialInterval = 5f;
    public float minimumInterval = 2f;
    public float durationToReachMinimum = 60f;
    public bool startGame = false;
    private float elapsedTime = 0f;
    public GameObject panel;

    void Start()
    {
        initialInterval = fruitManager.spawnInterval;
        if (fruitManager != null)
        {
            fruitManager.SetSpawnInterval(initialInterval);
        }
    }

    void Update()
    {
        if (!startGame && Input.anyKeyDown)
        {
            panel.SetActive(false);
            startGame = true;
            fruitManager.InstantiateRandomFruit();
        }
        if (startGame) {
            if (fruitManager != null && elapsedTime < durationToReachMinimum)
            {
                elapsedTime += Time.deltaTime;

                float t = elapsedTime / durationToReachMinimum;
                float newInterval = Mathf.Lerp(initialInterval, minimumInterval, t * t);

                fruitManager.SetSpawnInterval(newInterval);
            }
        }
    }
}
