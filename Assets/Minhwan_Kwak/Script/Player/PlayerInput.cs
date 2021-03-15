using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    public bool IsRunning = false;
    private int PressKeyCount;

    
    private Vector2 InputValue;

    public void OnWalk(InputAction.CallbackContext context)
    {
        InputValue = context.ReadValue<Vector2>();

        Vector3 MoveVec = new Vector3(InputValue.x, 0, 0);
        PlayerManager.Instance.playerMove.SetMove(MoveVec, IsRunning);
        PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Move);
        if (InputValue.x == 0)
        {
            IsRunning = false;
            PlayerManager.Instance.playerMove.MoveFunction = PlayerManager.Instance.playerMove.Idle;

            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Move);
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed && !IsRunning)
        {
            IsRunning = true;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
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
