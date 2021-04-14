using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageCamera : MonoBehaviour
{

    public CinemachineVirtualCamera BaseCam;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        BaseCam.Follow = PlayerManager.Instance.playerMove.Root_Tr;
        BaseCam.LookAt = PlayerManager.Instance.playerMove.Root_Tr;
    }
}
