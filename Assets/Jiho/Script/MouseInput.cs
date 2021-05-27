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
            GameManager.Instance.uiManager.uiInventory.ob.CheckMouseUp();
        }
        //Vector2 Difference = AfterVec - BeforeVec; 
        //if(Difference.sqrMagnitude < 2.0f ) // 처음 MouseDown과 MonseUp의 포지션 오차값 조정? sqrMagnitude는 벡터 길이를 제곱한 값이므로 플레이해보며 체크
        //{

        //}

        if(AfterVec == BeforeVec)
            GameManager.Instance.uiManager.uiInventory.ob.SetClickInput(true);
        else
            GameManager.Instance.uiManager.uiInventory.ob.SetClickInput(false);

    }
    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();

        GameManager.Instance.uiManager.uiInventory.SetMousePosVal(MoveInput);
        GameManager.Instance.uiManager.uiInventory.ob.SetMoveInput(MoveInput);



    }
}
