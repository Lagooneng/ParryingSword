using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인풋 매니저
public class PlayerMain : MonoBehaviour
{
    // 캐시
    PlayerController playerCtrl;
    CameraManager cam;
    MenuManager menuManager;
    SoundController swordSoundController, parryingSoundController, destructionSoundController;

    bool climbing = false;
    bool climbJump = false;

    private bool interaction = false, orgInteraction = false;
    public StageTrigger_Interaction interactingObject, orgInteractingObject;
    private Menu_SelectionBoxPause menu_SelectionBoxPause;

    int wire = -1, prevWire = -1;   // 계속 벽 잡고 있다가 상실하는 경우 대처용 prevWire 변수

    private void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
        cam = Camera.main.GetComponentInChildren<CameraManager>();
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        swordSoundController = transform.Find("SwordSound").gameObject.GetComponent<SoundController>();
        parryingSoundController = transform.Find("ParryingSound").gameObject.GetComponent<SoundController>();
        destructionSoundController = transform.Find("DestructionSound").gameObject.GetComponent<SoundController>();
        interactingObject = null;
        menu_SelectionBoxPause = GameObject.Find("SelectionBox_Pause").GetComponent<Menu_SelectionBoxPause>();
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            if( interactingObject != menu_SelectionBoxPause )
            {
                orgInteraction = interaction;
                interaction = true;
                orgInteractingObject = interactingObject;
                interactingObject = menu_SelectionBoxPause;
                menu_SelectionBoxPause.pause();
            }
            else
            {
                interaction = orgInteraction;
                interactingObject = orgInteractingObject;
                menu_SelectionBoxPause.undoPause();
            }

            return;
        }

        if( !playerCtrl.activeSts ) return;

        float movingH = Input.GetAxis("Horizontal");
        // Debug.Log(string.Format("{0}", movingH));



        // 선입력 문제로 인해 actionActive 체크가 Input 다음에 체크되어야 함
        // if( playerCtrl.actionActive ) { ~~~ } 가 불가능하다는 뜻

        // 와이어 액션 **************************************
        if ( Input.GetKey(KeyCode.S) && playerCtrl.actionActive && wire != 0 )
        {
            prevWire = wire;
            wire = playerCtrl.ActionWireJump();

            if( prevWire != -1 && wire == 0 || (prevWire == 1 && wire == -1) )
            {
                playerCtrl.ActionWireInertia();
            }
        }

        if( Input.GetKeyUp(KeyCode.S) )
        {
            if( wire > 0 )
            {
                wire = -1;
                playerCtrl.ActionWireInertia();
            }
            else if( wire == 0 )
            {
                wire = -1;
            }
        }
        
        if (wire > 0) return;
        // ************************************************

        /* 폭탄
        if( Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z) && playerCtrl.actionActive )
        {
            playerCtrl.ActionThrowBomb();
            return;
        }
        */

        // 회복 
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Z) && playerCtrl.actionActive)
        {
            playerCtrl.ActionHeal();
            return;
        }

        // 클라이밍 ****************************************
        if (Input.GetKey(KeyCode.UpArrow) && !climbJump && playerCtrl.actionActive)
        {
            if (playerCtrl.ActionClimb())
            {
                climbing = true;

                // 클라이밍 중 카메라 내리기
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    cam.cameraDown();
                }
                else
                {
                    cam.cameraRePosition();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            climbing = false;
            climbJump = false;
            playerCtrl.ActionUndoClimb();
        }

        if (Input.GetKeyDown(KeyCode.Space) && climbing)
        {
            climbing = false;
            climbJump = true;
            playerCtrl.ActionUndoClimb();
            playerCtrl.ActionMustJump();
        }
        // ***********************************************

        if (wire == 1 || climbing) return;

        // 시점 내리기 *************************************
        if (Input.GetKey(KeyCode.DownArrow) && playerCtrl.actionActive)
        {
            cam.cameraDown();
            playerCtrl.ActionMove(0.0f);
            return;
        }
        else
        {
            cam.cameraRePosition();
        }
        // *************************************************

        // 이동
        if( playerCtrl.actionActive )
        {
            playerCtrl.ActionMove(movingH);
        }
        
        //Debug.Log(Input.GetAxisRaw("Right Trigger"));
        if ( (Input.GetKeyDown(KeyCode.Space) ) && playerCtrl.actionActive)
        {
            playerCtrl.ActionJump();
        }

        if ( Input.GetKeyDown(KeyCode.Z) && playerCtrl.actionActive && !interaction )
        {
            playerCtrl.AttackNormal();
        }
        else if( Input.GetKey(KeyCode.X) && playerCtrl.actionActive && !interaction )
        {
            playerCtrl.AttackParrying();
        }
        else if( Input.GetKey(KeyCode.C) && playerCtrl.actionActive && !interaction )
        {
            //Debug.Log("a");
            playerCtrl.AttackSpecial();
        }


    }


    public void setInteraction(bool interaction)
    {
        this.interaction = interaction;
    }

    public bool getInteraction()
    {
        return interaction;
    }

    // 사운드 이펙트
    public void onSwordSound()
    {
        swordSoundController.playClip();
    }

    public void onSwordSound2()
    {
        swordSoundController.playClipWithStartTime(0.15f);
    }

    public void onParryingSound()
    {
        parryingSoundController.playClipWithStartTime(0.25f);
    }

    public void onDestructionSound()
    {
        oneDestructionSound();
        Invoke("oneDestructionSound", 0.05f);
        Invoke("oneDestructionSound", 0.1f);
    }

    public void oneDestructionSound()
    {
        destructionSoundController.playClipWithStartTime(0.25f);
    }
}
