using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    public bool IsHideWalk = false;
    
    private Vector2 InputValue;

    public void OnWalk(InputAction.CallbackContext context)
    {
        InputValue = context.ReadValue<Vector2>();

        Vector3 MoveVec = new Vector3(InputValue.x, 0, InputValue.y);
        PlayerManager.Instance.playerMove.SetMove(MoveVec);
        Debug.Log("Walk" + IsHideWalk);
        if (context.canceled)
        {
            Debug.Log("걷기종료");
            PlayerManager.Instance.playerMove.MoveFunction = PlayerManager.Instance.playerMove.Idle;

            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Walk);
        }
    }
    public void OnHideWalk(InputAction.CallbackContext context)
    {
       Vector2 vec = context.ReadValue<Vector2>();
        PlayerManager.Instance.playerMove.SetHideMoveCheck(vec);
        if (context.canceled)
        {
            Debug.Log("hide off");
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("SneakWalk", false);
            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.HideWalk);

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Vector3 JumpVec = new Vector3(0, value.y, 0);
        PlayerManager.Instance.playerMove.SetJump(JumpVec);
        PlayerManager.Instance.playerCliming.SetCliming(JumpVec);
            
        if(context.performed)
        {
            if (PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Climing))
            {
                PlayerManager.Instance.playerMove.climing();
            }
        }
        //climig test 
        if (context.canceled)
        {
            Debug.Log("climingoff");
            
        }
    }
}
