using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    public bool isBurger = false;
    private GameManager gameManager;

    private void Start()
    {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
    }

    void Update()
    {
        // Only rotate the fruit if the game has started
        if (gameManager != null && gameManager.startGame)
        {
            transform.Rotate(0, 90 * Time.deltaTime, 0);
        }
    }
}
