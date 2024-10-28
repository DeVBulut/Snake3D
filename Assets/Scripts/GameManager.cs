using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Fruit Manager Variable Increments")]
    public float initialInterval = 5f;
    public float minimumInterval = 2f;
    public float durationToReachMinimum = 60f;
    [Header("UI Element")]
    public GameObject panel;
    [Header("Game Start Variable(s)")]
    public bool startGame = false;
    private FruitManager fruitManager;
    private float elapsedTime = 0f;

    void Start()
    {
        fruitManager = GetComponent<FruitManager>();
        initialInterval = fruitManager.spawnInterval;
        if (fruitManager != null) {fruitManager.SetSpawnInterval(initialInterval);}
    }

    void Update()
    {
        //Game Start Detection
        if (!startGame && Input.anyKeyDown)
        {
            startGame = true;
            panel.SetActive(false);
            fruitManager.InstantiateRandomFruit();
        }


        if (startGame) 
        {
            if (fruitManager != null && elapsedTime < durationToReachMinimum)
            {
                elapsedTime += Time.deltaTime;

                float timeInterval = elapsedTime / durationToReachMinimum;
                float newInterval = Mathf.Lerp(initialInterval, minimumInterval, timeInterval * timeInterval);

                fruitManager.SetSpawnInterval(newInterval);
            }
        }
    }
}
