using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    private Vector3 WalkVec;
    private Vector3 JumpVec;


    public Rigidbody rb;

    public BoxCollider GroundCheckCol;
    public BoxCollider RigidBodyCol;


    public LayerMask GroundLayer;


    public Transform Root_Tr;
    public Transform Body_Tr;

    public delegate void MoveDel();
    public MoveDel MoveFunction;

    private bool isRunCheck = false;
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
            if (MoveFunction != null && !PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Wall | PlayerFSM.Move))
            {
                MoveFunction();
            }
            if (IsGrounded())
            {
                Jump();
            }
        }
    }
    public void SetMove(Vector3 value ,bool isRun)  // isRun = true(running) or isrun = false(walk)
    {
        WalkVec = value;
        isRunCheck = isRun;
        if (!isRun)
        {
            MoveFunction = Walk;
        }
        else if(isRun)
        {
            MoveFunction = Run;
        }
    }


    public void SetJump(Vector3 value)
    {
        JumpVec = value;
    }


    public void Walk()
    {
        if (WalkVec.sqrMagnitude > 0.1f)
        {
            //player 방향에 따라 회전 
            if (WalkVec.x == 1)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
               PlayerManager.Instance.playerStatus.direction = PlayerDirection.Right; 

            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                PlayerManager.Instance.playerStatus.direction = PlayerDirection.Left;

            }


            Vector3 WalkMove = WalkVec * Time.deltaTime * PlayerManager.Instance.playerStatus.WalkSpeed;
         


            if (IsGrounded()) {PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", true); }



            rb.MovePosition(transform.position + WalkMove);
        }
    }
    public void Run()
    {
        if (WalkVec.sqrMagnitude > 0.1f)
        {
            //player 방향에 따라 회전 
            if (WalkVec.x == 1)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }


            Vector3 RunMove = WalkVec * Time.deltaTime * PlayerManager.Instance.playerStatus.RunSpeed;

            if (IsGrounded()) { PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Run", true); }



            rb.MovePosition(transform.position + RunMove);
        }
    }

    public void Idle()
    {

       if(isRunCheck)
          { PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Run", false); }
       else 
          { PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", false); }
           MoveFunction -= Idle;

    }


    public void Jump()
    {
       
        if (JumpVec.sqrMagnitude > 0.1f)
        {
            
           rb.AddForce(Vector3.up * PlayerManager.Instance.playerStatus.JumpPower);
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
        PlayerManager.Instance.playerMove.rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

    }

    public void ClimingRigidBodyFrezen()
    {
        PlayerManager.Instance.playerMove.rb.constraints = RigidbodyConstraints.FreezeAll;

    }


    public void climing()
    {
        Debug.Log("올라가는중");
        RigidBodyCol.center = BaseCenterColVec;
        RigidBodyCol.size = BaseSizeColVec;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetTrigger("BranchToCrounch");
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", false);
        if (PlayerManager.Instance.playerStatus.direction == PlayerDirection.Right)
        {
            transform.DOMove(transform.position + new Vector3(1.3f, 2.8f, 0), 1f);
        }
        else
        {
            transform.DOMove(transform.position + new Vector3(-1.3f, 2.8f, 0), 1f);
        }
        PlayerManager.Instance.playerStatus.FsmAllRemove();

    }
    
    public void Hanging(Vector2 InValue)
    {
        if (InValue.sqrMagnitude > 0.1f && !PlayerManager.Instance.playerAnimationEvents.IsAnimStart)
        {
            Debug.Log("메달리는중");
            PlayerManager.Instance.playerStatus.fsm = PlayerFSM.Climing;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetTrigger("HangIdle");
        }
    }
}
