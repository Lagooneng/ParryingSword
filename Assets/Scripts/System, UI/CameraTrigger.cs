using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public bool isTriggerForX;

    public bool isStopTriggerX;
    public bool isStopTriggerY;

    private CameraManager cameraManager;

    private void Awake()
    {
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if( collision.CompareTag("PlayerBody") )
        {
            if( isTriggerForX )
            {
                if (isStopTriggerX)
                {
                    cameraManager.stopX = true;
                }
                else
                {
                    cameraManager.stopX = false;
                }
            }
            else
            {
                if (isStopTriggerY)
                {
                    cameraManager.stopY = true;
                }
                else if (!isStopTriggerY)
                {
                    cameraManager.stopY = false;
                }
            }
            
        }
    }

}
