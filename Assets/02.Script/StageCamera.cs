using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class StageCamera : MonoBehaviour
{

    public CinemachineVirtualCamera BaseCam;
    [HideInInspector]
    public CinemachineTransposer transposer;

    public float OffsetSpeed = 2f;


    public Vector3 LSideVec;
    public Vector3 RSideVec;
    public Vector3 BaseVec;

    public bool IsLside = false;

    public void Init()
    {
        BaseCam.Follow = PlayerManager.Instance.playerMove.Body_Tr;
        BaseCam.LookAt = PlayerManager.Instance.playerMove.Body_Tr;
       
        transposer = BaseCam.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void GoToRSide()
    {
        IsLside = false;
        DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, RSideVec, OffsetSpeed);
    }
    public void GoToLSide()
    {
        IsLside = true;
        DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, LSideVec, OffsetSpeed);
    }
    public void GoToBase()
    {
        DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, BaseVec, OffsetSpeed);
    }
}
