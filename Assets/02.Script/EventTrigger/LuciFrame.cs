using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LuciFrame : MonoBehaviour , IEventTrigger
{
    public bool IsOnTrigger = false;

    public GameObject DogSound;
    public Animator FrameAnim;


    public IEnumerator EventOn()
    {
        IsOnTrigger = true;
        DogSound.SetActive(true);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;



        yield return new WaitForSeconds(0.5f);
        FrameAnim.SetTrigger("InterActionOn");

        yield return new WaitForSeconds(0.3f);

        GameManager.Instance.uiManager.DialogueText.DialogueCount(4, 6);
        Action act = GameManager.Instance.uiManager.DialogueText.ShowDialogue;
        CameraManager.Instance.StageCam.MoveScreenX(0.2f, 3f, act);
    }

    public bool SetOnTrigger(bool IsOnTrigger)
    {
        return IsOnTrigger;
    }

    void IEventTrigger.EventOn()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !IsOnTrigger)
        {
           StartCoroutine(EventOn());
        }
    }

}
