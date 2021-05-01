using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimState
{
    WALK = 2,
    PUSH = 3,
    PULL = 4,
    CROUNCH = 5, 
    DOWN = 6,
    GETITEM = 7,
    HOLD = 8,
    CANCEL = -1,

}

public class PlayerAnimationEvents : MonoBehaviour
{
    readonly public static string State = "State";
    public bool IsAnimStart = false;
    public Animator PlayerAnim;
    public void CrounchStart()
    {
        IsAnimStart = true;
    }

    //idle
    public void CrounchEnd()
    {
        IsAnimStart = false;
        PlayerManager.Instance.playerMove.IsGravity = false;;
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
        IsAnimStart = true;
    }
    public void DownEnd()
    {
        PlayerManager.Instance.playerMove.IsGravity = false;
        IsAnimStart = false;
    }

}
