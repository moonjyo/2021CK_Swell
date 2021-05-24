using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterActionGet : MonoBehaviour, IInteractableUI
{
    public GameObject TargetObj;
    public Canvas Parentcanvas;

    public Vector3 OffsetVec;
    private bool IsInit = false;



    private PlayerInterActionObj TargetInterActionObj;

    private void FixedUpdate()
    {
        if (IsInit)
        {
            OffsetVec = TargetObj.transform.Find("UIOffsetGet").transform.localPosition;
            transform.position = CameraManager.Instance.MainCamera.WorldToScreenPoint(TargetObj.transform.position + new Vector3(OffsetVec.x, OffsetVec.y, OffsetVec.z));
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
        TargetInterActionObj = TargetObj.GetComponent<PlayerInterActionObj>();

       

        if (TargetInterActionObj != null)
        {
            if (TargetInterActionObj.IsTake)
            {
                //획득 초과시
                if(!GameManager.Instance.uiManager.uiInventory.GetItemIcon(TargetObj.GetComponent<PlayerInterActionObj>()))
                {
                    GameManager.Instance.uiManager.monologueText.SetText(GameManager.Instance.uiManager.monologueText.CurrentDialogue[8].context);
                    GameManager.Instance.uiManager.monologueText.ShowMonologue();
                    return;
                }

                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.PICKUPDOWN);
                if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(TargetInterActionObj))
                {
                    GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Remove(TargetInterActionObj);
                }

                TargetObj.SetActive(false); // ui도 관리해주어야 함
                TargetInterActionObj.AllDestroyObj();
                

                for(int i = 0; i < GameManager.Instance.uiManager.monologueText.CurrentDialogue.Length; ++i)
                {
                    if(GameManager.Instance.uiManager.monologueText.CurrentDialogue[i].name == TargetInterActionObj.MonologueKey)
                    {
                        GameManager.Instance.uiManager.monologueText.SetText(GameManager.Instance.uiManager.monologueText.CurrentDialogue[i].context);
                        GameManager.Instance.uiManager.monologueText.ShowMonologue();
                    }
                }

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