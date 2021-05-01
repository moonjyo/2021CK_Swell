using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageCamera : MonoBehaviour
{

    public CinemachineVirtualCamera BaseCam;
    [HideInInspector]
    public CinemachineTransposer transposer;

    public void Init()
    {
        BaseCam.Follow = PlayerManager.Instance.playerMove.Body_Tr;
        BaseCam.LookAt = PlayerManager.Instance.playerMove.Body_Tr;
       
        transposer = BaseCam.GetCinemachineComponent<CinemachineTransposer>();
    }
}
