using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public void OnWalk(InputAction.CallbackContext context)
    {
      
      Vector2 InputValue  =  context.ReadValue<Vector2>();
      Vector3 MoveVec = new Vector3(InputValue.x, 0, InputValue.y);
      
      
      PlayerManager.Instance.playerMove.SetWalkValue(MoveVec);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase.ToString());

        if (context.performed)
        {
            Vector2 InputValue = context.ReadValue<Vector2>();
            Vector3 WalkVec= new Vector3(InputValue.x, 0, InputValue.y);

            PlayerManager.Instance.playerMove.SetRunValue(WalkVec);
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
