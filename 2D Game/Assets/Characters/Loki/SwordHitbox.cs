using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    //
    //Vector2 leftAttackOffset;

    public float swordDamage = 1f;
    public float knockbackForce = 700f;
    public Collider2D swordCollider;

    public Vector3 faceRight = new Vector3(-0.03f, 0.01f, 0);
    public Vector3 faceLeft = new Vector3(0.03f, 0.01f, 0);

    // Start is called before the first frame update
    void Start()
    {
        //swordCollider.GetComponent<Collider2D>();
        if (swordCollider == null) {
            Debug.LogWarning("Sword Collider not set");
        }
        //
        //leftAttackOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnCollisionEnter2D(Collision2D col)
    {
        col.collider.SendMessage("OnHit", swordDamage);
    }*/

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

            Vector2 direction = (Vector2) (collider.gameObject.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;

            //collider.SendMessage("OnHit", swordDamage, knockback);
            damageableObject.OnHit(swordDamage, knockback);
        }
        else {
            Debug.LogWarning("Collider does not implement IDamageable");
        }
    }

    void IsFacingLeft(bool isFacingLeft) {
        if (isFacingLeft) {
            gameObject.transform.localPosition = faceLeft;
        }
        else
        {
            gameObject.transform.localPosition = faceRight;
        }

    }

    //Vector2 CalculateKnockback() {
        //GameObject character = gameObject.GetComponentInParent;
    //}

}
