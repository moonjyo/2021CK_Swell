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
    FIREPLACETIME = 4,
    ENDTRIGGER = 5,

}

public class EventCommand : MonoBehaviour
{
    public GameObject DogSound;
    public GameObject DogBark;
    public Animator FrameAnim;
    public GameObject[] CamObj;
    public GameObject Dog;
    public PlayerInterActionObj Memoobj;
    public bool IsLuciFrame = false;
    public bool IsDogActive = false;
    public bool IsRunning = false;

    public List<GameObject> EventsTriggerList = new List<GameObject>();
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

           
          

<<<<<<< HEAD
=======

            PlayerManager.Instance.playerMove.transform.DOMoveX(PlayerManager.Instance.playerMove.transform.position.x - 1f, 0.5f);
>>>>>>> f608ff43f6e016f6bb9f33d1625ba21f7abe5eaf

            PlayerManager.Instance.playerMove.transform.DOMoveX(PlayerManager.Instance.playerMove.transform.position.x - 1f, 0.5f);

<<<<<<< HEAD

=======
>>>>>>> f608ff43f6e016f6bb9f33d1625ba21f7abe5eaf
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

    public void Stage1EndTrigger()
    {
        DogBark.SetActive(true);
        //sound 반전 
        //걷기 변경 
    }
    private IEnumerator LuciFrameCo()
    {

        CameraManager.Instance.StageCam.BaseCam.Follow = PlayerManager.Instance.playerMove.Body_Tr;
        CameraManager.Instance.StageCam.BaseCam.LookAt = PlayerManager.Instance.playerMove.Body_Tr;
        DogSound.SetActive(true);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;

        yield return new WaitForSeconds(0.7f);
        GameManager.Instance.uiManager.DialogueText.DialogueCount(4, 6);
        Action act = LuciFrameDialogue;
        CameraManager.Instance.StageCam.MoveScreenX(0.2f, 3f, act);
        yield return new WaitForSeconds(1.8f);
        FrameAnim.SetTrigger("InterActionOn");
    }


    public void LuciFrameDialogue()
    {
        IsDogActive = true;
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
    }




<<<<<<< HEAD

    public void StartMemo()
    {
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        GameManager.Instance.uiManager.uiInventory.ob.ActivateObserverItem("MSG_BGLR_Memo", Memoobj);
    }
=======
>>>>>>> f608ff43f6e016f6bb9f33d1625ba21f7abe5eaf

}
