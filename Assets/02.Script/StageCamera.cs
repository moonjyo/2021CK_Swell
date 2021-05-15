using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System;

public class StageCamera : MonoBehaviour
{

    public CinemachineVirtualCamera BaseCam;
    [HideInInspector]
    public CinemachineTransposer transposer;
    [HideInInspector]
    public CinemachineComposer composer;


    public float OffsetSpeed = 2f;

    private Action ActionMoveScreenFunc;

    public Vector3 LSideVec;
    public Vector3 RSideVec;
    public Vector3 BaseVec;

    public bool IsLside = false;

    public void Init()
    {
        BaseCam.Follow = PlayerManager.Instance.playerMove.Body_Tr;
        BaseCam.LookAt = PlayerManager.Instance.playerMove.Body_Tr;
       
        transposer = BaseCam.GetCinemachineComponent<CinemachineTransposer>();
        composer = BaseCam.GetCinemachineComponent<CinemachineComposer>();
    }


    public void MoveScreenX(float value , float time , Action EndFunc)
    {
        DOTween.To(() => composer.m_ScreenX, x => composer.m_ScreenX = x, value, time).OnComplete(() => { EndFunc?.Invoke(); });
    }
    public void MoveScreenX(float value, float time)
    {
        DOTween.To(() => composer.m_ScreenX, x => composer.m_ScreenX = x, value, time);
    }


    public void MoveBasecamOffset()
    {
        DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, BaseVec, OffsetSpeed);
    }
}
