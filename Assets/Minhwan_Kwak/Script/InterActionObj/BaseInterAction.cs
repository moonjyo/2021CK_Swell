using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
[Serializable]
public class SwipEvents : UnityEvent { }

public class BaseInterAction : MonoBehaviour
{
    public bool IsInRange;
    public UnityEvent InterAction;
    public KeyCode InterActionKey;

    private void Update()
    {
        if(IsInRange)
        {
            if (Input.GetKeyDown(InterActionKey))
            {
                InterAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            IsInRange = true;
            Debug.Log("Player In Range");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInRange = false;
            Debug.Log("Player Out Range");
        }

    }


}
