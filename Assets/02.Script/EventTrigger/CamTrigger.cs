using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour, IEventTrigger
{


    public void EventOn()
    {
        CameraManager.Instance.StageCam.MoveFirePlaceOffset();
    }

    public void EventOff()
    {

        CameraManager.Instance.StageCam.MoveBasecamOffset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventOff();
        }

    }
}
