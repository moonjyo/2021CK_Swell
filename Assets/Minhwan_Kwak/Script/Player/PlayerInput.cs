using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private bool IsRunning = false;
    private int PressKeyCount;
    public void OnWalk(InputAction.CallbackContext context)
    {
        Vector2 InputValue = context.ReadValue<Vector2>();
        Vector3 MoveVec = new Vector3(InputValue.x, 0, InputValue.y);
        PlayerManager.Instance.playerMove.SetMove(MoveVec , IsRunning);
        //액션이 눌러진상태
        
     
    }

    public void OnWalkPress(InputAction.CallbackContext context)
    {
        if (context.performed && context.duration > 0.01f && !IsRunning)
        {
            IsRunning = false;

            Debug.Log("Walk 종료");
            PlayerManager.Instance.playerMove.MoveFunction = PlayerManager.Instance.playerMove.Idle;
        }
        

    }


    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed && !IsRunning && context.duration >= 0.07f)
        {
            IsRunning = true;
            Debug.Log("Run 시작");
            return;

        }
    }
    public void OnRunPress(InputAction.CallbackContext context)
    {
        if (context.performed && IsRunning && context.duration > 0.1f)
        {
            IsRunning = false;

            Debug.Log("Run 종료");
            PlayerManager.Instance.playerMove.PlayerAnim.SetBool("Run", false);
            PlayerManager.Instance.playerMove.MoveFunction -= PlayerManager.Instance.playerMove.Idle;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase.ToString());

        if (context.performed)
        {

        }
    }
}
