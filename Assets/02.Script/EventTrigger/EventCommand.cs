using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using DG.Tweening;

public enum EventTriggerEnum
{
    STARTDIALOGUE = 0,
    FRAME = 1,
    PUZZLESOLVE = 2,
    CAMTRIGGER = 3, 
    ENDTRIGGER = 4,
    WINDOWWICHTRIGGER = 5,
    DIALOGUE2 = 6, 
    FIREPLACE = 7,
    DOG = 8,
    TENISBALL = 9,
    FIREPLACETIMELINE = 10, 
    CLEANTIMER = 11,
    FLASHLIGHT = 12,    
    SHADOW = 13,

}

public class EventCommand : MonoBehaviour
{
    public GameObject DogSound;
    public GameObject DogBark;
    public Animator FrameAnim;
    public GameObject[] CamObj;
    public GameObject Dog;
    public bool IsLuciFrame = false;
    public bool IsDogActive = false;
    public bool IsRunning = false;
    public bool IsDoorCamTrigger = false;

    public List<GameObject> EventsTriggerList = new List<GameObject>();

    public PlayerInterActionObj memoInteractionobj;
    public void StartDialogueEvent()
    {
        GameManager.Instance.uiManager.DialogueText.DialogueCount(0, 4);
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
    }

    public void LuciFrame()
    {
        StartCoroutine(LuciFrameCo());
    }

    public void PuzzleSolve()
    {
           PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;

           PlayerManager.Instance.playerMove.IsGravity = true;

           PlayerManager.Instance.playerMove.transform.DOMoveX(PlayerManager.Instance.playerMove.transform.position.x - 1f, 0.5f);


           GameManager.Instance.uiManager.monologueText.SetText(GameManager.Instance.uiManager.monologueText.CurrentDialogue[1].context);
           GameManager.Instance.uiManager.monologueText.ShowMonologue();
           PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
    }


    public void CamTriggerOn()
    {
        CameraManager.Instance.StageCam.MoveFirePlaceOffset();
        for (int i = 0; i < CamObj.Length; ++i)
        {
            CamObj[i].SetActive(false);
        }
    }
    public void CamTriggerOff()
    {
        CameraManager.Instance.StageCam.MoveFirePlaceOffset();
        for (int i = 0; i < CamObj.Length; ++i)
        {
            CamObj[i].SetActive(true);
        }
    }

    private IEnumerator LuciFrameCo()
    {
        CameraManager.Instance.StageCam.BaseCam.Follow = PlayerManager.Instance.playerMove.Body_Tr;
        CameraManager.Instance.StageCam.BaseCam.LookAt = PlayerManager.Instance.playerMove.Body_Tr;
        DogSound.SetActive(true);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Stage1/SFX_St1_FrameShake", GetComponent<Transform>().position);
        yield return new WaitForSeconds(0.7f);
        GameManager.Instance.uiManager.DialogueText.DialogueCount(4, 6);
        Action act = LuciFrameDialogue;
        CameraManager.Instance.StageCam.MoveScreenX(0.2f, 3f, act);
        yield return new WaitForSeconds(1.8f);
        FrameAnim.SetTrigger("InterActionOn");

        PlayerManager.Instance.gameObject.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
    }


    public void LuciFrameDialogue()
    {
        IsDogActive = true;
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
    }


    public void PuzzleSovedOn()
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        GameManager.Instance.uiManager.DialogueText.DialogueCount(7, 8);
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
    }


    public void EndTrigger()
    {
        DogBark.SetActive(true);
        GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.WINDOWWICHTRIGGER].SetActive(true);
        GameManager.Instance.uiManager.DialogueText.DialogueCount(12, 13);
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
    }

    public void MemoTrigger()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Stage1/SFX_St1_Wastebasket", GetComponent<Transform>().position);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        GameManager.Instance.uiManager.uiInventory.ob.ActivateObserverItem("MSG_BGLR_Memo", memoInteractionobj);
    }

    public void DoorCamTriggerOn()
    {
      CameraManager.Instance.StageCam.BaseCam.gameObject.SetActive(true);
    }

    public void DoorCamTriggerOff()
    {
      CameraManager.Instance.StageCam.BaseCam.gameObject.SetActive(false);
    }
}
