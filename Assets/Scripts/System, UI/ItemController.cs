using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private int direction = 1;

    public float movingWeight = 0.1f;

    public ItemList itemName;

    private float max = 0.5f, min = -0.5f;
    private float orgPositionY;

    private void Awake()
    {
        orgPositionY = transform.position.y;
    }

    private void FixedUpdate()
    {
        if (transform.position.y > max + orgPositionY) direction = -1;
        else if (transform.position.y < min + orgPositionY) direction = 1; 

        transform.position = new Vector3(
                            transform.position.x,
                            transform.position.y + movingWeight * direction / 20, 0.0f );
    }
}
