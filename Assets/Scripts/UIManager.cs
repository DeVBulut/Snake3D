using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Score Variables")]
    [SerializeField] private int currentScore = 0;
    [Tooltip("How much score decreases every second")]
    public int scoreDecrement = 1; 
    [Tooltip("How much score increases when a fruit is eaten")]
    public int scoreIncrement = 5;
    public TMP_Text scoreText;

    private float scoreUpdateInterval = 1f;
    private float elapsedTime = 0f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        UpdateScoreUI();
    }

    void Update()
    {
        if (gameManager.runGame) {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= scoreUpdateInterval)
            {
                elapsedTime -= scoreUpdateInterval;
                DecreaseScore(scoreDecrement);
            }
        }
    }

    public void IncreaseScore(bool isBurger)
    {
        if (isBurger) { currentScore += scoreIncrement * 2;} 
        else          { currentScore += scoreIncrement; }
        UpdateScoreUI();
    }

    private void DecreaseScore(int amount)
    {
        currentScore = Mathf.Max(0, currentScore - amount);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Score: {currentScore}";
    }

    public void RefleshScene()
    {
        SceneManager.LoadScene("FinalScene");
    }
}
