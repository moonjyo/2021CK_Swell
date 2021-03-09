using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private bool IsRunning = false;
    private int PressKeyCount;

    [SerializeField]
    private InputAction InputAction;

    public void OnWalk(InputAction.CallbackContext context)
    {
        
        Vector2 InputValue = context.ReadValue<Vector2>();
        Vector3 MoveVec = new Vector3(InputValue.x, 0, 0);
        Debug.Log(MoveVec.x);
        PlayerManager.Instance.playerMove.SetMove(MoveVec, IsRunning);
        if(InputValue.x == 0)
        {
            PlayerManager.Instance.playerMove.MoveFunction = PlayerManager.Instance.playerMove.Idle;
           
        }
        else
        {
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed && !IsRunning)
        {
                IsRunning = true;
                return;
            
        }
    }
    [System.Obsolete]
    public void OnRunPress(InputAction.CallbackContext context)
    {
        if (context.performed && IsRunning && context.duration >= 0.1f)
        {
            IsRunning = false;
            PlayerManager.Instance.playerMove.PlayerAnim.SetBool("Run", false);
            PlayerManager.Instance.playerMove.MoveFunction = PlayerManager.Instance.playerMove.Idle;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
       Vector2 value =  context.ReadValue<Vector2>();
       Vector3 JumpVec = new Vector3(0, value.y, 0);
        PlayerManager.Instance.playerMove.SetJump(JumpVec);

      
      
    }


    
}
