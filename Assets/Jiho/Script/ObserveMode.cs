using System.Collections;
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

    bool IsOnObserveMode = false;
    public void SetRotateInput(Vector2 value)
    {
        RotHInput = value.x;
        RotVInput = value.y;

    }

    void Update()
    {
        if(IsOnObserveMode)
        {
            RotateObj();
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
            GO.transform.RotateAround(GO.transform.position, Vector3.right, RotVInput * 40 * Time.deltaTime);
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
        GO.SetActive(true);
        //
        IsOnObserveMode = true;
        //if (ObserveObj.TryGetValue(Key, out GameObject go))
        //{
        //    go.SetActive(true);
        //    //go.transform.position = 
        //    GO = go;
        //}
        //else
        //{
        //    FadeCanvas.gameObject.SetActive(false);
        //}
    }

    public void DeactivateObserverItem() // 관찰자모드 아이템 비활성화
    {
        GO.transform.eulerAngles = new Vector3(0, 0, 0);
        GO.SetActive(false);
        GO = null;

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
