﻿using System.Collections;
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

    public bool InterActionUIPressed = false;

    //현재 light를 가지고있는지 check 
    public bool IsLight = false;

    public PlayerData playerData;

    private void FixedUpdate()
    {
        MoveCheck();
        //PushItemCheck();
        if (WalkVec == Vector3.zero && moveDirection.y < 0f)
        {
            Idle();
        }
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
            if (InterActionrb != null)
            {
                if (IsGrounded() && !InterActionrb.CompareTag("InterActionItem") && !PlayerManager.Instance.PlayerInput.IsPickUpItem)
                {
                        if (InterActionrb != null)
                        {
                         PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Hold", true);
                         PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);


                        Vector3 Direction = VectorTruncate(transform.forward.x, transform.forward.y, transform.forward.z);
                       


                        WalkVec.x =  (float)Math.Truncate(WalkVec.x * 10) / 10;
                        WalkVec.y = (float)Math.Truncate(WalkVec.y * 10) / 10;
                        WalkVec.z = (float)Math.Truncate(WalkVec.z * 10) / 10;
                        
                        if (-Direction == WalkVec) 
                            {  //당기기   
                                Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * playerData.PullSpeed;
                                PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Pull);
                            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Pull", true);
                            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
                            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Idle", false);
                            Controller.Move(WalkMove);
                                InterActionrb.MovePosition(WalkMove + InterActionrb.transform.position);
                            }
                            else if (Direction == WalkVec)
                            { //밀기 
                                Vector3 WalkMove = WalkVec * playerData.PushSpeed * Time.fixedDeltaTime;
                                PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Push);
                                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", true);
                                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Pull", false);
                                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Idle", false);
                                transform.LookAt(transform.position + WalkVec);
                                InterActionrb.MovePosition(WalkMove + InterActionrb.transform.position);
                                Controller.Move(WalkMove);
                        }
                            //else if (transform.right == WalkVec)
                            //{ // 회전 right
                            //    Vector3 RotateVec = new Vector3(0, 0, WalkVec.x + WalkVec.z);
                            //    PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Pull);
                            //    InterActionrb.transform.Rotate(RotateVec);
                            //}
                            //else if (-transform.right == WalkVec)
                            //{ //회전 left 
                            //    Vector3 RotateVec = new Vector3(0, 0, WalkVec.x + WalkVec.z);
                            //    PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Pull);
                            //    InterActionrb.transform.Rotate(RotateVec);
                            //}
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
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Pull", false);
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
            //AudioManager.Instance.PlayOneShot("event:/Jump");
            PlayerManager.Instance.PlayerInput.IsJumpCanceled = false;

            moveDirection.y = playerData.jumpspeed;
        }
    }

    private bool IsGrounded()
    {
       
        bool IsCheckGround = Physics.CheckCapsule(Controller.bounds.center, new Vector3(Controller.bounds.center.x, Controller.bounds.min.y, Controller.bounds.center.z), 0.1f, GroundLayer);
        Debug.Log(IsCheckGround);
   
        return IsCheckGround;
    }

   
    public void ClimingJudge()
    {
        DirectionSelect();
        switch (PlayerManager.Instance.playerStatus.direction)
        {

            case PlayerDirection.Left:
                transform.DOMove(transform.position + new Vector3(0, ClimingOffsetVec.y, ClimingOffsetVec.x), 1f).OnComplete(() =>
                { InterActionUIPressed = false; });
                break;
            case PlayerDirection.Right:
                transform.DOMove(transform.position + new Vector3(0, ClimingOffsetVec.y, -ClimingOffsetVec.x), 1f).OnComplete(() =>
                { InterActionUIPressed = false; });
                break;
            case PlayerDirection.Bottom:
                transform.DOMove(transform.position + new Vector3(-ClimingOffsetVec.x, ClimingOffsetVec.y, 0), 1f)
                    .OnComplete(() => { InterActionUIPressed = false; }); ;
                break;
            case PlayerDirection.Top:
                transform.DOMove(transform.position + new Vector3(ClimingOffsetVec.x, ClimingOffsetVec.y, 0), 1f).OnComplete(() =>
                { InterActionUIPressed = false; });
                break;
            case PlayerDirection.TopLeft:
                transform.DOMove(transform.position + new Vector3(ClimingOffsetVec.x, ClimingOffsetVec.y, ClimingOffsetVec.x), 1f).OnComplete(() =>
                { InterActionUIPressed = false; });
                break;
            case PlayerDirection.TopRight:
                transform.DOMove(transform.position + new Vector3(ClimingOffsetVec.x, ClimingOffsetVec.y, -ClimingOffsetVec.x), 1f).OnComplete(() =>
                { InterActionUIPressed = false; });
                break;
            case PlayerDirection.BottomLeft:
                transform.DOMove(transform.position + new Vector3(-ClimingOffsetVec.x, ClimingOffsetVec.y, ClimingOffsetVec.x), 1f).OnComplete(() =>
                { InterActionUIPressed = false; });
                break;
            case PlayerDirection.BottomRight:
                transform.DOMove(transform.position + new Vector3(-ClimingOffsetVec.x, ClimingOffsetVec.y,
                    -ClimingOffsetVec.x), 1f).OnComplete(() =>
                    { InterActionUIPressed = false; });
                break;
        }
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


    //임시 
    public Vector3 VectorTruncate(float x , float y , float z )
    {
        x = (float)Math.Abs(transform.forward.x);
        y = (float)Math.Abs(transform.forward.y);
        z = (float)Math.Abs(transform.forward.z);

        x = (float)Mathf.Round(x);
        y = (float)Mathf.Round(y);
        z = (float)Mathf.Round(z);


        if (Mathf.Abs(x) == 1 && Mathf.Abs(z) == 1)
        {
            x = 0.6f;
            z = 0.6f;
        }


      if (Mathf.Sign(transform.forward.x) == -1) { x = -x; }
      if (Mathf.Sign(transform.forward.y) == -1) { y = -y; }
      if (Mathf.Sign(transform.forward.z) == -1) { z = -z; }

      return new Vector3(x, y, z);

    }


    

    public void DirectionSelect()
    {
        Vector3 DirectionCheck = VectorTruncate(transform.forward.x, transform.forward.y, transform.forward.z);

        if (DirectionCheck == Vector3.right)
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Top;
        }
        else if (DirectionCheck == Vector3.left)
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Bottom;
        }
        else if (new Vector3(DirectionCheck.x , DirectionCheck.z, 0) == Vector3.up)
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Left;
        }
        else if (new Vector3(DirectionCheck.x, DirectionCheck.z, 0) == Vector3.down)
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.Right;
        }
        else if (DirectionCheck == new Vector3(0.6f, 0 , 0.6f))
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.TopLeft;
        }
        else if (DirectionCheck == new Vector3(0.6f, 0, -0.6f))
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.TopRight;
        }
        else if (DirectionCheck == new Vector3(-0.6f, 0, 0.6f))
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.BottomLeft;
        }
        else if (DirectionCheck == new Vector3(-0.6f, 0, -0.6f))
        {
            PlayerManager.Instance.playerStatus.direction = PlayerDirection.BottomRight;
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
              if (!IsGrounded())
              {
                  PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Down", true);
              }
              else
              {
                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Down", false);
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


    public void InterActionUIPointDown(Rigidbody Targetrb)
    {
        if (Targetrb != null)
        {


            PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
            PlayerManager.Instance.playerMove.SetInterActionObj(Targetrb);
            PlayerManager.Instance.playerMove.transform.DOLookAt(new Vector3(Targetrb.transform.position.x, PlayerManager.Instance.playerMove.Body_Tr.position.y, Targetrb.transform.position.z), 0.5f).OnComplete(() =>
            {
                PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
            });
            Targetrb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Hold", true);
        }

    }

    public void InterActionUIPointUp()
    {
        PlayerManager.Instance.playerMove.SetRemoveInterActionObj();
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Idle", true);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Hold", false);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);

    }
   
}
