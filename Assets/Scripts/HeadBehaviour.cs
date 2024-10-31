using Unity.VisualScripting;
using UnityEngine;

public class HeadBehaviour : MonoBehaviour
{
    [Header("References To Manager Components")]
    [SerializeField] private MovementSnake parent;
    [SerializeField] private UIManager uimanager;
    [SerializeField] private GameManager gameManager;

    [Header("Audio Players")]
    public AudioClip deathSound;
    public AudioClip eatingSound;

    private Rigidbody rb;
    private AudioSource snakeSoundSource;

    private void Start() 
    {
        parent = transform.parent.GetComponent<MovementSnake>();    
        rb = GetComponent<Rigidbody>();
        snakeSoundSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Tail" || other.gameObject.tag == "Wall")
        {
            Debug.Log("Hit " + other.gameObject.tag);
            snakeSoundSource.PlayOneShot(deathSound);
            rb.constraints = RigidbodyConstraints.FreezeAll; 
            parent.Die();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Fruit")
        {
            Debug.Log("Hit " + other.gameObject.tag);
            FruitBehaviour fruitScript = other.GetComponent<FruitBehaviour>();
            if(fruitScript.isEaten == false)
            {
                snakeSoundSource.PlayOneShot(eatingSound);
                uimanager.IncreaseScore(fruitScript.isBurger);
                gameManager.IncreaseSnakeSpeed(fruitScript.isBurger);
                parent.Grow();
                fruitScript.isEaten = true;
            }
        }
    }
}
