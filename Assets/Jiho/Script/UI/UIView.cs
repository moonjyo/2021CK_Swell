using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    public RectTransform RootRect;
    private Vector3 OriginalPos;

    protected void Awake()
    {
        OriginalPos = RootRect.localPosition;
    }

    public virtual void Initialize()
    {

    }

    public virtual void Toggle(bool value)
    {
        if(value)
        {
            RootRect.localPosition = Vector3.zero;
        }
        else
        {
            RootRect.localPosition = OriginalPos;
        }

        RootRect.gameObject.SetActive(value);
    }
}
