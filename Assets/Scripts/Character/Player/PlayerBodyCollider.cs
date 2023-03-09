using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyCollider : MonoBehaviour
{
    Rigidbody2D rb;

    private PlayerController playerCtrl;
    private AnimatorStateInfo animStateInfo;

    public readonly static int AnimAttackParryingSuccess =
        Animator.StringToHash("Base Layer.Player_AttackParryingSuccess");

    public float damage;


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerCtrl = transform.parent.GetComponent<PlayerController>();
        damage = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerCtrl.animator.GetCurrentAnimatorStateInfo(0).fullPathHash == AnimAttackParryingSuccess) return;

        if (  collision.tag == "EnemyArm"   )
        {
            rb.AddForce(collision.GetComponent<AttackCollider>().knockBackVector);
            this.damage = collision.GetComponent<AttackCollider>().damage;
            // Debug.Log(damage);
        }
    }
}
