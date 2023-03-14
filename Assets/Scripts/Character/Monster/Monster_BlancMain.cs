using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_BlancMain : MonoBehaviour
{
    public Monster_BlancController monsterCtrl;

    public int jump = 10;
    public int wait = 2;

    public int sum;

    private int num;

    private void Awake()
    {
        sum = wait + jump;
        monsterCtrl = GetComponent<Monster_BlancController>();
    }

    private void FixedUpdate()
    {
        if (!monsterCtrl.activeSts) return;
        if (!monsterCtrl.timeCheck()) return;

        num = Random.Range(0, sum);

        if (num < jump)
        {
            monsterCtrl.setState(Monster_BlancState.JUMP, 1.5f);
        }
        else
        {
            monsterCtrl.setState(Monster_BlancState.WAIT, 1.0f);
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
