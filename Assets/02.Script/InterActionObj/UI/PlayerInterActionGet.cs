using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterActionGet : MonoBehaviour, IInteractableUI
{
    public GameObject TargetObj;
    public Canvas Parentcanvas;

    public Vector3 OffsetVec;
    private bool IsInit = false;


    //private bool IsPressed = false;

    private PlayerInterActionObj TargetInterActionObj;

    private void FixedUpdate()
    {
        if (IsInit)
        {
            OffsetVec = TargetObj.transform.Find("UIOffsetGet").transform.localPosition;
            transform.position = Camera.main.WorldToScreenPoint(TargetObj.transform.position + new Vector3(OffsetVec.x, OffsetVec.y, OffsetVec.z));
        }
    }

    public void Init()
    {
        transform.SetParent(Parentcanvas.transform);
        OffsetVec = TargetObj.transform.Find("UIOffsetGet").transform.localPosition;


        IsInit = true;
    }
    public void Interact()
    {
        Debug.Log("interactOn PlayerInterAction");
        TargetInterActionObj = TargetObj.GetComponent<PlayerInterActionObj>();
        if (TargetInterActionObj != null)
        {
            if (TargetInterActionObj.IsTake)
            {
                GameManager.Instance.uiManager.uiInventory.GetItemIcon(TargetObj.GetComponent<PlayerInterActionObj>());

                TargetObj.SetActive(false); // ui도 관리해주어야 함
                TargetInterActionObj.AllDestroyObj();
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