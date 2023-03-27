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
    private float swayTime = 1.0f, sizeDif = 4.0f;
    private float sizeOrg = 15.0f;
    private float startTime;
    private bool sawyWorking = false, zoomWorking = false;
    private CameraState cState = CameraState.NON;

    private void Awake()
    {
        Camera.main.orthographicSize = sizeOrg;
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
        if (this.cState != CameraState.NON) return;

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


    // 애니메이션에서 줌 인 -> 줌 아웃 순서로 사용해야 함
    // 원하는 타이밍으로 조절하기 위해 템플릿 메소드는 사용하지 않음
    public void zoomIn()
    {
        // Debug.Log(Camera.main.orthographicSize);
        if (sizeOrg - sizeDif < Camera.main.orthographicSize)
        {
            zoomWorking = true;
            Camera.main.orthographicSize -= 0.5f;
        }
        else
        {
            cState = CameraState.NON;
        }
    }

    public void zoomOut()
    {
        // Debug.Log(Camera.main.orthographicSize);
        if ( sizeOrg > Camera.main.orthographicSize  )
        {
            zoomWorking = true;
            Camera.main.orthographicSize += 0.5f;
        }
        else
        {
            cState = CameraState.NON;
        }
    }

    public void setZoomWorking( bool working )
    {
        zoomWorking = working;
        // Debug.Log(zoomWorking);
    }

    public void zoomFix()
    {
        if (zoomWorking) return;

        if( sizeOrg - Camera.main.orthographicSize < 0.1f )
        {
            Camera.main.orthographicSize = sizeOrg;
            return;
        }

        if( sizeOrg > Camera.main.orthographicSize )
        {
            Camera.main.orthographicSize += 0.05f;
        }
        else if( sizeOrg < Camera.main.orthographicSize )
        {
            Camera.main.orthographicSize -= 0.05f;
        }
    }
}
