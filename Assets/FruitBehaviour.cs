using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    public bool isBurger = false;
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }
}
