using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject subject;
    public GameObject player;
    public float cameraSize;
    public bool stopX = false, stopY = false;


    private CameraEffects camEf;
    private float offsetY = 5.0f;
    private float posX, posY;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camEf = GetComponent<CameraEffects>();
        subject = player;

        Camera.main.orthographicSize = cameraSize;

        Camera.main.transform.position =
            new Vector3(subject.transform.position.x,
                        subject.transform.position.y + offsetY,
                        Camera.main.transform.position.z);
    }

    private void FixedUpdate()
    {
        camEf.zoomFix();

        if( !stopX )
        {
            posX = Camera.main.transform.position.x + distanceCamearaToSubjectX() / 5;
        }

        if ( !stopY )
        {
            posY = Camera.main.transform.position.y + (distanceCamearaToSubjectY() + offsetY) / 3;
        }

        if ( (Mathf.Abs( distanceCamearaToSubjectX() ) < 0.01f &&
             Mathf.Abs( distanceCamearaToSubjectY() + offsetY) < 0.01f) ||
             (Mathf.Abs(distanceCamearaToSubjectX()) > 20.0f ||
             Mathf.Abs(distanceCamearaToSubjectY() + offsetY) > 20.0f) )
        {
            Camera.main.transform.position =
                new Vector3(subject.transform.position.x, subject.transform.position.y + offsetY,
                                Camera.main.transform.position.z);

            return;
        }
        Camera.main.transform.position =
            new Vector3( posX, posY, Camera.main.transform.position.z);

    }

    public float distanceCamearaToSubjectX()
    {
        return subject.transform.position.x - Camera.main.transform.position.x;
    }

    public float distanceCamearaToSubjectY()
    {
        return subject.transform.position.y - Camera.main.transform.position.y;
    }

    public void cameraDown()
    {
        offsetY = -10.0f;
    }

    public void cameraRePosition()
    {
        offsetY = 5.0f;
    }
}
