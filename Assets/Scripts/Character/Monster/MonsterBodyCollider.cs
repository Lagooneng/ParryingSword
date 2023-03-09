using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBodyCollider : MonoBehaviour
{
    Rigidbody2D rb;
    public float damage;
    public bool superArmor = false;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        damage = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "PlayerArm" )
        {
            if( ! superArmor )
            {
                rb.AddForce(collision.GetComponent<AttackCollider>().knockBackVector);
            }
            this.damage = collision.GetComponent<AttackCollider>().damage;
        }
    }
}
