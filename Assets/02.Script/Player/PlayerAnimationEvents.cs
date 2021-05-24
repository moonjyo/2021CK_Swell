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
        //PlayerManager.Instance.playerMove.transform.DOMoveX(PlayerManager.Instance.playerMove.transform.position.x + -0.35f, 1f);
        Vector3 forward = transform.forward * testvec.x;
        transform.DOMove(transform.position + forward, 1f);
    }

    public void CrawlMoveOff()
    {
        PlayerManager.Instance.playerMove.IsGravity = false;
        //PlayerManager.Instance.playerMove.transform.DOMoveX(PlayerManager.Instance.playerMove.transform.position.x + 0.7f, 1f);
        Vector3 forward = transform.forward * testvec.x;
        transform.DOMove(transform.position - forward, 1f);
        GameManager.Instance.uiManager.DialogueText.DialogueCount(11, 12);
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
    }

    public void CrawlEnd()
    {
        PlayerManager.Instance.playerMove.IsGravity = false;
        IsAnimStart = false;
    }

}
