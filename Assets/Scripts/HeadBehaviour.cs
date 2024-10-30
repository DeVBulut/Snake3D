using Unity.VisualScripting;
using UnityEngine;

public class HeadBehaviour : MonoBehaviour
{
    [SerializeField] private MovementSnake parent;
    [SerializeField] private UIManager uimanager;
    private Rigidbody rb; 
    private void Start() 
    {
        parent = transform.parent.GetComponent<MovementSnake>();    
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Tail" || other.gameObject.tag == "Wall")
        {
            Debug.Log("Hit " + other.gameObject.tag);
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
                uimanager.IncreaseScore(fruitScript.isBurger);
                parent.Grow();
                fruitScript.isEaten = true;
            }
        }
    }
}
