using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    private Vector2 InputValue;
    public bool IsPull = false;
    public bool IsPickUpItem = false;
    public bool IsJumpCanceled = false;

    public bool IsLightGet = false;
    public void OnWalk(InputAction.CallbackContext context)
    {
        InputValue = context.ReadValue<Vector2>();

        Vector3 MoveVec = new Vector3(InputValue.x, 0, InputValue.y);
        PlayerManager.Instance.playerMove.SetMove(MoveVec);

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Vector3 JumpVec = new Vector3(0, value.y, 0);
        PlayerManager.Instance.playerMove.SetJump(JumpVec);
        PlayerManager.Instance.playerCliming.SetCliming(JumpVec);

        if (context.performed)
        {
            IsJumpCanceled = true;
            if (PlayerManager.Instance.playerStatus.fsm.HasFlag(PlayerFSM.Climing))
            {
                PlayerManager.Instance.playerMove.climing();
            }
        }
    }

    public void OnPull(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        Vector3 PullVec = new Vector3(value.x, 0, 0);
        PlayerManager.Instance.playerMove.SetPull(PullVec);

        if(context.performed)
        {

                if (GameManager.Instance.stageManager.stage2.StickInterAction.IsOnTriggerStick)
                {
                    GameManager.Instance.stageManager.stage2.StickInterAction.StartStickInterAction();
                }
            
        }


        if (context.canceled)
        {
            IsPull = false;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Hold", false);
        }
    }

    public void OnPickUpObj(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           if(PlayerManager.Instance.playerMove.GetItemrb == null)
            {
                return;
            }
            if ((PlayerManager.Instance.playerMove.GetItemrb.CompareTag("InterActionItem") && PlayerManager.Instance.playerMove.IsItemCol) && 
                 !IsPickUpItem)
            {
                if (PlayerManager.Instance.playerMove.IsItemCol && !IsPickUpItem)
                {
                    IsPickUpItem = true;
                    StartCoroutine(PlayerManager.Instance.playerMove.InterActionItemPickUp());
                }
            }
            else
            {
                PlayerManager.Instance.playerMove.IsItemCol = false;
                PlayerManager.Instance.playerMove.SetRemoveGetItemObj();
                IsPickUpItem = false;
                StartCoroutine(PlayerManager.Instance.playerMove.InterActionItemPickDown());
            }
        }
    }
    public void OnRightMouseButton(InputAction.CallbackContext context)
    {
        // layermask를 활용해서 raycast로 클릭되면 sizemodulate의 변수로 넣어줌
        if (context.started)
        {
            Vector2 Input = Mouse.current.position.ReadValue();
            //sizeModulate.ItemSelect(Input);
           // PlayerManager.Instance.SizeModulate.ItemSelect(Input);
        }
    }

    public void OnSizeModulate(InputAction.CallbackContext context) // 
    {
        Vector2 value = context.ReadValue<Vector2>();
        switch (value.y)
        {
            case 120:

                break;
            case -120:
                break;
        }
        //if (value.y == 120f || value.y == -120f)
            //sizeModulate.ItemSizeModulate(value.y);
           // PlayerManager.Instance.SizeModulate.ItemSizeModulate(value.y);
    }


    public void LightOnOff(InputAction.CallbackContext context)
    {
        if (context.performed && IsLightGet)
        {
            PlayerManager.Instance.flashLight.Toggle();
        }
    }

    public void LighAngle(InputAction.CallbackContext context)
    {
        InputValue = context.ReadValue<Vector2>();
        if (IsLightGet)
        {
            Vector3 AngleValue = new Vector3(InputValue.x, InputValue.y, 0);
            PlayerManager.Instance.flashLight.SetAngleValue(AngleValue);
        }
    }




}
