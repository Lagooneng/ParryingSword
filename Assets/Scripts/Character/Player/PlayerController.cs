using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    [System.NonSerialized] public bool actionActive = true;
    [System.NonSerialized] public PlayerBodyCollider bodyCollider;

    public readonly static int AnimStanding =
        Animator.StringToHash("Base Layer.Player_Standing");
    public readonly static int AnimWalk =
        Animator.StringToHash("Base Layer.Player_Walk");
    public readonly static int AnimJump =
        Animator.StringToHash("Base Layer.Player_Jump");
    public readonly static int AnimAttackHauen =
        Animator.StringToHash("Base Layer.Player_AttackHauen");
    public readonly static int AnimAttackParrying =
        Animator.StringToHash("Base Layer.Player_AttackParrying");
    public readonly static int AnimAttackParryingSuccess =
        Animator.StringToHash("Base Layer.Player_AttackParryingSuccess");
    public readonly static int AnimAttackZornhauw =
        Animator.StringToHash("Base Layer.Player_AttackZornhauw");
    public readonly static int AnimPlayerClimb =
        Animator.StringToHash("Base Layer.Player_Climb");
    public readonly static int AnimPlayerDamage =
        Animator.StringToHash("Base Layer.Player_Damage");

    public bool superMode = false;
    public GameObject bomb;
    public GameObject muzzle;

    protected Transform roadConnection;
    protected WireAction wireAction;

    private AnimatorStateInfo animStateInfo;
    private CameraEffects camEf;
    private MagicCrystalManager magicCrystalManager;
    private bool climbing = false;
    private bool usingWire = false;
    private int additionalJumpCount = 0;

    public override void Dead()
    {
        base.Dead();
        // 리로드
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    protected override void Awake()
    {
        base.Awake();
        roadConnection = transform.Find("RoadConnection");
        attackCollider = GetComponentInChildren<AttackCollider>();
        bodyCollider = GetComponentInChildren<PlayerBodyCollider>();
        wireAction = GameObject.Find("WireSet").GetComponent<WireAction>();
        camEf = GameObject.Find("CameraManager").GetComponent<CameraEffects>();
        magicCrystalManager = GetComponent<MagicCrystalManager>();
        SetHP(hpMax, hpMax);
    }
    
    protected override void FixedUpdate()
    {
        // 낙하 체크
        if (transform.position.y < -60.0f)
        {
            Dead();
        }

        // 대미지 체크
        if ( !superMode && bodyCollider.damage > 0)
        {
            if (SetHP(hp - bodyCollider.damage, hpMax))
            {
                Dead();
            }
            animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

            animator.SetTrigger("Damage");

            bodyCollider.damage = 0;
            // Debug.Log(hp);
        }

        // 지면 체크
        groundedPrev = grounded;
        grounded = false;

        Collider2D[][] groundColliderLists = new Collider2D[3][];
        groundColliderLists[0] = Physics2D.OverlapPointAll(groundConnection_Left.position);
        groundColliderLists[1] = Physics2D.OverlapPointAll(groundConnection_Center.position);
        groundColliderLists[2] = Physics2D.OverlapPointAll(groundConnection_Right.position);

        foreach (Collider2D[] groundColliderList in groundColliderLists)
        {
            foreach (Collider2D groundCollider in groundColliderList)
            {
                if (groundCollider != null &&
                    groundCollider.CompareTag("Road") ||
                    groundCollider.CompareTag("EnemyPhysicalBody") ||
                    groundCollider.CompareTag("Liquid"))
                {
                    grounded = true;
                    additionalJumpCount = 0;
                }
            }
        }


        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if( animStateInfo.fullPathHash == AnimStanding ||
            animStateInfo.fullPathHash == AnimWalk ||
            animStateInfo.fullPathHash == AnimJump )
        {
            rb.AddForce(new Vector2(0.0f, force_y));
            force_y = 0;

            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir,
                                            transform.localScale.y, transform.localScale.z);

            if (!usingWire)
            {
                rb.velocity = new Vector2(Mathf.Clamp(velocity_x, velocityMin.x, velocityMax.x),
                                      Mathf.Clamp(rb.velocity.y, velocityMin.y, velocityMax.y));
            }
        }

        


        jumped = false;
        if (!grounded && !climbing && !usingWire )
        {
            animator.SetTrigger("Jump");
            jumped = true;
        }
    }

    public override void ActionMove(float n)
    {
        if (climbing) return;
        base.ActionMove(n);
    }

    public override void ActionJump()
    {
        base.ActionJump();

        if( jumped && additionalJumpCount < 1 && magicCrystalManager.haveMagicCrystal(MagicCrystalList.ACTIONDJUMP) )
        {
            ActionMustJump();
            additionalJumpCount++;
        }
    }

    public void ActionMustJump()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
        force_y = jumpAccel_y * rb.mass;
    }

    public void AttackNormal()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if ( !actionActive ) return;

        if( animStateInfo.fullPathHash == AnimAttackParryingSuccess )
        {
            attackCollider.knockBackVector = new Vector2(0.0f, 0.0f);
            attackCollider.damage = 1.0f;
            animator.Play("Player_AttackStechen");
        }
        else if( animStateInfo.fullPathHash != AnimAttackHauen )
        {
            attackCollider.knockBackVector = 
                new Vector2(6000.0f * dir, 2000.0f);
            attackCollider.damage = 2.0f;
            animator.SetTrigger("Normal");
        }
        else
        {
            attackCollider.knockBackVector = new Vector2(0.0f, 0.0f);
            attackCollider.damage = 1.0f;
            animator.Play("Player_AttackStechen");
        }
    }

    public void AttackParrying()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(!actionActive) return;

        if( animStateInfo.fullPathHash != AnimAttackParrying )
        {
            attackCollider.knockBackVector = new Vector2(0.0f, 0.0f);
            attackCollider.damage = 2.0f;
            animator.SetTrigger("Parrying");
        }
    }

    public void AttackSpecial()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!actionActive) return;

        if (animStateInfo.fullPathHash == AnimAttackParryingSuccess)
        {
            attackCollider.knockBackVector = 
                new Vector2(15000.0f * dir, 5000.0f);
            attackCollider.damage = 4.0f;
            animator.Play("Player_AttackZornhauw");
        }
    }

    // 성공 시 true 리턴
    public bool ActionClimb()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(roadConnection.position);

        foreach( Collider2D col in colliders ){
            if( col.CompareTag("Road") )
            {
                climbing = true;
                additionalJumpCount = 0;
                animator.SetTrigger("Climb");
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                return true;
            }
        }

        return false;
    }

    public void ActionUndoClimb()
    {
        climbing = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    // 와이어액션
    public int ActionWireJump()
    {
        if( !magicCrystalManager.haveMagicCrystal(MagicCrystalList.ACTIONWIRE) )
        {
            return 0;
        }

        int success = wireAction.ActionWireJump();

        if( success > 0 )
        {
            usingWire = true;
            animator.SetTrigger("WireAction");
        }

        return success;
    }

    public void ActionWireInertia()
    {
        if (!magicCrystalManager.haveMagicCrystal(MagicCrystalList.ACTIONWIRE))
        {
            return;
        }

        usingWire = false;
        wireAction.inactiveWire();
        rb.velocity = new Vector2(rb.velocity.x * 1.3f, 40.0f);
    }

    // 철거용 폭탄
    public void ActionThrowBomb()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(roadConnection.position);

        foreach (Collider2D col in colliders)
        {
            if (col.tag == "Road")
            {
                return;
            }
        }

        GameObject go = Instantiate(bomb, muzzle.transform.position , Quaternion.identity);
        go.GetComponentInChildren<DemolishingBomb>().addForce(dir);
    }

    public void weakJump(bool inLq)
    {
        if (inLq)
        {
            jumpAccel_y = 1000 / rb.mass + 10 * rb.gravityScale;
        }
        else
        {
            jumpAccel_y = 2700 / rb.mass + 10 * rb.gravityScale;
        }
    }

    public bool isSuperMode()
    {
        return superMode;
    }

    // 애니메이션 지원 메소드
    public void onActionActive()
    {
        actionActive = true;
    }

    public void offActionActive()
    {
        actionActive = false;
    }

    public void setState(CameraState cState)
    {
        camEf.setState(cState);
    }

    public void setSwayTime( float time )
    {
        camEf.setSwayTime(time);
    }

    private void zoomIn()
    {
        camEf.zoomIn();
    }

    private void zoomOut()
    {
        camEf.zoomOut();
    }

    private void onZoomWorking()
    {
        camEf.setZoomWorking(true);
    }

    private void offZoomWorking()
    {
        camEf.setZoomWorking(false);
    }
}
