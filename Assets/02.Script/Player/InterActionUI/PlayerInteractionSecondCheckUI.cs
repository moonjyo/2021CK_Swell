﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionSecondCheckUI : MonoBehaviour
{
    public LayerMask InterActionLayer;

    [SerializeField]
    private LayerMask CameraActionLayer;

    private PlayerInterActionObj TargetObj = null;


    private void OnTriggerEnter(Collider other)
    {
        InterActionCheckIn(other);
        CamActionCheckIn(other);
    }


    private void OnTriggerExit(Collider other)
    {
        InterActionCheckOut(other);
        CamActionCheckOut(other);
    }


    public void CamActionCheckIn(Collider other)
    {
        if ((1 << other.gameObject.layer & CameraActionLayer) != 0)
        {
            if(other.gameObject.CompareTag("RSideCol"))
            {
                CameraManager.Instance.StageCam.GoToRSide();
            }
            else
            {
                CameraManager.Instance.StageCam.GoToLSide();
            }
        }
    }
    public void CamActionCheckOut(Collider other)
    {
        if ((1 << other.gameObject.layer & CameraActionLayer) != 0)
        {
            CameraManager.Instance.StageCam.GoToBase();
        }
    }
    public void InterActionCheckIn(Collider other)
    {
        if ((1 << other.gameObject.layer & InterActionLayer) != 0)
        {
            TargetObj = other.GetComponent<PlayerInterActionObj>();
            if (TargetObj != null)
            {
                GameManager.Instance.uiManager.IsOnFirstInterActionUI = true; // start  firstinteraction ui exit 
                TargetObj.SecondInteractOn(); //현재 충돌된 interaction 에 second ui 들 on 
                GameManager.Instance.uiManager.OffFirstInterActionUI(); // 현재 충돌된 firstinteraction ui off 
                if (!GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(TargetObj))
                {
                    GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Add(TargetObj);

                }
            }
        }

    }

    public void InterActionCheckOut(Collider other)
    {
        if ((1 << other.gameObject.layer & InterActionLayer) != 0)
        {
            TargetObj = other.GetComponent<PlayerInterActionObj>();
            if (TargetObj != null)
            {
                GameManager.Instance.uiManager.IsOnFirstInterActionUI = false; // firstinteraction ui 활성화
                TargetObj.SecondInteractOff(); // 충돌된 second off 


                if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(TargetObj))
                {
                    GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Remove(TargetObj);
                }

                if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Count == 0)
                {
                    GameManager.Instance.uiManager.OnFirstInterActionUI(); //다시 충돌중인 first interaction on 
                }


                if (PlayerManager.Instance.playerMove.InterActionrb != null)
                {
                    if (PlayerManager.Instance.playerMove.InterActionrb.gameObject == other.gameObject)
                    {
                        PlayerManager.Instance.playerMove.InterActionrb = null;
                    }
                }
            }
        }
    }
}
