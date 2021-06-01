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
            ClimbingObj();
            Debug.Log("click");
        }
    }



    public void ClimbingObj()
    {

        Rigidbody rb = TargetObj.GetComponent<Rigidbody>();
        IInteractbale inter = TargetObj.GetComponent<IInteractbale>();
        if (rb != null && !inter.IsGetInterAction())
        {
            inter.SecondInteractOff();
            PlayerManager.Instance.playerMove.InterActionUIPressed = true;
            PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
            PlayerManager.Instance.playerMove.transform.DOLookAt(new Vector3(rb.transform.position.x, PlayerManager.Instance.playerMove.Body_Tr.position.y, rb.transform.position.z), 0.15f).OnComplete(() =>
            {
                PlayerManager.Instance.playerMove.IsGravity = true;
                PlayerManager.Instance.playerAnimationEvents.SetCliming(inter.GetClimingVec() , inter.GetAnimState());
                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)inter.GetAnimState());

            });
        }
        else
        {
            
            GameManager.Instance.uiManager.monologueText.SetText(GameManager.Instance.uiManager.monologueText.CurrentDialogue[0].context);

            GameManager.Instance.uiManager.monologueText.ShowMonologue();


            Debug.Log("서랍을 더 열어야 올라 갈 수 있을거 같아.");
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
