using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atros : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;

    bool isAlive = true;
    public float health = 5;
    public bool targetable = true;

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
                Targetable = false;
                //Destroy(gameObject);
                
            }
        }
        get {
            return health;
        }
    }

    public bool Targetable {
        get {
            return targetable; 
        }
        set {
            targetable = value;
            physicsCollider.enabled = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Debug.Log("Atros Hit " + damage);
        Health -= damage;
        rb.AddForce(knockback);
        Debug.Log("Force " + knockback);
    }

    public void OnHit(float damage)
    {
        Debug.Log("Atros Hit " + damage);
        Health -= damage;
    }

    public void MakeUntergetable()
    {
        rb.simulated = false;
    }

    public void OnObjectDestroyed()
    {
        GameObject.Destroy(gameObject);
    }
}
