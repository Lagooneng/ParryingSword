using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_FireGummyMain : MonoBehaviour
{
    public Monster_FireGummyController monsterCtrl;

    private void Awake()
    {
        monsterCtrl = GetComponent<Monster_FireGummyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            monsterCtrl.activeSts = true;
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}
