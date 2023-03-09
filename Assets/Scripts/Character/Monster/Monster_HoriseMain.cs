using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_HoriseMain : MonoBehaviour
{
    public Monster_HoriseController monsterCtrl;

    private void Awake()
    {
        monsterCtrl = GetComponent<Monster_HoriseController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBody")
        {
            monsterCtrl.activeSts = true;
            Destroy(this.gameObject, 1.0f);
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}
