using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerWithoutPlayer : MonoBehaviour
{
    private StageTrigger_Interaction interactingObject = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            interactingObject.buttonYes();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            interactingObject.buttonNo();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            interactingObject.buttonESC();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            interactingObject.buttonRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            interactingObject.buttonLeft();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            interactingObject.buttonDown();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            interactingObject.buttonUp();
        }

    }

    public void setInteractingObj( StageTrigger_Interaction interactingObject)
    {
        this.interactingObject = interactingObject;
    }
}
