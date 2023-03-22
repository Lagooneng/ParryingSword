using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    protected PlayerController playerCtrl;
    protected GameObject player;
    protected MonsterBodyCollider bodyCollider;

    protected float startTime = 0.0f;
    protected float nextDelay = 0.0f;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
        playerCtrl = player.GetComponent<PlayerController>();
        bodyCollider = GetComponentInChildren<MonsterBodyCollider>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackCollider = GetComponentInChildren<AttackCollider>();
    }

    public virtual void FixedUpdateAI() { }


    public void lookPlayer(bool forward)
    {
        if (distanceToPlayerX() < 0.5f )
        {
            dir = 1;
            return;
        }

        if( player.transform.position.x < gameObject.transform.position.x )
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }

        if( !forward )
        {
            dir *= -1;
        }
    }

    public bool timeCheck()
    {
        if (startTime + nextDelay > Time.fixedTime) return false;
        else return true;
    }

    public float distanceToPlayerX()
    {
        return Mathf.Abs(player.transform.position.x - transform.position.x);
    }

    public float distanceToPlayerY()
    {
        return Mathf.Abs(player.transform.position.y - transform.position.y);
    }

    public override void Dead()
    {
        base.Dead();
        Destroy(this.gameObject);
    }
}
