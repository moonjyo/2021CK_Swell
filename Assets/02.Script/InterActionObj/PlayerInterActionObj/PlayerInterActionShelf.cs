﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterActionShelf : MonoBehaviour , IInteractbale
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
        //UIFirstObj.gameObject.SetActive(false);
        // GameManager.Instance.uiManager.OnActiveFirstInterActionUI.Remove(UIFirstObj);
        GameManager.Instance.uiManager.IsOnFirstInterActionUI = false;
    }

    private void Update()
    {
        if(transform.localPosition.z > 0.9f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.9f);
            IsInterAction = true;
        }
        else if(transform.localPosition.z < 0.6f)
        {
            IsInterAction = false;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.6f);
        }
    }



    [SerializeField]
    private GameObject[] Objs;

    [SerializeField]
    Vector3 UIOffsetVec;


    [HideInInspector]
    public List<GameObject> UISecondObjList = new List<GameObject>();
    public FirstInterActionUI UIFirstObj;


    //자신에게 할당된 ui를 생성해주는 부분 
    private void Start()
    {
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

            if (UIFirstObj == null)
            {
                UIFirstObj = Targetobj.GetComponent<FirstInterActionUI>();
            }


            if (Targetobj.CompareTag("SecondInterActionUI"))
            {
                UISecondObjList.Add(Targetobj);
            }
        }
    }

    public void GreenKey(GameObject gameObject)
    {
        Debug.Log("Open GreenLoker");
    }

    public void PurpleKey(GameObject gameObject)
    {
        Debug.Log("Open PurpleLoker");
    }

    public List<GameObject> GetUIObjList()
    {
        return UISecondObjList;
    }

    public bool IsGetInterAction()
    {
        return IsInterAction;
    }

    public void InterAct()
    {
        throw new System.NotImplementedException();
    }
}