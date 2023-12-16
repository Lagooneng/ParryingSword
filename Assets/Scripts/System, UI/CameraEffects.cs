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
    public float sizeOrg;
    public GameObject darkFilter;

    private float swayTime = 1.0f, sizeDif = 4.0f;
    private float startTime;
    private bool sawyWorking = false, zoomWorking = false;
    private CameraState cState = CameraState.NON;
    private CameraManager camManager;
    private SpriteRenderer darkFilterRenderer;

    // 암전 효과를 분리한 이유 -> 줌이랑 겹치거나 하면 둘 중 하나가 무시될 가능성이 있음 
    bool isDarkEffectOn = false;

    private void Awake()
    {
        camManager = GetComponent<CameraManager>();
        darkFilterRenderer = darkFilter.GetComponent<SpriteRenderer>();
        sizeOrg = camManager.cameraSize;
    }

    private void FixedUpdate()
    {
        if( isDarkEffectOn )
        {
            darkEffectOn();
        }
        else
        {
            darkEffectOff();
        }

        switch (cState)
        {
            case CameraState.NON:
                zoomFix();
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

    public void switchDarkEffect(bool on)
    {
        isDarkEffectOn = on;
    }

    private void darkEffectOn()
    {
        if( darkFilterRenderer.color.a < 0.6f )
        {
            darkFilterRenderer.color = new Color(darkFilterRenderer.color.r,
                                                darkFilterRenderer.color.g,
                                                darkFilterRenderer.color.b,
                                                darkFilterRenderer.color.a + 0.01f);
        }
    }

    private void darkEffectOff()
    {
        if (darkFilterRenderer.color.a > 0.0f)
        {
            darkFilterRenderer.color = new Color(darkFilterRenderer.color.r,
                                                darkFilterRenderer.color.g,
                                                darkFilterRenderer.color.b,
                                                darkFilterRenderer.color.a - 0.05f);
        }
    }
}
