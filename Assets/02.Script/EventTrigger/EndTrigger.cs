using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour, IEventTrigger
{
    public GameObject DogBark;
    
    public void EventOn()
    {
        DogBark.SetActive(true);
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
