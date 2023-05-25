using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_DieAfterAtk : MonoBehaviour
{
    public float time = 0.03f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("PlayerBody") )
        {
            Destroy(this.transform.parent.gameObject, time);
        }
    }
}
