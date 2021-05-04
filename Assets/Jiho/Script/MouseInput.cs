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
    bool IsLeftMouseClick;

    public delegate void OnClickEvent();
    public static event OnClickEvent OnClick;

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

        if (context.canceled)
        {
            IsLeftMouseClick = true;
            GameManager.Instance.uiManager.uiInventory.ob.SetClickInput(IsLeftMouseClick);
        }
        else
        {
            IsLeftMouseClick = false;
            GameManager.Instance.uiManager.uiInventory.ob.SetClickInput(IsLeftMouseClick);
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
