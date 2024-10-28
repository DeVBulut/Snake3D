using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FruitManager fruitManager;
    public float initialInterval = 5f;
    public float minimumInterval = 2f;
    public float durationToReachMinimum = 60;
    public bool startGame = false;
    private float elapsedTime = 0f;

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
            startGame = true;
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
