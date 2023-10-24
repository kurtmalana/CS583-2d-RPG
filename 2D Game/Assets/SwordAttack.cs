using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public float damage = 3;

    public Collider2D swordCollider;

    Vector2 leftAttackOffset;

    // Start is called before the first frame update
    void Start()
    {
        leftAttackOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AttackRight() 
    {
        print("right");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(leftAttackOffset.x * -1, leftAttackOffset.y);
    }

    public void AttackLeft()
    {
        print("left");
        swordCollider.enabled = true;
        transform.localPosition = leftAttackOffset;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy") 
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.Health -= damage;
                    }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //col.collider.SendMessage("OnHit", 1);
    }
}
