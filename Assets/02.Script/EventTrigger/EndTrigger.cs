using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour, IEventTrigger
{
    public GameObject DogBark;
    
    
    public void EventOn()
    {
        DogBark.SetActive(true);
        GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.WINDOWWICHTRIGGER].SetActive(true);
        GameManager.Instance.uiManager.DialogueText.DialogueCount(12, 13);

        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
        //sound 반전 
        //걷기 변경 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventOn();
        }
    }
}
