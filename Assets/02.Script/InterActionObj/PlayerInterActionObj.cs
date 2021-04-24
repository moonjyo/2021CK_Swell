using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInterActionObj : MonoBehaviour, IInteractbale
{
    public string ItemKey;

    public bool IsTake;
    public bool IsWatch;
    public bool IsRotate;

    public Sprite InventoryIcon; // 이 아이템의 아이콘

    public void SecondInteract()
    {
        foreach(var Obj in UISecondObjList)
        {
            Obj.gameObject.SetActive(true);
        }
    }

    public void FirstInteract()
    {
        foreach (var Obj in UIFirstObjList)
        {
            Obj.gameObject.SetActive(true);
        }

    }
   

    [SerializeField]
    private GameObject[] Objs;

    [SerializeField]
    Vector3 UIOffsetVec;


    [HideInInspector]
    public List<GameObject> UIFirstObjList = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> UISecondObjList = new List<GameObject>();
    public bool IsInterAct= false;
    

    private void Start()
    {
        for(int i = 0; i < Objs.Length; ++i)
        {
            GameObject Targetobj = Instantiate(Objs[i]);
            Targetobj.SetActive(false);;
            IInteractableUI target = Targetobj.transform.GetComponent<IInteractableUI>();
            
           
            target.SetTargetObj(transform.gameObject);
            target.SetTargetCanvas(GameManager.Instance.uiManager.InterActionUICanvas);
            target.Init();

            GameManager.Instance.uiManager.AllInterActionUI.Add(Targetobj);
            if(!Targetobj.CompareTag("FirstInterActionUI"))
            {
                UIFirstObjList.Add(Targetobj);
            }
            else
            {
                UISecondObjList.Add(Targetobj);
            }
        }
    }

    private void Update()
    {
        IsInterAct = true;
    }

}
