using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueEvent : MonoBehaviour, IEventTrigger
{
    public bool IsOnTrigger = false;
    
    public void EventOn()
    {
        GameManager.Instance.uiManager.DialogueText.DialogueCount(0, 4);
        IsOnTrigger = true;
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
    }

    public bool SetOnTrigger(bool OnTrigger)
    {
       return IsOnTrigger = OnTrigger;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !IsOnTrigger)
        {
            EventOn();
        }
    }

}
