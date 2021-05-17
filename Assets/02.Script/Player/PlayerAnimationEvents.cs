using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


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

    CANCEL = -1,

}

public class PlayerAnimationEvents : MonoBehaviour
{
    readonly public static string State = "State";
    public bool IsAnimStart = false;
    public Animator PlayerAnim;
    public void CrounchStart()
    {
        PlayerManager.Instance.playerMove.IsGravity = true;
        IsAnimStart = true;
    }

    //idle
    public void CrounchEnd()
    {
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
        PlayerManager.Instance.playerMove.ClimingJudgeDown();
    }

    public void DownEnd()
    {

        PlayerManager.Instance.playerMove.IsGravity = false;
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
        PlayerManager.Instance.playerMove.transform.DOMoveX(PlayerManager.Instance.playerMove.transform.position.x + -0.35f, 1f);
    }

    public void CrawlMoveOff()
    {
        PlayerManager.Instance.playerMove.transform.DOMoveX(PlayerManager.Instance.playerMove.transform.position.x + 0.7f, 1f);
    }

    public void CrawlEnd()
    {
        PlayerManager.Instance.playerMove.IsGravity = false;
        IsAnimStart = false;
    }

}
