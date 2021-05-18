﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInterActionStove : MonoBehaviour , IInteractbale
{
    public string ItemKey;
    public string MonologueKey; //임시 

    public bool IsTake;
    public bool IsWatch;
    public bool IsRotate;

    public bool IsInterAction = false;

    public Sprite InventoryIcon; // 이 아이템의 아이콘

    public Vector3 SizeObj;

    //public PlayerInterActionObj InteractObj;
    public string InteractObjKey;

    public Animator InterActAnim;

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
            Destroy(Obj);
        }
        //UIFirstObj.gameObject.SetActive(false);
        // GameManager.Instance.uiManager.OnActiveFirstInterActionUI.Remove(UIFirstObj);
        GameManager.Instance.uiManager.IsOnFirstInterActionUI = false;
    }


    [SerializeField]
    private GameObject[] Objs;

    [SerializeField]
    Vector3 UIOffsetVec;


    [HideInInspector]
    public List<GameObject> UISecondObjList = new List<GameObject>();
    public FirstInterActionUI UIFirstObj;

    public GameObject EndTrigger;

    //자신에게 할당된 ui를 생성해주는 부분 
    private void Start()
    {
        IsInterAction = true;
        ItemKey = this.gameObject.name;

        //GameManager.Instance.uiManager.uiInventory.OnDistingush += TestCheck;

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
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CRAWL);
        gameObject.GetComponent<BoxCollider>().enabled= false;
        SecondInteractOff();
        PlayerManager.Instance.playerMove.IsGravity = true;
        EndTrigger.SetActive(true);

        if(GameManager.Instance.uiManager.uiInventory.Distinguish.ProductionClickItem.TryGetValue("Key", out GameObject KeyObj))
        {
            GameManager.Instance.uiManager.uiInventory.GetItemIcon(KeyObj.GetComponent<PlayerInterActionObj>());
        }
        

        yield break;
    }

    //public void TestCheck(PlayerInterActionObj Obj)
    //{
    //    if(Obj == this.GetComponent<PlayerInterActionObj>())
    //    {
    //        Debug.Log("Check Success");
    //    }
    //}
}