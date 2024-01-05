using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Vector2 knockBackVector = new Vector2(0f, 0);
    public float damage = 1000.0f;
    /*
    private void Awake()
    {
        knockBackVector = new Vector2(0f, 0);
        damage = 1000.0f;
    }
    */
}
