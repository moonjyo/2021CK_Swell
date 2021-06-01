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

    private Action DelWalkRun;



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

    public Vector2 DownOffsetVec;

    public bool InterActionUIPressed = false;

    public PlayerData playerData;

    public bool IsInterActionItemPress = false;

    private Vector3 WalkMove;

    public Action<float , float , AnimState> ClimbingAction;
    private void FixedUpdate()
    {
        MoveCheck();
        //PushItemCheck();
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

                        Vector3 Direction = VectorTruncate(transform.forward.x, transform.forward.y, transform.forward.z);

                        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.HOLD);


                        Vector3 ChangeMove;
                        ChangeMove.x = (float)Math.Truncate(WalkVec.z * 10) / 10;
                        ChangeMove.y = (float)Math.Truncate(WalkVec.y * 10) / 10;
                        ChangeMove.z = (float)Math.Truncate(WalkVec.x * 10) / 10;


                        if (-Direction == ChangeMove)
                        {  //당기기 

                          if (Direction.z == 1)
                            {
                                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.PULL);
                                ColumnMove();
                                return;
                            }

                            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.PUSH);
                            RowMove();
                        }
                        else if (Direction == ChangeMove)
                        { //밀기 

                           if (Direction.z == 1)
                            {
                                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.PUSH) ;
                                ColumnMove();
                                return;
                            }

                            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.PULL);
                            RowMove();
                        }
                    }
                }
            }
            else
            {
                if(!GameManager.Instance.eventCommand.IsRunning)
                {
                    DelWalkRun = BaseWalk;
                }
                else
                {
                    DelWalkRun = BaseRun;
                }
                DelWalkRun?.Invoke();
            }
        }
        else
        {
            Idle();
        }
    }

    //상호작용 오브젝트 밀고 당기기  행 
    public void RowMove()
    {
        WalkMove = -CameraManager.Instance.StageCam.BaseCam.transform.forward * WalkVec.x +
                        -CameraManager.Instance.StageCam.BaseCam.transform.right * WalkVec.z;
        Vector3 walkvec = WalkMove * Time.fixedDeltaTime * playerData.WalkSpeed;
        InterActionrb.MovePosition(walkvec + InterActionrb.transform.position);
        Controller.Move(walkvec);
    }
    //열 
    public void ColumnMove()
    {
        WalkMove = CameraManager.Instance.StageCam.BaseCam.transform.forward * WalkVec.x +
                                CameraManager.Instance.StageCam.BaseCam.transform.right * WalkVec.z;

        Vector3 dwalkvec = WalkMove * Time.fixedDeltaTime * playerData.WalkSpeed;
        InterActionrb.MovePosition(dwalkvec + InterActionrb.transform.position);
        Controller.Move(dwalkvec);
    }

    public void Idle()
    {
        if (IsInterActionItemPress)
        {
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.HOLD);
        }
    }
    public void BaseWalk()
    {

       Vector3 WalkMove = CameraManager.Instance.StageCam.BaseCam.transform.forward * WalkVec.x + -CameraManager.Instance.StageCam.BaseCam.transform.right * WalkVec.z;


        //if (!CameraManager.Instance.StageCam.IsLside)
        //{ve.y, 0, WalkMove.z);
        //}
        //else
        //{
        //    WalkMove = new Vector3(-WalkMove.y, 0, WalkMove.z);
        //}


        if (IsGrounded())
        {
            FunctionTimer.Create(OnWalkSound, playerData.WalkSoundTIme, "WalkSoundTimer");

            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.WALK);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetFloat(PlayerAnimationEvents.Velocity, (int)AnimState.WALK);
        }
        //    WalkMove = new Vector3(WalkMo
        Vector3 VecLook = transform.position + WalkMove;
        Body_Tr.DOLookAt(new Vector3(VecLook.x , transform.position.y , VecLook.z) , 0.25f);
  
       Controller.Move(WalkMove * Time.fixedDeltaTime * playerData.WalkSpeed);
    }
    public void BaseRun()
    {
        PlayerManager.Instance.playerMove.IsGravity = false;
        PlayerManager.Instance.playerMove.InterActionUIPressed = false;
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
        Vector3 WalkMove = CameraManager.Instance.StageCam.BaseCam.transform.forward * WalkVec.x + -CameraManager.Instance.StageCam.BaseCam.transform.right * WalkVec.z;


        //if (!CameraManager.Instance.StageCam.IsLside)
        //{
        //    WalkMove = new Vector3(WalkMove.y, 0, WalkMove.z);
        //}
        //else
        //{
        //    WalkMove = new Vector3(-WalkMove.y, 0, WalkMove.z);
        //}

        if (IsGrounded())
        {
            FunctionTimer.Create(OnRunSound, playerData.RunSoundTime, "WalkSoundTimer");

            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.WALK);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetFloat(PlayerAnimationEvents.Velocity, (int)AnimState.RUN);
        }
        Vector3 VecLook = transform.position + WalkMove;
        Body_Tr.DOLookAt(new Vector3(VecLook.x, transform.position.y, VecLook.z), 0.25f);
  
        Controller.Move(WalkMove * Time.fixedDeltaTime * playerData.RunSpeed);
    }


    private void OnWalkSound()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PlayerFoot", 0);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/SFX_Player_Foot", GetComponent<Transform>().position);
        //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Player/SFX_Player_Foot", this.gameObject);
    }
    private void OnRunSound()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PlayerFoot", 1);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/SFX_Player_Foot", GetComponent<Transform>().position);
        //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Player/SFX_Player_Foot", this.gameObject);
    }

    private bool IsGrounded()
    {
       
        bool IsCheckGround = Physics.CheckCapsule(Controller.bounds.center, new Vector3(Controller.bounds.center.x, Controller.bounds.min.y, Controller.bounds.center.z), 0.1f, GroundLayer);
      
        if(IsCheckGround)
        {
            GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.SHADOW].gameObject.SetActive(true);
        }
        else
        {
            GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.SHADOW].gameObject.SetActive(false);
        }

   
        return IsCheckGround;
    }

   
    public void ClimingJudge(float x , float y,AnimState state)
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)state);
        GameManager.Instance.uiManager.InterActionUICanvas.gameObject.SetActive(false);
        Vector3 forward = transform.forward * x;
        Vector3 up = transform.up * y;
        transform.DOMove(transform.position + forward + up, 1f).OnComplete(() =>
      {  
          InterActionUIPressed = false;
      });   
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

    private void MoveCheck()
    {
        if (!PlayerManager.Instance.playerAnimationEvents.IsAnimStart)
        {
              if (!IsGrounded())
              {
                Debug.Log("down");
                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.DOWN);
              }
              else
              {
                if (!PlayerManager.Instance.playerAnimationEvents.IsAnimStart)
                {
                    PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
                }
              }
              //hashflag  포함되어있는지 확인 
              if (MoveFunction != null)
              {
                 
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
            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.ItemTouch);
            PlayerManager.Instance.playerMove.SetRemoveInterActionObj();
        }

        return false;
    }


    public void InterActionUIPointDown(Rigidbody Targetrb)
    {
        if (Targetrb != null && !PlayerManager.Instance.playerAnimationEvents.IsAnimStart)
        {
            PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
            PlayerManager.Instance.playerMove.SetInterActionObj(Targetrb);
            PlayerManager.Instance.playerMove.transform.DOLookAt(new Vector3(Targetrb.transform.position.x, PlayerManager.Instance.playerMove.Body_Tr.position.y, Targetrb.transform.position.z), 0.5f).OnComplete(() =>
            {
                PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
            });
            Targetrb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.HOLD);
        }
    }

    public void InterActionUIPointUp()
    {
        PlayerManager.Instance.playerMove.IsInterActionItemPress = false;
        if (!InterActionUIPressed) 
        {
            PlayerManager.Instance.playerMove.SetRemoveInterActionObj();
        }
    }
   
}
