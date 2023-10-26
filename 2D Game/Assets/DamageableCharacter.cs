using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    public GameObject healthText;

    bool isAlive = true;
    public float health = 5;
    public bool targetable = true;
    public bool disableSimulation = true;
    public bool invincible = false;
    public bool isInvincibilityEnabled = false;
    public float invincibilityTime = 1f;
    public float invincibleTimeElapsed = 0f;

    public float Health
    {
        set
        {

            if (value < health)
            {
                animator.SetTrigger("hit");

                // Dmg text
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            health = value;

            if (health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
                

            }
        }
        get
        {
            return health;
        }
    }

    public bool Targetable
    {
        get
        {
            return targetable;
        }
        set
        {
            targetable = value;

            if (disableSimulation) {
                rb.simulated = false;
            }

            physicsCollider.enabled = value;
        }
    }

    public bool Invincible {
        get {
            return invincible;
                }
        set {
            invincible = value;

            if (invincible == true) {
                invincibleTimeElapsed = 0f;
            }
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
        if (!invincible)
        {
            //Debug.Log("Atros Hit " + damage);
            Health -= damage;
            rb.AddForce(knockback);
            //Debug.Log("Force " + knockback);

            if (isInvincibilityEnabled) {
                invincible = true;
            }
        }
    }

    public void OnHit(float damage)
    {
        //Debug.Log("Atros Hit " + damage);
        if (!invincible) {
            Health -= damage;

            if (isInvincibilityEnabled)
            {
                invincible = true;
            }
        }
    }

    public void MakeUntergetable()
    {
        rb.simulated = false;
    }

    public void OnObjectDestroyed()
    {
        GameObject.Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        if (invincible) {
            invincibleTimeElapsed += Time.deltaTime;

            if (invincibleTimeElapsed > invincibilityTime) {
                Invincible = false;
            }
        }
    }
}
