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

    public bool IsLightGetReady = false;

    public bool IsOnPressedLeftMouse = false;
    private GameObject ObjLight;
    public void OnWalk(InputAction.CallbackContext context)
    {
        //if(GameManager.Instance.GetComponent<ObserveMode>().IsOnObserveMode)
        //{
        //    return;
        //}
        InputValue = context.ReadValue<Vector2>();

        Vector3 MoveVec = new Vector3(InputValue.x, 0, InputValue.y);
        PlayerManager.Instance.playerMove.SetMove(MoveVec);
    }

    public void OnDialogueNext(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NextDialogue();
        }
    }

    public void NextDialogue()
    {
        if (GameManager.Instance.uiManager.DialogueText.IsNextDialogue)
        {
             if (GameManager.Instance.uiManager.DialogueText.TextStartCount >= GameManager.Instance.uiManager.DialogueText.TextEndCount) //종료
            {
                if(GameManager.Instance.eventCommand.IsDogActive)
                {
                    GameManager.Instance.eventCommand.IsRunning = true;
                    GameManager.Instance.eventCommand.IsDogActive = false;
                    GameManager.Instance.eventCommand.Dog.SetActive(false);
                }

                EndTalk();
                return;
            }

            GameManager.Instance.uiManager.DialogueText.IsNextDialogue = false;
            StartCoroutine(GameManager.Instance.uiManager.DialogueText.SetText());
        }
    }


    public void EndTalk()
    {
        CameraManager.Instance.StageCam.MoveScreenX(0.533f, 0f);
        GameManager.Instance.uiManager.DialogueText.gameObject.SetActive(false);
        GameManager.Instance.uiManager.DialogueText.IsNextDialogue = false;
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
    }


    public void InterActionStick()
    {
        if (GameManager.Instance.stageManager.CurrentSceneName == "Stage02")
        {
            if (GameManager.Instance.stageManager.stage2.StickInterAction.IsOnTriggerStick && IsPickUpItem)
            {
                GameManager.Instance.stageManager.stage2.StickInterAction.StartStickInterAction();
            }
        }
    }

    public void IsGetLight(GameObject light , bool IsCheck)
    {
        ObjLight = light;
        IsLightGetReady = true;
    }
    public void PickUpItem()
    {
        if (PlayerManager.Instance.playerMove.GetInterActionItem == null)
        {
            return;
        }
        if ((PlayerManager.Instance.playerMove.GetInterActionItem.CompareTag("InterActionItem") &&   PlayerManager.Instance.playerMove.IsItemCol) && !IsPickUpItem)
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




}
