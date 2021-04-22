using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class PlayerMove : MonoBehaviour
{

    //키값이 들어와있는지 체크
    private Vector3 WalkVec;
    private Vector3 JumpVec;
    private Vector3 InterActionVec;
    private Vector3 MouseLeftVec;

    public LayerMask GroundLayer;
    public LayerMask InteractionLayer;

    public bool IsGravity = false;
    public Vector3 moveDirection;
    

    //현재 tr과 다른 자식 tr을 알기위해
    public Transform Root_Tr;
    public Transform Body_Tr;
    public Transform InterActionObjTr;

    //move를 가려내기 위해 
    public delegate void MoveDel();
    public MoveDel MoveFunction;

    //현재 매달려있는지 check
    public bool isHanging = false;

    //현재 들수 있는 item을 check한다 
    public LayerMask InterActionLayerMask;
    public Rigidbody InterActionrb;
    public GetInterActionItem GetInterActionItem;

    RaycastHit hitinfo;
    //물체와 충돌하기위한 bool
    public bool IsItemCol = false;
    public bool IsInterActionCol = false;

    public bool IsGetItem = false;

    public CharacterController Controller;



    public Vector2 ClimingOffsetVec;



    //현재 light를 가지고있는지 check 
    public bool IsLight = false;

    public PlayerData playerData;

    private void FixedUpdate()
    {
        //PushItemCheck();
        if (WalkVec == Vector3.zero && moveDirection.y < 0f)
        {
            Idle();
        }
        MoveCheck();
        GravityFall();
    }

    public void SetMove(Vector3 value)  // isRun = true(running) or isrun = false(walk)
    {
        WalkVec = value;
        MoveFunction = Walk;
    }

    private void GravityFall()
    {
        if (!IsGravity)
        {
            moveDirection.y -= playerData.Gravity * Time.fixedDeltaTime;
            if (moveDirection.y < playerData.GravityAcceleration)
            {
                moveDirection.y = playerData.GravityAcceleration;
            }
            Controller.Move(moveDirection * Time.fixedDeltaTime);
        }
    }
    public void SetLeftMouseOn(Vector3 value)
    {
        MouseLeftVec = value;
    }


    public void SetJump(Vector3 value) // jump check 
    {
        JumpVec = value;
    }
    public void SetInterAction(Vector3 value)
    {
        InterActionVec = value;
    }
    public void Walk()
    {
        if (WalkVec.sqrMagnitude > 0.1f)
        {
            DirectionSelect(); //방향성 check 
            if (InterActionrb != null)
            {
                if (IsGrounded() && !InterActionrb.CompareTag("InterActionItem") && !PlayerManager.Instance.PlayerInput.IsPickUpItem)
                {
                        if (InterActionrb != null)
                        {
                         PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Hold", true);
                         PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
                         float x  = (float)Math.Abs(transform.forward.x);
                         float y  = (float)Math.Abs(transform.forward.y);
                         float z = (float)Math.Abs(transform.forward.z);

                         x = (float)Mathf.Round(x);
                         y = (float)Mathf.Round(y);
                         z = (float)Mathf.Round(z);


                        if (Mathf.Abs(x) == 1 && Mathf.Abs(z) == 1)
                        {
                            x = 0.6f;
                            z = 0.6f;
                        }

                        if (Mathf.Sign(transform.forward.x) == -1)  {x  = -x; }
                        if (Mathf.Sign(transform.forward.y) == -1) { y = -y; }
                        if (Mathf.Sign(transform.forward.z) == -1) { z = -z; }



                        Vector3 Direction = new Vector3(x, y, z);

                        WalkVec.x =  (float)Math.Truncate(WalkVec.x * 10) / 10;
                        WalkVec.y = (float)Math.Truncate(WalkVec.y * 10) / 10;
                        WalkVec.z = (float)Math.Truncate(WalkVec.z * 10) / 10;
                        Debug.Log(Direction.x + "," + Direction.y + "," + Direction.z);
                        Debug.Log(WalkVec.x + "," + WalkVec.y + "," + WalkVec.z);
                        
                        if (-Direction == WalkVec) 
                            {  //당기기   
                                Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * playerData.PullSpeed;
                                InterActionrb.constraints = RigidbodyConstraints.FreezeRotation;
                                PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Pull);
                                Controller.Move(WalkMove);
                                InterActionrb.MovePosition(WalkMove + InterActionrb.transform.position);
                            }
                            else if (Direction == WalkVec)
                            { //밀기 
                                Vector3 WalkMove = WalkVec * playerData.PushSpeed * Time.fixedDeltaTime;
                                PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Push);
                                InterActionrb.constraints = RigidbodyConstraints.FreezeRotation;
                                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", true);
                                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Idle", false);
                                transform.LookAt(transform.position + WalkVec);
                                InterActionrb.MovePosition(WalkMove + InterActionrb.transform.position);

                            //물체가 벽에 부딫쳤을경우 
                              if (InterActionrb.velocity.x != 0 || InterActionrb.velocity.z != 0) 
                              {
                                return;
                              }
                            Controller.Move(WalkMove);
                        }
                            else if (transform.right == WalkVec)
                            { // 회전 right
                                Vector3 RotateVec = new Vector3(0, 0, WalkVec.x + WalkVec.z);
                                PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Pull);
                                InterActionrb.transform.Rotate(RotateVec);
                            }
                            else if (-transform.right == WalkVec)
                            { //회전 left 
                                Vector3 RotateVec = new Vector3(0, 0, WalkVec.x + WalkVec.z);
                                PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Pull);
                                InterActionrb.transform.Rotate(RotateVec);
                            }
                            else
                            {
                                if (InterActionrb.CompareTag("DirectionItem"))
                                {
                                    BaseWalk();
                                    return;
                                }
                            }
                        }
                    
                    else
                    {
                        BaseWalk();
                    }
                }
            }
            else if(!IsInterActionCol)
            {
                BaseWalk();
            }
        }
    }

    public void Idle()
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", false);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
        PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Walk);
        PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Push);
        PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Pull);

    }
    public void BaseWalk()
    {
        Vector3 WalkMove = WalkVec * playerData.WalkSpeed * Time.fixedDeltaTime;
        if (IsGrounded())
        {
            FunctionTimer.Create(OnWalkSound, playerData.WalkSoundTIme, "WalkSoundTimer");
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", true);
            PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Walk);
           
        }
        Body_Tr.LookAt(transform.position + WalkVec);
        Vector3 test = transform.TransformDirection(Vector3.forward);
        Controller.Move(WalkMove);
    }

    private void OnWalkSound()
    {
        //AudioManager.Instance.PlayOneShot("event:/Player/Walk");
    }



    public void Jump()
    {
        if (JumpVec.sqrMagnitude > 0.1f && PlayerManager.Instance.PlayerInput.IsJumpCanceled)
        {
            bool ispush = PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Push);
            bool ispull = PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Pull);
            if ((ispull || ispush) && IsInterActionCol)
            {
                return;
            }
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", true);
            //AudioManager.Instance.PlayOneShot("event:/Jump");q
            PlayerManager.Instance.PlayerInput.IsJumpCanceled = false;

            moveDirection.y = playerData.jumpspeed;
        }
    }

    private bool IsGrounded()
    {
        bool IsCheckGround = Physics.CheckCapsule(Controller.bounds.center, new Vector3(Controller.bounds.center.x, Controller.bounds.min.y, Controller.bounds.center.z), 0.1f, GroundLayer);
        if (IsCheckGround)
        {
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", false);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Falling", false);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.ResetTrigger("HangIdle");
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.ResetTrigger("BranchToCrounch");
            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Climing);
        }
        return IsCheckGround;
    }

    public void climing()
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetTrigger("BranchToCrounch");
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", false);
        ClimingJudge();
        PlayerManager.Instance.playerStatus.FsmAllRemove();
    }


    public void HangingOff()
    {
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
        IsGravity = false;
        PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Climing);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("HangIdle", false);

    }

    public void HangingOn(Vector2 InValue , Transform TargetPos)
    {
        if (InValue.sqrMagnitude > 0.1f && !PlayerManager.Instance.playerAnimationEvents.IsAnimStart && HangingJudge() && 
            PlayerManager.Instance.playerStatus.fsm != PlayerFSM.Climing)
        {
            PlayerManager.Instance.playerMove.IsGravity = true;
            
            PlayerManager.Instance.playerStatus.fsm = PlayerFSM.Climing;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("HangIdle", true);

            GameObject child = TargetPos.Find("TargetHangingPos").gameObject;
            if (child == null)
            {
                return;
            }

                Root_Tr.transform.localPosition = new Vector3(Root_Tr.transform.localPosition.x, child.transform.localPosition.y, child.transform.localPosition.z);
           

        }
    }
    public IEnumerator InterActionItemPickUp()
    {
        if (GetInterActionItem == null)
        {
            yield break;
        }

        GetInterActionItem InteractionBase = GetInterActionItem.GetComponent<GetInterActionItem>();
        //domove하고 끝나면 해당 target transform에 계속 update 

        if (InteractionBase != null)
        {
            Controller.radius = 0.5f;
            GetInterActionItem.transform.DOMove(InterActionObjTr.position, 2f).Complete(InteractionBase.FollowOn());
            InteractionBase.Col.isTrigger = false;
        }
        yield return null;
    }
    public IEnumerator InterActionItemPickDown()
    {
        if (GetInterActionItem == null)
        {
            yield break;
        }
        GetInterActionItem InteractionBase = GetInterActionItem.GetComponent<GetInterActionItem>();

        if (InteractionBase != null)
        {
            Controller.radius = 0.12f;
            InteractionBase.FollowOff();
        }

        GetInterActionItem = null;
        yield return null;
    }



    public void DirectionSelect()
    {

        if (WalkVec.x == 1f && WalkVec.z == 0f)
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Top;
        }
        else if (WalkVec.x == -1f && WalkVec.z == 0f)
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Bottom;
        }
        else if (WalkVec.x == 0f && WalkVec.z == 1f)
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Left;
        }
        else if (WalkVec.x == 0f && WalkVec.z == -1f)
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Right;
        }
    }
    public void ClimingJudge()
    {

        switch (PlayerManager.Instance.playerStatus.direction)
        {
            case PlayerDirection.Left:
                transform.DOMove(transform.position + new Vector3(0, ClimingOffsetVec.y, ClimingOffsetVec.x), 1f);
                break;
            case PlayerDirection.Right:
                transform.DOMove(transform.position + new Vector3(0, ClimingOffsetVec.y, -ClimingOffsetVec.x), 1f);
                break;
            case PlayerDirection.Bottom:
                transform.DOMove(transform.position + new Vector3(ClimingOffsetVec.x, ClimingOffsetVec.y, 0), 1f);
                break;
            case PlayerDirection.Top:
                transform.DOMove(transform.position + new Vector3(-ClimingOffsetVec.x, ClimingOffsetVec.y, 0), 1f);
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
    public void ItemMoveDecide(float MasValue)
    {
        //push and pull on 
        if (MasValue <= 10)
        {
            //item lift
            if (MasValue <= 2 && PlayerManager.Instance.PlayerInput.IsPickUpItem)
            {

                return;
            }
            //item pull
            else if (PlayerManager.Instance.PlayerInput.IsPull)
            {
                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Pull", true);
                return;
            }

            //item push 
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", true);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", false);
        }
        //no move
        else if (MasValue <= 100)
        {

        }
    }
    private void MoveCheck()
    {
        if (!PlayerManager.Instance.playerAnimationEvents.IsAnimStart)
        {
            if (InterActionVec.sqrMagnitude > 0.1f)
            {
                PlayerManager.Instance.PlayerInput.IsPull = true;
            }
            if (!PlayerManager.Instance.playerAnimationEvents.IsAnimStart)
            {
                if (IsGrounded())
                {
                    Jump();
                }
                else
                {
                    PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Falling", true);

                }
                //hashflag  포함되어있는지 확인 
                if (MoveFunction != null)
                {
                    if (WalkVec == transform.forward && PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Wall))
                    {
                        return;
                    }
                    MoveFunction();
                }
            }
        }
    }

    public Rigidbody CurrentGetInterActionObj()
    {
        return InterActionrb;
    }

    public void SetRemoveInterActionObj()
    {
        if (InterActionrb != null)
        {
            InterActionrb.constraints = RigidbodyConstraints.FreezeAll;
            InterActionrb = null;
        }
    }
    public void SetRemoveGetItemObj()
    {
        PlayerManager.Instance.playerMove.IsItemCol = false;
    }
    public void SetInterActionObj(Rigidbody Col)
    {
        InterActionrb = Col;
    }

    public void SetGetItemObj(GetInterActionItem InterActionBase)
    {
        GetInterActionItem = InterActionBase;
    }
    public bool PushItemCheck()
    {
        bool isitemcheck = Physics.CheckCapsule(Controller.bounds.center, new Vector3(Controller.bounds.center.x + transform.forward.x, Controller.bounds.center.y + transform.forward.y, Controller.bounds.center.z + transform.forward.z), 0.15f, InterActionLayerMask);

        if (isitemcheck)
        {
            return true;
        }
        else
        {
            PlayerManager.Instance.playerMove.IsInterActionCol = false;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.ItemTouch);
            PlayerManager.Instance.playerMove.SetRemoveInterActionObj();
        }

        return false;
    }

   
}
