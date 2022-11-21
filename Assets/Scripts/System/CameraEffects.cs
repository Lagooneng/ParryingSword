using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState
{
    NON,
    SWAY,
    ZOOMIN,
    ZOOMOUT
}

public class CameraEffects : MonoBehaviour
{
    private float swayTime = 1.0f, sizeDif = 8.0f, sizeOrg;
    private float startTime;
    private bool sawyWorking = false;
    private CameraState cState = CameraState.NON;

    private void Awake()
    {
        sizeOrg = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        switch (cState)
        {
            case CameraState.NON:
                break;
            case CameraState.SWAY:
                cameraSway();
                break;
            case CameraState.ZOOMIN:
                zoomIn();
                break;
            case CameraState.ZOOMOUT:
                zoomOut();
                break;
        }
    }

    public void setState( CameraState cState )
    {
        this.cState = cState;
    }

    public void setSwayTime( float time )
    {
        this.swayTime = time;
    }

    // 카메라 흔들어 충격 효과
    private void cameraSway()
    {
        if( !sawyWorking )
        {
            startTime = Time.fixedTime;
            sawyWorking = true;
        }
        else
        {
            if( Time.fixedTime >= startTime + swayTime )
            {
                sawyWorking = false;
                // Debug.Log(string.Format("Start : {0} End : {1}", startTime, Time.fixedTime));
                cState = CameraState.NON;
            }
        }

        // 카메라 흔들기
        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x + Random.Range(-1, 2),
            Camera.main.transform.position.y + Random.Range(-1, 2),
            Camera.main.transform.position.z);
    }

    private void zoomIn()
    {
        // Debug.Log(Camera.main.orthographicSize);
        if ( sizeOrg + sizeDif > Camera.main.orthographicSize  )
        {
            Camera.main.orthographicSize += 1.0f;
        }
        else
        {
            cState = CameraState.NON;
        }
    }

    private void zoomOut()
    {
        // Debug.Log(Camera.main.orthographicSize);
        if ( sizeOrg < Camera.main.orthographicSize )
        {
            Camera.main.orthographicSize -= 1.0f;
        }
        else
        {
            cState = CameraState.NON;
        }
    }
}
