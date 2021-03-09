using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private bool IsRunning = false;
    private bool IsPress = false;
    private int PressKeyCount;

    [SerializeField]
    private InputAction InputAction;

    public void OnWalk(InputAction.CallbackContext context)
    {
        
        Vector2 InputValue = context.ReadValue<Vector2>();
        Vector3 MoveVec = new Vector3(InputValue.x, 0, 0);
        PlayerManager.Instance.playerMove.SetMove(MoveVec, IsRunning);
        if(InputValue.x == 0)
        {
            Debug.Log("아무것도 누르지않음.");
            PlayerManager.Instance.playerMove.MoveFunction = PlayerManager.Instance.playerMove.Idle;
            IsPress = false;
        }
        else
        {
            Debug.Log("누르는중.");
            IsPress = true;
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed && !IsRunning)
        {
                IsRunning = true;
                Debug.Log("Run 시작");
                return;
            
        }
    }
    [System.Obsolete]
    public void OnRunPress(InputAction.CallbackContext context)
    {
        if (context.performed && IsRunning && context.duration >= 0.1f)
        {
            IsRunning = false;
            Debug.Log("Run 종료");
            PlayerManager.Instance.playerMove.PlayerAnim.SetBool("Run", false);
            PlayerManager.Instance.playerMove.MoveFunction = PlayerManager.Instance.playerMove.Idle;
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
