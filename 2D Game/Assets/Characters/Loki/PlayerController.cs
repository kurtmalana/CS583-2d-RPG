using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 700f;
    public float collisionOffset = 0.05f;
    public float maxSpeed = 2.2f;
    public float idleFriction = 0.9f;
    private bool isMoving = false;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public GameObject swordHitbox;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    Collider2D swordCollider;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (canMove == true && movementInput != Vector2.zero)
        {

            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);

            if (movementInput.x > 0)
            {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingLeft", true);
                //swordAttack.IsFacingLeft(false);
            }
            else if (movementInput.x < 0)
            {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingLeft", false);
                //swordAttack.IsFacingLeft(true);
            }

            IsMoving = true;
        }
        else {
            //rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);

            IsMoving = false;
        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    void OnFire() 
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();

        if (spriteRenderer.flipX == false) { 
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack() {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
}
