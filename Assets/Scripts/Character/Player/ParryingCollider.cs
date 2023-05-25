using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryingCollider : MonoBehaviour
{
    PlayerController playerCtrl;

    private void Awake()
    {
        playerCtrl = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("EnemyArm") )
        {
            playerCtrl.attackCollider.knockBackVector = 
                new Vector2(12000.0f * playerCtrl.dir, 3000.0f);

            playerCtrl.animator.Play("Player_AttackParryingSuccess");
        }
    }
}
