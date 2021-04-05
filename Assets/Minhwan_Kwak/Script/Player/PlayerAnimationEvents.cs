using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
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
        PlayerManager.Instance.playerMove.IsGravity = false;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.ResetTrigger("HangIdle");
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

    public void MoveOn()
    {
        IsAnimStart = false;
        PlayerManager.Instance.playerMove.IsGravity = false;
    }
}
