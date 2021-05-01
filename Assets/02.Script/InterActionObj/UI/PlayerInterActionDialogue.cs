using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterActionDialogue : MonoBehaviour, IInteractableUI
{
    public GameObject TargetObj;
    public Canvas Parentcanvas;

    public Vector3 OffsetVec;
    private bool IsInit = false;


    private IDialogue TargetDialogue;

    private void FixedUpdate()
    {
        if (IsInit)
        {
            OffsetVec = TargetObj.transform.Find("UIOffsetDialogue").transform.localPosition;
            transform.position = CameraManager.Instance.MainCamera.WorldToScreenPoint(TargetObj.transform.position + new Vector3(OffsetVec.x,OffsetVec.y, OffsetVec.z));
        }
    }

    public void Init()
    {
        transform.SetParent(Parentcanvas.transform);
        OffsetVec = TargetObj.transform.Find("UIOffsetDialogue").transform.localPosition;

        IsInit = true;
    }
    public void Interact()
    {
        TargetDialogue = TargetObj.GetComponent<IDialogue>();
        if (TargetDialogue != null)
        {
            StartTalk();
        }
    }

    public void StartTalk()
    {
        GameManager.Instance.uiManager.DialogueText.IsDialogue = true;
        GameManager.Instance.uiManager.DialogueText.CurrentDialogue = TargetDialogue.GetDialogoue();
        StartCoroutine(GameManager.Instance.uiManager.DialogueText.SetText());
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
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
