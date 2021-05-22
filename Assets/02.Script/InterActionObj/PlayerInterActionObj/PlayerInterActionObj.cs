﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


public class PlayerInterActionObj : MonoBehaviour, IInteractbale
{
    [System.Serializable]
    public class InterActEvent : UnityEvent { }

    public string ItemKey;
    public string MonologueKey; //임시 

    public bool IsTake;
    public bool IsWatch;
    public bool IsRotate;

    public bool IsInterAction = false;

    public Sprite InventoryIcon; // 이 아이템의 아이콘

    public Vector3 SizeObj;

    public string InteractObjKey;

    public InterActEvent events;
   
    

    private bool IsFrameStart;
    public void SecondInteractOn()
    {
        foreach (var Obj in UISecondObjList)
        {
            Obj.gameObject.SetActive(true);
        }
    }
    public void SecondInteractOff()
    {
        foreach (var Obj in UISecondObjList)
        {
            Obj.gameObject.SetActive(false);
        }
    }
    public void AllDestroyObj()
    {
        foreach (var Obj in UISecondObjList)
        {
            Obj.SetActive(false);
        }
        GameManager.Instance.uiManager.IsOnFirstInterActionUI = false;  
    }


    [SerializeField]
    private GameObject[] Objs;

    [HideInInspector]
    public List<GameObject> UISecondObjList = new List<GameObject>();
    [HideInInspector]
    public FirstInterActionUI UIFirstObj;
    

    //자신에게 할당된 ui를 생성해주는 부분 
    private void Start()
    {
        IsInterAction = true;
        ItemKey = this.gameObject.name;

        for (int i = 0; i < Objs.Length; ++i)
        {
            GameObject Targetobj = Instantiate(Objs[i]);
            Targetobj.SetActive(false);
            IInteractableUI target = Targetobj.transform.GetComponent<IInteractableUI>();
            
           
            target.SetTargetObj(transform.gameObject);
            target.SetTargetCanvas(GameManager.Instance.uiManager.InterActionUICanvas);
            target.Init();
            if (Targetobj.CompareTag("SecondInterActionUI"))
            {
                UISecondObjList.Add(Targetobj);
            }
        }
    }

    public List<GameObject> GetUIObjList()
    {
        return UISecondObjList;
    }

    public bool IsGetInterAction()
    {
        return IsInterAction;
    }

    public IEnumerator InterAct()
    {
        events?.Invoke();

        yield break;
    }



    //InterActionEvent Func 
    #region
    public void FrameInterAction()
    {
        Animator FrameAnim =  gameObject.GetComponent<Animator>();

        if (FrameAnim != null)
        {
            IsFrameStart = true;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.FRAME);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            SecondInteractOff();
            FrameAnim.SetTrigger("InterActionOff");
            GameManager.Instance.eventCommand.IsLuciFrame = true;


            GameManager.Instance.uiManager.AchiveMents(10f);

            if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(this))
            {
                GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Remove(this);
            }
        }
    }

    public void TennisBallInterAction()
    {
        StartCoroutine(CoTennisBallInter());
    }

    private IEnumerator CoTennisBallInter()
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.PICKUPDOWN);
        GameManager.Instance.uiManager.uiInventory.Distinguish.FirePlace();
        yield return new WaitForSeconds(1.4f);
        GameManager.Instance.timeLine.Play("FirePlace");
        SecondInteractOff();


        if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(this))
        {
            GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Remove(this);
        }


        gameObject.SetActive(false);

    }


    public void StoveInterAction()
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CRAWL);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        SecondInteractOff();

        PlayerManager.Instance.playerMove.IsGravity = true;
        GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.ENDTRIGGER].SetActive(true);

        if (GameManager.Instance.uiManager.uiInventory.Distinguish.ProductionClickItem.TryGetValue("Key", out GameObject KeyObj))
        {
            GameManager.Instance.uiManager.uiInventory.GetItemIcon(KeyObj.GetComponent<PlayerInterActionObj>());
        }


        if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(this))
        {
            GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Remove(this);
        }

    }


    public void DoorInterAction()
    {
        GameManager.Instance.uiManager.monologueText.SetText(GameManager.Instance.uiManager.monologueText.CurrentDialogue[2].context);
        GameManager.Instance.uiManager.monologueText.ShowMonologue();
    }


    #endregion
}
