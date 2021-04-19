using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInterActionObj : MonoBehaviour, IInteractbale
{

    public void Interact()
    {
        foreach(var Obj in UIObjList)
        {
            Obj.gameObject.SetActive(true);
        }
    }
   

    [SerializeField]
    private GameObject[] Objs;

    [SerializeField]
    Vector3 UIOffsetVec;


    [HideInInspector]
    public List<GameObject> UIObjList = new List<GameObject>();

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
            UIObjList.Insert(i,Targetobj);
        }
    }

    private void Update()
    {
        IsInterAct = true;
    }

}
