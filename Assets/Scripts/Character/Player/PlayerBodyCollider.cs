using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyCollider : MonoBehaviour
{
    Rigidbody2D rb;

    private PlayerController playerCtrl;
    private AnimatorStateInfo animStateInfo;
    private ItemManager itemManager;

    //public readonly static int AnimAttackParryingSuccess =
    //    Animator.StringToHash("Base Layer.Player_AttackParryingSuccess");

    public float damage;


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerCtrl = transform.parent.GetComponent<PlayerController>();
        itemManager = transform.parent.GetComponent<ItemManager>();
        damage = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (playerCtrl.animator.GetCurrentAnimatorStateInfo(0).fullPathHash == AnimAttackParryingSuccess) return;

        if (  collision.CompareTag("EnemyArm") || collision.CompareTag("DamageObjectNoParrying"))
        {
            // rb.velocity = new Vector2(0.0f, rb.velocity.y);
            rb.AddForce(collision.GetComponent<AttackCollider>().knockBackVector);
            this.damage = collision.GetComponent<AttackCollider>().damage;
            // Debug.Log(damage);
        }
        else if( collision.CompareTag("Item") )
        {
            ItemController item = collision.GetComponent<ItemController>();

            itemManager.renewItem(item.itemName, itemManager.countItem(item.itemName) + 1);

            Destroy(collision.gameObject);
        }
    }
}
