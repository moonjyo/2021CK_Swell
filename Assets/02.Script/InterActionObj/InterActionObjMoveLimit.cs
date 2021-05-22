using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionObjMoveLimit : MonoBehaviour
{

    public bool IsLimmit = false;   
    public Vector2 LimitValue;
    // Update is called once per frame
    void Update()
    {

        if (IsLimmit)
        {
            if (transform.localPosition.z > LimitValue.x)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.7f);
            }
            else if (transform.localPosition.z < LimitValue.y)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.4f);
            }
        }
    }
}
