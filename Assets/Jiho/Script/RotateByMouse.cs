using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMouse : MonoBehaviour
{
    public float RotHInput;
    public float RotVInput;

    public GameObject GO;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    public void SetRotateInput(Vector2 value)
    {
        RotHInput = value.x;
        RotVInput = value.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(RotHInput) > 0.1f)
        {
            GO.transform.Rotate(Vector3.up, -RotHInput);
        }

        if (Mathf.Abs(RotVInput) > 0.1f)
        {
            GO.transform.Rotate(Vector3.right, RotVInput);
        }
    }
}
