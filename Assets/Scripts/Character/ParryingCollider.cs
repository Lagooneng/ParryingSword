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
        if( collision.tag == "EnemyArm" )
        {
            playerCtrl.actionActive = false;
            playerCtrl.animator.Play("Player_AttackParryingSuccess");
        }
    }
}
