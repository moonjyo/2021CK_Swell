﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveMode : MonoBehaviour
{
    [HideInInspector]
    public float RotHInput;
    [HideInInspector]
    public float RotVInput;

    public GameObject GO;
    public Canvas FadeCanvas;

    Dictionary<string, GameObject> ObserveObj = new Dictionary<string,GameObject>();

    public bool IsOnObserveMode = false;
    bool IsObjRotate = false;

    public StageCamera baseCam; 
    public void SetRotateInput(Vector2 value)
    {
        RotHInput = value.x;
        RotVInput = value.y;

    }

    private void Start()
    {
        PlayerInterActionObj[] go = GameObject.FindObjectsOfType<PlayerInterActionObj>();
        for(int i = 0; i < go.Length; i++)
        {

            AddObserveItem(go[i].ItemKey, go[i].gameObject);
        }
    }

    void Update()
    {
        if(IsOnObserveMode && IsObjRotate)
        {
            RotateObj();
        }
        if(GO != null)
        {
            GO.transform.position = baseCam.transform.position + (baseCam.transform.forward * 2.5f);
            //GO.transform.LookAt(baseCam.transform.position);
            //Vector3 lookVec = baseCam.transform.position - GO.transform.position;
            //GO.transform.eulerAngles = lookVec;
        }
        
    }

    public void RotateObj()
    {
        if (Mathf.Abs(RotHInput) > 0.1f)
        {
            GO.transform.RotateAround(GO.transform.position, Vector3.up, -RotHInput * 40 * Time.deltaTime);
        }

        if (Mathf.Abs(RotVInput) > 0.1f)
        {
            GO.transform.RotateAround(GO.transform.position, baseCam.transform.right, RotVInput * 40 * Time.deltaTime);
        }
    }

    public void AddObserveItem(string Key, GameObject go)
    {
        ObserveObj.Add(Key, go);
    }

    public void ActivateObserverItem(string Key) // 관찰자모드 아이템 활성화
    {
        FadeCanvas.gameObject.SetActive(true);
        //
        //GO.SetActive(true);
        //
        IsOnObserveMode = true;
        if (ObserveObj.TryGetValue(Key, out GameObject go))
        {
            float a = 300f;
            GO = Instantiate(go, baseCam.transform.position + baseCam.transform.forward, Quaternion.identity);
            GO.SetActive(true);
            if(!go.GetComponent<PlayerInterActionObj>().IsRotate)
            {
                IsObjRotate = false;
            }
            else if(go.GetComponent<PlayerInterActionObj>().IsRotate)
            {
                IsObjRotate = true;
            }
            //go.transform.position = baseCam.transform.position + (baseCam.transform.forward * 1.5f);
            
        }
        else
        {
            FadeCanvas.gameObject.SetActive(false);
        }
    }

    public void DeactivateObserverItem() // 관찰자모드 아이템 비활성화
    {
        //GO.transform.eulerAngles = new Vector3(0, 0, 0);
        //GO.SetActive(false);
        Destroy(GO);
        GO = null;
        FadeCanvas.gameObject.SetActive(false);
        IsOnObserveMode = false;
    }

    public void DummyExit() // 관찰자 모드 Exit 더미로사용중
    {
        GO.transform.eulerAngles = new Vector3(0, 0, 0);
        FadeCanvas.gameObject.SetActive(false);
        GO.SetActive(false);

        IsOnObserveMode = false;
    }
}
