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

    public Vector3 FirePlaceVec;
    public Vector3 BaseVec;
    public Vector3 BaseTrackedOffsetVec;
    public Vector3 FireTrackedOffsetVec;

    public bool IsLside = false;

    public void Init()
    {
        BaseCam.Follow = PlayerManager.Instance.playerMove.Body_Tr;

        composer = BaseCam.GetCinemachineComponent<CinemachineComposer>();
        transposer = BaseCam.GetCinemachineComponent<CinemachineTransposer>();
     
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
    public void MoveBaseTrackedOffset()
    {
        DOTween.To(() => composer.m_TrackedObjectOffset, x => composer.m_TrackedObjectOffset = x, BaseTrackedOffsetVec, OffsetSpeed);
    }

    public void MoveFirePlaceOffset()
    {
        DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, FirePlaceVec, OffsetSpeed);
    }

    public void MoveFireTrackedOffset()
    {

        DOTween.To(() => composer.m_TrackedObjectOffset, x => composer.m_TrackedObjectOffset = x, FireTrackedOffsetVec, OffsetSpeed);
    }

}
