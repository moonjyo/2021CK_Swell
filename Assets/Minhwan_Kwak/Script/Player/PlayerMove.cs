using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    //키값이 들어와있는지 체크
    private Vector3 WalkVec;
    private Vector3 JumpVec;
    private Vector3 HideWalkVec;
    private Vector3 PullVec;

    //가지고있는 rb
    public Rigidbody rb;

    
    public BoxCollider GroundCheckCol;



    public LayerMask GroundLayer;


    //현재 tr과 다른 자식 tr을 알기위해
    public Transform Root_Tr;
    public Transform Body_Tr;


    //move를 가려내기 위해 
    public delegate void MoveDel();
    public MoveDel MoveFunction;

    //현재 매달려있는지 check
    public bool isHanging = false;

    //현재 들수 있는 item을 check한다 
    public LayerMask ItemLayerMask;
    public Transform HitItemTr;

    //물체와 충돌하기위한 bool
    public bool isItemCol = false;

    public bool isitempick = false;

    public bool IsGetItem = false;



    [HideInInspector]
    public Rigidbody ColliderItemRb;



    public bool IsTime = false;
    private float deltime = 0f;
    private bool IsItemPickupSwitch = false;

    private void Start()
    {
    }
    

    private void FixedUpdate()
    {
        if(PlayerManager.Instance.PlayerInput.IsPickUpItem && ColliderItemRb != null && !IsGetItem) // item 줍기 여기서 mass 비교까지 해야
        {
            ItemPickUp();
        }
        OnItemMove();
        ItemTimeTick();
        MoveCheck();
       
    }

    public void SetMove(Vector3 value)  // isRun = true(running) or isrun = false(walk)
    {
        WalkVec = value;
        MoveFunction = Walk;
    }

    public void SetHideMoveCheck(Vector3 value) // hidemove check
    {
        HideWalkVec = value;
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
            if (HideWalkVec.sqrMagnitude > 0.1f) // 조심히걷기 
            {
                Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * PlayerManager.Instance.playerStatus.HideWalkSpeed;
                if (IsGrounded())
                {
                    PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("SneakWalk", true);
                    PlayerManager.Instance.playerStatus.fsm = PlayerFSM.HideWalk;
                }
                transform.LookAt(transform.position + WalkVec);
                rb.MovePosition(transform.position + WalkMove);
            }
            else if(isItemCol && IsGrounded() && ColliderItemRb != null)
            {
                if (PullVec.sqrMagnitude > 0.1f) //당길떄 
                {
                    if (-transform.forward == WalkVec)
                    {
                        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Pull", true);
                        ColliderItemRb.constraints = RigidbodyConstraints.FreezeRotation;
                        Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * PlayerManager.Instance.playerStatus.PullSpeed;
                        PlayerManager.Instance.playerStatus.fsm = PlayerFSM.Pull;
                        rb.MovePosition(transform.position + WalkMove);
                        ColliderItemRb.MovePosition(ColliderItemRb.transform.position + WalkMove);
                    }
                }
                else //물건 push 
                {
                    ColliderItemRb.constraints = RigidbodyConstraints.FreezeRotation;
                    Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * PlayerManager.Instance.playerStatus.PushSpeed;
                    ItemMoveDecide(ColliderItemRb.mass);
                    PlayerManager.Instance.playerStatus.fsm = PlayerFSM.Push;
                    transform.LookAt(transform.position + WalkVec);
                    rb.MovePosition(transform.position + WalkMove);
                }
            }
            else //그냥걷기 
            {
                Vector3 WalkMove = WalkVec * Time.fixedDeltaTime * PlayerManager.Instance.playerStatus.WalkSpeed;
                if (IsGrounded())
                {
                    PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Walk", true);
                    PlayerManager.Instance.playerStatus.fsm = PlayerFSM.Walk;
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
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
        PlayerManager.Instance.playerStatus.FsmAllRemove();
        MoveFunction -= Idle;
    }


    public void Jump()
    {
       
        if (JumpVec.sqrMagnitude > 0.1f)
        {
           bool ispush =  PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Push);
           bool ispull = PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Pull);
           if(ispull || ispush)
           {
               return;
           }
           Debug.Log(rb.velocity.magnitude);
           rb.velocity = Vector3.up * PlayerManager.Instance.playerStatus.JumpPower;
           PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", true);
            
        }
    }

    private bool IsGrounded()
    {
        //bool IsCheckGround =  Physics.CheckCapsule(GroundCheckCol.bounds.center, Vector3.down, GroundCheckCol.bounds.extents.y + 0.2f , GroundLayer);
        bool IsCheckGround = Physics.CheckCapsule(GroundCheckCol.bounds.center, new Vector3(GroundCheckCol.bounds.center.x, GroundCheckCol.bounds.min.y - 0.1f, GroundCheckCol.bounds.center.z), 0.18f,GroundLayer);
        if (IsCheckGround) 
        {
            Debug.Log("ground true");
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Jump", false); 
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Falling", false);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.ResetTrigger("HangIdle");
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.ResetTrigger("BranchToCrounch"); 
            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Climing);
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

    private void ItemPickUp()
    {
        if (!isitempick && !IsItemPickupSwitch)
        {
            IsItemPickupSwitch = true;
            Debug.Log("ItemPickup");
            rb.constraints = RigidbodyConstraints.FreezeAll;
            PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.ItemPickUp);
            ColliderItemRb.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), 1.0f).OnComplete(() =>
            {
                IsItemPickupSwitch = false;
                isitempick = true;
                DOTween.Kill(ColliderItemRb);
            });
        }
        
    }
    public void ItemPickDown()
    {
        if (ColliderItemRb != null)
        {
            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.ItemPickUp);
            ColliderItemRb.transform.DOMove(new Vector3(ColliderItemRb.position.x + (transform.forward.x * 2f), ColliderItemRb.position.y, ColliderItemRb.position.z + (transform.forward.z * 2f)), 0.4f);
            ColliderItemRb.constraints = RigidbodyConstraints.FreezeRotation;
            ColliderItemRb = null;
            isitempick = false;
            IsGetItem = false;
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

    public bool ItemColCheck(Transform tr)
    {
      ColliderItemRb =  tr.GetComponent<Rigidbody>();
       if(ColliderItemRb != null)
        {
            return true;
        }
        return false;
    }


    private void OnItemMove()
    {
        if (isitempick && ColliderItemRb != null)
        {
            IsTime = true;
            IsGetItem = true;
            ColliderItemRb.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
        }

    }

    private void ItemTimeTick()
    {
        if (IsTime)
        {
            deltime += Time.fixedDeltaTime;
        }
        if (deltime > 1.0f)
        {
            deltime = 0f;
            BaseRigidBodyFrezen();
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


}
