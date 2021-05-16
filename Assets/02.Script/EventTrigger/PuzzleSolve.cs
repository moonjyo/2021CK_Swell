using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
public class PuzzleSolve : MonoBehaviour , IEventTrigger
{
    public  LuciFrame luciFrame;
    public bool IsOnTrigger = false;

    public void EventOn()
    {
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;

        PlayerManager.Instance.playerMove.IsGravity = true;

        PlayerManager.Instance.playerMove.transform.DOMoveX(transform.position.x - 1f, 0.5f);


        GameManager.Instance.uiManager.monologueText.SetText(GameManager.Instance.uiManager.monologueText.CurrentDialogue[1].context);
        GameManager.Instance.uiManager.monologueText.ShowMonologue();
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !luciFrame.IsLuciFrame)
        {
            EventOn();
        }
    }
}
    