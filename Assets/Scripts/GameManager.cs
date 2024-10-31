using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Fruit Manager Variable Increments")]
    public float initialInterval = 5f;
    public float minimumInterval = 2f;
    public float durationToReachMinimum = 60f;
    [Header("UI Element")]
    public GameObject panel;
    [Header("Game Start Variable(s)")]
    public bool runGame = false;
    private FruitManager fruitManager;
    private float elapsedTime = 0f;

    [Header("Snake Variables")]
    public MovementSnake snake;
    public float intervalChange = 0.0025f;
    private AudioSource musicSource;
    [Header("Sound Components")]
    [SerializeField] private AudioSource playSoundSource;

    void Start()
    {
        fruitManager = GetComponent<FruitManager>();
        initialInterval = fruitManager.spawnInterval;
        musicSource = GetComponent<AudioSource>(); 
        if (fruitManager != null) {fruitManager.SetSpawnInterval(initialInterval);}
    }

    void Update()
    {
        //Game Start Detection
        if (!runGame && Input.anyKeyDown)
        {
            runGame = true;
            panel.SetActive(false);
            musicSource.Play(0);
            playSoundSource.Play(0);
            fruitManager.InstantiateRandomFruit();

        }
        else if(runGame && snake.dead)
        {
            musicSource.Pause();
            runGame = false;
        }

        IncreaseFruitSpawnRate();
        
    }

    void IncreaseFruitSpawnRate()
    {
        if (runGame) 
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

    public void IncreaseSnakeSpeed(bool isBurger)
    {
        if (snake.moveInterval > 0.03) {
            if (!isBurger)
            {
                snake.moveInterval -= intervalChange;
            }
            else
            {
                snake.moveInterval -= intervalChange * 2;
            }
        }
    }
}
