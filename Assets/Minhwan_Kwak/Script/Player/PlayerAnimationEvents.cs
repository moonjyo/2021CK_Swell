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
        
        PlayerManager.Instance.playerMove.BaseRigidBodyFrezen();
    }
   
    //idle
    public void CrounchEnd()
    {
        IsAnimStart = false;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.ResetTrigger("HangIdle");
    }

    public void HangingStart()
    {
        PlayerManager.Instance.playerMove.ClimingRigidBodyFrezen();
    }
}
