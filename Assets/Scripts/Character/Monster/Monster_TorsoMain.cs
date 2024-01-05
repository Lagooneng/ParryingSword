using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_TorsoMain : MonoBehaviour
{
    public Monster_TorsoController monsterCtrl;


    private void Awake()
    {
        monsterCtrl = GetComponent<Monster_TorsoController>();
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
