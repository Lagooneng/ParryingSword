using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_ObserverMain : MonoBehaviour
{
    public Monster_ObserverController monsterCtrl;

    private void Awake()
    {
        monsterCtrl = GetComponent<Monster_ObserverController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            monsterCtrl.activeSts = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            monsterCtrl.activeSts = false;
        }
    }
}
