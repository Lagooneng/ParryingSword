using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_OcciMain : MonoBehaviour
{
    public Monster_OcciController monsterCtrl;
    public int walk = 10;
    public int wait = 2;
    public int attack = 10;
    public int sum;

    private int num;


    private void Awake()
    {
        sum = walk + wait + attack;
        monsterCtrl = GetComponent<Monster_OcciController>();
    }

    private void FixedUpdate()
    {
        if (!monsterCtrl.activeSts) return;
        if (!monsterCtrl.timeCheck()) return;

        num = Random.Range(0, sum);

        if (num < walk)
        {
            monsterCtrl.setState(Monster_OcciState.WALK, 1.0f);
        }
        else if( num < walk + attack )
        {
            monsterCtrl.setState(Monster_OcciState.ATTACK, 1.5f);
        }
        else
        {
            monsterCtrl.setState(Monster_OcciState.WAIT, 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            // Debug.Log("a");
            monsterCtrl.activeSts = true;
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}
