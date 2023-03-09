using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Vector2 knockBackVector;
    public float damage;

    private void Awake()
    {
        knockBackVector = new Vector2(0, 0);
        damage = 0.0f;
    }
}
