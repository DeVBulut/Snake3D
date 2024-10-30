using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    public bool isBurger = false;
    public bool isEaten = false;
    private Animator animator; 
    private void Start() 
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(!isEaten)
        {
            transform.Rotate(0, 90 * Time.deltaTime, 0);
        }
        else
        {
            animator.SetTrigger("Dead");
            StartCoroutine(DestroyFruit(0.5f));
        }
    }

    private System.Collections.IEnumerator DestroyFruit(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
