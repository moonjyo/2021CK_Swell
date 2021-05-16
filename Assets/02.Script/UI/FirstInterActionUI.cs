using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstInterActionUI : MonoBehaviour, IInteractableUI
{
    public GameObject TargetObj;
    public Canvas Parentcanvas;
    private bool IsInit = false;
    public Vector3 OffsetVec;
    private void Update()
    {
        if (IsInit)
        {
            //if (TargetObj == null)
            //{
            //    gameObject.SetActive(false);
            //    return;
            //}
            OffsetVec = TargetObj.transform.Find("UIOffsetFirstCheck").transform.localPosition;

            transform.position = CameraManager.Instance.MainCamera.WorldToScreenPoint(TargetObj.transform.position + new Vector3(OffsetVec.x, OffsetVec.y, OffsetVec.z));
        }
    }

    public void Init()
    {
        transform.SetParent(Parentcanvas.transform);
        OffsetVec = TargetObj.transform.Find("UIOffsetFirstCheck").transform.localPosition;

        IsInit = true;
    }
    public void Interact()
    {
        TargetObj.GetComponent<IInteractbale>().InterAct();
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
