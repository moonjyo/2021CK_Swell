using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveUI : MonoBehaviour,IInteractableUI
{
    public GameObject TargetObj;
    public Canvas Parentcanvas;
    private bool IsInit = false;
    public Vector3 OffsetVec;
    PlayerInterActionObj TargetInterAction;

    private void FixedUpdate()
    {
        if (IsInit)
        {
            OffsetVec = TargetObj.transform.Find("UIOffsetEyes").transform.localPosition;
            transform.position = Camera.main.WorldToScreenPoint(TargetObj.transform.position + new Vector3(OffsetVec.x, OffsetVec.y, OffsetVec.z));
        }
    }
  
    public void Init()
    {
        transform.SetParent(Parentcanvas.transform);
        OffsetVec = TargetObj.transform.Find("UIOffsetEyes").transform.localPosition;

        IsInit = true;
    }
    public void Interact()
    {
        TargetInterAction = TargetObj.GetComponent<PlayerInterActionObj>();
        Debug.Log("interactOn Observer");

        if (TargetInterAction != null)
        {
            if (TargetInterAction.IsWatch)
            {
                TargetInterAction.SecondInteractOff();
                GameManager.Instance.uiManager.uiInventory.ClickItemIcon(TargetInterAction.ItemKey , TargetInterAction);
            }
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
