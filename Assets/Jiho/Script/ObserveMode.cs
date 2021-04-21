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

    Dictionary<int,GameObject> ObserveObj = new Dictionary<int,GameObject>();
    public void SetRotateInput(Vector2 value)
    {
        RotHInput = value.x;
        RotVInput = value.y;

    }

    void Update()
    {
        RotateObj();
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

    public void AddObserveItem(int Keynum, GameObject go)
    {
        ObserveObj.Add(Keynum, go);
    }

    public void ActivateObserverItem(int Keynum) // 관찰자모드 아이템 활성화
    {
        if(ObserveObj.TryGetValue(Keynum, out GameObject go))
        {
            go.SetActive(true);
            GO = go;
        }
    }

    public void DeactivateObserverItem() // 관찰자모드 아이템 비활성화
    {
        GO.SetActive(false);
        GO = null;
    }

    public void EnterObserveMode() // 관찰자모드 Enter
    {
        
    }

    public void DummyExit() // 관찰자 모드 Exit
    {
        FadeCanvas.gameObject.SetActive(false);
        GO.SetActive(false);
    }
}
