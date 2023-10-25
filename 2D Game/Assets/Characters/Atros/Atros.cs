using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atros : MonoBehaviour
{
    public float damage = 1;
    public float knockbackForce = 300f;



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
            damageable.OnHit(damage, knockback);
        }
    }
}
