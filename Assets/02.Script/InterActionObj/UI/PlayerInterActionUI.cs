using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterActionUI : MonoBehaviour, IInteractableUI
{
    [HideInInspector]
    public GameObject TargetObj;
    public Canvas Parentcanvas;

    public Vector3 OffsetVec;
    private bool IsInit = false;    

 
    private void Update()
    {
        if (IsInit)
        {
            OffsetVec = TargetObj.transform.Find("UIOffsetInterAction").transform.localPosition;

            transform.position = Camera.main.WorldToScreenPoint(TargetObj.transform.position + new Vector3(OffsetVec.x, OffsetVec.y, OffsetVec.z)) ;

        }
    }

    public void Init()
    {
        transform.parent = Parentcanvas.transform;
        OffsetVec = TargetObj.transform.Find("UIOffsetInterAction").transform.localPosition;


        IsInit = true;
    }
    public void Interact()
    {
        Debug.Log("interactOn PlayerInterAction");

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
