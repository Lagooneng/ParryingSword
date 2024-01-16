using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrollX : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public float speed = 0.4f;
    public float startDelay = 0.0f;
    private float startTime;

    private float endX;

    private void Awake()
    {
        startTime = Time.time;
        transform.position = new Vector3(start.transform.position.x,
                                            start.transform.position.y, transform.position.z);
        endX = end.transform.position.x;
    }

    private void Update()
    {
        if (Time.time < startTime + startDelay) return;

        if( transform.position.x < endX )
        {
            transform.position = new Vector3(transform.position.x + speed,
                                            start.transform.position.y, transform.position.z);
        }
    }

}
