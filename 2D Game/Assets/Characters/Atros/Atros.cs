using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atros : MonoBehaviour
{
    public float damage = 1;
    public float knockbackForce = 2500f;
    public float moveSpeed = 900f;
    public float maxSpeed = 4;
    private bool isMoving = false;

    public DetectionZone detectionZone;
    Rigidbody2D rb;
    DamageableCharacter damageableCharacter;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageableCharacter = GetComponent<DamageableCharacter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Collider2D detectedObject0 = detectionZone.detectedObjs[0];

        if (damageableCharacter.Targetable && detectionZone.detectedObjs.Count > 0) {

            //Calculate the direction to target object
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            // Move towards detected object
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (direction * moveSpeed * Time.deltaTime), maxSpeed);

            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
                //gameObject.BroadcastMessage("IsFacingLeft", true);
                //swordAttack.IsFacingLeft(false);
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
                //gameObject.BroadcastMessage("IsFacingLeft", false);
                //swordAttack.IsFacingLeft(true);
            }

            IsMoving = true;
            Debug.Log("it's going");
        }
        else
        {
            IsMoving = true;
        }
    }

    // Damage
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Test Collision");
        Collider2D collider = col.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null) {
            
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

            Vector2 direction = (Vector2)(collider.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            //collider.SendMessage("OnHit", swordDamage, knockback);
            if (collider.gameObject.tag == "Player")
            {
                damageable.OnHit(damage, knockback);
            }
        }
    }

    // Idle to movement transition
    public bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
}
