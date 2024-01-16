using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    public GameObject dropItem;
    public float hpMax = 10.0f;
    [System.NonSerialized] public float hp = 10.0f;

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
        if( dropItem )
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
        base.Dead();
        Destroy(this.gameObject);
    }

    public virtual bool SetHP(float hp, float hpMax)
    {
        this.hp = hp <= 0 ? 0 : hp;
        this.hpMax = hpMax;
        return (this.hp == 0);
    }
}
