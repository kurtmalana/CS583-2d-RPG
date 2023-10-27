using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atros : MonoBehaviour
{
    public float damage = 1;
    public float knockbackForce = 2500f;
    public float moveSpeed = 900f;
    public float maxSpeed = 4;
    public float maxDistance = 50f;
    private bool isMoving = false;
    private bool isIdleSoundPlaying = false;

    public DetectionZone detectionZone;
    Rigidbody2D rb;
    DamageableCharacter damageableCharacter;
    SpriteRenderer spriteRenderer;
    Animator animator;

    [SerializeField] private AudioSource idleSoundEffect;
    [SerializeField] private AudioSource attackSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageableCharacter = GetComponent<DamageableCharacter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Collider2D detectedObject0 = detectionZone.detectedObjs.Count > 0 ? detectionZone.detectedObjs[0] : null;

        if (damageableCharacter.Targetable && detectedObject0 != null)
        {

            //Calculate the direction to target object
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            /*
            // Calculate the distance between the character and the detected object
            float distance = Vector2.Distance(transform.position, detectedObject0.transform.position);

            // Adjust the volume based on distance (you can modify this formula)
            float maxVolume = 1.0f; // Max volume
            float minVolume = 0.05f; // Min volume (adjust as needed)
            float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);

            // Set the volume of the audio source
            idleSoundEffect.volume = volume;
            attackSoundEffect.volume = volume;
            */

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
            //animator.SetBool("isMoving", true);

            if (isIdleSoundPlaying)
            {
                idleSoundEffect.Stop();
                attackSoundEffect.Play();
                isIdleSoundPlaying = false;
            }

        }
        else
        {
            //idleSoundEffect.Play();
            IsMoving = false;
            //animator.SetBool("isMoving", false);

            if (!isIdleSoundPlaying)
            {
                idleSoundEffect.Play();
                attackSoundEffect.Stop();
                isIdleSoundPlaying = true;
            }

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
