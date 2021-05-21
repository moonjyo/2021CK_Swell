using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour, IEventTrigger
{
    public GameObject[] CamObj;

    public void EventOn()
    {
        CameraManager.Instance.StageCam.MoveFirePlaceOffset();
        CameraManager.Instance.StageCam.MoveFireTrackedOffset();
        for (int i = 0; i < CamObj.Length; ++i)
        {
            CamObj[i].SetActive(false);
        }
    }

    public void EventOff()
    {
        CameraManager.Instance.StageCam.MoveBasecamOffset();
        CameraManager.Instance.StageCam.MoveBaseTrackedOffset();
        for (int i = 0; i < CamObj.Length; ++i)
        {
            CamObj[i].SetActive(true);
        }
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
