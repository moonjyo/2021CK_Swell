using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerInterActionUI : MonoBehaviour, IInteractableUI
{
    public GameObject TargetObj;
    public Canvas Parentcanvas;

    public Vector3 OffsetVec;
    private bool IsInit = false;


    private bool IsPressed = false;


    private void FixedUpdate()
    {
        if (IsInit)
        {
            OffsetVec = TargetObj.transform.Find("UIOffsetInterAction").transform.localPosition;
            transform.position = Camera.main.WorldToScreenPoint(TargetObj.transform.position + new Vector3(OffsetVec.x, OffsetVec.y, OffsetVec.z));
        }
    }

    public void Init()
    {
        transform.SetParent(Parentcanvas.transform);
        OffsetVec = TargetObj.transform.Find("UIOffsetInterAction").transform.localPosition;


        IsInit = true;
    }
    public void Interact()
    {
        //Debug.Log("interactOn PlayerInterAction");
    }

    public void PointDown()
    {
       IsPressed = true;
       Rigidbody rb = TargetObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
            PlayerManager.Instance.playerMove.SetInterActionObj(rb);
            PlayerManager.Instance.playerMove.transform.DOLookAt(new Vector3(rb.transform.position.x,PlayerManager.Instance.playerMove.Body_Tr.position.y,rb.transform.position.z),      1f).OnComplete(() =>
            {
                PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
            });
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Hold", true);
        }
        Debug.Log("InterActionOn");
    }

    public void PointUp()
    {
        IsPressed = false;
        PlayerManager.Instance.playerMove.SetRemoveInterActionObj();
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Idle", true);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Hold", false);
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
        Debug.Log("InterActionOff");
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
