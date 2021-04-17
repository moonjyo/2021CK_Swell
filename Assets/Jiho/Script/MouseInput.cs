using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{

    public RotateByMouse MouseFunction;

    private bool IsLeftMousePressed;

    public void OnLook(InputAction.CallbackContext context)
    {
        if(context.control.device.GetType() == typeof(Mouse))
        {
            if(!IsLeftMousePressed)
            {
                return;
            }
        }

        Vector2 Input = context.ReadValue<Vector2>();
        MouseFunction.SetRotateInput(Input);
    }

    public void OnLeftMouseButton(InputAction.CallbackContext context)
    {
        if(context.started || context.performed)
        {
            IsLeftMousePressed = true;
        }
        else
        {
            IsLeftMousePressed = false;
        }
    }
}
