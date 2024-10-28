using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int currentScore = 0;
    public GameManager gameManager;

    public int scoreDecrement = 1; // How much score decreases every second
    public int scoreIncrement = 5; // How much score increases when a fruit is eaten

    private float scoreUpdateInterval = 1f; // Interval for score reduction
    private float elapsedTime = 0f;

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        if (gameManager.startGame) {
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
}
