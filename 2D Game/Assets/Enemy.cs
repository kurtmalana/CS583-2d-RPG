using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Health {
        set {
            health = value;
            if (health <= 0) {
                Defeated();
            }
        }

        get {
            return health;
        }
    }

    public float health = 10;

    public void TakeDamage(float damage) {
        Health -= damage;
    }

    public void Defeated() {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }
}
