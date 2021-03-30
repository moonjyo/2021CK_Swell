using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    //키값이 들어와있는지 체크
    private Vector3 WalkVec;
    private Vector3 JumpVec;
    private Vector3 PullVec;

    public LayerMask GroundLayer;
    public LayerMask InteractionLayer;

    public Vector3 moveDirection;
    public float jumpspeed = 8.0f;
    public float Gravity = 20f;
    public float GravityAcceleration = 12f;


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
    private Rigidbody InterActionrb;

    //물체와 충돌하기위한 bool
    public bool isItemCol = false;

    public bool isitempick = false;

    public bool IsGetItem = false;

    public CharacterController Controller;
    
    private bool IsTime = false;
    private float deltime = 0f;
    


    public float PullSpeed;
    public float PushSpeed;
    public float WalkSpeed;

    

    private void FixedUpdate()
    {
        //if(PlayerManager.Instance.PlayerInput.IsPickUpItem && ColliderItemRb != null && !IsGetItem) // item 줍기 여기서 mass 비교까지 해야
        //{
        //    ItemPickUp();
        //}
        //OnItemMove();
        if (WalkVec == Vector3.zero && moveDirection.y < 0)
        {
            Idle();
        }

        ItemTimeTick();
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
        moveDirection.y -= Gravity * Time.fixedDeltaTime;
        if (moveDirection.y < GravityAcceleration)
        {
            moveDirection.y = GravityAcceleration;
        }
        Controller.Move(moveDirection * Time.fixedDeltaTime);
    }


    
    public void SetJump(Vector3 value) // jump check 
    {
        JumpVec = value;
    }
    public void SetPull(Vector3 value)
    {
        PullVec = value;
    }
    public void Walk()
    {
        if (WalkVec.sqrMagnitude > 0.1f)
        {
            DirectionSelect(); //방향성 check 

            if (isItemCol && IsGrounded())
            {
                if (PullVec.sqrMagnitude > 0.1f) //당길떄 
                {
                    if (-transform.forward == WalkVec && InterActionrb != null)
                    {
                        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Pull", true);
                        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
                        Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * PullSpeed;
                        PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Pull);
                        Controller.Move(WalkMove);
                        InterActionrb.MovePosition(WalkMove + InterActionrb.transform.position);
                    }
                }
                else //물건 push 
                {
                    if (InterActionrb != null)
                    {

                     
                        Vector3 WalkMove = WalkVec * PushSpeed * Time.fixedDeltaTime;
                        PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Push);
                        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", false);
                        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", true);
                        transform.LookAt(transform.position + WalkVec);
                        Controller.Move(WalkMove);
                        InterActionrb.MovePosition(WalkMove + InterActionrb.transform.position);
                    }
                }
            }
            else if(!isItemCol)//그냥걷기 
            {
                Vector3 WalkMove = WalkVec * WalkSpeed * Time.fixedDeltaTime; 
                if (IsGrounded())
                {
                    Debug.Log("walk");
                    PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", true);
                    PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Walk);
                }
                Body_Tr.LookAt(transform.position + WalkVec);
                Vector3 test = transform.TransformDirection(Vector3.forward);
                Controller.Move(WalkMove);
            }
        }
    }

    public void Idle()
    {
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", false);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
            PlayerManager.Instance.playerStatus.FsmAllRemove();
    }


    public void Jump()
    {
       if (JumpVec.sqrMagnitude > 0.1f && PlayerManager.Instance.PlayerInput.IsJumpCanceled)
        {
           bool ispush =  PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Push);
           bool ispull = PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Pull);
           if(ispull || ispush && isItemCol)
           {
               return;
           }
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", true);
            //AudioManager.Instance.PlayOneShot("event:/Jump");
            PlayerManager.Instance.PlayerInput.IsJumpCanceled = false;
          
            moveDirection.y = jumpspeed;

        }
    }

    private bool IsGrounded()
    {
        //bool IsCheckGround =  Physics.CheckCapsule(GroundCheckCol.bounds.center, Vector3.down, GroundCheckCol.bounds.extents.y + 0.2f , GroundLayer);
        bool IsCheckGround = Physics.CheckCapsule(Controller.bounds.center, new Vector3(Controller.bounds.center.x, Controller.bounds.min.y - 0.1f, Controller.bounds.center.z), 0.18f,GroundLayer);
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
    
    public void Hanging(Vector2 InValue)
    {
        if (InValue.sqrMagnitude > 0.1f && !PlayerManager.Instance.playerAnimationEvents.IsAnimStart && HangingJudge())
        {
            PlayerManager.Instance.playerStatus.fsm = PlayerFSM.Climing;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetTrigger("HangIdle");
        }
    }
    

    public IEnumerator InterActionItemPickUp()
    {
     InterActionObjBase InteractionBase  = InterActionrb.GetComponent<InterActionObjBase>();
        //domove하고 끝나면 해당 target transform에 계속 update 

        if (InteractionBase != null)
        {
            InterActionrb.DOMove(InterActionObjTr.position, 2f);
            InteractionBase.Col.enabled = false;
        }
       yield return null;
    }
    public IEnumerator InterActionItemPickDown()
    {


        yield return null;
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
    public void ItemMoveDecide(float MasValue)
    {
        //push and pull on 
        if(MasValue <= 10)
        {
            //item lift
            if(MasValue <= 2 && PlayerManager.Instance.PlayerInput.IsPickUpItem)
            {

                return; 
            }
            //item pull
            else if(PlayerManager.Instance.PlayerInput.IsPull)
            {
                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Pull", true);
                return;
            }

            //item push 
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", true);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", false);
        }
        //no move
        else if(MasValue <= 100)
        {

        }
    }

    //public bool ItemColCheck(Transform tr)
    //{
    //  ColliderItemRb =  tr.GetComponent<Rigidbody>();
    //   if(ColliderItemRb != null)
    //    {
    //        return true;
    //    }
    //    return false;
    //}


    //private void OnItemMove()
    //{
    //    if (isitempick && ColliderItemRb != null)
    //    {
    //        IsTime = true;
    //        IsGetItem = true;
    //        ColliderItemRb.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    //    }

    //}

    private void ItemTimeTick()
    {
        if (IsTime)
        {
            deltime += Time.fixedDeltaTime;
        }
        if (deltime > 1.0f)
        {
            deltime = 0f;
            IsTime = false;
        }

    }
    private void MoveCheck()
    {
        if (!PlayerManager.Instance.playerAnimationEvents.IsAnimStart)
        {
            if (PullVec.sqrMagnitude > 0.1f)
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


    public void SetInterActionObj(Rigidbody Col)
    {
        InterActionrb = Col;
    }

    public Rigidbody CurrentGetInterActionObj()
    {
        return InterActionrb;
    }

    public void SetRemoveInterActionObj()
    {
        InterActionrb = null;
    }
}
