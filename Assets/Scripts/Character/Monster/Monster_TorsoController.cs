using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster_TorsoController : MonsterController
{
    private Transform roadConnection;
    private bool on = false;

    protected override void Awake()
    {
        base.Awake();
        SetHP(hpMax, hpMax);
        activeSts = false;
        roadConnection = transform.Find("RoadConnection");
    }

    private void Start()
    {
        attackCollider.damage = 15.0f;
        attackCollider.knockBackVector = new Vector2(2000.0f * dir, 0.0f);
    }

    protected override void FixedUpdate()
    {
        if (!activeSts) return;

        if (bodyCollider.damage > 0)
        {
            if (SetHP(hp - bodyCollider.damage, hpMax))
            {
                Dead();
            }
            bodyCollider.damage = 0;
        }

        Collider2D[] groundColliderList = Physics2D.OverlapPointAll(groundConnection_Right.position);
        Collider2D[] roadColliderList = Physics2D.OverlapPointAll(roadConnection.position);


        grounded = false;

        foreach (Collider2D groundCollider in groundColliderList)
        {
            if (groundCollider != null &&
                groundCollider.CompareTag("Road") /*|| groundCollider.CompareTag("EnemyPhysicalBody")*/ )
            {
                // Debug.Log("a");
                grounded = true;
                on = true;
            }
        }

        foreach (Collider2D roadCollider in roadColliderList)
        {
            if (roadCollider != null &&
                roadCollider.CompareTag("Road") /*|| groundCollider.CompareTag("EnemyPhysicalBody")*/ )
            {
                // Debug.Log("a");
                grounded = false;       // 벽에 막히면 돌아가도록 
            }
        }

        if (!on) return;

        if( !grounded )     // 진행 방향에 바닥이 없거나, 막혀있으면 돌아가도록 설계 
        {
            if( transform.localScale.x > 0.0f )
            {
                dir = -1.0f;
            }
            else
            {
                dir = 1.0f;
            }
            
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir,
                                            transform.localScale.y, transform.localScale.z);
        }

        //Debug.Log(movingWeight * (dir));

        rb.velocity = new Vector2( movingWeight * (dir), rb.velocity.y);
    }
}
