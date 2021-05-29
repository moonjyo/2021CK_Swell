using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[SerializeField]
public enum AnimState
{
    WALK = 2,
    PUSH = 3,
    PULL = 4,
    CROUNCH = 5, 
    DOWN = 6,
    GETITEM = 7,
    HOLD = 8,
    FRAME = 9,
    CRAWL = 10,
    PICKUPDOWN = 11,
    LOOKAROUND = 12,
    RUN = 13,
    STAIRCROUNCH = 14,
    CANCEL = -1,

}

public class PlayerAnimationEvents : MonoBehaviour
{
     public static int State;
     public static int Velocity;
    public bool IsAnimStart = false;
    public Animator PlayerAnim;

    public Vector2 CurrentClimingVec;
    public AnimState CurrentClimingState;

    public Vector2 testvec;
    private void Start()
    {
        State = Animator.StringToHash("State");
        Velocity = Animator.StringToHash("Velocity");
    }
    public void CrounchStart()
    {
        PlayerManager.Instance.playerMove.IsGravity = true;
        IsAnimStart = true; 
        PlayerManager.Instance.playerMove.ClimingJudge(CurrentClimingVec.x, CurrentClimingVec.y, CurrentClimingState);
    }

    //idle
    public void CrounchEnd()
    {
        GameManager.Instance.uiManager.InterActionUICanvas.gameObject.SetActive(true);
        IsAnimStart = false;
        PlayerManager.Instance.playerMove.IsGravity = false; 
    }

    public void HangingStart()
    {

        IsAnimStart = true;
    }

    public void JumpOff()
    {
        
          IsAnimStart = false;
          PlayerManager.Instance.playerMove.IsGravity = false;

    }

    public void IdleOn()
    {
        IsAnimStart = false;
        PlayerManager.Instance.playerMove.IsGravity = false;
    }


    public void DownStart()
    {
        PlayerManager.Instance.playerMove.IsGravity = true;
       // PlayerManager.Instance.playerMove.ClimingJudgeDown();
    }

    public void DownEnd()
    {

        PlayerManager.Instance.playerMove.IsGravity = false;
        IsAnimStart = false;
    }


    public void PickUpDownOn()
    {
        IsAnimStart = true;
    }

    public void PickUpDownOff()
    {
        IsAnimStart = false;
    }

    public void FrameStart()
    {
        IsAnimStart = true;
    }

    public void FrameEnd()
    {
        IsAnimStart = false;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
    }

    public void CrawlMoveOn()
    {
        PlayerManager.Instance.playerMove.IsGravity = true; 
        IsAnimStart = true;
        Vector3 forward = transform.forward * 0.7f;

        PlayerManager.Instance.playerMove.transform.DOMove(transform.position + forward , 1f).OnComplete(()=> {
            if (GameManager.Instance.uiManager.uiInventory.Distinguish.ProductionClickItem.TryGetValue("MSG_BGLR_key_1", out GameObject KeyObj))
            {
                GameManager.Instance.uiManager.uiInventory.GetItemIcon(KeyObj.GetComponent<PlayerInterActionObj>());
                GameManager.Instance.uiManager.DialogueText.DialogueCount(13, 14);
                GameManager.Instance.uiManager.DialogueText.ShowDialogue();
            }
        });
    }

    public void CrawlMoveOff()
    {
        Vector3 forward = transform.forward * 0.7f;
        PlayerManager.Instance.playerMove.transform.DOMove(transform.position - forward , 1f);
    }

    public void CrawlEnd()
    {
        FunctionTimer.Create(DogFind, 1.5f, "DogFind");
        PlayerManager.Instance.playerMove.IsGravity = false;
    }


    public void DogFind()
    {
        IsAnimStart = true;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        GameManager.Instance.uiManager.DialogueText.DialogueCount(10, 12);
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
    }


    public void SetCliming(Vector2 vec , AnimState state)
    {
        CurrentClimingVec = vec;
        CurrentClimingState = state;
    }
}
