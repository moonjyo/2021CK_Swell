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

    public bool IsOnTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !IsOnTrigger)
        {
            IsOnTrigger = true;
            OnEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OffEvent?.Invoke();
        }
    }
}
