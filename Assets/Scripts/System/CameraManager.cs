using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject subject;
    public GameObject player;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        subject = player;
    }

    private void FixedUpdate()
    {
        Camera.main.transform.position =
            new Vector3(subject.transform.position.x,
                        subject.transform.position.y + 5,
                        Camera.main.transform.position.z);


    }
}
