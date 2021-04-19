using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMouse : MonoBehaviour
{
    [HideInInspector]
    public float RotHInput;
    [HideInInspector]
    public float RotVInput;

    public GameObject GO;

    public Canvas FadeCanvas;

    public void SetRotateInput(Vector2 value)
    {
        RotHInput = value.x;
        RotVInput = value.y;
    }

    void Update()
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

    public void EnterObserveMode()
    {
        // 관찰자모드 Enter
    }

    public void DummyExit()
    {
        FadeCanvas.gameObject.SetActive(false);
        GO.SetActive(false);
    }
}
