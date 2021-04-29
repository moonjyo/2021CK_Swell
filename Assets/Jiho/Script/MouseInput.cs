using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{

    //public ObserveMode MouseFunction;
    //public UIInventory uiInventory;

    private bool IsLeftMousePressed;

    public void OnLook(InputAction.CallbackContext context)
    {
        if(context.control.device.GetType() == typeof(Mouse))
        {
            if(!IsLeftMousePressed)
            {
                //MouseFunction.SetRotateInput(Vector2.zero);
                GameManager.Instance.uiManager.uiInventory.ob.SetRotateInput(Vector2.zero);
                return;
            }
        }
        
        Vector2 Input = context.ReadValue<Vector2>();
        //if (!uiInventory.IsInventoryWindowOpen)
        if(!GameManager.Instance.uiManager.uiInventory.IsInventoryWindowOpen)
        {
            //MouseFunction.SetRotateInput(Input);
            GameManager.Instance.uiManager.uiInventory.ob.SetRotateInput(Input);
        }
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

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        //if (IsSelectItemIcon)
        //{
            // 마우스 포지션 전달
        //}
        Vector2 input = context.ReadValue<Vector2>();
        //uiInventory.SetMousePosVal(input);
        GameManager.Instance.uiManager.uiInventory.SetMousePosVal(input);
        
    }
}
