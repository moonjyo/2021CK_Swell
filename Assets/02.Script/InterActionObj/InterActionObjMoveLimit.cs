using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionObjMoveLimit : MonoBehaviour
{

    public bool IsLimmit = false;   
    public Vector2 LimitValue;

    public bool IsMoveShelf = false;
    // Update is called once per frame
    void Update()
    {

        if (IsLimmit)
        {
            if (transform.localPosition.z >= LimitValue.x)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, LimitValue.x);
                IsMoveShelf = false;
            }
            else if (transform.localPosition.z <= LimitValue.y)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, LimitValue.y);
                IsMoveShelf = true;
            }
            else
            {
                IsMoveShelf = true;
            }
        }
    }
}
