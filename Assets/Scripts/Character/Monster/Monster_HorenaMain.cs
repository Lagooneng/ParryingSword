using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_HorenaMain : MonoBehaviour
{
    public Monster_HorenaController monsterCtrl;
    public int moveToPlayer = 30;
    public int wait = 10;
    public int sum;

    private int num;
    

    private void Awake()
    {
        sum = moveToPlayer + wait;
        monsterCtrl = GetComponent<Monster_HorenaController>();
    }

    private void FixedUpdate()
    {
        if (!monsterCtrl.activeSts) return;
        if (!monsterCtrl.timeCheck()) return;

        num = Random.Range(0, sum);

        if( num < moveToPlayer )
        {
            monsterCtrl.setState(Monster_HorenaState.MOVETOPLAYER, 3.0f);
        }
        else
        {
            monsterCtrl.setState(Monster_HorenaState.WAIT, 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "PlayerBody" )
        {
            // Debug.Log("a");
            monsterCtrl.activeSts = true;
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}
