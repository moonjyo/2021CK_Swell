using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInterActionUp : MonoBehaviour , IInteractableUI
{
    public GameObject TargetObj;
    public Canvas Parentcanvas;

    public Vector3 OffsetVec;
    private bool IsInit = false;

    


    private void FixedUpdate()
    {
        if (IsInit)
        {
            OffsetVec = TargetObj.transform.Find("UIOffsetUp").transform.localPosition;
            transform.position = CameraManager.Instance.MainCamera.WorldToScreenPoint(TargetObj.transform.position + new Vector3(OffsetVec.x, OffsetVec.y, OffsetVec.z));
        }
    }

    public void Init()
    {
        transform.SetParent(Parentcanvas.transform);
        OffsetVec = TargetObj.transform.Find("UIOffsetUp").transform.localPosition;
        IsInit = true;
    }
    public void Interact()
    {
      
        if (!PlayerManager.Instance.playerMove.InterActionUIPressed)
        {
            PlayerManager.Instance.playerMove.InterActionUIPressed = true;
            ClimbingObj();
            Debug.Log("click");
        }
    }

    public void ClimbingObj()
    {
        Rigidbody rb = TargetObj.GetComponent<Rigidbody>();
        if (rb != null) 
        {
            PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
            PlayerManager.Instance.playerMove.transform.DOLookAt(new Vector3(rb.transform.position.x, PlayerManager.Instance.playerMove.Body_Tr.position.y, rb.transform.position.z), 0.15f).OnComplete(() =>
            {
                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CROUNCH);
                PlayerManager.Instance.playerMove.IsGravity = true;
                PlayerManager.Instance.playerMove.ClimingJudge();
            });
        }
    }


    public GameObject GetTargetObj()
    {
        return gameObject;
    }
    public Canvas GetParentCanvas()
    {
        return Parentcanvas;
    }
    public void SetTargetCanvas(Canvas targetcanvas)
    {
        Parentcanvas = targetcanvas;
    }
    public void SetTargetObj(GameObject targetobj)
    {
        TargetObj = targetobj;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }

    public string GetTag()
    {
        return transform.tag;
    }
}
