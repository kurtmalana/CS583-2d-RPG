using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atros : MonoBehaviour
{
    Animator animator;

    bool isAlive = true;
    public float health = 5;

    public float Health
    {
        set {

            if (value < health)
            {
                animator.SetTrigger("hit");
            }

            health = value;

            if (health <= 0) {
                animator.SetBool("isAlive", false);
                //Destroy(gameObject);
                
            }
        }
        get {
            return health;
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHit(float damage) {
        //animator.SetTrigger("hit");
        Debug.Log("Atros Hit " + damage);
        Health -= damage;
    }
}
