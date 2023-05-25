using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemolishingBomb : MonoBehaviour
{
    Collider2D[] colliders;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("bomb", 3.0f);
    }

    public void addForce(float dir)
    {
        rb.AddForce(new Vector2(7000.0f * dir, 6000.0f));
    }

    private void bomb()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, 5.0f);

        foreach( Collider2D collider in colliders )
        {
            if( collider.CompareTag("Road") )
            {
                Destroy(collider.gameObject);
            }
        }

        Destroy(this.transform.parent.gameObject);
    }
}
