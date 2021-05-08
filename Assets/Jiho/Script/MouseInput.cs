using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    //public ObserveMode MouseFunction;
    //public UIInventory uiInventory;

    private bool IsLeftMousePressed;
    

    Vector2 MoveInput;
    Vector2 BeforeVec;
    Vector2 AfterVec;

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

        if (context.started)
        {
            BeforeVec = MoveInput;
            GameManager.Instance.uiManager.uiInventory.ob.CheckMouseDown();
        }
        else if(context.canceled)
        {
            AfterVec = MoveInput;
        }

        if(AfterVec == BeforeVec)
            GameManager.Instance.uiManager.uiInventory.ob.SetClickInput(true);
        else
            GameManager.Instance.uiManager.uiInventory.ob.SetClickInput(false);

    }
    public void OnMousePosition(InputAction.CallbackContext context)
    {
        //if (IsSelectItemIcon)
        //{
        // 마우스 포지션 전달
        //}
        MoveInput = context.ReadValue<Vector2>();
        //uiInventory.SetMousePosVal(input);
        GameManager.Instance.uiManager.uiInventory.SetMousePosVal(MoveInput);
    }
}
