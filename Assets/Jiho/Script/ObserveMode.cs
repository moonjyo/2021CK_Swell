using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ObserveMode : MonoBehaviour
{
    [HideInInspector]
    public float RotHInput;
    [HideInInspector]
    public float RotVInput;
    [HideInInspector]
    public Vector2 mousePos;

    public bool IsClickCol = false;

    [HideInInspector]
    public GameObject GO;
    public Canvas FadeCanvas;

    public Dictionary<string, GameObject> ObserveObj = new Dictionary<string,GameObject>();

    public bool IsOnObserveMode = false;
    bool IsObjRotate = false;
   
    private PlayerInterActionObj CurrentTargetObj;

    public GameObject SelectObserveObj;

    RaycastHit HitOrigin;
    public void SetRotateInput(Vector2 value)
    {
        RotHInput = value.x;
        RotVInput = value.y;
    }
    
    public void SetMoveInput(Vector2 value)
    {
        mousePos = value;
    }

    public void SetClickInput(bool value)
    {
        IsClickCol = value;
    }

    private void Start()
    {
        PlayerInterActionObj[] go = FindObjectsOfType<PlayerInterActionObj>();
        for(int i = 0; i < go.Length; i++)
        {
            //AddObserveItem(go[i].ItemKey, go[i].gameObject);
            AddObserveItem(go[i].gameObject.name, go[i].gameObject);
        }
    }

   

    void Update()
    {
        if(IsOnObserveMode && IsObjRotate)
        {
            RotateObj();            
        }
        else if(IsOnObserveMode && SelectObserveObj != null)
        {
            MoveObj();
        }
        if(GO != null)
        {
            GO.transform.position = CameraManager.Instance.ObserveCamera.transform.position + (CameraManager.Instance.ObserveCamera.transform.forward * 3f);
        }

        if(IsClickCol)
        {
            CheckClick();
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
            GO.transform.RotateAround(GO.transform.position, CameraManager.Instance.ObserveCamera.transform.right, RotVInput * 40 * Time.deltaTime);
        }
    }

    public void MoveObj()
    {
        Vector2 Pos = CameraManager.Instance.ObserveCamera.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,-CameraManager.Instance.ObserveCamera.transform.position.z));
        SelectObserveObj.transform.position = new Vector3(Pos.x, Pos.y, SelectObserveObj.transform.position.z);
    }

    public void AddObserveItem(string Key, GameObject go)
    {
        ObserveObj.Add(Key, go);
    }

    public void ActivateObserverItem(string Key , PlayerInterActionObj Target) // 관찰자모드 아이템 활성화
    {
        if (IsOnObserveMode)
            return;

        CurrentTargetObj = Target;

        CameraManager.Instance.CaptureCamera.gameObject.SetActive(true);
        FadeCanvas.gameObject.SetActive(true);
        CameraManager.Instance.ObserveCamera.gameObject.SetActive(true);

        IsOnObserveMode = true;
        if (ObserveObj.TryGetValue(Key, out GameObject go))
        {
            GO = Instantiate(go, CameraManager.Instance.ObserveCamera.transform.position + CameraManager.Instance.ObserveCamera.transform.forward *3f, Quaternion.identity);
            GO.gameObject.layer = 18;
            //GO.GetComponent<BoxCollider>().enabled = false;
            Transform[] GOArray = GO.GetComponentsInChildren<Transform>();
            foreach (Transform Object in GOArray)
            {
                Object.gameObject.layer = 18;
            }
            GO.transform.localScale = go.GetComponent<PlayerInterActionObj>().SizeObj;
            GO.transform.forward = -(CameraManager.Instance.ObserveCamera.transform.position - GO.transform.position);
            GO.SetActive(true);
            if(!go.GetComponent<PlayerInterActionObj>().IsRotate)
            {
                IsObjRotate = false;
            }
            else if(go.GetComponent<PlayerInterActionObj>().IsRotate)
            {
                IsObjRotate = true;
            }            
        }
        else
        {
            CameraManager.Instance.CaptureCamera.gameObject.SetActive(false);
            FadeCanvas.gameObject.SetActive(false);
            CameraManager.Instance.ObserveCamera.gameObject.SetActive(false);
            GO.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void DeactivateObserverItem() // 관찰자모드 아이템 비활성화
    {
        //GO.transform.eulerAngles = new Vector3(0, 0, 0);
        //GO.SetActive(false);
        GO.GetComponent<BoxCollider>().enabled = true;
        Destroy(GO);
        GO = null;
        CameraManager.Instance.CaptureCamera.gameObject.SetActive(false);
        FadeCanvas.gameObject.SetActive(false);
        CameraManager.Instance.ObserveCamera.gameObject.SetActive(false);
       
        IsOnObserveMode = false;
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
        GameManager.Instance.uiManager.OnSecondInterActionUI();
    }

    public void CheckClick()
    {
        RaycastHit hit;
        Ray ray = CameraManager.Instance.ObserveCamera.ScreenPointToRay(GameManager.Instance.uiManager.uiInventory.GetMousePosVal());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GameManager.Instance.uiManager.uiInventory.ObserveObjLayerMask))
        {
            if (GO == null || !hit.collider.CompareTag("ProductionInteractionObj") || !IsOnObserveMode || HitOrigin.collider != hit.collider)
            {
                IsClickCol = false;
                return;
            }

            bool IsPass = false;
            Transform[] tr = GO.GetComponentsInChildren<Transform>();
            foreach (Transform transform in tr)
            {
                if (transform == hit.transform)
                {
                    IsPass = true;
                }
            }

            if (!IsPass)
                return;

            if (hit.collider.name == "MSG_Lr_woodstorage_glass_1")
                return;

            if (GameManager.Instance.uiManager.uiInventory.Distinguish.DistinguishItemDic.TryGetValue(GO.name, out Action<GameObject> value))
            {

                value(hit.collider.gameObject);
            }
        }

        IsClickCol = false;
    }

    public void CheckMouseDown()
    {
        RaycastHit hit;
        Ray ray = CameraManager.Instance.ObserveCamera.ScreenPointToRay(GameManager.Instance.uiManager.uiInventory.GetMousePosVal());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GameManager.Instance.uiManager.uiInventory.ObserveObjLayerMask))
        {
            HitOrigin = hit;
          
            if (hit.collider.gameObject.GetComponent<PlayerInterActionObj>() == null)
                return;

            if (hit.collider.CompareTag("ProductionInteractionObj"))// || hit.collider.gameObject == hit.transform.gameObject)// || !hit.collider.gameObject.GetComponent<PlayerInterActionObj>().IsRotate) 
                return;

            SelectObserveObj = hit.collider.gameObject;

        }
        else
        {
            SelectObserveObj = null;
        }
    }

    public void CheckMouseUp()
    {
        if (SelectObserveObj == null)
            return;

        RaycastHit hit;
        Ray ray = CameraManager.Instance.ObserveCamera.ScreenPointToRay(GameManager.Instance.uiManager.uiInventory.GetMousePosVal());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GameManager.Instance.uiManager.uiInventory.ObserveObjLayerMask))
        {
            if(hit.collider.gameObject == SelectObserveObj)
            {
                SelectObserveObj = null;
                return;
            }
        }
    }
}
