using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_MagmaSlimeMain : MonoBehaviour
{
    public Monster_MagmaSlimeController monsterCtrl;
    public int wait = 15;
    public int walk = 10;
    public int jump = 10;

    public int sum, num = 0;


    private Dictionary<Monster_MagmaSlimeState, float> delayDict;
    private Monster_MagmaSlimeState nextState;

    private void Awake()
    {
        delayDict = new Dictionary<Monster_MagmaSlimeState, float>();
        delayDict.Add(Monster_MagmaSlimeState.WALK, 1.5f);
        delayDict.Add(Monster_MagmaSlimeState.WAIT, 1.5f);
        delayDict.Add(Monster_MagmaSlimeState.JUMP, 1.5f);

        sum = walk + wait + jump;
        monsterCtrl = GetComponent<Monster_MagmaSlimeController>();
    }

    private void FixedUpdate()
    {
        if (!monsterCtrl.activeSts) return;
        if (!monsterCtrl.timeCheck()) return;

        num = Random.Range(0, sum);

        if( num < walk )
        {
            nextState = Monster_MagmaSlimeState.WALK;
        }
        else if( num < walk + wait )
        {
            nextState = Monster_MagmaSlimeState.WAIT;
        }
        else if( num < walk + walk + jump )
        {
            nextState = Monster_MagmaSlimeState.JUMP;
        }

        monsterCtrl.setState(nextState, delayDict[nextState]);
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
