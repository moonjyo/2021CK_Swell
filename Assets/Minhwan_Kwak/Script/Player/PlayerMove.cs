using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    private Vector3 WalkVec;
    private Vector3 JumpVec;
    private Vector3 HideWalkVec;

    public Rigidbody rb;

    public BoxCollider GroundCheckCol;
    public BoxCollider RigidBodyCol;


    public LayerMask GroundLayer;


    public Transform Root_Tr;
    public Transform Body_Tr;

    public delegate void MoveDel();
    public MoveDel MoveFunction;

    public bool isHanging = false;



    private Vector3 BaseCenterColVec;
    private Vector3 BaseSizeColVec;

    private Vector3 JumpColCenterVec;
    private Vector3 JumpColSizeVec;



    private void Start()
    {
        BaseCenterColVec = new Vector3(RigidBodyCol.center.x, 0.86f, RigidBodyCol.center.z);
        BaseSizeColVec = new Vector3(RigidBodyCol.size.x, 1.65f, RigidBodyCol.size.z);

        JumpColCenterVec = new Vector3(RigidBodyCol.center.x, 1.19f, RigidBodyCol.center.z);
        JumpColSizeVec = new Vector3(RigidBodyCol.size.x, 1.0f, RigidBodyCol.size.z);
    }


    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.playerAnimationEvents.IsAnimStart)
        {
            //hashflag  포함되어있는지 확인 
            if (MoveFunction != null && !PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Wall))
            {
                 MoveFunction();
            }
            if (IsGrounded())
            {
                Jump();
            }
        }
    }

    public void SetMove(Vector3 value)  // isRun = true(running) or isrun = false(walk)
    {
        WalkVec = value;
        MoveFunction = Walk;
    }

    public void SetHideMoveCheck(Vector3 value)
    {
        HideWalkVec = value;
    }
    
    public void SetJump(Vector3 value)
    {
        JumpVec = value;
    }

    public void Walk()
    {
        if (WalkVec.sqrMagnitude > 0.1f)
        {
            DirectionSelect();
            if (HideWalkVec.sqrMagnitude > 0.1f)
            {
                Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * PlayerManager.Instance.playerStatus.HideWalkSpeed;
                if (IsGrounded())
                {
                    PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("SneakWalk", true);
                    PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Walk);
                    PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.HideWalk);
                }
                transform.LookAt(transform.position + WalkVec);
                rb.MovePosition(transform.position + WalkMove);
            }
            else
            {
                Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * PlayerManager.Instance.playerStatus.WalkSpeed;

                if (IsGrounded())
                {
                    PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", true);
                    PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Walk);
                    PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.HideWalk);
                }

                transform.LookAt(transform.position + WalkVec);
                rb.MovePosition(transform.position + WalkMove);
            }
        }
    }

    public void HideWalk()
    {
        if (WalkVec.sqrMagnitude > 0.1f)
        {
            //player 방향에 따라 회전 
       
            Vector3 RunMove = WalkVec * Time.deltaTime * PlayerManager.Instance.playerStatus.HideWalkSpeed;

            if (IsGrounded()) { PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("SneakWalk", true); }

            transform.LookAt(transform.position + WalkVec);
            rb.MovePosition(transform.position + RunMove);
        }
    }

    public void Idle()
    {
           PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("SneakWalk", false); 
           PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", false); 
           MoveFunction -= Idle;
    }


    public void Jump()
    {
       
        if (JumpVec.sqrMagnitude > 0.1f)
        {
           
            Debug.Log(rb.velocity.magnitude);
            rb.velocity = Vector3.up * PlayerManager.Instance.playerStatus.JumpPower;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", true);
            RigidBodyCol.center = JumpColCenterVec;
            RigidBodyCol.size = JumpColSizeVec;

        }
    }

    private bool IsGrounded()
    {
        bool IsCheckGround =  Physics.Raycast(GroundCheckCol.bounds.center, Vector3.down, GroundCheckCol.bounds.extents.y + 0.1f , GroundLayer);
        if (IsCheckGround)
        {
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", false);
            RigidBodyCol.center = BaseCenterColVec;
            RigidBodyCol.size = BaseSizeColVec;
        }
        return IsCheckGround;
    }


    public void BaseRigidBodyFrezen()
    {
        PlayerManager.Instance.playerMove.rb.constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

    }

    public void ClimingRigidBodyFrezen()
    {
        PlayerManager.Instance.playerMove.rb.constraints = RigidbodyConstraints.FreezeAll;
    }


    public void climing()
    {
        RigidBodyCol.center = BaseCenterColVec;
        RigidBodyCol.size = BaseSizeColVec;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetTrigger("BranchToCrounch");
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", false);
        ClimingJudge();
        PlayerManager.Instance.playerStatus.FsmAllRemove();

    }
    
    public void Hanging(Vector2 InValue)
    {
        if (InValue.sqrMagnitude > 0.1f && !PlayerManager.Instance.playerAnimationEvents.IsAnimStart && HangingJudge())
        {
            PlayerManager.Instance.playerStatus.fsm = PlayerFSM.Climing;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetTrigger("HangIdle");
        }
    }

    public void DirectionSelect()
    {
      
        if (WalkVec.x  == 1f && WalkVec.z == 0f)//right
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Right;
        }
        else if (WalkVec.x == -1f && WalkVec.z == 0f)//left
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Left;
        }
        else if (WalkVec.x == 0f && WalkVec.z == 1f)//top
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Top;
        }
        else if (WalkVec.x == 0f && WalkVec.z == -1f)//Bottom
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Bottom;
        }
        else if (Mathf.Sign(WalkVec.x) == 1 && Mathf.Sign(WalkVec.z) == 1)//top right 
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.TopRight;
        }
        else if (Mathf.Sign(WalkVec.x) == 1 && Mathf.Sign(WalkVec.z) == -1)//top right 
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.BottomRight;
        }
        else if (Mathf.Sign(WalkVec.x) == -1 && Mathf.Sign(WalkVec.z) == 1)//top right 
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.TopLeft;
        }
        else if (Mathf.Sign(WalkVec.x) == -1 && Mathf.Sign(WalkVec.z) == -1)//top right 
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.BottomLeft;
        }
    }
    public void ClimingJudge()
    {

        switch(PlayerManager.Instance.playerStatus.direction)
        {
            case PlayerDirection.Right:
                transform.DOMove(transform.position + new Vector3(0.8f, 2.8f, 0), 1f);
                break;
            case PlayerDirection.Left:
                transform.DOMove(transform.position + new Vector3(-0.8f, 2.8f, 0), 1f);
                break;
            case PlayerDirection.Top:
                transform.DOMove(transform.position + new Vector3(0, 2.8f, 0.8f), 1f);
                break;
            case PlayerDirection.Bottom:
                transform.DOMove(transform.position + new Vector3(0, 2.8f, -0.8f), 1f);
                break;
            default:
                Debug.Log("잘못된 방향");
                break;
        }
    }
    public bool HangingJudge()
    {
        switch (PlayerManager.Instance.playerStatus.direction)
        {
            case PlayerDirection.Right:
                return true;
            case PlayerDirection.Left:
                return true;
            case PlayerDirection.Top:
                return true;
            case PlayerDirection.Bottom:
                return true;
            default:
                Debug.Log("잘못된 방향");
                return false;
        }
    }

}
