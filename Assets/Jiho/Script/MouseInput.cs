using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    public void OnLeftMouseButton(InputAction.CallbackContext context)
    {
        if(context.started || context.performed)
        {
            
        }
    }

    public void OnLeftButtonDrag(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Vector2 value = context.ReadValue<Vector2>();
            Debug.Log(value);
        }
    }
}
