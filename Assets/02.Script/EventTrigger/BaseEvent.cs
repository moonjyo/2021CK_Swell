using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEvent : MonoBehaviour
{
    [System.Serializable]
    public class MyEvent : UnityEvent { }


    public MyEvent OnEvent;

    public MyEvent OffEvent;

    private bool IsOnTrigger = false;

    public bool IsOneShot = false;

    public bool IstTest = false;

    private void OnTriggerStay(Collider other)
    {   
        if (other.CompareTag("Player") && !IsOnTrigger && !IstTest)
        {
            IstTest = true;
            if (IsOneShot)
            {
                IsOnTrigger = true;
            }
            OnEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IstTest = false;
            OffEvent?.Invoke();
        }
    }
}
