using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject subject;
    public GameObject player;

    private CameraEffects camEf;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camEf = GetComponent<CameraEffects>();
        subject = player;

        Camera.main.transform.position =
            new Vector3(subject.transform.position.x,
                        subject.transform.position.y + 5,
                        Camera.main.transform.position.z);
    }

    private void FixedUpdate()
    {
        camEf.zoomFix();

        if ( (Mathf.Abs( distanceCamearaToSubjectX() ) < 0.01f &&
             Mathf.Abs( distanceCamearaToSubjectY() + 5 ) < 0.01f) ||
             (Mathf.Abs(distanceCamearaToSubjectX()) > 20.0f ||
             Mathf.Abs(distanceCamearaToSubjectY() + 5) > 20.0f) )
        {
            Camera.main.transform.position =
                new Vector3(subject.transform.position.x, subject.transform.position.y + 5,
                                Camera.main.transform.position.z);

            return;
        }
        Camera.main.transform.position =
            new Vector3( Camera.main.transform.position.x + ( distanceCamearaToSubjectX() ) / 3,
                         Camera.main.transform.position.y + ( distanceCamearaToSubjectY() + 5 ) / 3,
                         Camera.main.transform.position.z);

    }

    public float distanceCamearaToSubjectX()
    {
        return subject.transform.position.x - Camera.main.transform.position.x;
    }

    public float distanceCamearaToSubjectY()
    {
        return subject.transform.position.y - Camera.main.transform.position.y;
    }
}
