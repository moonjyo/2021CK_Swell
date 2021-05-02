using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObserveMode : MonoBehaviour
{
    [HideInInspector]
    public float RotHInput;
    [HideInInspector]
    public float RotVInput;

    [HideInInspector]
    public GameObject GO;
    public Canvas FadeCanvas;

    Dictionary<string, GameObject> ObserveObj = new Dictionary<string,GameObject>();

    public bool IsOnObserveMode = false;
    bool IsObjRotate = false;

    private PlayerInterActionObj CurrentTargetObj;
    public void SetRotateInput(Vector2 value)
    {
        RotHInput = value.x;
        RotVInput = value.y;

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
        if(GO != null)
        {
            GO.transform.position = CameraManager.Instance.ObserveCamera.transform.position + (CameraManager.Instance.ObserveCamera.transform.forward * 2.5f);
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

    public void AddObserveItem(string Key, GameObject go)
    {
        ObserveObj.Add(Key, go);
    }

    public void ActivateObserverItem(string Key , PlayerInterActionObj Target) // 관찰자모드 아이템 활성화
    {

        CurrentTargetObj = Target;

        CameraManager.Instance.CaptureCamera.gameObject.SetActive(true);
        FadeCanvas.gameObject.SetActive(true);
        CameraManager.Instance.ObserveCamera.gameObject.SetActive(true);

        IsOnObserveMode = true;
        if (ObserveObj.TryGetValue(Key, out GameObject go))
        {
            GO = Instantiate(go, CameraManager.Instance.ObserveCamera.transform.position + CameraManager.Instance.ObserveCamera.transform.forward, Quaternion.identity);
            GO.gameObject.layer = 18;
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
        }
    }

    public void DeactivateObserverItem() // 관찰자모드 아이템 비활성화
    {
        //GO.transform.eulerAngles = new Vector3(0, 0, 0);
        //GO.SetActive(false);
        Destroy(GO);
        GO = null;
        CameraManager.Instance.CaptureCamera.gameObject.SetActive(false);
        FadeCanvas.gameObject.SetActive(false);
        CameraManager.Instance.ObserveCamera.gameObject.SetActive(false);
        IsOnObserveMode = false;
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
        if (CurrentTargetObj != null)
        {
            CurrentTargetObj.SecondInteractOn();
        }
        
    }
}
